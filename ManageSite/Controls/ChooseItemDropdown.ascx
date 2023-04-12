<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ChooseItemDropdown.ascx.cs" Inherits="Controls_ChooseItemDropdown" %>
<script type="text/javascript" language="javascript">
    $(document).ready(function () {
        searchList();
        $("select").SelectStyle();
    })

    function searchList() {
        var strkey = $("#<%=txtSearch.ClientID %>").val();
        var strreturn = "";
        if (strkey != "") {
            for (i = 0; i < arrListData.length; i++) {
                if (arrListData[i][1].indexOf(strkey) != -1) {
                    strreturn += "<option value=" + arrListData[i][0] + ">" + arrListData[i][1] + "</option>"
                }
            }
        }
        else {
            for (i = 0; i < arrListData.length; i++) {
                strreturn += "<option value=" + arrListData[i][0] + ">" + arrListData[i][1] + "</option>"

            }
        }
        document.getElementById("divSearch").innerHTML = "<select id='ddlItemID' class='select_120'  onchange='javascript:funSetValue();'>" + strreturn + "</select>";

        funSetValue();
    }

    function funSetValue() {
        $("#<%=txtItemID.ClientID %>").val($("#ddlItemID").val());
    }
</script>

<div style="float:left;line-height:25px; height:25px;"><asp:TextBox ID="txtSearch" runat="server" CssClass="inputbox_90"></asp:TextBox></div>
<div id="divSearch" style="float:left; line-height:25px;height:25px;"><select id='ddlItemID' class='select_120' onchange="javascript:funSetValue();"><option value="-1">请输入项目名称搜索</option></select></div>
<asp:HiddenField ID="txtItemID" runat="server" />
