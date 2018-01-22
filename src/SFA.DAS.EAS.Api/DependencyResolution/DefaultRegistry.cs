// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultRegistry.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using MediatR;
using SFA.DAS.EAS.Domain.Configuration;
using SFA.DAS.EAS.Infrastructure.DependencyResolution;
using SFA.DAS.HashingService;

using StructureMap;
using WebGrease.Css.Extensions;

namespace SFA.DAS.EAS.Api.DependencyResolution {
    using Domain.Interfaces;
    using Infrastructure.Caching;
    using SFA.DAS.EAS.Api.App_Start;
    using SFA.DAS.NLog.Logger;

    using StructureMap.Graph;
    using System.Web;

    public class DefaultRegistry : Registry {
        private const string ServiceName = "SFA.DAS.EmployerApprenticeshipsService";
        private const string ServiceNamespace = "SFA.DAS";

        public DefaultRegistry()
        {

            Scan(
                scan =>
                {
                    scan.AssembliesFromApplicationBaseDirectory(a => a.GetName().Name.StartsWith(ServiceNamespace));
                    scan.RegisterConcreteTypesAgainstTheFirstInterface();
                });

            For<IConfiguration>().Use<EmployerApprenticeshipsServiceConfiguration>();

            For<ICache>().Use<InMemoryCache>(); //RedisCache
            var config = ConfigurationHelper.GetConfiguration<EmployerApprenticeshipsServiceConfiguration>(ServiceName);

            ConfigureHashingService(config);
            RegisterMapper();
            RegisterMediator();
            RegisterLogger();
        }

        private void RegisterMediator()
        {
            For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => t => ctx.GetInstance(t));
            For<MultiInstanceFactory>().Use<MultiInstanceFactory>(ctx => t => ctx.GetAllInstances(t));
            For<IMediator>().Use<Mediator>();

            //Assembly.GetGetAssembly("SFA.DAS.EAS.Application");

            Scan(scanner => {
                scanner.TheCallingAssembly();
                scanner.AddAllTypesOf(typeof(IRequestHandler<,>));
                scanner.AddAllTypesOf(typeof(INotificationHandler<>));
            });
        }

        private void RegisterMapper()
        {
            var infrastructureProfiles = Assembly.Load($"{ServiceNamespace}.EAS.Infrastructure").GetTypes()
                            .Where(t => typeof(Profile).IsAssignableFrom(t))
                            .Select(t => (Profile)Activator.CreateInstance(t));

            var apiProfiles = Assembly.Load($"{ServiceNamespace}.EAS.Api").GetTypes()
                            .Where(t => typeof(Profile).IsAssignableFrom(t))
                            .Select(t => (Profile)Activator.CreateInstance(t));

            var config = new MapperConfiguration(cfg =>
            {
                infrastructureProfiles.ForEach(cfg.AddProfile);
                apiProfiles.ForEach(cfg.AddProfile);
            });

            var mapper = config.CreateMapper();

            For<IConfigurationProvider>().Use(config).Singleton();
            For<IMapper>().Use(mapper).Singleton();
        }

        private void RegisterLogger()
        {
            //For<IRequestContext>().Use(x => new RequestContext(new HttpContextWrapper(HttpContext.Current)));
            For<ILog>().Use(x => new NLogLogger(
                x.ParentType,
                null,//x.GetInstance<IRequestContext>(),
                null)).AlwaysUnique();
        }

        private void ConfigureHashingService(EmployerApprenticeshipsServiceConfiguration config)
        {
            For<IHashingService>().Use(x => new HashingService.HashingService(config.AllowedHashstringCharacters, config.Hashstring));
        }
    }
}