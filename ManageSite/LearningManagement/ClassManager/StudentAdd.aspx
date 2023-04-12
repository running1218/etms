<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="StudentAdd.aspx.cs" Inherits="LearningManagement_ClassManager_StudentAdd" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" runat="Server">
    <a id="aBack" runat="server" class="btn_Return">返回</a>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--查找条件-->   
    <div class="dv_searchbox">
        <table width="98%" border="0" cellspacing="0" cellpadding="0" class="GridviewGray th70"
            id="tableQueryControlList" runat="server">
            <tr>
                <th >
                    姓&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;名：
                </th>
                <td >
                    <asp:TextBox ID="txtRealName" runat="server" CssClass="inputbox_160" />
                    <asp:Button ID="btnSearch" runat="server" CssClass="btn_Search" Text="查询" OnClick="btnSearch_Click" />
                    <a href="javascript:hideGridview()" class="dropdownico" id="A2">高级搜索</a>
                </td>
                
            </tr>
            <tr id="trorg" runat="server">
                <th>
                    组织机构：
                </th>
                <td colspan="3">
                    <asp:DropDownList runat="server" ID="ddlOrg" AutoPostBack="True" CssClass="select_160" OnSelectedIndexChanged="ddlOrg_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltlDepartment" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>：
                </th>
                <td>                    
                    <asp:DropDownList runat="server" ID="ddlDepartment" CssClass="select_160">
                        <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th style="width:100px;">
                    <asp:Literal ID="ltlRank" runat="server" Text="<%$ Resources:UIResource, ui_rank%>"></asp:Literal>：
                </th>
                <td>
                    <cc1:DictionaryDropDownList runat="server" ID="ddlRank" DictionaryType="vw_Dic_Sys_Rank" CssClass="select_160"
                        IsShowAll="true" />
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltlPosition" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>：
                </th>
                <td colspan="3">
                    <asp:DropDownList runat="server" ID="ddlPost" CssClass="select_160">
                        <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                    </asp:DropDownList>                       
                </td>
            </tr>
            
        </table>
    </div>
    <!--查找结果-->
    <div class="dv_searchlist">
        <asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <!--翻页-->
                <div class="dv_pagePanel">
                    <div class="dv_selectAll">
                        <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" />
                        <asp:Button ID="btnSave" runat="server" CssClass="btn_Ok" Text="确定" OnClick="btnSave_Click" />
                    </div>
                    <div class="dv_pageControl">
                        <uc2:PageSet ID="PageSet1" runat="server" />
                    </div>
                </div>
                <!--列表 begin-->
                <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="False"
                    DataKeyNames="UserID,StudentSignupID" OnRowDataBound="CustomGridView1_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <ItemStyle HorizontalAlign="Center" Width="40" />
                            <HeaderStyle HorizontalAlign="Center" Width="20" />
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="WorkerNo" HeaderText="工号" SortExpression="WorkerNo" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft" HeaderStyle-Width="60">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="RealName" HeaderText="姓名" SortExpression="RealName" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft" HeaderStyle-Width="60">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="组织机构"  HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                            <ItemTemplate>
                                <cc1:DictionaryLabel runat="server" ID="lblOrg" DictionaryType="vw_Dic_Sys_Organization"
                                    FieldIDValue='<%#Eval("OrganizationID").ToString() %>' TextLength="10" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                            <ItemTemplate>
                                <cc1:DictionaryLabel runat="server" ID="lblDepartment" DictionaryType="vw_Dic_Sys_Department"
                                    FieldIDValue='<%#Eval("DepartmentID").ToString() %>' TextLength="10" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_position%>"  HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                            <ItemTemplate>
                                <cc1:DictionaryLabel runat="server" ID="lblPost" DictionaryType="vw_Dic_Sys_Post"
                                    FieldIDValue='<%#Eval("PostID").ToString() %>' TextLength="10" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_rank%>"  HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                            <ItemTemplate>
                                <cc1:DictionaryLabel runat="server" ID="lblRank" DictionaryType="vw_Dic_Sys_Rank"
                                    FieldIDValue='<%#Eval("RankID").ToString() %>' TextLength="10" />
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
        </asp:UpdatePanel>
    </div>
    <%--</div>--%>
</asp:Content>
