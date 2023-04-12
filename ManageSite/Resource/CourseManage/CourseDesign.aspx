<%@ Page Title="课程设计" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="CourseDesign.aspx.cs" Inherits="Resource_CourseManage_CourseDesign" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="cphNav" runat="server" ContentPlaceHolderID="cphBack">
    <asp:LinkButton ID="linRetun" runat="server" Text="返回"  CssClass="btn_Return"></asp:LinkButton>
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
            <asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <!--翻页-->
                    <div class="dv_pagePanel">
                        <div class="dv_selectAll">
                            <input type="button" class="btn_Add" value="新增" onclick="javascript: showWindow('新增', '<%= AddUrl %>',480,340)" />
                            <input type="button" class="btn_AddLong" value="批量导入" onclick="javascript: showWindow('批量导入', '<%= AddBatchUrl %>',600,380)" />
                            <input id="btnSort" type="button" class="btn_Sort" value="排序" title="课程资源排序" onclick="javascript:showWindow('课程资源排序','<%= SortUrl %>',500,400)" />
                        </div>
                        <div class="dv_pageControl">
                            <uc2:PageSet ID="PageSet1" runat="server" />
                        </div>
                    </div>
                    <!--列表 begin-->
                    <cc1:CustomGridView ID="gvList" runat="server" CssClass="GridviewGray" EmptyDataSourceTip="没有任何记录！"
                        AutoCreateColumnInsertIndex="0" AutoGenerateColumns="False" CurrentPageIndex="1"
                        CustomAllowPaging="False" IsEmpty="False" TotalRecordCount="0" DataKeyNames="ContentID" OnRowCommand="CustomGridView1_RowCommand">
                        <Columns>
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
                                    <a href="javascript:showWindow('编辑课程','<%# this.ActionHref(string.Format("VideoManage.aspx?action=edit&CourseWareID={0}&ContentID={1}",CourseWareID,Eval("ContentID").ToString())) %>',480,340)">编辑</a>
                                    <cc1:CustomLinkButton runat="server" ID="lbtn_Del" CommandArgument='<%# Eval("ContentID") %>'
                                        CommandName="Del" Text="删除" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="确定要删除吗？" />
                                    <%# GetViewLink(Eval("ContentID").ToGuid(),Eval("PlayTime").ToInt(),Eval("Type").ToInt()) %>
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <script>
<%--        function preview()
        {  
            var StudyingUrl='<%=StudyingUrl%>';
             //var StudyingUrl='http://10.96.142.126:9001/Admin';
            var CourseID='<%=CourseID%>';
            if(StudyingUrl.indexOf("Admin")>0)
            {
                StudyingUrl=StudyingUrl.replace("Admin","Studying");
            }
            window.open(StudyingUrl+'/Public/CourseView.aspx?courseid='+CourseID);       
        }--%>
    </script>
</asp:Content>
