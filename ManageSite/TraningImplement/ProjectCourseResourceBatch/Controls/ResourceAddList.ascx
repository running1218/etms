<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ResourceAddList.ascx.cs" Inherits="TraningImplement_ProjectCourseResourceBatch_Controls_ResourceAddList" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<!--查找条件-->
<div class="dv_searchbox" id="ResourceAddList">
    <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
        id="tableQueryControlList">
        <tr>
            <th width="120">
                项目编码：
            </th>
            <td width="130">
                <asp:TextBox ID="txt_i999ItemCode" runat="server" CssClass="inputbox_120" MaxLength="50"></asp:TextBox>
            </td>
            <th width="120">
                项目名称：
            </th>
            <td width="130">
                <asp:TextBox ID="txt_i999ItemName" runat="server" CssClass="inputbox_120" MaxLength="50"></asp:TextBox>
                <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
                <a href="javascript:hideGridviewByID('ResourceAddList')" class="dropdownico" id="Highsearch_ResourceAddList">
                    高级搜索</a>
            </td>
        </tr>
        <tr>
            <th width="120">
                课程编码：
            </th>
            <td width="130">
                <asp:TextBox ID="txt_c999CourseCode" runat="server" CssClass="inputbox_120" MaxLength="50"></asp:TextBox>
            </td>
            <th width="120">
                课程名称：
            </th>
            <td width="130">
                <asp:TextBox ID="txt_c999CourseName" runat="server" CssClass="inputbox_120" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th width="100">
                资源类型：
            </th>
            <td>
                <%--<cc1:DictionaryDropDownList runat="server" ID="ddl_icr999CourseResTypeID" DictionaryType="Dic_Sys_CourseResType"
                    IsShowAll="true" />--%>
                <asp:DropDownList runat="server" ID="ddl_cr999CourseResTypeID">
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem Value="1">在线课件</asp:ListItem>
                    <asp:ListItem Value="2">在线作业</asp:ListItem>
                    <asp:ListItem Value="5">在线测试</asp:ListItem>
                </asp:DropDownList>
            </td>
            <th width="100">
                资源名称：
            </th>
            <td>
                <asp:TextBox ID="txt_cr999ResName" runat="server" CssClass="inputbox_120" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th width="100">
                资源创建日期：
            </th>
            <td colspan="3">
                <cc1:DateTimeTextBox ID="begin_cr999CreateTime" runat="server" EndTimeControlID="end_cr999CreateTime"></cc1:DateTimeTextBox>
                至
                <cc1:DateTimeTextBox ID="end_cr999CreateTime" runat="server" BeginTimeControlID="begin_cr999CreateTime"></cc1:DateTimeTextBox>
            </td>
        </tr>
    </table>
</div>
<!--查找结果-->
<div class="dv_searchlist">
    <!--翻页-->
    <div class="dv_pagePanel" id="divPage1">
        <div class="dv_selectAll">
            <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" />
            <asp:Button ID="btnAdd" runat="server" Text="确定" CssClass="btn_Ok" OnClick="btnAdd_Click"
                    ValidationGroup="Saves" />
        </div>
        <div class="dv_pageControl">
            <uc2:PageSet ID="PageSet1" runat="server" />
        </div>
    </div>
    <!--列表 begin-->
    <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
        CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0">
        <Columns>
            <asp:TemplateField HeaderText="">
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle HorizontalAlign="Center" Width="20" />
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" />
                    <asp:HiddenField ID="hfTrainingItemCourseID" runat="server" Value='<%# Eval("TrainingItemCourseID")%>' />
                    <asp:HiddenField ID="hfCourseResTypeID" runat="server" Value='<%# Eval("CourseResTypeID")%>' />
                    <asp:HiddenField ID="hfResID" runat="server" Value='<%# Eval("ResID")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="项目名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                <ItemTemplate>
                    <cc1:ShortTextLabel ID="lblItemName" runat="server" ShowTextNum="6" Text='<%# Eval("ItemName")%>'></cc1:ShortTextLabel>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="课程名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                <ItemTemplate>
                    <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="6" Text='<%# Eval("CourseName")%>'></cc1:ShortTextLabel>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="课程开始日期" HeaderStyle-Width="80">
                <ItemTemplate>
                    <cc1:ShortTextLabel ID="lblCourseBeginTime" runat="server" ShowTextNum="6" Text='<%# Eval("CourseBeginTime").ToDate()%>'></cc1:ShortTextLabel>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="课程结束日期" HeaderStyle-Width="80">
                <ItemTemplate>
                    <cc1:ShortTextLabel ID="lblCourseEndTime" runat="server" ShowTextNum="6" Text='<%# Eval("CourseEndTime").ToDate()%>'></cc1:ShortTextLabel>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="资源类型" HeaderStyle-Width="70">
                <ItemTemplate>
                    <cc1:ShortTextLabel ID="lblCourseResTypeName" runat="server" ShowTextNum="6" Text='<%# Eval("CourseResTypeName")%>'></cc1:ShortTextLabel>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="资源名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                <ItemTemplate>
                    <a href='<%# getResUrl(Eval("ResID").ToString() ,Eval("CourseID").ToString(),Eval("CourseResTypeID").ToString()) %>'
                        class="btn_Study" target="_blank">
                        <cc1:ShortTextLabel ID="lblResName" runat="server" ShowTextNum="6" Text='<%# Eval("ResName")%>'></cc1:ShortTextLabel></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="课件类型" HeaderStyle-Width="80">
                <ItemTemplate>
                    <cc1:ShortTextLabel ID="lblCoursewareTypeName" runat="server" ShowTextNum="6" Text='<%# Eval("CoursewareTypeName")%>'></cc1:ShortTextLabel>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="资源创建日期" HeaderStyle-Width="80">
                <ItemTemplate>
                    <cc1:ShortTextLabel ID="lblCreateTime" runat="server" ShowTextNum="6" Text='<%# Eval("CreateTime").ToDate()%>'></cc1:ShortTextLabel>
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