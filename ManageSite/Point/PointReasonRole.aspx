<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="PointReasonRole.aspx.cs" Inherits="Point_PointReasonRole" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="dv_searchbox">
        <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <th width="120">
                    积分原因：
                </th>
                <td width="130">
                    <div runat="server" id="tableQueryControlList">
                        <asp:TextBox ID="txt_PointReason" runat="server"></asp:TextBox>
                    </div>
                </td>
                <th width="120">
                    积分原因类型：
                </th>
                <th width="130">
                    <asp:DropDownList ID="ddlPointReasonTypeID" runat="server" />
                </th>
                <td width="60">
                    <asp:Button ID="btnSearch" runat="server" CssClass="btn_Search" Text="查询" OnClick="btnSearch_Click" />
                    <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <th>
                    状&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;态：
                </th>
                <td>
                    <cc1:DictionaryDropDownList runat="server" ID="ddl_IsUse" DictionaryType="Dic_Status" />
                </td>
                <th>
                    积&nbsp;&nbsp;分&nbsp;&nbsp;值：
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtMinNum" runat="server" CssClass="inputbox_40" MaxLength="6" />
                    至
                    <asp:TextBox ID="txtMaxNum" runat="server" CssClass="inputbox_40" MaxLength="6" />                     
                </td>
            </tr>
        </table>
    </div>
    <div class="dv_searchlist">
        <!--翻页-->
        <div class="dv_pagePanel">
            <div class="dv_selectAll">
                <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="GridViewList"
                    Visible="false" />
                <input type="button" class="btn_Add" value="新增" onclick="javascript:showWindow('新增积分原因','<%=this.ActionHref(string.Format("PointReasonRoleInfo.aspx")) %>')" />
                <%-- <cc1:CustomButton CssClass="btn_Del" Text="删除" runat="server" ID="btnDeletes" EnableConfirm="true"
                        ConfirmMessage="确信要执行“批量删除”操作吗?" /> --%>
            </div>
            <div class="dv_pageControl">
                <uc2:PageSet ID="PageSet1" runat="server" />
            </div>
        </div>
        <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="False"
            DataKeyNames="StudentPointReasonRoleID" OnRowCommand="CustomGridView1_RowCommand"
            OnRowDataBound="CustomGridView1_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="序号" HeaderStyle-Width="40">
                    <ItemStyle HorizontalAlign="Center" Width="60" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="LabNo" runat="server" Text='<%#((this.PageSet1.PageIndex -1) * this.PageSet1.PageSize) + Container.DataItemIndex+1 %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="积分原因类型" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <%--<cc1:DictionaryLabel ID="lblPointReasonTypeID" DictionaryType="Dic_PointReasonType" FieldIDValue='<%#Eval("PointReasonTypeID") %>' runat="server" />--%>
                        <cc1:ShortTextLabel ID="lblPointReasonTypeID" runat="server" ShowTextNum="10" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="积分原因" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                    <ItemTemplate>                        
                        <cc1:ShortTextLabel ID="lblPointReason" runat="server" ShowTextNum="10" Text='<%#Eval("PointReason") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:BoundField DataField="GivePoints" HeaderText="积分" SortExpression="GivePoints"
                    HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright" HeaderStyle-Width="40">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="状态" HeaderStyle-Width="60">
                    <ItemTemplate>
                        <cc1:DictionaryLabel ID="lblIsUse" runat="server" DictionaryType="Dic_Status" FieldIDValue='<%#Eval("IsUse") %>' />
                        <asp:LinkButton runat="server" ID="lblIsUse1" Text='<%#Eval("IsUse").ToString()=="1"?"启用":"停用" %>'
                            CommandArgument='<%#Eval("StudentPointReasonRoleID") %>' CommandName="isuse"
                            Visible="false" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作" HeaderStyle-Width="80">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <a href="javascript:showWindow('编辑积分原因','<%#this.ActionHref(String.Format("PointReasonRoleInfo.aspx?StudentPointReasonRoleID={0}",Eval("StudentPointReasonRoleID")))%>')">
                            编辑</a>
                        <cc1:CustomLinkButton runat="server" ID="lbtn_Del" CommandArgument='<%#Eval("StudentPointReasonRoleID") %>'
                            CommandName="del" Text="删除" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="确定要删除吗？" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </cc1:CustomGridView>
        <!--列表 end-->
        <div class="dv_splitLine">
        </div>
        <!--翻页-->
        <div class="dv_pagePanel">
        </div>
    </div>
</asp:Content>
