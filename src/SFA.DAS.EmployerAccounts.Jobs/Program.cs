﻿using Microsoft.Azure.WebJobs;
using NServiceBus;
using SFA.DAS.EAS.Infrastructure.DependencyResolution;
using System.Threading;
using System.Threading.Tasks;
using SFA.DAS.EAS.Domain.Configuration;
using SFA.DAS.EAS.Infrastructure.Data;
using SFA.DAS.EAS.Infrastructure.NServiceBus;
using SFA.DAS.EmployerAccounts.Jobs.DependencyResolution;
using SFA.DAS.NServiceBus;
using SFA.DAS.NServiceBus.EntityFramework;
using SFA.DAS.NServiceBus.NewtonsoftSerializer;
using SFA.DAS.NServiceBus.NLog;
using SFA.DAS.NServiceBus.StructureMap;

namespace SFA.DAS.EmployerAccounts.Jobs
{
    public class Program
    {
        public static void Main()
        {
            var isDevelopment = ConfigurationHelper.IsEnvironmentAnyOf(Environment.Local);
            var config = new JobHostConfiguration();

            if (isDevelopment)
            {
                config.UseDevelopmentSettings();
            }
            
            config.UseTimers();

            var host = new JobHost(config);

            host.Call(typeof(Program).GetMethod(nameof(AsyncMain)));
            host.RunAndBlock();
        }

        [NoAutomaticTrigger]
        public static async Task AsyncMain(CancellationToken cancellationToken)
        {
            var container = IoC.Initialize();

            var endpointConfiguration = new EndpointConfiguration("SFA.DAS.EmployerAccounts.Jobs")
                .SetupAzureServiceBusTransport(() => container.GetInstance<EmployerApprenticeshipsServiceConfiguration>().MessageServiceBusConnectionString)
                .SetupEntityFrameworkUnitOfWork<EmployerAccountsDbContext>()
                .SetupErrorQueue()
                .SetupInstallers()
                .SetupNewtonsoftSerializer()
                .SetupNLogFactory()
                .SetupSendOnly()
                .SetupStructureMapBuilder(container);

            var endpoint = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

            container.Configure(c =>
            {
                c.For<IMessageSession>().Use(endpoint);
            });

            ServiceLocator.Initialize(container);
        }
    }
}