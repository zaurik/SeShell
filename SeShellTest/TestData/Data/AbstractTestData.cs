using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeShell.Test.TestData.Data
{
    public abstract class AbstractTestData
    {
        public int BrowserTypeEnum { get; set; }

        public int CaseId { get; set; }

        public virtual List<AbstractTestData> GetTestCases { get; set; }

        /// <summary>
        /// Gets the class properties as array.
        /// </summary>
        /// <returns>
        /// An array of the properties of the current object
        /// with each item as string type
        /// </returns>
        public abstract string[] GetClassPropertiesAsArray();
    }

    public class CopyPeriodData : AbstractTestData
    {
        public string AccountPeriod { get; set; }

        public override string[] GetClassPropertiesAsArray()
        {
            throw new NotImplementedException();
        }
    }
}
