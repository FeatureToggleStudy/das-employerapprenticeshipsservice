﻿using System.ComponentModel.DataAnnotations;
using MediatR;
using SFA.DAS.EAS.Application.Attributes;
using SFA.DAS.EAS.Application.Formatters.TransactionDowloads;
using SFA.DAS.EAS.Application.Messages;
using SFA.DAS.EAS.Domain;

namespace SFA.DAS.EAS.Application.Queries.GetTransactionsDownload
{
    public class GetTransactionsDownloadQuery : IAsyncRequest<GetTransactionsDownloadResponse>
    {
        [Required]
        [RegularExpression(Constants.HashedAccountIdRegex)]
        public string HashedAccountId { get; set; }

        [Display(Name = "Start date")]
        [Required]
        [Month, Year, Date]
        public MonthYear StartDate { get; set; }

        [Display(Name = "End date")]
        [Required]
        [Month, Year, Date]
        public MonthYear EndDate { get; set; }

        [Required]
        public DownloadFormatType? DownloadFormat { get; set; }
    }
}