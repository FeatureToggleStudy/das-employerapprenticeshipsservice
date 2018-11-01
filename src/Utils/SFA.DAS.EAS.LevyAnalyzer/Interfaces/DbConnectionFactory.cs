﻿using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace SFA.DAS.EAS.LevyAnalyzer.Interfaces
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly IConfigProvider _configProvider;

        public DbConnectionFactory(IConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }

        public DbConnection GetConnection(string name)
        {
            var dbConfig = _configProvider.Get<DatabaseConfig>();

            var connectionString = dbConfig.ConnectionStrings.First(cs =>
                string.Equals(name, cs.Name, StringComparison.InvariantCultureIgnoreCase));

            return new SqlConnection(connectionString.ConnectionString);
        }
    }
}