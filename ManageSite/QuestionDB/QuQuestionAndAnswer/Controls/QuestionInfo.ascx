<%@ Control Language="C#" AutoEventWireup="true" CodeFile="QuestionInfo.ascx.cs"
    Inherits="QuestionDB_QuQuestionAndAnswer_Controls_QuestionInfo" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/ChooseCourseDropdown.ascx" TagName="ChooseCourseDropdown"
    TagPrefix="uc1" %>
<!--功能标题-->
<h2 class="dv_title">
    简答题
</h2>
<!--表单录入-->
<div class="dv_information">
    <!--查找条件-->
    <div class="dv_searchbox">
        <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0">
           <tr>
            <th  style="width: 80px;">
                课程名称：
            </th>
            <td >
                <uc1:ChooseCourseDropdown ID="ChooseCourseDropdown1" runat="server" />
            </td>
            <th   style="width: 60px;">
                难度：
            </th>
            <td  style="width: 80px;">
               
            </td>
        </tr>
        </table>
    </div>
    <table class="GridviewGray GridviewGrayboder" style="width: 96%">
        <tr>
            <th class="thleft">
                题目
            </th>
        </tr>
        <tr>
            <td>

            </td>
        </tr>
    </table>
    <table class="GridviewGray" style="width: 96%">
        <tr>
            <th class="thleft">
                正确答案
            </th>
        </tr>
        <tr>
            <td>
            
            </td>
        </tr>
    </table>
    <!--解题思路-->
    <table class="GridviewGray" style="width: 96%">
        <tr>
            <th class="thleft">
                解题思路
            </th>
        </tr>
        <tr>
            <td>
               
            </td>
        </tr>
    </table>
</div>
<!--提交表单-->
<div class="dv_submit">
    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn_Save" OnClick="LinkButton1_Click">保存</asp:LinkButton>
    <a href="javascript:closeWindow();" class="btn_Cancel padleft10">取消</a>
</div>
