﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.1.0.0
//      SpecFlow Generator Version:2.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace SFA.DAS.EAS.Transactions.AcceptanceTests.Features
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.1.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("TransactionLine")]
    public partial class TransactionLineFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "TransactionLine.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "TransactionLine", "\tIn order to show details of my balance\r\n\tI want view transactions in and out for" +
                    " my account", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
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
        [NUnit.Framework.DescriptionAttribute("Transaction History levy declarations")]
        public virtual void TransactionHistoryLevyDeclarations()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Transaction History levy declarations", ((string[])(null)));
#line 6
this.ScenarioSetup(scenarioInfo);
#line 7
 testRunner.Given("I have an account", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Paye_scheme",
                        "LevyDueYtd",
                        "Payroll_Year",
                        "Payroll_Month",
                        "English_Fraction",
                        "SubmissionDate",
                        "CreatedDate"});
            table1.AddRow(new string[] {
                        "223/ABC",
                        "1000",
                        "16-17",
                        "11",
                        "1",
                        "2017-03-18",
                        "2017-03-23"});
            table1.AddRow(new string[] {
                        "223/ABC",
                        "1100",
                        "16-17",
                        "12",
                        "1",
                        "2017-04-18",
                        "2017-04-23"});
#line 8
 testRunner.When("I have the following submissions", ((string)(null)), table1, "When ");
#line 12
 testRunner.Then("the balance should be 1210 on the screen", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Transaction History levy declarations with multiple schemes")]
        public virtual void TransactionHistoryLevyDeclarationsWithMultipleSchemes()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Transaction History levy declarations with multiple schemes", ((string[])(null)));
#line 14
this.ScenarioSetup(scenarioInfo);
#line 15
 testRunner.Given("I have an account", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Paye_scheme",
                        "LevyDueYtd",
                        "Payroll_Year",
                        "Payroll_Month",
                        "English_Fraction",
                        "SubmissionDate",
                        "CreatedDate"});
            table2.AddRow(new string[] {
                        "123/ABC",
                        "1000",
                        "16-17",
                        "11",
                        "1",
                        "2017-03-17",
                        ""});
            table2.AddRow(new string[] {
                        "456/ABC",
                        "1000",
                        "16-17",
                        "11",
                        "1",
                        "2017-03-18",
                        "2017-03-23"});
            table2.AddRow(new string[] {
                        "123/ABC",
                        "1100",
                        "16-17",
                        "12",
                        "1",
                        "2017-04-18",
                        "2017-04-23"});
#line 16
 testRunner.When("I have the following submissions", ((string)(null)), table2, "When ");
#line 21
 testRunner.Then("the balance should be 2310 on the screen", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Transaction History levy declarations over Payroll_year")]
        public virtual void TransactionHistoryLevyDeclarationsOverPayroll_Year()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Transaction History levy declarations over Payroll_year", ((string[])(null)));
#line 23
this.ScenarioSetup(scenarioInfo);
#line 24
 testRunner.Given("I have an account", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "Paye_scheme",
                        "LevyDueYtd",
                        "Payroll_Year",
                        "Payroll_Month",
                        "English_Fraction",
                        "SubmissionDate"});
            table3.AddRow(new string[] {
                        "323/ABC",
                        "1000",
                        "16-17",
                        "12",
                        "1",
                        "2017-04-18"});
            table3.AddRow(new string[] {
                        "323/ABC",
                        "100",
                        "17-18",
                        "01",
                        "1",
                        "2017-05-18"});
#line 25
 testRunner.When("I have the following submissions", ((string)(null)), table3, "When ");
#line 29
 testRunner.Then("the balance should be 1210 on the screen", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("End of Year Adjustment to account is applied to levy credit for adjustment")]
        public virtual void EndOfYearAdjustmentToAccountIsAppliedToLevyCreditForAdjustment()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("End of Year Adjustment to account is applied to levy credit for adjustment", ((string[])(null)));
#line 31
this.ScenarioSetup(scenarioInfo);
#line 32
 testRunner.Given("I have an account", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        "Paye_scheme",
                        "LevyDueYtd",
                        "Payroll_Year",
                        "Payroll_Month",
                        "English_Fraction",
                        "SubmissionDate",
                        "EndOfYearAdjustment",
                        "EndOfYearAdjustmentAmount"});
            table4.AddRow(new string[] {
                        "423/ABC",
                        "1000",
                        "17-18",
                        "11",
                        "1",
                        "2018-03-18",
                        "0",
                        "0"});
            table4.AddRow(new string[] {
                        "423/ABC",
                        "1100",
                        "17-18",
                        "12",
                        "1",
                        "2018-04-18",
                        "0",
                        "0"});
            table4.AddRow(new string[] {
                        "423/ABC",
                        "100",
                        "18-19",
                        "01",
                        "1",
                        "2018-05-18",
                        "0",
                        "0"});
            table4.AddRow(new string[] {
                        "423/ABC",
                        "1050",
                        "17-18",
                        "12",
                        "1",
                        "2018-05-18",
                        "1",
                        "50"});
