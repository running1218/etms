<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="StudentPointSearch.aspx.cs" Inherits="Point_StudentPointSearch" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="dv_GradeviewList">
        <table class="" runat="server" id="tableQueryControlList">
            <tr>
                <th width="120">
                    学员姓名：
                </th>
                <td width="220">
                    <asp:TextBox ID="txt_u999RealName" runat="server"></asp:TextBox>                    
                </td>
                <th width="120">
                    部&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;门：
                </th>
                <td width="220">
                    <asp:TextBox ID="txt_d999DepartmentName" runat="server"></asp:TextBox>
                </td>               
            </tr>
            <tr>
             <th>
                    日期范围：
                </th>
                <td>
                    <cc1:DateTimeTextBox runat="server" ID="begin_a999IssueTime" DateTimeFormat="%Y-%M-%D"
                        EndTimeControlID="end_a999IssueTime" />至
                    <cc1:DateTimeTextBox runat="server" ID="end_a999IssueTime" DateTimeFormat="%Y-%M-%D" BeginTimeControlID="begin_a999IssueTime" />
                    <asp:Button ID="btn_Search" runat="server" Text="查询" CssClass="btn_Search" OnClick="btn_Search_Click" />
                    <%--<a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>--%>
                </td>
            </tr>            
        </table>
    </div>
    <div class="dv_searchlist">
        <!--翻页-->
        <div class="dv_pagePanel">
            <div class="dv_pageControl">
                <uc2:PageSet ID="PageSet1" runat="server" />
            </div>
        </div>
        <cc1:CustomGridView ID="GridViewList" runat="server" AutoGenerateColumns="False"
            DataKeyNames="StudentID">
            <Columns>
                <asp:TemplateField HeaderText="序号" HeaderStyle-Width="40">
                    <ItemStyle HorizontalAlign="Center" Width="60" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="LabNo" runat="server" Text='<%#((this.PageSet1.PageIndex -1) * this.PageSet1.PageSize) + Container.DataItemIndex+1 %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="学员姓名" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft" HeaderStyle-Width="80">
                    <ItemTemplate>
                        <asp:Label ID="lblRealName" runat="server" Text='<%#Eval("RealName") %>' />
                    </ItemTemplate>
                </asp:TemplateField>               
                <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <cc1:DictionaryLabel runat="server" ID="lblDepartment" DictionaryType="vw_Dic_Sys_Department"
                            TextLength="10" FieldIDValue='<%#Eval("DepartmentID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_position%>" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <cc1:DictionaryLabel runat="server" ID="lblPost" DictionaryType="vw_Dic_Sys_Post"
                            TextLength="10" FieldIDValue='<%#Eval("PostID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="学员状态" HeaderStyle-Width="60">
                   <ItemTemplate>
                       <cc1:DictionaryLabel DictionaryType="Dic_Status" runat="server" ID="lblStatus" FieldIDValue='<%#Eval("Status") %>' />
                   </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="期初积分" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright" HeaderStyle-Width="60" >
                   <ItemTemplate>                   
                       <asp:Label ID="lblStartPoint" runat="server" Text='<%#isssueDetailLogic.StatStudentPointByBeforeDateTime(Eval("StudentID").ToInt(),begin_a999IssueTime.Text.Trim().ToDateTime()) %>' />
                   </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="本期积分获取" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright" HeaderStyle-Width="90">
                   <ItemTemplate>
                       <asp:Label ID="lblGivePoint" runat="server"  Text='<%#Eval("AccessPoints") %>' />
                   </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="本期积分消费" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright" HeaderStyle-Width="90">
                   <ItemTemplate>
                       <asp:Label ID="lblConSumPoint" runat="server"  Text='<%#isssueDetailLogic.StatStudentExpensePointBetweenTwoDate(Eval("StudentID").ToInt(),begin_a999IssueTime.Text.Trim().ToDateTime(),end_a999IssueTime.Text.Trim().ToDateTime()) %>' />
                   </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="期末积分" HeaderStyle-Width="60" ItemStyle-CssClass="alignright" HeaderStyle-CssClass="alignright">
                   <ItemTemplate>
                       <asp:Label ID="lblEndPoint" runat="server"  Text='<%#isssueDetailLogic.StatStudentPointByBeforeDateTime(Eval("StudentID").ToInt(),begin_a999IssueTime.Text.Trim().ToDateTime())+Eval("AccessPoints").ToInt()-isssueDetailLogic.StatStudentExpensePointBetweenTwoDate(Eval("StudentID").ToInt(),begin_a999IssueTime.Text.Trim().ToDateTime(),end_a999IssueTime.Text.Trim().ToDateTime())  %>' />
                   </ItemTemplate>
                </asp:TemplateField>               
                <asp:TemplateField HeaderText="操作" HeaderStyle-Width="60">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <a href='<%# this.ActionHref(string.Format("StudentPointSearchDetail.aspx?StudentID={0}&BeginDateTime={1}&EndDateTime={2}",Eval("StudentID"),string.IsNullOrEmpty(begin_a999IssueTime.Text.Trim())?DateTime.Now.ToString("yyyy-01-01"):begin_a999IssueTime.Text.Trim(),string.IsNullOrEmpty(end_a999IssueTime.Text.Trim())?DateTime.Now.ToDate():end_a999IssueTime.Text.Trim()))%>'>
                            明细</a>
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
</asp:Content>
