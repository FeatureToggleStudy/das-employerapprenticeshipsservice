﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.EAS.TestCommon.TestRepositories
{
    public interface ITestTransactionRepository
    {
        Task SetTransactionLineDateCreatedToTransactionDate(IEnumerable<long> submissionIds);

        Task SetTransactionLineDateCreatedToTransactionDate(IDictionary<long, DateTime?> submissionIds);
    }
}
