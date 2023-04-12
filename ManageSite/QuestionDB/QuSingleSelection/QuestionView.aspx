<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="QuestionView.aspx.cs" Inherits="QuestionDB_QuSingleSelection_QuestionView" %>
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

        <table class="GridviewGray fixedTable">
            <tr>
                <td class="colorYellow">
                    <asp:Literal ID="ltlQuestionTitle" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td >
                <%for (int i = 0; i < OptionCount; i++){%>
                <p>
                   <input type="radio" id="rbtnOption_<%=i %>" name="myradio" <%= GetQuestionChecked(i) %>  /><%= GetQuestionOptionContent(i) %>
                   </p>
                   <% }%>
                </td>
            </tr>
            </table>
           
       
    </div>
     <!--提交表单-->
        <div class="dv_submit">
            <input value="关闭" type="button" onclick="javascript:closeWindow();" class="btn_Close">
        </div>
</asp:Content>
