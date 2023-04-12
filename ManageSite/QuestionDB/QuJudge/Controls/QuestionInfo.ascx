<%@ Control Language="C#" AutoEventWireup="true" CodeFile="QuestionInfo.ascx.cs"
    Inherits="QuestionDB_QuJudge_Controls_QuestionInfo" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/ChooseCourseDropdown.ascx" TagName="ChooseCourseDropdown"
    TagPrefix="uc1" %>

<!--表单录入-->
<div class="dv_information">
    <!--查找条件-->
    <table class="GridviewGray fixedTable">
        <tr>
            <th>
                课程名称：
            </th>
            <td>
                <asp:Literal ID="ltlQuestionBankName" runat="server"></asp:Literal>
            </td>
            <th>
                难度：
            </th>
            <td>
                <cc1:DictionaryDropDownList runat="server" ID="ddlDifficulty" DictionaryType="Dic_DegreeDifficulty"
                    CssClass="select_60" IsShowChoose="false" IsShowAll="false" />
            </td>
        </tr>
    </table>
    <table class="GridviewGray GridviewGrayboder">
        <tr>
            <th class="thleft">
                题目
            </th>
        </tr>
        <tr>
            <td class="alignleft">
                <asp:TextBox ID="RichTextTitle" runat="server" TextMode="MultiLine" CssClass="inputbox_area520"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table class="GridviewGray GridviewGrayboder fixedTable">
        <tr>
            <th style="width: 60px;">
                答案
            </th>
            <th style="width: 60px; display: none;">
                选项
            </th>
            <th style="width: auto" class="alignleft">
                答案内容
            </th>
        </tr>
        <tr>
            <td>
                <asp:RadioButton ID="rbtnOptionA" GroupName="rbtnOption" runat="server" />
                <asp:HiddenField ID="txtOptionA" runat="server" />
            </td>
            <td style="display: none;" class="alignleft">
                A
            </td>
            <td class="alignleft">
                是
            </td>
        </tr>
        <tr>
            <td>
                <asp:RadioButton ID="rbtnOptionB" GroupName="rbtnOption" runat="server" />
                <asp:HiddenField ID="txtOptionB" runat="server" />
            </td>
            <td style="display: none;">
                B
            </td>
            <td class="alignleft">
                否
            </td>
        </tr>
    </table>
</div>
<!--提交表单-->
<div class="dv_submit">
    <asp:LinkButton ID="lbtnSave" runat="server" CssClass="btn_Save" OnClick="lbtnSave_Click">保存</asp:LinkButton>
    <a href="javascript:closeWindow();" class="btn_Cancel padleft10">取消</a>
</div>
