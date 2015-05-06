using OpenQA.Selenium;
using SeShell.Test.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeShell.Test.Flows
{
    /// <summary>
    /// This was created for demonstrations purposes ONLY.
    /// This class MUST implement the class BaseClass.
    /// </summary>
    public class DummyPageFlow : BaseClass
    {
        public DummyPageFlow(IWebDriver driver)
        {
            base.SetWebDriverInstance(driver);
            NavigateTo(string.Empty);
        }

        public DummyPageFlow DummyPageOperation()
        {
            try
            {
                /*
                 * Operation calls and logic.
                 */

                return new DummyPageFlow(base.Driver);
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }
    }
}
