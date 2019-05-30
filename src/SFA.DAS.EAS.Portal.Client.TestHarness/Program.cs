﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SFA.DAS.EAS.Portal.Client.TestHarness.DependencyResolution;
using SFA.DAS.EAS.Portal.Client.TestHarness.Scenarios;
using SFA.DAS.EAS.Portal.Client.TestHarness.Startup;
using SFA.DAS.EAS.Portal.Startup;
using StructureMap;

namespace SFA.DAS.EAS.Portal.Client.TestHarness
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            using (var host = CreateHostBuilder(args).Build())
            {
                await host.StartAsync();

                var getAccount = host.Services.GetService<GetAccountScenario>();
                
                var accountTasks = Enumerable.Range(0, 9)
                    .AsParallel()
                    .Select(n => getAccount.Run());

                var accounts = await Task.WhenAll(accountTasks);

                foreach (var account in accounts)
                {
                    Console.WriteLine($"Account #{account.Id} has {account.Organisations.Count()} ALEs");
                }

                await host.StopAsync();
            }            
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            new HostBuilder()
                .ConfigurePortalClientConfiguration(args)
                .UseDasEnvironment()
                .UseStructureMap()
                .ConfigureServices(s => s.AddTransient<GetAccountScenario, GetAccountScenario>())
                .ConfigureContainer<Registry>(IoC.Initialize);
    }
}