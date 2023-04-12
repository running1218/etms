<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="ChooseCourse.aspx.cs" Inherits="Resource_ProfessorManage_ChooseCourse" %>

<%@ Register src="../../Controls/ChooseCourse.ascx" tagname="ChooseCourse" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ChooseCourse ID="ChooseCourse1" runat="server" />
    <div class="center">
            
            <a href="javascript:history.go(-1);"
                 Class="btn_Ok">确定</a>
            <a href="javascript:history.go(-1);"
                class="btn_Cancel padleft10">取消</a>
        </div>
</asp:Content>

