﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.3.0.0
//      SpecFlow Generator Version:2.3.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace SFA.DAS.EmployerFinance.AcceptanceTests.Features
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.3.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("HMRC-Scenario-07-No-Payment-for-Period-and-Ceased-PAYE-Scheme")]
    public partial class HMRC_Scenario_07_No_Payment_For_Period_And_Ceased_PAYE_SchemeFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "Scenario-07-No-Payment-for-Period-and-Ceased-PAYE-Scheme.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "HMRC-Scenario-07-No-Payment-for-Period-and-Ceased-PAYE-Scheme", null, ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.OneTimeTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Sceanrio-01-Balance-should-remain-if-no-payment-occurs")]
        public virtual void Sceanrio_01_Balance_Should_Remain_If_No_Payment_Occurs()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Sceanrio-01-Balance-should-remain-if-no-payment-occurs", ((string[])(null)));
#line 3
this.ScenarioSetup(scenarioInfo);
#line 4
 testRunner.Given("user Dave registered as role Owner for account A and added a paye scheme \"123/ABC" +
                    "\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Id",
                        "LevyDueYtd",
                        "Payroll_Year",
                        "Payroll_Month",
                        "English_Fraction",
                        "SubmissionDate",
                        "CreatedDate",
                        "LevyAllowanceForFullYear",
                        "NoPaymentForPeriod",
                        "DateCeased"});
            table1.AddRow(new string[] {
                        "999000701",
                        "1000",
                        "17-18",
                        "1",
                        "1",
                        "2017-04-15",
                        "2017-04-23",
                        "15000",
                        "",
                        ""});
            table1.AddRow(new string[] {
                        "999000702",
                        "2000",
                        "17-18",
                        "2",
                        "1",
                        "2017-05-15",
                        "2017-05-23",
                        "15000",
                        "true",
                        ""});
#line 5
 testRunner.And("Hmrc return the following submissions for paye scheme 123/ABC", ((string)(null)), table1, "And ");
#line 9
 testRunner.When("we refresh levy data for account A paye scheme 123/ABC", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 10
 testRunner.And("All the transaction lines in this scenario have had there transaction date update" +
                    "d to the specified created date", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 11
 testRunner.Then("user Dave from account A should see a level 1 screen with a balance of 1100 on th" +
                    "e 06/2017", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Sceanrio-02-Balance-should-not-be-affected-by-past-non-payment-months")]
        public virtual void Sceanrio_02_Balance_Should_Not_Be_Affected_By_Past_Non_Payment_Months()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Sceanrio-02-Balance-should-not-be-affected-by-past-non-payment-months", ((string[])(null)));
#line 14
this.ScenarioSetup(scenarioInfo);
#line 15
 testRunner.Given("user Dave registered as role Owner for account A and added a paye scheme \"123/ABC" +
                    "\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Id",
                        "LevyDueYtd",
                        "Payroll_Year",
                        "Payroll_Month",
                        "English_Fraction",
                        "SubmissionDate",
                        "CreatedDate",
                        "LevyAllowanceForFullYear",
                        "NoPaymentForPeriod",
                        "DateCeased"});
            table2.AddRow(new string[] {
                        "999000703",
                        "1000",
                        "17-18",
                        "1",
                        "1",
                        "2017-04-15",
                        "2017-04-23",
                        "15000",
                        "",
                        ""});
            table2.AddRow(new string[] {
                        "999000704",
                        "0",
                        "17-18",
                        "2",
                        "1",
                        "2017-05-15",
                        "2017-05-23",
                        "15000",
                        "true",
                        ""});
            table2.AddRow(new string[] {
                        "999000705",
                        "2000",
                        "17-18",
                        "3",
                        "1",
                        "2017-06-15",
                        "2017-06-23",
                        "15000",
                        "",
                        ""});
#line 16
 testRunner.And("Hmrc return the following submissions for paye scheme 123/ABC", ((string)(null)), table2, "And ");
#line 21
 testRunner.When("we refresh levy data for account A paye scheme 123/ABC", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 22
 testRunner.And("All the transaction lines in this scenario have had there transaction date update" +
                    "d to the specified created date", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 23
 testRunner.Then("user Dave from account A should see a level 1 screen with a balance of 2200 on th" +
                    "e 06/2017", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
