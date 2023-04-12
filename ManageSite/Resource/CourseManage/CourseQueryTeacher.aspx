<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master"
    AutoEventWireup="true" CodeFile="CourseQueryTeacher.aspx.cs" Inherits="ETMS.WebApp.Manage.CourseQueryTeacher" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="dv_information">
        <!--项目信息-->
        <div class="dv_searchbox">
            <table class="GridviewGray fixedTable" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <th style="width:60px;">
                        课程编码：
                    </th>
                    <td class="field8">
                        <asp:Literal ID="ltlProjectCode" runat="server"></asp:Literal>
                    </td>
                    <th  style="width:60px;">
                        课程名称：
                    </th>
                    <td>
                        <cc1:ShortTextLabel ID="lblProjectName" runat="server" ShowTextNum="20"></cc1:ShortTextLabel>
                    </td>
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel">                
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" PageSize="8" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="gvList" runat="server" AutoGenerateColumns="false" CustomAllowPaging="true" ShowFooter="false" AutoCreateColumnInsertIndex="0" DataKeyNames="TeacherID">
                <Columns>
                    <asp:TemplateField HeaderText="讲师姓名" HeaderStyle-CssClass="alignleft field8" ItemStyle-CssClass="alignleft" >
                        <ItemTemplate>
                            <asp:Literal ID="ltlTeacherName" runat="server" Text='<%# Eval("RealName") %>'></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="讲师级别" HeaderStyle-Width="80" >
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblLevel" runat="server" DictionaryType="Dic_Sys_TeacherType" FieldIDValue='<%# Eval("TeacherTypeID") %>'></cc1:DictionaryLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="讲师来源" HeaderStyle-Width="80">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblTeacherSource" runat="server" DictionaryType="Dic_Sys_TeacherSource" FieldIDValue='<%# Eval("TeacherSourceID") %>'></cc1:DictionaryLabel>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField HeaderText="所属机构" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblOrgName" runat="server" Text='<%# Eval("BelongOrgName") %>' ShowTextNum="12"></cc1:ShortTextLabel>
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
    </div>
</asp:Content>
