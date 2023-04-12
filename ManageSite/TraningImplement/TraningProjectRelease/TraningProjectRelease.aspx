<%@ Page Title="培训项目发布" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="TraningProjectRelease.aspx.cs" Inherits="TraningImplement_TraningProjectRelease_TraningProjectRelease" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" runat="Server">
    <a id="ABack" runat="server" class="btn_Return">返回</a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--查找条件-->
        <div class="dv_GradeviewList">
            <table class="" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <th width="100">
                        项目名称：
                    </th>
                    <td width="200">
                        <asp:Label ID="lbl_ItemName" runat="server"></asp:Label>
                    </td>
                    <th width="100">
                        项目周期：
                    </th>
                    <td>
                        <asp:Label ID="lbl_ItemBeginTime" runat="server"></asp:Label>~<asp:Label ID="lbl_ItemEndTime" runat="server"></asp:Label>
                    </td>  
                </tr>
                <tr class="hide">
                    <th width="100">
                        允许报名：
                    </th>
                    <td colspan="3">
                        <asp:Label ID="lbl_IsAllowSignup" runat="server"></asp:Label>
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
                DataKeyNames="TrainingItemCourseID" OnRowCommand="CustomGridView1_RowCommand"
                OnRowDataBound="CustomGridView1_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="CourseCode" HeaderText="课程编码"  ItemStyle-CssClass="alignleft"
                        HeaderStyle-CssClass="alignleft field12" />
                    <asp:TemplateField HeaderText="课程名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="10" Text='<%# Eval("CourseName")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="培训日期" HeaderStyle-Width="80">
                        <ItemTemplate>
                            <%# Eval("CourseBeginTime").ToDate()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="培训方式" HeaderStyle-Width="80" HeaderStyle-CssClass="hide" ItemStyle-CssClass="hide">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="DictionaryTrainingModel" DictionaryType="Dic_Sys_TrainingModel"
                                FieldIDValue='<%# Eval("TrainingModelID") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="授课方式" HeaderStyle-Width="80" HeaderStyle-CssClass="hide" ItemStyle-CssClass="hide">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="DictionaryTeachModel" DictionaryType="Dic_Sys_TeachModel"
                                FieldIDValue='<%# Eval("TeachModelID") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="报名人数" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <asp:Label ID="lblSignupNumbers" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="状态" HeaderStyle-Width="50">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblCourseStatus" DictionaryType="Dic_Status" FieldIDValue='<%# Eval("CourseStatus") %>'
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtn_Open" runat="server" CommandName="Open" CommandArgument='<%# Eval("TrainingItemCourseID")%>'>启用</asp:LinkButton>
                            <asp:LinkButton ID="lbtn_Close" runat="server" CommandName="Close" CommandArgument='<%# Eval("TrainingItemCourseID")%>'>停用</asp:LinkButton>
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
            <div class="center">
                <cc1:CustomButton runat="server" ID="btn_Release" CssClass="btn_Deploy" Text="发布"
                    OnClick="btn_Release_Click" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="此操作涉及很多后台的处理，学员学习课程信息不能修改，操作不可逆，请慎重操作！" />
            </div>
        </div>
    </div>
</asp:Content>
