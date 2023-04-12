<%@ Control Language="C#" AutoEventWireup="true" CodeFile="QuestionList.ascx.cs"
    Inherits="QuestionDB_Controls_QuestionList" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<%@ Register Src="~/Controls/ChooseCourseDropdown.ascx" TagName="ChooseCourseDropdown"
    TagPrefix="uc1" %>
<!--查找条件-->
<div class="dv_searchbox noneetHide">
    <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <th width="120">
                课程名称：
            </th>
            <td colspan="4">
                <asp:Literal ID="ltlCourseName" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th style="width: 80px;">
                难&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;度：
            </th>
            <td>
                <cc1:DictionaryDropDownList runat="server" ID="ddl_Difficulty" DictionaryType="Dic_DegreeDifficulty"
                    CssClass="select_60" />
            </td>
            <th>
                题目名称：
            </th>
            <td>
                <asp:TextBox ID="txt_QuestionTitle" runat="server" CssClass="inputbox_120"></asp:TextBox>
                <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>
</div>
<!--查找结果-->
<div class="dv_searchlist">
    <!--翻页-->
    <div class="dv_pagePanel">
        <div class="dv_selectAll">
            <asp:Literal ID="ltlBtnAdd" runat="server"></asp:Literal>
            <cc1:CustomButton ID="lbtnImport" runat="server" CssClass="btn_Import" OnClientClick=""
                Text="导入" EnableConfirm="false"></cc1:CustomButton>
        </div>
        <div class="dv_pageControl">
            <uc2:PageSet ID="PageSet1" runat="server" />
        </div>
    </div>
    <!--列表 begin-->
    <cc1:CustomGridView ID="CustomGridView1" runat="server" CssClass="GridviewGray" EmptyDataSourceTip="没有任何记录！"
        AutoCreateColumnInsertIndex="0" AutoGenerateColumns="False" CurrentPageIndex="1"
        CustomAllowPaging="False" IsEmpty="False" TotalRecordCount="0" DataKeyNames="QuestionID"
        OnRowCommand="CustomGridView1_RowCommand">
        <Columns>
            <asp:TemplateField HeaderText="序号" HeaderStyle-Width="40">
                <ItemStyle HorizontalAlign="Center" Width="60" />
                <HeaderStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="LabNo" runat="server" Text='<%#  ((this.PageSet1.PageIndex -1) * this.PageSet1.PageSize) + Container.DataItemIndex+1 %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="题目名称" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                <ItemTemplate>
                    <cc1:ShortTextLabel ID="lblQuestionTitle" runat="server" ShowTextNum="40" Text='<%# Eval("QuestionTitle")%>'></cc1:ShortTextLabel>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="难度" HeaderStyle-Width="40">
                <ItemTemplate>
                    <cc1:DictionaryLabel ID="lblDifficulty" DictionaryType="Dic_DegreeDifficulty" FieldIDValue='<%# Eval("Difficulty") %>'
                        runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作" HeaderStyle-Width="100">
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <%# GetUrl(Eval("QuestionID").ToString())%><cc1:CustomLinkButton runat="server" ID="lbtn_Del"
                        CommandArgument='<%# Eval("QuestionID") %>' CommandName="Del" Text="删除" EnableConfirm="true"
                        ConfirmTitle="提示" ConfirmMessage="确定要删除吗？" />
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
