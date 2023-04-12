<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="TraningProjectView.aspx.cs" Inherits="TraningImplement_TraningProjectQuery_TraningProjectView" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<%@ Register Src="../TraningProjectManager/Controls/TraningProjectView.ascx" TagName="TraningProjectView"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" runat="Server">
    <asp:LinkButton ID="lbtnBack" runat="server" CssClass="btn_Return" Text="返回" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="dv_serviceTab">
        <div class="dv_Tabmenus">
            <ul>
                <li id="Tab_0" onclick="showTab('Tab_0', 'Div_Select_0','selected')"><a onfocus="blur()"
                    href="javascript:void(0);"><span class="bj">项目信息</span></a></li>
                <li id="Tab_1" onclick="showTab('Tab_1', 'Div_Select_1','selected')"><a onfocus="blur()"
                    href="javascript:void(0);"><span class="bj">课程学员</span></a></li>
            </ul>
        </div>
    </div>
    <div class="info">
        <div id="Div_Select_0" class="dv_pageInformation" style="display: none">
            <uc1:TraningProjectView ID="TraningProjectView1" runat="server" />
        </div>
        <div id="Div_Select_1" style="display: none">
            <!--查找条件-->
            <div class="dv_GradeviewList">
                <table class="" border="0" cellpadding="0" cellspacing="0" runat="server" id="tableQueryControlList">
                    <tr>
                        <th width="100">
                            项目编码：
                        </th>
                        <td width="200">
                            <asp:Label ID="Lbl_ItemCode" runat="server" Text=""></asp:Label>
                        </td>
                        <th width="100">
                            项目名称：
                        </th>
                        <td>
                            <asp:Label ID="Lbl_ItemName" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th width="100">
                            项目周期：
                        </th>
                        <td colspan="3">
                            <asp:Label ID="lbl_ItemDate" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th width="100">
                            课程名称：
                        </th>
                        <td width="200">
                            <asp:DropDownList ID="ddl_d999TrainingItemCourseID" runat="server" CssClass="select_190">
                            </asp:DropDownList>
                        </td>
                        <th width="100">
                            学员姓名：
                        </th>
                        <td class="Search_Area">
                            <asp:TextBox ID="txt_u999RealName" runat="server" CssClass="inputbox_120 floatleft"
                                MaxLength="100"></asp:TextBox>
                            <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <!--查找结果-->
            <div class="dv_searchlist">
                <!--翻页-->
                <div class="dv_pagePanel" id="divPage1">
                    <div class="dv_selectAll">
                        <asp:Button ID="btnExport" runat="server" CssClass="btn_Export" Text="导出" OnClick="btnExport_Click" />
                    </div>
                    <div class="dv_pageControl">
                        <uc2:PageSet ID="PageSet1" runat="server" />
                    </div>
                </div>
                <!--列表 begin-->
                <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
                    CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0"
                    DataKeyNames="TrainingItemCourseID" OnRowDataBound="CustomGridView1_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="课程名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="10" Text='<%# Eval("CourseName")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="培训周期" HeaderStyle-Width="160">
                            <ItemTemplate>
                                <%# Eval("CourseBeginTime").ToDate()%>
                                至
                                <%# Eval("CourseEndTime").ToDate()%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="授课方式" HeaderStyle-Width="60" HeaderStyle-CssClass="hide" ItemStyle-CssClass="hide">
                            <ItemTemplate>
                                <cc1:DictionaryLabel ID="DictionaryTeachModel" DictionaryType="Dic_Sys_TeachModel"
                                    FieldIDValue='<%# Eval("TeachModelID") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="LoginName" HeaderText="学员帐号"  HeaderStyle-CssClass="field12 alignleft" ItemStyle-CssClass="alignleft" />
                        <asp:BoundField DataField="RealName" HeaderText="学员姓名"  HeaderStyle-CssClass="field10 alignleft" ItemStyle-CssClass="alignleft" />
                        <asp:TemplateField HeaderText="组织机构"  HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft"  >
                            <ItemTemplate>
                                <cc1:DictionaryLabel ID="lblOrganization" DictionaryType="Dic_CurrentAndSubOrganization"
                                    FieldIDValue='<%# Eval("OrganizationID") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>" HeaderStyle-CssClass="alignleft hide" ItemStyle-CssClass="alignleft hide">
                            <ItemTemplate>
                                <cc1:DictionaryLabel ID="lblDepartmentID" TextLength="10" DictionaryType="vw_Dic_Sys_Department"
                                    FieldIDValue='<%# Eval("DepartmentID") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_position%>" ItemStyle-CssClass="alignleft hide" HeaderStyle-CssClass="alignleft hide">
                            <ItemTemplate>
                                <cc1:DictionaryLabel ID="lblPost" DictionaryType="vw_Dic_Sys_Post" TextLength="8"
                                    FieldIDValue='<%# Eval("PostID") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="SumGrade" HeaderText="成绩"  ItemStyle-CssClass="alignright" HeaderStyle-CssClass="alignright" HeaderStyle-Width="60"/>
                    </Columns>
                </cc1:CustomGridView>
                <!--列表 end-->
                <div class="dv_splitLine">
                </div>
                <!--翻页-->
                <div class="dv_pagePanel" id="divPage2">
                </div>
                <div style="display: none">
                    <asp:GridView ID="GridViewExport" runat="server" AutoGenerateColumns="false" CustomAllowPaging="false"
                        ShowFooter="false" AutoCreateColumnInsertIndex="0" DataKeyNames="TrainingItemCourseID" OnRowDataBound="GridViewExport_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="课程名称">
                                <ItemTemplate>
                                    <cc1:ShortTextLabel ID="lblCourseName" runat="server" Text='<%# Eval("CourseName")%>'></cc1:ShortTextLabel>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="培训周期">
                                <ItemTemplate>
                                    <%# Eval("CourseBeginTime").ToDate()%>
                                    至
                                    <%# Eval("CourseEndTime").ToDate()%>
                                </ItemTemplate>
                            </asp:TemplateField>                           
                            <asp:BoundField DataField="LoginName" HeaderText="学员帐号"  HeaderStyle-CssClass="field12 alignleft" ItemStyle-CssClass="alignleft" />
                            <asp:BoundField DataField="RealName" HeaderText="学员姓名" />
                            <asp:TemplateField HeaderText="组织机构">
                                <ItemTemplate>
                                    <cc1:DictionaryLabel ID="lblOrganization" DictionaryType="Dic_CurrentAndSubOrganization"
                                        FieldIDValue='<%# Eval("OrganizationID") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>                            
                            <asp:BoundField DataField="SumGrade" HeaderText="成绩" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
