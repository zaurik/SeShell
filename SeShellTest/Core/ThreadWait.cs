using System;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace SeShell.Test.Core
{
    /// <summary>
    /// Utility is being used to keep the actions waiting for a specified time or 
    /// till the next required object/contorls avaiable 
    /// </summary>
    public sealed class ThreadWait
    {
        const long TimeOut = 3;

        /// <summary>
        /// Standards the sleep.
        /// </summary>
        /// <param name="count">The count.</param>
        public static void StandardSleep(int count)
        {
            for (int i = 0; i < count; i++)
                Thread.Sleep(Configuration.MeanThreadSleepTime);
        }

        /// <summary>
        /// Finds the element with timeout wait.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="by">The by.</param>
        /// <param name="skipEmptyText">if set to <c>true</c> [skip empty text].</param>
        /// <returns></returns>
        public static IWebElement FindElementWithTimeoutWait(IWebDriver driver, By by, bool skipEmptyText)
        {
            Exception ex = null;
            IWebElement e = null;
            long elapsedTime = 0;
            while (elapsedTime < TimeOut)
            {
                try
                {
                    elapsedTime++;
                    StandardSleep(1);
                    e = driver.FindElement(by);
                    if (e.Text.Length > 0 || skipEmptyText)
                    {
                        break;
                    }
                }
                catch (NoSuchElementException nse)
                {
                    ex = nse;
                }
            }

            if (e == null)
                throw ex;

            return e;
        }

        /// <summary>
        /// Finds the element with timeout wait.
        /// </summary>
        /// <param name="currentElement">The current element.</param>
        /// <param name="by">The by.</param>
        /// <returns></returns>
        public static IWebElement FindElementWithTimeoutWait(IWebElement currentElement, By by)
        {
            Exception ex = null;
            IWebElement e = null;
            long elapsedTime = 0;
            while (elapsedTime < TimeOut)
            {
                try
                {
                    elapsedTime++;
                    StandardSleep(1);
                    e = currentElement.FindElement(by);
                    break;
                }
                catch (NoSuchElementException nse)
                {
                    ex = nse;
                }
            }

            if (e == null)
                throw ex;

            return e;
        }

        /// <summary>
        /// Finds the elements with timeout wait.
        /// </summary>
        /// <param name="currentElement">The current element.</param>
        /// <param name="by">The by.</param>
        /// <returns></returns>
        public static ReadOnlyCollection<IWebElement> FindElementsWithTimeoutWait(IWebElement currentElement, By by)
        {
            Exception ex = null;
            ReadOnlyCollection<IWebElement> e = null;
            long elapsedTime = 0;
            while (elapsedTime < TimeOut)
            {
                try
                {
                    elapsedTime++;
                    StandardSleep(1);
                    e = currentElement.FindElements(by);
                    break;
                }
                catch (NoSuchElementException nse)
                {
                    if (e == null)
                        throw ex;
                }
            }

            if (e == null)
                throw ex;

            return e;
        }

        /// <summary>
        /// Finds the element with timeout wait.
        /// </summary>
        /// <param name="currentElement">The current element.</param>
        /// <param name="by">The by.</param>
        /// <returns></returns>
        public static IWebElement FindElementWithTimeoutWait(IWebDriver currentElement, By by)
        {
            Exception ex = null;
            IWebElement e = null;
            long elapsedTime = 0;
            while (elapsedTime < TimeOut)
            {
                try
                {
                    elapsedTime++;
                    StandardSleep(1);
                    e = currentElement.FindElement(by);
                    break;
                }
                catch (NoSuchElementException nse)
                {
                    if (e == null)
                        throw ex;
                }
            }

            if (e == null)
                throw ex;

            return e;
        }

        /// <summary>
        /// Finds the elements with timeout wait.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="by">The by.</param>
        /// <returns></returns>
        public static ReadOnlyCollection<IWebElement> FindElementsWithTimeoutWait(IWebDriver driver, By by)
        {
            Exception ex = null;
            ReadOnlyCollection<IWebElement> e = null;
            long elapsedTime = 0;
            while (elapsedTime < TimeOut)
            {
                try
                {
                    elapsedTime++;
                    StandardSleep(1);
                    e = driver.FindElements(by);
                    break;
                }
                catch (NoSuchElementException nse)
                {
                    if (e == null)
                        throw ex;
                }
            }

            if (e == null)
                throw ex;

            return e;
        }       
    }
}
