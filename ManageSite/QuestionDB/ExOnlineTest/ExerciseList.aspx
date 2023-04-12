<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="ExerciseList.aspx.cs" Inherits="QuestionDB_ExOnlineTest_ExerciseList" %>

<%@ Register src="~/QuestionDB/ExOnlineTest/Controls/ExerciseList.ascx" tagname="ExerciseList" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ExerciseList ID="ExerciseList1" runat="server" />
</asp:Content>
