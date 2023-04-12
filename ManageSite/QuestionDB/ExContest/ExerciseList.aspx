<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="ExerciseList.aspx.cs" Inherits="QuestionDB_ExContest_ExerciseList" %>

<%@ Register src="~/QuestionDB/ExContest/Controls/ExerciseList.ascx" tagname="ExerciseList" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ExerciseList ID="ExerciseList1" runat="server" />
</asp:Content>