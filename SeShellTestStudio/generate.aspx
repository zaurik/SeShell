<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="generate.aspx.cs" Inherits="SeShellTestStudio.generate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head>
    <title>Selenium Shell - Generate</title>
	<meta name="description" content="Generate batch file" />
	<meta name="author" content="Selenium Shell Team" />

	<link href="css/basic_styles.css" rel="Stylesheet" type="text/css"/>
	<style type="text/css">
	    body 
	    {
		    margin-top: 0px;
		    background-image: url(images/top.jpg);
		    background-repeat: repeat-x;
		    background-position: top;
		    background-color: #e4e4e4;
	    }
	</style>
    <link href="jquery/css/custom-theme/jquery-ui-1.9.0.custom.css" rel="Stylesheet" type="text/css"/>

    <script src="jquery/js/jquery-1.8.2.js"></script>
    <script src="jquery/js/jquery-ui-1.9.0.custom.min.js"></script> 
    <script type="text/javascript" src="downloadify/js/swfobject.js"></script>
	<script type="text/javascript" src="downloadify/src/downloadify.js"></script>       
</head>

<body onload="load();">           
    <form id="form1" runat="server">
	    <div class="wrapper-container">
		    <div id="header">
		        <center><img src="images/logo.png" alt="logo"/></center>
		    </div>  
		    <div id="content" class="content-wrap">
			    <div id="nav">
				    <div class="menu">
					    <ul>
						    <li><a href="dashboard.aspx"><span>DASHBOARD</span></a></li>
						    <li><a href="configure.aspx"><span>CONFIG</span></a></li>
						    <li><a href="generate.aspx"><span>GENERATE</span></a></li>
					    </ul>
				    </div>    
			    </div>
		    </div>           
            <div class="main" runat="server">
                <div id="summary_box" class="ui-widget-box" runat="server">
                    <center><h1>Generate Test Fixture Bat File</h1></center><br/>
                    <br />
                    <table width=100% align="center">
                        <tr align="center">
                            <td width=50%>
                                <asp:Button runat="server" id="executeButton" Text="Execute"/>
                            </td>
                        </tr>    
                    </table>                                                                                                                  
                </div>
            </div>          
	    </div>
        <div class="clear"><p><br /></p></div>
    </form>
     <script>
         $(function () {
             $("input:submit").button();
         });
	</script>
</body>

</html>
