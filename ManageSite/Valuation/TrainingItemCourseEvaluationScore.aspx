<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="TrainingItemCourseEvaluationScore.aspx.cs" Inherits="Valuation_TrainingItemCourseEvaluationScore" %>

<%@ Register Src="~/Valuation/Controls/EvaluationMark.ascx" TagName="EvaluationMark" TagPrefix="uc3" %>

<%@ Register src="Controls/EvaluationScore.ascx" tagname="EvaluationScore" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" Runat="Server">
    <a href="ValuationCourse.aspx" class="btn_Return">返回</a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <!--课程信息 begin-->
    <div class="dv_CourseContent">
        <div class="dv_Coursetitle">
            <span class="dv_CourseName">
                <asp:Literal ID="ltlCourseName" runat="server"></asp:Literal></span>
        </div>
        <div class="dv_courseBox">
            <asp:Image ID="imgCourse" runat="server" BorderWidth="0" />
            <!--课程信息 end-->
            <!--评价信息 begin-->
            <uc3:EvaluationMark ID="EvaluationMark1" runat="server" />
        </div>
        <!--评价信息 end-->
     
    </div>   
    <!--主观题评价结果 begin-->
    <uc1:EvaluationScore ID="EvaluationScore1" runat="server" />    
    <!--主观题评价结果 end-->

</asp:Content>

