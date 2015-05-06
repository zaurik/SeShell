using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SeShell.Test.Core;
using SeShell.Test.TestData.Data;

namespace SeShell.Test.TestCases
{
    /// <summary>
    /// A dummy test class created for demonstration ONLY.
    /// This class MUST implement the (abstract) class 'AbstractTest'.
    /// </summary>
    public class DummyTest : AbstractTest
    {
        [Test]
        public void DummyTestOperation()
        {
            string currentExecutingMethod = Utilities.GetCurrentMethod();
            var resultsWriter = new ResultsWriter(Constants.ParameterizedTest, currentExecutingMethod, true);
            var loginTestData = DummyOperationData.GetTestData();

            Parallel.ForEach(WebDrivers, (driver, loopState) =>
            {
                var testAsserter = new TestCaseAsserts();
                string currentWebBrowserString = Utilities.GetWebBrowser(driver);

                if (loginTestData != null)
                {
                    ResultReport testResultReport = new ResultReport();
                    string testFixtureName = Utilities.GenerateTestFixtureName(this.GetType(), currentExecutingMethod,
                    currentWebBrowserString);
                    testResultReport.StartMethodTimerAndInitiateCurrentTestCase(testFixtureName, true);
                    try
                    {
                        /*
                         * Call page flow respective method here. 
                         */

                        /*
                         * Call necessary assertion from TestCaseAsserts class here. 
                         */
                        testResultReport.SetCurrentTestCaseOutcome(true, testAsserter.AssertionCount.ToString());
                    }
                    catch (Exception e)
                    {
                        string screenShotIdentifier = String.Format("{0} - {1}", "{ENTER AN IDENTIFIER (E.G. USER NAME}", currentExecutingMethod);
                        base.HandleException(e, screenShotIdentifier, driver, testResultReport, testAsserter, resultsWriter);
                        Assert.Fail("***** DummyTest Failed *****");
                    }
                    finally
                    {
                        testResultReport.StopMethodTimerAndFinishCurrentTestCase();
                        base.TestCases.Add(testResultReport.currentTestCase);
                    }
                }
            });

            resultsWriter.WriteResultsToFile(this.GetType().Name, TestCases);
        }
    }
}
