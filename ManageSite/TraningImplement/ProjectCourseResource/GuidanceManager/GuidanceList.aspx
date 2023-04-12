<%@ Page Title="导学资料管理" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="GuidanceList.aspx.cs" Inherits="QuestionDB_GuidanceManager_GuidanceList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--导航路径-->
        <div class="dv_path">
            当前位置：资源管理系统>>学习资源管理>>导学资料管理
        </div>
        <!--功能标题-->
        <h2 class="dv_title">
            导学资料管理
        </h2>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <th width="20%">
                        培训项目：
                    </th>
                    <td colspan="3">
                        <cc1:DictionaryDropDownList runat="server" ID="DictionaryDropDownList1" DictionaryType="Dic_ProjectList"
                            IsShowAll="true" />
                        <input class="btn_Search" type="button" value="查询"/><a href="javascript:hideGridview()" class="dropdownico"
                            id="Highsearch">高级搜索</a>
                    </td>
                </tr>
                <tr>
                    <th width="20%">
                        资料类型：
                    </th>
                    <td width="30%">
                        <cc1:DictionaryDropDownList runat="server" ID="DictionaryDropDownList2" DictionaryType="Dic_GuidanceManagerType"
                            IsShowAll="true" />
                    </td>
                    <th width="20%">
                        资料名称：
                    </th>
                    <td width="30%">
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        状&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;态：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="DictionaryDropDownList3" DictionaryType="Dic_GuidanceManagerState"
                            IsShowAll="true" />
                    </td>
                    <th>
                        课程名称：
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
                    <input type="button" class="btn_Add" value="新增" onclick="javascript:showWindow('新增导学资料','GuidanceAdd.aspx')" />
                    <cc1:CustomButton runat="server" ID="Button3" Text="发布" CssClass="btn_Deploy" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="确定要发布吗？" />
                    <cc1:CustomButton runat="server" ID="CustomButton1" Text="关闭" CssClass="btn_Close" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="确定要关闭吗？" />
                    <cc1:CustomButton runat="server" ID="CustomButton2" Text="删除" CssClass="btn_Del" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="确定要删除吗？" />
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="True"
                CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0">
                <Columns>
                    <asp:TemplateField HeaderText="">
                        <ItemStyle HorizontalAlign="Center" Width="28" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <a href="javascript:showWindow('编辑导学资料','GuidanceEdit.aspx')">编辑</a><a href="javascript:showWindow('查看导学资料','GuidanceView.aspx','500',360)">查看</a>
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
