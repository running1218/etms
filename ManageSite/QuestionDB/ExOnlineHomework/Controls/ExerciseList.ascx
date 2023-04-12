<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ExerciseList.ascx.cs"
    Inherits="QuestionDB_ExOnlineHomework_Controls_ExerciseList" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<div>
    <!--查找条件-->
    <div class="dv_searchbox">
        <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
            id="tableQueryControlList">
            <tr>
                
                <th width="120">
                    课程名称：
                </th>
                <td>
                    <asp:TextBox ID="txt_CourseName" runat="server" CssClass="inputbox_120 floatleft" MaxLength="50"></asp:TextBox>
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
                <input type="button" class="btn_Add" value="新增" onclick="javascript:showWindow('新增在线作业','ExerciseAdd.aspx','800','480')" />
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
                <asp:TemplateField HeaderText="在线作业" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <cc1:ShortTextLabel ID="lblOnLineJobName" runat="server" ShowTextNum="20" Text='<%# Eval("OnLineJobName")%>'></cc1:ShortTextLabel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="课程名称" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="10" Text='<%# Eval("CourseName")%>'></cc1:ShortTextLabel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="当前状态" HeaderStyle-Width="60">
                    <ItemTemplate>
                        <cc1:DictionaryLabel ID="lblOnLineJobStatus" DictionaryType="Dic_Status" FieldIDValue='<%# Eval("OnLineJobStatus") %>'
                            runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="创建时间" HeaderStyle-Width="80">
                    <ItemTemplate>
                        <%# Eval("CreateTime").ToDate()%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="总分" HeaderStyle-Width="50" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright">
                    <ItemTemplate>
                        <%# getSumScore(Eval("TestPaperID").ToString(), 100)%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CreateUser" HeaderText="创建者"    HeaderStyle-CssClass="field8 alignleft" ItemStyle-CssClass="alignleft"/>
                <asp:TemplateField HeaderText="操作" HeaderStyle-Width="130">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <a href="javascript:showWindow('编辑在线作业','<%# this.ActionHref(string.Format("~/QuestionDB/ExOnlineHomework/ExerciseEdit.aspx?OnlineJobID={0}", Eval("OnlineJobID")))%>')">编辑</a><cc1:CustomLinkButton runat="server" ID="lbtn_Del" CommandArgument='<%# Eval("OnlineJobID") %>' CommandName="Del" Text="删除" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="确定要删除吗？" /><a href='<%#  this.ActionHref(string.Format("~/QuestionDB/Testpaper/TestpaperView.aspx?ExerciseType={0}&ExerciseID={1}", getExerciseType(), Eval("OnlineJobID").ToString()))%>'  target="_blank">浏览</a><a href='<%#  this.ActionHref(string.Format("~/QuestionDB/Testpaper/AddTestPaper.aspx?ExerciseType={0}&ExerciseID={1}", getExerciseType(), Eval("OnlineJobID").ToString()))%>' >出题</a>
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
