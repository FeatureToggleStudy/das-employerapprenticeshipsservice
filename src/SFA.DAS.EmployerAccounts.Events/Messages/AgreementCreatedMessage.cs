﻿namespace SFA.DAS.EmployerAccounts.Events.Messages
{
    public class AgreementCreatedMessage
    {
        public long AccountId { get; set; }
        public long LegalEntityId { get; set; }
        public long AgreementId { get; set; }
    }
}
