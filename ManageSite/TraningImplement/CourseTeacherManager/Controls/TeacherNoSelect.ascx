<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TeacherNoSelect.ascx.cs"
    Inherits="TraningImplement_CourseTeacherManager_Controls_TeacherNoSelect" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<!--查找条件-->
<div class="dv_searchbox">
    <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <th style="width:120px">
                姓&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;名：
            </th>
            <th style="width:120px">
                <asp:TextBox ID="txt_RealName" runat="server" CssClass="inputbox_120" MaxLength="50"></asp:TextBox>
            </th>
            <td style="width:200px">
                <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
            </td>
            <td>
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
            <asp:Button ID="btnAdd" runat="server" Text="确定" CssClass="btn_Ok" OnClick="btnAdd_Click" />
        </div>
        <div class="dv_pageControl">
            <uc2:PageSet ID="PageSet1" runat="server" />
        </div>
    </div>
    <!--列表 begin-->
    <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
        CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0"
        DataKeyNames="TeacherID" OnRowDataBound="CustomGridView1_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="">
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle HorizontalAlign="Center" Width="20" />
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="RealName" HeaderText="姓名" HeaderStyle-CssClass="alignleft"
                ItemStyle-CssClass="alignleft" />
            <asp:TemplateField HeaderText="等级" HeaderStyle-Width="60">
                <ItemTemplate>
                    <cc1:DictionaryLabel ID="lblTeacherLevel" DictionaryType="Dic_Sys_TeacherLevel" FieldIDValue='<%# Eval("TeacherLevelID") %>'
                        runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="来源" HeaderStyle-Width="60">
                <ItemTemplate>
                    <cc1:DictionaryLabel ID="lblTeacherSource" DictionaryType="Dic_Sys_TeacherSource"
                        FieldIDValue='<%# Eval("TeacherSourceID") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="所属机构" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                <ItemTemplate>
                    <asp:Label ID="lblOrganization" runat="server" Text='<%# Eval("OrganizationName") %>'
                        ToolTip='<%# Eval("DisplayPath") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="MobilePhone" HeaderText="手机" HeaderStyle-CssClass="field12" />
            <asp:TemplateField HeaderText="邮箱">
                <ItemTemplate>
                    <cc1:ShortTextLabel ID="lblEmail" runat="server" ShowTextNum="50" Text='<%# Eval("Email")%>'></cc1:ShortTextLabel>
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
