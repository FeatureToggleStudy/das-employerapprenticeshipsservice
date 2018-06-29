﻿using MediatR;
using SFA.DAS.EAS.Application.Exceptions;
using SFA.DAS.EAS.Application.Validation;
using SFA.DAS.EAS.Domain.Data.Repositories;
using SFA.DAS.EAS.Messages.Events;
using SFA.DAS.NServiceBus;
using System.Threading.Tasks;

namespace SFA.DAS.EAS.Application.Commands.CreateEmployerAgreement
{
    public class CreateEmployerAgreementCommandHandler : AsyncRequestHandler<CreateEmployerAgreementCommand>
    {
        private readonly IMediator _mediator;
        private readonly IEmployerAgreementRepository _employerAgreementRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IValidator<CreateEmployerAgreementCommand> _validator;

        public CreateEmployerAgreementCommandHandler(
            IMediator mediator,
            IEmployerAgreementRepository employerAgreementRepository,
            IEventPublisher eventPublisher,
            IValidator<CreateEmployerAgreementCommand> validator)
        {
            _mediator = mediator;
            _employerAgreementRepository = employerAgreementRepository;
            _eventPublisher = eventPublisher;
            _validator = validator;
        }

        protected override async Task HandleCore(CreateEmployerAgreementCommand message)
        {
            var validationResult = _validator.Validate(message);

            if (!validationResult.IsValid())
                throw new InvalidRequestException(validationResult.ValidationDictionary);

            await SetAccountLegalEntityAgreementStatus(message.AccountId, message.LegalEntityId);

            var newAgreementId = await _employerAgreementRepository.CreateEmployerAgreeement(
                                message.LatestTemplateId,
                                message.AccountId,
                                message.LegalEntityId);

            await _eventPublisher.Publish<CreatedAgreementEvent>(c =>
            {
                c.AccountId = message.AccountId;
                c.AgreementId = newAgreementId;
                c.LegalEntityId = message.LegalEntityId;
            });
        }

        private Task SetAccountLegalEntityAgreementStatus(long accountId, long legalEntityId)
        {
            return _employerAgreementRepository.EvaluateEmployerLegalEntityAgreementStatus(accountId, legalEntityId);
        }
    }
}