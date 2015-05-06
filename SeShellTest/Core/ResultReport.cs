using System;
using System.Globalization;
using System.Linq;
using SeShell.Test.XMLTestResult.XMLObjects;
using System.Collections.Generic;
using System.Diagnostics;

namespace SeShell.Test.Core
{
    public sealed class ResultReport
    {
        private ResultTestSuite currentTestSuite;

        private Stopwatch methodTimeStopwatch;

        public TestCase currentTestCase;

      
        public void StartMethodTimerAndInitiateCurrentTestCase(string testFixtureName, bool isExecuted)
        {
            this.methodTimeStopwatch = new Stopwatch();
            this.methodTimeStopwatch.Start();
            this.currentTestCase = new TestCase { Name = testFixtureName, Executed = true };
        }

        public void SetCurrentTestCaseOutcome(bool success, string asserts, string message = null,
            string stackTrace = null)
        {
            this.currentTestCase.FailureSpecified = (!success);
            this.currentTestCase.Asserts = asserts;

            if (!success)
            {
                this.currentTestCase.Failure = new Failure()
                {
                    Message = message,
                    StackTrace = stackTrace
                };

            }

            this.currentTestCase.Success = success;
        }

        public void StopMethodTimerAndFinishCurrentTestCase()
        {
            this.methodTimeStopwatch.Stop();
            this.currentTestCase.Time = Math.Round(this.methodTimeStopwatch.Elapsed.TotalSeconds).ToString(CultureInfo.InvariantCulture);
        }       
    }
}