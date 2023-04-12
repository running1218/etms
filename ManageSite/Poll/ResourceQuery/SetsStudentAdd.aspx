<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="SetsStudentAdd.aspx.cs" Inherits="Poll_ResourceQuery_SetsStudentAdd" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content runat="server" ContentPlaceHolderID="cphBack" ID="Content2">
    <a class="btn_Return" onclick='window.location ="<%=this.ActionHref(string.Format("SetArea_R2.aspx?QueryID={0}",this.PublishObject.QueryID))%>"'>
        返回</a>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" id="tableQueryControlList">
                <tr>
                    <th width="120">
                        姓名：
                    </th>
                    <td width="120">
                        <asp:TextBox ID="txtRealName" runat="server" CssClass="inputbox_120 floatleft"></asp:TextBox>
                    </td>
                    <th width="120">
                        工号：
                    </th>
                    <td>
                        <asp:TextBox ID="txtWorkerNo" runat="server" CssClass="inputbox_120"></asp:TextBox><asp:Button
                            ID="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" SkinID="Search" />
                        <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
                    </td>
                </tr>
                <%-- 多机构时显示 --%>
                <tr>
                    <%if (!ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
                      { %>
                    <th style="width: 120">
                        机构：
                    </th>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlOrg" AutoPostBack="true" CssClass="select_190"
                            OnSelectedIndexChanged="ddlOrg_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <%} %>
                    <th style="width: 120">
                        <asp:Literal ID="ltlRank" runat="server" Text="<%$ Resources:UIResource, ui_rank%>"></asp:Literal>：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="ddlRank" DictionaryType="vw_Dic_Sys_Rank"
                            IsShowAll="true" CssClass="select_190" />
                    </td>
                </tr>
                <tr>
                    <th style="width: 120">
                        <asp:Literal ID="ltlDepartment" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>：
                    </th>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlDepartment" CssClass="select_190">
                        </asp:DropDownList>
                    </td>
                    <th style="width: 120">
                        <asp:Literal ID="ltlPosition" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>：
                    </th>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlPost" CssClass="select_190">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel" id="divPage1">
                <div class="dv_selectAll">
                    <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" />
                    <asp:Button ID="btnAdd" runat="server" Text="确定" CssClass="btn_Ok" OnClick="btnAdd_Click" />
                    <asp:Button ID="Button1" runat="server" Text="增加全部" CssClass="btn_AddNew" OnClick="btnAddAll_Click" />
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
                CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0"
                DataKeyNames="UserID">
                <Columns>
                    <asp:TemplateField HeaderStyle-Width="18">
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="RealName" HeaderText="姓名" ItemStyle-CssClass="alignleft"
                        HeaderStyle-CssClass="alignleft" HeaderStyle-Width="80" />
                    <asp:BoundField DataField="WorkerNo" HeaderText="工号" ItemStyle-CssClass="alignleft"
                        HeaderStyle-CssClass="alignleft" HeaderStyle-Width="80" />
                    <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblDepartmentID" DictionaryType="vw_Dic_Sys_Department"
                                FieldIDValue='<%# Eval("DepartmentID") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_position%>" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblPost" DictionaryType="vw_Dic_Sys_Post" FieldIDValue='<%# Eval("PostID") %>'
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_rank%>" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblRank" DictionaryType="vw_Dic_Sys_Rank" FieldIDValue='<%# Eval("RankID") %>'
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </cc1:CustomGridView>
            <!--列表 end-->
            <div class="dv_splitLine">
            </div>
            <!--翻页-->
            <div class="dv_pagePanel" id="divPage2">
            </div>
        </div>
    </div>
</asp:Content>
