<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="TeachingList.aspx.cs" Inherits="TraningImplement_TeachingManager_TeachingList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<style>
   .dropdown
   { float:left;
   }
</style>
    <div>
        <!--导航路径-->
        <div class="dv_path">
            当前位置：教学管理>>离线作业批改
        </div>
        <!--功能标题-->
        <h2 class="dv_title">
            离线作业批改
        </h2>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <th width="20%">
                        课程名称：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="DictionaryDropDownList2" DictionaryType="Dic_TeachingCourseName"
                            IsShowAll="true" />
                    </td>
                    <th width="20%">
                        作业名称：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="DictionaryDropDownList1" DictionaryType="Dic_TeachingHomeWorkName"
                            IsShowAll="true" />
                        <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="btn_Search floatleft" />
                        <%--<input class="btn_Search" type="button" value="查询"/>--%>
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
                    <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="True"
                        CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="-1"
                        OnRowDataBound="CustomGridView1_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="课程名称">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="10" Text='<%# Eval("CourseName")%>' ></cc1:ShortTextLabel>
                                    <%--<asp:Label ID="lblCourseName" runat="server" ShowTextNum="10" Text='<%# Eval("CourseName")%>'/>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="作业名称">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <cc1:ShortTextLabel ID="lblJobName" runat="server" ShowTextNum="10" Text='<%# Eval("JobName")%>' ></cc1:ShortTextLabel>
                                    <%--<asp:Label ID="lblJobName" runat="server" ShowTextNum="10" Text='<%# Eval("JobName")%>'/>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="开始时间">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblBeginDate" runat="server" Text='<%#Eval("BeginDate").ToDate() %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="结束时间">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblEndDate" runat="server" Text='<%#Eval("EndDate").ToDate() %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="学员总数">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlkStudentNumber" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="未提交人数">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlkNoSubmit" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="已批阅人数">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlkEvaluationed" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="待批阅人数">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlkEvaluation" runat="server" />
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
