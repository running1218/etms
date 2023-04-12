<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="SetsStudentList.aspx.cs" Inherits="LearningManagement_GroupManager_SetsStudentList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" Runat="Server">
   <!--导航路径-->
        <div class="dv_path" id="dv_path">
            当前位置：学习管理>>群组管理>>设置学员
        </div>
        <!--功能标题-->
        <h2 class="dv_title">
            设置学员
        </h2>
        <a id="aBack" runat="server" class="btn_Return">返回</a>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        
        <!--查找条件-->
        <div class="dv_pageInformation">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <th width="15%">
                        项目名称：
                    </th>
                    <td width="20%">
                        <cc1:ShortTextLabel ID="lblItemName" ShowTextNum="10" runat="server" />
                    </td>
                    <th width="15%">
                        班级名称：
                    </th>
                    <td width="20%">
                        <cc1:ShortTextLabel ID="lblClassName" ShowTextNum="10" runat="server" />
                    </td>
                    <th width="15%">
                        群组名称：
                    </th>
                    <td width="15%">
                        <cc1:ShortTextLabel ID="lblGroupName" ShowTextNum="10" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel" id="divPage1">
                <div class="dv_selectAll">
                    <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" />
                    <input type="button" class="btn_Add" value="新增" onclick="javascript:window.location='<%=this.ActionHref(string.Format("SetsStudentAdd.aspx?ClassSubgroupID={0}&ClassID={1}&TrainingItemID={2}&ClassSubgroupName={3}",ClassSubgroupID,ClassID,TrainingItemID,ClassSubgroupName)) %>'" />
                    <cc1:CustomButton runat="server" ID="btnLeader" CssClass="btn_Agree" Text="设置队长" OnClick="btnLeader_Click"
                        EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="确定要设置队长吗？" Visible="false" />
                    <cc1:CustomButton runat="server" ID="btnDel" CssClass="btn_Del" Text="删除" OnClick="btnDel_Click"
                        EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="确定要删除吗？" />
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
             <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="SubgroupStudentID"  OnRowDataBound="CustomGridView1_RowDataBound" >
                <Columns>
                    <asp:TemplateField HeaderText="">
                        <ItemStyle HorizontalAlign="Center" Width="40" />
                        <HeaderStyle HorizontalAlign="Center" Width="20" />
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                            <input type="hidden" value='<%#Eval("ClassStudentID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>                   
                    <asp:BoundField DataField="WorkerNo" HeaderText="工号" SortExpression="WorkerNo" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft" HeaderStyle-Width="40">
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RealName" HeaderText="姓名" SortExpression="RealName" HeaderStyle-Width="60">
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                     <asp:TemplateField HeaderText="组织机构" HeaderStyle-Width="60">
                                <ItemTemplate>
                                    <cc1:DictionaryLabel runat="server" ID="lblOrg" DictionaryType="vw_Dic_Sys_Organization" FieldIDValue='<%#Eval("OrganizationID").ToString() %>' TextLength="10" />  
                                 </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                                <ItemTemplate>
                                     <cc1:DictionaryLabel runat="server" ID="lblDepartment" DictionaryType="vw_Dic_Sys_Department" FieldIDValue='<%#Eval("DepartmentID").ToString() %>' TextLength="10" />   
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_position%>" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                                <ItemTemplate>
                                    <cc1:DictionaryLabel runat="server" ID="lblPost" DictionaryType="vw_Dic_Sys_Post" FieldIDValue='<%#Eval("PostID").ToString() %>'  TextLength="10"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_rank%>" HeaderStyle-Width="100">
                                <ItemTemplate>                                
                                    <cc1:DictionaryLabel runat="server" ID="lblRank" DictionaryType="vw_Dic_Sys_Rank" FieldIDValue='<%#Eval("RankID").ToString() %>' TextLength="10" />
                                </ItemTemplate>
                            </asp:TemplateField>                                                     
                    <asp:TemplateField HeaderText="是否队长" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <asp:Label ID="lblIsLeader" runat="server" Text='<%# Eval("IsLeader").ToBoolean()?"是":"否" %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="班级职务" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <asp:Label ID="lblPostion" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnLeader" runat="server" Text="设置队长"  />
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
</asp:Content>
