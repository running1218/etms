<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="OnlinePlayingView.aspx.cs" Inherits="TraningImplement_ProjectCoursePeriod_OnlinePlayingView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="dv_information">
    <table class="GridviewGray GridveiwFixed">   
        <tr>
            <th>
                直播主题：
            </th>
            <td colspan="3">
               <asp:Label ID="lblSubject" runat="server"></asp:Label>
            </td>
        </tr>   
        <tr>
            <th>
                培训日期：
            </th>
            <td colspan="3">
                <asp:Label ID="lblTrainingDate" runat="server"></asp:Label>
            </td>            
        </tr>
        <tr>
            <th>
                培训时段：
            </th>
            <td colspan="3">
                <asp:Label ID="lblTrainingBeginTime" runat="server"></asp:Label>
                至
                <asp:Label ID="lblTrainingEndTime" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>
                讲&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;师：
            </th>
            <td colspan="3">
                <asp:Label ID="lblTeacher" runat="server"></asp:Label>
            </td>
        </tr>      
        <tr>
            <th>
                直播介绍：
            </th>
            <td colspan="3">
                <asp:Label ID="lblOnlinePlayingDesc" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
</div>
</asp:Content>

