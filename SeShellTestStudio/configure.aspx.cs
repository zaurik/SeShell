using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using SeShell.Test.Core;
using SeShell.Test.Enums;
using SeShellTestStudio.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace SeShellTestStudio
{
    public partial class Configure : System.Web.UI.Page
    {
        private List<TextBox> txtBoxList = new List<TextBox>();
        private List<Control> hiddenControlsList = new List<Control>();
        private Dictionary<string, string> data;
        private const int noOfNewControls = 3;
        private string fileName;
        private List<String> baseValues = new List<string>();
        private Boolean canContinue;
        private CheckBoxList browserCheckBoxList;

        protected void Page_Load(object sender, EventArgs e)
        {
            GetSettings settings = new GetSettings();
            fileName = settings.GetConfigFilePath(Server.MapPath("~"));
            InitBaseValues();
            ReadConfigData(settings.GetConfigFilePath(Server.MapPath("~")));
        }

        private void InitBaseValues()
        {
            baseValues.Add("BaseUrl");
            baseValues.Add("MeanThreadSleepTime");
            baseValues.Add("WebBrowsers");
            baseValues.Add("ErrorImagePath");
            baseValues.Add("TestDataDirectory");
            baseValues.Add("DbConnectionString");
            baseValues.Add("NUnitModifiedTestResultLocation");
            baseValues.Add("AutomationResultNamespace");
            baseValues.Add("ClassesToExecute");
        }

        private void ReadConfigData(string filePathName)
        {
            string xml = System.IO.File.ReadAllText(filePathName);
            data = XElement.Parse(xml)
            .Elements("add")
            .ToDictionary(
                el => (string)el.Attribute("key"),
                el => (string)el.Attribute("value")
            );

            foreach (var obj in data)
            {
                Area1.Controls.Add(new LiteralControl("<p><dd>"));

                Label tempLabel = new Label();
                tempLabel.Text = ConvertToHumanReadable(obj.Key) + ":";
                tempLabel.CssClass = "ui-widget-header";
                Area1.Controls.Add(tempLabel);
                Area1.Controls.Add(new LiteralControl("</dd>"));
                Area1.Controls.Add(new LiteralControl("<dd>"));

                if (obj.Key.Equals("WebBrowsers"))
                {
                    browserCheckBoxList = new CheckBoxList {ID = "browserCheckBoxList"};
                    this.AddWebBrowserCheckBoxListItems(obj.Value.Split(','));
                    Area1.Controls.Add(browserCheckBoxList);
                }
                else
                {
                    TextBox tempTxtbox = new TextBox();
                    tempTxtbox.Text = obj.Value;
                    tempTxtbox.ID = "txt" + obj.Key;
                    switch (obj.Key)
                    {                      
                        default:
                            tempTxtbox.Width = 750;
                            break;
                    }
                    tempTxtbox.Height = 25;
                    tempTxtbox.CssClass = "ui-widget input";
                    Area1.Controls.Add(tempTxtbox);
                    txtBoxList.Add(tempTxtbox);
                                  }
                Area1.Controls.Add(new LiteralControl("</dd></p><br />"));
            }

            //AddHiddenControls();

            Area2.Controls.Add(new LiteralControl("<table width=60% align='center'>"));
            Area2.Controls.Add(new LiteralControl("<tr align='center'>"));

          

            Area2.Controls.Add(new LiteralControl("<td width=33%>"));
            Button btnSubmit = new Button();
            btnSubmit.Text = "Save";
            btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            Area2.Controls.Add(btnSubmit);
            Area2.Controls.Add(new LiteralControl("</td>"));

            Area2.Controls.Add(new LiteralControl("</tr>"));
            Area2.Controls.Add(new LiteralControl("</table><br />"));
        }

        /// <summary>
        /// Adds the WebBrowser CheckBox list items.
        /// </summary>
        private void AddWebBrowserCheckBoxListItems(string[] checkBoxSelectedValues)
        {
            var browserEnumList = (IEnumerable<WebBrowsers>)Enum.GetValues(typeof (WebBrowsers));

            var webBrowserses = browserEnumList as WebBrowsers[] ?? browserEnumList.ToArray();
            for (int i = 0; i < webBrowserses.Count(); i++)
            {
                WebBrowsers currentBrowser = webBrowserses.ElementAt(i);
                var listItem = new ListItem(currentBrowser.ToString());
                listItem.Selected = checkBoxSelectedValues.Contains(currentBrowser.ToString());
                listItem.Attributes.CssStyle.Clear();
                browserCheckBoxList.Items.Add(listItem);
            }

        }

        private bool BelongsToBaseValues(string p)
        {
            Boolean res = false;
            if (baseValues.Contains(p))
            {
                res = true;
            }
            return res;
        }

        private void AddHiddenControls()
        {
            for (int i = 0; i < noOfNewControls; i++)
            {
                TextBox txtKey = new TextBox();
                txtKey.Width = 200;
                txtKey.ID = "txtKey" + i;
                txtKey.CssClass = "ui-widget input";

                TextBox txtValue = new TextBox();
                txtValue.Width = 200;
                txtValue.ID = "txtValue" + i;
                txtValue.CssClass = "ui-widget input";

                HiddenArea.Controls.Add(new LiteralControl("<div><dd>"));

                Label labelKey = new Label();
                labelKey.Text = "Key:";
                labelKey.CssClass = "ui-widget-header";
                HiddenArea.Controls.Add(labelKey);
                HiddenArea.Controls.Add(new LiteralControl("&nbsp;"));
                HiddenArea.Controls.Add(txtKey);
                HiddenArea.Controls.Add(new LiteralControl("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"));

                Label labelValue = new Label();
                labelValue.Text = "Value:";
                labelValue.CssClass = "ui-widget-header";
                HiddenArea.Controls.Add(labelValue);
                HiddenArea.Controls.Add(new LiteralControl("&nbsp;"));
                HiddenArea.Controls.Add(txtValue);
                HiddenArea.Controls.Add(new LiteralControl("</dd></div><br/>"));

                hiddenControlsList.Add(txtKey);
                hiddenControlsList.Add(txtValue);
            }
            HiddenArea.Visible = false;
        }

        private static String ConvertToHumanReadable(String s)
        {
            Regex r = new Regex(@"(?!^)(?=[A-Z])");
            return r.Replace(s, " ");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SaveAllConfig();
        }

        private void SaveAllConfig()
        {
            SaveChanges();
            WriteToFile();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "refresh", "window.setTimeout('var url = window.location.href;window.location.href = url',100);", true);
        }

        private void WriteToFile()
        {
            try
            {
                string[] tmpData = data.Select(x => "\t<add key=\"" + x.Key + "\" value=\"" + x.Value + "\"/>").ToArray();
                string[] arrCopy = new string[500];
                arrCopy[0] = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
                arrCopy[1] = "<appSettings>";
                for (int i = 0; i < tmpData.Length; i++)
                {
                    arrCopy[i + 2] = tmpData[i];
                }
                arrCopy[tmpData.Length + 2] = "</appSettings>";
                File.WriteAllLines(fileName, arrCopy);
                Area1.Controls.Add(new LiteralControl("<script>alert('Configuration is successfully saved')</script>"));
            }
            catch (Exception e)
            {
                Area1.Controls.Add(new LiteralControl("<script>alert('An error occured when saving the configurations')</script>"));
            }
        }

      

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ShowHiddenControls();
        }

        private void ShowHiddenControls()
        {
            HiddenArea.Visible = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "a", "window.scrollTo(0, document.body.scrollHeight);", true);
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> tmpDataD = new Dictionary<string, string>();
            foreach (var it in data)
            {
                tmpDataD.Add(it.Key, it.Value);
            }

            foreach (var item in data)
            {
                if (!BelongsToBaseValues(item.Key))
                {
                    CheckBox chk = (CheckBox)Area1.FindControl("chk" + item.Key);
                    if (chk.Checked == true)
                    {
                        tmpDataD.Remove(item.Key);
                    }
                }
            }
            data = tmpDataD;
            SaveAllConfig();
        }

        private void SaveChanges()
        {
            for (int i = 0; i < data.Count; i++)
            {
                string value;
                if (i < baseValues.Count && baseValues[i].Equals("WebBrowsers"))
                {
                    string browsers = string.Empty;
                    for (int j = 0; j < browserCheckBoxList.Items.Count; j++)
                    {
                        var box = browserCheckBoxList.Items[j];

                        if (box.Selected)
                        {
                            browsers += string.Format("{0}{1}", (browsers.Length > 0) ? "," : string.Empty, box.Text);
                        }
                    }
                    value = browsers;
                    data[baseValues[i]] = value;
                }
                else
                {
                    if (i < baseValues.Count)
                    {
                        value = ((TextBox)Area1.FindControl("txt" + baseValues[i])).Text;
                        data[baseValues[i]] = value;
                    }
                    else
                    {
                        value = ((TextBox)Area1.FindControl("txt" + data.ElementAt(i).Key)).Text;
                        data[data.ElementAt(i).Key] = value;
                    }
                }
            }                             
        }

        private Boolean Validate(String tmpKey)
        {
            Boolean result = false;
            if (!tmpKey.Equals("") && !checkForDuplicates(tmpKey))
            {
                result = true;
            }
            else if (tmpKey.Equals(""))
            {
                //No need
            }
            else
            {
                Area1.Controls.Add(new LiteralControl("<script>alert('Key " + tmpKey + " already exists!')</script>"));
            }
            return result;
        }

        private bool checkForDuplicates(String str)
        {
            return data.ContainsKey(str);
        }
    }
}