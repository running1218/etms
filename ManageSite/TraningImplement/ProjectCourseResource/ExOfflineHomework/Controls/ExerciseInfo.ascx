<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ExerciseInfo.ascx.cs"
    Inherits="QuestionDB_ExOfflineHomework_Controls_ExerciseInfo" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/ChooseCourseDropdown.ascx" TagName="ChooseCourseDropdown" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/MiniUpFile.ascx" TagName="uploader" TagPrefix="uc" %>
<!--功能标题-->
<h2 class="dv_title">
    实践基本信息
</h2>
<!--表单录入-->
<div class="dv_information">
   <table  class="GridviewGray fixedTable">
            <tr>
                <th>
                    项目名称：
                </th>
                <td>                    
                    <asp:Label ID="lblItemID" runat="server" />                    
                </td>
                <th>
                    课程名称：
                </th>
                <td>
                    <asp:Label ID="lblCourseID" runat="server" />                    
                </td>
            </tr> 
    </table>
    <table  class="GridviewGray fixedTable">             
            <tr>
                <th>
                    名称：
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtJobName" runat="server" CssClass="inputbox_210" MaxLength="50"></asp:TextBox>
                     <span style="color: Red;">*</span><asp:RequiredFieldValidator
                        ID="RequiredFieldValidatorJobName" runat="server" ErrorMessage="请填写名称！" ControlToValidate="txtJobName"
                        ValidationGroup="Error" Display="None"></asp:RequiredFieldValidator>                                           
                </td>
            </tr> 

             <tr>
                <th>
                    描述：
                </th>
                <td colspan="3">
                    <asp:TextBox id="txtJobDescription" runat="server" TextMode="MultiLine" CssClass="inputbox_area349" />
                    <%--<textarea id="txtJobDescription" runat="server" cols="60" rows="10"  CssClass="inputbox_210"/>--%>
                    <%--<asp:TextBox ID="txtJobDescription" runat="server" CssClass="inputbox_210"></asp:TextBox>--%>
                    <%-- <span style="color: Red;">*</span><asp:RequiredFieldValidator
                        ID="RequiredFieldValidatorJobDescription" runat="server" ErrorMessage="请填写作业描述！" ControlToValidate="txtJobDescription"
                        ValidationGroup="Edit"></asp:RequiredFieldValidator> --%>
                </td>
            </tr>
                       
            <tr>
                <th>
                    附件：
                </th>
                <td colspan="3">                    
                    <asp:Label ID="lblFilePath" runat="server" ></asp:Label>
                    <uc:uploader ID="uploader" runat="server" FunctionType="OfflineJob" CallBack="doCallBack" />
                    <script language="javascript">
                        function doCallBack(imgName, imgUrl, imgSize, FileOldName) {
                            $('#<%=lblFilePath.ClientID%>').html("<a href='" + imgUrl + "' target='_blank'>" + FileOldName + "</a>");
                        }
                    </script>                   
                </td>
            </tr>
            
             <tr>
                <th>有&nbsp;&nbsp;效&nbsp;&nbsp;期：
                </th>
                <td colspan="3">
                    <cc1:DateTimeTextBox runat="server" ID="dttbBeginDate" DataTimeFormat="%Y-%M-%D %h:%m:%s"  EndTimeControlID="dttbEndDate"></cc1:DateTimeTextBox>
                    <%--<span style="color: Red;">*</span>  --%>                              
                    至<cc1:DateTimeTextBox runat="server" ID="dttbEndDate" DataTimeFormat="%Y-%M-%D %h:%m:%s" BeginTimeControlID="dttbBeginDate"></cc1:DateTimeTextBox>
                    <span style="color: Red;">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorJobDescription" runat="server" ErrorMessage="请填写有效期开始时间！" ControlToValidate="dttbBeginDate" ValidationGroup="Error" Display="None"></asp:RequiredFieldValidator>    
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请填写有效期结束时间！" ControlToValidate="dttbEndDate" ValidationGroup="Error" Display="None"></asp:RequiredFieldValidator> 
                </td>
            </tr>            
            
            <tr>
                <th>
                    状&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;态：
                </th>
                <td colspan="3">
                   <asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal" CssClass="noborder" >
                      <asp:ListItem Value="1" Selected="True">启用</asp:ListItem>
                      <asp:ListItem Value="0">停用</asp:ListItem>
                   </asp:RadioButtonList>                   
                </td>
            </tr>             

           <%-- <tr>
                <th>
                    创建人：
                </th>
                <td>
                    <asp:TextBox ID="txtTeacherID" runat="server" CssClass="inputbox_210"></asp:TextBox>
                    <%-- <span style="color: Red;">*</span><asp:RequiredFieldValidator
                        ID="RequiredFieldValidatorTeacherID" runat="server" ErrorMessage="请填写创建人！" ControlToValidate="txtTeacherID"
                        ValidationGroup="Edit"></asp:RequiredFieldValidator> 
                </td>
            </tr>--%>
            
        </table>
</div>
<!--提交表单-->
<div class="dv_submit">
    <asp:LinkButton ID="lbnSave" runat="server" CssClass="btn_Save" OnClick="lbnSave_Click" ValidationGroup="Error">保存</asp:LinkButton>
    <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Error"
        ShowMessageBox="true" ShowSummary="false"  />
    <a href="javascript:closeWindow();" class="btn_Cancel padleft10">取消</a>
</div>
