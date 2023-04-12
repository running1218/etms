<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="StudentPointSearchDetail.aspx.cs" Inherits="Point_StudentPointSearchDetail" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" runat="Server">
    <a id="aBack" runat="server" class="btn_Return">返回</a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="dv_GradeviewList">
        <table>
            <tr>
                <th>
                    学员姓名：
                </th>
                <td>
                    <asp:Label ID="lblRealName" runat="server" />
                </td>
                <th>
                    <asp:Literal ID="ltlDepartment" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>
                </th>
                <td>
                    <cc1:DictionaryLabel runat="server" ID="lblDepartment" DictionaryType="vw_Dic_Sys_Department"
                        TextLength="10" />
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltlPosition" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>：
                </th>
                <td>
                    <cc1:DictionaryLabel runat="server" ID="lblPost" DictionaryType="vw_Dic_Sys_Post"
                        TextLength="10" />
                </td>
                <th>
                    <asp:Literal ID="ltlRank" runat="server" Text="<%$ Resources:UIResource, ui_rank%>"></asp:Literal>：
                </th>
                <td>
                    <cc1:DictionaryLabel runat="server" ID="lblRank" DictionaryType="vw_Dic_Sys_Rank"
                        TextLength="10" />
                </td>
            </tr>
            <tr>
                <th>
                    日期范围：
                </th>
                <td>
                    <asp:Label ID="lblDate" runat="server" />
                </td>
            </tr>
            <tr>
                <th>
                    期初积分：
                </th>
                <td>
                    <asp:Label ID="lblStartPoint" runat="server" />
                </td>
                <th>
                    期末积分：
                </th>
                <td>
                    <asp:Label ID="lblEndPoint" runat="server" />
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
        <cc1:CustomGridView ID="GridViewList" runat="server" AutoGenerateColumns="False">
            <Columns>               
                 <asp:TemplateField HeaderText="日期" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft" HeaderStyle-Width="80"> 
                   <ItemTemplate>
                      <asp:Label ID="lblDateTime" runat="server" Text='<%#Eval("IssueTime").ToDate() %>' />
                   </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="类型" HeaderStyle-Width="80">
                   <ItemTemplate>
                      <cc1:ShortTextLabel ID="lblStyle" runat="server" ShowTextNum="10" Text='<%#Eval("StudentPointIssueTypeName") %>' />
                   </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="摘要" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                   <ItemTemplate>
                      <cc1:ShortTextLabel ID="lblRemark" runat="server" ShowTextNum="10" Text='<%#Eval("AccessPointReason") %>' />
                   </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="获取积分" HeaderStyle-Width="80" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright">
                   <ItemTemplate>
                      <cc1:ShortTextLabel ID="lblGivePoints" runat="server" ShowTextNum="10" Text='<%#Eval("AccessPoints") %>' />
                   </ItemTemplate>
                </asp:TemplateField>
                <%-- <asp:TemplateField HeaderText="消费积分">
                   <ItemTemplate>
                      <cc1:ShortTextLabel ID="lblConSumPoints" runat="server" ShowTextNum="10" Text='<%#Eval("").ToDate() %>' />
                   </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="积分余额">
                   <ItemTemplate>
                      <cc1:ShortTextLabel ID="lblLeavePoints" runat="server" ShowTextNum="10" Text='<%#Eval("").ToDate() %>' />
                   </ItemTemplate>
                </asp:TemplateField>        --%>        
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
