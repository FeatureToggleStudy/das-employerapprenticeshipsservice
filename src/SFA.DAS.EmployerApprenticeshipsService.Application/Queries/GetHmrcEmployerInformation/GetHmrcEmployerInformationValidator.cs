﻿using System.Threading.Tasks;
using SFA.DAS.Validation;

namespace SFA.DAS.EAS.Application.Queries.GetHmrcEmployerInformation
{
    public class GetHmrcEmployerInformationValidator
    {
        public ValidationResult Validate(GetHmrcEmployerInformationQuery item)
        {
            var validationResult = new ValidationResult();

            if (string.IsNullOrWhiteSpace(item.AuthToken))
            {
                validationResult.AddError(nameof(item.AuthToken),"AuthToken has not been supplied");
            }
            
            return validationResult;
        }

        public Task<ValidationResult> ValidateAsync(GetHmrcEmployerInformationQuery item)
        {
            throw new System.NotImplementedException();
        }
    }
}