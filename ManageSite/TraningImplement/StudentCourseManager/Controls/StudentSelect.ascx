<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StudentSelect.ascx.cs"
    Inherits="TraningImplement_StudentCourseManager_Controls_StudentSelect" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<!--查找条件-->
<div class="dv_searchbox">
    <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
        id="tableQueryControlList">
        <tr>
            <th width="100" style="display:none;">
                工号：
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
        <tr runat="server" id="trOrg1">
            <th width="100">
                组织机构：
            </th>
            <td colspan="3" align="left">
                <asp:DropDownList runat="server" ID="ddl_u999OrganizationID" IsShowAll="true" CssClass="select_390" />
            </td>
        </tr>
        <tr>
            <th>
                报名方式：
            </th>
            <td colspan="3" align="left">
                <cc1:DictionaryDropDownList runat="server" ID="dddl_b999SignupModeID" DictionaryType="Dic_Sys_SignupMode"
                    IsShowAll="true" CssClass="select_190" />
            </td>
        </tr>
        <tr>
            <th>
                报名时间：
            </th>
            <td colspan="3" align="left">
                <cc1:DateTimeTextBox ID="begin_b999CreateTime" runat="server" EndTimeControlID="end_b999CreateTime"></cc1:DateTimeTextBox>
                至
                <cc1:DateTimeTextBox ID="end_b999CreateTime" runat="server" BeginTimeControlID="begin_b999CreateTime"></cc1:DateTimeTextBox>
            </td>
        </tr>
    </table>
</div>
<!--查找结果-->
<div class="dv_searchlist">
    <!--翻页-->
    <div class="dv_pagePanel" id="divPage1">
        <div class="dv_selectAll">
            <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" />
            <cc1:CustomButton runat="server" ID="lbtnDel" CommandName="del" CssClass="btn_Del"
                Text="删除" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="确定删除吗？" OnClick="btnDel_Click" />
        </div>
        <div class="dv_pageControl">
            <uc2:PageSet ID="PageSet1" runat="server" />
        </div>
    </div>
    <!--列表 begin-->
    <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
        CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0"
        DataKeyNames="StudentCourseID" 
        OnRowDataBound="CustomGridView1_RowDataBound" 
        onrowcommand="CustomGridView1_RowCommand">
        <Columns>
            <asp:TemplateField HeaderText="">
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle HorizontalAlign="Center" Width="20" />
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="LoginName" HeaderText="学员帐号" HeaderStyle-CssClass="alignleft filed8"
                ItemStyle-CssClass="alignleft filed8" />
            <asp:BoundField DataField="RealName" HeaderText="姓名" HeaderStyle-Width="60" HeaderStyle-CssClass="alignleft"
                ItemStyle-CssClass="alignleft" />
            <asp:TemplateField HeaderText="组织机构" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                <ItemTemplate>
                    <cc1:DictionaryLabel ID="lblOrganization" DictionaryType="Dic_CurrentAndSubOrganization"
                        FieldIDValue='<%# Eval("OrganizationID") %>' runat="server" TextLength="6" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                <ItemTemplate>
                    <cc1:DictionaryLabel ID="lblDepartmentID" DictionaryType="vw_Dic_Sys_Department"
                        TextLength="6" FieldIDValue='<%# Eval("DepartmentID") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_position%>" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                <ItemTemplate>
                    <cc1:DictionaryLabel ID="lblPost" DictionaryType="vw_Dic_Sys_Post" FieldIDValue='<%# Eval("PostID") %>'
                        TextLength="6" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_rank%>" HeaderStyle-Width="90" HeaderStyle-CssClass="alignleft"
                ItemStyle-CssClass="alignleft">
                <ItemTemplate>
                    <cc1:DictionaryLabel ID="lblRank" DictionaryType="vw_Dic_Sys_Rank" FieldIDValue='<%# Eval("RankID") %>'
                        TextLength="6" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="报名时间" HeaderStyle-Width="80" HeaderStyle-CssClass="alignleft"
                ItemStyle-CssClass="alignleft">
                <ItemTemplate>
                    <%# Eval("CreateTime").ToDate()%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="报名方式" HeaderStyle-Width="80" HeaderStyle-CssClass="alignleft"
                ItemStyle-CssClass="alignleft">
                <ItemTemplate>
                    <cc1:DictionaryLabel ID="lblSignupMode" DictionaryType="Dic_Sys_SignupMode" FieldIDValue='<%# Eval("SignupModeID") %>'
                        TextLength="6" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="学员课程状态" HeaderStyle-Width="100">
                <ItemTemplate>
                    <cc1:DictionaryLabel ID="dlblIsUseStudentCourse" DictionaryType="Dic_Status" FieldIDValue='<%# Eval("IsUseStudentCourse") %>'
                        runat="server" />
                    <asp:HiddenField ID="hfCourseStatus" runat="server" Value='<%# Eval("IsUseStudentCourse") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作" HeaderStyle-Width="60">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtn_Open" runat="server" CommandName="Open" CommandArgument='<%# Eval("StudentCourseID")%>'>启用</asp:LinkButton><asp:LinkButton
                        ID="lbtn_Close" runat="server" CommandName="Close" CommandArgument='<%# Eval("StudentCourseID")%>'
                        Visible="false">停用</asp:LinkButton>
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
