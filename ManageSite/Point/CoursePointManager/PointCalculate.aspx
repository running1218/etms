<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="PointCalculate.aspx.cs" Inherits="Point_CoursePointManager_PointCalculate" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="Controls/CourseRoleListView.ascx" TagName="CourseRoleListView"
    TagPrefix="uc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" runat="Server">
    <asp:LinkButton ID="lbtn_Return" runat="server" CssClass="btn_Return" Text="返回"></asp:LinkButton>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="dv_GradeviewList">
        <table class="">
            <tr>
                <th width="120">
                    培训项目：
                </th>
                <td width="220">
                    <cc1:ShortTextLabel ID="lbl_ItemName" runat="server"></cc1:ShortTextLabel>
                </td>
                <th width="120">
                课程名称：
                </th>
                <td width="220">
                    <cc1:ShortTextLabel ID="lbl_CourseName" runat="server"></cc1:ShortTextLabel>
                </td>
            </tr>
            <tr>
                <th width="120">
                课　　时：
                </th>
                <td width="220">
                    <cc1:ShortTextLabel ID="lbl_CourseHours" runat="server"></cc1:ShortTextLabel>
                </td>
                <th width="120">
                课程属性：
                </th>
                <td width="220">
                    <cc1:DictionaryLabel ID="lblCourseAttrID" runat="server" DictionaryType="Dic_Sys_CourseAttr" />
                </td>
            </tr>
        </table>
    </div>
    <div class="dv_serviceTab">
        <div class="dv_Tabmenus">
            <ul>
                <li id="Tab_0" onclick="showTab('Tab_0', 'Div_Select_0','selected')"><a onfocus="blur()"
                    href="javascript:void(0);"><span class="bj">未计算</span></a></li>
                <li id="Tab_1" onclick="showTab('Tab_1', 'Div_Select_1','selected')"><a onfocus="blur()"
                    href="javascript:void(0);"><span class="bj">已计算</span></a></li>
            </ul>
        </div>
    </div>
    <div class="info">
        <div id="Div_Select_0" style="display: none">
            <!--查找条件-->
            <div class="dv_GradeviewList">
                <table class="" runat="server" id="tableQueryControlList">
                    <tr id="tr_NotCalculated_Org" runat="server">
                        <th>
                            组织机构：
                        </th>
                        <td colspan="3">
                            <cc1:DictionaryDropDownList ID="ddl_NotCalculated_OrganizationID" runat="server"
                                DictionaryType="Dic_CurrentAndSubOrganization" IsShowAll="true" AutoPostBack="true"
                                OnSelectedIndexChanged="ddl_NotCalculated_Organization_SelectedIndexChanged"
                                CssClass="select_190" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            部&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;门：
                        </th>
                        <td>
                            <asp:DropDownList ID="ddl_NotCalculated_DepartmentID" runat="server" CssClass="select_190" />
                        </td>
                        <th width="120">
                            学员姓名：
                        </th>
                        <td>
                            <asp:TextBox ID="txt_NotCalculated_StudentName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            获得积分成绩分数线：
                        </th>
                        <td colspan="3">
                            <asp:TextBox ID="txt_Point" runat="server" Text="60" MaxLength="3"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_Point"
                                Display="None" ErrorMessage="获得积分成绩分数线格式错误！" ValidationExpression="\d{0,6}(\.\d{1,2})?"
                                ValidationGroup="Saves"></asp:RegularExpressionValidator><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator1" Text="*" Display="None" runat="server" ErrorMessage="请填写获得积分成绩分数线！"
                                    ControlToValidate="txt_Point" ValidationGroup="Saves"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Saves"
                                ShowMessageBox="true" ShowSummary="false" />
                            <asp:Button ID="btnScoreSeach1" runat="server" Text="查询" CssClass="btn_Search" OnClick="btnScoreSeach_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <!--查找结果-->
            <div class="dv_searchlist">
                <!--翻页-->
                <div class="dv_pagePanel" id="div2">
                    <div class="dv_selectAll">
                        <cc1:CheckBoxsController runat="server" ID="CheckBoxsController1" ContainerID="CustomGridView2" />
                        <cc1:CustomButton runat="server" ID="btnPoint" Visible="true" CssClass="btn_integral"
                            Text="计算积分" OnClick="btnPoint_Click" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="您确认进行计算积分操作吗？" />
                        <cc1:CustomButton runat="server" ID="btnAllPoint" Visible="true" CssClass="btn_integral"
                            Text="全部计算" OnClick="btnAllPoint_Click" EnableConfirm="true" ConfirmTitle="提示"
                            ConfirmMessage="您确认进行全部计算积分操作吗？" />
                    </div>
                    <div class="dv_pageControl">
                        <uc2:PageSet ID="PageSet2" runat="server" />
                    </div>
                </div>
                <!--列表 begin-->
                <cc1:CustomGridView ID="CustomGridView2" runat="server" AutoGenerateColumns="false"
                    CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0"
                    DataKeyNames="StudentCourseID" OnRowDataBound="CustomGridView2_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <ItemStyle HorizontalAlign="Center" Width="26" />
                            <HeaderStyle HorizontalAlign="Center" Width="20" />
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="用户ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblUserID" runat="server" Text='<%#Eval("UserID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="学员姓名" HeaderStyle-Width="100">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblRealName" runat="server" ShowTextNum="10" Text='<%# Eval("RealName")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="组织机构" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                            <ItemTemplate>
                                <cc1:DictionaryLabel ID="lblOrganization" DictionaryType="Dic_CurrentAndSubOrganization"
                                    FieldIDValue='<%# Eval("OrganizationID") %>' runat="server" TextLength="10" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                            <ItemTemplate>
                                <cc1:DictionaryLabel runat="server" ID="lblDepartment" DictionaryType="vw_Dic_Sys_Department"
                                    TextLength="10" FieldIDValue='<%#Eval("DepartmentID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="成绩" HeaderStyle-Width="80">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblSumGrade" runat="server" ShowTextNum="10" Text='<%# Eval("SumGrade")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="学习进度" HeaderStyle-Width="100">
                            <ItemTemplate>
                                <asp:Label ID="lblLearningProgress" runat="server">0%</asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </cc1:CustomGridView>
                <!--列表 end-->
                <div class="dv_splitLine">
                </div>
                <!--翻页-->
                <div class="dv_pagePanel" id="div3">
                </div>
            </div>
        </div>
        <div id="Div_Select_1" style="display: none">
            <!--查找条件-->
            <div class="dv_GradeviewList">
                <table class="">
                    <tr id="trOrg" runat="server">
                        <th>
                            组织机构：
                        </th>
                        <td colspan="3">
                            <cc1:DictionaryDropDownList ID="ddl_OrganizationID" runat="server" DictionaryType="Dic_CurrentAndSubOrganization"
                                IsShowAll="true" AutoPostBack="true" OnSelectedIndexChanged="ddl_Organization_SelectedIndexChanged"
                                CssClass="select_210" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            部&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;门：
                        </th>
                        <td>
                            <asp:DropDownList ID="ddl_DepartmentID" runat="server" CssClass="select_190" />
                        </td>
                        <th width="120">
                            学员姓名：
                        </th>
                        <td>
                            <asp:TextBox ID="txt_StudentName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th width="120">
                            获得积分：
                        </th>
                        <td colspan="3">
                            <asp:TextBox ID="txt_beginAccessPoints" runat="server"></asp:TextBox>至<asp:TextBox
                                ID="txt_endAccessPoints" runat="server"></asp:TextBox>
                            <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="btn_Search" OnClick="btnSearch_Click" />
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
                        <cc1:CustomButton runat="server" ID="btnDelete" Visible="true" CssClass="btn_DelAll"
                            Text="批量删除" OnClick="btnDelete_Click" EnableConfirm="true" ConfirmTitle="提示"
                            ConfirmMessage="您确认进行删除操作吗？" />
                        <cc1:CustomButton runat="server" ID="btnAllDelete" Visible="true" CssClass="btn_DelAll"
                            Text="全部删除" OnClick="btnAllDelete_Click" EnableConfirm="true" ConfirmTitle="提示"
                            ConfirmMessage="您确认进行全部删除操作吗？" />
                    </div>
                    <div class="dv_pageControl">
                        <uc2:PageSet ID="PageSet1" runat="server" />
                    </div>
                </div>
                <!--列表 begin-->
                <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
                    CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0"
                    DataKeyNames="StudentCourseID" OnRowDataBound="CustomGridView1_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <ItemStyle HorizontalAlign="Center" Width="26" />
                            <HeaderStyle HorizontalAlign="Center" Width="20" />
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="学员姓名" HeaderStyle-Width="100">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblRealName" runat="server" ShowTextNum="10" Text='<%# Eval("RealName")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="组织机构" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                            <ItemTemplate>
                                <cc1:DictionaryLabel ID="lblOrganization" DictionaryType="Dic_CurrentAndSubOrganization"
                                    FieldIDValue='<%# Eval("OrganizationID") %>' runat="server" TextLength="10" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                            <ItemTemplate>
                                <cc1:DictionaryLabel runat="server" ID="lblDepartment" DictionaryType="vw_Dic_Sys_Department"
                                    TextLength="10" FieldIDValue='<%#Eval("DepartmentID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="成绩" HeaderStyle-Width="80">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblSumGrade" runat="server" ShowTextNum="10" Text='<%# Eval("SumGrade")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="获得积分时间" HeaderStyle-Width="100">
                            <ItemTemplate>
                                <asp:Label ID="lblAccessPointsTime" runat="server" Text='<%# Eval("AccessPointsTime").ToDate() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="获得积分" HeaderStyle-Width="80">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblAccessPoints" runat="server" ShowTextNum="10" Text='<%# Eval("AccessPoints")%>'></cc1:ShortTextLabel>
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
    </div>
</asp:Content>
