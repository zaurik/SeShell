using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Xml.Serialization;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeShell.Test.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SeShell.Test.Flows;
using SeShell.Test.PageObjects;
using SeShell.Test.Resources;
using SeShell.Test.TestCases;
using SeShell.Test.TestData.Data;
using SeShell.Test.XMLTestResult.Interface;

namespace SeShell.Test.Core
{
    public class Utilities
    {
        public static string ProjectDirectory 
        {
            get
            {
                return Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString()).ToString();
            }
        }

        public static string CombinedString(string a, string b)
        {
            return string.Format("{0}\\{1}", a, b);
        }

        public static IWebDriver CreateBrowser(WebBrowsers browserType)
        {
            string driversPath = Environment.CurrentDirectory + "\\BrowserDrivers";
            switch (browserType)
            {
                case WebBrowsers.Chrome:
                    return new ChromeDriver(driversPath);
                case WebBrowsers.FireFox:
                    return new FirefoxDriver();
                case WebBrowsers.Ie:
                    return new InternetExplorerDriver(driversPath, new InternetExplorerOptions { IntroduceInstabilityByIgnoringProtectedModeSettings = true });
                default:
                    return null;
            }
        }

        /// <summary>
        /// Clears the elements.
        /// </summary>
        /// <param name="elements">The elements.</param>
        public static void ClearElements(IWebElement[] elements)
        {
            if (elements != null)
            {
                foreach (var item in elements)
                {
                    item.Clear();
                } 

                ThreadWait.StandardSleep(1);
            }
        }

        /// <summary>
        /// Finds the element.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="by">The by.</param>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        public static IWebElement FindElement(IWebDriver driver, HtmlElementBy by, string element)
        {
            switch (by)
            {
                case HtmlElementBy.Id:
                    return driver.FindElement(By.Id(element));
                case HtmlElementBy.XPath:
                    return driver.FindElement(By.XPath(element));
                case HtmlElementBy.ClassName:
                    return driver.FindElement(By.ClassName(element));
                case HtmlElementBy.Name:
                    return driver.FindElement(By.Name(element));
                default:
                    return null;
            }
        }

        /// <summary>
        /// Scroll the page to locate the element
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="element"></param>
        public static void ScrollToElement(IWebDriver driver, IWebElement element)
        {
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            js.ExecuteScript("window.scrollTo(" + 0 + "," + (element.Location.Y - 100) + ");");
        }

        /// <summary>
        /// Finds the elements.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="by">The by.</param>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        public static ReadOnlyCollection<IWebElement> FindElements(IWebDriver driver, HtmlElementBy by, string element)
        {
            switch (by)
            {
                case HtmlElementBy.Id:
                    return driver.FindElements(By.Id(element));
                case HtmlElementBy.XPath:
                    return driver.FindElements(By.XPath(element));
                case HtmlElementBy.ClassName:
                    return driver.FindElements(By.ClassName(element));
                case HtmlElementBy.Name:
                    return driver.FindElements(By.Name(element));
                case HtmlElementBy.CssSelector:
                    return driver.FindElements(By.CssSelector(element));
                default:
                    return null;
            }
        }

        /// <summary>
        /// Finds the element.
        /// </summary>
        /// <param name="parentElement">The parent element.</param>
        /// <param name="by">The by.</param>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        public static IWebElement FindElement(IWebElement parentElement, HtmlElementBy by, string element)
        {
            switch (by)
            {
                case HtmlElementBy.Id:
                    return parentElement.FindElement(By.Id(element));
                case HtmlElementBy.XPath:
                    return parentElement.FindElement(By.XPath(element));
                case HtmlElementBy.ClassName:
                    return parentElement.FindElement(By.ClassName(element));
                case HtmlElementBy.Name:
                    return parentElement.FindElement(By.Name(element));
                case HtmlElementBy.CssSelector:
                    return parentElement.FindElement(By.CssSelector(element));
                default:
                    return null;
            }
        }

        /// <summary>
        /// Finds the element.
        /// </summary>
        /// <param name="parentElement">The parent element.</param>
        /// <param name="by">The by.</param>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        public static ReadOnlyCollection<IWebElement> FindElements(IWebElement parentElement, HtmlElementBy by, string element)
        {
            switch (by)
            {
                case HtmlElementBy.Id:
                    return parentElement.FindElements(By.Id(element));
                case HtmlElementBy.XPath:
                    return parentElement.FindElements(By.XPath(element));
                case HtmlElementBy.ClassName:
                    return parentElement.FindElements(By.ClassName(element));
                case HtmlElementBy.Name:
                    return parentElement.FindElements(By.Name(element));
                case HtmlElementBy.CssSelector:
                    return parentElement.FindElements(By.CssSelector(element));
                default:
                    return null;
            }
        }

        public static string GetWebBrowser(IWebDriver driver)
        {
            string webBrowserString = string.Empty;
            if (driver is ChromeDriver)
            {
                webBrowserString = WebBrowsers.Chrome.ToString();
            }
            else if (driver is FirefoxDriver)
            {
                webBrowserString = WebBrowsers.FireFox.ToString();
            }
            else if (driver is InternetExplorerDriver)
            {
                webBrowserString = WebBrowsers.Ie.ToString();
            }

            return webBrowserString;
        }

