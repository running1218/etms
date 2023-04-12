<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ClassInfo.ascx.cs" Inherits="LearningManagement_ClassManager_Controls_ClassInfo" %>
<div class="dv_information">
    <table class="GridviewGray">
        <tr>
            <th>
                班级名称：
            </th>
            <td>
                <asp:TextBox ID="txtClassName" runat="server" CssClass="inputbox_210" MaxLength="30" />
                <span style="color: Red;">*</span><asp:RequiredFieldValidator
                        ID="RequiredFieldValidatorJobName" runat="server" ErrorMessage="请填写班级名称！" ControlToValidate="txtClassName"
                        ValidationGroup="Error" Display="None"></asp:RequiredFieldValidator>  
            </td>
        </tr>
         <tr>
            <th>
                负&nbsp;&nbsp;责&nbsp;&nbsp;人：
            </th>
            <td>
                <asp:TextBox ID="txtDutyUser" runat="server" CssClass="inputbox_210" MaxLength="30" />
                <span style="color: Red;">*</span><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator1" runat="server" ErrorMessage="请填写负责人！" ControlToValidate="txtDutyUser"
                        ValidationGroup="Error" Display="None"></asp:RequiredFieldValidator>  
            </td>
        </tr>
        <tr>
           <th>
                负责人电话：
            </th>
            <td>
                <asp:TextBox ID="txtTel" runat="server" CssClass="inputbox_210" MaxLength="11"/>
                <asp:RegularExpressionValidator ValidationExpression="\d{11}" ID="RegularExpressionValidator1"
                        Display="None" runat="server" ErrorMessage="请正确填写手机！" ControlToValidate="txtTel"
                        ValidationGroup="Error"></asp:RegularExpressionValidator>
            </td>            
        </tr>
        <tr>
          <th>
                负责人邮箱：
            </th>
            <td>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="inputbox_210" MaxLength="60" />
                <asp:RegularExpressionValidator ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        ID="RequiredFieldValidatorEmail" runat="server" ErrorMessage="请正确填写邮箱！" ControlToValidate="txtEmail"
                        Display="None" ValidationGroup="Error"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <th>
                班级说明：
            </th>
            <td>
                <asp:TextBox ID="txtClassDesc" runat="server" CssClass="inputbox_area300" TextMode="MultiLine" />
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtClassDesc"
                    ValidationExpression="^(\s|\S){0,500}$" Display="None" ErrorMessage="班级说明字数最多不能超过500个字符！"
                    ValidationGroup="Error" />
            </td>
        </tr>
    </table>
</div>
<div class="dv_submit">
    <asp:Button ID="btnSave" runat="server" CssClass="btn_Save" OnClick="btnSave_Click" Text="保存" ValidationGroup="Error" />
     <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Error"
        ShowMessageBox="true" ShowSummary="false"  />
    <%--<input type="button" class="btn_Save" value="保存" onclick="javascript:popSuccessMsg('恭喜你保存成功！','提示',closeWindow);" />--%>
    <input type="button" class="btn_Cancel" value="取消" onclick="javascript:closeWindow();" />
</div>
