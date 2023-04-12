<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="ProjectCourseList.aspx.cs" Inherits="Point_CoursePointManager_ProjectCourseList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <!--查找条件-->
        <div class="dv_searchbox">
                <table class="GridviewGray" runat="server" id="tableQueryControlList">
            <tr>
                <th width="120">
                    培训项目：
                </th>
                <td width="220">
                      <asp:DropDownList ID="ddl_ItemName" runat="server" CssClass="select_210">
                        </asp:DropDownList>
                </td>
                <th width="120">
                    课程名称：
                </th>
                <td width="220">
                    <asp:TextBox ID="txt_CourseName" runat="server"></asp:TextBox>
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
                    <asp:TemplateField HeaderText="项目名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblItemName" runat="server" ShowTextNum="30" Text='<%# Eval("ItemName")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="课程名称" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="30" Text='<%# Eval("CourseName")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程周期" HeaderStyle-Width="150">
                        <ItemTemplate>
                        <%# Eval("CourseBeginTime").ToDate()%>~<%# Eval("CourseEndTime").ToDate()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="在线测试数" HeaderStyle-Width="70">
                        <ItemTemplate>
                            <asp:Label ID="lblOnlineTest" runat="server">0</asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="学员数" HeaderStyle-Width="50" HeaderStyle-CssClass="alignright"
                        ItemStyle-CssClass="alignright">
                        <ItemTemplate>
                            <asp:Label ID="lbl_SignUpStudentTotal" runat="server">0</asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                    
                     <asp:TemplateField HeaderText="已发布数" HeaderStyle-Width="60" >
                        <ItemTemplate>
                            <asp:Label ID="lblPublished" runat="server">0</asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="待发布数" HeaderStyle-Width="60" >
                        <ItemTemplate>
                        <asp:HyperLink ID="lblUnpublished" runat="server" NavigateUrl='<%#this.ActionHref(string.Format("UnPublishedStudentAllListView.aspx?TrainingItemCourseID={0}",Eval("TrainingItemCourseID"))) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="未获积分数" HeaderStyle-Width="70">
                        <ItemTemplate>
                        <asp:Label ID="lblNotCalculated" runat="server">0</asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                                   
                    <asp:TemplateField HeaderText="操作" HeaderStyle-Width="120">
                        <ItemTemplate>
                            <asp:LinkButton ID="lblSystemCalculatePoint" runat="server" Text="计算积分"></asp:LinkButton>
                            <asp:LinkButton ID="lblManualSetupPoint" runat="server" Text="设置积分"></asp:LinkButton>
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
