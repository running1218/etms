<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ResourceExerciseList.ascx.cs" Inherits="ResourceExerciseList" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<div>
    <!--查找条件-->
    <div class="dv_searchbox">
        <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
            id="tableQueryControlList">
            <tr>
                <th width="15%">
                    课程编码：
                </th>
                <td width="20%">
                    <asp:Label ID="lblCourseCode" runat="server"></asp:Label>
                </td>
                <th width="15%">
                    课程名称：
                </th>
                <td>
                    <asp:Label ID="lblCourseName" runat="server"></asp:Label>
                    <span class="hide">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn_Search" Text="查询" OnClick="btnSearch_Click" />
                    </span>
                </td>    
            </tr>
        </table>
    </div>
    <!--查找结果-->
    <div class="dv_searchlist">
        <!--翻页-->
        <div class="dv_pagePanel">
            <div class="dv_selectAll">                
                <asp:Button ID="btnAdd" runat="server" CssClass="btn_Add" Text="新增" />
            </div>
            <div class="dv_pageControl">
                <uc2:PageSet ID="PageSet1" runat="server" />
            </div>
        </div>
        <!--列表 begin-->
        <cc1:CustomGridView ID="CustomGridView1" runat="server" CssClass="GridviewGray" EmptyDataSourceTip="没有任何记录！"
            AutoCreateColumnInsertIndex="0" AutoGenerateColumns="False" CurrentPageIndex="1"
            CustomAllowPaging="False" IsEmpty="False" TotalRecordCount="0" DataKeyNames="OnlineJobID"
            OnRowCommand="CustomGridView1_RowCommand">
            <Columns>
          
                <asp:BoundField DataField="OnLineJobName" HeaderText="在线作业" />
                <asp:TemplateField HeaderText="当前状态">
                    <ItemTemplate>
                        <cc1:DictionaryLabel ID="lblElearningMapType" DictionaryType="Dic_Status" FieldIDValue='<%# Eval("OnLineJobStatus") %>'
                            runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CreateTime" HeaderText="创建时间" />
                <asp:BoundField DataField="CreateUser" HeaderText="创建者" />
                <asp:TemplateField HeaderText="操作">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <a href="javascript:showWindow('编辑在线作业','<%# this.ActionHref(string.Format("~/QuestionDB/ExOnlineHomework/ExerciseEdit.aspx?OnlineJobID={0}", Eval("OnlineJobID")))%>')">编辑</a><cc1:CustomLinkButton runat="server" ID="lbtn_Del" CommandArgument='<%# Eval("OnlineJobID") %>' CommandName="Del" Text="删除" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="确定要删除吗？" /><a
                                    href="javascript:showWindow('查看在线作业','<%# this.ActionHref(string.Format("~/QuestionDB/ExOnlineHomework/ExerciseView.aspx?OnlineJobID={0}", Eval("OnlineJobID")))%>','800','400')">查看</a>
                                    <%--<a href='<%# this.ActionHref(string.Format("~/QuestionDB/TestPaper/AddTestPaper.aspx?ExerciseType={0}&ExerciseID={1}", getExerciseType(), Convert.ToString(Eval("OnlineJobID"))))%>'>出题</a>--%>
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