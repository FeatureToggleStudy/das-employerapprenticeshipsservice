﻿using NServiceBus;
using SFA.DAS.EAS.Infrastructure.DependencyResolution;
using SFA.DAS.EmployerFinance.Messages.Commands;
using SFA.DAS.NServiceBus.AzureServiceBus;
using Environment = SFA.DAS.EAS.Infrastructure.DependencyResolution.Environment;

namespace SFA.DAS.EAS.Infrastructure.NServiceBus
{
    public static class EndpointConfigurationExtensions
    {
        public static EndpointConfiguration SetupAzureServiceBusTransport(this EndpointConfiguration config, string connectionString)
        {
            var isDevelopment = ConfigurationHelper.IsEnvironmentAnyOf(Environment.Local);

            config.SetupAzureServiceBusTransport(isDevelopment, connectionString, r =>
            {
                r.RouteToEndpoint(
                    typeof(ImportLevyDeclarationsCommand).Assembly,
                    typeof(ImportLevyDeclarationsCommand).Namespace,
                    "SFA.DAS.EmployerFinance.MessageHandlers");
            });

            return config;
        }
    }
}