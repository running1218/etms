<%@ Page Language="C#" AutoEventWireup="true" Inherits="Admin_InnerRole_View" MasterPageFile="~/MasterPages/MPagePop.Master"
    CodeFile="View.aspx.cs" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="../Controls/TreeNavigationTool.ascx" TagName="TreeNavigationTool"
    TagPrefix="uc1" %>
<asp:Content runat="server" ID="PageContent" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:ObjectDataSource ID="ObjectDataSourceSite_Role" runat="server" DataObjectTypeName="ETMS.Components.Basic.API.Entity.Security.Role"
        DeleteMethod="Remove" InsertMethod="Save" SelectMethod="GetNodeByID" UpdateMethod="Save"
        TypeName="ETMS.Components.Basic.Implement.BLL.Security.RoleLogic" OnInserting="ObjectDataSourceSite_Role_Inserting"
        OnUpdating="ObjectDataSourceSite_Role_Updating" OnInserted="ObjectDataSourceSite_Role_Save"
        OnUpdated="ObjectDataSourceSite_Role_Save" OnDeleted="ObjectDataSourceSite_Role_Save"
        OnDeleting="ObjectDataSourceSite_Role_Deleting">
        <SelectParameters>
            <asp:QueryStringParameter Name="NodeID" QueryStringField="id" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:DetailsView runat="server" ID="dvRole" DataSourceID="ObjectDataSourceSite_Role"
        OnDataBound="dvRole_DataBound">
        <Fields>
            <asp:TemplateField HeaderText="角色ID：" SortExpression="NodeID" InsertVisible="false"
                HeaderStyle-Font-Bold="true">
                <ItemTemplate>
                    <asp:Label ID="LabelRoleID" runat="server" Text='<%# Bind("NodeID") %>'></asp:Label>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("NodeCode") %>' Visible="false"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="角色名称：" SortExpression="NodeName" HeaderStyle-Font-Bold="true">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxRoleName" runat="server" Text='<%# Bind("NodeName") %>' MaxLength="25"
                        Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvTextBoxRoleName" runat="server" Text="*" ErrorMessage="*请填写角色名称"
                        Display="dynamic" ValidationGroup="valids" ControlToValidate="TextBoxRoleName"></asp:RequiredFieldValidator>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBoxRoleName" runat="server" Text='<%# Bind("NodeName") %>' MaxLength="25"
                        Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvTextBoxRoleName" runat="server" Text="*" ErrorMessage="*请填写角色名称"
                        Display="dynamic" ValidationGroup="valids" ControlToValidate="TextBoxRoleName"></asp:RequiredFieldValidator>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="LabelRoleName" runat="server" Text='<%# Bind("NodeName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="角色状态：" SortExpression="State" HeaderStyle-Font-Bold="true">
                <ItemTemplate>
                    <asp:RadioButtonList runat="server" ID="rbtnlistState" RepeatDirection="horizontal"
                        RepeatLayout="Flow" SelectedValue='<%# Bind("State")%>'>
                        <asp:ListItem Text="启用" Value="1" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="禁用" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="角色描述：" SortExpression="Description" HeaderStyle-Font-Bold="true">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxDescription" runat="server" Text='<%# Bind("Description") %>'
                        TextMode="multiLine" SkinID="textarea"></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBoxDescription" runat="server" Text='<%# Bind("Description") %>'
                        TextMode="multiLine" SkinID="textarea"></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="LabelDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="创建者：" SortExpression="Creator" InsertVisible="False"
                HeaderStyle-Font-Bold="true">
                <ItemTemplate>
                    <asp:Label ID="LabelCreator" runat="server" Text='<%# Bind("Creator") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="创建时间：" SortExpression="CreateTime" InsertVisible="False"
                HeaderStyle-Font-Bold="true">
                <ItemTemplate>
                    <asp:Label ID="LabelCreateTime" runat="server" Text='<%# Bind("CreateTime") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="修改者：" SortExpression="Modifier" InsertVisible="False"
                HeaderStyle-Font-Bold="true">
                <ItemTemplate>
                    <asp:Label ID="LabelModifier" runat="server" Text='<%# Bind("Modifier") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="修改时间：" SortExpression="ModifyTime" InsertVisible="False"
                HeaderStyle-Font-Bold="true">
                <ItemTemplate>
                    <asp:Label ID="LabelModifyTime" runat="server" Text='<%# Bind("ModifyTime") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Fields>
        <FooterTemplate>
            <div class="dv_submit">
                <asp:Button ID="ImageButtonInsertOk" runat="server" SkinID="Insert" CommandName="Insert"
                    ValidationGroup="valids" />
                <asp:Button ID="ImageButtonUpdateOk" runat="server" SkinID="Edit" CommandName="Update"
                    ValidationGroup="valids" Visible="false" />
                <cc1:CustomButton EnableConfirm="true" ConfirmTitle="删除提示" ConfirmMessage="确定要删除吗？"
                    ID="ImageButtonDeleteOk" runat="server" SkinID="DeleteOk" CommandName="Delete"
                    ValidationGroup="valids" Visible="false" />
                <asp:Button ID="ImageButtonReturn" runat="server" SkinID="Return" OnClientClick="closeWindow()"
                    CausesValidation="False" />
                <asp:ValidationSummary runat="server" ID="validatiomSummary" ValidationGroup="valids"
                    ShowMessageBox="true" ShowSummary="false" />
            </div>
        </FooterTemplate>
    </asp:DetailsView>
</asp:Content>
