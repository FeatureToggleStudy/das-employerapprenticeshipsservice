﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using NServiceBus;
using SFA.DAS.EAS.Domain.Configuration;
using SFA.DAS.EAS.Domain.Extensions;
using SFA.DAS.EAS.Infrastructure.Data;
using SFA.DAS.EAS.Messages.Events;
using SFA.DAS.NLog.Logger;
using SFA.DAS.Notifications.Api.Client;
using SFA.DAS.Notifications.Api.Types;

namespace SFA.DAS.EAS.MessageHandlers.EventHandlers
{
    public class ApprovedTransferConnectionRequestEventNotificationHandler : IHandleMessages<ApprovedTransferConnectionRequestEvent>
    {
        public const string UrlFormat = "/accounts/{0}/transfers";

        private readonly EmployerApprenticeshipsServiceConfiguration _config;
        private readonly ILog _logger;
        private readonly INotificationsApi _notificationsApi;
        private readonly Lazy<EmployerAccountDbContext> _db;

        public ApprovedTransferConnectionRequestEventNotificationHandler(
            EmployerApprenticeshipsServiceConfiguration config,
            ILog logger,
            INotificationsApi notificationsApi,
            Lazy<EmployerAccountDbContext> db)
        {
            _config = config;
            _logger = logger;
            _notificationsApi = notificationsApi;
            _db = db;
        }

        public async Task Handle(ApprovedTransferConnectionRequestEvent message, IMessageHandlerContext context)
        {
            var users = await _db.Value.Users.WhereReceiveNotifications(message.SenderAccountId).ToListAsync();

            if (!users.Any())
            {
                _logger.Info($"There are no users that receive notifications for SenderAccountId '{message.SenderAccountId}'");
            }

            foreach (var user in users)
            {
                try
                {
                    var email = new Email
                    {
                        RecipientsAddress = user.Email,
                        TemplateId = "TransferConnectionRequestApproved",
                        ReplyToAddress = "noreply@sfa.gov.uk",
                        Subject = "x",
                        SystemId = "x",
                        Tokens = new Dictionary<string, string>
                        {
                            {"name", user.FirstName},
                            {"account_name", message.ReceiverAccountName},
                            {
                                "link_notification_page",
                                $"{_config.DashboardUrl}{string.Format(UrlFormat, message.SenderAccountHashedId)}"
                            }
                        }
                    };

                    await _notificationsApi.SendEmail(email);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, $"Unable to send approved transfer request notification to UserId '{user.Id}' for SenderAccountId '{message.SenderAccountId}'");
                }
            }
        }
    }
}