<%@ Page Title="课程查询" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="CourseQuery.aspx.cs" Inherits="ETMS.WebApp.Manage.Resource.CourseManage.CourseQuery" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
                id="tableQueryControlList">
                <tr>
                    <th width="120">
                        课程编码：
                    </th>
                    <td width="130">
                        <asp:TextBox ID="txt_CourseCode" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                    <th width="120">
                        课程名称：
                    </th>
                    <td class="Search_Area">
                        <asp:TextBox ID="txt_CourseName" runat="server" CssClass="inputbox_120 floatleft"></asp:TextBox>
                        <asp:UpdatePanel ID="upQuery" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <a href="javascript:hideGridview()" class="dropdownico " id="Highsearch">高级搜索</a>
                    </td>
                </tr>
                <tr>
                    <th>
                        课程类型：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="ddl_CourseTypeID" DictionaryType="Dic_Sys_CourseType" CssText="select_120" />
                    </td>
                    <th>
                        课程状态：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" TabIndex=1 ID="ddl_CourseStatus" DictionaryType="Dic_Status" CssText="select_120" IsShowAll="true" />
                    </td>
                </tr>
                 <tr>
                    <th>
                        创建时间：
                    </th>
                    <td colspan="3">
                        <cc1:DateTimeTextBox ID="begin_CreateTime" runat="server" EndTimeControlID="end_CreateTime"
                            DataTimeFormat="%Y-%M-%D" ></cc1:DateTimeTextBox>
                        至
                        <cc1:DateTimeTextBox ID="end_CreateTime" runat="server" BeginTimeControlID="begin_CreateTime"
                            DataTimeFormat="%Y-%M-%D" ></cc1:DateTimeTextBox>
                    </td>                   
                </tr>
                <tr>                    
                    <th class="hide_norm">
                        是否公开：
                    </th>
                    <td class="hide_norm" colspan="3">
                        <cc1:DictionaryDropDownList runat="server" ID="ddl_IsPublic"  TabIndex=3 DictionaryType="Dic_TrueOrFalse" CssText="select_120" IsShowAll="true"/>
                    </td>
                </tr>               
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <!--翻页-->
                    <div class="dv_pagePanel">
                        <div class="dv_pageControl">
                            <uc2:PageSet ID="PageSet1" runat="server" />
                        </div>
                    </div>
                    <!--列表 begin-->
                    <cc1:CustomGridView ID="gvList" runat="server" CssClass="GridviewGray" EmptyDataSourceTip="没有任何记录！"
                        AutoCreateColumnInsertIndex="0" AutoGenerateColumns="False" CurrentPageIndex="1"
                        CustomAllowPaging="False" IsEmpty="False" TotalRecordCount="0" 
                        DataKeyNames="CourseID" onrowdatabound="gvList_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="CourseCode" HeaderText="课程编码" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft field12"  />
                            <asp:TemplateField HeaderText="课程名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                                <ItemTemplate>                                    
                                    <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="10" Text='<%# Eval("CourseName")%>'></cc1:ShortTextLabel>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="课程状态"  HeaderStyle-Width="60">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("CourseStatus").ToString()=="1"?"启用":"停用" %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="课程类型"  HeaderStyle-Width="60"  ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                                <ItemTemplate>
                                    <cc1:DictionaryLabel runat="server" ID="ddl_CourseTypeID" DictionaryType="Dic_Sys_CourseType" FieldIDValue='<%# Eval("CourseTypeID") %>'/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="所属机构" AccessibleHeaderText="CreateOrgID"  HeaderStyle-Width="100"  ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                                <ItemTemplate>
                                    <cc1:DictionaryLabel runat="server" ID="ddl_BelongOrgID" DictionaryType="vw_Dic_Sys_Organization" FieldIDValue='<%# Eval("CreateOrgID") %>'/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="讲师"  HeaderStyle-Width="40">                                
                                <ItemTemplate>
                                    <a href="javascript:showWindow('课程讲师','<%# this.ActionHref(string.Format("~/Resource/CourseManage/CourseQueryTeacher.aspx?CourseID={0}",Eval("CourseID").ToString())) %>')"> <%# GetCourseTeacherNum(Eval("CourseID").ToGuid())%></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="操作" HeaderStyle-Width="40">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>                                    
                                    <a href='<%# this.ActionHref(string.Format("CourseView.aspx?Flag=1&CourseID={0}",Eval("CourseID").ToString())) %>'>查看</a>
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

