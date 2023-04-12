<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="TeacherList.aspx.cs" Inherits="ETMS.WebApp.Manage.CourseTeacherList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="cphNav" runat="server" ContentPlaceHolderID="cphBack">
    <a class="btn_Return" href="CourseList.aspx">返回</a>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--项目信息-->
        <div class="dv_searchbox">
            <table class="GridviewGray fixedTable" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <th width="100px">
                        课程编码：
                    </th>
                    <td width="200px">
                        <asp:Literal ID="ltlProjectCode" runat="server"></asp:Literal>
                    </td>
                    <th width="100px">
                        课程名称：
                    </th>
                    <td>
                        <cc1:ShortTextLabel ID="lblProjectName" runat="server" ShowTextNum="30"></cc1:ShortTextLabel>
                        <span style="display: none;">
                            <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" /></span>
                    </td>
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel">
                <div class="dv_selectAll">
                    <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="gvList" />
                    <cc1:CustomButton ID="btnAdd" runat="server" Text="添加" CssClass="btn_Add" EnableConfirm="false">
                    </cc1:CustomButton>
                    <cc1:CustomButton ID="btnDelete" runat="server" Text="删除" CssClass="btn_Del" EnableConfirm="true"
                        ConfirmTitle="提示" ConfirmMessage="确定删除吗？" OnClick="btnDelete_Click"></cc1:CustomButton>
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" PageSize="10" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="gvList" runat="server" AutoGenerateColumns="false" CustomAllowPaging="true"
                ShowFooter="false" AutoCreateColumnInsertIndex="0" DataKeyNames="TeacherID">
                <Columns>
                    <asp:TemplateField HeaderText="">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" Width="20" />
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="讲师姓名" HeaderStyle-CssClass="alignleft field8" ItemStyle-CssClass="alignleft"
                        >
                        <ItemTemplate>
                            <asp:Literal ID="ltlTeacherName" runat="server" Text='<%# Eval("RealName") %>'></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="讲师等级" HeaderStyle-Width="100">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblLevel" runat="server" DictionaryType="Dic_Sys_TeacherLevel"
                                FieldIDValue='<%# Eval("TeacherLevelID") %>'></cc1:DictionaryLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="讲师来源" HeaderStyle-Width="100">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblTeacherSource" runat="server" DictionaryType="Dic_Sys_TeacherSource"
                                FieldIDValue='<%# Eval("TeacherSourceID") %>'></cc1:DictionaryLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="讲师分类" HeaderStyle-Width="100">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblType" runat="server" DictionaryType="Dic_Sys_TeacherType"
                                FieldIDValue='<%# Eval("TeacherTypeID") %>'></cc1:DictionaryLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft" Visible="false">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="ltlDepartMent" runat="server" DictionaryType="Site_DepartmentByOrgID"
                                FieldIDValue='<%# Eval("DepartmentID") %>'></cc1:DictionaryLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </cc1:CustomGridView>
            <!--列表 end-->
            <div class="dv_splitLine">
            </div>
            <!--翻页-->
            <div class="dv_pagePanel">
            </div>
        </div>
    </div>
</asp:Content>
