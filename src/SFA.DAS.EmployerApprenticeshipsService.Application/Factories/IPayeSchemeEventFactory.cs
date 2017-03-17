﻿using SFA.DAS.EAS.Account.Api.Types.Events;

namespace SFA.DAS.EAS.Application.Factories
{
    public interface IPayeSchemeEventFactory
    {
        PayeSchemeAddedEvent CreatePayeSchemeAddedEvent(string hashedAccountId, string payeScheme);
        PayeSchemeRemovedEvent CreatePayeSchemeRemovedEvent(string hashedAccountId, string payeScheme);
    }
}
