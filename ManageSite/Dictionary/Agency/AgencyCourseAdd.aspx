<%@ Page Title="" ValidateRequest="false" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master"
    AutoEventWireup="true" CodeFile="AgencyCourseAdd.aspx.cs" Inherits="ETMS.WebApp.Manage.AgencyCourseAdd" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="dv_information">
        <table class="GridviewGray fixedTable">
            <tr>
                <th>
                    课程：
                </th>
                <td>
                    <asp:DropDownList runat="server" ID="ddlCourse" CssText="select_120" />
                    <font color="red">*</font>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCourse"
                        Display="None" ErrorMessage="请选择课程！" ValidationGroup="Error"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>优惠码：</th>
                <td>
                    <asp:Label ID="lblDiscountCode" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>                
                <th>
                    优惠金额：
                </th>
                <td>
                    <cc1:CustomTextBox ContentType="Decimal" ID="txtDiscountPrice" runat="server" CssClass="inputbox_120" MaxLength="15"></cc1:CustomTextBox>
                    <font color="red">*</font>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorName" runat="server" ControlToValidate="txtDiscountPrice"
                        Display="None" ErrorMessage="请填写优惠金额！" ValidationGroup="Error"></asp:RequiredFieldValidator>
                </td>
            </tr>                              
        </table>
    </div>
    <!--提交表单-->
    <div class="dv_submit">
        <asp:LinkButton ID="lbtnSave" runat="server" CssClass="btn_Save" OnClick="lbtnSave_Click"
            ValidationGroup="Error">保存</asp:LinkButton>
        <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Error"
            ShowMessageBox="true" ShowSummary="false" />
        <a href="javascript:closeWindow();" class="btn_Cancel padleft10">取消</a>
    </div>
</asp:Content>
