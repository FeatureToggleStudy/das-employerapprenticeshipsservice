﻿namespace SFA.DAS.EmployerFinance.Api.IntegrationTests.TestUtils.DataAccess.Dtos
{
    public class LegalEnityWithAgreementOutput
    {
        public long LegalEntityId { get; set; }
        public long EmployerAgreementId { get; set; }
        public string HashedAgreementId { get; set; }
    }
}