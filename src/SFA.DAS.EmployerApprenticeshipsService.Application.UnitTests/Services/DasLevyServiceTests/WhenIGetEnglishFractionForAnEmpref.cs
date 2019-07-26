﻿using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Moq;
using NUnit.Framework;
using SFA.DAS.EAS.Application.Queries.AccountTransactions.GetEnglishFrationDetail;
using SFA.DAS.EAS.Application.Services;
using SFA.DAS.EAS.Domain.Data.Repositories;
using SFA.DAS.EAS.Domain.Models.Levy;

namespace SFA.DAS.EAS.Application.UnitTests.Services.DasLevyServiceTests
{
    public class WhenIGetEnglishFractionForAnEmpref
    {
        private Mock<IMediator> _mediator;
        private DasLevyService _dasLevyService;
        private Mock<ITransactionRepository> _transactionRepoMock;

        [SetUp]
        public void Arrange()
        {
            _transactionRepoMock = new Mock<ITransactionRepository>();
            _mediator = new Mock<IMediator>();
            _mediator.Setup(x => x.SendAsync(It.IsAny<GetEnglishFractionDetailByEmpRefQuery>())).ReturnsAsync(new GetEnglishFractionDetailResposne() { FractionDetail = new List<DasEnglishFraction> { new DasEnglishFraction() } });

            _dasLevyService = new DasLevyService(_mediator.Object, _transactionRepoMock.Object);
        }

        [Test]
        public async Task ThenTheMediatorMethodIsCalled()
        {
            //Arrange
            var empRef = "123FGV";
            var accountId = 24593543;

            //Act
            await _dasLevyService.GetEnglishFractionHistory(accountId, empRef);

            //Assert
            _mediator.Verify(x => x.SendAsync(It.Is<GetEnglishFractionDetailByEmpRefQuery>(c=> c.AccountId == accountId && c.EmpRef.Equals(empRef))), Times.Once);
        }

        [Test]
        public async Task ThenTheResponseFromTheQueryIsReturned()
        {
            //Arrange
            var empRef = "123FGV";
            var accountId = 25983435;

            //Act
            var actual = await _dasLevyService.GetEnglishFractionHistory(accountId, empRef);

            //Assert
            Assert.IsNotEmpty(actual);
            Assert.IsAssignableFrom<List<DasEnglishFraction>>(actual);
        }
        
    }
}
