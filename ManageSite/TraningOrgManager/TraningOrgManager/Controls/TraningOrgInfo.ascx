<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TraningOrgInfo.ascx.cs"
    Inherits="TraningOrgManager_TraningOrgManager_Controls_TraningOrgInfo" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<div class="dv_information">
    <table class="GridviewGray">
        <tr>
            <th width="20%">
                机构名称：
            </th>
            <td colspan="3">
                <asp:TextBox ID="txtOuterOrgName" runat="server" CssClass="inputbox_210" MaxLength="50"></asp:TextBox>
                <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidatorOuterOrgName"
                    runat="server" ErrorMessage="请填写培训机构名称！" ControlToValidate="txtOuterOrgName"
                    ValidationGroup="Error" Display="None"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th width="20%">
                机构编码：
            </th>
            <td width="30%">
                <asp:TextBox ID="txtOuterOrgCode" runat="server" CssClass="inputbox_120" MaxLength="20"></asp:TextBox>
                <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidatorOuterOrgCode"
                    runat="server" ErrorMessage="请填写培训机构编码！" ControlToValidate="txtOuterOrgCode"
                    ValidationGroup="Error" Display="None"></asp:RequiredFieldValidator>
            </td>
            <th width="20%">
                联&nbsp;&nbsp;系&nbsp;&nbsp;人：
            </th>
            <td width="30%">
                <asp:TextBox ID="txtLinkMan" runat="server" CssClass="inputbox_120" MaxLength="20"></asp:TextBox>
                <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                    runat="server" ErrorMessage="请填写联系人！" ControlToValidate="txtLinkMan" ValidationGroup="Error"
                    Display="None"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                邮&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;箱：
            </th>
            <td>
                <asp:TextBox ID="txtEMAIL" runat="server" CssClass="inputbox_120" MaxLength="60"></asp:TextBox>
                <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                    runat="server" ErrorMessage="请填写邮箱！" ControlToValidate="txtEMAIL" ValidationGroup="Error"
                    Display="None"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtEMAIL"
                    Display="None" ErrorMessage="邮箱格式错误！" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    ValidationGroup="Error"></asp:RegularExpressionValidator>
            </td>
            <th>
                联系方式：
            </th>
            <td>
                <asp:TextBox ID="txtLinkMode" runat="server" CssClass="inputbox_120" MaxLength="50"></asp:TextBox>
                <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                    runat="server" ErrorMessage="请填写联系方式！" ControlToValidate="txtLinkMode" ValidationGroup="Error"
                    Display="None"></asp:RequiredFieldValidator>                   
            </td>
        </tr>
        <tr>
            <th>
                状&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;态：
            </th>
            <td colspan="3">
                <cc1:DictionaryRadioButtonList runat="server" ID="Dic_Status" DictionaryType='Dic_Status' />
            </td>
        </tr>
        <tr>
           <th>
                公司地址：
            </th>
            <td colspan="3">
                <asp:TextBox ID="txtAddress" runat="server" CssClass="inputbox_300" MaxLength="60"></asp:TextBox>
            </td>            
        </tr>
        <tr>
           <th>
                公司网址：
            </th>
            <td colspan="3">
                <asp:TextBox ID="txtHttp" runat="server" CssClass="inputbox_300" MaxLength="60"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                服务内容：
            </th>
            <td colspan="3">
                <asp:TextBox ID="txtServiceContent" TextMode="MultiLine" runat="server" CssClass="inputbox_area600"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                精品课程：
            </th>
            <td colspan="3">
                <asp:TextBox ID="txtBestClass" TextMode="MultiLine" runat="server" CssClass="inputbox_area600"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                机构评价：
            </th>
            <td colspan="3">
                <asp:TextBox ID="txtOrgAssess" TextMode="MultiLine" runat="server" CssClass="inputbox_area600"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                历史合作：
            </th>
            <td colspan="3">
                <asp:TextBox ID="txtHistoryCooperation" TextMode="MultiLine" runat="server" CssClass="inputbox_area600"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                合同模板：
            </th>
            <td colspan="3">
                <asp:Label ID="lblFilePath" runat="server"></asp:Label><br />
                <cc1:FileUpload runat="server" ID="fileUpload1" FunctionType="OfflineJob" CallBack="callback" />
                <script language="javascript">
                    function callback(imgName, imgUrl) {
                        $('#<%=lblFilePath.ClientID%>').html("<a href='" + imgUrl + "' target='_blank'>" + imgName + "</a>");
                    }
                </script>
            </td>
        </tr>
        <tr>
            <th>
                备注：
            </th>
            <td colspan="3">
                <asp:TextBox ID="txtRemark" TextMode="MultiLine" runat="server" CssClass="inputbox_area600"></asp:TextBox>
            </td>
        </tr>
    </table>
</div>
<div class="dv_submit">
    <asp:Button ID="lbnSave" Text="保存" runat="server" CssClass="btn_Save" OnClick="lbnSave_Click"
        ValidationGroup="Error"></asp:Button>
    <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Error"
        ShowMessageBox="true" ShowSummary="false" />
    <input type="button" class="btn_Cancel" value="取消" onclick="javascript:closeWindow();" /></div>
