﻿using System.Data;
using System.Threading.Tasks;
using Dapper;
using SFA.DAS.EAS.Domain.Configuration;
using SFA.DAS.EAS.Domain.Data.Repositories;

namespace SFA.DAS.EAS.TestCommon.DbCleanup
{
    public class CleanTransactionsDatabase : BaseRepository, ICleanTransactionsDatabase
    {
        public CleanTransactionsDatabase(LevyDeclarationProviderConfiguration configuration) : base(configuration)
        {
        }

        public async Task Execute()
        {

            var parameters = new DynamicParameters();
            parameters.Add("@INCLUDETOPUPTABLE", 1, DbType.Int16);
            await WithConnection(async c => await c.ExecuteAsync(
                "[employer_financial].[Cleardown]",
                parameters,
                commandType: CommandType.StoredProcedure));
            
        }
    }
}
