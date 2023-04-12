<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TraningCourseListView.ascx.cs"
    Inherits="TraningImplement_TraningProjectManager_Controls_TraningCourseListView" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<div>
    <!--查找条件-->
    <div class="dv_searchbox">
        <table class="GridviewGray fixedTable" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <th>
                    项目编码：
                </th>
                <td>
                    <asp:Label ID="Lbl_ItemCode" runat="server" Text=""></asp:Label>
                </td>
                <th>
                    项目名称：
                </th>
                <td>
                    <asp:Label ID="Lbl_ItemName" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <!--查找结果-->
    <div class="dv_searchlist">
        <!--翻页-->
        <div class="dv_pagePanel" id="divPage3">
            <div class="dv_pageControl">
                <uc2:PageSet ID="PageSet1" runat="server" />
            </div>
        </div>
        <!--列表 begin-->
        <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
            CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0"
            DataKeyNames="TrainingItemCourseID" OnRowDataBound="CustomGridView1_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="课程编码" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft "  >
                    <ItemTemplate>
                        <cc1:ShortTextLabel ID="lblCourseCode" runat="server" ShowTextNum="50" Text='<%# Eval("CourseCode")%>'></cc1:ShortTextLabel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="课程名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="50" Text='<%# Eval("CourseName")%>'></cc1:ShortTextLabel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="培训方式" HeaderStyle-Width="60" HeaderStyle-CssClass="hide" ItemStyle-CssClass="hide">
                    <ItemTemplate>
                        <cc1:DictionaryLabel ID="DictionaryTrainingModel" DictionaryType="Dic_Sys_TrainingModel"
                            FieldIDValue='<%# Eval("TrainingModelID") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="课程属性" HeaderStyle-Width="60" HeaderStyle-CssClass="hide" ItemStyle-CssClass="hide">                    
                    <ItemTemplate>
                        <cc1:DictionaryLabel ID="DictionaryCourseAttr" DictionaryType="Dic_Sys_CourseAttr"
                            FieldIDValue='<%# Eval("CourseAttrID") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="授课方式" HeaderStyle-Width="60">
                    <ItemTemplate>
                        <cc1:DictionaryLabel ID="DictionaryTeachModel" DictionaryType="Dic_Sys_TeachModel"
                            FieldIDValue='<%# Eval("TeachModelID") %>' runat="server" />
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
                <asp:BoundField DataField="BudgetFee" HeaderText="预算" HeaderStyle-CssClass="hide" ItemStyle-CssClass="hide" HeaderStyle-Width="80" />
                <asp:TemplateField HeaderText="状态" HeaderStyle-Width="40">
                    <ItemTemplate>
                        <cc1:DictionaryLabel ID="dlblCourseStatus" DictionaryType="Dic_Status" FieldIDValue='<%# Eval("CourseStatus") %>'
                            runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="负责讲师" ItemStyle-CssClass="alignright" HeaderStyle-CssClass="alignright"
                    HeaderStyle-Width="60">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnTeacherTotal" runat="server"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </cc1:CustomGridView>
        <!--列表 end-->
        <div class="dv_splitLine">
        </div>
        <!--翻页-->
        <div class="dv_pagePanel" id="divPage4">
        </div>
    </div>
</div>
