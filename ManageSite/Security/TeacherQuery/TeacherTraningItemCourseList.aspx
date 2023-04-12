<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="TeacherTraningItemCourseList.aspx.cs" Inherits="Security_TeacherQuery_TeacherTraningItemCourseList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" runat="Server">
<asp:LinkButton ID="lbtnReturn" runat="server" CssClass="btn_Return" Text="返回"></asp:LinkButton>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--查找条件-->
        <div class="dv_GradeviewList fixedTable">
            <table id="Table1" class="" border="0" cellpadding="0" cellspacing="0"
                runat="server">
                <tr>
                    <th width="100">
                        讲师姓名：
                    </th>
                    <td width="200">
                        <asp:Label ID="lblTeacherName" runat="server"></asp:Label>
                    </td>
                    <th width="100">
                        <asp:Label ID="lblOrgName" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Label>/
                        <asp:Literal ID="ltlDepartment" runat="server" Text="<%$ Resources:UIResource, ui_org%>"></asp:Literal>：
                    </th>
                    <td>
                        <asp:Label ID="lblOrg" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        课程开始日期：
                    </th>
                    <td width="200">
                        <asp:Label ID="lblItemDate" runat="server"></asp:Label>
                    </td>
                    <th width="100">
                        负责项目总课时：
                    </th>
                    <td>
                        <asp:Label ID="lblCourseHours" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
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
                CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0"
                DataKeyNames="">
                <Columns>
                    <asp:TemplateField HeaderText="项目名称" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblItemName" runat="server" ShowTextNum="10" Text='<%# Eval("ItemName")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程名称" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="10" Text='<%# Eval("CourseName")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="状态" HeaderStyle-Width="40">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="dlblCourseStatus" DictionaryType="Dic_Status" FieldIDValue='<%# Eval("CourseStatus") %>'
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程属性" HeaderStyle-Width="60" HeaderStyle-CssClass="hide" ItemStyle-CssClass="hide">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="DictionaryCourseAttr" DictionaryType="Dic_Sys_CourseAttr"
                                FieldIDValue='<%# Eval("CourseAttrID") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="授课方式" HeaderStyle-Width="60" HeaderStyle-CssClass="hide" ItemStyle-CssClass="hide">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="DictionaryLabel1" DictionaryType="Dic_Sys_TeachModel" FieldIDValue='<%# Eval("TeachModelID") %>'
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="培训开始日期" HeaderStyle-Width="90">
                        <ItemTemplate>
                            <%# Eval("CourseBeginTime").ToDate()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="培训截止日期" HeaderStyle-Width="90">
                        <ItemTemplate>
                            <%# Eval("CourseEndTime").ToDate()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="CourseHours" HeaderText="课程学时" HeaderStyle-Width="60"  ItemStyle-CssClass="alignright" HeaderStyle-CssClass="alignright"/>
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
