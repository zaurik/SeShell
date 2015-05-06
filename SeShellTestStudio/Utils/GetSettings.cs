using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Xml.Linq;
using System.Web.Configuration;
using System.Resources;


namespace SeShellTestStudio.Utils
{
    public class GetSettings
    {
        private Dictionary<string, string> data;
        private string fileName;
        private string DefaultConfigPath = "..\\SeShell.Test.XMLTestResult\\Resources\\TestResultResources.resx";

        public string GetResultPath(string appPath)
        {
            // temporarily hardcoded the default path => change this TODO:
            string DefalutPath = string.Empty;
            string AppPath = appPath ;
            fileName = AppPath + "..\\SeShell.Test.XMLTestResult\\Resources\\TestResultResources.resx";
            string xml = System.IO.File.ReadAllText(fileName);
          
            ResXResourceReader resXResourceReader = new ResXResourceReader(fileName);
            IDictionaryEnumerator dict = resXResourceReader.GetEnumerator();
            while (dict.MoveNext())
            {
                if ((string)dict.Key == "FinalResultLocation")
                {
                    DefalutPath = (string)dict.Value;
                    break;
                }
            }

            return DefalutPath;
        }

        public string GetConfigFilePath(string appPath)
        {
            String path = appPath;
            fileName = path + GetCustomerSetPropertyPath();
            return fileName;
        }

        public string GetCustomerSetPropertyPath()
        {
            return @"..\SeShellTest\Core\Settings\Property.config";
        }

        
    } 
}