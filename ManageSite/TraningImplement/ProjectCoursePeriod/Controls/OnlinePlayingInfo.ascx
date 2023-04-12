<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OnlinePlayingInfo.ascx.cs"
    Inherits="TraningImplement_ProjectCoursePeriod_Controls_OnlinePlayingInfo" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<!--表单录入-->
<div class="dv_information">
    <table class="GridviewGray GridveiwFixed">   
        <tr>
            <th>
                直播主题：
            </th>
            <td colspan="3">
               <asp:TextBox ID="txtSubject" runat="server" CssClass="inputbox_300" MaxLength= "100"></asp:TextBox>
                <font color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                    Text="*" Display="None" runat="server" ErrorMessage="请输入直播主题！"
                    ControlToValidate="txtSubject" ValidationGroup="Saves"></asp:RequiredFieldValidator>
            </td>
        </tr>   
        <tr>
            <th>
                培训日期：
            </th>
            <td colspan="3">
                <cc1:DateTimeTextBox ID="dtxtTrainingDate" runat="server"></cc1:DateTimeTextBox><font
                    color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidator3" Text="*"
                        Display="None" runat="server" ErrorMessage="请输入直播日期！" ControlToValidate="dtxtTrainingDate"
                        ValidationGroup="Saves"></asp:RequiredFieldValidator>
            </td>            
        </tr>
        <tr>
            <th>
                培训时段：
            </th>
            <td colspan="3">
                <cc1:DateTimeTextBox ID="dtxtTrainingBeginTime" runat="server" Width="50px"
                    DateTimeFormat="%h:%m" MaxLength="5" ></cc1:DateTimeTextBox><font color="red">*</font><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator1" Text="*" Display="None" runat="server" ErrorMessage="请输入直播开始时间！"
                        ControlToValidate="dtxtTrainingBeginTime" ValidationGroup="Saves"></asp:RequiredFieldValidator>
                至
                <cc1:DateTimeTextBox ID="dtxtTrainingEndTime" runat="server" Width="60px"
                    DateTimeFormat="%h:%m" MaxLength="5"  ></cc1:DateTimeTextBox><font color="red">*</font><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator2" Text="*" Display="None" runat="server" ErrorMessage="请输入直播结束时间！"
                        ControlToValidate="dtxtTrainingEndTime" ValidationGroup="Saves"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                讲&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;师：
            </th>
            <td colspan="3">
                <asp:DropDownList ID="ddlTeacher" runat="server">
                </asp:DropDownList>
                <font color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                    ValidationExpression="\d[0,*]" Text="*" Display="None" runat="server" ErrorMessage="请选择讲师！"
                    ControlToValidate="ddlTeacher" ValidationGroup="Saves"></asp:RequiredFieldValidator>
            </td>
        </tr>      
        <tr>
            <th>
                直播介绍：
            </th>
            <td colspan="3">
                <asp:TextBox ID="txtOnlinePlayingDesc" TextMode="MultiLine" runat="server" CssClass="inputbox_area300"></asp:TextBox>
            </td>
        </tr>
    </table>
</div>
<div class="dv_submit">
    <asp:Button ID="Btn_Save" CssClass="btn_Save" runat="server" Text="保存" OnClick="Btn_Save_Click"
        ValidationGroup="Saves" />
    <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Saves"
        ShowMessageBox="true" ShowSummary="false" />
    <input type="button" class="btn_Cancel" value="取消" onclick="javascript:closeWindow();" /></div>
