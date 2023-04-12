<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="CousreManage.aspx.cs" Inherits="TraningOrgManager_TraningOrgManager_CousreManage" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" runat="Server">
    <a id="aBack" runat="server" class="btn_Return">返回</a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server" >
    <div class="dv_searchbox">
        <table class="GridviewGray th100 fixedTable" border="0" cellpadding="0" cellspacing="0" id="tableQueryControlList" runat="server">
            <tr>
                <th >
                    外部培训机构：
                </th>
                <td style="width:190px">
                    <cc1:ShortTextLabel ID="lblOuterOrgName" runat="server" ShowTextNum="10" />
                </td>
                <th >
                    课程编码：
                </th>
                <td >
                    <asp:TextBox ID="txt_CourseCode" runat="server" Width="80"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" CssClass="btn_Search" Text="查询" OnClick="btnSearch_Click" />
                    <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
                </td>
            </tr>
            <tr>
                <th>
                    课程名称：
                </th>
                <td>
                    <asp:TextBox ID="txt_CourseName" runat="server"></asp:TextBox>
                </td>
                <th>
                    课程类型：
                </th>
                <td >
                    <cc1:DictionaryDropDownList DictionaryType="Dic_Sys_CourseType" ID="ddl_CourseTypeID"
                        runat="server" IsShowAll="true" />
                </td>
            </tr>
        </table>
    </div>
    <div class="dv_searchlist">
        <!--翻页-->
        <div class="dv_pagePanel" id="divPage1">
            <div class="dv_selectAll">
                <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="GridViewList" />
                 <input type="button" class="btn_Add" value="新增" onclick="javascript:showWindow('课程管理','<%=this.ActionHref(string.Format("CourseAdd.aspx?OuterOrgID={0}",OuterOrgID)) %>')" />
                <%--<asp:Button ID="btnAdd" runat="server" Text="新增" CssClass="btn_Add" OnClick="btnAdd_Click" />--%>
                <%--<asp:Button ID="btnDeletes" runat="server" Text="删除" CssClass="btn_Del" 
                    OnClick="btnDeletes_Click" />--%>
                 <cc1:CustomButton runat="server" ID="btnDeletes" Text="删除" CssClass="btn_Del" EnableConfirm="true"
                                ConfirmTitle="提示" ConfirmMessage="确定删除吗？" OnClick="btnDeletes_Click" />      
            </div>
            <div class="dv_pageControl" style="float: right;">
                <uc2:PageSet ID="PageSet1" runat="server" />
            </div>
        </div>
        <!--列表 begin-->
        <cc1:CustomGridView ID="GridViewList" runat="server" AutoGenerateColumns="False"
            DataKeyNames="OuterOrgCourseID">
            <Columns>
                <asp:TemplateField HeaderText="">
                    <ItemStyle HorizontalAlign="Center"  />
                    <HeaderStyle HorizontalAlign="Center" Width="20" />
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="课程编码"  ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft field12">
                    <ItemTemplate>
                        <cc1:ShortTextLabel ShowTextNum="10" ID="lblCourseCode" runat="server" Text='<%#Eval("CourseCode") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="课程名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <cc1:ShortTextLabel ShowTextNum="10" ID="lblCourseName" runat="server" Text='<%#Eval("CourseName") %>' />
                    </ItemTemplate>
                </asp:TemplateField>               
                <asp:TemplateField HeaderText="课程类型" HeaderStyle-Width="60">
                    <ItemTemplate>
                        <cc1:DictionaryLabel DictionaryType="Dic_Sys_CourseType" ID="lblCourseType" runat="server"
                            FieldIDValue='<%#Eval("CourseTypeID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="试用地址" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                    <ItemTemplate>
                       <cc1:ShortTextLabel ID="lblAddress" runat="server" ShowTextNum="10" Text='<%#Eval("AddrURL") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="操作" HeaderStyle-Width="50">
                    <ItemTemplate>
                       <a href="javascript:showWindow('课程管理','<%#this.ActionHref(string.Format("CourseEdit.aspx?OuterOrgCourseID={0}&OuterOrgID={1}",Eval("OuterOrgCourseID"),OuterOrgID)) %>')">编辑</a>
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
    </div>
</asp:Content>
