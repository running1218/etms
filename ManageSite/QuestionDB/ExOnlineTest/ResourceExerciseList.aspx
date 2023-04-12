<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="ResourceExerciseList.aspx.cs" Inherits="ETMS.WebApp.Manage.ExOnlineTest_ExerciseList" %>

<%@ Register src="~/QuestionDB/ExOnlineTest/Controls/ResourceExerciseList.ascx" tagname="ResourceExerciseList" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ResourceExerciseList ID="ExerciseList1" runat="server" />
</asp:Content>
