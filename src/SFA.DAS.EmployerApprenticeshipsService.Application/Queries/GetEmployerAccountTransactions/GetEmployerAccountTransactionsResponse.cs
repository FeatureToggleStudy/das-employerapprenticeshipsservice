﻿using SFA.DAS.EAS.Domain.Models.Transaction;

namespace SFA.DAS.EAS.Application.Queries.GetEmployerAccountTransactions
{
    public class GetEmployerAccountTransactionsResponse
    {
        public AggregationData Data { get; set; }
        public bool AccountHasPreviousTransactions { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
    }
}