﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.EmployerAccounts.Models.Account;
using SFA.DAS.EmployerAccounts.Queries.GetPagedEmployerAccounts;
using SFA.DAS.EmployerAccounts.TestCommon.Extensions;
using SFA.DAS.EmployerAccounts.Api.Types;

namespace SFA.DAS.EmployerAccounts.Api.UnitTests.Controllers.EmployerAccountsControllerTests
{
    [TestFixture]
    public class WhenIGetAccounts : EmployerAccountsControllerTests
    {
        [Test]
        public async Task ThenAccountsAreReturnedWithTheirBalanceAndAUriToGetAccountDetails()
        {
            var pageNumber = 123;
            var pageSize = 9084;
            var toDate = DateTime.Now.AddDays(-1).ToString("yyyyMMddHHmmss");

            var accountsResponse = new GetPagedEmployerAccountsResponse
            {
                AccountsCount = 2,
                Accounts = new List<Account>
                {
                    new Account { HashedId = "ABC123", Id = 123, Name = "Test 1" },
                    new Account { HashedId = "ABC999", Id = 987, Name = "Test 2" }
                }
            };
            Mediator.Setup(x => x.SendAsync(It.Is<GetPagedEmployerAccountsQuery>(q => q.PageNumber == pageNumber && q.PageSize == pageSize && q.ToDate == toDate)))
                .ReturnsAsync(accountsResponse);        

            UrlHelper.Setup(x => x.Route("GetAccount", It.Is<object>(o => o.IsEquivalentTo(new { hashedAccountId = accountsResponse.Accounts[0].HashedId })))).Returns($"/api/accounts/{accountsResponse.Accounts[0].HashedId}");
            UrlHelper.Setup(x => x.Route("GetAccount", It.Is<object>(o => o.IsEquivalentTo(new { hashedAccountId = accountsResponse.Accounts[1].HashedId })))).Returns($"/api/accounts/{accountsResponse.Accounts[1].HashedId}");

            var response = await Controller.GetAccounts(toDate, pageSize, pageNumber);

            Assert.IsNotNull(response);
            Assert.IsInstanceOf<OkNegotiatedContentResult<PagedApiResponseViewModel<AccountViewModel>>>(response);
            var model = response as OkNegotiatedContentResult<PagedApiResponseViewModel<AccountViewModel>>;

            model?.Content?.Data.Should().NotBeNull();
            model?.Content?.Page.Should().Be(pageNumber);
            model?.Content?.Data.Should().HaveCount(accountsResponse.AccountsCount);

            foreach (var expectedAccount in accountsResponse.Accounts)
            {
                var returnedAccount = model?.Content?.Data.SingleOrDefault(x => x.AccountId == expectedAccount.Id && x.AccountHashId == expectedAccount.HashedId && x.AccountName == expectedAccount.Name);
                returnedAccount.Should().NotBeNull();
                returnedAccount?.Href.Should().Be($"/api/accounts/{returnedAccount.AccountHashId}");
            }
        }

        [Test]
        public async Task AndNoToDateIsProvidedThenAllAccountsAreReturned()
        {
            await Controller.GetAccounts();

            Mediator.Verify(x => x.SendAsync(It.Is<GetPagedEmployerAccountsQuery>(q => q.ToDate == DateTime.MaxValue.ToString("yyyyMMddHHmmss"))));
        }

        [Test]
        public async Task AndNoPageSizeIsProvidedThen1000AccountsAreReturned()
        {
            await Controller.GetAccounts(DateTime.Now.AddDays(-1).ToString("yyyyMMddHHmmss"));

            Mediator.Verify(x => x.SendAsync(It.Is<GetPagedEmployerAccountsQuery>(q => q.PageSize == 1000)));
        }

        [Test]
        public async Task AndNoPageNumberIsProvidedThenTheFirstPageOfAccountsAreReturned()
        {
            await Controller.GetAccounts(DateTime.Now.AddDays(-1).ToString("yyyyMMddHHmmss"));

            Mediator.Verify(x => x.SendAsync(It.Is<GetPagedEmployerAccountsQuery>(q => q.PageNumber == 1)));
        }
    }
}
