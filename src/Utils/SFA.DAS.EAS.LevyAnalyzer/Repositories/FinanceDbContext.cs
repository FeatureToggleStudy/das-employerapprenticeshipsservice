﻿using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Threading.Tasks;
using SFA.DAS.EAS.LevyAnalyser.Models;

namespace SFA.DAS.EAS.LevyAnalyser.Repositories
{
    public class FinanceDbContext : DbContext 
    {
        public FinanceDbContext(DbConnection connection) : base(connection, true)
        {
            // just call base    
        }

        public Task<TransactionLine[]> GetTransactionsAsync(long accountId)
        {
            return Database.SqlQuery<TransactionLine>(
                    $"SELECT * FROM [employer_financial].TransactionLine WHERE AccountId = {accountId} ORDER BY DateCreated")
                .ToArrayAsync();
        }

        public Task<LevyDeclaration[]> GetLevyDeclarationsAsync(long accountId)
        {
            return Database.SqlQuery<LevyDeclaration>(
                    $"SELECT * FROM [employer_financial].LevyDeclaration WHERE AccountId = {accountId} OR empRef IN (SELECT empRef FROM [employer_financial].LevyDeclaration ld2 WHERE ld2.AccountId = {accountId}) ORDER BY CreatedDate")
                .ToArrayAsync();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.HasDefaultSchema("employer_financial");
        }
    }
}