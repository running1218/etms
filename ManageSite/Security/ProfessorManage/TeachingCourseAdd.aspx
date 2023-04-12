<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true"
    CodeFile="TeachingCourseAdd.aspx.cs" Inherits="ETMS.WebApp.Manage.Resource.ProfessorManage.TeachingCourseAdd" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" runat="Server">
   <a id="aBack" runat="server" class="btn_Return">返回</a>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--查找条件-->
    <div class="dv_searchbox">
        <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <th width="120">
                    课程编码：
                </th>
                <td width="120">
                    <asp:TextBox runat="server" ID="txtCourseCode" />
                </td>
                <th width="120">
                    课程名称：
                </th>
                <td width="120">
                    <asp:TextBox ID="txtCourseName" runat="server" />
                    <asp:Button ID="btnSearch" runat="server" CssClass="btn_Search" Text="查询" OnClick="btnSearch_Click" /> 
                </td>
                <td>                    
                                           
                </td>                
            </tr>            
        </table>
    </div>
    <!--查找结果-->
    <div class="dv_searchlist">
        <%--<asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional">
            <ContentTemplate>--%>
                <!--翻页-->
                <div class="dv_pagePanel" id="divPage1">
                    <div class="dv_selectAll">
                        <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="GridViewList" />
                        <asp:Button ID="btnAdd" runat="server" Text="确定" CssClass="btn_Ok" OnClick="btnAdd_Click" />                        
                    </div>
                    <div class="dv_pageControl" style="float: right;">
                        <uc2:PageSet ID="PageSet1" runat="server" />
                    </div>
                </div>
                <!--列表 begin-->
                <cc1:CustomGridView ID="GridViewList" runat="server" AutoGenerateColumns="False"
                    DataKeyNames="CourseID">
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <ItemStyle HorizontalAlign="Center"  />
                            <HeaderStyle HorizontalAlign="Center" Width="18" />
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="课程编码" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft field12">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ShowTextNum="10" ID="lblCourseCode" runat="server" Text='<%#Eval("CourseCode") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="课程名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ShowTextNum="10" ID="lblCourseName" runat="server" Text='<%#Eval("CourseName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>                       
                        <asp:TemplateField HeaderText="课程等级" HeaderStyle-Width="60">
                            <ItemTemplate>
                                <cc1:DictionaryLabel DictionaryType="Dic_Sys_CourseLevel" ID="lblCourseLevel" runat="server"
                                    FieldIDValue='<%#Eval("CourseLevelID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="课程类型" HeaderStyle-Width="60">
                            <ItemTemplate>
                                <cc1:DictionaryLabel DictionaryType="Dic_Sys_CourseType" ID="lblCourseType" runat="server"
                                    FieldIDValue='<%#Eval("CourseTypeID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="课时" HeaderStyle-Width="60" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright">
                            <ItemTemplate >                                
                                 <asp:Label ID="lblCourseHours" runat="server" Text='<%#Eval("CourseHours") %>' />
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
                <script language="javascript" type="text/javascript">
                    divPage2.innerHTML = divPage1.innerHTML;
                </script>
           <%-- </ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
</asp:Content>
