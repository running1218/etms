<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="QuestionList.aspx.cs" Inherits="Questionnaire_QuestionManage_QuestionList" %>

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
                <input type="button" id="btn_add1" class="btn_SingleSelection" value="单选" onclick='javascript:showWindow("新增单选题","<%= this.ActionHref(String.Format("QuestionHandler.aspx?Action=add&QueryID={0}&QuestionType=1&ColumnID={1}", new Object[]{this.QueryID,this.ColumnID})) %>",650,500)' />
                <input type="button" id="Button1" class="btn_Multi-Choice" value="多选" onclick='javascript:showWindow("新增多选题","<%= this.ActionHref(String.Format("QuestionHandler.aspx?Action=add&QueryID={0}&QuestionType=2&ColumnID={1}", new Object[]{this.QueryID,this.ColumnID})) %>",650,500)' />
                <input type="button" id="Button2" class="btn_EssayQuestions" value="简答" onclick='javascript:showWindow("新增简答题","<%= this.ActionHref(String.Format("QuestionHandler.aspx?Action=add&QueryID={0}&QuestionType=4&ColumnID={1}", new Object[]{this.QueryID,this.ColumnID})) %>",650,500)' />
                <input type="button" id="Button3" class="btn_Sort" value="排序" onclick='javascript:showWindow("题目排序","<%= this.ActionHref(String.Format("QuestionSort.aspx?QueryID={0}", new Object[]{this.QueryID})) %>",650,500)' />
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
                <asp:TemplateField HeaderText="序号">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="40" />
                    <ItemTemplate>
                        <asp:Label ID="LabNo" runat="server" Text='<%#  ((this.PageSet1.PageIndex -1) * this.PageSet1.PageSize) + Container.DataItemIndex+1 %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="题目" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft"
                    DataField="TitleName" />
                <asp:TemplateField HeaderText="题目类型">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="60" />
                    <ItemTemplate>
                        <%# (1==(int)Eval("TitleTypeID"))?"单选": ((2==(int)Eval("TitleTypeID"))?"多选":"问答")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="题目序号" DataField="TitleNo" HeaderStyle-Width="60" ItemStyle-CssClass="alignright"
                    HeaderStyle-CssClass="alignright" />
                <asp:TemplateField HeaderText="操作" HeaderStyle-Width="60">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <% if (!isJoinQuery())
                           { %>
                        <a href='javascript:showWindow("<%#  (1==(int)Eval("TitleTypeID"))?"单选": ((2==(int)Eval("TitleTypeID"))?"多选":"问答") %>题编辑","<%# this.ActionHref(String.Format("QuestionHandler.aspx?Action=edit&QuestionID={0}&QuestionType={1}", new Object[]{Eval("TitleID"),Eval("TitleTypeID")})) %>",650,500)'>
                            编辑</a>
                        <%} %>
                        <a href='javascript:showWindow("<%#  (1==(int)Eval("TitleTypeID"))?"单选": ((2==(int)Eval("TitleTypeID"))?"多选":"问答") %>题查看","<%# this.ActionHref(String.Format("QuestionHandler.aspx?Action=view&QuestionID={0}&QuestionType={1}", new Object[]{Eval("TitleID"),Eval("TitleTypeID")})) %>",650,500)'>
                            查看</a>
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
