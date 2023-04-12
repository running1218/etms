<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="PublishCourse.aspx.cs" Inherits="Resource_CoursewareManage_PublishCourse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--功能标题-->
    <h2 class="dv_title">
        非SCORM标准
    </h2>
    <div class="dv_information">
        <table class="GridviewGray">
            <tr>
                <td>
                    将课件发布到其他课程上！
                </td>
            </tr>
        </table>
    </div>
    <!--关闭-->
    <div class="dv_submit">
        <a href="javascript:closeWindow();" class="btn_Close">关闭</a>
    </div>
</asp:Content>
