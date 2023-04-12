<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="OffLineHomeWork.aspx.cs" Inherits="TraningImplement_ProjectCourseResourceQuery_OffLineHomeWork" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" runat="Server">
    <a class="btn_Return" runat="server" id="aBack">返回</a>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <th width="120">
                        项目名称：
                    </th>
                    <td width="120">
                        <asp:Label ID="lblItemName" runat="server"></asp:Label>
                    </td>
                    <th width="100">
                        课程名称：
                    </th>
                    <td>
                        <asp:Label ID="lblCourseName" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <div style="display: none">
                <asp:UpdatePanel ID="upQuery" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" /></ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
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
                        CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0"
                        DataKeyNames="" onrowdatabound="CustomGridView1_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                                <ItemTemplate>
                                    <cc1:ShortTextLabel ID="lblJobName" runat="server" ShowTextNum="10" Text='<%# Eval("JobName")%>'></cc1:ShortTextLabel>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="附件" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lblJobFileName" runat="server" Text='<%#Eval("JobFileName")%>' NavigateUrl='<%#Eval("JobFileURL")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="状态" HeaderStyle-Width="40">
                                <ItemTemplate>
                                    <%# Eval("IsUse").ToBoolean() ? "启用" : "停用"%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="有效期" HeaderStyle-Width="160">
                                <ItemTemplate>
                                    <%# Eval("BeginTime").ToDate() +" 至 "+ Eval("EndTime").ToDate()%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="创建人" HeaderStyle-Width="70">
                                <ItemTemplate>
                                   <cc1:DictionaryLabel ID="dlblCreateUserID" runat="server" DictionaryType="" FieldIDValue='<%# Eval("CreateUserID")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="创建时间" HeaderStyle-Width="80">
                                <ItemTemplate>
                                    <%# Eval("CreateTime").ToDate()%>
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
