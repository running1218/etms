<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="PointReasonDetailUnpublishList.aspx.cs" Inherits="Point_PointReasonDetailUnpublishList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" runat="Server">
    <a id="aBack" runat="server" class="btn_Return">返回</a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="dv_searchbox">
        <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <th width="100">
                    项目名称：
                </th>
                <td>
                    <asp:Label ID="lblItemName" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div class="dv_searchlist">
        <!--翻页-->
        <div class="dv_pagePanel">
            <div class="dv_selectAll">
                <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1"
                    Visible="false" />
            </div>
            <div class="dv_pageControl">
                <uc2:PageSet ID="PageSet1" runat="server" />
            </div>
        </div>
        <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="False"
            DataKeyNames="StudentSignupID">
            <Columns>
                <%-- <asp:TemplateField HeaderText="项目名称">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblItemName" runat="server" Text='<%#Eval("ItemName") %>'
                                    ShowTextNum="10" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="学员姓名" HeaderStyle-CssClass="alignleft field8" ItemStyle-CssClass="alignleft" >
                    <ItemTemplate>
                        <asp:Label ID="lblRealName" runat="server" Text='<%#Eval("RealName") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="组织机构" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <cc1:DictionaryLabel runat="server" ID="lblOrg" DictionaryType="vw_Dic_Sys_Organization"
                            FieldIDValue='<%#Eval("OrganizationID").ToString() %>' TextLength="10" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <cc1:DictionaryLabel runat="server" ID="lblDepartment" DictionaryType="vw_Dic_Sys_Department"
                            TextLength="10" FieldIDValue='<%#Eval("DepartmentID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="积分原因" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <cc1:ShortTextLabel Text='<%#Eval("PointReason") %>' ID="lblPointReason" runat="server"
                            ShowTextNum="10" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="积分值" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright" HeaderStyle-Width="60">
                    <ItemTemplate>
                        <cc1:ShortTextLabel ID="lblGroupName" runat="server" ShowTextNum="10" Text='<%#Eval("AccessPoints") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="创建时间" HeaderStyle-Width="100">
                    <ItemTemplate>
                        <asp:Label ID="lblCreateTime" runat="server" Text='<%#Eval("CreateTime").ToDate() %>' />
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
