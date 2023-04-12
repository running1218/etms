<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="OnLineHomeWorkAdd.aspx.cs" Inherits="TraningImplement_ProjectCourseResource_OnLineHomeWorkAdd" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" runat="Server">
    <a class="btn_Return" runat="server" id="aBack">返回</a>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div>
                <!--查找条件-->
                <div class="dv_searchbox">
                    <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <th width="120">
                                项目名称：
                            </th>
                            <td>
                                <asp:Label ID="lblItemName" runat="server"></asp:Label>
                            </td>
                            <th width="120">
                                课程名称：
                            </th>
                            <td>
                                <asp:Label ID="lblCourseName" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <!--设置信息-->
                <div class="dv_pageInformation">
                    <h4 class="h4_title" id="title1">
                        设置信息</h4>
                    <table class="GridviewGray">
                        <tr>
                            <th>
                                开始时间：
                            </th>
                            <td>
                                <cc1:DateTimeTextBox ID="ttbResBeginTime" runat="server" EndTimeControlID="ttbResEndTime"
                                    ValidationGroup="Saves"></cc1:DateTimeTextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                        Text="*" Display="None" runat="server" ErrorMessage="请填写开始时间！" ControlToValidate="ttbResBeginTime"
                                        ValidationGroup="Saves"></asp:RequiredFieldValidator>
                            </td>
                            <th>
                                结束时间：
                            </th>
                            <td>
                                <cc1:DateTimeTextBox ID="ttbResEndTime" runat="server" BeginTimeControlID="ttbResBeginTime"
                                    ValidationGroup="Saves"></cc1:DateTimeTextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                                        Text="*" Display="None" runat="server" ErrorMessage="请填写结束时间！" ControlToValidate="ttbResEndTime"
                                        ValidationGroup="Saves"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </div>
                <!--查找结果-->
                <div class="dv_searchlist">
                    <!--翻页-->
                    <div class="dv_pagePanel" id="divPage1">
                        <div class="dv_selectAll">
                            <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" /><asp:Button
                                ID="btnAdd" runat="server" Text="确定" CssClass="btn_Ok" OnClick="btnAdd_Click"
                                ValidationGroup="Saves" /><asp:ValidationSummary runat="server" ID="ValidationSummary1"
                                    ValidationGroup="Saves" ShowMessageBox="true" ShowSummary="false" />
                        </div>
                        <div class="dv_pageControl">
                            <uc2:PageSet ID="PageSet1" runat="server" />
                        </div>
                    </div>
                    <!--列表 begin-->
                    <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
                        CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="1"
                        DataKeyNames="OnLineJobID">
                        <Columns>
                            <asp:TemplateField HeaderText="">
                                <ItemStyle HorizontalAlign="Center" Width="28" />
                                <HeaderStyle HorizontalAlign="Center" width="20" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="作业名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                                <ItemTemplate>
                                    <cc1:ShortTextLabel ID="lblOnLineJobName" runat="server" ShowTextNum="10" Text='<%# Eval("OnLineJobName")%>'></cc1:ShortTextLabel>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CreateUser" HeaderText="创建人" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft" HeaderStyle-Width="60"/>
                            <asp:TemplateField HeaderText="创建时间" HeaderStyle-Width="80">
                                <ItemTemplate>
                                    <%# Eval("CreateTime").ToDate()%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="操作" HeaderStyle-Width="60">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <a href='<%#  this.ActionHref(string.Format("~/QuestionDB/Testpaper/TestpaperView.aspx?ExerciseType=3&ExerciseID={0}", Eval("OnLineJobID")))%>'
                                        target="_blank">预览</a>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
