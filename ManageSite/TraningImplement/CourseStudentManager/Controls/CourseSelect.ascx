<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CourseSelect.ascx.cs"
    Inherits="TraningImplement_CourseStudentManager_Controls_CourseSelect" %>
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
                <asp:TextBox ID="txt_e999CourseCode" runat="server" CssClass="inputbox_120" MaxLength="50"></asp:TextBox>
            </td>
            <th style="width: 100px">
                课程名称：
            </th>
            <td style="width: 200px">
                <asp:TextBox ID="txt_e999CourseName" runat="server" CssClass="inputbox_120" MaxLength="50"></asp:TextBox>
                <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
            </td>
            <td>
            </td>
        </tr>
    </table>
</div>
<!--查找结果-->
<div class="dv_searchlist">
    <!--翻页-->
    <div class="dv_pagePanel" id="divPage1">
        <div class="dv_selectAll">
        </div>
        <div class="dv_pageControl">
            <uc2:PageSet ID="PageSet1" runat="server" />
        </div>
    </div>
    <!--列表 begin-->
    <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
        CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0"
        DataKeyNames="StudentCourseID" OnRowDataBound="CustomGridView1_RowDataBound"
        OnRowCommand="CustomGridView1_RowCommand">
        <Columns>
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
            <asp:TemplateField HeaderText="学员课程状态" HeaderStyle-Width="100">
                <ItemTemplate>
                    <cc1:DictionaryLabel ID="dlblIsUseStudentCourse" DictionaryType="Dic_Status" FieldIDValue='<%# Eval("IsUseStudentCourse") %>'
                        runat="server" />
                    <asp:HiddenField ID="hfCourseStatus" runat="server" Value='<%# Eval("IsUseStudentCourse") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作" HeaderStyle-Width="60">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtn_Open" runat="server" CommandName="Open" CommandArgument='<%# Eval("StudentCourseID")%>'>启用</asp:LinkButton><asp:LinkButton
                        ID="lbtn_Close" runat="server" CommandName="Close" CommandArgument='<%# Eval("StudentCourseID")%>'
                        Visible="false">停用</asp:LinkButton>
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
