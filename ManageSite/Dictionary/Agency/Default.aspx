<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Agency_Default" %>

<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc1" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--功能标题-->
    <h2 class="dv_title">
        <asp:Label runat="server" ID="Label1"></asp:Label>字典管理
    </h2>
    <div class="dv_searchlist">
        <!-- 数据呈现区域 -->
        <cc1:CustomGridView ID="GridViewList" runat="server" AutoGenerateColumns="False" PageSize="10000"
            DataKeyNames="AgencyID,AgencyCode,AgencyName">
            <Columns>             
                <asp:BoundField HeaderText="编码" DataField="AgencyCode" ItemStyle-HorizontalAlign="Center"  HeaderStyle-CssClass="alignleft field12" ItemStyle-CssClass="alignleft"/>
                <asp:BoundField HeaderText="名称" DataField="AgencyName" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft" />              
                <asp:TemplateField ShowHeader="False" HeaderText="操作" HeaderStyle-Width="180">
                    <ItemTemplate>                        
                        <a href='<%# this.ActionHref(string.Format("AgencyCourseList.aspx?AgencyID={0}",Eval("AgencyID").ToString())) %>'>代理课程</a>
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
    <div class="dv_information">
        <table class="GridviewGray" style="width:750px">
            <tr>
                <th>
                    编码：
                </th>
                <td>                    
                    <asp:TextBox ID="ColumnCode" runat="server"></asp:TextBox>
                    <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidatorLoginName"
                        Text="&nbsp;" Display="None" runat="server" ErrorMessage="请填写编码！" ControlToValidate="ColumnCode"
                        ValidationGroup="Edit"></asp:RequiredFieldValidator>
                    <asp:HiddenField ID="hfID" runat="server" />
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
        <div class="dv_submit">
            <asp:Button ID="Button1" runat="server" Text="保存" SkinID="Save" OnClick="Button1_Click"
                ValidationGroup="Edit" />
        </div>
    </div>
</asp:Content>
