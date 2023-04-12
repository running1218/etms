<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="ExerciseList.aspx.cs" Inherits="QuestionDB_ExOfflineHomework_ExerciseList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" runat="Server">
    <a id="aBack" runat="server" class="btn_Return">返回</a>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="dv_searchbox">
        <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
            id="tableQueryControlList">
            <tr>
                <th style=" width:15%;">
                    作业名称：
                </th>
                <td style=" width:15%;">
                    <asp:TextBox ID="txt_Res_e_OffLineJob999JobName" runat="server"></asp:TextBox>
                </td>
                <th style=" width:15%;">
                    状态：
                </th>
                <td style=" width:15%;">
                    <asp:DropDownList runat="server" ID="ddl_Res_e_OffLineJob999IsUse">
                        <asp:ListItem Value="-1">全部</asp:ListItem>
                        <asp:ListItem Value="1">启用</asp:ListItem>
                        <asp:ListItem Value="0">停用</asp:ListItem>
                    </asp:DropDownList>                    
                </td>
                <td style=" width:40%;">
                    <asp:Button ID="btnSearch" runat="server" CssClass="btn_Search" Text="查询" OnClick="btnSearch_Click" />
                </td>
               
            </tr>
        </table>
    </div>
    <asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="dv_searchlist">
                <!--翻页-->
                <div class="dv_pagePanel">
                    <div class="dv_selectAll">
                        <asp:Button runat="server" ID="btnAdd" CssClass="btn_Add" Text="新增" />                        
                    </div>
                    <div class="dv_pageControl">
                        <uc2:PageSet ID="PageSet1" runat="server" />
                    </div>
                </div>
                <cc1:CustomGridView ID="GridViewList" runat="server" AutoGenerateColumns="False"
                    OnRowCommand="GridViewList_RowCommand" OnRowDataBound="GridViewList_RowDataBound"
                    DataKeyNames="ItemCourseOffLineJobID">
                    <Columns>
                        <asp:TemplateField HeaderText="序号" HeaderStyle-Width="40">
                            <ItemStyle HorizontalAlign="Center" Width="60" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="LabNo" runat="server" Text='<%#((this.PageSet1.PageIndex -1) * this.PageSet1.PageSize) + Container.DataItemIndex+1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                        <asp:TemplateField HeaderText="作业名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblJobName" runat="server" ShowTextNum="10" Text='<%# Eval("JobName")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                        <asp:TemplateField HeaderText="作业附件" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                            <ItemStyle HorizontalAlign="Center" CssClass="alignleft" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:HyperLink ID="lblJobFileName" runat="server" Text='<%#Eval("JobFileName")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="状态" HeaderStyle-Width="60">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lbnIsUse" runat="server" Text='<%#Eval("IsUse").ToInt()==1?"启用":"停用" %>' />
                               
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="开始时间" HeaderStyle-Width="80">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblBeginDate" runat="server" Text='<%# Eval("BeginTime").ToDate()%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="结束时间" HeaderStyle-Width="80">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblEndDate" runat="server" Text='<%#Eval("EndTime").ToDate()%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="创建时间" HeaderStyle-Width="80">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblCreateTime" runat="server" Text='<%#Eval("CreateTime").ToDate()%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="创建人" HeaderStyle-Width="60">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="TeacherID" runat="server" Text='<%#Eval("TeacherID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="操作" HeaderStyle-Width="100">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:LinkButton ID="btnModify" runat="server" Text="编辑" />
                                
                                <cc1:CustomLinkButton runat="server" ID="lbtn_Del" CommandArgument='<%# Eval("JobID") %>'
                                    CommandName="Del" Text="删除" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="确定要删除吗？" />
                                <asp:LinkButton ID="ltnEvaluation" runat="server" Text="批阅" PostBackUrl='<%# this.ActionHref(string.Format("ExcerciseStatus.aspx?JobID={0}&TrainingItemCourseID={1}&CourseID={2}&TrainingItemID={3}&ItemCourseOffLineJobID={4}",Eval("JobID").ToGuid(),TrainingItemCourseID,CourseID,TrainingItemID,Eval("ItemCourseOffLineJobID").ToGuid()))%>' />                                
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </cc1:CustomGridView>
                <!--列表 end-->
                <div class="dv_splitLine">
                </div>
                <!--翻页-->
                <div class="dv_pagePanel">
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
