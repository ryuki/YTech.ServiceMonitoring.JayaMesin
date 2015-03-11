<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="YTech.ServiceTracker.JayaMesin.Web.Mvc.ReportViewer" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="<%= ResolveUrl("~/Scripts/kendo/2013.1.514/jquery.min.js") %>"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <rsweb:ReportViewer ID="rv" runat="server" Width="98%" Height="650px">
        </rsweb:ReportViewer>
    </div>
    </form>

    <script type="text/javascript">

     function getURLParameter(name) {
        return decodeURI(
            (RegExp(name + '=' + '(.+?)(&|$)').exec(location.search) || [, null])[1]
        );
     }

     jQuery(document).ready(function () {
         //var height = $(window).height();
         //var rv = $("#rv_ReportViewer");
         //alert(height);
         //alert(rv.height());
         //if (height != rv.height()) {
         //    rv.height(height - 18);
         //}

        if (getURLParameter("autoPrint") === "1")
            setTimeout('window.print();', 3000);
     })
        </script>
</body>
</html>
