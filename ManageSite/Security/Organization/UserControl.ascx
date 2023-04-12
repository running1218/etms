<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserControl.ascx.cs" Inherits="Admin_Site_Organization_UserControl" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/MiniUpFile.ascx" TagName="uploader" TagPrefix="uc" %>
<%@ Register Assembly="ETMS.Editor" Namespace="ETMS.Editor.UEditor" TagPrefix="wuc" %>
<script type="text/javascript" language="javascript">
    function checkTextValue(obj) {
        var str_text = $.trim($(obj).val());
        var correct = vaildNumber(str_text);
        if (correct > 0) {
            popAlertMsg("输入了非法字符‘／’！", "提示");
            obj.focus();
            $(obj).val("");
        }

    };
    function vaildNumber(str1) {
        var patrn = "/";
        var patrn1 = "／";
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
                    上级机构名称：
                </th>
                <td colspan="3">
                    <asp:Label ID="lblParentName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="hide">
                <th style="text-align: right;">
                    上级机构编码：
                </th>
                <td colspan="3">
                    <asp:Label ID="lblParentCode" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="text-align: right;" width="120">
                    机构名称：
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtOrgName" runat="server" SkinID="Text300" onchange="checkTextValue(this);"></asp:TextBox>
                    <%if (!IsSelfChangeOperate)
                      { %><span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidatorLoginName"
                          Text="&nbsp;" Display="None" runat="server" ErrorMessage="请填写机构名称！" ControlToValidate="txtOrgName"
                          ValidationGroup="Edit"></asp:RequiredFieldValidator><%} %>
                </td>
            </tr>
            <tr>
                <th style="text-align: right;">
                    机构编码：
                </th>
                <td>
                    <asp:TextBox ID="txtOrgCode" runat="server"></asp:TextBox>
                    <%if (!IsSelfChangeOperate)
                      { %><span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                          Text="&nbsp;" Display="None" runat="server" ErrorMessage="请填写机构编码！" ControlToValidate="txtOrgCode"
                          ValidationGroup="Edit"></asp:RequiredFieldValidator><%} %>
                </td>
                 <th style="text-align: right;" width="120">
                    域名：
                </th>
                <td>
                    <asp:TextBox ID="txtDomain" runat="server" SkinID="Text300" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="text-align: right;">
                    机构图标：
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
                    <span class="upload-img-standard">支持png透明底色图片，最佳尺寸为150×50像素</span>
                </td>
            </tr>
            <tr>
                <th style="text-align: right;">
                    开始时间：
                </th>
                <td>
                    <cc1:DateTimeTextBox ID="txtEstablishTime" runat="server"></cc1:DateTimeTextBox>
                </td>
                <th style="text-align: right;">
                    学员上限：
                </th>
                <td>
                    <cc1:CustomTextBox ID="txtStudentNum" runat="server" ContentType="Number"></cc1:CustomTextBox>
                    <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                          Text="&nbsp;" Display="None" runat="server" ErrorMessage="请填写学员上限！" ControlToValidate="txtStudentNum"
                          ValidationGroup="Edit"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th style="text-align: right;">
                    联系地址：
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtAddress" runat="server" SkinID="Text300"></asp:TextBox>
                    <%-- <span style="color: Red;">*</span><asp:RequiredFieldValidator  Display="None" 
                        ID="RequiredFieldValidatorRealName" runat="server" ErrorMessage="请填写！" ControlToValidate="txtRealName"
                        ValidationGroup="Edit"></asp:RequiredFieldValidator> --%>
                </td>
            </tr>
            <tr>
                <th style="text-align: right;">
                    邮政编码：
                </th>
                <td>
                    <asp:TextBox ID="txtPostCode" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ValidationExpression="\d{6}" ID="RegularExpressionValidator3"
                        runat="server" ErrorMessage="请正确填写邮政编码！" ControlToValidate="txtPostCode" Display="None"
                        ValidationGroup="Edit"></asp:RegularExpressionValidator>
                </td>
                <th style="text-align: right;">
                    电子邮箱：
                </th>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        ID="RegularExpressionValidator4" runat="server" ErrorMessage="请正确填写电子邮箱！" ControlToValidate="txtEmail"
                        Display="None" ValidationGroup="Edit"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <th style="text-align: right;">
                    负责人姓名：
                </th>
                <td>
                    <asp:TextBox ID="txtManager" runat="server"></asp:TextBox>
                </td>
                <th style="text-align: right;">
                    联系电话：
                </th>
                <td>
                    <asp:TextBox ID="txtTelphone" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="text-align: right;">
                    移动电话：
                </th>
                <td>
                    <asp:TextBox ID="txtMobilePhone" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ValidationExpression="\d{11}" ID="RegularExpressionValidator6"
                        Display="None" runat="server" ErrorMessage="请正确填写移动电话！" ControlToValidate="txtMobilePhone"
                        ValidationGroup="Edit"></asp:RegularExpressionValidator>
                </td>
                <th style="text-align: right;">
                    传真电话：
                </th>
                <td>
                    <asp:TextBox ID="txtFax" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="text-align: right;">
                    培训负责人：
                </th>
                <td>
                    <asp:TextBox ID="txtTrainer" runat="server"></asp:TextBox>
                </td>
                <th style="text-align: right;">
                    培训负责人电话：
                </th>
                <td>
                    <asp:TextBox ID="txtTrainerTelphonePhone" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="text-align: right;">
                    培训负责人邮箱：
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
                            ID="RegularExpressionValidator2" runat="server" ErrorMessage="请正确填写培训负责人邮箱！"
                            ControlToValidate="txtTrainerEmail" Display="None" ValidationGroup="Edit"></asp:RegularExpressionValidator>
                    </td>
                    <%if (!IsSelfChangeOperate)
                      { %>
                    <th style="text-align: right;">
                        显示顺序：
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
                    机构图标：
                </th>
                <td colspan="3">
                    <asp:Image runat="server" ID="imgLogo" Style="width: 120px; height: 70px; border: 1px solid;" />
                </td>
            </tr>
            <tr>
                <th>
                    机构名称：
                </th>
                <td>
                    <asp:Label runat="server" ID="lblOrgName" />
                </td>
                <th>
                    机构域名：
                </th>
                <td>
                    <asp:Label runat="server" ID="lblDomain" />
                </td>
            </tr>
            <tr>
                <th>
                    机构编码：
                </th>
                <td>
                    <asp:Label runat="server" ID="lblOrgCode" />
                </td>
                <th width="120px">
                    开始时间：
                </th>
                <td>
                    <asp:Label ID="lblEstablishTime" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    机构状态：
                </th>
                <td>
                    <asp:Label runat="server" ID="lblStatus" />
                </td>
                <th width="120px">
                     学员上限：
                </th>
                <td>
                    <asp:Label ID="lblStudentNum" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    联系地址：
                </th>
                <td colspan="3">
                    <asp:Label ID="lblAddress" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    邮政编码：
                </th>
                <td>
                    <asp:Label ID="lblPostCode" runat="server"></asp:Label>
                </td>
                <th>
                    电子邮箱：
                </th>
                <td>
                    <asp:Label ID="lblEmail" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    负责人姓名：
                </th>
                <td>
                    <asp:Label ID="lblManager" runat="server"></asp:Label>
                </td>
                <th>
                    联系电话：
                </th>
                <td>
                    <asp:Label ID="lblTelphone" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    移动电话：
                </th>
                <td>
                    <asp:Label ID="lblMobilePhone" runat="server"></asp:Label>
                </td>
                <th>
                    传真电话：
                </th>
                <td>
                    <asp:Label ID="lblFax" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    培训负责人：
                </th>
                <td>
                    <asp:Label ID="lblTrainer" runat="server"></asp:Label>
                </td>
                <th>
                    培训负责人电话：
                </th>
                <td>
                    <asp:Label ID="lblTrainerTelphonePhone" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    培训负责人邮箱：
                </th>
                <td colspan="3">
                    <asp:Label ID="lblTrainerEmail" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </asp:View>
</cc1:CustomMuliView>
