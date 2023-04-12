<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ResourceList.ascx.cs"
    Inherits="TraningImplement_ProjectCourseResourceBatch_Controls_ResourceList" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<!--查找条件-->
<div class="dv_searchbox" id="ResourceList">
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
                <a href="javascript:hideGridviewByID('ResourceList')" class="dropdownico" id="Highsearch_ResourceList">
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
                资源学习开始日期：
            </th>
            <td colspan="3">
                <cc1:DateTimeTextBox ID="begin_icr999ResBeginTime" runat="server" EndTimeControlID="end_icr999ResBeginTime"></cc1:DateTimeTextBox>
                至
                <cc1:DateTimeTextBox ID="end_icr999ResBeginTime" runat="server" BeginTimeControlID="begin_icr999ResBeginTime"></cc1:DateTimeTextBox>
            </td>
        </tr>
        <tr>
            <th width="100">
                项目资源添加日期：
            </th>
            <td colspan="3">
                <cc1:DateTimeTextBox ID="begin_icr999CreateTime" runat="server" EndTimeControlID="end_icr999CreateTime"></cc1:DateTimeTextBox>
                至
                <cc1:DateTimeTextBox ID="end_icr999CreateTime" runat="server" BeginTimeControlID="begin_icr999CreateTime"></cc1:DateTimeTextBox>
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
            <cc1:CustomButton ID="btn_Close" runat="server" CssClass="btn_Stop2" Text="停用" ConfirmMessage="确定停用选中的项目课程资源吗？"
                EnableConfirm="true" OnClick="btn_Close_Click"></cc1:CustomButton><asp:Button ID="btnSetLearnCycle1" CssClass="btn_amendperiod"
                    runat="server" Text="修改资源学习周期" OnClick="btnSetLearnCycle1_Click" />
        </div>
        <div class="dv_pageControl">
            <uc2:PageSet ID="PageSet1" runat="server" />
        </div>
    </div>
    <!--列表 begin-->
    <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
        CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0"
        DataKeyNames="ItemCourseResID">
        <Columns>
            <asp:TemplateField HeaderText="">
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle HorizontalAlign="Center" Width="20" />
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" />
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
            <asp:TemplateField HeaderText="课程学习周期" HeaderStyle-Width="140">
                <ItemTemplate>
                    <cc1:ShortTextLabel ID="lblCourseTime" runat="server" ShowTextNum="6" Text='<%# string.Format("{0}至{1}", Eval("CourseBeginTime").ToDate(),Eval("CourseEndTime").ToDate())%>'></cc1:ShortTextLabel>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="资源类型" HeaderStyle-Width="55">
                <ItemTemplate>
                    <cc1:ShortTextLabel ID="lblCourseResTypeName" runat="server" ShowTextNum="6" Text='<%# Eval("CourseResTypeName")%>'></cc1:ShortTextLabel>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="资源名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                <ItemTemplate>
                    <a href='<%# getResUrl(Eval("CourseResID").ToString() ,Eval("CourseID").ToString(),Eval("CourseResTypeID").ToString()) %>'
                        class="btn_Study" target="_blank">
                        <cc1:ShortTextLabel ID="lblResName" runat="server" ShowTextNum="6" Text='<%# Eval("ResName")%>'></cc1:ShortTextLabel></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="课件类型" HeaderStyle-Width="80">
                <ItemTemplate>
                    <cc1:ShortTextLabel ID="lblCoursewareTypeName" runat="server" ShowTextNum="6" Text='<%# Eval("CoursewareTypeName")%>'></cc1:ShortTextLabel>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="资源学习周期" HeaderStyle-Width="140">
                <ItemTemplate>
                    <cc1:ShortTextLabel ID="lblResTime" runat="server" ShowTextNum="6" Text='<%# string.Format("{0}至{1}", Eval("ResBeginTime").ToDate(),Eval("ResEndTime").ToDate())%>'></cc1:ShortTextLabel>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="添加日期" HeaderStyle-Width="70">
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
