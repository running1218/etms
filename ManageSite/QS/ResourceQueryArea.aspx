<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    CodeFile="ResourceQueryArea.aspx.cs" Inherits="QS_ResourceQueryArea" %>

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
        $(function () {
            $(".dv_searchbox").find("table tr:gt(0)").show();
            $("#Highsearch").addClass("dropupico");
        })
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
            <!--查找条件-->
            <div class="dv_searchbox">
                <table class="GridviewGray">
                    <tr>
                        <th>
                            机构编码：
                        </th>
                        <td>
                            <asp:TextBox ID="txtOrgCode" runat="server" CssClass="inputbox_120 floatleft"></asp:TextBox>
                            <asp:Button ID="btnOrgSearch" runat="server" Text="查询" OnClick="btnSearchOrgClick"
                                SkinID="Search" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            机构名称：
                        </th>
                        <td>
                            <asp:TextBox ID="txtOrgName" runat="server" CssClass="inputbox_120 floatleft"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <div class="dv_searchlist">
                    <!--翻页-->
                    <div class="dv_pagePanel">
                        <div class="dv_selectAll">
                            <cc1:CheckBoxsController runat="server" ID="CheckBoxsController1" ContainerID="OrgGridView" />
                            <input type="button" value="新增" class="btn_Add" onclick="window.location.href='<%=this.ActionHref(string.Format("ResourceQueryAreaAdd.aspx?QueryID={0}",this.QueryID))%>'" />
                            <cc1:CustomButton CssClass="btn_Del" Text="删除" runat="server" ID="CustomButton1"
                                EnableConfirm="true" ConfirmMessage="已经提交答卷人员的机构不能被删除，确定要删除吗?" OnClick="btnDeleteOrgClick" />
                        </div>
                        <div class="dv_pageControl">
                            <uc2:PageSet ID="PageSet2" runat="server" />
                        </div>
                    </div>
                    <cc1:CustomGridView ID="OrgGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="QueryAreaID">
                        <Columns>
                            <asp:TemplateField HeaderStyle-Width="18">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="OrganizationCode" HeaderText="机构编码" ItemStyle-CssClass="alignleft"
                                HeaderStyle-CssClass="alignleft field8"></asp:BoundField>
                            <asp:BoundField DataField="OrganizationName" HeaderText="机构名称" ItemStyle-CssClass="alignleft"
                                HeaderStyle-CssClass="alignleft field8"></asp:BoundField>
                            <asp:BoundField DataField="StudentNum" HeaderText="启用学员数" ItemStyle-CssClass="alignleft"
                                HeaderStyle-CssClass="alignleft field8"></asp:BoundField>
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
        <%} %>
        <div id="Div_Select_1" style="display: none">
            <div class="dv_description">
                <b>“按个人”调查范围说明：</b><span class="colorYellow">如果调查范围没有指定任何学员，则表示机构下所有学员都参与此调查；否则，仅指定的学员才可参与此调查。
                </span>
            </div>
            <%--  <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>--%>
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
                            <%--<a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>--%>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            组织机构：
                        </th>
                        <td>
                            <cc1:DictionaryDropDownList runat="server" ID="ddl_OrganizationID" DictionaryType="Dic_CurrentAndSubOrganization"
                                IsShowAll="true" AutoPostBack="True" CssClass="select_390" OnSelectedIndexChanged="ddl_OrganizationID_SelectedIndexChanged" />
                        </td>
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
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:DropDownList runat="server" ID="ddl_DepartmentID" CssClass="select_120">
                                    </asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddl_OrganizationID" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <th>
                            <asp:Literal ID="ltlPosition" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>：
                        </th>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:DropDownList runat="server" ID="ddl_PostID" CssClass="select_120">
                                    </asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddl_OrganizationID" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>
            <input type="hidden" runat="server" id="HidDelValue" />
            <div class="dv_searchlist">
                <!--翻页-->
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="dv_pagePanel">
                            <div class="dv_selectAll">
                                <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="GridViewList" />
                                <input type="button" value="新增" class="btn_Add" onclick="window.location.href='<%=this.ActionHref(string.Format("ResourceQueryStudentAdd.aspx?queryid={0}",QueryID))%>'" />
                                <cc1:CustomButton CssClass="btn_Del" Text="删除" runat="server" ID="btnDelete" EnableConfirm="true"
                                    ConfirmMessage="已经提交的答卷人员不能被删除，确定要删除吗?" OnClick="btnDelete_Click" /><cc1:CustomButton
                                        ID="lbtnImport" runat="server" CssClass="btn_Import" OnClientClick="" Text="导入"
                                        EnableConfirm="false"></cc1:CustomButton>
                                <asp:Button runat="server" ID="btnExport" Text="导出" OnClick="btnExport_Click" CssClass="btn_Export" />
                            </div>
                            <div class="dv_pageControl">
                                <uc2:PageSet ID="PageSet1" runat="server" />
                            </div>
                        </div>
                        <cc1:CustomGridView ID="GridViewList" runat="server" AutoGenerateColumns="False"
                            DataKeyNames="QueryAreaID">
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
                                <asp:TemplateField HeaderText="工号" HeaderStyle-CssClass="alignleft">
                                    <ItemStyle CssClass="alignleft" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <cc1:ShortTextLabel ID="ShortTextLabel11" runat="server" Text='<%#Eval("WorkerNo") %>'></cc1:ShortTextLabel>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="组织机构" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                                    <ItemTemplate>
                                        <cc1:DictionaryLabel ID="DictionaryLabel1" runat="server" DictionaryType='vw_Dic_Sys_Organization'
                                            FieldIDValue='<%#Eval("OrganizationID") %>'></cc1:DictionaryLabel>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                                    <ItemTemplate>
                                        <cc1:DictionaryLabel ID="DictionaryLabel2" runat="server" DictionaryType='vw_Dic_Sys_Department'
                                            FieldIDValue='<%#Eval("DepartmentID") %>'></cc1:DictionaryLabel>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_position%>" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <cc1:DictionaryLabel ID="DictionaryLabel3" runat="server" DictionaryType='vw_Dic_Sys_Post'
                                            FieldIDValue='<%#Eval("PostID") %>'></cc1:DictionaryLabel>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_rank%>" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <cc1:DictionaryLabel ID="DictionaryLabel4" runat="server" DictionaryType='vw_Dic_Sys_Rank'
                                            FieldIDValue='<%#Eval("RankID") %>'></cc1:DictionaryLabel>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="邮箱" HeaderStyle-CssClass="alignleft">
                                    <ItemStyle CssClass="alignleft" />
                                    <ItemTemplate>
                                        <cc1:ShortTextLabel ID="ShortTextLabel1" runat="server" Text='<%#Eval("Email") %>'></cc1:ShortTextLabel>
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
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnExport" />
                        <asp:AsyncPostBackTrigger EventName="Click" ControlID="btn_Search" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
