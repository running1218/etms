<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="SetLearnCycle.aspx.cs" Inherits="TraningImplement_ProjectCourseResourceBatch_SetLearnCycle" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--表单录入-->
    <div class="dv_information">
        <table class="GridviewGray">
            <tr>
                <th colspan="2">
                    您共选择了 <asp:Label ID="labCount" runat="server" Text="0"></asp:Label> 个项目课程资源进行批量修改资源学习周期，请选择修改学习周期方式：
                </th>
            </tr>
            <tr>
                <th style="width: 10%">
                    <asp:RadioButton ID="rb_BatchSetResStudyTimeToItemCourse" runat="server" GroupName="rad" Checked="true" />
                </th>
                <td style="width: 90%">
                    修改项目课程资源学习周期与项目课程学习周期一致。
                </td>
            </tr>
            <tr>
                <th>
                    <asp:RadioButton ID="rb_BatchSetResStudyTime" runat="server" GroupName="rad" />
                </th>
                <td>
                    统一设置学习周期
                    <cc1:DateTimeTextBox ID="Dtt_LearnCycleBegin" runat="server" EndTimeControlID="Dtt_LearnCycleEnd"></cc1:DateTimeTextBox>
                    至
                    <cc1:DateTimeTextBox ID="Dtt_LearnCycleEnd" runat="server" BeginTimeControlID="Dtt_LearnCycleBegin"></cc1:DateTimeTextBox>
                </td>
            </tr>
        </table>
    </div>
    <div class="dv_submit">
        <asp:Button ID="Btn_Save" CssClass="btn_Save" runat="server" Text="保存" OnClick="Btn_Save_Click" />
        <input type="button" class="btn_Cancel" value="取消" onclick="javascript:closeWindow();" /></div>
</asp:Content>
