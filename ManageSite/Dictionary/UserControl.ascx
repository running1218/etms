<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserControl.ascx.cs" Inherits="Dictionary_UserControl" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<div class="dv_information">
    <div class="dv_searchlist">
        <!-- 数据呈现区域 -->
        <cc1:CustomGridView ID="GridViewList" runat="server" AutoGenerateColumns="False"
            DataKeyNames="ColumnCodeValue,ColumnNameValue">
            <Columns>
                <asp:BoundField HeaderText="编码" DataField="ColumnCodeValue" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft"  />
                <asp:BoundField HeaderText="名称" DataField="ColumnNameValue"  HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft" />
                <asp:TemplateField ShowHeader="False" HeaderText="操作" HeaderStyle-Width="80px">
                    <ItemTemplate>
                        <cc1:CustomLinkButton runat="server" ID="lkbtnEdit" CommandArgument='<%# Container.DataItemIndex %>'
                            Text="修改" CommandName="Edit1" OnCommand="lkbtnEdit_Command"></cc1:CustomLinkButton>
                        <cc1:CustomLinkButton runat="server" ID="lkbtnDelete" CommandArgument='<%# Container.DataItemIndex %>'
                            Text="删除" CommandName="Delete1" EnableConfirm="true" ConfirmMessage="确认要删除？"
                            OnCommand="lkbtnEdit_Command"></cc1:CustomLinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </cc1:CustomGridView>
        <!--列表 end-->
        <div class="dv_splitLine">
        </div>
    </div>
    <table style="width: 600px">
        <tr>
            <th>
                编码：
            </th>
            <td>
                <asp:TextBox ID="ColumnCode" runat="server"></asp:TextBox>
                <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidatorLoginName"
                    Text="&nbsp;" Display="None" runat="server" ErrorMessage="请填写编码！" ControlToValidate="ColumnCode"
                    ValidationGroup="Edit"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                名称：
            </th>
            <td>
                <asp:TextBox ID="ColumnName" runat="server"></asp:TextBox>
                <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                    Text="&nbsp;" Display="None" runat="server" ErrorMessage="请填写名称！" ControlToValidate="ColumnName"
                    ValidationGroup="Edit"></asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
</div>
<div class="dv_submit">
    <asp:Button ID="Button1" runat="server" Text="保存" SkinID="Ok" OnClick="Button1_Click"
        ValidationGroup="Edit" />
</div>
