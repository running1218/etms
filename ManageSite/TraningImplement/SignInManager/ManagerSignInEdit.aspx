<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master"
    AutoEventWireup="true" CodeFile="ManagerSignInEdit.aspx.cs" Inherits="TraningImplement_SignInManager_ManagerSignInEdit" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--功能标题-->
    <h2 class="dv_title">
        管理签到
    </h2>
    <!--表单录入-->
    <div class="dv_information">
        <table class="GridviewGray fixedTable">
            <tr>
                    <th width="100">
                        项目编码：
                    </th>
                    <td width="200">
                        <asp:Label ID="lblItemCode" runat="server"></asp:Label>
                    </td>
                    <th width="100">
                        项目名称：
                    </th>
                    <td>
                        <cc1:ShortTextLabel ShowTextNum="10" ID="lblItemName" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th>
                        课程编码：
                    </th>
                    <td>
                        <asp:Label ID="lblCourseCode" runat="server"></asp:Label>
                    </td>
                    <th>
                        课程名称：
                    </th>
                    <td>
                        <cc1:ShortTextLabel ShowTextNum="10" ID="lblCourseName" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th>
                        培训日期：
                    </th>
                    <td>
                        <asp:Label ID="lblTrainingDate" runat="server" />
                    </td>
                    <th>
                        讲&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;师：
                    </th>
                    <td>
                        <asp:Label ID="lblTeacherName" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th>
                        培训时段：
                    </th>
                    <td >
                        <asp:Label ID="lblTrainingTime" runat="server" />
                    </td>
                    <% if (ItemCourseHoursStudentID==Guid.Empty)
                       { %>
                    <th>
                        选择学员数：
                    </th>
                    <td >
                        <asp:Label ID="lblSelectStudentNum" runat="server" />
                    </td>
                    <%}
                       else
                       { %>
                     <th>
                        学员姓名：
                    </th>
                    <td >
                        <asp:Label ID="lblSelectStudentName" runat="server" />
                    </td>
                    <%} %>
                </tr>
            <tr>
                <th>
                    签到信息：
                </th>
                <td >
                     <cc1:DictionaryDropDownList runat="server" ID="ddlSigninTypeID" DictionaryType="Dic_Sys_SigninType" IsShowChoose="true" /><asp:Label ID="labSigninTypeName" runat="server" Visible="false"></asp:Label>
                     <span id="signMsg" runat="server" style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidatorJobName"
                    runat="server" ErrorMessage="请选择签到信息！" ControlToValidate="ddlSigninTypeID" ValidationGroup="Error"
                    Display="None"></asp:RequiredFieldValidator>
                </td>
                 <th>
                    违纪情况：
                </th>
                <td>
                     <cc1:DictionaryDropDownList runat="server" ID="ddlLawlessness" DictionaryType="Dic_Sys_Lawlessness"
                            IsShowChoose="true" />
                </td>
            </tr>
            
            <tr>
           
              <th>
                    缺课时长：
             </th>
                <td colspan="3" >
                 <asp:TextBox ID="txtLeaveMinutes" runat="server" CssClass="inputbox_60" MaxLength="7"></asp:TextBox>分钟
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtLeaveMinutes" Display="None" ErrorMessage="缺课时长格式错误！" ValidationExpression="^\d+$" ValidationGroup="Error"></asp:RegularExpressionValidator>
                    <%-- <cc1:DateTimeTextBox runat="server" ID="txtLeaveMinutes" DateTimeFormat="%m"></cc1:DateTimeTextBox>--%>
                </td>
                 
           
               
            </tr>            
        </table>
    </div>
    <div class="dv_submit">
        <asp:Button ID="lbnSave" Text="保存" runat="server" CssClass="btn_Save" OnClick="lbnSave_Click" ValidationGroup="Error"></asp:Button>
        <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Error" ShowMessageBox="true" ShowSummary="false" />
        <%--<input type="button" class="btn_Save" value="保存" onclick="javascript:popSuccessMsg('恭喜你保存成功！','提示',closeWindow);" />--%>
        <input type="button" class="btn_Cancel" value="取消" onclick="javascript:closeWindow();" />
   </div>
</asp:Content>
