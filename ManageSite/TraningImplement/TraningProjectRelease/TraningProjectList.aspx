<%@ Page Title="培训项目发布" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="TraningProjectList.aspx.cs" Inherits="TraningImplement_TraningProjectRelease_TraningProjectList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
                id="tableQueryControlList">
                <tr>
                    <th width="120">
                        项目编码：
                    </th>
                    <td width="120">
                        <asp:TextBox ID="txt_ItemCode" runat="server" CssClass="inputbox_120" MaxLength="50"></asp:TextBox>
                    </td>
                    <th width="100">
                        项目名称：
                    </th>
                    <td class="Search_Area">
                        <asp:TextBox ID="txt_ItemName" runat="server" CssClass="inputbox_120 floatleft" MaxLength="100"></asp:TextBox>
                        <asp:UpdatePanel ID="upQuery" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        专业类别：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="ddl_SpecialtyTypeCode" DictionaryType="Dic_Sys_SpecialtyType"
                            IsShowAll="true" />
                    </td>
                    <th>
                        发布状态：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="ddl_IsIssue" DictionaryType="Dic_TraningProjectReleaseState"
                            IsShowAll="true" />
                    </td>
                </tr>
                <tr>
                    <th>
                        创建时间：
                    </th>
                    <td colspan="3">
                        <cc1:DateTimeTextBox ID="begin_CreateTime" runat="server" EndTimeControlID="end_CreateTime"></cc1:DateTimeTextBox>
                        至
                        <cc1:DateTimeTextBox ID="end_CreateTime" runat="server" BeginTimeControlID="begin_CreateTime"></cc1:DateTimeTextBox>
                    </td>
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <!--翻页-->
                    <div class="dv_pagePanel" id="divPage1">
                        <div class="dv_pageControl">
                            <uc2:PageSet ID="PageSet1" runat="server" />
                        </div>
                    </div>
                    <!--列表 begin-->
                    <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
                        CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0"
                        DataKeyNames="TrainingItemID" OnRowDataBound="CustomGridView1_RowDataBound" OnRowCommand="CustomGridView1_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="项目编码" HeaderStyle-Width="110" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                                <ItemTemplate>
                                    <cc1:ShortTextLabel ID="lblItemCode" runat="server" ShowTextNum="50" Text='<%# Eval("ItemCode")%>'></cc1:ShortTextLabel>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="项目名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                                <ItemTemplate>
                                    <cc1:ShortTextLabel ID="lblItemName" runat="server" ShowTextNum="50" Text='<%# Eval("ItemName")%>'></cc1:ShortTextLabel>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="项目开始时间" HeaderStyle-Width="80">
                                <ItemTemplate>
                                    <%# Eval("ItemBeginTime").ToDate()%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="项目结束时间" HeaderStyle-Width="80">
                                <ItemTemplate>
                                    <%# Eval("ItemEndTime").ToDate() %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="课程数" HeaderStyle-Width="60">
                                <ItemTemplate>
                                    <%#  new ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Tr_ItemCourseLogic().GetItemCourseCountByTrainingItemID(Eval("TrainingItemID").ToGuid())%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="学员数" HeaderStyle-Width="60">
                                <ItemTemplate>
                                    <asp:Label ID="lblStudentTotal" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="报名方式" HeaderStyle-Width="100" HeaderStyle-CssClass="hide" ItemStyle-CssClass="hide">
                                <ItemTemplate><cc1:DictionaryLabel ID="dlabSignupMode" DictionaryType="Dic_Sys_SignupMode" FieldIDValue='<%# Eval("SignupModeID") %>'
                runat="server" />
                                    <asp:HiddenField ID="hfSignupMode" runat="server" Value='<%# Eval("SignupModeID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="发布状态" HeaderStyle-Width="60">
                                <ItemTemplate>
                                    <cc1:DictionaryLabel ID="DictionaryReleaseState" DictionaryType="Dic_TraningProjectReleaseStateBool"
                                        FieldIDValue='<%# Eval("IsIssue") %>' runat="server" />
                                    <asp:HiddenField ID="hfIsIssue" runat="server" Value='<%# Eval("IsIssue") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="操作" HeaderStyle-Width="60">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnRelease" runat="server" Text="发布" CommandName="Release" CommandArgument='<%# Eval("TrainingItemID") %>'></asp:LinkButton></ItemTemplate>
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
