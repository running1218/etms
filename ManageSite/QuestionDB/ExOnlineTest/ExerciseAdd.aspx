<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="ExerciseAdd.aspx.cs" Inherits="QuestionDB_ExOnlineTest_ExerciseAdd" %>

<%@ Register src="~/QuestionDB/ExOnlineTest/Controls/ExerciseInfo.ascx" tagname="ExerciseInfo" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ExerciseInfo ID="ExerciseInfo1" runat="server" />
</asp:Content>
