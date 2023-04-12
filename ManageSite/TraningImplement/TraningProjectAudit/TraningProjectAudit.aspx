<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="TraningProjectAudit.aspx.cs" Inherits="TraningImplement_TraningProjectAudit_TraningProjectAudit" %>

<%@ Register Src="../TraningProjectManager/Controls/TraningProjectView.ascx" TagName="TraningProjectView"
    TagPrefix="uc1" %>
<%@ Register Src="../TraningProjectManager/Controls/TraningStudentListView.ascx"
    TagName="TraningStudentListView" TagPrefix="uc2" %>
<%@ Register Src="../TraningProjectManager/Controls/TraningCourseListView.ascx" TagName="TraningCourseListView"
    TagPrefix="uc3" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" runat="Server">
    <a href="TraningProjectList.aspx" class="btn_Return">返回</a>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="dv_serviceTab">
        <div class="dv_Tabmenus">
            <ul>
                <li id="Tab_0" onclick="showTab('Tab_0', 'Div_Select_0','selected');"><a onfocus="blur()"
                    href="javascript:void(0);"><span class="bj">项目信息</span></a></li>
                <li id="Tab_1" onclick="showTab('Tab_1', 'Div_Select_1','selected');"><a onfocus="blur()"
                    href="javascript:void(0);"><span class="bj">包含课程</span></a></li>
            </ul>
        </div>
    </div>
    <div class="info">
        <div id="Div_Select_0" class="dv_pageInformation" style="display: none">
            <uc1:TraningProjectView ID="TraningProjectView1" runat="server" />
            <table class="GridviewGray">
                <tr>
                    <th width="10%">
                        审&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;核
                    </th>
                    <td>
                    </td>
                </tr>
                <tr>
                    <th width="20%">
                        审核意见：
                    </th>
                    <td align="left">
                        <asp:TextBox ID="labOpinion" runat="server" TextMode="MultiLine" CssClass="inputbox_area440"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnAgree" runat="server" CssClass="btn_Agree" Text="审核通过" OnClick="btnAgree_Click" />
                        <asp:Button ID="btnDeny" runat="server" CssClass="btn_Deny" Text="审核不通过" OnClick="btnDeny_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="Div_Select_1" style="display: none">
            <uc3:TraningCourseListView ID="TraningCourseListView3" runat="server" />
        </div>
    </div>
    <script type="text/javascript">
        function locationBack() {
            window.location = "TraningProjectList.aspx";
        }
    </script>
</asp:Content>
