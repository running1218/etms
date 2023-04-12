<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SetsPlanCourseAdd.ascx.cs"
    Inherits="TraningImplement_TraningProjectManager_Controls_SetsPlanCourseAdd" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<div>
    <!--设置信息-->
    <div class="dv_pageInformation">
        <h4 class="h4_title" id="title1">
            设置信息</h4>
        <table class="GridviewGray">
            <tr>
                <th>
                    授课方式：
                </th>
                <td>
                    <cc1:DictionaryDropDownList runat="server" ID="ddlTeachModelID" DictionaryType="Dic_Sys_TeachModel"
                        SelectedDefaultValue="" IsShowChoose="false" IsShowAll="false" />
                </td>
                <th>
                    课程属性：
                </th>
                <td>
                    <cc1:DictionaryDropDownList runat="server" ID="ddlCourseAttrID" DictionaryType="Dic_Sys_CourseAttr"
                        IsShowChoose="false" IsShowAll="false" />
                </td>
            </tr>
            <tr>
                <th>
                    培训日期：
                </th>
                <td colspan="3">
                    <cc1:DateTimeTextBox ID="ttbCourseBeginTime" runat="server" EndTimeControlID="ttbCourseEndTime"
                        ValidationGroup="PlanSaves"></cc1:DateTimeTextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                            Text="*" Display="None" runat="server" ErrorMessage="请填写培训日期开始时间！" ControlToValidate="ttbCourseBeginTime"
                            ValidationGroup="PlanSaves"></asp:RequiredFieldValidator>
                    至
                    <cc1:DateTimeTextBox ID="ttbCourseEndTime" runat="server" BeginTimeControlID="ttbCourseBeginTime"
                        ValidationGroup="PlanSaves"></cc1:DateTimeTextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                            Text="*" Display="None" runat="server" ErrorMessage="请填写培训日期结束时间！" ControlToValidate="ttbCourseEndTime"
                            ValidationGroup="PlanSaves"></asp:RequiredFieldValidator>
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
                <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="PlanSaves"
                    ShowMessageBox="true" ShowSummary="false" />
                <asp:Button ID="btnAdd" runat="server" Text="确定" CssClass="btn_Ok" OnClick="btnAdd_Click"
                    ValidationGroup="PlanSaves" />
            </div>
            <div class="dv_pageControl">
                <uc2:PageSet ID="PageSet1" runat="server" />
            </div>
        </div>
        <!--列表 begin-->
        <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
            CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0"
            DataKeyNames="CourseID" OnRowDataBound="CustomGridView1_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="20" />
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" /><asp:HiddenField ID="hfCourseID" runat="server" Value='<%# Eval("CourseID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CourseCode" HeaderText="课程编码" ItemStyle-CssClass="alignleft"
                    HeaderStyle-CssClass="alignleft field12" />
                <asp:TemplateField HeaderText="课程名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="10" Text='<%# Eval("CourseName")%>'></cc1:ShortTextLabel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="课程学时" HeaderStyle-Width="80">
                    <ItemTemplate>
                    <cc1:CustomTextBox ID="ctxtCourseHours" runat="server" Text='<%# Eval("CourseHours") %>' CssClass="inputbox_60" ContentType="Decimal"
                    MaxLength="12"></cc1:CustomTextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CourseTypeName" HeaderStyle-Width="60" HeaderText="课程类型" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft"/>
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
