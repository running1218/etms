<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="ProjectCourseList.aspx.cs" Inherits="Point_CoursePointRelease_ProjectCourseList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <th width="120">
                        培训项目：
                    </th>
                    <th width="220">
                        <asp:DropDownList ID="ddl_ItemName" runat="server" CssClass="select_210">
                        </asp:DropDownList>
                    </th>
                    <th width="120">
                        课程名称：
                    </th>
                    <th width="220">
                        <asp:TextBox ID="txt_CourseName" runat="server"></asp:TextBox>
                    </th>
                    <td width="100">
                        <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
                    </td>
                    <td>
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
                    <cc1:CustomButton runat="server" ID="btnPoint" Visible="true" CssClass="btn_integral"
                        Text="积分发布" OnClick="btnPoint_Click" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="发布后，将不能重新计算已发布的学员课程积分，请慎重操作！您确认进行积分发布吗？" />
                          <cc1:CustomButton runat="server" ID="btnAllPoint" Visible="true" CssClass="btn_integral"
                        Text="全部发布" OnClick="btnAllPoint_Click" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="发布后，将不能重新计算已发布的学员课程积分，请慎重操作！您确认进行积分发布吗？" />
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
                    <asp:TemplateField HeaderText="项目名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblItemName" runat="server" ShowTextNum="30" Text='<%# Eval("ItemName")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="课程名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="30" Text='<%# Eval("CourseName")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="组织机构" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <cc1:DictionaryLabel ID="DictionaryLabel1" runat="server" DictionaryType='vw_Dic_Sys_Organization'
                            FieldIDValue='<%#Eval("OrganizationID") %>'></cc1:DictionaryLabel>
                    </ItemTemplate>
                </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:DictionaryLabel runat="server" ID="lblDepartment" DictionaryType="vw_Dic_Sys_Department"
                                TextLength="10" FieldIDValue='<%#Eval("DepartmentID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                     
                    <asp:TemplateField HeaderText="学员姓名" HeaderStyle-Width="100">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblRealName" runat="server" ShowTextNum="10" Text='<%# Eval("RealName")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课时" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblCourseHours" runat="server" ShowTextNum="10" Text='<%# Eval("CourseHours")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="成绩" HeaderStyle-Width="80">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblSumGrade" runat="server" ShowTextNum="10" Text='<%# Eval("SumGrade")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程积分" ItemStyle-CssClass="alignright" HeaderStyle-CssClass="alignright"
                        HeaderStyle-Width="100">
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
            <div>
                说明：列表显示项目下成绩已发布，积分已计算但未发布的课程列表。<br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;积分发布操作不可逆，发布后，学员课程积分将正式加入学员的总积分中，不能重新计算已发布的学员课程积分，请慎重操作！
            </div>
        </div>
    </div>
</asp:Content>