#line 33
 testRunner.When("I have the following submissions", ((string)(null)), table4, "When ");
#line 39
 testRunner.Then("the balance should be 1265 on the screen", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("End of Year Adjustment to account is applied to levy credit for positive adjustme" +
            "nt")]
        public virtual void EndOfYearAdjustmentToAccountIsAppliedToLevyCreditForPositiveAdjustment()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("End of Year Adjustment to account is applied to levy credit for positive adjustme" +
                    "nt", ((string[])(null)));
#line 41
this.ScenarioSetup(scenarioInfo);
#line 42
 testRunner.Given("I have an account", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                        "Paye_scheme",
                        "LevyDueYtd",
                        "Payroll_Year",
                        "Payroll_Month",
                        "English_Fraction",
                        "SubmissionDate",
                        "EndOfYearAdjustment",
                        "EndOfYearAdjustmentAmount"});
            table5.AddRow(new string[] {
                        "423/ABC",
                        "1000",
                        "17-18",
                        "11",
                        "1",
                        "2018-03-18",
                        "0",
                        "0"});
            table5.AddRow(new string[] {
                        "423/ABC",
                        "1100",
                        "17-18",
                        "12",
                        "1",
                        "2018-04-18",
                        "0",
                        "0"});
            table5.AddRow(new string[] {
                        "423/ABC",
                        "100",
                        "18-19",
                        "01",
                        "1",
                        "2018-05-18",
                        "0",
                        "0"});
            table5.AddRow(new string[] {
                        "423/ABC",
                        "1150",
                        "17-18",
                        "12",
                        "1",
                        "2018-05-18",
                        "1",
                        "-50"});
#line 43
 testRunner.When("I have the following submissions", ((string)(null)), table5, "When ");
#line 49
 testRunner.Then("the balance should be 1375 on the screen", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Transaction History levy declarations and Payments")]
        public virtual void TransactionHistoryLevyDeclarationsAndPayments()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Transaction History levy declarations and Payments", ((string[])(null)));
#line 51
this.ScenarioSetup(scenarioInfo);
#line 52
 testRunner.Given("I have an account", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                        "Paye_scheme",
                        "LevyDueYtd",
                        "Payroll_Year",
                        "Payroll_Month",
                        "English_Fraction",
                        "SubmissionDate"});
            table6.AddRow(new string[] {
                        "423/ABC",
                        "1000",
                        "17-18",
                        "01",
                        "1",
                        "2017-03-18"});
            table6.AddRow(new string[] {
                        "423/ABC",
                        "1100",
                        "17-18",
                        "02",
                        "1",
                        "2017-04-18"});
#line 53
 testRunner.When("I have the following submissions", ((string)(null)), table6, "When ");
#line hidden
            TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                        "Payment_Amount",
                        "Payment_Type"});
            table7.AddRow(new string[] {
                        "100",
                        "levy"});
            table7.AddRow(new string[] {
                        "200",
                        "cofund"});
#line 57
 testRunner.And("I have the following payments", ((string)(null)), table7, "And ");