        public static int GetWebBrowserValue(IWebDriver driver)
        {
            if (driver != null)
            {
                if (driver is ChromeDriver)
                {
                    return (int) WebBrowsers.Chrome;
                }
                if (driver is FirefoxDriver)
                {
                    return (int)WebBrowsers.FireFox;
                }
                if (driver is InternetExplorerDriver)
                {
                    return (int)WebBrowsers.Ie;
                }
            }

            return 0;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCurrentMethod()
        {
            var st = new StackTrace();
            var sf = st.GetFrame(1);

            return sf.GetMethod().Name;
        }

        public static void WriteToFile(ITestResults foo, string fileName)
        {

            XmlSerializer serializer = new XmlSerializer(foo.GetType());
            Directory.CreateDirectory(Configuration.NUnitModifiedTestResultLocation);

            using (var writer = 
                new StreamWriter(string.Format("{0}{1}-{2}.xml", Configuration.NUnitModifiedTestResultLocation, fileName, DateTime.Now.Ticks)))
            {
                serializer.Serialize(writer, foo);
            }
        }

        // TODO - Is this necessary?
        //public static FBLoginData GetBrowserBasedLoginCredentials(IWebDriver driver)
        //{
        //    var loginTestData = FBLoginData.GetLoginTestCases(); 
        //    string currentWebBrowserString = GetWebBrowser(driver);
        //    WebBrowsers currentWebBrowser;
        //    Enum.TryParse<WebBrowsers>(currentWebBrowserString, true, out currentWebBrowser);
        //    return loginTestData.FirstOrDefault(x => x.BrowserTypeEnum == (int)currentWebBrowser);
        //}      
       
        /// <summary>
        /// Js the query date work around.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="date">The date.</param>
        public static void JQueryDateWorkAround(IWebElement element, string date, int iter, IWebDriver driver = null, string XPath = null)
        {
            // workaround to get element by XPath in JS iteself
            var script = string.Format("document.getElementById('{0}').value = ''", element.GetAttribute("id"));
            var javaScriptExecutor = (IJavaScriptExecutor)driver;
            var ele = (IWebElement) javaScriptExecutor.ExecuteScript(script);
            ThreadWait.StandardSleep(1);
            javaScriptExecutor.ExecuteScript(string.Format("document.getElementById('{0}').value = '{1}'", element.GetAttribute("id"), date));
       }

        public static void ClearTextFieldWorkAround(IWebElement element, IWebDriver driver)
        {
            var script = string.Format("document.getElementById('{0}').value = ''", element.GetAttribute("id"));
            //, element.GetAttribute("id"), iter

            // var script = string.Format(AMCommonResources.String1);
            var javaScriptExecutor = (IJavaScriptExecutor)driver;
            var ele = (IWebElement)javaScriptExecutor.ExecuteScript(script);
        }

        public static IWebElement ShowHiddenFileUploadElement(IWebDriver driver)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("document.getElementsByName('file[]')[0].setAttribute('style','display:block');", "");
            return Utilities.FindElement(driver, HtmlElementBy.Name, "file[]");            
        }

        public static void UploadFile(IWebDriver driver, string fileLocation)
        {
            var hiddenFileuploadElement = ShowHiddenFileUploadElement(driver);
            hiddenFileuploadElement.SendKeys(fileLocation);
        }

        public static void SelectItemFromDropdown(IWebElement dropdownElement, string optionText)
        {
            var selectElement = new SelectElement(dropdownElement);
            selectElement.SelectByText(optionText);
        }

        public static string GenerateTestFixtureName(Type currentType, string currentlyExecutingMethod, string currentBrowser)
        {
            return string.Format("{0}.{1}.{2}{3}", Configuration.AutomationResultNamespace, currentType.Name, currentlyExecutingMethod, currentBrowser);
        }

        public static string CombineTestOutcomeString(string testOutcomeMessage, string uniqueIdentifier)
        {
            return string.Format(testOutcomeMessage, uniqueIdentifier);
        }

        /// <summary>
        /// Gets the enum description.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// the string based description of the enums
        /// </returns>
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static bool VerifyLink(string link, IWebDriver driver, IWebElement element)
        {
            //Asumes the link is always a web page link. Not a link to a resource
            string currentWebBrowserString = GetWebBrowser(driver);
            var downloadDir = Configuration.DownloadsFolder;
            if (currentWebBrowserString == "Chrome")
            {
                element.Click();
                ThreadWait.StandardSleep(1);
                
                //new Actions(driver).SendKeys(driver.FindElement(By.TagName("html")), Keys.Control).SendKeys(driver.FindElement(By.TagName("html")), Keys.NumberPad2).Build().Perform();
                //Thread.Sleep(1);
                //new Actions(driver).SendKeys(driver.FindElement(By.TagName("html")), Keys.Control).SendKeys(driver.FindElement(By.TagName("html")), Keys.NumberPad1).Build().Perform();
                var popup = driver.WindowHandles[1]; // handler for the new tab
                if ((string.IsNullOrEmpty(popup)))
                {
                    return false;
                }
                if (driver.SwitchTo().Window(popup).Url != link)
                {
                    return false;
                }

                driver.SwitchTo().Window(driver.WindowHandles[1]).Close(); // close the tab
                driver.SwitchTo().Window(driver.WindowHandles[0]); // get back to the main window
            }
            else if (currentWebBrowserString == "FireFox")
            {
            }
            else
            {
                throw new Exception();
            }
            return true;
        }

        public static string CustomException(Exception exception, string testCaseId)
        {
            return string.Format(@"Test Case Id {0}: {1}", testCaseId,
                 exception.Message);
        }

        public static List<WebBrowsers> GetWebBrowsersBasedOnArray(string[] arrayFromConfig)
        {
            return arrayFromConfig.Select(t => (WebBrowsers)Enum.Parse(typeof(WebBrowsers), t, true)).ToList();
        }

        public static string DataFileLocation(string fileLocation)
        {
            string assemblyLocation = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString()).ToString();
            return Path.Combine(assemblyLocation, fileLocation);
        }
    }
}
