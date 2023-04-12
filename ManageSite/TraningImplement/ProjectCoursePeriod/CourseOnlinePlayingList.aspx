<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="CourseOnlinePlayingList.aspx.cs" Inherits="TraningImplement_ProjectCoursePeriod_CourseOnlinePlayingList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" runat="Server">
    <asp:LinkButton ID="lbtnReturn" runat="server" Text="返回" CssClass="btn_Return"></asp:LinkButton>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 
    <!--表单录入-->
    <div class="dv_GradeviewList">
        <table>
            <tr>
                <th width="100">
                    项目名称：
                </th>
                <td>
                    <asp:Label ID="lblItemName" runat="server"></asp:Label>
                </td>
                <th>
                    课程名称：
                </th>
                <td>
                    <asp:Label ID="lblCourseName" runat="server"></asp:Label>
                </td>
            </tr>           
        </table>
        <div style="display: none">
            <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" /></div>
    </div>
    <!--查找结果-->
    <div class="dv_searchlist">
        <!--翻页-->
        <div class="dv_pagePanel" id="divPage1">
            <div class="dv_selectAll">
                <asp:Button ID="btnAdd" runat="server" CssClass="btn_Add" Text="新增" />
            </div>
            <div class="dv_pageControl">
                <uc2:PageSet ID="PageSet1" runat="server" />
            </div>
        </div>
        <!--列表 begin-->
        <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
            CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="1"
            DataKeyNames="OnlinePlayingID" OnRowDataBound="CustomGridView1_RowDataBound"
            OnRowCommand="CustomGridView1_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="直播主题" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <asp:Label ID="lblOnlineSubject" runat="server" Text='<%# Eval("PlayingSubject")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="直播日期" HeaderStyle-Width="100" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <asp:Label ID="lblTrainingDate" runat="server" Text='<%# Eval("StartTime").ToDate()%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="直播时段"  HeaderStyle-Width="100">
                    <ItemTemplate>
                        <asp:Label ID="lblTrainingBeginTime" runat="server" Text='<%#  Eval("StartTime").ToDateTime().ToString("HH:mm")%>'></asp:Label>至<asp:Label
                            ID="lblTrainingEndTime" runat="server" Text='<%#  Eval("EndTime").ToDateTime().ToString("HH:mm")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="讲师" HeaderStyle-Width="80" >
                    <ItemTemplate>
                        <%# Eval("TeacherName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="状态" HeaderStyle-Width="50" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <asp:Label ID="lblOnlineStatus" runat="server" Text='<%# Eval("OnlineStatus").ToBoolean() == true ? "未结束":"已结束"%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>                   
                <asp:TemplateField HeaderText="操作" HeaderStyle-Width="120">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnEdit" runat="server" Text="编辑" CommandName="Edit"></asp:LinkButton><cc1:CustomLinkButton
                            runat="server" ID="clbtnDel" CommandName="delonlineplaying" CommandArgument='<%# Eval("OnlinePlayingID") %>'
                            Text="删除" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="确定删除吗？" /><asp:LinkButton
                                ID="lbtnView" runat="server" Text="查看" CommandName="View"></asp:LinkButton>
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
</asp:Content>
