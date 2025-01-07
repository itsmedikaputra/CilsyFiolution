using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Config;

namespace GLMSys.SpecFlow.Playwright.Support
{
    internal class ExtentReport
    {
        public static ExtentReports? ExtentReports;
        public static ExtentTest? Feature;
        public static ExtentTest? Scenario;

        public static string Directory = AppDomain.CurrentDomain.BaseDirectory;
        public static string TestResultPath = "";

        public static void ExtentReportInit()
        {
            TestResultPath = $"{Directory}TestResults\\{Utilities.Parameters.VirtualDirectory}\\{DateTime.Now.ToString("yyyyMMdd_HHmmss")}\\";
            System.IO.Directory.CreateDirectory(TestResultPath);

            var sparkReporter = new ExtentSparkReporter($"{TestResultPath}index.html");
            string reportName = $"Automation Status Report {Utilities.Parameters.VirtualDirectory}";
            sparkReporter.Config.ReportName = reportName;
            sparkReporter.Config.DocumentTitle = reportName;
            sparkReporter.Config.Theme = Theme.Standard;
            //sparkReporter..

            ExtentReports = new ExtentReports();
            ExtentReports.AttachReporter(sparkReporter);
            ExtentReports.AddSystemInfo("Browser", "Chrome");
            ExtentReports.AddSystemInfo("OS", "Windows");
        }

        public static void ExtentReportTearDown()
        {
            if (ExtentReports != null)
                ExtentReports.Flush();
        }
    }
}
