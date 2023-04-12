<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="StudentOpenLearnList.aspx.cs" Inherits="LearningManagement_LearnProcessControl_StudentOpenLearnList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" runat="Server">
    <asp:LinkButton ID="lbtnReturn" runat="server" Text="返回" CssClass="btn_Return"></asp:LinkButton>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <th width="120">
                        项目名称：
                    </th>
                    <td>
                        <asp:Label ID="lblItemName" runat="server"></asp:Label>
                    </td>
                    <th width="120">
                        课程名称：
                    </th>
                    <td>
                        <asp:Label ID="lblCourseName" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <!--翻页-->
                    <div class="dv_pagePanel" id="divPage1">
                        <div class="dv_pageControl">
                            <uc2:PageSet ID="PageSet1" runat="server" />
                        </div>
                    </div>
                    <!--列表 begin-->
                    <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
                        CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0"
                        DataKeyNames="StudentCourse" onrowdatabound="CustomGridView1_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="WorkerNo" HeaderText="工号" HeaderStyle-Width="40" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft" />
                            <asp:BoundField DataField="RealName" HeaderText="姓名" HeaderStyle-Width="60" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft" />
                            <asp:TemplateField HeaderText="组织机构" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                                <ItemTemplate>
                                    <cc1:DictionaryLabel ID="lblOrganization" DictionaryType="Dic_CurrentAndSubOrganization"
                                        FieldIDValue='<%# Eval("OrganizationID") %>' runat="server" TextLength="10" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>"  HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                                <ItemTemplate>
                                    <cc1:DictionaryLabel ID="lblDepartmentID" DictionaryType="vw_Dic_Sys_Department"
                                        FieldIDValue='<%# Eval("DepartmentID") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_position%>"  HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                                <ItemTemplate>
                                    <cc1:DictionaryLabel ID="lblPost" DictionaryType="vw_Dic_Sys_Post" FieldIDValue='<%# Eval("PostID") %>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_rank%>" HeaderStyle-Width="100"  HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                                <ItemTemplate>
                                    <cc1:DictionaryLabel ID="lblRank" DictionaryType="vw_Dic_Sys_Rank" FieldIDValue='<%# Eval("RankID") %>'
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
