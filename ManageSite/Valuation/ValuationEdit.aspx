<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="ValuationEdit.aspx.cs" Inherits="Valuation_ValuationEdit" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--表单录入-->
    <div class="dv_information">
        <table class="GridviewGray">
            
            <tr>
                <th width="100">
                    名&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;称：
                </th>
                <td>
                    <asp:Literal ID="ltlPlateName" runat="server"></asp:Literal>
                </td>
                 </tr>
            <tr>
                <th>
                    评价对象：
                </th>
                <td>
                    <cc1:DictionaryLabel ID="lblObjectTypeName" DictionaryType="Evaluation_b_ObjectType" runat="server" />
                </td>
            </tr>
            <tr>
                <th>
                    状&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;态：
                </th>
                <td>
                    <cc1:DictionaryRadioButtonList runat="server" ID="rblIsUse" DictionaryType='Dic_Status' />
                </td>
            </tr>
            <tr>
                <th>
                   限制评论次数：
                </th>
                <td>
                    <asp:CheckBox ID="cbxIsRepeat" runat="server" 
                        oncheckedchanged="cbxIsRepeat_CheckedChanged" AutoPostBack="true" /><asp:TextBox ID="txtMaxRepeat" runat="server" MaxLength="5"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                   允许查看结果：
                </th>
                <td>
                     <cc1:DictionaryRadioButtonList runat="server" ID="rblIsViewResult" DictionaryType='Dic_TrueOrFalseBool' />
                </td>
            </tr>

            <tr>
                <th>
                   含其他评价：
                </th>
                <td>
                    <asp:CheckBox ID="cbxIsOther" runat="server" 
                        oncheckedchanged="cbxIsOther_CheckedChanged" AutoPostBack="true" /><asp:TextBox ID="txtOtherTitle" runat="server" MaxLength="50" CssClass=""></asp:TextBox>
                </td>
            </tr>
            

            <tr>
                <th style="vertical-align:text-top;">评 价 项：
                </th>
                <td>
                    <asp:TextBox ID="txtItems" runat="server" CssClass="inputbox_area300" TextMode="MultiLine"></asp:TextBox>
                    <br />
                    评价量表中的评价项统一采用五星制，分别对应：差（1分）、较差（2分）、一般（3分）、较好（4分）、好（5分）。多个评价项请以“;”分隔。
                </td>
            </tr>
        </table>
       
    </div>
     <!--提交表单-->
        <div class="dv_submit">
           <asp:LinkButton ID="lbtnSave" runat="server" CssClass="btn_Save" OnClick="lbtnSave_Click"
        ValidationGroup="Error">保存</asp:LinkButton>
            <a href="javascript:closeWindow();" class="btn_Cancel padleft10">取消</a>
        </div>
</asp:Content>
