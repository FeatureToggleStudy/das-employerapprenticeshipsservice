﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.EmployerAccounts.Jobs.Data
{
    public interface IPopulateRepository
    {
        Task<IEnumerable<MembershipUser>> GetAllAccountUsers();
        Task<bool> HasJobRun(string job);
        Task MarkJobAsRan(string job);
    }
}
