﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Moq;
using NUnit.Framework;
using SFA.DAS.EmployerAccounts.Configuration;
using SFA.DAS.EmployerAccounts.Interfaces;
using SFA.DAS.EmployerAccounts.Models.EmployerAgreement;
using SFA.DAS.EmployerAccounts.Queries.GetOrganisationsForAccount;
using SFA.DAS.EmployerAccounts.Web.Orchestrators;
using SFA.DAS.NLog.Logger;
using SFA.DAS.Validation;

namespace SFA.DAS.EmployerAccounts.Web.UnitTests.Orchestrators.EmployerAgreementOrchestratorTests
{
    public class WhenIGetAListOfAgreementsToRemove
    {
        private Mock<IMediator> _mediator;
        private Mock<IReferenceDataService> _referenceDataService;
        private EmployerAgreementOrchestrator _orchestrator;

        private const string ExpectedHahsedAccountId = "RT456";
        private const string ExpectedUserId = "TYG68UY";

        [SetUp]
        public void Arrange()
        {
            _mediator = new Mock<IMediator>();
            _mediator.Setup(x => x.SendAsync(It.IsAny<GetOrganisationsForAccountRequest>()))
                .ReturnsAsync(new GetOrganisationsForAccountResponse
                {
                    Organisation = new List<RemoveOrganisationView>
                    {
                        new RemoveOrganisationView { Name = "Test Name", CanBeRemoved = false}
                    }
                });

            _referenceDataService = new Mock<IReferenceDataService>();

            _orchestrator = new EmployerAgreementOrchestrator(_mediator.Object, Mock.Of<IMapper>(), _referenceDataService.Object);
        }

        [Test]
        public async Task ThenTheMediatorIsCalledWithTheListOfOrganisations()
        {

            //Act
            await _orchestrator.GetOrganisationsToRemove(ExpectedHahsedAccountId, ExpectedUserId);

            //Assert
            _mediator.Verify(x => x.SendAsync(It.Is<GetOrganisationsForAccountRequest>(
                                c => c.HashedAccountId.Equals(ExpectedHahsedAccountId)
                                && c.UserId.Equals(ExpectedUserId))), Times.Once);
        }


        [Test]
        public async Task ThenIfAnInvalidRequestExceptionIsThrownTheOrchestratorResponseContainsTheError()
        {
            //Arrange
            _mediator.Setup(x => x.SendAsync(It.IsAny<GetOrganisationsForAccountRequest>())).ThrowsAsync(new InvalidRequestException(new Dictionary<string, string>()));

            //Act
            var actual = await _orchestrator.GetOrganisationsToRemove(ExpectedHahsedAccountId, ExpectedUserId);

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, actual.Status);
        }

        [Test]
        public async Task ThenIfAUnauthroizedAccessExceptionIsThrownThenTheOrchestratorResponseShowsAccessDenied()
        {
            //Arrange
            _mediator.Setup(x => x.SendAsync(It.IsAny<GetOrganisationsForAccountRequest>())).ThrowsAsync(new UnauthorizedAccessException());

            //Act
            var actual = await _orchestrator.GetOrganisationsToRemove(ExpectedHahsedAccountId, ExpectedUserId);

            //Assert
            Assert.AreEqual(HttpStatusCode.Unauthorized, actual.Status);
        }

        [Test]
        public async Task ThenTheValuesAreReturnedInTheResponseFromTheMediatorCall()
        {
            //Act
            var actual = await _orchestrator.GetOrganisationsToRemove(ExpectedHahsedAccountId, ExpectedUserId);

            //Assert
            Assert.IsTrue(actual.Data.Organisations.Any());
        }

    }
}
