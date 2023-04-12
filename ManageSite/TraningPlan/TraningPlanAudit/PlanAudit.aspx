<%@ Page Title="审核培训计划" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="PlanAudit.aspx.cs" Inherits="TraningPlan_TraningPlanAudit_PlanAudit" %>

<%@ Register Src="../TraningPlan/Controls/PlanInfoView.ascx" TagName="PlanInfoView"
    TagPrefix="uc3" %>
<%@ Register Src="../TraningPlan/Controls/CourseListView.ascx" TagName="CourseListView"
    TagPrefix="uc4" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" runat="Server">
    <a id="aBack" runat="server" class="btn_Return">返回</a>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="dv_serviceTab">
        <div class="dv_Tabmenus">
            <ul>
                <li id="Tab_0" onclick="showTab('Tab_0', 'Div_Select_0','selected')"><a onfocus="blur()"
                    href="javascript:void(0);"><span class="bj">计划信息</span></a></li>
                <li id="Tab_1" onclick="showTab('Tab_1', 'Div_Select_1','selected')"><a onfocus="blur()"
                    href="javascript:void(0);"><span class="bj">课程信息</span></a></li>
            </ul>
        </div>
    </div>
    <div class="info">
        <div id="Div_Select_0" class="dv_pageInformation">
            <uc3:PlanInfoView ID="PlanInfoView1" runat="server" />
            <table class="GridviewGray">
                <tr>
                    <th width="120">
                        审&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;核
                    </th>
                    <td>
                    </td>
                </tr>
                <tr>
                    <th width="120">
                        审核意见：
                    </th>
                    <td align="left">
                        <asp:TextBox ID="labOpinion" runat="server" TextMode="MultiLine" CssClass="inputbox_area440"></asp:TextBox><font
                            color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidator1" Text="*"
                                Display="None" runat="server" ErrorMessage="请填写审核意见！" ControlToValidate="labOpinion"
                                ValidationGroup="Saves"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnAgree" runat="server" CssClass="btn_Agree" Text="审核通过" OnClick="btnAgree_Click" ValidationGroup="Saves" />
                        <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Saves"
                            ShowMessageBox="true" ShowSummary="false" />
                        <asp:Button ID="btnDeny" runat="server" CssClass="btn_Deny" Text="审核不通过" OnClick="btnDeny_Click" ValidationGroup="Saves" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="Div_Select_1" style="display: none">
            <uc4:CourseListView ID="CourseListView1" runat="server" />
        </div>
    </div>
</asp:Content>
