<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="ProjectStudentList.aspx.cs" Inherits="TraningImplement_TraningProjectQuery_ProjectStudentList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" runat="Server">
    <asp:LinkButton ID="lbtnBack" runat="server" CssClass="btn_Return" Text="返回" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--查找条件-->
        <div class="dv_GradeviewList">
            <table class="" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <th width="100">
                        项目编码：
                    </th>
                    <td width="300">
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
                    <td>
                        <asp:Label ID="lbl_ItemDate" runat="server" Text=""></asp:Label>
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
                DataKeyNames="StudentSignupID" OnRowDataBound="CustomGridView1_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="WorkerNo" HeaderText="工号" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft field8"/>
                    <asp:BoundField DataField="RealName" HeaderText="姓名" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft field8"  />
                    <asp:TemplateField HeaderText="组织机构" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblOrganization" DictionaryType="Dic_CurrentAndSubOrganization" FieldIDValue='<%# Eval("OrganizationID") %>'
                                runat="server" TextLength="10" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblDepartmentID" TextLength="10" DictionaryType="vw_Dic_Sys_Department"
                                FieldIDValue='<%# Eval("DepartmentID") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_position%>" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblPost" DictionaryType="vw_Dic_Sys_Post" TextLength="10" FieldIDValue='<%# Eval("PostID") %>'
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_rank%>" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblRank" DictionaryType="vw_Dic_Sys_Rank" FieldIDValue='<%# Eval("RankID") %>'
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="项目学员状态" HeaderStyle-CssClass="field10">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblIsUse" DictionaryType="Dic_Status" FieldIDValue='<%# Eval("IsUse") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程数">
                        <ItemTemplate>
                           <asp:LinkButton ID="lbtnCourseTotal" runat="server" CommandArgument='<%# Eval("UserID") %>'></asp:LinkButton>
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
