<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="TeacherEvaluationScore.aspx.cs" Inherits="Valuation_TeacherEvaluationScore" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Valuation/Controls/EvaluationMark.ascx" TagName="EvaluationMark" TagPrefix="uc3" %>
<%@ Register src="~/Valuation/Controls/EvaluationScore.ascx" tagname="EvaluationScore" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" Runat="Server">
<a href="ValuationTeacher.aspx" class="btn_Return">返回</a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <!--讲师信息 begin-->
    <div class="dv_CourseContent">
        <div class="dv_Coursetitle">
            <span class="dv_CourseName"><b>姓&nbsp;&nbsp;名：</b><%= getRealName()%><b>
               &nbsp;&nbsp; 讲师等级：</b><cc1:DictionaryLabel ID="lblTeacherLevel" DictionaryType="Dic_Sys_TeacherLevel"
                    runat="server" /></span>
        </div>
        <!--讲师信息 end-->
        <div class="dv_courseBox">
            <!--评价信息 begin-->
            <img src='<%= getUserImg()%>' border=0 />
            <!--评价信息 end-->
            <uc3:EvaluationMark ID="EvaluationMark1" runat="server" />
        </div>
    </div>
    <!--评价项大分结果 begin-->
    <uc1:EvaluationScore ID="EvaluationScore1" runat="server" />
    <!--评价项大分结果 end-->
    
</asp:Content>

