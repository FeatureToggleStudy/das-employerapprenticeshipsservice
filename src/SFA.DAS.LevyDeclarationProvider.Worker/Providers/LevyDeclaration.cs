﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Azure;
using SFA.DAS.EAS.Application.Commands.CreateEnglishFractionCalculationDate;
using SFA.DAS.EAS.Application.Commands.RefreshEmployerLevyData;
using SFA.DAS.EAS.Application.Commands.UpdateEnglishFractions;
using SFA.DAS.EAS.Application.Messages;
using SFA.DAS.EAS.Application.Queries.GetEnglishFractionUpdateRequired;
using SFA.DAS.EAS.Application.Queries.GetHMRCLevyDeclaration;
using SFA.DAS.EAS.Domain.Interfaces;
using SFA.DAS.EAS.Domain.Models.HmrcLevy;
using SFA.DAS.EAS.Domain.Models.Levy;
using SFA.DAS.Messaging.AzureServiceBus.Attributes;
using SFA.DAS.Messaging.Interfaces;
using SFA.DAS.NLog.Logger;

namespace SFA.DAS.EAS.LevyDeclarationProvider.Worker.Providers
{
    [TopicSubscription("MA_LevyDeclaration")]
    public class LevyDeclaration : ILevyDeclaration
    {
        private readonly IMessageSubscriberFactory _messageSubscriberFactory;
        private readonly IMediator _mediator;
        private readonly ILog _logger;
        private readonly IDasAccountService _dasAccountService;

        private static bool HmrcProcessingEnabled => CloudConfigurationManager.GetSetting("DeclarationsEnabled")
                                      .Equals("both", StringComparison.CurrentCultureIgnoreCase);

        private static bool DeclarationProcessingOnly => CloudConfigurationManager.GetSetting("DeclarationsEnabled")
            .Equals("declarations", StringComparison.CurrentCultureIgnoreCase);

        private static bool FractionProcessingOnly => CloudConfigurationManager.GetSetting("DeclarationsEnabled")
            .Equals("fractions", StringComparison.CurrentCultureIgnoreCase);

        public LevyDeclaration(IMessageSubscriberFactory messageSubscriberFactory, IMediator mediator,
            ILog logger, IDasAccountService dasAccountService)
        {
            _messageSubscriberFactory = messageSubscriberFactory;
            _mediator = mediator;
            _logger = logger;
            _dasAccountService = dasAccountService;
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            using (var subscriber = _messageSubscriberFactory.GetSubscriber<EmployerRefreshLevyQueueMessage>())
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var message = await subscriber.ReceiveAsAsync();

                    if(message == null) continue;

                    try
                    {
                        if (HmrcProcessingEnabled || DeclarationProcessingOnly || FractionProcessingOnly)
                        {
                            await ProcessMessage(message);
                        }
                        else
                        {
                            //Ignore the message as we are not processing declarations

                            if (message.Content != null)
                            {
                                await message.CompleteAsync();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Fatal(ex,
                            $"Levy declaration processing failed for account with ID [{message.Content?.AccountId}]");
                        break; //Stop processing anymore messages as this failure needs to be investigated
                    }
                }
            }
        }

        private async Task ProcessMessage(IMessage<EmployerRefreshLevyQueueMessage> message)
        {
            if (message?.Content == null)
            {
                if (message != null)
                {
                    await message.CompleteAsync();
                }
                return;
            }
            var timer = Stopwatch.StartNew();

            var employerAccountId = message.Content.AccountId;
            var payeRef = message.Content.PayeRef;

            _logger.Trace($"Processing LevyDeclaration for {employerAccountId} paye scheme {payeRef}");
            
            var englishFractionUpdateResponse = await _mediator.SendAsync(new GetEnglishFractionUpdateRequiredRequest());
            
            var payeSchemeDeclarations = await ProcessScheme(payeRef, englishFractionUpdateResponse);
            
            await RefreshEmployerAccountLevyDeclarations(employerAccountId, payeSchemeDeclarations);
            
            await message.CompleteAsync();

            timer.Stop();

            _logger.Trace($"Finished processing LevyDeclaration for {employerAccountId} paye scheme {payeRef}. Completed in {timer.Elapsed:g} (hh:mm:ss:ms)");
        }

        private async Task RefreshEmployerAccountLevyDeclarations(long employerAccountId, ICollection<EmployerLevyData> payeSchemeDeclarations)
        {
            await _mediator.SendAsync(new RefreshEmployerLevyDataCommand
            {
                AccountId = employerAccountId,
                EmployerLevyData = payeSchemeDeclarations
            });
        }

        private async Task<ICollection<EmployerLevyData>> ProcessScheme(string payeRef, GetEnglishFractionUpdateRequiredResponse englishFractionUpdateResponse)
        {
            var payeSchemeDeclarations = new List<EmployerLevyData>();

            await UpdateEnglishFraction(payeRef, englishFractionUpdateResponse);

            var levyDeclarationQueryResult = HmrcProcessingEnabled || DeclarationProcessingOnly ?
                await _mediator.SendAsync(new GetHMRCLevyDeclarationQuery {EmpRef = payeRef }) : null;

            if (levyDeclarationQueryResult?.LevyDeclarations?.Declarations != null)
            {
                var declarations = CreateDasDeclarations(levyDeclarationQueryResult);

                var employerData = new EmployerLevyData
                {
                    EmpRef = payeRef,
                    Declarations = {Declarations = declarations}
                };

                payeSchemeDeclarations.Add(employerData);
            }

            return payeSchemeDeclarations;
        }

        private static List<DasDeclaration> CreateDasDeclarations(GetHMRCLevyDeclarationResponse levyDeclarationQueryResult)
        {
            var declarations = new List<DasDeclaration>();

            foreach (var declaration in levyDeclarationQueryResult.LevyDeclarations.Declarations)
            {
                var dasDeclaration = new DasDeclaration
                {
                    SubmissionDate = DateTime.Parse(declaration.SubmissionTime),
                    Id = declaration.Id,
                    PayrollMonth = declaration.PayrollPeriod?.Month,
                    PayrollYear = declaration.PayrollPeriod?.Year,
                    LevyAllowanceForFullYear = declaration.LevyAllowanceForFullYear,
                    LevyDueYtd = declaration.LevyDueYearToDate,
                    NoPaymentForPeriod = declaration.NoPaymentForPeriod,
                    DateCeased = declaration.DateCeased,
                    InactiveFrom = declaration.InactiveFrom,
                    InactiveTo = declaration.InactiveTo,
                    SubmissionId = declaration.SubmissionId
                };

                declarations.Add(dasDeclaration);
            }

            return declarations;
        }

        private async Task UpdateEnglishFraction(string payeRef,
            GetEnglishFractionUpdateRequiredResponse englishFractionUpdateResponse)
        {
            if (HmrcProcessingEnabled || FractionProcessingOnly)
            {
                await _mediator.SendAsync(new UpdateEnglishFractionsCommand
                {
                    EmployerReference = payeRef,
                    EnglishFractionUpdateResponse = englishFractionUpdateResponse
                });

                await _dasAccountService.UpdatePayeScheme(payeRef);
            }

            if (englishFractionUpdateResponse.UpdateRequired)
            {
                await _mediator.SendAsync(new CreateEnglishFractionCalculationDateCommand
                {
                    DateCalculated = englishFractionUpdateResponse.DateCalculated
                });
            }
        }
    }
}

