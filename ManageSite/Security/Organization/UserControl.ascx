<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserControl.ascx.cs" Inherits="Admin_Site_Organization_UserControl" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/MiniUpFile.ascx" TagName="uploader" TagPrefix="uc" %>
<%@ Register Assembly="ETMS.Editor" Namespace="ETMS.Editor.UEditor" TagPrefix="wuc" %>
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
        <table style="margin-bottom: 20px;" class="GridviewGray">
            <tr class="hide">
                <th style="text-align: right;" width="120">
                    �ϼ��������ƣ�
                </th>
                <td colspan="3">
                    <asp:Label ID="lblParentName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="hide">
                <th style="text-align: right;">
                    �ϼ��������룺
                </th>
                <td colspan="3">
                    <asp:Label ID="lblParentCode" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="text-align: right;" width="120">
                    �������ƣ�
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtOrgName" runat="server" SkinID="Text300" onchange="checkTextValue(this);"></asp:TextBox>
                    <%if (!IsSelfChangeOperate)
                      { %><span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidatorLoginName"
                          Text="&nbsp;" Display="None" runat="server" ErrorMessage="����д�������ƣ�" ControlToValidate="txtOrgName"
                          ValidationGroup="Edit"></asp:RequiredFieldValidator><%} %>
                </td>
            </tr>
            <tr>
                <th style="text-align: right;">
                    �������룺
                </th>
                <td>
                    <asp:TextBox ID="txtOrgCode" runat="server"></asp:TextBox>
                    <%if (!IsSelfChangeOperate)
                      { %><span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                          Text="&nbsp;" Display="None" runat="server" ErrorMessage="����д�������룡" ControlToValidate="txtOrgCode"
                          ValidationGroup="Edit"></asp:RequiredFieldValidator><%} %>
                </td>
                 <th style="text-align: right;" width="120">
                    ������
                </th>
                <td>
                    <asp:TextBox ID="txtDomain" runat="server" SkinID="Text300" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="text-align: right;">
                    ����ͼ�꣺
                </th>
                <td colspan="3">
                    <asp:Image runat="server" ID="Image1" Style="width: 120px; height: 70px; margin-bottom: 5px;
                        border: 1px solid;" />
                    <uc:uploader ID="uploader" runat="server" FunctionType="OrgLogo" CallBack="doCallBack" FileTypeIsDisplay="false" />
                    <script type="text/javascript">
                        function doCallBack(imgName, imgUrl, imgSize) {
                            document.getElementById('<%=Image1.ClientID %>').src = imgUrl;
                        }
                    </script>
                    <span class="upload-img-standard">֧��png͸����ɫͼƬ����ѳߴ�Ϊ150��50����</span>
                </td>
            </tr>
            <tr>
                <th style="text-align: right;">
                    ��ʼʱ�䣺
                </th>
                <td>
                    <cc1:DateTimeTextBox ID="txtEstablishTime" runat="server"></cc1:DateTimeTextBox>
                </td>
                <th style="text-align: right;">
                    ѧԱ���ޣ�
                </th>
                <td>
                    <cc1:CustomTextBox ID="txtStudentNum" runat="server" ContentType="Number"></cc1:CustomTextBox>
                    <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                          Text="&nbsp;" Display="None" runat="server" ErrorMessage="����дѧԱ���ޣ�" ControlToValidate="txtStudentNum"
                          ValidationGroup="Edit"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th style="text-align: right;">
                    ��ϵ��ַ��
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtAddress" runat="server" SkinID="Text300"></asp:TextBox>
                    <%-- <span style="color: Red;">*</span><asp:RequiredFieldValidator  Display="None" 
                        ID="RequiredFieldValidatorRealName" runat="server" ErrorMessage="����д��" ControlToValidate="txtRealName"
                        ValidationGroup="Edit"></asp:RequiredFieldValidator> --%>
                </td>
            </tr>
            <tr>
                <th style="text-align: right;">
                    �������룺
                </th>
                <td>
                    <asp:TextBox ID="txtPostCode" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ValidationExpression="\d{6}" ID="RegularExpressionValidator3"
                        runat="server" ErrorMessage="����ȷ��д�������룡" ControlToValidate="txtPostCode" Display="None"
                        ValidationGroup="Edit"></asp:RegularExpressionValidator>
                </td>
                <th style="text-align: right;">
                    �������䣺
                </th>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        ID="RegularExpressionValidator4" runat="server" ErrorMessage="����ȷ��д�������䣡" ControlToValidate="txtEmail"
                        Display="None" ValidationGroup="Edit"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <th style="text-align: right;">
                    ������������
                </th>
                <td>
                    <asp:TextBox ID="txtManager" runat="server"></asp:TextBox>
                </td>
                <th style="text-align: right;">
                    ��ϵ�绰��
                </th>
                <td>
                    <asp:TextBox ID="txtTelphone" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="text-align: right;">
                    �ƶ��绰��
                </th>
                <td>
                    <asp:TextBox ID="txtMobilePhone" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ValidationExpression="\d{11}" ID="RegularExpressionValidator6"
                        Display="None" runat="server" ErrorMessage="����ȷ��д�ƶ��绰��" ControlToValidate="txtMobilePhone"
                        ValidationGroup="Edit"></asp:RegularExpressionValidator>
                </td>
                <th style="text-align: right;">
                    ����绰��
                </th>
                <td>
                    <asp:TextBox ID="txtFax" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="text-align: right;">
                    ��ѵ�����ˣ�
                </th>
                <td>
                    <asp:TextBox ID="txtTrainer" runat="server"></asp:TextBox>
                </td>
                <th style="text-align: right;">
                    ��ѵ�����˵绰��
                </th>
                <td>
                    <asp:TextBox ID="txtTrainerTelphonePhone" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="text-align: right;">
                    ��ѵ���������䣺
                </th>
                <%if (IsSelfChangeOperate)
                  { %>
                <td colspan="3">
                    <%}
                  else
                  { %>
                    <td>
                        <%} %>
                        <asp:TextBox ID="txtTrainerEmail" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            ID="RegularExpressionValidator2" runat="server" ErrorMessage="����ȷ��д��ѵ���������䣡"
                            ControlToValidate="txtTrainerEmail" Display="None" ValidationGroup="Edit"></asp:RegularExpressionValidator>
                    </td>
                    <%if (!IsSelfChangeOperate)
                      { %>
                    <th style="text-align: right;">
                        ��ʾ˳��
                    </th>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlOrder">
                        </asp:DropDownList>
                    </td>
                    <%} %>
            </tr>
        </table>              
        <asp:ValidationSummary runat="server" ID="validationSummary1" ValidationGroup="Edit"
            ShowMessageBox="true" ShowSummary="false" />
    </asp:View>
    <asp:View runat="server" ID="View_Browse">
        <table class="GridviewGray">
            <tr>
                <th width="120px">
                    ����ͼ�꣺
                </th>
                <td colspan="3">
                    <asp:Image runat="server" ID="imgLogo" Style="width: 120px; height: 70px; border: 1px solid;" />
                </td>
            </tr>
            <tr>
                <th>
                    �������ƣ�
                </th>
                <td>
                    <asp:Label runat="server" ID="lblOrgName" />
                </td>
                <th>
                    ����������
                </th>
                <td>
                    <asp:Label runat="server" ID="lblDomain" />
                </td>
            </tr>
            <tr>
                <th>
                    �������룺
                </th>
                <td>
                    <asp:Label runat="server" ID="lblOrgCode" />
                </td>
                <th width="120px">
                    ��ʼʱ�䣺
                </th>
                <td>
                    <asp:Label ID="lblEstablishTime" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    ����״̬��
                </th>
                <td>
                    <asp:Label runat="server" ID="lblStatus" />
                </td>
                <th width="120px">
                     ѧԱ���ޣ�
                </th>
                <td>
                    <asp:Label ID="lblStudentNum" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    ��ϵ��ַ��
                </th>
                <td colspan="3">
                    <asp:Label ID="lblAddress" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    �������룺
                </th>
                <td>
                    <asp:Label ID="lblPostCode" runat="server"></asp:Label>
                </td>
                <th>
                    �������䣺
                </th>
                <td>
                    <asp:Label ID="lblEmail" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    ������������
                </th>
                <td>
                    <asp:Label ID="lblManager" runat="server"></asp:Label>
                </td>
                <th>
                    ��ϵ�绰��
                </th>
                <td>
                    <asp:Label ID="lblTelphone" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    �ƶ��绰��
                </th>
                <td>
                    <asp:Label ID="lblMobilePhone" runat="server"></asp:Label>
                </td>
                <th>
                    ����绰��
                </th>
                <td>
                    <asp:Label ID="lblFax" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    ��ѵ�����ˣ�
                </th>
                <td>
                    <asp:Label ID="lblTrainer" runat="server"></asp:Label>
                </td>
                <th>
                    ��ѵ�����˵绰��
                </th>
                <td>
                    <asp:Label ID="lblTrainerTelphonePhone" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    ��ѵ���������䣺
                </th>
                <td colspan="3">
                    <asp:Label ID="lblTrainerEmail" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </asp:View>
</cc1:CustomMuliView>
