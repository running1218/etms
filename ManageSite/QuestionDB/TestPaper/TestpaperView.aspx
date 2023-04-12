<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="TestpaperView.aspx.cs" Inherits="QuestionDB_TestPaper_TestpaperView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        body
        {
            background: url(../App_Themes/ThemeAdmin/Images/bjlad.gif) repeat;
        }
    </style>
    <div class="dv_questionTitle">
        <asp:Literal ID="ltlExerciseName" runat="server"></asp:Literal></div>
    <div class="dv_CousreInformation">
        <div class="dv_questionList">
            <table cellspacing="5" cellpadding="5" style="width:98%;">
                <asp:Literal ID="ltlTestpaperInfo" runat="server"></asp:Literal>
            </table>
        </div>
        <div class="boderDashed">
            <input type="button" value="关 闭" class="btn_Cancel" onclick="javascript:window.close()" /><br />
            <br />
        </div>
    </div>
    
</asp:Content>
