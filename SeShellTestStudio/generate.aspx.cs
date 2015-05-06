using System;
using SeShellTestStudio.Utils;

namespace SeShellTestStudio
{
    public partial class generate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           executeButton.Click +=executeButton_Click;
        }

        private void executeButton_Click(object sender, EventArgs e)
        {          
        }

        protected void Page_Init(object sender, EventArgs e)
        {
        }

        private void CreateAndExecuteBatFile()
        {
        }
    }
}