<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OnlineHomeworkListView.ascx.cs"
    Inherits="Resource_CourseManage_Controls_OnlineHomeworkListView" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<!--查找结果-->
<div class="dv_searchlist">
    <!--翻页-->
    <div class="dv_pagePanel" id="divPage1">
        <div class="dv_selectAll">
        </div>
        <div class="dv_pageControl">
            <uc2:PageSet ID="PageSet1" runat="server" />
        </div>
    </div>
    <!--列表 begin-->
    <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
        CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="1"
        DataKeyNames="OnLineJobID">
        <Columns>
            <asp:TemplateField HeaderText="作业名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                <ItemTemplate>
                    <cc1:ShortTextLabel ID="lblOnLineJobName" runat="server" ShowTextNum="10" Text='<%# Eval("OnLineJobName")%>'></cc1:ShortTextLabel>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="CreateUser" HeaderText="创建人" ItemStyle-CssClass="alignleft"
                HeaderStyle-CssClass="alignleft" HeaderStyle-Width="60" />
            <asp:TemplateField HeaderText="创建时间" HeaderStyle-Width="80">
                <ItemTemplate>
                    <%# Eval("CreateTime").ToDate()%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作" HeaderStyle-Width="60">
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <a href='<%#  this.ActionHref(string.Format("~/QuestionDB/Testpaper/TestpaperView.aspx?ExerciseType=3&ExerciseID={0}", Eval("OnLineJobID")))%>'
                        target="_blank">预览</a>
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
