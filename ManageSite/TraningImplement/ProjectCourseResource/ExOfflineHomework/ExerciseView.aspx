<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="ExerciseView.aspx.cs" Inherits="QuestionDB_ExOfflineHomework_ExerciseView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--功能标题-->
    <h2 class="dv_title">
        离线作业基本信息
    </h2>
    <!--表单录入-->
    <div class="dv_information">
        <table  class="GridviewGray">            
            <tr>
                <th>
                    作业名称：
                </th>
                <td>
                    <asp:Label ID="lblJobName" runat="server"></asp:Label>
                </td>
            </tr>
            
            <tr>
                <th>
                    作业描述：
                </th>
                <td>
                    <asp:Label ID="lblJobDescription" runat="server"></asp:Label>
                </td>
            </tr>
            
            <tr>
                <th>
                    作业附件：
                </th>
                <td>
                    <asp:Label ID="lblJobFileName" runat="server"></asp:Label>
                </td>
            </tr>           
            
            <tr>
                <th>
                    作业有效状态：
                </th>
                <td>
                    <asp:Label ID="lblJobStatus" runat="server"></asp:Label>
                </td>
            </tr>            
           
            <tr>
                <th>
                    创建时间：
                </th>
                <td>
                    <asp:Label ID="lblCreateTime" runat="server"></asp:Label>
                </td>
            </tr>
            
            <tr>
                <th>
                    创建人：
                </th>
                <td>
                    <asp:Label ID="lblTeacherID" runat="server"></asp:Label>
                </td>
            </tr>
            
        </table>
    </div>
    <!--提交表单-->
    <div class="dv_submit">
        <a href="javascript:closeWindow();" class="btn_Close">关闭</a>
    </div>
</asp:Content>
