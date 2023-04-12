<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AppraisalEdit.aspx.cs" Inherits="Activity_AppraisalEdit" EnableEventValidation="false" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<%@ Register Src="Controls/AppraisalInfo.ascx" TagName="Appraisal" TagPrefix="uc2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <link href="<%= ETMS.Utility.WebUtility.AppPath %>/JScript/DateTimePicker/jquery.datetimepicker.css" type="text/css" rel="Stylesheet" />
    <script lang="javascript" type="text/javascript" src="<%= ETMS.Utility.WebUtility.AppPath %>/JScript/jquery-1.11.1.min.js"></script>
    <script type="text/javascript" lang="javascript" src='<%=ETMS.Utility.WebUtility.AppPath%>/JScript/jquery.cookie.js'></script>
    <script lang="javascript" type="text/javascript" src="<%= ETMS.Utility.WebUtility.AppPath %>/JScript/DateTimePicker/jquery.datetimepicker.js"></script>
    <script lang="javascript" type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/JScript/jsfunction.js"></script>
    <script lang="javascript" type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/JScript/ymPromptYuan.js"></script>
    <script type="text/javascript" lang="javascript" src='<%=ETMS.Utility.WebUtility.AppPath%>/JScript/tablecloth.js'></script>
    <script type="text/javascript" lang="javascript" src='<%=ETMS.Utility.WebUtility.AppPath%>/JScript/jQueryCache.js'></script> 
    <form id="form1" runat="server">
    <div>
        <uc2:Appraisal ID="appraisal" runat="server" Action="Edit" />
    </div>
    </form>

    <script type="text/javascript" lang="javascript">
        $(function () {
            autoLoadHideGridview();
            isLoadFish();
            createPageControl();
            showTabinfor();
        })
    </script>
</body>
</html>
