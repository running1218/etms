<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="LivingList.aspx.cs" Inherits="Living_LivingList" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" Runat="Server">
    <a class="btn_Return" href="CourseList.aspx">返回</a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
                id="tableQueryControlList">
                <tr>
                    <th width="120">课程名称：
                    </th>
                    <td>
                        <asp:Label ID="lblCourseName" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel">
                <div class="dv_selectAll">
                    <input type="button" class="btn_Add" value="新增" onclick="<%=string.Format("javascript: showWindow('新增', '{0}', 480, 360)", this.ActionHref(string.Format("LivingAdd.aspx?CourseID={0}", CourseID))) %>" />                
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="gvList" runat="server" CssClass="GridviewGray" EmptyDataSourceTip="没有任何记录！"
                AutoCreateColumnInsertIndex="0" AutoGenerateColumns="False" CurrentPageIndex="1"
                CustomAllowPaging="False" IsEmpty="False" TotalRecordCount="0" DataKeyNames="LivingID" OnRowCommand="CustomGridView1_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="直播名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="ShortTextLabel1" runat="server" ShowTextNum="100" Text='<%# Eval("LivingName")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="开始时间" HeaderStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="lblTeacherName" runat="server" Text='<%# Eval("StartTime").ToDateTime().ToString("yyyy-MM-dd HH:mm:ss") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="结束时间" HeaderStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="lblVideoTime" runat="server" Text='<%# Eval("EndTime").ToDateTime().ToString("yyyy-MM-dd HH:mm:ss") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="直播讲师" HeaderStyle-Width="100">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("TeacherName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                    
                    <asp:TemplateField HeaderText="操作" HeaderStyle-Width="200px">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <a href="javascript:showWindow('编辑','<%# this.ActionHref(string.Format("LivingEdit.aspx?LivingID={0}",Eval("LivingID"))) %>',480,360)">编辑</a>
                            <cc1:CustomLinkButton runat="server" ID="lbtn_Del" CommandArgument='<%# Eval("LivingID") %>'
                                CommandName="Del" Text="删除" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="确定要删除吗？" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </cc1:CustomGridView>
            <!--列表 end-->
            <div class="dv_splitLine"></div>
            <!--翻页-->
            <div class="dv_pagePanel"></div>
        </div>
    </div>
</asp:Content>

