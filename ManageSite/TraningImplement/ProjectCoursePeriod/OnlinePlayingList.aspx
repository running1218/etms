<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="OnlinePlayingList.aspx.cs" Inherits="TraningImplement_ProjectCoursePeriod_OnlinePlayingList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" runat="Server">
  
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 
    <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
                id="tableQueryControlList">
                <tr>
                    <th width="100">
                        项目名称：
                    </th>
                    <td>
                        <asp:TextBox ID="txtItemName" runat="server" CssClass="inputbox_210" MaxLength="50"></asp:TextBox>
                    </td>
                    <th width="100">
                        课程名称：
                    </th>
                    <td class="Search_Area">
                        <asp:TextBox ID="txtCourseName" runat="server" CssClass="inputbox_160 floatleft"
                            MaxLength="100"></asp:TextBox><asp:Button ID="btnSearch" CssClass="btn_Search" runat="server"
                                Text="查询" OnClick="btnSearch_Click" /><a href="javascript:hideGridview()" class="dropdownico"
                                    id="Highsearch">高级搜索</a>
                    </td>
                </tr>
                <tr>
                    <th>
                        直播主题：
                    </th>
                    <td>
                        <asp:TextBox ID="txtPlayingSubject" runat="server" CssClass="inputbox_210" MaxLength="100"></asp:TextBox>
                    </td>
                    <th>
                        直播状态：
                    </th>
                    <td>
                        <asp:DropDownList ID="rblOnlineStatus" runat="server" CssClass="select_100">                            
                            <asp:ListItem Value="1" Text="未结束" Selected="True">未结束</asp:ListItem>
                            <asp:ListItem Value="0" Text="已结束">已结束</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>
                        直播时间：
                    </th>
                    <td colspan="3">
                        <cc1:DateTimeTextBox ID="txtStartTime" runat="server" EndTimeControlID="txtEndTime"></cc1:DateTimeTextBox>
                        至
                        <cc1:DateTimeTextBox ID="txtEndTime" runat="server" BeginTimeControlID="txtStartTime"></cc1:DateTimeTextBox>
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
            CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="1"
            DataKeyNames="OnlinePlayingID" OnRowDataBound="CustomGridView1_RowDataBound"
            OnRowCommand="CustomGridView1_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="项目名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="课程名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("CourseName")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
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
                <asp:TemplateField HeaderText="讲师" HeaderStyle-Width="60" >
                    <ItemTemplate>
                        <%# Eval("TeacherName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="状态" HeaderStyle-Width="60" >
                    <ItemTemplate>
                        <asp:Label ID="lblOnlineStatus" runat="server" Text='<%# Eval("OnlineStatus").ToBoolean() == true ? "未结束":"已结束" %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>                    
                <asp:TemplateField HeaderText="操作" HeaderStyle-Width="120">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Literal ID="lbnEntery" runat="server" Text="开始直播"></asp:Literal>
                        <cc1:CustomLinkButton runat="server" ID="lbnEnd" CommandName="end" CommandArgument='<%# Eval("OnlinePlayingID") %>'
                            Text="结束直播" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="确定结束吗？" />                        
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </cc1:CustomGridView>
        <!--列表 end-->
        <div class="dv_splitLine">
        </div>
        <!--翻页-->
        <div class="dv_pagePanel" id="divPage2"><a target="_blank" href
        </div>
    </div>              
</asp:Content>
