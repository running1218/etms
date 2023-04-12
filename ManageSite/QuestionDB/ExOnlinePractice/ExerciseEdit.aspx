<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="ExerciseEdit.aspx.cs" Inherits="QuestionDB_ExOnlinePractice_ExerciseEdit" %>
<%@ Register src="../Controls/ExerciseInfo.ascx" tagname="ExerciseInfo" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ExerciseInfo ID="ExerciseInfo1" runat="server" />
</asp:Content>
