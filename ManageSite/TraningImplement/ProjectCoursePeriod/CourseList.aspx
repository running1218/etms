<%@ Page Title="项目课时安排管理" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="CourseList.aspx.cs" Inherits="TraningImplement_ProjectCoursePeriod_CourseList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
                id="tableQueryControlList">
                <tr>
                    <th width="100">
                        项目编码：
                    </th>
                    <td>
                        <asp:TextBox ID="txt_Tr_Item999ItemCode" runat="server" CssClass="inputbox_120" MaxLength="50"></asp:TextBox>
                    </td>
                    <th width="100">
                        项目名称：
                    </th>
                    <td class="Search_Area">
                        <asp:TextBox ID="txt_Tr_Item999ItemName" runat="server" CssClass="inputbox_120 floatleft"
                            MaxLength="100"></asp:TextBox><asp:Button ID="btnSearch" CssClass="btn_Search" runat="server"
                                Text="查询" OnClick="btnSearch_Click" /><a href="javascript:hideGridview()" class="dropdownico"
                                    id="Highsearch">高级搜索</a>
                    </td>
                </tr>
                <tr>
                    <th>
                        课程编码：
                    </th>
                    <td>
                        <asp:TextBox ID="txt_Res_Course999CourseCode" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                    <th>
                        课程名称：
                    </th>
                    <td>
                        <asp:TextBox ID="txt_Res_Course999CourseName" runat="server" CssClass="inputbox_120 floatleft"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        授课方式：
                    </th>
                    <td colspan="3">
                        <asp:DropDownList ID="ddl_Tr_ItemCourse999TeachModelID" runat="server"></asp:DropDownList>
                        <%--<cc1:DictionaryDropDownList runat="server" ID="ddl_Tr_ItemCourse999TeachModelID2" DictionaryType="Dic_Sys_TeachModel"  />--%>
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
                DataKeyNames="TrainingItemCourseID" OnRowDataBound="CustomGridView1_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="项目编码"  ItemStyle-CssClass="alignleft"
                        HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblItemCode" runat="server" ShowTextNum="50" Text='<%# Eval("ItemCode")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="项目名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblItemName" runat="server" ShowTextNum="50" Text='<%# Eval("ItemName")%>'></cc1:ShortTextLabel>
                            <asp:HiddenField ID="Hf_ItemStatus" runat="server" Value='<%# Eval("ItemStatus") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程编码"  HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblCourseCode" runat="server" ShowTextNum="50" Text='<%# Eval("CourseCode")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程名称" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="50" Text='<%# Eval("CourseName")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程状态" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <%# Eval("CourseStatus").ToBoolean() ? "启用" : "停用"%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="授课方式" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="DictionaryLabel1" DictionaryType="Dic_Sys_TeachModel" FieldIDValue='<%# Eval("TeachModelID") %>'
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="讲师数" HeaderStyle-Width="60" ItemStyle-CssClass="alignright" HeaderStyle-CssClass="alignright">
                        <ItemTemplate>
                            <asp:Label ID="lblTeacher" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="学员数" HeaderStyle-Width="60" ItemStyle-CssClass="alignright" HeaderStyle-CssClass="alignright">
                        <ItemTemplate>
                            <asp:Label ID="lblSelectCourse" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="已设课时安排数" HeaderStyle-Width="100" ItemStyle-CssClass="alignright" HeaderStyle-CssClass="alignright">
                        <ItemTemplate>
                            <asp:Label ID="lblPeriod" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作" HeaderStyle-Width="115">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnPeriod" runat="server" Text="课时管理" CommandName="Period"></asp:LinkButton><asp:LinkButton
                                ID="lbtnView" runat="server" Text="查看" CommandName="View"></asp:LinkButton>
                            <asp:LinkButton ID="lbnOnlinePlaying" runat="server" Text="直播管理" CommandName="OnlinePlaying" CssClass="hide"></asp:LinkButton>
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
        说明：项目课时安排管理仅针对面授课程，不适用于在线课程。
        </div>
    </div>
</asp:Content>
