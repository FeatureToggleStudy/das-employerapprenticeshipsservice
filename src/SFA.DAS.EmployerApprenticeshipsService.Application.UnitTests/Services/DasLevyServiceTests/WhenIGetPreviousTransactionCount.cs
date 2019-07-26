﻿using System;
using System.Threading.Tasks;
using MediatR;
using Moq;
using NUnit.Framework;
using SFA.DAS.EAS.Application.Queries.AccountTransactions.GetPreviousTransactionsCount;
using SFA.DAS.EAS.Application.Services;
using SFA.DAS.EAS.Domain.Data.Repositories;

namespace SFA.DAS.EAS.Application.UnitTests.Services.DasLevyServiceTests
{
    class WhenIGetPreviousTransactionCount
    {
        private const int TransactionCount = 2;

        private Mock<IMediator> _mediator;
        private Mock<ITransactionRepository> _transactionRepoMock;
        private DasLevyService _dasLevyService;
        
        [SetUp]
        public void Arrange()
        {
            _transactionRepoMock = new Mock<ITransactionRepository>();
            _mediator = new Mock<IMediator>();
            _mediator.Setup(x => x.SendAsync(It.IsAny<GetPreviousTransactionsCountRequest>()))
                .ReturnsAsync(new GetPreviousTransactionsCountResponse { Count = TransactionCount });

            _dasLevyService = new DasLevyService(_mediator.Object, _transactionRepoMock.Object);
        }

        [Test]
        public async Task ThenTheMediatorMethodIsCalled()
        {
            //Act
            await _dasLevyService.GetPreviousAccountTransaction(1, DateTime.Now);

            //Assert
            _mediator.Verify(x => x.SendAsync(It.IsAny<GetPreviousTransactionsCountRequest>()), Times.Once);
        }

        [Test]
        public async Task ThenTheResponseFromTheQueryIsReturned()
        {
            //Act
            var actual = await _dasLevyService.GetPreviousAccountTransaction(1, DateTime.Now);

            //Assert
            Assert.AreEqual(TransactionCount, actual);
        }
    }
}
