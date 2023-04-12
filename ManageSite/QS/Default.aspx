<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="QS_Default"
    MasterPageFile="~/MasterPages/MPageAdmin.master" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content runat="server" ID="PageContent" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="dv_searchbox">
        <table class="GridviewGray th70">
            <tr>
                <th>
                    调查名称:
                </th>
                <td colspan="3">
                    <cc1:CustomTextBox ButtonControlId="btn_Search" ID="txtQueryName" runat="server"
                        CssClass="inputbox_300"></cc1:CustomTextBox>
                    <asp:Button ID="btn_Search" runat="server" Text="查询" OnClick="btn_Search_Click" SkinID="Search" />
                    <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
                </td>
            </tr>
            <tr>
                <th>
                    查看结果:
                </th>
                <td>
                    <cc1:DictionaryDropDownList runat="server" ID="ddlIsDiplayResult" DictionaryType="Dic_TrueOrFalse"
                        IsShowAll="true" />
                </td>
                <th>
                    是否模板:
                </th>
                <td>
                    <cc1:DictionaryDropDownList runat="server" ID="ddlIsTemplate" DictionaryType="Dic_TrueOrFalse" />
                </td>
            </tr>
            <tr>
                <th>
                    问卷状态:
                </th>
                <td>
                    <cc1:DictionaryDropDownList runat="server" ID="ddlIsStatus" DictionaryType="Dic_Status" />
                </td>
                <th>
                    发布状态:
                </th>
                <td>
                    <cc1:DictionaryDropDownList runat="server" ID="ddlIsPublish" DictionaryType="Dic_PublishStatus"
                        IsShowAll="true" />
                </td>
            </tr>
            <tr>
                <th>
                    创建时间:
                </th>
                <td>
                    <cc1:DateTimeTextBox ID="txtCreateBeginTime" runat="server" EndTimeControlID="txtCreateEndTime"></cc1:DateTimeTextBox>至<cc1:DateTimeTextBox
                        ID="txtCreateEndTime" runat="server" MaxLength="10" BeginTimeControlID="txtCreateBeginTime"></cc1:DateTimeTextBox>
                </td>
                <th>
                    调查时间:
                </th>
                <td>
                    <cc1:DateTimeTextBox ID="txtQueryBeginTime" runat="server" EndTimeControlID="txtQueryEndTime"></cc1:DateTimeTextBox>至<cc1:DateTimeTextBox
                        ID="txtQueryEndTime" runat="server" BeginTimeControlID="txtQueryBeginTime"></cc1:DateTimeTextBox>
                </td>
            </tr>
        </table>
        <div class="center">
        </div>
    </div>
    <div class="dv_searchlist">
        <!--翻页-->
        <div class="dv_pagePanel">
            <div class="dv_selectAll">
                <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="GridViewList" />
                <input type="button" class="btn_Add" value="新增" onclick="showWindow('新增调查问卷','<%=this.ActionHref(string.Format("View.aspx?op=add&id=0&PollTypeID={0}",this.PollType))%>')" />
                <input type="button" class="btn_Addmodel" value="按模板新增" onclick="showWindow('按模板新增调查问卷','<%=this.ActionHref(string.Format("TemplateAdd.aspx?PollTypeID={0}",this.PollType))%>')" />
                <cc1:CustomButton CssClass="btn_Deploy" Text="发布" runat="server" ID="btnPublish"
                    EnableConfirm="true" ConfirmMessage="确定要发布吗?" OnClick="btnPublish_Click" />
                <cc1:CustomButton CssClass="btn_Del" Text="删除" runat="server" ID="btnDelete" EnableConfirm="true"
                    ConfirmMessage="确定要删除吗?" OnClick="btnDelete_Click" />
            </div>
            <div class="dv_pageControl">
                <uc2:PageSet ID="PageSet1" runat="server" />
            </div>
        </div>
        <cc1:CustomGridView ID="GridViewList" runat="server" AutoGenerateColumns="False"
            OnRowDataBound="GridViewList_RowDataBound" DataKeyNames="QueryID">
            <Columns>
                <asp:TemplateField HeaderStyle-Width="18">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" Enabled='<%#!(bool)Eval("IsPublish")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="调查名称" HeaderStyle-CssClass="alignleft">
                    <ItemStyle CssClass="alignleft" />
                    <ItemTemplate>
                        <cc1:ShortTextLabel runat="server" Text='<%#Eval("QueryName") %>'></cc1:ShortTextLabel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="调查时间" HeaderStyle-Width="140">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <cc1:DateTimeLabel runat="server" DateTimeValue=' <%#Eval("BeginTime")%>'></cc1:DateTimeLabel>~
                        <cc1:DateTimeLabel runat="server" DateTimeValue=' <%#Eval("EndTime")%>'></cc1:DateTimeLabel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="问卷状态" HeaderStyle-Width="60">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <cc1:DictionaryLabel runat="server" DictionaryType="Dic_Status" FieldIDValue='<%#Eval("QueryStatus") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="发布状态" HeaderStyle-Width="60">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <cc1:DictionaryLabel runat="server" DictionaryType="Dic_PublishStatus" FieldIDValue='<%#Convert.ToInt32(Eval("IsPublish")) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="查看结果" HeaderStyle-Width="60">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <cc1:DictionaryLabel runat="server" DictionaryType="Dic_TrueOrFalseBool" FieldIDValue='<%#Eval("IsDisplayResult")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="模板" HeaderStyle-Width="40">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <cc1:DictionaryLabel runat="server" DictionaryType="Dic_TrueOrFalseBool" FieldIDValue='<%#Eval("IsTemplate")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False" HeaderText="操作" HeaderStyle-Width="170">
                    <ItemTemplate>
                        <a href='javascript:showWindow("编辑调查问卷","<%# this.ActionHref(String.Format("View.aspx?op=edit&id={0}", new Object[]{Eval("QueryID")})) %>")'>
                            编辑</a> <a href='javascript:showWindow("查看调查问卷","<%# this.ActionHref(String.Format("View.aspx?op=view&id={0}", new Object[]{Eval("QueryID")})) %>")'>
                                查看</a> <a href='<%# this.ActionHref(String.Format("../qs/QueryPreView.aspx?QueryID={0}", new Object[]{Eval("QueryID")})) %>'
                                    target="QueryPreView">预览</a><br />
                        <a href='<%# this.ActionHref(String.Format("QuestionList.aspx?PollTypeID={1}&QueryID={0}", new Object[]{Eval("QueryID"),this.PollType})) %>'>
                            题目管理</a>
                        <asp:HyperLink runat="server" ID="hySetArea" Text="调查范围"></asp:HyperLink>
                        <cc1:CustomLinkButton runat="server" ID="lkSwitchStatus" Text='<%#(((bool)Eval("IsTemplate"))?"取消模板":"设为模板") %>'
                            OnCommand="UserOpeator_Command" EnableConfirm="true" ConfirmMessage='<%#string.Format("确认要{0}？",(((bool)Eval("IsTemplate"))?"取消模板":"设为模板")) %>'
                            CommandName="SwitchTemplate" CommandArgument='<%#Eval("QueryID") %>'></cc1:CustomLinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
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
