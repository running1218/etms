<%@ Page Title="新增课程" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="SetsCourseAdd.aspx.cs" Inherits="ETMS.WebApp.Manage.TraningPlan.TraningPlan.SetsCourseAdd" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" runat="Server">
    <a id="aBack" runat="server" class="btn_Return">返回</a>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="">
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
                id="tableQueryControlList">
                <tr>
                    <th width="100">
                        课程编码：
                    </th>
                    <td width="200">
                        <asp:TextBox ID="txt_CourseCode" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                    <th width="100">
                        课程名称：
                    </th>
                    <td>
                        <asp:TextBox ID="txt_CourseName" runat="server" CssClass="inputbox_120"></asp:TextBox>
                        <asp:Button ID="btn_Search" runat="server" Text="查询" OnClick="btn_Search_Click" SkinID="Search" />
                        <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
                    </td>
                </tr>
                <tr>
                    <th>
                        课程类型：
                    </th>
                    <td colspan="3">
                        <cc1:DictionaryDropDownList runat="server" ID="ddl_CourseTypeID" DictionaryType="Dic_Sys_CourseType"
                            IsShowAll="true" />
                    </td>
                </tr>
            </table>
        </div>
        <!--设置信息-->
        <div class="dv_pageInformation" style="display:none;">
            <h4 class="h4_title" id="title1">
                设置信息</h4>
            <table class="GridviewGray">
                <tr>
                    <th width="150" style="text-align:right;">
                        培训方式：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="ddlTrainingModel" DictionaryType="Dic_Sys_TrainingModel"
                            IsShowChoose="false" IsShowAll="false" />
                    </td>
                    <th width="150" style="text-align:right;">
                        授课方式：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="ddlTeachModelID" DictionaryType="Dic_Sys_TeachModel"
                            SelectedDefaultValue="" IsShowChoose="false" IsShowAll="false" />
                    </td>
                </tr>
                <tr>
                    <th style="text-align:right;">
                        预&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;算：
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txtBudgetFee" runat="server" CssClass="inputbox_120" Text="0"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtBudgetFee"
                            Display="None" ErrorMessage="预算格式错误！" ValidationExpression="\d{0,8}(\.\d{1,2})?" ValidationGroup="Saves"></asp:RegularExpressionValidator><asp:RequiredFieldValidator
                                ID="RequiredFieldValidator1" Text="*" Display="None" runat="server" ErrorMessage="请填写预算！"
                                ControlToValidate="txtBudgetFee" ValidationGroup="Saves"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel" id="divPage1">
                <div class="dv_selectAll">
                    <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" /><asp:Button
                        ID="btnAdd" runat="server" CssClass="btn_Ok" Text="确定" ValidationGroup="Saves"
                        OnClick="btnAdd_Click" /><asp:ValidationSummary runat="server" ID="ValidationSummary1"
                            ValidationGroup="Saves" ShowMessageBox="true" ShowSummary="false" />
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
                CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0" DataKeyNames="CourseID">
                <Columns>
                    <asp:TemplateField HeaderText="">
                        <ItemStyle HorizontalAlign="Center"  />
                        <HeaderStyle HorizontalAlign="Center" Width="18" />
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程编码"  ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft field12">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblCourseCode" runat="server" ShowTextNum="10" Text='<%# Eval("CourseCode")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="10" Text='<%# Eval("CourseName")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程类型" HeaderStyle-Width="60" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="dlblCourseType" DictionaryType="Dic_Sys_CourseType" runat="server"
                                FieldIDValue='<%# Eval("CourseTypeID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程等级" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="dlblCourseLevel" DictionaryType="Dic_Sys_CourseLevel" runat="server"
                                FieldIDValue='<%# Eval("CourseLevelID") %>' />
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
