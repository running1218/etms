<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CourseNoSelect.ascx.cs"
    Inherits="TraningImplement_CourseStudentManager_Controls_CourseNoSelect" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<!--查找条件-->
<div class="dv_searchbox">
    <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
        id="tableQueryControlList">
        <tr>
            <th style="width: 100px">
                课程编码：
            </th>
            <td style="width: 160px">
                <asp:TextBox ID="txt_Res_Course999CourseCode" runat="server" CssClass="inputbox_120" MaxLength="50"></asp:TextBox>
            </td>
            <th style="width: 100px">
                课程名称：
            </th>
            <td style="width: 200px">
                <asp:TextBox ID="txt_Res_Course999CourseName" runat="server" CssClass="inputbox_120" MaxLength="50"></asp:TextBox>
                <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
            </td>
            <td>
            </td>
        </tr>
    </table>
</div>
<div class="dv_searchlist">
    <!--翻页-->
    <div class="dv_pagePanel" id="divPage1">
        <div class="dv_selectAll">
            <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" />
            <asp:Button ID="btnAdd" runat="server" Text="确定" CssClass="btn_Ok" OnClick="btnAdd_Click" />
        </div>
        <div class="dv_pageControl">
            <uc2:PageSet ID="PageSet1" runat="server" />
        </div>
    </div>
    <!--列表 begin-->
    <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
        CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0"
        DataKeyNames="TrainingItemCourseID" OnRowDataBound="CustomGridView1_RowDataBound" IsRemeberChecks="true">
        <Columns>
            <asp:TemplateField HeaderText="">
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle HorizontalAlign="Center" Width="20" />
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" />
                    <asp:HiddenField ID="hfStudentSignupID" runat="server" Value='<%# Eval("StudentSignupID")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="CourseCode" HeaderText="课程编码" HeaderStyle-CssClass="field12 alignleft"
                ItemStyle-CssClass="alignleft" />
            <asp:TemplateField HeaderText="课程名称" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                <ItemTemplate>
                    <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="10" Text='<%# Eval("CourseName")%>'></cc1:ShortTextLabel>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="培训开始日期" HeaderStyle-Width="90">
                <ItemTemplate>
                    <%# Eval("CourseBeginTime").ToDate()%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="培训截止日期" HeaderStyle-Width="90">
                <ItemTemplate>
                    <%# Eval("CourseEndTime").ToDate()%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="课程属性" HeaderStyle-Width="60">
                <ItemTemplate>
                    <cc1:DictionaryLabel ID="DictionaryCourseAttr" DictionaryType="Dic_Sys_CourseAttr"
                        FieldIDValue='<%# Eval("CourseAttrID") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="授课方式" HeaderStyle-Width="60">
                <ItemTemplate>
                    <cc1:DictionaryLabel ID="DictionaryLabel1" DictionaryType="Dic_Sys_TeachModel" FieldIDValue='<%# Eval("TeachModelID") %>'
                        runat="server" />
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
