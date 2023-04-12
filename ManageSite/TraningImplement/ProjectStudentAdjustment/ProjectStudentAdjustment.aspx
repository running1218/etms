<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="ProjectStudentAdjustment.aspx.cs" Inherits="TraningImplement_ProjectStudentAdjustment_ProjectStudentAdjustment" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" runat="Server">
    <a href="TraningProjectList.aspx" class="btn_Return">返回</a>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--查找条件-->
        <div class="dv_GradeviewList">
            <table class="" border="0" cellpadding="0" cellspacing="0" runat="server" id="tableQueryControlList">
                <tr>
                    <th width="100">
                        项目编码：
                    </th>
                    <td>
                        <asp:Label ID="lblItemCode" runat="server"></asp:Label>
                    </td>
                    <th width="100">
                        项目名称：
                    </th>
                    <td>
                        <asp:Label ID="lblItemName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        工&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;号：
                    </th>
                    <td width="200" >
                        <asp:TextBox ID="txt_Site_Student999WorkerNo" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                    <th width="100">
                        姓&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;名：
                    </th>
                    <td>
                        <asp:TextBox ID="txt_Site_User999RealName" runat="server" CssClass="inputbox_120"></asp:TextBox><asp:Button
                            ID="btnSearch" CssClass="btn_Search " runat="server" Text="查询" OnClick="btnSearch_Click" />
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        学员状态：
                    </th>
                    <td colspan="3">
                        <cc1:DictionaryDropDownList runat="server" ID="dddl_Sty_StudentSignup999IsUse" DictionaryType="Dic_Status"
                            IsShowAll="false" IsShowChoose="false" CssClass="select_190" />
                    </td>
                </tr>
                <tr id="trOrg" runat="server">
                    <th width="100">
                        组织机构：
                    </th>
                    <td colspan="3">
                        <asp:DropDownList runat="server" ID="ddl_Site_User999OrganizationID" IsShowAll="true"
                            AutoPostBack="True" CssClass="select_390" />
                    </td>
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel" id="divPage1">
                <div class="dv_selectAll">
                    <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" /><asp:Button
                        ID="btn_Add" runat="server" CssClass="btn_Add" Text="新增"></asp:Button><cc1:CustomButton
                            ID="btn_Open" runat="server" CssClass="btn_start_2" Text="启用" ConfirmMessage="启用学员，您确认在此项目中启用所选学员吗？"
                            EnableConfirm="true" OnClick="btn_Open_Click"></cc1:CustomButton><cc1:CustomButton
                                ID="btn_Close" runat="server" CssClass="btn_Stop2" Text="停用" ConfirmMessage="停用学员，将学员项目课程停用，您确认在此培训项目中停用所选的学员吗？"
                                EnableConfirm="true" OnClick="btn_Close_Click"></cc1:CustomButton>
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
                CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0"
                DataKeyNames="StudentSignupID" OnRowDataBound="CustomGridView1_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" Width="20" />
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="WorkerNo" HeaderText="工号" HeaderStyle-CssClass="alignleft field10"
                        ItemStyle-CssClass="alignleft" />
                    <asp:BoundField DataField="RealName" HeaderText="姓名" HeaderStyle-CssClass="alignleft field8"
                        ItemStyle-CssClass="alignleft" />
                    <asp:TemplateField HeaderText="组织机构" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblOrganization" DictionaryType="Dic_CurrentAndSubOrganization"
                                FieldIDValue='<%# Eval("OrganizationID") %>' runat="server" TextLength="10" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblDepartmentID" DictionaryType="vw_Dic_Sys_Department"
                                FieldIDValue='<%# Eval("DepartmentID") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_position%>" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblPost" DictionaryType="vw_Dic_Sys_Post" FieldIDValue='<%# Eval("PostID") %>'
                                runat="server" TextLength="10" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="报名方式" HeaderStyle-Width="70" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblSignupMode" DictionaryType="Dic_Sys_SignupMode" FieldIDValue='<%# Eval("SignupModeID") %>'
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="学员状态" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblIsUse" DictionaryType="Dic_Status" FieldIDValue='<%# Eval("IsUse") %>'
                                runat="server" />
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
