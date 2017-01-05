﻿using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using SFA.DAS.EAS.Domain;
using SFA.DAS.EAS.Domain.Interfaces;
using SFA.DAS.EAS.Web.Authentication;
using SFA.DAS.EAS.Web.Models;
using SFA.DAS.EAS.Web.Orchestrators;

namespace SFA.DAS.EAS.Web.Controllers
{
    [RoutePrefix("accounts/{HashedAccountId}")]
    public class OrganisationController : BaseController
    {
        private readonly OrganisationOrchestrator _orchestrator;

        public OrganisationController(
            IOwinWrapper owinWrapper, 
            OrganisationOrchestrator orchestrator,
            IFeatureToggle featureToggle, 
            IUserWhiteList userWhiteList) 
            :base(owinWrapper, featureToggle, userWhiteList)
        {
            _orchestrator = orchestrator;
        }

        [HttpGet]
        [Route("Agreements/Add")]
        public async Task<ActionResult> AddOrganisation(string hashedAccountId)
        {
            var response = await _orchestrator.GetAddLegalEntityViewModel(hashedAccountId, OwinWrapper.GetClaimValue(@"sub"));
            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Agreements/Add")]
        public async Task<ActionResult> AddOrganisation(string hashedAccountId, OrganisationType orgType, string companiesHouseNumber, string publicBodyName, string charityRegNo)
        {
            var searchTerm = "";
            switch (orgType)
            {
                case OrganisationType.Charities:
                    searchTerm = charityRegNo;
                    break;
                case OrganisationType.CompaniesHouse:
                    searchTerm = companiesHouseNumber;
                    break;
                case OrganisationType.PublicBodies:
                    searchTerm = publicBodyName;
                    break;
                case OrganisationType.Other:
                    searchTerm = String.Empty;
                    break;
                default:
                    throw new NotImplementedException("Org Type Not Implemented");
            }

            var response = await _orchestrator.FindLegalEntity(hashedAccountId, orgType, searchTerm, OwinWrapper.GetClaimValue(@"sub"));

            if (response.Status == HttpStatusCode.OK)
            {
                return View("FindLegalEntity", response);
            }

            var errorResponse = new OrchestratorResponse<AddLegalEntityViewModel>
            {
                Data = new AddLegalEntityViewModel { HashedAccountId = hashedAccountId },
                Status = HttpStatusCode.OK,
            };

            if (response.Status == HttpStatusCode.NotFound)
            {
                switch (orgType)
                {
                    case OrganisationType.CompaniesHouse:
                        TempData["companyError"] = "Company not found";
                        break;
                    case OrganisationType.Charities:
                        TempData["charityError"] = "Charity not found";
                        break;
                    case OrganisationType.PublicBodies:
                        TempData["publicBodyError"] = "Public sector body not found";
                        break;
                    case OrganisationType.Other:
                        break;
                }
            }

            if (response.Status == HttpStatusCode.Conflict)
            {
                TempData["companyError"] = "Enter a company that isn't already registered";
            }


            if (orgType == OrganisationType.Charities)
            {
                var charityResult = (FindCharityViewModel)response.Data;
                if (charityResult.IsRemovedError)
                {
                    TempData["charityError"] = "Charity is removed";
                }
            }

            if (orgType == OrganisationType.PublicBodies)
            {
                var pbresult = (FindPublicBodyViewModel)response.Data;
                if (pbresult.Results.Data.Count == 1)
                {
                    return View("FindLegalEntity", response);
                }
                else
                {
                    return View("SelectPublicBody", response);
                }
            }

            return View("AddOrganisation", errorResponse);

        }


    }
}