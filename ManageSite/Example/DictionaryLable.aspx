<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DictionaryLable.aspx.cs" Inherits="Example_DictionaryLable" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <cc1:DictionaryLabel DictionaryType="Dic_Sys_ELearningMapType" FieldIDValue="1" runat="server"  text="Label" />
    </div>
    </form>
</body>

</html>
