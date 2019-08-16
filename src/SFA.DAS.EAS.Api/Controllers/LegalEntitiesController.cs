﻿using System.Threading.Tasks;
using System.Web.Http;
using MediatR;
using SFA.DAS.EAS.Account.Api.Attributes;
using SFA.DAS.EAS.Application.Queries.GetLegalEntity;
using SFA.DAS.EAS.Domain.Configuration;
using SFA.DAS.Validation.WebApi;

namespace SFA.DAS.EAS.Account.Api.Controllers
{
    [RoutePrefix("api/accounts/{hashedAccountId}/legalentities")]
    public class LegalEntitiesController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly EmployerApprenticeshipsServiceConfiguration _configuration;

        public LegalEntitiesController(IMediator mediator, EmployerApprenticeshipsServiceConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        [Route("", Name = "GetLegalEntities")]
        [ApiAuthorize(Roles = "ReadAllEmployerAccountBalances")]
        [HttpGet]
        public async Task<IHttpActionResult> GetLegalEntities(string hashedAccountId)
        {
            return Redirect(_configuration.EmployerAccountsApiBaseUrl + $"/api/accounts/{hashedAccountId}/legalentities");
        }

        [Route("{legalEntityId}", Name = "GetLegalEntity")]
        [ApiAuthorize(Roles = "ReadAllEmployerAccountBalances")]
        [HttpNotFoundForNullModel]
        public async Task<IHttpActionResult> GetLegalEntity([FromUri] GetLegalEntityQuery query)
        {
            return Redirect(_configuration.EmployerAccountsApiBaseUrl + $"/api/accounts/{query.AccountHashedId}/legalentities/{query.LegalEntityId}");
        }
    }
}