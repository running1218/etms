<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="ExerciseView.aspx.cs" Inherits="QuestionDB_ExOnlinePractice_ExerciseView" %>

<%@ Register src="../Controls/ExerciseView.ascx" tagname="ExerciseView" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ExerciseView ID="ExerciseView1" runat="server" />
</asp:Content>