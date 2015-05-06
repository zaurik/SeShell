using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using System.IO;
using System.Web.Services;
using System.Collections.Generic;
using System.Xml.Linq;
using SeShell.Test.XMLTestResult;
using SeShell.Test.XMLTestResult.XMLObjects;
using SeShellTestStudio.Utils;

namespace SeShellTestStudio
{
    public partial class dashboard : System.Web.UI.Page
    {
        private string dir = "";
        private string result;
        private string summary;
        private string acc_name;
        private int acc_num;
        private int openDivCount;
        private int numOfTestFixtures;
        private bool isBeingExecuted;

        protected void Page_Load(object sender, EventArgs e)
        {
            GetSettings settings = new GetSettings();
            dir = settings.GetResultPath(Server.MapPath("~"));
            isBeingExecuted = BatFileProcessor.TestsBeingExecuted();
            showContent();
        }

        private void showContent()
        {
            execute();
            summary_box.InnerHtml = summary;
            result_accordion.InnerHtml = result;
        }

        private void TestCaseHeaderAndSummary(ResultTestSuite obj)
        {
            numOfTestFixtures++;
            if (openDivCount != 0)
            {
                result += "</div></div>";
                openDivCount = 0;
            }
            acc_name = "accordion" + acc_num;
            acc_num++;
            result += "<h3><a href='#'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + obj.Name;
            if (obj.Success == true)
            {
                result += "<img src='images/success.png' ";
            }
            else
            {
                result += "<img src='images/failure.png' ";
            }
            result += "style='position: absolute; left: .5em; top: 50%; margin-top: -8px;'/></a></h3><div>";
            result += "<script>$(document).ready(function (){$('#" + acc_name + "').accordion({ clearStyle: true, autoHeight: false, collapsible: true, active: false });});</script>";
            result += "<div id='" + acc_name + "'>";
        }

        private void TestCaseDetail(ResultTestSuite obj)
        {
            openDivCount = 2;
            foreach (var TCaseObject in obj.Results.TestCases)
            {
                result += "<h3><a href='#'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + TCaseObject.Name;
                if (TCaseObject.Success)
                {
                    result += "<img src='images/success.png' ";
                }
                else
                {
                    result += "<img src='images/failure.png' ";
                }
                result += "style='position: absolute; left: .5em; top: 50%; margin-top: -8px;'/></a></h3><div>";
                result += "<table width='100%'>";
                result += "<tr>";
                result += "<td width='20%' align='center'><b>Executed</b></td>";
                result += "<td width='20%' align='center'><b>Success</b></td>";
                result += "<td width='20%' align='center'><b>Asserts</b></td>";
                result += "<td width='20%' align='center'><b>Time</b></td>";
                result += "</tr>";
                result += "<tr>";
                result += "<td width='20%' align='center'>" + TCaseObject.Executed + "</td>";
                result += "<td width='20%' align='center'>" + TCaseObject.Success + "</td>";
                result += "<td width='20%' align='center'>" + TCaseObject.Asserts + "</td>";
                result += "<td width='20%' align='center'>" + TCaseObject.Time + "</td>";
                result += "</tr>";
                result += "</table>";
                if (TCaseObject.Failure != null)
                {
                    result += "<br/>";
                    result += "<table width='100%'>";
                    result += "<tr>";
                    result += "<td width='20%' align='center'><b>Message</b></td>";
                    result += "<td width='80%'>" + TCaseObject.Failure.Message + "</td>";
                    result += "</tr>";
                    result += "<tr>";
                    result += "<td width='20%' align='center'><b>Stack Trace</b></td>";
                    result += "<td width='80%'>" + TCaseObject.Failure.StackTrace + "</td>";
                    result += "</tr>";
                    result += "</table>";
                }
                result += "</div>";
            }
        }

