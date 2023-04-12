<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ChooseCourseDropdown.ascx.cs" Inherits="Controls_ChooseCourseDropdown" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<script type="text/javascript" language="javascript">
    $(document).ready(function(){
        searchList();
        $("select").SelectStyle();
    })

    function searchList() {
        document.getElementById("divSearch").innerHTML = "<select id='ddlCourseID' class='select_190'  onchange='javascript:funSetValue();'>" + SetListData() + "</select>";
        funSetValue();
        $("#ddlCourseID").SelectStyle();
    }

    function SetListData() {
        var strkey = $("#<%=txtSearch.ClientID %>").val();
        var courseList = $("#<%=hfCourseList.ClientID %>").val();
        var courses = courseList.split("#$#");
        var options = "";

        if (strkey != "") {            
            for (i = 0; i < courses.length; i++) {
                var courseinfos = courses[i].split("$#$");
                if (courseinfos[1].indexOf(strkey) != -1) {
                    options += "<option value=" + courseinfos[0] + ">" + courseinfos[1] + "</option>";
                }
            }
        }
        else {

            for (i = 0; i < courses.length; i++) {
                if (courses[i] != "") {
                    var courseinfos = courses[i].split("$#$");
                    options += "<option value=" + courseinfos[0] + ">" + courseinfos[1] + "</option>";
                }
            }
        }       
        return options;        
    }

    function funSetValue() {
        $("#<%=txtCourseID.ClientID %>").val($("#ddlCourseID").val());
    }
</script>

<div style="float:left;line-height:25px; height:25px;"><asp:TextBox ID="txtSearch" runat="server" CssClass="inputbox_90"></asp:TextBox></div>
<div id="divSearch" style="float:left; line-height:25px;height:25px;"><select id='ddlCourseID' class='select_190' onchange="javascript:funSetValue();"><option value="-1">请输入课程名称搜索</option></select></div>
<asp:HiddenField ID="txtCourseID" runat="server" />
<asp:HiddenField ID="hfCourseList" runat="server" />

