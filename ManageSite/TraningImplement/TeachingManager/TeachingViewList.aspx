<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="TeachingViewList.aspx.cs" Inherits="TraningImplement_TeachingManager_TeachingViewList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--导航路径-->
        <div class="dv_path">
            当前位置：教学管理>>离线作业批阅及查看
        </div>
        <!--功能标题-->
        <h2 class="dv_title">
            离线作业批阅及查看
        </h2>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <th>
                        课程名称：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="DictionaryDropDownList2" DictionaryType="Dic_TeachingCourseName"
                            IsShowAll="true" />
                    </td>
                    <th>
                        作业名称：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="DictionaryDropDownList1" DictionaryType="Dic_TeachingHomeWorkName"
                            IsShowAll="true" />
                    </td>
                    <td>
                        <asp:UpdatePanel ID="upQuery" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn_Search" Text="查询" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                        <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
                    </td>
                </tr>
                <tr>
                    <th>
                        提交批阅情况：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="DictionaryDropDownList3" DictionaryType="Dic_TeachingManagerState"
                            IsShowAll="true" />
                    </td>
                    <th>
                        学员姓名：
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="inputbox_120"></asp:TextBox>
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
                        CustomAllowPaging="false" ShowFooter="false">
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
                            <asp:TemplateField HeaderText="学员姓名">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblStudentName" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblOrg" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="最新提交附件">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblAttatch" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="提交时间">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblAttTime" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="最近批阅附件">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblEvaluationAttach" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="批阅情况">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblEvaluationStation" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="操作">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <a href="Teaching.aspx">批阅及查看</a></ItemTemplate>
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
