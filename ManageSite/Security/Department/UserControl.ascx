<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserControl.ascx.cs" Inherits="Admin_Site_Department_UserControl" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<script type="text/javascript" language="javascript">
    function checkTextValue(obj) {
        var str_text = $.trim($(obj).val());
        var correct = vaildNumber(str_text);
        if (correct > 0) {
            popAlertMsg("�����˷Ƿ��ַ���������", "��ʾ");
            obj.focus();
            $(obj).val("");
        }

    };
    function vaildNumber(str1) {
        var patrn = "/";
        var patrn1 = "��";
        var s = str1.indexOf(patrn);
        var s1 = str1.indexOf(patrn1);
        var result = (s == -1 && s1 == -1 ? -1 : 1);
        return (result);
    }
</script>
<cc1:CustomMuliView runat="server" ID="Views">
    <asp:View runat="server" ID="View_Edit">
        <table class="GridviewGray">
            <tr>
                <th>
                    �ϼ�<asp:Literal ID="ltlDepartment5" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>���ƣ�
                </th>
                <td colspan="3">
                    <asp:Label ID="lblParentName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    �ϼ�<asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>���룺
                </th>
                <td colspan="3">
                    <asp:Label ID="lblParentCode" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltlDepartment4" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>��
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtDepartmentName" runat="server" SkinID="Text190" MaxLength="50" onchange="checkTextValue(this);"></asp:TextBox><span
                        style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidatorLoginName"
                            Text="&nbsp;" Display="None" runat="server" ErrorMessage="����д���ƣ�" ControlToValidate="txtDepartmentName"
                            ValidationGroup="Edit"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr style="display:none">
                <th>
                    <asp:Literal ID="ltlDepartment3" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>���룺
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtDepartmentCode" runat="server" MaxLength="20" Enabled="false"></asp:TextBox>(�Զ�����)
                </td>
            </tr>
            <tr>
                <th>
                    ������������
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtManager" runat="server" MaxLength="20"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    ��ʾ˳��
                </th>
                <td colspan="3">
                    <asp:DropDownList runat="server" ID="ddlOrder">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>
                    ��ע��
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" SkinID="textarea"></asp:TextBox>
                </td>
            </tr>
        </table>
        <asp:ValidationSummary runat="server" ID="validationSummary1" ValidationGroup="Edit"
            ShowMessageBox="true" ShowSummary="false" />
    </asp:View>
    <asp:View runat="server" ID="View_Browse">
        <table class="GridviewGray">
            <tr>
                <th>
                    <asp:Literal ID="ltlDepartment1" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>���ƣ�
                </th>
                <td>
                    <asp:Label runat="server" ID="lblDepartmentName" />
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltlDepartment2" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>���룺
                </th>
                <td>
                    <asp:Label runat="server" ID="lblDepartmentCode" />
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltlDepartment" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>״̬��
                </th>
                <td>
                    <asp:Label runat="server" ID="lblStatus" />
                </td>
            </tr>
            <tr>
                <th>
                    �����ˣ�
                </th>
                <td>
                    <asp:Label ID="lblManager" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    ��ע��
                </th>
                <td>
                    <asp:Label ID="lblDescription" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </asp:View>
</cc1:CustomMuliView>
