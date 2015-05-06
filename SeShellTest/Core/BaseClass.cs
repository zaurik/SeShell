using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using Selenium;
using SeShell.Test.Enums;
using System.Net;
using System.ComponentModel;
using System.Linq;
using SeShell.Test.Resources;

namespace SeShell.Test.Core
{
    /// <summary>
    /// Base class is used to make the driver initiate and set the base URL
    /// for other page classes to use 
    /// </summary>
    public class BaseClass
    {
        public IWebDriver Driver;
        public string BaseUrl;

        /// <summary>
         /// Navigates the browser to a particular url/page.
         /// </summary>
         /// <param name="url">Page's path (excluding the domain)</param>
        public void NavigateTo(string url)
        {
            var navigateToThisUrl = BaseUrl + url;
            Driver.Navigate().GoToUrl(navigateToThisUrl);
        }

        /// <summary>
        /// Sets the web driver instance.
        /// </summary>
        /// <param name="driver">The driver.</param>
        public void SetWebDriverInstance(IWebDriver driver)
        {
            Driver = driver;
            Driver.Manage().Window.Maximize();
            BaseUrl = Configuration.BaseSiteUrl;
        }
    }
}
