using System;
using System.Globalization;
using OpenQA.Selenium;
using System.IO;

namespace SeShell.Test.Core
{
    public sealed class ScreenShotImage
    {
        /// <summary>
        /// Capture and store the image in jpg format
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="imageName">Name of the image.</param>
        public static void  CaptureScreenShot(IWebDriver driver, string imageName)
        {
            if (driver != null)
            {
                Directory.CreateDirectory(Configuration.ErrorImagePath);    // Creates directory if it doesn't exist
                string imagePath = Configuration.ErrorImagePath + "\\" + imageName + 
                    DateTime.Now.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("hhmmss.") + 
                    DateTime.Now.Millisecond.ToString(CultureInfo.InvariantCulture) + ".jpg";
                Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                ss.SaveAsFile(imagePath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        /// <summary>
        /// delete the given file
        /// </summary>
        /// <param name="filePath"></param>
        public void DeleteFile(string filePath)
        {
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
    }
}
