<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="CoursePeriodList.aspx.cs" Inherits="TraningImplement_ProjectCoursePeriodResult_CoursePeriodList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray th80" border="0" cellpadding="0" cellspacing="0" runat="server"
                id="tableQueryControlList">
                <tr>
                    <th width="120">
                        项目名称：
                    </th>
                    <td width="120">
                        <asp:DropDownList ID="ddl_f999TrainingItemID" runat="server" CssClass="easyui-combobox">
                        </asp:DropDownList>
                    </td>
                    <th width="100">
                        课程名称：
                    </th>
                    <td class="Search_Area">
                        <asp:TextBox ID="txt_g999CourseName" runat="server" CssClass="inputbox_120 floatleft"></asp:TextBox>
                        <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
                        <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        课时状态：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="ddl_a999CourseHoursStatusID" DictionaryType="Dic_Sys_CourseHoursStatus"
                            IsShowAll="true" />
                    </td>
                    <th>
                        讲&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;师：
                    </th>
                    <td>
                        <asp:TextBox ID="txt_d999RealName" runat="server" CssClass="inputbox_120"></asp:TextBox>
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
                CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="1"
                DataKeyNames="ItemCourseHoursID" OnRowDataBound="CustomGridView1_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="课程名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("CourseName")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="培训日期" HeaderStyle-Width="90" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <asp:Label ID="lblTrainingDate" runat="server" Text='<%# Eval("TrainingDate").ToDate()%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="培训时段" HeaderStyle-Width="90">
                        <ItemTemplate>
                            <asp:Label ID="lblTrainingBeginTime" runat="server" Text='<%#  Eval("TrainingBeginTime").ToDateTime().ToString("HH:mm")%>'></asp:Label>至<asp:Label
                                ID="lblTrainingEndTime" runat="server" Text='<%#  Eval("TrainingEndTime").ToDateTime().ToString("HH:mm")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="讲师" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <%# Eval("TeacherName")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="是否支付" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <%# Eval("PayStatus").ToInt()==1 ? "已支付":"未支付" %>
                            <asp:HiddenField ID="hfPayStatus" runat="server" Value='<%# Eval("PayStatus")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课时状态" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="dlblCourseHoursStatus" DictionaryType="Dic_Sys_CourseHoursStatus"
                                FieldIDValue='<%# Eval("CourseHoursStatusID") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作" HeaderStyle-Width="120">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnSetResult" runat="server" Text="设置执行结果" CommandName="SetResult"
                                CommandArgument='<%# Eval("TrainingItemCourseID") %>'></asp:LinkButton><asp:LinkButton
                                    ID="lbtnView" runat="server" Text="查看" CommandName="View" CommandArgument='<%# Eval("TrainingItemCourseID") %>'></asp:LinkButton>
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
