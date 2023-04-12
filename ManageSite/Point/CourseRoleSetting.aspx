<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="CourseRoleSetting.aspx.cs" Inherits="Point_CourseRoleSetting" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" runat="Server">
    <a id="aBack" runat="server" class="btn_Return">返回</a>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript" src='<%=ETMS.Utility.WebUtility.AppPath%>/JScript/Point.js'></script>
    <script type="text/javascript" language="javascript">
       
    </script>

    <div class="dv_searchbox">
        <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
            id="tableQueryControlList">
            <tr>
                <th width="100">
                    课程属性：
                </th>
                <td >
                    <cc1:DictionaryLabel ID="lblCourseAttrID" runat="server" DictionaryType="Dic_Sys_CourseAttr"
                        TextLength="20" />
                </td>
            </tr>
        </table>
    </div>
    <!--表单录入-->
    <div class="dv_searchlist">
        <!--查找条件-->
        <table class="GridviewGray GridviewGrayboder" id="poinTable">
            <tr>
                <th class="center">
                    课时临界点（含临界点）
                </th>
                <th class="center">
                    积分
                </th>
                <th class="center">
                    操作
                </th>
            </tr>
            <%for (int i = 0; i < CourseRoleTable.Rows.Count; i++)
              {%>
            <tr id="tr_<%=i %>">
                <td class="center">
                    <span>
                        <input type="text" id="MaxNum_<%=i %>" name="MaxNum" value='<%=MaxNum(i) %>' maxlength="6" onchange="javascript:maxCheck('MaxNum_<%=i %>');" />
                    </span>
                </td>
                <td class="center">
                    <input type="text" id="GivePoints_<%=i %>" name="GivePoints" value='<%= GivePoints(i) %>' maxlength="6"
                         onchange="javascript:pointcheck('GivePoints_<%=i %>');"/>
                </td>
                <td width="40">
                    <a href="javascript:pointdeloption('tr_<%=i %>')">删除</a>
                </td>
            </tr>
            <% }%>
        </table>
        <div class="dv_splitLine"> </div>
        <a href="javascript:void(0)" id="addansweroption" onclick="pointaddoption()" class="btn_Add GridviewGraya">
            增加</a>
        <asp:HiddenField ID="pointOptionList" runat="server" />
    </div>
    <div class="dv_submit">
        <asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Button ID="btnUpdate" runat="server" Text="保存" OnClick="btnUpdate_Click" SkinID="Edit"
                    CommandName="edit" />
                <asp:Button ID="btnReturn" SkinID="Return" runat="server" Text="返回"/>
            </ContentTemplate>
        </asp:UpdatePanel>
       
    </div>
</asp:Content>
