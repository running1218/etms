<%@ Page Language="C#" AutoEventWireup="true" Inherits="Admin_Role_View" MasterPageFile="~/MasterPages/MPagePop.Master"
    CodeFile="View.aspx.cs" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<asp:Content runat="server" ID="PageContent" ContentPlaceHolderID="ContentPlaceHolder1">
    <h2 class="dv_title">
        <!-- 功能描述 -->
        功能组：<%=this.Operator %>
    </h2>
    <asp:ObjectDataSource ID="ObjectDataSourceSite_Role" runat="server" DataObjectTypeName="ETMS.Components.Basic.API.Entity.Security.FunctionGroup"
        DeleteMethod="Remove" InsertMethod="Save" SelectMethod="GetNodeByID" UpdateMethod="Save"
        TypeName="ETMS.Components.Basic.Implement.BLL.Security.FunctionGroupLogic" OnInserting="ObjectDataSourceSite_Role_Inserting"
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
            <asp:TemplateField HeaderText="功能组ID：" SortExpression="NodeID" InsertVisible="false"
                HeaderStyle-Font-Bold="true">
                <ItemTemplate>
                    <asp:Label ID="LabelRoleID" runat="server" Text='<%# Bind("NodeID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="功能组名称：" SortExpression="NodeName" HeaderStyle-Font-Bold="true">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxRoleName" runat="server" Text='<%# Bind("NodeName") %>' MaxLength="25"
                        Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvTextBoxRoleName" runat="server" Text="*" ErrorMessage="*请填写功能组名称"
                        Display="dynamic" ValidationGroup="valids" ControlToValidate="TextBoxRoleName"></asp:RequiredFieldValidator>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBoxRoleName" runat="server" Text='<%# Bind("NodeName") %>' MaxLength="25"
                        Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvTextBoxRoleName" runat="server" Text="*" ErrorMessage="*请填写功能组名称"
                        Display="dynamic" ValidationGroup="valids" ControlToValidate="TextBoxRoleName"></asp:RequiredFieldValidator>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="LabelRoleName" runat="server" Text='<%# Bind("NodeName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="功能组编码：" SortExpression="NodeCode" HeaderStyle-Font-Bold="true">
                <ItemTemplate>
                    <asp:Label ID="LabelRoleCode" runat="server" Text='<%# Bind("NodeCode") %>'></asp:Label>
                    (系统自动生成)
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="隶属组件：" HeaderStyle-Font-Bold="true">
                <ItemTemplate>
                    <cc1:DictionaryDropDownList ID="ddlComponent" runat="server" DictionaryType="Site_Dic_Component"
                        IsShowChoose="true" SelectedValue='<%#Bind("ComponentID") %>' />
                    <br />
                    如果功能组设置了组件关系，在组件停用的时候隐藏此功能组！
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="顺序号：" SortExpression="OrderNo" HeaderStyle-Font-Bold="true">
                <ItemTemplate>
                    <asp:Label ID="lblOrderNo" runat="server" Text='<%# Bind("OrderNo") %>'></asp:Label>
                </ItemTemplate>
                <InsertItemTemplate>
                    <asp:DropDownList runat="server" ID="ddlOrderNo" SelectedValue='<%# Bind("OrderNo") %>'>
                        <asp:ListItem Text="1" Value="1" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                        <asp:ListItem Text="6" Value="6"></asp:ListItem>
                        <asp:ListItem Text="7" Value="7"></asp:ListItem>
                        <asp:ListItem Text="8" Value="8"></asp:ListItem>
                        <asp:ListItem Text="9" Value="9"></asp:ListItem>
                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                        <asp:ListItem Text="11" Value="11"></asp:ListItem>
                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                        <asp:ListItem Text="13" Value="13"></asp:ListItem>
                        <asp:ListItem Text="14" Value="14"></asp:ListItem>
                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                        <asp:ListItem Text="16" Value="16"></asp:ListItem>
                        <asp:ListItem Text="17" Value="17"></asp:ListItem>
                        <asp:ListItem Text="18" Value="18"></asp:ListItem>
                        <asp:ListItem Text="19" Value="19"></asp:ListItem>
                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                        <asp:ListItem Text="21" Value="21"></asp:ListItem>
                        <asp:ListItem Text="22" Value="22"></asp:ListItem>
                        <asp:ListItem Text="23" Value="23"></asp:ListItem>
                        <asp:ListItem Text="24" Value="24"></asp:ListItem>
                        <asp:ListItem Text="25" Value="25"></asp:ListItem>
                        <asp:ListItem Text="26" Value="26"></asp:ListItem>
                        <asp:ListItem Text="27" Value="27"></asp:ListItem>
                        <asp:ListItem Text="28" Value="28"></asp:ListItem>
                        <asp:ListItem Text="29" Value="29"></asp:ListItem>
                        <asp:ListItem Text="30" Value="30"></asp:ListItem>
                    </asp:DropDownList>
                </InsertItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList runat="server" ID="ddlOrderNo" SelectedValue='<%# Bind("OrderNo") %>'>
                        <asp:ListItem Text="1" Value="1" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                        <asp:ListItem Text="6" Value="6"></asp:ListItem>
                        <asp:ListItem Text="7" Value="7"></asp:ListItem>
                        <asp:ListItem Text="8" Value="8"></asp:ListItem>
                        <asp:ListItem Text="9" Value="9"></asp:ListItem>
                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                        <asp:ListItem Text="11" Value="11"></asp:ListItem>
                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                        <asp:ListItem Text="13" Value="13"></asp:ListItem>
                        <asp:ListItem Text="14" Value="14"></asp:ListItem>
                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                        <asp:ListItem Text="16" Value="16"></asp:ListItem>
                        <asp:ListItem Text="17" Value="17"></asp:ListItem>
                        <asp:ListItem Text="18" Value="18"></asp:ListItem>
                        <asp:ListItem Text="19" Value="19"></asp:ListItem>
                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                        <asp:ListItem Text="21" Value="21"></asp:ListItem>
                        <asp:ListItem Text="22" Value="22"></asp:ListItem>
                        <asp:ListItem Text="23" Value="23"></asp:ListItem>
                        <asp:ListItem Text="24" Value="24"></asp:ListItem>
                        <asp:ListItem Text="25" Value="25"></asp:ListItem>
                        <asp:ListItem Text="26" Value="26"></asp:ListItem>
                        <asp:ListItem Text="27" Value="27"></asp:ListItem>
                        <asp:ListItem Text="28" Value="28"></asp:ListItem>
                        <asp:ListItem Text="29" Value="29"></asp:ListItem>
                        <asp:ListItem Text="30" Value="30"></asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="功能组状态：" SortExpression="State" HeaderStyle-Font-Bold="true">
                <ItemTemplate>
                    <asp:RadioButtonList runat="server" ID="rbtnlistState" RepeatDirection="horizontal"
                        RepeatLayout="Flow" SelectedValue='<%# Bind("State")%>'>
                        <asp:ListItem Text="启用" Value="1" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="禁用" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="功能组描述：" SortExpression="Description" HeaderStyle-Font-Bold="true">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxDescription" runat="server" Text='<%# Bind("Description") %>'
                        TextMode="multiLine" SkinID="Text300"></asp:TextBox>
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
