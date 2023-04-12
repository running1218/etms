<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TraningStudentListView.ascx.cs"
    Inherits="TraningImplement_TraningProjectManager_Controls_TraningStudentListView" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<!--查找条件-->
<div class="dv_searchbox">
    <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
        id="tableQueryControlList">
        <tr>
            <th width="100">
                工&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;号：
            </th>
            <td width="200">
                <asp:TextBox ID="txt_Site_Student999WorkerNo" runat="server" CssClass="inputbox_120"></asp:TextBox>
            </td>
            <th width="100">
                姓&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;名：
            </th>
            <td class="Search_Area">
                <asp:TextBox ID="txt_Site_User999RealName" runat="server" CssClass="inputbox_120 floatleft"></asp:TextBox>
                <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>
</div>

<!--查找结果-->
<div class="dv_searchlist">
    <!--翻页-->
    <div class="dv_pagePanel" id="divPage1">
        <div class="dv_pageControl">
            <uc2:PageSet ID="PageSet1" runat="server" />
        </div>
    </div>
    <!--列表 begin-->
    <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
        CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0"
        DataKeyNames="StudentSignupID" 
        onrowdatabound="CustomGridView1_RowDataBound">
        <Columns>
            <asp:BoundField DataField="WorkerNo" HeaderText="工号" ItemStyle-CssClass="alignleft"
                HeaderStyle-CssClass="alignleft" />
            <asp:BoundField DataField="RealName" HeaderText="姓名" ItemStyle-CssClass="alignleft"
                HeaderStyle-CssClass="alignleft" />
            <asp:TemplateField HeaderText="组织机构" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                <ItemTemplate>
                    <cc1:DictionaryLabel ID="lblOrganization" DictionaryType="Dic_CurrentAndSubOrganization"
                        FieldIDValue='<%# Eval("OrganizationID") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <%--<asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                <ItemTemplate>
                    <cc1:DictionaryLabel ID="lblDepartmentID" DictionaryType="vw_Dic_Sys_Department"
                        FieldIDValue='<%# Eval("DepartmentID") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_position%>" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                <ItemTemplate>
                    <cc1:DictionaryLabel ID="lblPost" DictionaryType="vw_Dic_Sys_Post" FieldIDValue='<%# Eval("PostID") %>'
                        runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_rank%>" HeaderStyle-Width="100">
                <ItemTemplate>
                    <cc1:DictionaryLabel ID="lblRank" DictionaryType="vw_Dic_Sys_Rank" FieldIDValue='<%# Eval("RankID") %>'
                        runat="server" />
                </ItemTemplate>
            </asp:TemplateField>--%>
        </Columns>
    </cc1:CustomGridView>
    <!--列表 end-->
    <div class="dv_splitLine">
    </div>
    <!--翻页-->
    <div class="dv_pagePanel" id="divPage2">
    </div>
</div>
