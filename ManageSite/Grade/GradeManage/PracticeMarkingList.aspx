<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/MasterPages/MPageAdmin.Master" CodeFile="PracticeMarkingList.aspx.cs" Inherits="Grade_GradeManage_PracticeMarkingList" %>
<%@ Register Src="Controls/MarkingNoSubmit.ascx" TagName="NoSubmit"
    TagPrefix="uc1" %>
<%@ Register Src="Controls/MarkingUnEvaluation.ascx" TagName="UnEvaluation"
    TagPrefix="uc2" %>
<%@ Register Src="Controls/MarkingEvaluated.ascx" TagName="Evaluated"
    TagPrefix="uc3" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" runat="Server">
    <a id="aBack" runat="server" class="btn_Return">返回</a>
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
                id="tableQueryControlList">
                <tr>
                    <th width="120">
                        实践名称：
                    </th>
                    <td width="130">
                        <asp:TextBox ID="txt_JobName" runat="server" CssClass="inputbox_120" MaxLength="50"></asp:TextBox>
                    </td>
                    <th width="100">
                        项目名称：
                    </th>
                    <td class="Search_Area">
                        <asp:TextBox ID="txt_ItemName" runat="server" CssClass="inputbox_120 floatleft" MaxLength="100"></asp:TextBox>
                        <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" OnClick="btnSearch_Click"  Text="查询" />
                    </td>
                </tr>
            </table>
        </div>
    <!--表单录入-->
    <div class="">
        <div class="dv_serviceTab">
            <div class="dv_Tabmenus">
                <ul>
                    <li id="Tab_0" onclick="showTab('Tab_0', 'Div_Select_0','selected')"><a onfocus="blur()"
                        href="javascript:void(0);"><span class="bj">待批阅</span></a></li>
                    <li id="Tab_1" onclick="showTab('Tab_1', 'Div_Select_1','selected')"><a onfocus="blur()"
                        href="javascript:void(0);"><span class="bj">已批阅</span></a></li>
                    <li id="Tab_2" onclick="showTab('Tab_2', 'Div_Select_2','selected')"><a onfocus="blur()"
                        href="javascript:void(0);"><span class="bj">未提交</span></a></li>
                </ul>
            </div>
        </div>
        <div class="info">
            <div id="Div_Select_0" style="display: none">
                <uc2:UnEvaluation ID="MarkingUnEvaluation" runat="server"/>
            </div>
            <div id="Div_Select_1" style="display: none">
                <uc3:Evaluated ID="MarkingEvaluated" runat="server" />
            </div>
            <div id="Div_Select_2" style="display: none">
                <uc1:NoSubmit ID="MarkingNoSubmit" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>

