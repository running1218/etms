<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CoursePointSearchPublish.ascx.cs" Inherits="Point_CoursePointManager_Controls_CoursePointSearchPublish" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<div class="dv_searchbox">
        <table class="GridviewGray" runat="server" id="tableQueryControlList">
            <tr>
                 <th width="120">
                    项目编码：
                </th>
                <td width="130">
                   <asp:TextBox ID="txt_c999ItemCode" runat="server" />                        
                </td>
                <th width="120">
                   项目名称：
                </th> 
                <td width="130">
                   <asp:TextBox ID="txt_c999ItemName" runat="server" />
                </td>                  
                <td class="Search_Area">
                    <asp:Button ID="btnSearch" runat="server" CssClass="btn_Search" Text="查询" OnClick="btnSearch_Click" />
                    <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
                </td>
            </tr>  
            <tr>
                <th width="120">
                    课程名称：
                </th>
                <td width="130">
                    <asp:TextBox ID="txt_e999CourseName" runat="server"></asp:TextBox>
                </td>  
                 <th width="120">
                    学员姓名：
                </th>
                <td colspan="2">
                    <asp:TextBox ID="txt_u999RealName" runat="server"></asp:TextBox>
                </td>           
            </tr>        
            <tr id="trOrg" runat="server" >
                <th>
                    组织机构：
                </th>
                <td colspan="4">                    
                    <cc1:DictionaryDropDownList ID="ddl_u999OrganizationID" runat="server" DictionaryType="Dic_CurrentAndSubOrganization"
                        IsShowAll="true" AutoPostBack="true" OnSelectedIndexChanged="ddl_Organization_SelectedIndexChanged" CssClass="select_390" />                        
                </td>
            </tr>
            <tr>
                <th>
                    部&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;门：
                </th>
                <td colspan="4">
                    <asp:DropDownList  ID="ddl_u999DepartmentID" runat="server" CssClass="select_190"/>

                </td>
            </tr>           
            <tr>
                <th>
                    发布日期：
                </th >
                <td colspan="4">
                    <cc1:DateTimeTextBox runat="server" ID="begin_a999ScoreIssueTime" DateTimeFormat="%Y-%M-%D"
                        EndTimeControlID="end_a999ScoreIssueTime" />至
                    <cc1:DateTimeTextBox runat="server" ID="end_a999ScoreIssueTime" DateTimeFormat="%Y-%M-%D"
                        BeginTimeControlID="begin_a999ScoreIssueTime" />
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
          <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
                CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0"
                DataKeyNames="StudentCourseID" OnRowDataBound="CustomGridView1_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="项目名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblItemName" runat="server" ShowTextNum="25" Text='<%# Eval("ItemName")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="课程名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="25" Text='<%# Eval("CourseName")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="组织机构" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <cc1:DictionaryLabel ID="DictionaryLabel1" runat="server" DictionaryType='vw_Dic_Sys_Organization'
                            FieldIDValue='<%#Eval("OrganizationID") %>'></cc1:DictionaryLabel>
                    </ItemTemplate>
                </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:DictionaryLabel runat="server" ID="lblDepartment" DictionaryType="vw_Dic_Sys_Department"
                                TextLength="10" FieldIDValue='<%#Eval("DepartmentID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                     
                    <asp:TemplateField HeaderText="学员姓名" HeaderStyle-Width="100">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblRealName" runat="server" ShowTextNum="10" Text='<%# Eval("RealName")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课时" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblCourseHours" runat="server" ShowTextNum="10" Text='<%# Eval("CourseHours")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="成绩" HeaderStyle-Width="80">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblSumGrade" runat="server" ShowTextNum="10" Text='<%# Eval("SumGrade")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="积分值" 
                        HeaderStyle-Width="80">
                        <ItemTemplate>
                             <cc1:ShortTextLabel ID="lblAccessPoints" runat="server" ShowTextNum="10" Text='<%# Eval("AccessPoints")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="发布日期" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft" HeaderStyle-Width="90">
                    <ItemTemplate>
                        <asp:Label ID="lblIssueTime" runat="server" Text='<%#Eval("ScoreIssueTime").ToDate() %>' />
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