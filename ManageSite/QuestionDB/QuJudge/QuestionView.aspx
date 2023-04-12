<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="QuestionView.aspx.cs" Inherits="QuestionDB_QuJudge_QuestionView" %>

  <%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--表单录入-->
    <div class="dv_information">
        <table class="GridviewGray fixedTable">
           <tr>
                <th style="width: 80px;">
                    课程名称：
                </th>
                <td>
                    <asp:Literal ID="ltlQuestionBankName" runat="server"></asp:Literal>
                </td>
                <th style="width: 80px;">
                    难 度：
                </th>
                <td width="100">
                   <cc1:DictionaryLabel ID="lblDifficulty" DictionaryType="Dic_DegreeDifficulty" runat="server" />
                </td>
            </tr>
        </table>

        <table class="GridviewGray">
            <tr>
                <td class="colorYellow">
                     <asp:Literal ID="ltlQuestionTitle" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td style="height:100px; vertical-align:text-top;">
                    <asp:RadioButton ID="rbtnOptionA" GroupName="rbtnOption" runat="server"  Text="是"/><br />
                    <asp:RadioButton ID="rbtnOptionB" GroupName="rbtnOption" runat="server"  Text="否"/>
                </td>
            </tr>
            </table>
       
    </div>
<!--提交表单-->
        <div class="dv_submit">
            <input value="关闭" type="button" onclick="javascript:closeWindow();" class="btn_Close">
        </div>     
</asp:Content>