        private void TestSummary(TestResults TResults)
        {
            string reportType = (!isBeingExecuted) ? "Final Report" : "Intermediate Report";
            summary += "<center><h1>" + TResults.Name + " " + reportType + "</h1><h3>Date: " + TResults.Date + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Report Generated At: " + TResults.Time + "</h3></center><br/>";
            summary += "<table width=100%>";
            summary += "<tr style='font-size:14px'>";
            summary += "<td width=20% align='center'><b>Total</b></td>";
            summary += "<td width=20% align='center'><b>Successful</b></td>";
            summary += "<td width=20% align='center'><b>Errors</b></td>";
            summary += "</tr>";
            summary += "<tr style='font-size:56px'>";
            summary += "<td width=20% align='center'>" + TResults.Total + "</td>";
            summary += "<td width=20% align='center'><font color='#00D300'>" + (int.Parse(TResults.Total) - int.Parse(TResults.Errors)) + "</font></td>";
            summary += "<td width=20% align='center'><font color='#FF0000'>" + TResults.Errors + "</font></td>";
            summary += "</table>";
        }

        protected void execute()
        {
            TestResults TResults = null;
            bool emptyReport = false;

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            string reportDir = string.Empty;
            if (isBeingExecuted)
            {
                reportDir = System.IO.Path.GetTempPath();
                string report = reportDir + "temp";
                emptyReport = !MainClass.Report(report + ".xml", report);
            }
            else
            {
                reportDir = dir;
            }

            var directory = new DirectoryInfo(reportDir);
            string path = "";

            if (directory.GetFiles("*.xml").Length > 0)
            {
                if (Data.xmlFileName == null)
                {
                    var latestFile = directory.GetFiles().OrderByDescending(f => f.LastWriteTime).First();
                    path = directory + "\\" + latestFile.ToString();
                }
                else
                {
                    path = directory + "\\" + Data.xmlFileName + ".xml";
                    Data.xmlFileName = null;
                }

                var serializer = new XmlSerializer(typeof(TestResults));

                using (var reader = new StreamReader(path))
                {
                    TResults = (TestResults)serializer.Deserialize(reader);
                    reader.Close();

                    if (TResults.Total == null || emptyReport)
                    {
                        summary += "<h1><center>Invalid test result file. Please run again.</center></h1>";
                        //selectButton.Visible = false;
                        return;
                    }

                    result = "<script>$(document).ready(function (){$('#accordion').accordion({ clearStyle: true, autoHeight: false, collapsible: true, active: false});});</script><div id='accordion'>";

                    // parse the xml file here!!  based on the object model
                    if (TResults.TestSuite != null && TResults.TestSuite.Results != null)
                    {
                        for (int i = 0; i < TResults.TestSuite.Results.TestSuites[0].Results.TestSuites.Count(); i++)
                        {
                            TestCaseHeaderAndSummary(TResults.TestSuite.Results.TestSuites[0].Results.TestSuites[i]);
                            TestCaseDetail(TResults.TestSuite.Results.TestSuites[0].Results.TestSuites[i]);
                        }
                    }

                    result += "</div></div></div><br />";

                    // selectButton.Controls.Add(new LiteralControl("<input type='submit' id='selectFiles' value='Select File' />"));
                    //selectButton.Controls.Add(new LiteralControl("<div id='dialog' title='Select Test Result File'>"));

                    DropDownList drplist = new DropDownList { ID = "dropdownlistFiles" };
                    foreach (var file in directory.GetFiles("*.xml"))
                    {
                        drplist.Items.Add(file.Name.Replace(".xml", ""));
                    }

                    //selectButton.Controls.Add(drplist);
                    //selectButton.Controls.Add(new LiteralControl("</div>"));

                    TestSummary(TResults);
                }                              
            }
            else
            {
                summary += "<h1><center>THERE ARE NO XML FILES IN THIS DIRECTORY</center></h1>";
                //selectButton.Visible = false;
            }
        }

        [WebMethod]
        public static void reloadResults(string selectedfileName)
        {
            Data.xmlFileName = selectedfileName;
        }
    }
}