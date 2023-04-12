<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="SetArea_R2.aspx.cs" Inherits="Poll_ResourceQuery_SetArea_R2" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content runat="server" ContentPlaceHolderID="cphBack" ID="Content2">
    <asp:HyperLink runat="server" Text="返回" ID="hylReturn" CssClass="btn_Return"></asp:HyperLink>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function CloseRefesh() {
            window.parent.location.href = window.parent.location.href;
        }
    </script>
    <div class="dv_serviceTab">
        <div class="dv_Tabmenus">
            <ul>
                <%-- 多机构时显示 --%>
                <%if (!ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
                  { %>
                <li id="Tab_0" onclick="showTab('Tab_0', 'Div_Select_0','selected')"><a onfocus="blur()"
                    href="javascript:void(0);"><span class="bj">按机构</span></a> </li>
                <%} %>
                <li id="Tab_1" onclick="showTab('Tab_1', 'Div_Select_1','selected')"><a onfocus="blur()"
                    href="javascript:void(0);"><span class="bj">按个人</span></a></li>
            </ul>
        </div>
    </div>
    <div class="info">
        <%-- 多机构时显示 --%>
        <%if (!ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
          { %>
        <div id="Div_Select_0" style="display: none">
            <div class="dv_information">
                <table class="GridviewGray">
                    <tr>
                        <th>
                            调查名称：
                        </th>
                        <td>
                            <asp:Label ID="lblQueryName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            调查时间：
                        </th>
                        <td>
                            <cc1:DateTimeLabel ID="lblBeginTime" runat="server" />至<cc1:DateTimeLabel ID="lblEndTime"
                                runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            机构范围：
                        </th>
                        <td>
                            <asp:RadioButtonList runat="server" ID="rblAreaType" RepeatLayout="Flow" RepeatDirection="Horizontal">
                                <asp:ListItem Text="本组织机构" Value="CurrentOrg"></asp:ListItem>
                                <asp:ListItem Text="本组织机构及下级组织机构" Value="AllOrg"></asp:ListItem>
                                <asp:ListItem Text="仅所有下级组织机构" Value="SubOrg"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
                <!--提交表单-->
                <div class="dv_submit">
                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn_Save" OnClick="btnSave_Click">保存</asp:LinkButton>
                </div>
            </div>
        </div>
        <%} %>
        <div id="Div_Select_1" style="display: none">
            <div class="dv_description">
                <b>“按个人”调查范围说明：</b><span class="colorYellow">如果调查范围没有指定任何学员，则表示机构下所有学员都参与此调查；否则，仅指定的学员才可参与此调查。
                </span>
            </div>
            <div class="dv_searchbox">
                <table class="GridviewGray">
                    <tr>
                        <th style="width: 100px">
                            姓名：
                        </th>
                        <td>
                            <asp:TextBox ID="txtRealName" runat="server"></asp:TextBox>
                        </td>
                        <th>
                            工号：
                        </th>
                        <td>
                            <asp:TextBox ID="txtWorkerNo" runat="server"></asp:TextBox>
                            <asp:Button ID="btn_Search" runat="server" Text="查询" OnClick="btn_Search_Click" SkinID="Search" />
                            <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
                        </td>
                    </tr>
                    <tr>
                        <%-- 多机构时显示 --%>
                        <%if (!ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
                          { %>
                        <th>
                            组织机构：
                        </th>
                        <td>
                            <asp:DropDownList runat="server" ID="ddl_OrganizationID" AutoPostBack="true" CssClass="select_190"
                                OnSelectedIndexChanged="ddl_OrganizationID_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <%} %>
                        <th>
                            <asp:Literal ID="ltlRank" runat="server" Text="<%$ Resources:UIResource, ui_rank%>"></asp:Literal>：
                        </th>
                        <td>
                            <cc1:DictionaryDropDownList runat="server" ID="ddlRank" DictionaryType="vw_Dic_Sys_Rank"
                                IsShowAll="true" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <asp:Literal ID="ltlDepartment" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>：
                        </th>
                        <td>
                            <asp:DropDownList runat="server" ID="ddl_DepartmentID" CssClass="select_120">
                            </asp:DropDownList>
                        </td>
                        <th>
                            <asp:Literal ID="ltlPosition" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>：
                        </th>
                        <td>
                            <asp:DropDownList runat="server" ID="ddl_PostID" CssClass="select_120">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="dv_searchlist">
                <!--翻页-->
                <div class="dv_pagePanel">
                    <div class="dv_selectAll">
                        <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="GridViewList" />
                        <input type="button" value="新增" class="btn_Add" onclick="window.location.href='<%=this.ActionHref(string.Format("SetsStudentAdd.aspx?QueryAreaID={0}",this.CurrentQueryArea.QueryAreaID))%>'" />
                        <cc1:CustomButton CssClass="btn_Del" Text="删除" runat="server" ID="btnDelete" EnableConfirm="true"
                            ConfirmMessage="已经提交的答卷人员不能被删除，确定要删除吗?" OnClick="btnDelete_Click" />
                        <cc1:CustomButton CssClass="btn_DelAll" Text="删除全部" runat="server" ID="CustomButton1"
                            EnableConfirm="true" ConfirmMessage="已经提交的答卷人员不能被删除，确定要全部删除吗?" OnClick="btnDeleteAll_Click" />
                        <cc1:CustomButton ID="lbtnImport" runat="server" CssClass="btn_Import" OnClientClick=""
                            Text="导入" EnableConfirm="false"></cc1:CustomButton>
                        <asp:Button runat="server" ID="btnExport" Text="导出" OnClick="btnExport_Click" CssClass="btn_Export" />
                    </div>
                    <div class="dv_pageControl">
                        <uc2:PageSet ID="PageSet1" runat="server" />
                    </div>
                </div>
                <cc1:CustomGridView ID="GridViewList" runat="server" AutoGenerateColumns="False"
                    DataKeyNames="QueryAreaDetailID">
                    <Columns>
                        <asp:TemplateField HeaderStyle-Width="18">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="LoginName" HeaderText="用户名" ItemStyle-CssClass="aligncenter"
                            HeaderStyle-CssClass="alignleft field8"></asp:BoundField>
                        <asp:TemplateField HeaderText="姓名" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft field8">
                            <ItemTemplate>
                                <a href='javascript:showWindow("学员信息","<%# this.ActionHref(String.Format("~/Security/StudentManager/View.aspx?op=view&id={0}", new Object[]{Eval("UserID")})) %>")'>
                                    <%#Eval("RealName") %></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="WorkerNo" HeaderText="工号" ItemStyle-CssClass="alignleft"
                            HeaderStyle-CssClass="alignleft field8"></asp:BoundField>
                        <asp:TemplateField HeaderText="组织机构" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                            <ItemTemplate>
                                <cc1:DictionaryLabel runat="server" DictionaryType='vw_Dic_Sys_Organization' FieldIDValue='<%#Eval("OrganizationID") %>'></cc1:DictionaryLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                            <ItemTemplate>
                                <cc1:DictionaryLabel runat="server" DictionaryType='vw_Dic_Sys_Department' FieldIDValue='<%#Eval("DepartmentID") %>'></cc1:DictionaryLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_position%>" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <cc1:DictionaryLabel runat="server" DictionaryType='vw_Dic_Sys_Post' FieldIDValue='<%#Eval("PostID") %>'></cc1:DictionaryLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_rank%>" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <cc1:DictionaryLabel runat="server" DictionaryType='vw_Dic_Sys_Rank' FieldIDValue='<%#Eval("RankID") %>'></cc1:DictionaryLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="邮箱" HeaderStyle-CssClass="alignleft">
                            <ItemStyle CssClass="alignleft" />
                            <ItemTemplate>
                                <cc1:ShortTextLabel runat="server" Text='<%#Eval("Email") %>'></cc1:ShortTextLabel>
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
        </div>
    </div>
</asp:Content>
