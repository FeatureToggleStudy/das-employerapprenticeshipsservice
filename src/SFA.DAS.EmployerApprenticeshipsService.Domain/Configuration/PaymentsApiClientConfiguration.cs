using System.Collections.Generic;
using SFA.DAS.EAS.Domain.Interfaces;
using SFA.DAS.Provider.Events.Api.Client;

namespace SFA.DAS.EAS.Domain.Configuration
{
    public class PaymentsApiClientConfiguration : IPaymentsEventsApiConfiguration, IConfiguration
    {
        public string ClientToken { get; set; }
        public string ApiBaseUrl { get; set; }
        public string DatabaseConnectionString { get; set; }
        public string ServiceBusConnectionString { get; set; }
        public string MessageServiceBusConnectionString { get; set; }
        public bool PaymentsDisabled { get; set; }
    }
}