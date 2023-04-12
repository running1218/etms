<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TraningProjectList.ascx.cs"
    Inherits="TraningImplement_TraningProjectManager_Controls_TraningProjectList" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<div>
    <!--查找条件-->
    <div class="dv_searchbox">
        <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
            id="tableQueryControlList">
            <tr>
                <th width="100">
                    项目编码：
                </th>
                <td width="200">
                    <asp:TextBox ID="txt_ItemCode" runat="server" CssClass="inputbox_120"></asp:TextBox>
                </td>
                <th width="100">
                    项目名称：
                </th>
                <td class="Search_Area">
                    <asp:TextBox ID="txt_ItemName" runat="server" CssClass="inputbox_120 floatleft"></asp:TextBox>
                    <asp:UpdatePanel ID="upQuery" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
                </td>
            </tr>
            <tr>
                <th width="100">
                    来自计划：
                </th>
                <td>
                    <cc1:DictionaryDropDownList runat="server" ID="ddl_IsPlanItem" DictionaryType="Dic_TrueOrFalse"
                        IsShowAll="true" />
                </td>
                <th width="100">
                    培训级别：
                </th>
                <td>
                    <cc1:DictionaryDropDownList runat="server" ID="ddl_TrainingLevelID" DictionaryType="Dic_Sys_TrainingLevel"
                        IsShowAll="true" />
                </td>
                
            </tr>
            <tr>
                <th width="100">
                    专业类别：
                </th>
                <td>
                    <cc1:DictionaryDropDownList runat="server" ID="ddl_SpecialtyTypeCode" DictionaryType="Dic_Sys_SpecialtyType"
                        IsShowAll="true" />
                </td>
                <th width="100">
                    项目状态：
                </th>
                <td>
                    <cc1:DictionaryDropDownList runat="server" ID="ddl_ItemStatus" DictionaryType="Dic_TraningProjectType"
                        IsShowAll="true" />
                </td>
            </tr>
            <tr>
                <th width="100">
                    允许报名：
                </th>
                <td colspan="3">
                    <cc1:DictionaryDropDownList runat="server" ID="ddl_IsAllowSignup" DictionaryType="Dic_TrueOrFalse"
                        IsShowAll="true" />
                </td>
            </tr>
            <tr>
                <th width="100">
                    创建时间：
                </th>
                <td colspan="3">
                    <cc1:DateTimeTextBox ID="begin_CreateTime" runat="server" EndTimeControlID="end_CreateTime"></cc1:DateTimeTextBox>
                    至
                    <cc1:DateTimeTextBox ID="end_CreateTime" runat="server" BeginTimeControlID="begin_CreateTime"></cc1:DateTimeTextBox>
                </td>
            </tr>
        </table>
    </div>
    <!--查找结果-->
    <div class="dv_searchlist">
        <asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <!--翻页-->
                <div class="dv_pagePanel" id="divPage1">
                    <div class="dv_selectAll">
                        <input id="btnAdd" type="button" class="btn_Add" runat="server" value="新增" visible="false"
                            onclick="javascript:showWindow('新增培训项目','TraningProjectAdd.aspx',700,540)" />
                    </div>
                    <div class="dv_pageControl">
                        <uc2:PageSet ID="PageSet1" runat="server" />
                    </div>
                </div>
                <!--列表 begin-->
                <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
                    CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0"
                    DataKeyNames="TrainingItemID" OnRowDataBound="CustomGridView1_RowDataBound" OnRowCommand="CustomGridView1_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="项目编码">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblItemCode" runat="server" ShowTextNum="50" Text='<%# Eval("ItemCode")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="项目名称">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblItemName" runat="server" ShowTextNum="50" Text='<%# Eval("ItemName")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="项目状态">
                            <ItemTemplate>
                                <cc1:DictionaryLabel ID="DictionaryLabel1" DictionaryType="Dic_TraningProjectType"
                                    FieldIDValue='<%# Eval("ItemStatus") %>' runat="server" />
                                <asp:HiddenField ID="Hf_ItemStatus" runat="server" Value='<%# Eval("ItemStatus") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="项目开始时间">
                            <ItemTemplate>
                                <%# Eval("ItemBeginTime").ToDate()%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="项目结束时间">
                            <ItemTemplate>
                                <%# Eval("ItemEndTime").ToDate() %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="课程数">
                            <ItemTemplate>
                                <%#  new ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Tr_ItemCourseLogic().GetItemCourseCountByTrainingItemID(new Guid(Eval("TrainingItemID").ToString()))%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="学员数">
                            <ItemTemplate>
                                <%# new ETMS.Components.Basic.Implement.BLL.TrainingItem.Student.Sty_StudentSignupLogic().GetTrainingItemStudentTotal(new Guid(Eval("TrainingItemID").ToString()))%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="允许报名">
                            <ItemTemplate>
                                <cc1:DictionaryLabel ID="DictionaryLabel2" DictionaryType="Dic_TrueOrFalseBool" FieldIDValue='<%# Eval("IsAllowSignup") %>'
                                    runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="操作">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:LinkButton ID="Lbtn_Edit" runat="server" Enabled="false">编辑</asp:LinkButton><asp:LinkButton
                                    ID="Lbtn_CourseList" runat="server" Enabled="false">课程</asp:LinkButton><cc1:CustomLinkButton
                                        runat="server" ID="lbtnDel" CommandName="del" CommandArgument='<%# Eval("TrainingItemID") %>'
                                        Text="删除" EnableConfirm="false" ConfirmTitle="提示" ConfirmMessage="确定删除吗？" Enabled="false" /><asp:LinkButton
                                            ID="lbtnStudent" runat="server" Visible="false">设置学员</asp:LinkButton><asp:LinkButton
                                                ID="lbtnView" runat="server">查看</asp:LinkButton></ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </cc1:CustomGridView>
                <!--列表 end-->
                <div class="dv_splitLine">
                </div>
                <!--翻页-->
                <div class="dv_pagePanel" id="divPage2">
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
<script type="text/javascript">
    function showWindowPage(title, url) {
        showWindow(title, url);
        return false;
    }
</script>
