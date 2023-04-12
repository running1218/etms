<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="ExcerciseStatus.aspx.cs" Inherits="QuestionDB_ExOfflineHomework_ExcerciseStatus" %>
<%@ Register Src="Controls/ExcerciseNoSubmit.ascx" TagName="NoSubmit" TagPrefix="uc1" %>
<%@ Register Src="Controls/ExerciseUnEvaluation.ascx" TagName="UnEvaluation" TagPrefix="uc2" %>
<%@ Register Src="Controls/ExerciseEvaluated.ascx" TagName="Evaluated" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" Runat="Server">
<!--导航路径-->
        <div class="dv_path">
            当前位置：资源管理系统>>学习资源管理>>离线作业管理
        </div>
        <!--功能标题-->
        <h2 class="dv_title">
            离线作业管理
        </h2>
        <a id="aBack" runat="server" class="btn_Return">返回</a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="dv_GradeviewList">
        <table  border="0" cellpadding="0" cellspacing="0" runat="server" id="tableQueryControlList">
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
            <tr>
               <th>
                  名称：
               </th>
                <td>
                   <asp:Label ID="lblJobID" runat="server" />
                </td>
                <th>
                  起止时间：
               </th>
                <td>
                   <asp:Label ID="lblTime" runat="server" />
                </td>
            </tr>
            <tr>
               <th>
                  描述：
               </th>
                <td colspan="3">
                   <asp:Label ID="lblJobDescription" runat="server" />
                </td>
            </tr>
             <tr>
               <th>
                  附件：
               </th>
                <td>
                   <asp:Label ID="lblJobFileName" runat="server"/>
                </td>
                <th>
                  学员总数：
               </th>
                <td>
                   <asp:Label ID="lblStudentNum" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <!--表单录入-->
    <div class="dv_serviceTab">
        <div class="dv_Tabmenus" style="min-width: 800px;">
            <ul>
                <li id="Tab_0" onclick="showTab('Tab_0', 'Div_Select_0','selected')">
                    <a onfocus="blur()" href="javascript:void(0);"><span class="bj">待批阅</span></a></li>
                <li id="Tab_1" onclick="showTab('Tab_1', 'Div_Select_1','selected')"><a onfocus="blur()"
                    href="javascript:void(0);"><span class="bj">已批阅</span></a></li>
                <li id="Tab_2" onclick="showTab('Tab_2', 'Div_Select_2','selected')"><a onfocus="blur()"
                    href="javascript:void(0);"><span class="bj">未提交</span></a></li>
            </ul>
        </div>
    </div>
    <div class="info">
      <%-- <asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>
        <div id="Div_Select_0" class="">
           <uc2:UnEvaluation ID="ExerciseUnEvaluation" runat="server" />
        </div>
        <div id="Div_Select_1" class="" style="display: none">
           <uc3:Evaluated ID="Evaluated" runat="server" />
        </div>        
        <div id="Div_Select_2" class="" style="display: none">
             <uc1:NoSubmit ID="ExcerciseNoSubmit" runat="server" />
        </div>
       <%-- </ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
   
</asp:Content>

