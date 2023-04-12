<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="AskApplyOption.aspx.cs" Inherits="TraningImplement_AskApplyAudit_AskApplyOption" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--功能标题-->
    <h2 class="dv_title">
        审核
    </h2>
    <!--表单录入-->
    <div class="dv_information">
        <table class="GridviewGray">
            <tr>
                <th>
                    审核意见
                </th>
                <td>
                    <asp:TextBox TextMode="MultiLine" ID="txtOption" runat="server" CssClass="inputbox_area300 inputbox_area349"  />
                </td>
            </tr>          
        </table>
    </div>
    <div class="dv_submit">
        <asp:Button ID="btnSave" runat="server" CssClass="btn_Save" Text="保存" OnClick="btnSave_Click" />
        <input type="button" class="btn_Cancel" value="取消" onclick="javascript:closeWindow();" />
    </div>
</asp:Content>
