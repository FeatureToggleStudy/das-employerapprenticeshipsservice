﻿using SFA.DAS.ExecutionPolicies;
using StructureMap;

namespace SFA.DAS.EmployerAccounts.DependencyResolution
{
    public class ExecutionPoliciesRegistry : Registry
    {
        public ExecutionPoliciesRegistry()
        {
            For<ExecutionPolicy>().Use<HmrcExecutionPolicy>().Named(HmrcExecutionPolicy.Name).SelectConstructor(() => new HmrcExecutionPolicy(null));
            For<ExecutionPolicy>().Use<IdamsExecutionPolicy>().Named(IdamsExecutionPolicy.Name);
            Policies.Add(new ExecutionPolicyPolicy());
        }
    }
}