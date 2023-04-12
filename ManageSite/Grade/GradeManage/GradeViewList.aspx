<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="GradeViewList.aspx.cs" Inherits="Grade_GradeManage_GradeViewList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" runat="Server">
    <!--导航路径-->
    <!--导航路径-->
    <div class="dv_path" id="dv_path">
        当前位置：测评系统&gt;&gt;考试与成绩&gt;&gt;成绩查询
    </div>
    <!--功能标题-->
    <h2 class="dv_title">
        成绩查询
    </h2>
    <a id="aBack" runat="server" class="btn_Return">返回</a>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <div class="dv_GradeviewList">
            <table>
                <tr>
                    <th width="80">
                        项目编码：
                    </th>
                    <td width="180">
                        <cc1:ShortTextLabel ID="lblItemCode" runat="server" ShowTextNum="12" />
                    </td>
                    <th>
                        项目名称：
                    </th>
                    <td>
                        <cc1:ShortTextLabel ID="lblItemName" runat="server" ShowTextNum="15"></cc1:ShortTextLabel>
                    </td>
                </tr>
                <tr>
                    <th>
                        课程编码：
                    </th>
                    <td>
                        <cc1:ShortTextLabel ID="lblCourseCode" runat="server" ShowTextNum="12" />
                    </td>
                    <th>
                        课程名称：
                    </th>
                    <td>
                        <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="15" />
                    </td>
                </tr>
                <tr>
                    <th>
                        授课方式：
                    </th>
                    <td>
                        <cc1:DictionaryLabel ID="lblTeachModel" runat="server" DictionaryType="Dic_Sys_TeachModel" />
                    </td>
                    <th>
                        学员人数：
                    </th>
                    <td>
                        <asp:Literal ID="lblStudentNum" runat="server"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel">
                <div class="dv_selectAll">
                    <asp:Button runat="server" ID="btnExport" Text="导出" OnClick="btnExport_Click" CssClass="btn_Export" />
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" CssClass="GridviewGray" EmptyDataSourceTip="没有任何记录！"
                AutoCreateColumnInsertIndex="0" AutoGenerateColumns="False" CurrentPageIndex="1"
                OnRowDataBound="CustomGridView1_RowDataBound" CustomAllowPaging="False" IsEmpty="False">
                <Columns>
                    <asp:BoundField DataField="WorkerNo" HeaderText="工号" SortExpression="JobName" HeaderStyle-CssClass="field8" Visible="false">
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RealName" HeaderText="学员姓名" SortExpression="JobName" HeaderStyle-CssClass="alignleft field12"
                        ItemStyle-CssClass="alignleft">
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="组织机构" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:DictionaryLabel runat="server" ID="lblOrg" DictionaryType="vw_Dic_Sys_Organization"
                                FieldIDValue='<%#Eval("OrganizationID").ToString() %>' TextLength="10" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>" HeaderStyle-CssClass="alignleft hide" ItemStyle-CssClass="alignleft hide">
                        <ItemTemplate>
                            <cc1:DictionaryLabel runat="server" ID="lblDepartment" DictionaryType="vw_Dic_Sys_Department"
                                FieldIDValue='<%#Eval("DepartmentID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_rank%>" HeaderStyle-CssClass="alignleft hide" ItemStyle-CssClass="alignleft hide">
                        <ItemTemplate>
                            <cc1:DictionaryLabel runat="server" ID="lblRank" DictionaryType="vw_Dic_Sys_Rank"
                                FieldIDValue='<%#Eval("RankID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_position%>" HeaderStyle-CssClass="alignleft hide" ItemStyle-CssClass="alignleft hide">
                        <ItemTemplate>
                            <cc1:DictionaryLabel runat="server" ID="lblPost" DictionaryType="vw_Dic_Sys_Post"
                                FieldIDValue='<%#Eval("PostID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="学员状态" HeaderStyle-Width="80">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblIsUse" DictionaryType="Dic_Status" runat="server" FieldIDValue='<%#Eval("Status") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="成绩" HeaderStyle-Width="80" ItemStyle-CssClass="alignright"
                        HeaderStyle-CssClass="alignright">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblSumGrade" runat="server" Text='<%#Eval("SumGrade")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作" HeaderStyle-Width="60">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%--<asp:HyperLink ID="lblView" runat="server" Text="查看" NavigateUrl='javascript:showWindow("成绩查询",<%= this.ActionHref(string.Format("{0}/Grade/GradeManage/GradeView.aspx?TrainingItemCourseID={1}&CourseID={2}&TrainingItemID={3}&UserID={4}", WebUtility.AppPath, TrainingItemCourseID, CourseID, TrainingItemID,Eval("UserID").ToInt()) %>' />--%>
                            <asp:LinkButton ID="lblView" runat="server" Text="查看" />
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
        </div>
        <cc1:CustomGridView ID="CustomGridView2" runat="server" CssClass="GridviewGray" EmptyDataSourceTip="没有任何记录！"
            AutoCreateColumnInsertIndex="0" AutoGenerateColumns="False" CurrentPageIndex="1" CustomAllowPaging="False" IsEmpty="False">
            <Columns>
                <asp:BoundField DataField="RealName" HeaderText="学员姓名" SortExpression="JobName" HeaderStyle-CssClass="alignleft field12"
                    ItemStyle-CssClass="alignleft">
                    <ItemStyle HorizontalAlign="Left" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="组织机构" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <cc1:DictionaryLabel runat="server" ID="lblOrg" DictionaryType="vw_Dic_Sys_Organization"
                            FieldIDValue='<%#Eval("OrganizationID").ToString() %>' TextLength="10" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="学员状态" HeaderStyle-Width="80">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <cc1:DictionaryLabel ID="lblIsUse" DictionaryType="Dic_Status" runat="server" FieldIDValue='<%#Eval("Status") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="成绩" HeaderStyle-Width="80" ItemStyle-CssClass="alignright"
                    HeaderStyle-CssClass="alignright">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblSumGrade" runat="server" Text='<%#Eval("SumGrade")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </cc1:CustomGridView>
    </div>
</asp:Content>
