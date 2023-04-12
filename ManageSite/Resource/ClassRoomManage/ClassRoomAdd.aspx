<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="ClassRoomAdd.aspx.cs" Inherits="ETMS.WebApp.Manage.Resource.ClassRoomManage.ClassRoomAdd" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="Controls/ClassRoomInfo.ascx" TagName="ClassRoomInfo" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="dv_information">
        <table class="GridviewGray fixedTable">
            <tr>
                <th >
                    教室编码：
                </th>
                <td >
                    <asp:TextBox ID="txtClassRoomCode" runat="server" CssClass="inputbox_210" MaxLength="20"></asp:TextBox>
                    <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidatorClassRoomCode"
                        runat="server" ErrorMessage="请填写教室编码！" ControlToValidate="txtClassRoomCode" ValidationGroup="Edit"
                        Display="None"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th >
                    教室名称：
                </th>
                <td >
                    <asp:TextBox ID="txtClassRoomName" runat="server" CssClass="inputbox_300" MaxLength="50"></asp:TextBox>
                    <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidatorClassRoomName"
                        runat="server" ErrorMessage="请填写教室名称！" ControlToValidate="txtClassRoomName" ValidationGroup="Edit"
                        Display="None"></asp:RequiredFieldValidator>
                </td>
            </tr>
            </table>
             <table class="GridviewGray fixedTable">
            <tr>
                <th>
                    状&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;态：
                </th>
                <td>
                    <cc1:DictionaryRadioButtonList runat="server" ID="Dic_Statuses" DictionaryType='Dic_Status' />
                </td>
                <th>
                    用&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;途：
                </th>
                <td>
                    <asp:TextBox ID="txtClassRoomPurpose" runat="server" CssClass="inputbox_120" />
                    <%--<cc1:DictionaryDropDownList runat="server" ID="Dic_ClassroomUse1" DictionaryType="Dic_ClassroomUse"
                            IsShowChoose="true" />--%>
                    <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                        runat="server" ErrorMessage="请填写教室用途！" ControlToValidate="txtClassRoomPurpose"
                        ValidationGroup="Edit" Display="None"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>
                    容&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;量：
                </th>
                <td>
                    <asp:TextBox ID="txtCapacity" runat="server" CssClass="inputbox_120" MaxLength="5"></asp:TextBox>
                    <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                        runat="server" ErrorMessage="请填写教室容量！" ControlToValidate="txtCapacity" ValidationGroup="Edit"
                        Display="None"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtCapacity"
                        Display="None" ErrorMessage="容量格式错误！" ValidationExpression="^[0-9]*[1-9][0-9]*$"
                        ValidationGroup="Edit"></asp:RegularExpressionValidator>
                </td>
                <th>
                    负&nbsp;&nbsp;责&nbsp;&nbsp;人：
                </th>
                <td>
                    <asp:TextBox ID="txtDutyUser" runat="server" CssClass="inputbox_120" MaxLength="30"></asp:TextBox>
                    <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                        runat="server" ErrorMessage="请填写教室负责人！" ControlToValidate="txtDutyUser" ValidationGroup="Edit"
                        Display="None"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>
                    联系电话：
                </th>
                <td>
                    <asp:TextBox ID="txtPhone" runat="server" CssClass="inputbox_120" MaxLength="11"></asp:TextBox>
                    <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                        runat="server" ErrorMessage="请填写联系电话！" ControlToValidate="txtPhone" ValidationGroup="Edit"
                        Display="None"></asp:RequiredFieldValidator>   
                </td>
                <th>
                    价&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;格：
                </th>
                <td >
                    <asp:TextBox ID="txtPrice" runat="server" MaxLength="6"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="validator" ValidationExpression="^[\+\-]?\d*?\.?\d*?$"
                            ControlToValidate="txtPrice" ErrorMessage="价格格式错误！" ValidationGroup="Edit"
                            runat="server" Display="None" />
                </td>
               
            </tr>
            <tr>
                 <th>
                    地&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;址：
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtAddress" runat="server" CssClass="inputbox_300" MaxLength="100"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    教室说明：
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtDescription" runat="server" CssClass="inputbox_300"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    管理制度：
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtRegulations" TextMode="MultiLine" runat="server" CssClass="inputbox_area300"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <!--提交表单-->
    <div class="dv_submit">
        <asp:LinkButton ID="lbnSave" runat="server" OnClick="lbnSave_Click" CssClass="btn_Save"
            ValidationGroup="Edit">保存</asp:LinkButton>
        <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Edit"
            ShowMessageBox="true" ShowSummary="false" />
        <a href="javascript:closeWindow();" class="btn_Cancel padleft10">取消</a>
    </div>
</asp:Content>
