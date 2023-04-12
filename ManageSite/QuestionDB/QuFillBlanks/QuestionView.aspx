<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="QuestionView.aspx.cs" Inherits="QuestionDB_QuFillBlanks_QuestionView" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
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

        <table  class="GridviewGray">
    
            <tr>
                <td colspan="2"  class="colorYellow">
           谈判的要素是 <input maxlength="30" size="30" value="谈判主题和参与者的个性" />。
                </td>
            </tr>
            </table>
            <table  class="GridviewGray">
            <tr>
            <th  style="width:80px;">答案反馈：</th>
                <td class="thleft">
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
            <th>解题思路：</th>
                <td class="thleft">
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


