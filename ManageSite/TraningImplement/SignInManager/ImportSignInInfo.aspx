<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master"
    AutoEventWireup="true" CodeFile="ImportSignInInfo.aspx.cs" Inherits="TraningImplement_SignInManager_ImportSignInInfo" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--功能标题-->
    <h2 class="dv_title">
        管理签到
    </h2>
    <!--表单录入-->
    <div class="dv_information">
        <table class="GridviewGray">
            <tr>
                    <th style=" width:15%;">
                        项目编码：
                    </th>
                    <td >
                        <asp:Label ID="lblItemCode" runat="server"></asp:Label>
                    </td>
                    <th style=" width:15%;">
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
                    <td colspan="3">
                        <asp:Label ID="lblTrainingTime" runat="server" />
                    </td>                    
                </tr>
            <tr>
                <th colspan="4">
                    点击此处<a href="#">导出学生签到表</a>，在导出的学员签到表中修改学员签到信息后上传。
                </th>                
            </tr>
            <tr>
                <th>
                    导入文件：
                </th>
                <td colspan="3">
                    <asp:Label ID="lblFilePath" runat="server" ></asp:Label>
                    <cc1:FileUpload runat="server" ID="fileUpload1" FunctionType="OfflineJob" CallBack="callback" />
                    <script language="javascript">
                        function callback(imgName, imgUrl) {
                            $('#<%=lblFilePath.ClientID%>').html("<a href='" + imgUrl + "' target='_blank'>" + imgName + "</a>");
                        }
                    </script>
                </td>
            </tr>
        </table>
    </div>
    <div class="dv_submit">
          <input type="button" class="btn_Save" value="保存" onclick="javascript:popSuccessMsg('恭喜你保存成功！','提示',closeWindow);" />
          <input type="button" class="btn_Cancel" value="取消" onclick="javascript:closeWindow();" />
    </div>
</asp:Content>