#line 61
 testRunner.Then("the balance should be 1110 on the screen", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Transaction History levy declarations late account registration in payroll year")]
        [NUnit.Framework.TestCaseAttribute("02", "2420", new string[0])]
        [NUnit.Framework.TestCaseAttribute("03", "3300", new string[0])]
        public virtual void TransactionHistoryLevyDeclarationsLateAccountRegistrationInPayrollYear(string month, string balance, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Transaction History levy declarations late account registration in payroll year", exampleTags);
#line 63
this.ScenarioSetup(scenarioInfo);
#line 64
 testRunner.Given("I have an account", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            TechTalk.SpecFlow.Table table8 = new TechTalk.SpecFlow.Table(new string[] {
                        "Paye_scheme",
                        "LevyDueYtd",
                        "Payroll_Year",
                        "Payroll_Month",
                        "English_Fraction",
                        "SubmissionDate"});
            table8.AddRow(new string[] {
                        "425/ABC",
                        "1000",
                        "16-17",
                        "01",
                        "1",
                        "2016-05-18"});
            table8.AddRow(new string[] {
                        "424/ABC",
                        "100",
                        "16-17",
                        "01",
                        "1",
                        "2016-05-18"});
            table8.AddRow(new string[] {
                        "425/ABC",
                        "2000",
                        "16-17",
                        "02",
                        "1",
                        "2016-06-18"});
            table8.AddRow(new string[] {
                        "424/ABC",
                        "200",
                        "16-17",
                        "02",
                        "1",
                        "2016-06-18"});
#line 65
 testRunner.When("I have the following submissions", ((string)(null)), table8, "When ");
#line 71
 testRunner.And(string.Format("I register on month \"{0}\"", month), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 72
 testRunner.Then(string.Format("the balance should be {0} on the screen", balance), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Transaction History levy declarations next year registration")]
        public virtual void TransactionHistoryLevyDeclarationsNextYearRegistration()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Transaction History levy declarations next year registration", ((string[])(null)));
#line 78
this.ScenarioSetup(scenarioInfo);
#line 79
 testRunner.Given("I have an account", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            TechTalk.SpecFlow.Table table9 = new TechTalk.SpecFlow.Table(new string[] {
                        "Paye_scheme",
                        "LevyDueYtd",
                        "Payroll_Year",
                        "Payroll_Month",
                        "English_Fraction",
                        "SubmissionDate"});
            table9.AddRow(new string[] {
                        "323/ABC",
                        "1000",
                        "16-17",
                        "12",
                        "1",
                        "2016-05-18"});
            table9.AddRow(new string[] {
                        "323/ABC",
                        "100",
                        "17-18",
                        "01",
                        "1",
                        "2016-05-18"});
#line 80
 testRunner.When("I have the following submissions", ((string)(null)), table9, "When ");
#line 84
 testRunner.And("I register on DAS in year 16-17 month 12", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 85
 testRunner.Then("the balance should be 1210 on the screen", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Transaction History levy declarations next year registration multiple PAYE scheme" +
            "s")]
        public virtual void TransactionHistoryLevyDeclarationsNextYearRegistrationMultiplePAYESchemes()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Transaction History levy declarations next year registration multiple PAYE scheme" +
                    "s", ((string[])(null)));
#line 87
this.ScenarioSetup(scenarioInfo);
#line 88
 testRunner.Given("I have an account", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            TechTalk.SpecFlow.Table table10 = new TechTalk.SpecFlow.Table(new string[] {
                        "Paye_scheme",
                        "LevyDueYtd",
                        "Payroll_Year",
                        "Payroll_Month",
                        "English_Fraction",
                        "SubmissionDate"});
            table10.AddRow(new string[] {
                        "327/ABC",
                        "1000",
                        "16-17",
                        "12",
                        "1",
                        "2016-05-18"});
            table10.AddRow(new string[] {
                        "427/ABC",
                        "1000",
                        "16-17",
                        "12",
                        "1",
                        "2016-05-18"});
            table10.AddRow(new string[] {
                        "327/ABC",
                        "100",
                        "17-18",
                        "01",
                        "1",
                        "2016-06-18"});
            table10.AddRow(new string[] {
                        "427/ABC",
                        "100",
                        "17-18",
                        "01",
                        "1",
                        "2016-06-18"});
#line 89
 testRunner.When("I have the following submissions", ((string)(null)), table10, "When ");
#line 95
 testRunner.And("I register on DAS in year 16-17 month 01", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 96
 testRunner.Then("the balance should be 2420 on the screen", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Single PAYE scheme removed")]
        public virtual void SinglePAYESchemeRemoved()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Single PAYE scheme removed", ((string[])(null)));
#line 98
this.ScenarioSetup(scenarioInfo);
#line 99
 testRunner.Given("I have an account", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            TechTalk.SpecFlow.Table table11 = new TechTalk.SpecFlow.Table(new string[] {
                        "Paye_scheme",
                        "LevyDueYtd",
                        "Payroll_Year",
                        "Payroll_Month",
                        "English_Fraction",
                        "SubmissionDate"});
            table11.AddRow(new string[] {
                        "328/ABC",
                        "1000",
                        "16-17",
                        "01",
                        "1",
                        "2016-05-18"});
            table11.AddRow(new string[] {
                        "328/ABC",
                        "1100",
                        "16-17",
                        "02",
                        "1",
                        "2016-05-18"});
            table11.AddRow(new string[] {
                        "328/ABC",
                        "1200",
                        "16-17",
                        "03",
                        "1",
                        "2016-06-18"});
#line 100
 testRunner.When("I have the following submissions", ((string)(null)), table11, "When ");
#line 105
 testRunner.And("I register on DAS in year 16-17 month 01", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 106
 testRunner.And("I remove the PAYE scheme in month 04", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 107
 testRunner.Then("the balance should be 1320 on the screen", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
