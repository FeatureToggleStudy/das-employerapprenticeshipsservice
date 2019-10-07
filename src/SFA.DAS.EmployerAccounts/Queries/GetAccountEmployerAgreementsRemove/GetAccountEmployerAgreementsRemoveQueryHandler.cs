using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.Commitments.Api.Client.Interfaces;
using SFA.DAS.Commitments.Api.Types;
using SFA.DAS.EmployerAccounts.Data;
using SFA.DAS.EmployerAccounts.Features;
using SFA.DAS.EmployerAccounts.Models.EmployerAgreement;
using SFA.DAS.HashingService;
using SFA.DAS.Validation;

namespace SFA.DAS.EmployerAccounts.Queries.GetAccountEmployerAgreementsRemove
{
    public class GetAccountEmployerAgreementsRemoveQueryHandler : IAsyncRequestHandler<GetAccountEmployerAgreementsRemoveRequest, GetAccountEmployerAgreementsRemoveResponse>
    {
        private readonly IValidator<GetAccountEmployerAgreementsRemoveRequest> _validator;
        private readonly IEmployerAgreementRepository _employerAgreementRepository;
        private readonly IHashingService _hashingService;
        private readonly IEmployerCommitmentApi _employerCommitmentApi;
        private readonly IDasRecruitService _dasRecruitService;

        public GetAccountEmployerAgreementsRemoveQueryHandler(IValidator<GetAccountEmployerAgreementsRemoveRequest> validator, IEmployerAgreementRepository employerAgreementRepository, IHashingService hashingService, IEmployerCommitmentApi employerCommitmentApi, IDasRecruitService dasRecruitService)
        {
            _validator = validator;
            _employerAgreementRepository = employerAgreementRepository;
            _hashingService = hashingService;
            _employerCommitmentApi = employerCommitmentApi;
            _dasRecruitService = dasRecruitService;
        }

        public async Task<GetAccountEmployerAgreementsRemoveResponse> Handle(GetAccountEmployerAgreementsRemoveRequest message)
        {
            var validationResult = await _validator.ValidateAsync(message);

            if (!validationResult.IsValid())
            {
                throw new InvalidRequestException(validationResult.ValidationDictionary);
            }
            if (validationResult.IsUnauthorized)
            {
                throw new UnauthorizedAccessException();
            }

            var accountId = _hashingService.DecodeValue(message.HashedAccountId);

            var result = await _employerAgreementRepository.GetEmployerAgreementsToRemove(accountId);
            
            var commitments = new List<ApprenticeshipStatusSummary>();

            if (result != null && result.Count == 1)
            {
                result.First().CanBeRemoved = false;
            }
            else
            {
                commitments = await _employerCommitmentApi.GetEmployerAccountSummary(accountId);
            }

            if (result == null) return new GetAccountEmployerAgreementsRemoveResponse();

            if (result != null)
            {
                foreach (var removeEmployerAgreementView in result)
                {
                    removeEmployerAgreementView.HashedAgreementId =
                        _hashingService.HashValue((long) removeEmployerAgreementView.Id);
                    removeEmployerAgreementView.HashedAccountId = message.HashedAccountId;

                    var legalAgreementId = _hashingService.DecodeValue(removeEmployerAgreementView.HashedAgreementId);

                    var agreement = await _employerAgreementRepository.GetEmployerAgreement(legalAgreementId);

                    var vacanciesSummary = await _dasRecruitService.GetVacanciesByLegalEntity(message.HashedAccountId, agreement.LegalEntityId, 10);
                    
                    if (result.Count == 1) continue;

                    switch (removeEmployerAgreementView.Status)
                    {
                        case EmployerAgreementStatus.Pending:
                            removeEmployerAgreementView.CanBeRemoved = true;
                            break;
                        case EmployerAgreementStatus.Signed:
                            var commitmentConnectedToEntity = commitments.FirstOrDefault(c =>
                                !string.IsNullOrEmpty(c.LegalEntityIdentifier)
                                && c.LegalEntityIdentifier.Equals(removeEmployerAgreementView.LegalEntityCode)
                                && c.LegalEntityOrganisationType == removeEmployerAgreementView.LegalEntitySource);

                            if (vacanciesSummary.Vacancies.Any())
                            {
                                removeEmployerAgreementView.CanBeRemoved = false;
                                removeEmployerAgreementView.HasVacancies = true;
                            }
                            if (commitmentConnectedToEntity != null &&
                                (commitmentConnectedToEntity.ActiveCount +
                                 commitmentConnectedToEntity.PendingApprovalCount +
                                 commitmentConnectedToEntity.PausedCount) != 0)
                            {
                                removeEmployerAgreementView.CanBeRemoved = false;
                                removeEmployerAgreementView.HasCommitments = true;
                            }
                            break;
                        default:
                            removeEmployerAgreementView.CanBeRemoved = false;
                            break;
                    }
                }
            }

            return new GetAccountEmployerAgreementsRemoveResponse { Agreements = result };
        }
    }
}