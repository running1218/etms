<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="TeacherEvaluation.aspx.cs" Inherits="Valuation_TeacherEvaluation" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="Controls/EvaluationMark.ascx" TagName="EvaluationMark" TagPrefix="uc3" %>
<%@ Register Src="Controls/PlateResult.ascx" TagName="PlateResult" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" runat="Server">
    <a href="ValuationTeacher.aspx" class="btn_Return">返回</a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--讲师信息 begin-->
    <div class="dv_CourseContent">
        <div class="dv_Coursetitle dv_searchbox2">
            <span class="dv_CourseName"><b>姓&nbsp;&nbsp;名：</b><%= getRealName()%><b>
               &nbsp;&nbsp; 讲师等级：</b><cc1:DictionaryLabel ID="lblTeacherLevel" DictionaryType="Dic_Sys_TeacherLevel"
                    runat="server" /></span>
        </div>
        <!--讲师信息 end-->
        <div class="dv_courseBox">
            <div class="padding10">
            <!--评价信息 begin-->
            <img src='<%= getUserImg()%>' border=0 />
            <!--评价信息 end-->
            <uc3:EvaluationMark ID="EvaluationMark1" runat="server" />
            </div>
        </div>
    </div>
    <!--主观题评价结果 begin-->
    <uc4:PlateResult ID="PlateResult1" runat="server" />
    <!--主观题评价结果 end-->
</asp:Content>
