<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StudentNoSelect.ascx.cs"
    Inherits="TraningImplement_StudentCourseManager_Controls_StudentNoSelect" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<!--查找条件-->
<div class="dv_searchbox">
    <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
        id="tableQueryControlList">
        <tr>
            <th width="100" style="display:none;">
                学员工号：
            </th>
            <td align="left" style="display:none;">
                <asp:TextBox ID="txt_s999WorkerNo" runat="server" CssClass="inputbox_120 floatleft"></asp:TextBox>
            </td>
            <th width="100">
                学员姓名：
            </th>
            <td class="Search_Area" align="left">
                <asp:TextBox ID="txt_u999RealName" runat="server" CssClass="inputbox_120 floatleft"
                    MaxLength="100"></asp:TextBox><asp:Button ID="btnSearch" CssClass="btn_Search" runat="server"
                        Text="查询" OnClick="btnSearch_Click" /><a href="javascript:hideGridview()" class="dropdownico"
                            id="Highsearch">高级搜索</a>
            </td>
        </tr>
        <tr runat="server" id="trOrg2">
            <th width="100">
                组织机构：
            </th>
            <td colspan="3" align="left">
                <asp:DropDownList runat="server" ID="ddl_u999OrganizationID" IsShowAll="true" CssClass="select_390" />
            </td>
        </tr>
    </table>
</div>
<!--查找结果-->
<div class="dv_searchlist">
    <!--翻页-->
    <div class="dv_pagePanel" id="divPage3">
        <div class="dv_selectAll">
            <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" />
            <asp:Button ID="btnAdd" runat="server" Text="确定" CssClass="btn_Ok" OnClick="btnAdd_Click" />
        </div>
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
            <asp:TemplateField HeaderText="">
                <ItemStyle HorizontalAlign="Center"  />
                <HeaderStyle HorizontalAlign="Center" Width="20"/>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="LoginName" HeaderText="学员帐号"  HeaderStyle-CssClass="alignleft field12" ItemStyle-CssClass="alignleft" />
            <asp:BoundField DataField="RealName" HeaderText="姓名"  HeaderStyle-CssClass="alignleft field8" ItemStyle-CssClass="alignleft"/>
            <asp:TemplateField HeaderText="组织机构" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                <ItemTemplate>
                    <cc1:DictionaryLabel ID="lblOrganization" DictionaryType="Dic_CurrentAndSubOrganization"
                        FieldIDValue='<%# Eval("OrganizationID") %>' runat="server" TextLength="6" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                <ItemTemplate>
                    <cc1:DictionaryLabel ID="lblDepartmentID" DictionaryType="vw_Dic_Sys_Department" TextLength="6"
                        FieldIDValue='<%# Eval("DepartmentID") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_position%>"  HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                <ItemTemplate>
                    <cc1:DictionaryLabel ID="lblPost" DictionaryType="vw_Dic_Sys_Post" FieldIDValue='<%# Eval("PostID") %>'
                        runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_rank%>" HeaderStyle-Width="100"  HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                <ItemTemplate>
                    <cc1:DictionaryLabel ID="lblRank" DictionaryType="vw_Dic_Sys_Rank" FieldIDValue='<%# Eval("RankID") %>'
                        runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </cc1:CustomGridView>
    <!--列表 end-->
    <div class="dv_splitLine">
    </div>
    <!--翻页-->
    <div class="dv_pagePanel" id="divPage4">
    </div>
<%--    <script language="javascript" type="text/javascript">
        divPage4.innerHTML = divPage3.innerHTML;
    </script>--%>
</div>
