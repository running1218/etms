<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="IDPList.aspx.cs" Inherits="IDP_ManageIDP_IDPList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
                id="tableQueryControlList">
                <tr>
                    <th >
                        学员姓名：
                    </th>
                    <td >
                        <asp:TextBox ID="txt_c999RealName" runat="server" CssClass="inputbox_120 floatleft"
                            MaxLength="50"></asp:TextBox>
                    </td>
                    <th >
                        导师姓名：
                    </th>
                    <td>
                        <asp:TextBox ID="txt_d999RealName" runat="server" CssClass="inputbox_120 floatleft"
                            MaxLength="50"></asp:TextBox>
                   
                        <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
                        <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a> 
                   
                    </td>
                </tr>
                 <tr>
                    <th>
                        <asp:Literal ID="ltlDepartment" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>：
                    </th>
                    <td>
                       <cc1:DictionaryDropDownList runat="server" ID="ddl_c999DepartmentID" DictionaryType="Site_DepartmentByOrgID" />
                    </td>
                    <th>
                        学员<asp:Literal ID="ltlPosition" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="ddl_b999PostID" DictionaryType="Dic_PostByOrgID" />
                    </td>
                </tr>       
                <tr>
                    <th>
                        导师<asp:Literal ID="ltlDepartment1" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>：
                    </th>
                    <td>
                       <cc1:DictionaryDropDownList runat="server" ID="ddl_d999DepartmentID" DictionaryType="Site_DepartmentByOrgID" />
                    </td>
                    <th>
                        导师<asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="ddls_mb999PostID" DictionaryType="Dic_PostByOrgID" />
                    </td>
                </tr>     
                <tr>
                <th>
                    创建日期：
                </th >
                <td colspan="4">
                    <cc1:DateTimeTextBox runat="server" ID="begin_a999CreateTime" DateTimeFormat="%Y-%M-%D"
                        EndTimeControlID="end_a999CreateTime" />至
                    <cc1:DateTimeTextBox runat="server" ID="end_a999CreateTime" DateTimeFormat="%Y-%M-%D"
                        BeginTimeControlID="begin_a999CreateTime" />
                </td>
            </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            
            <!--翻页-->
            <div class="dv_pagePanel">
                <div class="dv_selectAll">
                     <asp:Button ID="btnExport" runat="server" CssClass="btn_Export" Text="导出" OnClick="btnExport_Click" />
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="gvList" runat="server" CssClass="GridviewGray" EmptyDataSourceTip="没有任何记录！"
                AutoCreateColumnInsertIndex="0" AutoGenerateColumns="False" CurrentPageIndex="1"
                CustomAllowPaging="False" IsEmpty="False" TotalRecordCount="0" DataKeyNames="IDP_PlanID"
                OnRowCommand="gvList_RowCommand">
                <Columns>
                    <asp:BoundField DataField="StudentName" HeaderText="学员姓名" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft field8"/>
                    <asp:TemplateField ItemStyle-CssClass="alignleft"  HeaderStyle-CssClass="alignleft" >
                        <HeaderTemplate>
                            学员<asp:Literal ID="ltlDepartment" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblStudentDepartment" DictionaryType="Site_DepartmentByOrgID" TextLength="10"
                                FieldIDValue='<%# Eval("StudentDepartmentID") %>' runat="server" />

                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="MentorName" HeaderText="导师姓名"  HeaderStyle-CssClass="alignleft field8" ItemStyle-CssClass="alignleft" />
                    <asp:TemplateField ItemStyle-CssClass="alignleft"  HeaderStyle-CssClass="alignleft" >
                        <HeaderTemplate>
                            导师<asp:Literal ID="ltlDepartment" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblMentorDepartment" DictionaryType="Site_DepartmentByOrgID" TextLength="10"
                                FieldIDValue='<%# Eval("MentorDepartmentID") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="辅导类型"  ItemStyle-CssClass="alignleft"  HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblIDPType" DictionaryType="Dic_Sys_IDPType" 
                                FieldIDValue='<%# Eval("IDPTypeID") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="计划周期" HeaderStyle-Width="160"  >
                        <ItemTemplate>
                          <%# Eval("IDPPlanBeginTime").ToDate() + " 至 " + Eval("IDPPlanEndTime").ToDate()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="创建日期" HeaderStyle-Width="80">
                        <ItemTemplate>
                            <%# Eval("CreateTime").ToDate()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="是否关闭" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblIsClose" DictionaryType="Dic_TrueOrFalseBool" 
                                FieldIDValue='<%# Eval("IsClose") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作" HeaderStyle-Width="100">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <a href="<%# this.ActionHref(string.Format("~/IDP/ManageIDP/IDPView.aspx?PlanID={0}", Eval("IDP_PlanID")))%>">查看</a>
                            <cc1:CustomLinkButton runat="server" ID="lbtn_Del" CommandArgument='<%# Eval("IDP_PlanID") %>' CommandName="Del" Text="删除" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="确定要删除吗？" />
                             <%# getUrl2(Eval("IDP_PlanID").ToGuid(), Eval("IsClose").ToString())%> 
                          
                          <cc1:CustomLinkButton runat="server" ID="CustomLinkButton1" CommandArgument='<%# Eval("IDP_PlanID") %>' CommandName="Open" Text="启用" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="确定要启用吗？"  Visible='<%# bool.Parse(Eval("IsClose").ToString()) %>'/>
                          
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
</asp:Content>

