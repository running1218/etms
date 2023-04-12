<%@ Page Title="课程设计" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="CourseResourceOpen.aspx.cs" Inherits="Resource_CourseResourceOpen" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="cphNav" runat="server" ContentPlaceHolderID="cphBack">
    <a class="btn_Return" href="../../SiteManage/RecommendCourse/RecommendCourseList.aspx">返回</a>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
                id="tableQueryControlList">
                <tr>
                    <th width="120">课程名称：
                    </th>
                    <td width="130">
                        <asp:Label ID="lblCourseName" runat="server" Text=""></asp:Label>
                    </td>
                    <th width="120"></th>
                    <td class="Search_Area">
                        <%--<asp:Button ID="btnPreview" CssClass="btn_Search" OnClick="btnPreview_Click" runat="server" Text="预览" />--%>
                        <%--<input  type="button" class="btn_Search" onclick="preview()" value="预览"/>--%>
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
                    <asp:Button ID="btnSetting" runat="server" Text="开放" CssClass="btn_Choosing" OnClick="btnSetting_Click" />
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" PageSize="10" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="gvList" runat="server" CssClass="GridviewGray" EmptyDataSourceTip="没有任何记录！"
                AutoCreateColumnInsertIndex="0" AutoGenerateColumns="False" CurrentPageIndex="1" IsRemeberChecks="true"
                CustomAllowPaging="False" IsEmpty="False" TotalRecordCount="0" DataKeyNames="ContentID" OnRowCommand="CustomGridView1_RowCommand" OnDataBound="gvList_DataBound" OnRowDataBound="gvList_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" Width="20" />
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="资源名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="100" Text='<%# Eval("Name")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="讲课老师" HeaderStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="lblTeacherName" runat="server" Text='<%# Eval("TeacherName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="时长" HeaderStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="lblVideoTime" runat="server" Text='<%# Eval("ContentTime") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="状态" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status").ToString()=="1"?"启用":"停用" %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="是否开放" HeaderStyle-Width="60" HeaderStyle-CssClass="hide" ItemStyle-CssClass="hide">
                        <ItemTemplate>
                            <asp:Label ID="lblOpen" runat="server" Text='<%# Eval("IsOpen").ToBoolean()==true?"是":"否" %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作" HeaderStyle-Width="200px">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%# GetViewLink(Eval("ContentID").ToGuid(),Eval("PlayTime").ToInt(),Eval("Type").ToInt()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </cc1:CustomGridView>
            <!--列表 end-->
            <div class="dv_splitLine">
            </div>
            <!--翻页-->
            <div class="dv_pagePanel hide">
            </div>
        </div>
    </div>
</asp:Content>
