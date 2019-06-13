using System.Linq;
using SFA.DAS.EAS.Portal.Client.Types;

namespace SFA.DAS.EAS.Portal.Worker.TypesExtensions
{
    public static class AccountExtensions
    {
        public static (Organisation, EntityCreation) GetOrAddOrganisation(this Account account, long accountLegalEntityId)
        {
            var organisation = account.Organisations.SingleOrDefault(o => o.AccountLegalEntityId == accountLegalEntityId);
            if (organisation == null)
            {
                organisation = new Organisation {AccountLegalEntityId = accountLegalEntityId};
                account.Organisations.Add(organisation);
                return (organisation, EntityCreation.Created);
            }

            return (organisation, EntityCreation.Existed);
        }

    }
}