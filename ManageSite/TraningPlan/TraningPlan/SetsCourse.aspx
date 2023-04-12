<%@ Page Title="设置课程" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="SetsCourse.aspx.cs" Inherits="ETMS.WebApp.Manage.TraningPlan.TraningPlan.SetsCourse" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" runat="Server">
    <a id="aBack" runat="server" class="btn_Return">返回</a>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <th width="100">
                        计划编码：
                    </th>
                    <td style="width:180px;">
                        <asp:Label ID="lblPlanCode" runat="server"></asp:Label>
                    </td>
                    <th width="100">
                        计划名称：
                    </th>
                    <td>
                        <asp:Label ID="lblPlanName" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <cc1:DictionaryDropDownList runat="server" ID="dddlTeachModely" DictionaryType="Dic_Sys_TeachModel"
                IsShowChoose="false" IsShowAll="false" Visible="false" />
            <cc1:DictionaryDropDownList runat="server" ID="dddlTrainingModel" DictionaryType="Dic_Sys_TrainingModel"
                IsShowChoose="false" IsShowAll="false" Visible="false" />
            <cc1:DictionaryDropDownList ID="dddlOuterOrg" DictionaryType="Tr_OuterOrg" runat="server"
                IsShowChoose="true" Visible="false" />
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel" id="divPage1">
                <div class="dv_selectAll">
                    <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" /><asp:Button
                        ID="btnAdd" runat="server" CssClass="btn_Add" Text="新增" /><cc1:CustomButton runat="server"
                            ID="cbtnDel" Text="删除" CssClass="btn_Del" EnableConfirm="true" ConfirmTitle="提示"
                            ConfirmMessage="确定删除吗？" OnClick="cbtnDel_Click" />
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
                DataKeyNames="PlanCourseID" OnRowCommand="CustomGridView1_RowCommand" OnRowDataBound="CustomGridView1_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center"  Width="18"/>
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
                    <asp:TemplateField HeaderText="授课方式" HeaderStyle-Width="70" >
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblTeachModel" DictionaryType="Dic_Sys_TeachModel" runat="server"
                                FieldIDValue='<%# Eval("TeachModelID") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" ID="ddlTeachModel"  CssClass="select_60" />
                            <asp:HiddenField ID="Hf_TeachModelID" runat="server" Value='<%# Eval("TeachModelID") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="预算（元）" HeaderStyle-Width="70" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright">
                        <ItemTemplate>
                            <%# Eval("BudgetFee")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtBudgetFee" runat="server" Text='<%# Eval("BudgetFee")%>' CssClass="inputbox_60"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="培训方式" HeaderStyle-Width="80" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="dlblTrainingModel" DictionaryType="Dic_Sys_TrainingModel"
                                runat="server" FieldIDValue='<%# Eval("TrainingModelID") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" ID="ddlTrainingModel"   CssClass="select_60"/>
                            <asp:HiddenField ID="Hf_TrainingModelID" runat="server" Value='<%# Eval("TrainingModelID") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="外训机构" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft" HeaderStyle-Width="110"  >
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="dlblOuterOrg" DictionaryType="Tr_OuterOrg" runat="server"
                                FieldIDValue='<%# Eval("OuterOrgID") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" ID="ddlOuterOrg"  CssClass="select_100"/>
                            <asp:HiddenField ID="Hf_OuterOrgID" runat="server" Value='<%# Eval("OuterOrgID") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="操作" HeaderStyle-Width="80">
                        <ItemTemplate>
                            <%--<asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Edit" Text="编辑"></asp:LinkButton>--%>
                            <asp:LinkButton ID="lbtnView" runat="server" CommandName="View" Text="查看"></asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="lbtnSave" runat="server" CommandName="Save" Text="保存" CssClass="btn_savedata"></asp:LinkButton><asp:LinkButton
                                ID="lbtnCancel" runat="server" CommandName="Cancel" Text="取消" CssClass="btn_canceldata"></asp:LinkButton>
                        </EditItemTemplate>
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
