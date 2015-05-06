using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeShell.Test.XMLTestResult.XMLObjects;

namespace SeShell.Test.Core
{
    public class ResultsWriter
    {
        private ResultTestSuite currentTestSuite;

        public ResultsWriter(string type, string currentMethodName, bool executed)
        {
            this.currentTestSuite = new ResultTestSuite()
            {
                Type = type,
                Name = currentMethodName,
                Executed = executed
            };
        }

        public void WriteResultsToFile(string testClassName, List<TestCase> testCases)
        {
            this.currentTestSuite.Results = new Results()
            {
                TestCases = testCases.ToArray()
            };

            ResultTestSuite resultTestSuite = new ResultTestSuite();

            if (this.currentTestSuite.Results != null && this.currentTestSuite.Results.TestCases.Any())
            {
                this.currentTestSuite.Success = true;
                Utilities.WriteToFile(this.currentTestSuite, testClassName);
            }

            this.currentTestSuite = null;
        }

    }
}
