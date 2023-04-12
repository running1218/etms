<%@ Control Language="C#" AutoEventWireup="true" CodeFile="QuFillBlanksInfo.ascx.cs"
    Inherits="Poll_QuestionManage_Controls_QuFillBlanksInfo" %>
<!--表单录入-->
<div class="dv_information">
    <table class="GridviewGray GridviewGrayboder">
        <tr>
            <th class="thleft">
                题目
            </th>
        </tr>
        <tr>
            <td class="alignleft">
                <asp:Label ID="RichTextTitle" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th class="alignleft">
                题目序号：<asp:Label runat="server" ID="lblTitleNo"></asp:Label>
            </th>
        </tr>
    </table>
</div>
<!--提交表单-->
<div class="dv_submit">
    <a href="javascript:closeWindow();" class="btn_Cancel padleft10">关闭</a>
</div>
