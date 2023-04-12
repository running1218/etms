<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master"
    AutoEventWireup="true" CodeFile="ChooseTeacher.aspx.cs" Inherits="ETMS.WebApp.Manage.CourseChooseTeacher" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="dv_information">
        <!--项目信息-->
        <div style="width:95%" class="dv_searchbox">
            <table class="GridviewGray marginb" border="0" cellpadding="0" cellspacing="0" runat="server"
                id="tableQueryControlList">
                <tr>
                    <th width="65" >
                        讲师名称：
                    </th>
                    <td class="Search_Area">
                        <asp:TextBox ID="txt_TeacherName" runat="server" CssClass="inputbox_120 floatleft"></asp:TextBox>
                        <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel">
                <div class="dv_selectAll">
                    <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="gvList" />
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" PageSize="8" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="gvList" runat="server" AutoGenerateColumns="false" CustomAllowPaging="true" DataKeyNames="TeacherID"
                ShowFooter="false" AutoCreateColumnInsertIndex="0">
                <Columns>
                    <asp:TemplateField HeaderText="">
                        <ItemStyle HorizontalAlign="Center"  />
                        <HeaderStyle HorizontalAlign="Center" Width="18"/>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="讲师姓名"  HeaderStyle-CssClass="alignleft field12" ItemStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblTeacherName" runat="server" Text='<%# Eval("RealName") %>' ShowTextNum="6"></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="讲师等级" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblLevel" runat="server" DictionaryType="Dic_Sys_TeacherLevel" FieldIDValue='<%# Eval("TeacherLevelID") %>'></cc1:DictionaryLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="讲师来源" HeaderStyle-Width="60">
                            <ItemTemplate>
                                <cc1:DictionaryLabel ID="lblTeacherSource" runat="server" DictionaryType="Dic_Sys_TeacherSource" FieldIDValue='<%# Eval("TeacherSourceID") %>'></cc1:DictionaryLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                    <asp:TemplateField HeaderText="讲师分类" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblType" runat="server" DictionaryType="Dic_Sys_TeacherType" FieldIDValue='<%# Eval("TeacherTypeID") %>'></cc1:DictionaryLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft" Visible="false">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="ltlDepartMent" runat="server" DictionaryType="Site_DepartmentByOrgID" FieldIDValue='<%# Eval("DepartmentID") %>'></cc1:DictionaryLabel>
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
    <div class="dv_submit">
        <asp:Button ID="lbtnSave" runat="server" CssClass="btn_Ok" OnClick="lbtnSave_Click" ValidationGroup="Error" Text="确定"></asp:Button>
        <input type="button" class="btn_Cancel" value="取消" onclick="javascript:closeWindow();" />
    </div>    
</asp:Content>
