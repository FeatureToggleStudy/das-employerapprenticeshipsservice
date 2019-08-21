﻿using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using SFA.DAS.EAS.Account.Api.Attributes;
using SFA.DAS.EAS.Account.Api.Orchestrators;
using SFA.DAS.EAS.Domain.Configuration;

namespace SFA.DAS.EAS.Account.Api.Controllers
{
    [RoutePrefix("api/accounts/{hashedAccountId}/payeschemes")]
    public class AccountPayeSchemesController : ApiController
    {
        private readonly AccountsOrchestrator _orchestrator;
        private readonly EmployerAccountsApiConfiguration _employerAccountsApiconfiguration;

        public AccountPayeSchemesController(AccountsOrchestrator orchestrator, EmployerAccountsApiConfiguration employerAccountsApiconfiguration)
        {
            _orchestrator = orchestrator;
            _employerAccountsApiconfiguration = employerAccountsApiconfiguration;
        }

        [Route("", Name = "GetPayeSchemes")]
        [ApiAuthorize(Roles = "ReadAllEmployerAccountBalances")]
        [HttpGet]
        public async Task<IHttpActionResult> GetPayeSchemes(string hashedAccountId)
        {
            var result = await _orchestrator.GetAccount(hashedAccountId);

            if (result.Data == null)
            {
                return NotFound();
            }

            return Ok(result.Data.PayeSchemes);
        }

        [Route("{payeschemeref}", Name = "GetPayeScheme")]
        [ApiAuthorize(Roles = "ReadAllEmployerAccountBalances")]
        [HttpGet]
        public IHttpActionResult GetPayeScheme(string hashedAccountId, string payeSchemeRef)
        {
            return Redirect($"{_employerAccountsApiconfiguration.BaseUrl}/api/accounts/{hashedAccountId}/payeschemes/{HttpUtility.UrlEncode(payeSchemeRef)}");
        }
    }
}