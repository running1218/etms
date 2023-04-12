<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CoursewareList.ascx.cs"
    Inherits="Resource_CourseManage_Controls_CoursewareList" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<div class="dv_searchlist">
    <!--翻页-->
    <div class="dv_pagePanel">
        <div class="dv_pageControl">
            <uc2:PageSet ID="PageSet1" runat="server" />
        </div>
    </div>
    <!--列表 begin-->
    <cc1:CustomGridView ID="CustomGridView1" runat="server" CssClass="GridviewGray" EmptyDataSourceTip="没有任何记录！"
        AutoCreateColumnInsertIndex="0" AutoGenerateColumns="False" CurrentPageIndex="1"
        CustomAllowPaging="False" IsEmpty="False" TotalRecordCount="0" DataKeyNames="CoursewareID">
        <Columns>
            <asp:TemplateField HeaderText="课程名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft field12">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <cc1:ShortTextLabel ID="lblCourseName" runat="server" Text='<%# Eval("CourseName")%>'></cc1:ShortTextLabel>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="课件名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft field12">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <cc1:ShortTextLabel ID="lblCoursewareName" runat="server" Text='<%# Eval("CoursewareName")%>'></cc1:ShortTextLabel>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="课件类型" HeaderStyle-Width="80">
                <ItemTemplate>
                    <asp:Label ID="lblCoursewareType" runat="server" Text='<%# Eval("CoursewareTypeID").ToString()=="1"?"SCORM标准":"非SCORM标准" %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="是否有效" HeaderStyle-Width="80">
                <ItemTemplate>
                    <asp:Label ID="lblCanUse" runat="server" Text='<%# Eval("CoursewarePath").ToString()=="" ? "<font color=\"#C03219\">无效</font>":"有效"  %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="课件状态" HeaderStyle-Width="60">
                <ItemTemplate>
                    <asp:Label ID="lblCoursewareStatus" runat="server" Text='<%# Eval("CoursewareStatus").ToString()=="1"?"启用":"停用" %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="创建时间" HeaderStyle-Width="90">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="lblCreateTime" runat="server" Text='<%#Eval("CreateTime").ToDate() %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="CreateUser" HeaderText="创建者" HeaderStyle-CssClass="field8 alignleft"
                ItemStyle-CssClass="alignleft" />
            <asp:TemplateField HeaderText="操作" HeaderStyle-Width="30">
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <a href='<%# this.ActionHref(string.Format("~/Courseware/OpenCourseware.aspx?CourseWareID={0}&CourseID={1}",Eval("CoursewareID"),Eval("CourseID"))) %>'
                        class="btn_Study" target="_blank">浏览</a>
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
