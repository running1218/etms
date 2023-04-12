<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="TeacherItemList.aspx.cs" Inherits="Security_TeacherQuery_TeacherItemList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" runat="Server">
<asp:LinkButton ID="lbtnReturn" runat="server" CssClass="btn_Return" Text="返回"></asp:LinkButton>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table id="Table1" class="GridviewGray fixedTable" border="0" cellpadding="0" cellspacing="0"
                runat="server">
                <tr>
                    <th width="100">
                        讲师姓名：
                    </th>
                    <td width="200">
                        <asp:Label ID="lblTeacherName" runat="server"></asp:Label>
                    </td>
                    <th width="100">
                        <asp:Label ID="lblOrgName" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Label>/
                        <asp:Literal ID="ltlDepartment" runat="server" Text="<%$ Resources:UIResource, ui_org%>"></asp:Literal>：
                    </th>
                    <td>
                        <asp:Label ID="lblOrg" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel" id="divPage1">
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
                CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0"
                DataKeyNames="TrainingItemID" OnRowDataBound="CustomGridView1_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="项目名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblItemName" runat="server" ShowTextNum="10" Text='<%# Eval("ItemName")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="开始时间" HeaderStyle-Width="80">
                        <ItemTemplate>
                            <%# Eval("ItemBeginTime").ToDate()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="结束时间" HeaderStyle-Width="80">
                        <ItemTemplate>
                            <%# Eval("ItemEndTime").ToDate() %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="项目状态" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="DictionaryLabel1" DictionaryType="Dic_TraningProjectType"
                                FieldIDValue='<%# Eval("ItemStatus") %>' runat="server" />
                            <asp:HiddenField ID="Hf_ItemStatus" runat="server" Value='<%# Eval("ItemStatus") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="是否发布" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblIsIssue" DictionaryType="Dic_TrueOrFalseBool" FieldIDValue='<%# Eval("IsIssue") %>'
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="项目级别" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="dlblTrainingLevel" DictionaryType="Dic_Sys_TrainingLevel"
                                runat="server" FieldIDValue='<%# Eval("TrainingLevelID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="项目负责人" HeaderStyle-CssClass="alignleft field8" ItemStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <%# Eval("DutyUser")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="手机" HeaderStyle-CssClass="field12" >
                        <ItemTemplate>
                            <%# Eval("Mobile")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作" HeaderStyle-Width="60">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnView" runat="server">查看</asp:LinkButton></ItemTemplate>
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
