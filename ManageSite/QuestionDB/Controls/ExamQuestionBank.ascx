<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ExamQuestionBank.ascx.cs"
    Inherits="QuestionDB_Controls_ExamQuestionBank" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<div>
    <!--查找条件-->
    <div class="dv_searchbox">
        <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
            id="tableQueryControlList">
            <tr>
                <th width="120">
                    课程编码：
                </th>
                <td width="125">
                    <asp:TextBox ID="txt_CourseCode" runat="server" CssClass="inputbox_120" MaxLength="30"></asp:TextBox>
                </td>
                <th width="120">
                    课程名称：
                </th>
                <td>
                    <asp:TextBox ID="txt_CourseName" runat="server" CssClass="inputbox_120" MaxLength="50"></asp:TextBox>
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
            </div>
            <div class="dv_pageControl">
                <uc2:PageSet ID="PageSet1" runat="server" />
            </div>
        </div>
        <!--列表 begin-->
        <cc1:CustomGridView ID="CustomGridView1" runat="server" CssClass="GridviewGray" EmptyDataSourceTip="没有任何记录！"
            AutoCreateColumnInsertIndex="0" AutoGenerateColumns="False" CurrentPageIndex="1"
            CustomAllowPaging="False" IsEmpty="False" TotalRecordCount="0" DataKeyNames="CourseID"
            OnRowDataBound="CustomGridView1_RowDataBound">
            <Columns>
                <asp:BoundField DataField="CourseCode" HeaderText="课程编码" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft field12" />
                <asp:TemplateField HeaderText="课程名称" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum='<%# TextLength %>' Text='<%# Eval("CourseName")%>'></cc1:ShortTextLabel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="单选题" HeaderStyle-Width="60" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright">
                    <ItemTemplate>
                    
                        <a href='<%#  this.ActionHref(string.Format("~/QuestionDB/QuSingleSelection/QuestionList.aspx?CourseID={0}",BasePage.UrlParamEncode(Eval("CourseID").ToString()))) %>'>
                            <%# getQuestionCountByCourseIDAndQuestionType(new Guid(Eval("CourseID").ToString()), ETMS.Components.Exam.API.Entity.ItemBank.QuestionType.SingleChoice)%></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="多选题" HeaderStyle-Width="60" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <a href='<%#  this.ActionHref(string.Format("~/QuestionDB/QuMultipleChoice/QuestionList.aspx?CourseID={0}", BasePage.UrlParamEncode(Eval("CourseID").ToString())))%>'>
                            <%# getQuestionCountByCourseIDAndQuestionType(Eval("CourseID").ToGuid(), (ETMS.Components.Exam.API.Entity.ItemBank.QuestionType.MultipleChoice))%></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="判断题" HeaderStyle-Width="60" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                    
                        <a href='<%#  this.ActionHref(string.Format("~/QuestionDB/QuJudge/QuestionList.aspx?CourseID={0}", BasePage.UrlParamEncode(Eval("CourseID").ToString())))%>'>
                            <%# getQuestionCountByCourseIDAndQuestionType(Eval("CourseID").ToGuid(), (ETMS.Components.Exam.API.Entity.ItemBank.QuestionType.Judgement))%></a>
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
