<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="PointReasonDetailListAdd.aspx.cs" Inherits="Point_PointReasonDetailListAdd" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div class="dv_information">
        <table class="GridviewGray">
            <tr>
               <th>培训项目：</th>
               <td colspan="3">
                  <cc1:ShortTextLabel ID="lblItemName" runat="server" ShowTextNum="20" />
               </td>
            </tr>
            <tr>
               <th>班&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;级：</th>
               <td>
                  <cc1:ShortTextLabel ID="lblClassName" runat="server" ShowTextNum="10" />
               </td>
                <th>学习群组：</th>
               <td>
                  <cc1:ShortTextLabel ID="lblGroupName" runat="server"  ShowTextNum="10"/>
               </td>
            </tr>             
            <tr>
               <th>选择学生数：</th>
               <td colspan="3">
                  <cc1:ShortTextLabel ID="lblStudentNum" runat="server"  ShowTextNum="20"/>
               </td>               
            </tr>
            <tr>
                <th>
                    积分原因类型：
                </th>
                <td colspan="3">
                    <asp:DropDownList ID="ddlPointReasonTypeID" runat="server"  AutoPostBack="true"  OnSelectedIndexChanged="ddlPointReasonTypeID_SelectedIndexChanged"/>    
                    <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                        runat="server" ErrorMessage="请选择积分原因类型！" ControlToValidate="ddlPointReasonTypeID" ValidationGroup="Edit" Display="None"></asp:RequiredFieldValidator>               
                </td>
            </tr>
           <tr>
                <th>
                    积分原因：
                </th>
                <td colspan="3">
                    <asp:DropDownList ID="ddlPointReason" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="ddlPointReason_SelectedIndexChanged"/>
                    <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidatorPointReason"
                        runat="server" ErrorMessage="请选择积分原因！" ControlToValidate="ddlPointReason" ValidationGroup="Edit" Display="None"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>
                    积&nbsp;&nbsp;分&nbsp;&nbsp;值：
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtGivePoints" runat="server" CssClass="inputbox_60" MaxLength="5"></asp:TextBox>[若扣分，请输入负数。]
                    <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidatorGivePoints"
                        runat="server" ErrorMessage="请填写积分值！" ControlToValidate="txtGivePoints" ValidationGroup="Edit" Display="None"/>
                    <asp:RegularExpressionValidator ValidationExpression="^(-?[1-9]\d*|0)$" ControlToValidate="txtGivePoints" ID="RegularExpressionValidator1" runat="server" ErrorMessage="积分值格式错误！" ValidationGroup="Edit" Display="None" />
                </td>
            </tr>
            <tr>
                <th>
                    备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注：
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtRemark" runat="server" CssClass="inputbox_300"></asp:TextBox>                   
                </td>
            </tr>
        </table>
    </div>
    <div class="dv_submit">
        <asp:Button ID="btnUpdate" runat="server" Text="保存" CssClass="btn_Save" OnClick="btnUpdate_Click"
            CommandName="edit" ValidationGroup="Edit" />
        <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Edit"
            ShowMessageBox="true" ShowSummary="false" />
        <asp:Button ID="btnReturn" SkinID="Return" runat="server" Text="返回" OnClientClick="closeWindow()"
            CssClass="btn_Cancel" />
    </div>
</asp:Content>

