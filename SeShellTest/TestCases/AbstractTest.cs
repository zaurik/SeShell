using NUnit.Framework;
using OpenQA.Selenium;
using SeShell.Test.Core;
using SeShell.Test.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeShell.Test.XMLTestResult.XMLObjects;

namespace SeShell.Test.TestCases
{
    public abstract class AbstractTest
    {
        internal List<IWebDriver> WebDrivers;
        internal List<TestCase> TestCases;
        internal TestCaseAsserts TestCaseAsserts;

        [SetUp]
        public void Setup()
        {
            this.InitializeDependencies();
            //(IEnumerable<WebBrowsers>)Enum.GetValues(typeof(WebBrowsers)) , WebBrowsers.Ie
            var webBrowserList = Configuration.WebBrowsers;
            Parallel.ForEach(
               webBrowserList , webBrowser => WebDrivers.Add(Utilities.CreateBrowser(webBrowser)));
        }

        [TearDown]
        public void TearDown()
        {
            Parallel.ForEach(WebDrivers, driver => driver.Quit());
        }

        protected void InitializeDependencies()
        {
            WebDrivers = new List<IWebDriver>();
            TestCases = new List<TestCase>();           
        }

        protected void HandleException(
            Exception ex,
            string screenshotIdentifier, 
            IWebDriver webDriver,
            ResultReport resultReport, 
            TestCaseAsserts asserts, 
            ResultsWriter writer)
        {
            ScreenShotImage.CaptureScreenShot(
                            webDriver,
                            Utilities.CombineTestOutcomeString(Constants.ScreenshotError, screenshotIdentifier));
            resultReport.SetCurrentTestCaseOutcome(
                false,
                asserts.AssertionCount.ToString(),
                ex.Message,
                ex.StackTrace);
            resultReport.StopMethodTimerAndFinishCurrentTestCase();
            this.TestCases.Add(resultReport.currentTestCase);
            writer.WriteResultsToFile(this.GetType().Name, TestCases);
        }
    }
}
