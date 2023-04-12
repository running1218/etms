<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"  AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="Test" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:FileUpload ID="fuFile" runat="server" />
    <asp:Button ID="btnUnRar" runat="server" Text="UnRar" />
    <div class="dv_searchlist">
        <!--翻页-->
        <div class="dv_pagePanel">
            <div class="dv_selectAll">
                <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="gvList" />
                <asp:LinkButton ID="btnCheck" runat="server" Text="Check"></asp:LinkButton>
            </div>
            <div class="dv_pageControl">
                <uc2:PageSet ID="PageSet1" runat="server" />
            </div>
        </div>
        <!--列表 begin-->
        <cc1:CustomGridView ID="gvList" runat="server" CssClass="GridviewGray" EmptyDataSourceTip="没有任何记录！"
            AutoCreateColumnInsertIndex="0" AutoGenerateColumns="False" CurrentPageIndex="1"
            CustomAllowPaging="False" IsEmpty="False" TotalRecordCount="0" DataKeyNames="CourseID"
            OnRowCommand="CustomGridView1_RowCommand">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CourseCode" HeaderText="课程编码" ItemStyle-CssClass="alignleft"
                    HeaderStyle-CssClass="alignleft field12"  />
                <asp:TemplateField HeaderText="课程名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="100" Text='<%# Eval("CourseName")%>'></cc1:ShortTextLabel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="课程状态" HeaderStyle-Width="60">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("CourseStatus").ToString()=="1"?"启用":"停用" %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CourseTypeName" HeaderText="课程类型" HeaderStyle-Width="60" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft"/>
                <asp:TemplateField HeaderText="操作" HeaderStyle-Width="160px">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <a href="javascript:showWindow('编辑课程','<%# this.ActionHref(string.Format("CourseEdit.aspx?CourseID={0}",Eval("CourseID").ToString())) %>')">
                            编辑</a>
                        <cc1:CustomLinkButton runat="server" ID="lbtn_Del" CommandArgument='<%# Eval("CourseID") %>'
                            CommandName="Del" Text="删除" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="确定要删除吗？" />
                        <a href='<%# this.ActionHref(string.Format("TeacherList.aspx?CourseID={0}",Eval("CourseID").ToString())) %>'>
                            讲师(<%# GetCourseTeacherNum(Eval("CourseID").ToGuid())%>)</a> <a href='<%# this.ActionHref(string.Format("CourseView.aspx?CourseID={0}",Eval("CourseID").ToString())) %>'>
                                查看</a>
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
</asp:Content>

