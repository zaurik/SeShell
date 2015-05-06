using SeShell.Test.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeShell.Test.TestData.Data
{
    /// <summary>
    /// Dummy data class created for demonstration ONLY.
    /// This class MUST implement the (abstract) class 'AbstractTestData'.
    /// </summary>
    public class DummyOperationData : AbstractTestData
    {
        // Dummy properties
        #region Properties
        
        #endregion

        public static List<object> GetTestData()
        {
            List<object> testData = new List<object>();
            string inputLine;
            using (FileStream inputStream =
                new FileStream(Configuration.TestDataFilePath + @"\FBLoginTestData.csv",
                    FileMode.Open,
                    FileAccess.Read))
            {
                StreamReader streamReader = new StreamReader(inputStream);

                while ((inputLine = streamReader.ReadLine()) != null)
                {
                    var data = inputLine.Split(',');
                    testData.Add(new DummyOperationData
                    {
                        //initialise properties

                    });
                }

                streamReader.Close();
                inputStream.Close();
            }

            return testData;
        }

        public override string[] GetClassPropertiesAsArray()
        {
            throw new NotImplementedException();
        }
    }
}
