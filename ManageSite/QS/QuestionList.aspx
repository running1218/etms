<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    CodeFile="QuestionList.aspx.cs" Inherits="QS_QuestionList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content runat="server" ContentPlaceHolderID="cphBack" ID="Content2">
    <asp:HyperLink runat="server" Text="返回" ID="hylReturn" CssClass="btn_Return"></asp:HyperLink>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--查找条件-->
    <div class="dv_searchbox">
        <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <th style="width: 100px">
                    调查名称：
                </th>
                <td>
                    <asp:Label runat="server" ID="lblQueryName"></asp:Label>
                </td>
                <th style="width: 100px">
                    调查时间：
                </th>
                <td style="width: 280px">
                    <cc1:DateTimeLabel runat="server" ID="lblBeginTime" />&nbsp;至&nbsp;<cc1:DateTimeLabel
                        runat="server" ID="lblEndTime" />
                </td>
            </tr>
        </table>
    </div>
    <!--查找结果-->
    <div class="dv_searchlist">
        <!--翻页-->
        <div class="dv_pagePanel">
            <div class="dv_selectAll">
                <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" />
                <input type="button" id="btn_add1" class="btn_SingleSelection" value="增加" onclick='javascript:showWindow("增加题目","<%= this.ActionHref(String.Format("QuFillBlanksAdd.aspx?Action=add&QueryID={0}&TitleID={1}", new Object[]{this.QueryID,Guid.NewGuid().ToString()})) %>")' />
                <input type="button" id="Button1" class="btn_Sort" value="排序" onclick='javascript:showWindow("题目排序","<%= this.ActionHref(String.Format("QuestionSort.aspx?QueryID={0}", new Object[]{this.QueryID})) %>")' />
                <cc1:CustomButton runat="server" ID="btnDelete" CssClass="btn_Del" Text="删除" EnableConfirm="true"
                    OnClick="btnDelete_Click" ConfirmMessage="确定要删除吗？" ConfirmTitle="提示" />
            </div>
            <div class="dv_pageControl">
                <uc2:PageSet ID="PageSet1" runat="server" />
            </div>
        </div>
        <!--列表 begin-->
        <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="False"
            DataKeyNames="TitleID" CustomAllowPaging="false" ShowFooter="false">
            <Columns>
                <asp:TemplateField HeaderText="">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="18" />
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="题目序号" DataField="TitleNo" HeaderStyle-Width="60" ItemStyle-CssClass="alignright"
                    HeaderStyle-CssClass="alignright" />
                <asp:BoundField HeaderText="题目" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft"
                    DataField="TitleName" />
                <asp:BoundField HeaderText="题目类型" DataField="TitleTypeName" HeaderStyle-Width="60"
                    ItemStyle-CssClass="alignright" HeaderStyle-CssClass="alignright" />
                <asp:TemplateField HeaderText="操作" HeaderStyle-Width="160">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <a href='javascript:showWindow("编辑题目","<%# this.ActionHref(String.Format("QuFillBlanksAdd.aspx?Action=edit&TitleID={0}", new Object[]{Eval("TitleID")})) %>")' />
                        编辑</a> <a href='javascript:showWindow("<%# Eval("PollTypeName")%>题查看","<%# this.ActionHref(String.Format("QuFillBlanksView.aspx?QuestionID={0}&QuestionType={1}", new Object[]{Eval("TitleID"),Eval("TitleTypeID")})) %>")'>
                            查看</a><a href='javascript:showWindow("选项管理","<%# this.ActionHref(String.Format("QuSelectionAdd.aspx?TitleID={0}&QuestionType={1}", new Object[]{Eval("TitleID"),Eval("TitleTypeID")})) %>")'>
                                选项管理</a>
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
