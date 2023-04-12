<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SetsStudentListView.ascx.cs"
    Inherits="TraningImplement_ProjectCoursePeriod_Controls_SetsStudentListView" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<div class="dv_GradeviewList">
    <table  border="0" cellpadding="0" cellspacing="0" runat="server"
        id="tableQueryControlList" class="th70">
        <tr>
            <th style="width:80px">
                姓&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;名：
            </th>
            <td style="width:100px">
                <asp:TextBox ID="txt_u999RealName" runat="server" CssClass="inputbox_90"></asp:TextBox>
            </td>
            <th style="width:80px">
                工&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;号：
            </th>
            <td  class="Search_Area" >
                <asp:TextBox ID="txt_s999WorkerNo" runat="server" CssClass="inputbox_90 floatleft"></asp:TextBox><asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>
</div>
<!--查找结果-->
<div class="dv_searchlist">
    <!--翻页-->
    <div class="dv_pagePanel" id="divPage1">
        <div class="dv_selectAll">
        </div>
        <div class="dv_pageControl">
            <uc2:PageSet ID="PageSet1" runat="server" />
        </div>
    </div>
    <!--列表 begin-->
    <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
        CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="1"
        DataKeyNames="ItemCourseHoursStudentID" 
        onrowdatabound="CustomGridView1_RowDataBound">
        <Columns>
            <asp:BoundField DataField="WorkerNo" HeaderText="工号" HeaderStyle-CssClass="alignleft field10" ItemStyle-CssClass="alignleft" />
            <asp:BoundField DataField="RealName" HeaderText="姓名" HeaderStyle-CssClass="field8 alignleft" ItemStyle-CssClass="alignleft" />
            <asp:TemplateField HeaderText="组织机构" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                <ItemTemplate>
                    <cc1:DictionaryLabel ID="lblOrganization" DictionaryType="Dic_CurrentAndSubOrganization" FieldIDValue='<%# Eval("OrganizationID") %>'
                        runat="server" TextLength="10" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                <ItemTemplate>
                    <cc1:DictionaryLabel ID="lblDepartmentID" DictionaryType="vw_Dic_Sys_Department"
                        FieldIDValue='<%# Eval("DepartmentID") %>' runat="server" TextLength="10" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_position%>" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                <ItemTemplate>
                    <cc1:DictionaryLabel ID="lblPost" DictionaryType="vw_Dic_Sys_Post" TextLength="10"
                        FieldIDValue='<%# Eval("PostID") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_rank%>" HeaderStyle-Width="100" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                <ItemTemplate>
                    <cc1:DictionaryLabel ID="lblRank" DictionaryType="vw_Dic_Sys_Rank" TextLength="10"
                        FieldIDValue='<%# Eval("RankID") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </cc1:CustomGridView>
    <!--列表 end-->
    <div class="dv_splitLine">
    </div>
    <!--翻页-->
    <div class="dv_pagePanel" id="divPage2">
    </div>
</div>
