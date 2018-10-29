﻿using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using SFA.DAS.Authentication;
using SFA.DAS.Authorization;
using SFA.DAS.Common.Domain.Types;
using SFA.DAS.EAS.Domain.Interfaces;
using SFA.DAS.EAS.Web.Extensions;
using SFA.DAS.EAS.Web.Helpers;
using SFA.DAS.EAS.Web.Orchestrators;
using SFA.DAS.EAS.Web.ViewModels;
using SFA.DAS.EAS.Web.ViewModels.Organisation;
using SFA.DAS.NLog.Logger;

namespace SFA.DAS.EAS.Web.Controllers
{
    public class OrganisationSharedController : BaseController
    {
        private readonly OrganisationOrchestrator _orchestrator;
        private readonly IMapper _mapper;

        public OrganisationSharedController(IAuthenticationService owinWrapper,
            OrganisationOrchestrator orchestrator,
            IAuthorizationService authorization,
            IMultiVariantTestingService multiVariantTestingService,
            IMapper mapper,
            ILog logger,
            ICookieStorageService<FlashMessageViewModel> flashMessage)
            : base(owinWrapper, multiVariantTestingService, flashMessage)

        {
            _orchestrator = orchestrator;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("accounts/{HashedAccountId}/organisations/custom/add", Order = 0)]
        [Route("accounts/organisations/custom/add", Order = 1)]
        public ActionResult AddCustomOrganisationDetails(string hashedAccountId)
        {
            OrchestratorResponse<OrganisationDetailsViewModel> response = null;

            if (!string.IsNullOrWhiteSpace(hashedAccountId))
            {
                response = _orchestrator.GetAddOtherOrganisationViewModel(hashedAccountId);
            }
            else
            {
                response = new OrchestratorResponse<OrganisationDetailsViewModel>
                {
                    Data = new OrganisationDetailsViewModel()
                };

            }

            return View(ControllerConstants.AddOtherOrganisationDetailsViewName, response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("accounts/{HashedAccountId}/organisations/custom/add", Order = 0)]
        [Route("accounts/organisations/custom/add", Order = 1)]
        public async Task<ActionResult> AddOtherOrganisationDetails(OrganisationDetailsViewModel model)
        {
            var response = await _orchestrator.ValidateLegalEntityName(model);

            if (response.Status == HttpStatusCode.BadRequest)
            {
                return View(ControllerConstants.AddOtherOrganisationDetailsViewName, response);
            }

            model.Type = OrganisationType.Other;

            var addressModel = new OrchestratorResponse<FindOrganisationAddressViewModel>
            {
                Data = _mapper.Map<FindOrganisationAddressViewModel>(response.Data)
            };

            return View(ControllerConstants.FindAddressViewName, addressModel);
        }

        [HttpPost]
        [Route("accounts/{HashedAccountId}/organisations/address/find", Order = 0)]
        [Route("accounts/organisations/address/find", Order = 1)]
        public ActionResult FindAddress(FindOrganisationAddressViewModel request)
        {
            var response = new OrchestratorResponse<FindOrganisationAddressViewModel>
            {
                Data = request,
                Status = HttpStatusCode.OK
            };

            if (RouteData.Values[ControllerConstants.AccountHashedIdRouteKeyName] == null && !string.IsNullOrEmpty(request.OrganisationAddress))
            {
                var organisationDetailsViewModel = _orchestrator.StartConfirmOrganisationDetails(request);

                organisationDetailsViewModel.Data.CreateOrganisationCookie(_orchestrator, HttpContext);

                return RedirectToAction(ControllerConstants.GatewayInformViewName, ControllerConstants.EmployerAccountControllerName);
            }

            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("accounts/{HashedAccountId}/organisations/address/select", Order = 0)]
        [Route("accounts/organisations/address/select", Order = 1)]
        public async Task<ActionResult> SelectAddress(FindOrganisationAddressViewModel request)
        {
            var response = await _orchestrator.GetAddressesFromPostcode(request);

            if (response?.Data?.Addresses != null && response.Data.Addresses.Count == 1)
            {
                var viewModel = _mapper.Map<AddOrganisationAddressViewModel>(request);

                viewModel.Address = response.Data.Addresses.Single();

                var addressResponse = new OrchestratorResponse<AddOrganisationAddressViewModel>
                {
                    Data = viewModel,
                    Status = HttpStatusCode.OK
                };

                return View(ControllerConstants.AddOrganisationAddressViewName, addressResponse);
            }

            return View(response);
        }

        [HttpGet]
        [Route("accounts/{HashedAccountId}/organisations/address/update", Order = 0)]
        [Route("accounts/organisations/address/update", Order = 1)]
        public ActionResult AddOrganisationAddress(AddOrganisationAddressViewModel request)
        {
            var actionResult = ReturnConfirmOrganisationDetailsViewIfHashedAccountIdIsNotPresentInTheRouteAndOrganisationAddressIsNotNullOrEmpty(request);

            if (actionResult != null)
            {
                return actionResult;
            }

            if (request.Address == null)
            {
                request.Address = new AddressViewModel();
            }

            var response = new OrchestratorResponse<AddOrganisationAddressViewModel>
            {
                Data = request,
                Status = HttpStatusCode.OK
            };

            return View(ControllerConstants.AddOrganisationAddressViewName, response);
        }

        private ActionResult
            ReturnConfirmOrganisationDetailsViewIfHashedAccountIdIsNotPresentInTheRouteAndOrganisationAddressIsNotNullOrEmpty(
                AddOrganisationAddressViewModel request)
        {
            if (RouteData.Values[ControllerConstants.AccountHashedIdRouteKeyName] != null ||
                string.IsNullOrEmpty(request.OrganisationAddress))
            {
                return null;
            }
            var organisationDetailsViewModel = _orchestrator.StartConfirmOrganisationDetails(request);
            
            return View(ControllerConstants.ConfirmOrganisationDetailsViewName, organisationDetailsViewModel);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("accounts/{HashedAccountId}/organisations/address/update", Order = 0)]
        [Route("accounts/organisations/address/update", Order = 1)]
        public ActionResult UpdateOrganisationAddress(AddOrganisationAddressViewModel request)
        {
            return Redirect(Url.EmployerAccountsAction(("accounts/{HashedAccountId}/organisations/address/update")));
        }
    }
}