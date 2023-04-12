﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="StudentList.aspx.cs" Inherits="TraningImplement_StudentRegistration_StudentList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <!--导航路径-->
        <div class="dv_path" id="dv_path">
            当前位置：培训管理>>培训实施>>管理学员报名>>学员
        </div>
        <!--功能标题-->
        <h2 class="dv_title">
            学员
        </h2>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <th>
                        姓名
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="inputbox_210"></asp:TextBox>
                        <input class="btn_Search" type="button" value="查询"/><a href="javascript:hideGridview()" class="dropdownico"
                            id="Highsearch">高级搜索</a>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltlRank" runat="server" Text="<%$ Resources:UIResource, ui_rank%>"></asp:Literal>
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="DictionaryDropDownList1" DictionaryType="Dic_Rank"
                            IsShowAll="true" />
                    </td>
                    <th>
                        所属<asp:Literal ID="ltlDepartment" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="TraningProjectType1" DictionaryType="Dic_DepartmentList"
                            IsShowAll="true" />
                    </td>
                    <th>
                        <asp:Literal ID="ltlPosition" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="DictionaryDropDownList2" DictionaryType="Dic_PostType"
                            IsShowAll="true" />
                    </td>
                    <th>
                        工号
                    </th>
                    <td>
                        <asp:TextBox ID="TextBox2" runat="server" CssClass="inputbox_120"></asp:TextBox>
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
                    <input type="button" class="btn_Add" value="新增" onclick="javascript:showWindow('新增','StudentAdd.aspx')" />
                    <input type="button" class="btn_Del" value="删除" />
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="True"
                CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="1">
                <Columns>
                    <asp:TemplateField HeaderText="">
                        <ItemStyle HorizontalAlign="Center" Width="40" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="序号">
                        <ItemStyle HorizontalAlign="Center" Width="30" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="LabNo" runat="server" Text='<%#  ((this.PageSet1.PageIndex -1) * this.PageSet1.PageSize) + Container.DataItemIndex+1 %>'></asp:Label>
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
            <script language="javascript" type="text/javascript">
                divPage2.innerHTML = divPage1.innerHTML;
            </script>
        </div>
    </div>
</asp:Content>
