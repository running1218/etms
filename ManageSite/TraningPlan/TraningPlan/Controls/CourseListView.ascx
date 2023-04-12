<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CourseListView.ascx.cs"
    Inherits="TraningPlan_TraningPlan_Controls_CourseListView" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<div class="dv_searchlist">
    <!--翻页-->
    <div class="dv_pagePanel" id="divPage1">
        <div class="dv_pageControl">
            <uc2:PageSet ID="PageSet1" runat="server" />
        </div>
    </div>
    <!--列表 begin-->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
                DataKeyNames="PlanCourseID" >
                <Columns>
                    <asp:TemplateField HeaderText="课程编码" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft field12">
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
                    <asp:TemplateField HeaderText="授课方式" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblTeachModel" DictionaryType="Dic_Sys_TeachModel" runat="server"
                                FieldIDValue='<%# Eval("TeachModelID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="预算（元）" HeaderStyle-Width="70" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright">
                        <ItemTemplate>
                            <%# Eval("BudgetFee")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="培训方式" HeaderStyle-Width="60" >
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="dlblTrainingModel" DictionaryType="Dic_Sys_TrainingModel"
                                runat="server" FieldIDValue='<%# Eval("TrainingModelID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="外训机构" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft" >
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="dlblOuterOrg" DictionaryType="Tr_OuterOrg" runat="server"
                                FieldIDValue='<%# Eval("OuterOrgID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
            </cc1:CustomGridView>
    <!--列表 end-->
    <div class="dv_splitLine">
    </div>
    <!--翻页-->
    <div class="dv_pagePanel" id="divPage2">
    </div>
</div>
