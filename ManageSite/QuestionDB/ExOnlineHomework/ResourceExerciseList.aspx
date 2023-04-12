<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="ResourceExerciseList.aspx.cs" Inherits="QuestionDB_ExOnlineHomework_ExerciseList" %>

<%@ Register src="~/QuestionDB/ExOnlineHomework/Controls/ResourceExerciseList.ascx" tagname="ExerciseList" tagprefix="uc1" %>

<asp:Content ID="cpback" runat="server" ContentPlaceHolderID="cphBack">
    <a href="../../Resource/CourseResource.aspx" class="btn_Return">返回</a>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ExerciseList ID="ExerciseList1" runat="server" />
</asp:Content>
