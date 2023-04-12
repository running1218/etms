<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MPageAdmin.Master" CodeFile="TrainingProjectPracticeList.aspx.cs" Inherits="TraningImplement_TraningProjectManager_TrainingProjectPracticeList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
           <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
                id="tableQueryControlList">
                <tr>
                    <th width="120">
                        实践名称：
                    </th>
                    <td width="130">
                        <asp:TextBox ID="txt_JobName" runat="server" CssClass="inputbox_120" MaxLength="50"></asp:TextBox>
                    </td>
                    <th width="100">
                        项目名称：
                    </th>
                    <td class="Search_Area">
                        <asp:TextBox ID="txt_ItemName" runat="server" CssClass="inputbox_120 floatleft" MaxLength="100"></asp:TextBox>
                        <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" OnClick="btnSearch_Click"  Text="查询" />
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
                    DataKeyNames="JobID">
                    <Columns>
                        <asp:TemplateField HeaderText="序号" HeaderStyle-Width="40">
                            <ItemStyle HorizontalAlign="Center" Width="60" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="LabNo" runat="server" Text='<%#((this.PageSet1.PageIndex -1) * this.PageSet1.PageSize) + Container.DataItemIndex+1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblJobName" runat="server" ShowTextNum="10" Text='<%# Eval("JobName")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="所属项目" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft"> 
                            <ItemStyle HorizontalAlign="Center" CssClass="alignleft" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:HyperLink ID="lblItemName" runat="server" Text='<%#Eval("ItemName")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="状态" HeaderStyle-Width="60">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <cc1:DictionaryLabel ID="lblStatus" runat="server" DictionaryType="Dic_Status" FieldIDValue='<%# Eval("IsUse") %>'></cc1:DictionaryLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="开始时间" HeaderStyle-Width="80">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblBeginDate" runat="server" Text='<%#Eval("BeginTime").ToDate()%>'></asp:Label>
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
                                <asp:Label ID="lblCreateTime" runat="server" Text='<%#Eval("CreateTime").ToDate() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="创建人" HeaderStyle-Width="80">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <cc1:DictionaryLabel ID="lblCreateUser" runat="server" DictionaryType="" FieldIDValue='<%# Eval("CreateUserID") %>'></cc1:DictionaryLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="操作" HeaderStyle-Width="100">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:LinkButton ID="btnModify" runat="server" Text="编辑" /><cc1:CustomLinkButton runat="server"
                                    ID="lbtn_Del"   CommandArgument='<%# Eval("JobID") +","+Eval("TrainingItemID")%>' CommandName="Del" Text="删除"
                                    EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="确定要删除吗？" /><%--<asp:LinkButton
                                        ID="ltnEvaluation" runat="server" Text="批阅" PostBackUrl='<%# this.ActionHref(string.Format("PracticeMarking.aspx?JobID={0}&TrainingItemID={1}&ItemCourseOffLineJobID={2}",Eval("JobID").ToGuid(),Eval("TrainingItemID").ToGuid(),Eval("ItemCourseOffLineJobID").ToGuid()))%>' />--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </cc1:CustomGridView><div style="text-align:center"><asp:Literal ID="ltlNull" runat="server"></asp:Literal></div>
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
