using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports;
using GLMSys.SpecFlow.Playwright.Drivers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLMSys.SpecFlow.Playwright.Support;

namespace GLMSys.SpecFlow.Playwright.Hooks
{
    [Binding]
    internal class Hook : ExtentReport
    {
        private readonly Driver _driver;

        public Hook(Driver driver)
        {
            _driver = driver;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            Utilities.Parameters.VirtualDirectory = TestContext.Parameters["VirtualDirectory"];
            Utilities.Parameters.Headless = Convert.ToBoolean(TestContext.Parameters["Headless"]);

            ExtentReportInit();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            ExtentReportTearDown();
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            Feature = ExtentReports?.CreateTest<AventStack.ExtentReports.Gherkin.Model.Feature>(featureContext.FeatureInfo.Title);
        }

        [BeforeScenario(Order = 1)]
        public void FirstBeforeScenario(ScenarioContext scenarioContext)
        {
            Console.WriteLine("Virtual Directory: " + Utilities.Parameters.VirtualDirectory);
            Console.WriteLine("Extent Report Directory: " + TestResultPath);
            Scenario = Feature?.CreateNode<AventStack.ExtentReports.Gherkin.Model.Scenario>(scenarioContext.ScenarioInfo.Title);
        }

        [AfterStep()]
        public async Task AfterStep(ScenarioContext scenarioContext)
        {
            string stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            string stepName = scenarioContext.StepContext.StepInfo.Text;

            //When scenario passed
            if (Scenario != null && scenarioContext.TestError == null)
            {
                if (stepType == "Given")
                {
                    Scenario.CreateNode<Given>(stepName);
                }
                else if (stepType == "When")
                {
                    Scenario.CreateNode<When>(stepName);
                }
                else if (stepType == "Then")
                {
                    Scenario.CreateNode<Then>(stepName);
                }
                else if (stepType == "And")
                {
                    Scenario.CreateNode<And>(stepName);
                }
            }

            if (Scenario != null && scenarioContext.TestError != null)
            {
                var basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                string fileName = $"{Utilities.Parameters.VirtualDirectory}_{scenarioContext.ScenarioInfo.Title}_{scenarioContext.StepContext.StepInfo.Text.Replace("\"", "")}.png";
                var filePath = $"{basePath}\\{fileName}";

                await _driver.Page.ScreenshotAsync(new() { Path = filePath });

                var bytes = await _driver.Page.ScreenshotAsync();
                var base64Image = Convert.ToBase64String(bytes);

                TestContext.AddTestAttachment(filePath);

                if (stepType == "Given")
                {
                    Scenario.CreateNode<Given>(stepName).Fail(scenarioContext.TestError.Message,
                    MediaEntityBuilder.CreateScreenCaptureFromBase64String(base64Image).Build());
                }
                else if (stepType == "When")
                {
                    Scenario.CreateNode<When>(stepName).Fail(scenarioContext.TestError.Message,
                    MediaEntityBuilder.CreateScreenCaptureFromBase64String(base64Image).Build());
                }
                else if (stepType == "Then")
                {
                    Scenario.CreateNode<Then>(stepName).Fail(scenarioContext.TestError.Message,
                    MediaEntityBuilder.CreateScreenCaptureFromBase64String(base64Image).Build());
                }
                else if (stepType == "And")
                {
                    Scenario.CreateNode<And>(stepName).Fail(scenarioContext.TestError.Message,
                    MediaEntityBuilder.CreateScreenCaptureFromBase64String(base64Image).Build());
                }
            }
        }
    }
}
