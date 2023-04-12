<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SetsCourseAdd.ascx.cs"
    Inherits="TraningImplement_TraningProjectManager_Controls_SetsCourseAdd" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<div>
    <!--查找条件-->
    <div class="dv_searchbox" runat="server" id="div_Query">
        <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
            id="tableQueryControlList">
            <tr>
                <th width="120">
                    课程编码：
                </th>
                <td width="120">
                    <asp:TextBox ID="txt_CourseCode" runat="server" CssClass="inputbox_120"></asp:TextBox>
                </td>
                <th width="100">
                    课程名称：
                </th>
                <td class="Search_Area">
                    <asp:TextBox ID="txt_CourseName" runat="server" CssClass="inputbox_120 floatleft"></asp:TextBox>
                    <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
                    <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
                </td>
            </tr>
            <tr>
                <th width="100">
                    课程类型：
                </th>
                <td>
                    <cc1:DictionaryDropDownList runat="server" ID="ddl_CourseTypeID" DictionaryType="Dic_Sys_CourseType"
                        IsShowAll="true" />
                </td>
            </tr>
        </table>
    </div>
    <!--设置信息-->
    <div class="dv_pageInformation">
        <h4 class="h4_title" id="title1">
            设置信息</h4>
        <table class="GridviewGray">
            <tr class="hide">
                <th width="100" class="alignright ">
                    授课方式：
                </th>
                <td>
                    <cc1:DictionaryDropDownList runat="server" ID="ddlTeachModelID" DictionaryType="Dic_Sys_TeachModel"
                        SelectedDefaultValue="" IsShowChoose="false" IsShowAll="false" />
                </td>               
            </tr>
            <tr>
                <th class="alignright">
                    培训日期：
                </th>
                <td>
                    <cc1:DateTimeTextBox ID="ttbCourseBeginTime" runat="server" EndTimeControlID="ttbCourseEndTime"
                        ValidationGroup="Saves"></cc1:DateTimeTextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                            Text="*" Display="None" runat="server" ErrorMessage="请填写培训日期开始时间！" ControlToValidate="ttbCourseBeginTime"
                            ValidationGroup="Saves"></asp:RequiredFieldValidator>
                    至
                    <cc1:DateTimeTextBox ID="ttbCourseEndTime" runat="server" BeginTimeControlID="ttbCourseBeginTime"
                        ValidationGroup="Saves"></cc1:DateTimeTextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                            Text="*" Display="None" runat="server" ErrorMessage="请填写培训日期结束时间！" ControlToValidate="ttbCourseEndTime"
                            ValidationGroup="Saves"></asp:RequiredFieldValidator>
                </td>
                 <th width="100" class="alignright">
                    课程属性：
                </th>
                <td>
                    <cc1:DictionaryDropDownList runat="server" ID="ddlCourseAttrID" DictionaryType="Dic_Sys_CourseAttr"
                        IsShowChoose="true" IsShowAll="false" />
                    <font color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                    ValidationExpression="\d[0,*]" Text="*" Display="None" runat="server" ErrorMessage="请选择课程属性！"
                    ControlToValidate="ddlCourseAttrID" ValidationGroup="Saves"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </div>
    <!--查找结果-->
    <div class="dv_searchlist">
        <!--翻页-->
        <div class="dv_pagePanel" id="divPage1">
            <div class="dv_selectAll">
                <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" />
                <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Saves"
                    ShowMessageBox="true" ShowSummary="false" />
                <asp:Button ID="btnAdd" runat="server" Text="确定" CssClass="btn_Ok" OnClick="btnAdd_Click"
                    ValidationGroup="Saves" />
            </div>
            <div class="dv_pageControl">
                <uc2:PageSet ID="PageSet1" runat="server" />
            </div>
        </div>
        <!--列表 begin-->
        <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
            CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0"
            DataKeyNames="CourseID">
            <Columns>
                <asp:TemplateField HeaderText="">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="20" />
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" /><asp:HiddenField ID="hfCourseID" runat="server" Value='<%# Eval("CourseID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CourseCode"  HeaderText="课程编码" ItemStyle-CssClass="alignleft"
                    HeaderStyle-CssClass="alignleft field12" />
                <asp:TemplateField HeaderText="课程名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="10" Text='<%# Eval("CourseName")%>'></cc1:ShortTextLabel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CourseTypeName" HeaderStyle-Width="60" HeaderText="课程类型" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft" />
                <asp:TemplateField HeaderText="课程学时" HeaderStyle-Width="80">
                    <ItemTemplate>
                    <cc1:CustomTextBox ID="ctxtCourseHours" runat="server" Text='<%# Eval("CourseHours") %>' CssClass="inputbox_60" ContentType="Decimal"
                    MaxLength="12"></cc1:CustomTextBox>
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
