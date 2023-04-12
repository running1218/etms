<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemDB2BeSelect.ascx.cs"
    Inherits="QuestionDB_TestPaper_Controls_ItemDB2BeSelect" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<div style="background-color: #fbf1ef; padding: 10px; border: 1px solid #c03218;">
    <div style="text-indent: 45px; font-weight: bold; padding: 5px;">
        【手动选题】</div>
    <div id="ItemSelectDiv" style="display: block;">
        <!--查找未选的题目-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <th style="width: 60px;">
                        题型：
                    </th>
                    <td style="width: 80px;">
                        <cc1:DictionaryDropDownList runat="server" ID="ddlQuestionType" DictionaryType="Dic_QuestionType"
                            CssClass="select_60" />
                    </td>
                    <th style="width: 60px;">
                        难度：
                    </th>
                    <td style="width: 80px;">
                        <cc1:DictionaryDropDownList runat="server" ID="ddlDifficulty" DictionaryType="Dic_DegreeDifficulty"
                            CssClass="select_60" />
                    </td>
                    <th style="width: 60px;">
                        题目：
                    </th>
                    <td>
                        <asp:TextBox ID="txtQuestionTitle" runat="server" CssClass="inputbox_120"></asp:TextBox>
                        <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="dv_searchlist">
            <%--<asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>
            <!--翻页-->
            <div class="dv_pagePanel" id="divPage1">
                <div class="dv_selectAll">
                    <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" />
                    <asp:LinkButton ID="lbtnSave" runat="server" CssClass="btn_Choosing" OnClick="lbtnSave_Click">选择题目</asp:LinkButton>
                </div>
                <div class="dv_pageControl" style="float: right;">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" CssClass="GridviewGray" EmptyDataSourceTip="没有任何记录！"
                AutoCreateColumnInsertIndex="0" AutoGenerateColumns="False" CurrentPageIndex="1"
                CustomAllowPaging="False" IsEmpty="False" TotalRecordCount="0" DataKeyNames="QuestionID,QuestionType">
                <Columns>
                    <asp:TemplateField HeaderText="">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" Width="18" />
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="题型" HeaderStyle-Width="60" ItemStyle-CssClass="aligncenter"
                        HeaderStyle-CssClass="aligncenter">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblQuestionType" DictionaryType="Dic_QuestionType" FieldIDValue='<%# (int)((ETMS.Components.Exam.API.Entity.ItemBank.QuestionType)Eval("QuestionType")) %>'
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="难度" HeaderStyle-Width="60" HeaderStyle-CssClass="aligncenter">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblDifficulty" DictionaryType="Dic_DegreeDifficulty" FieldIDValue='<%# Eval("Difficulty") %>'
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="题目名称" HeaderStyle-CssClass="alignleft widthauto" ItemStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblQuestionTitle" runat="server" ShowTextNum="40" Text='<%# Eval("QuestionTitle")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </cc1:CustomGridView>
            <!--列表 end-->
            <%--     </ContentTemplate>
    </asp:UpdatePanel>--%>
            <!--翻页-->
            <div class="dv_splitLine">
            </div>
            <div class="dv_pagePanel" id="divPage2">
            </div>
        </div>
    </div>
</div>
<script type="text/javascript">

    createPageControl();
    //$("#divPage2").addClass("hide");
</script>
