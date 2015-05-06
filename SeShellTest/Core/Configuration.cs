using System;
using System.Collections.Generic;
using System.Configuration;
using SeShell.Test.Enums;

namespace SeShell.Test.Core
{
    class Configuration {         

        //Application base URL
        public static string BaseSiteUrl         
        {
            get { return ConfigurationManager.AppSettings["BaseUrl"]; }         
        }

        public static List<Type> ClassesToExecute
        {
            get
            {
                var classNames = ConfigurationManager.AppSettings["ClassesToExecute"].Split(',');
                List<Type> typeArray = new List<Type>();
                foreach (var className in classNames)
                {
                    Type classType = Type.GetType("SeShell.Test.TestCases." + className, true);
                    typeArray.Add(classType);
                }
                return typeArray;
            }
        }

        //Storage path location of Error image snapshots
        public static string ErrorImagePath
        {
            get { return Utilities.CombinedString(NUnitModifiedTestResultLocation, ConfigurationManager.AppSettings["ErrorImagePath"]); }
        }

        //Data driven test data path specifined in confige
        public static string TestDataFilePath
        {
            get { return Utilities.CombinedString(Utilities.ProjectDirectory, ConfigurationManager.AppSettings["TestDataDirectory"]); }
        }

        //upload data path specifined in confige
        public static string TestDataUploadDirectory
        {
            get { return ConfigurationManager.AppSettings["TestDataUploadDirectory"]; }
        }

        //Retunrs the Login page name
        public static string LoginPage
        {
            get { return ConfigurationManager.AppSettings["LoginPage"]; }
        }

        //System setting to wait for a response
        //Returns the configured values else returns 10 seconds
        public static int MeanThreadSleepTime
        {
            get
            {
                try { return Int32.Parse(ConfigurationManager.AppSettings["MeanThreadSleepTime"]); }
                catch
                { return 6500; }
            }
        }

        //Returns AdminSiteURL if given
        public static string AdminSiteUrl         
        {             
            get { return ConfigurationManager.AppSettings["AdminSiteUrl"]; }         
        }

        //Retuns the configured web browser  
        public static WebBrowsers BrowserType        
        {             
            get {
                try { 
                    return (WebBrowsers) Enum.Parse(typeof(WebBrowsers), ConfigurationManager.AppSettings["BrowserType"],true); 
                }  
                catch
                { return Enums.WebBrowsers.FireFox; }
            }
                       
        }

        public static string DatabaseConnectionString 
        {
            get
            {
                return ConfigurationManager.AppSettings["DbConnectionString"]; 
            }
        }

        public static string NUnitModifiedTestResultLocation
        {
            get
            {
                return ConfigurationManager.AppSettings["NUnitModifiedTestResultLocation"];
            }
        }

        public static string AutomationResultNamespace
        {
            get { return ConfigurationManager.AppSettings["AutomationResultNamespace"]; }
        }

        public static string DownloadsFolder
        {
            get { return ConfigurationManager.AppSettings["DownloadsFolder"]; }
        }

        //Retuns the configured web browser  
        public static List<WebBrowsers> WebBrowsers
        {
            get
            {
                try
                {
                    return Utilities.GetWebBrowsersBasedOnArray(ConfigurationManager.AppSettings["WebBrowsers"].Split(','));
                }
                catch
                {
                    return null;
                }
            }

        }

    } 
} 
