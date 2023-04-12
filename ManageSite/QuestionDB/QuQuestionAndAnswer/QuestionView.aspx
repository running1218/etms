<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="QuestionView.aspx.cs" Inherits="QuestionDB_QuQuestionAndAnswer_QuestionView" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Assembly="ETMS.Editor" Namespace="ETMS.Editor.UEditor" TagPrefix="wuc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--功能标题-->
    <h2 class="dv_title">
        试题浏览
    </h2>
    <!--表单录入-->
    <div class="dv_information">
        <table class="GridviewGray Questionview">
            <tr>
                <th style="width: 80px;">
                    课程名称：
                </th>
                <td>
                    <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                </td>
                <th style="width: 80px;">
                    难 度：
                </th>
                <td width="100">
                    <asp:Literal ID="Literal3" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
        <table class="GridviewGray">
            <tr>
                <td class="colorYellow">
                    周恩来谈判艺术的特点？ (字数不得少于200字)
                </td>
            </tr>
            <tr>
                <td>
                    
                </td>
            </tr>
        </table>
        <table class="GridviewGray">
            <tr>
                <th style="width:80px;">
                    解题思路：
                </th>
                <td>
                    <asp:Literal ID="Literal5" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
    </div>
    <!--提交表单-->
    <div class="dv_submit">
        <a href="javascript:closeWindow();" class="btn_Close">关闭</a>
    </div>
</asp:Content>
