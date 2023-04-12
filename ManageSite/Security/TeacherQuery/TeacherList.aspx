<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="TeacherList.aspx.cs" Inherits="Security_TeacherQuery_TeacherList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray th120" border="0" cellpadding="0" cellspacing="0" runat="server"
                id="tableQueryControlList">
                <tr>
                    <th width="100">
                        讲师状态：
                    </th>
                    <td width="200">
                        <cc1:DictionaryDropDownList runat="server" ID="ddl_IsUse" DictionaryType="Dic_Status"
                            IsShowAll="true" />
                    </td>
                    <th width="100">
                        讲师姓名：
                    </th>
                    <td class="Search_Area">
                        <asp:TextBox ID="txt_TeacherName" runat="server" CssClass="inputbox_120 floatleft"
                            MaxLength="50"></asp:TextBox><asp:Button ID="btnSearch" CssClass="btn_Search" runat="server"
                                Text="查询" OnClick="btnSearch_Click" /><a href="javascript:hideGridview()" class="dropdownico"
                                    id="Highsearch">高级搜索</a>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        负责课程：
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txt_CourseName" runat="server" CssClass="inputbox_120" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        课程开始日期：
                    </th>
                    <td colspan="3">
                        <cc1:DateTimeTextBox ID="begin_CourseBeginTime" runat="server" EndTimeControlID="end_CourseBeginTime"></cc1:DateTimeTextBox>
                        至
                        <cc1:DateTimeTextBox ID="end_CourseBeginTime" runat="server" BeginTimeControlID="begin_CourseBeginTime"></cc1:DateTimeTextBox>
                    </td>
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel" id="divPage1">
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
                CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0"
                DataKeyNames="TeacherID" OnRowDataBound="CustomGridView1_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="讲师姓名"   ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft field8"> 
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnRealName" runat="server" Text='<%# Eval("RealName") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="状态" HeaderStyle-Width="40">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblIsUse" DictionaryType="Dic_Status" FieldIDValue='<%# Eval("IsUse") %>'
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="来源" HeaderStyle-Width="40">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblTeacherSource" DictionaryType="Dic_Sys_TeacherSource" FieldIDValue='<%# Eval("TeacherSourceID") %>'
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="讲师等级" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblTeacherLevel" DictionaryType="Dic_Sys_TeacherLevel" FieldIDValue='<%# Eval("TeacherLevelID") %>'
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="负责课程数" HeaderStyle-Width="80" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnTeachNum" runat="server" Text='<%# Eval("TeachNum")%>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="参与项目数" HeaderStyle-Width="80" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnItemNum" runat="server" Text='<%# Eval("ItemNum")%>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="负责项目总课次" HeaderStyle-Width="96" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnTeachCourseNum" runat="server" Text='<%# Eval("TeachCourseNum")%>'></asp:LinkButton>
                         </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="负责项目总课时" HeaderStyle-Width="100" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright">
                        <ItemTemplate>
                            <%# Eval("CourseHours")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="项目总课时数"  HeaderStyle-Width="100" HeaderStyle-CssClass="alignright hide" ItemStyle-CssClass="alignright hide">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnCourseHoursNum" runat="server" Text='<%# Eval("CourseHoursNum")%>'></asp:LinkButton>
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
    </div>
</asp:Content>
