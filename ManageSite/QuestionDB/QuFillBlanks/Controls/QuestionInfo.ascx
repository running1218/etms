<%@ Control Language="C#" AutoEventWireup="true" CodeFile="QuestionInfo.ascx.cs"
    Inherits="QuestionDB_QuFillBlanks_Controls_QuestionInfo" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="../../../Controls/ChooseCourseDropdown.ascx" TagName="ChooseCourseDropdown"
    TagPrefix="uc1" %>
<%@ Register Assembly="ETMS.Editor" Namespace="ETMS.Editor.UEditor" TagPrefix="wuc" %>
<!--功能标题-->
<h2 class="dv_title">
    填空题
</h2>
<!--表单录入-->
<div class="dv_information">
    <!--查找条件-->
    <div class="dv_searchbox">
        <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <th style="width: 17%;">
                    课程名称：
                </th>
                <td style="width: 53%;">
                    <uc1:ChooseCourseDropdown ID="ChooseCourseDropdown1" runat="server" />
                </td>
                <th style="width: 10%;">
                    难度：
                </th>
                <td style="width: 20%;">
                    <cc1:DictionaryDropDownList runat="server" ID="Dic_QuestionLevel1" DictionaryType="Dic_QuestionLevel"
                        IsShowChoose="true" />
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
                <wuc:UEditor runat="server" ID="FCKeditor1" ToolType="Basic" Width="520" Height="120" />
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
                <wuc:UEditor runat="server" ID="FCKeditor2" ToolType="Basic" Width="520" Height="80" />
            </td>
        </tr>
    </table>
</div>
<!--提交表单-->
<div class="dv_submit">
    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn_Save" OnClick="LinkButton1_Click">保存</asp:LinkButton>
    <a href="javascript:closeWindow();" class="btn_Cancel padleft10">取消</a>
</div>
