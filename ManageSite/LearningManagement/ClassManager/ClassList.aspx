<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="ClassList.aspx.cs" Inherits="LearningManagement_ClassManager_ClassList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" Runat="Server">
  <!--导航路径-->
        <div class="dv_path" id="dv_path">
            当前位置：学习管理>>班级管理
        </div>
        <!--功能标题-->
        <h2 class="dv_title">
            班级管理
        </h2>
        <a id="aBack" runat="server" class="btn_Return">返回</a>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
      
        <!--查找条件-->
        <div class="dv_GradeviewList">
            <table>
                <tr>
                    <th width="120">
                        项目名称：
                    </th>
                    <td>
                       <cc1:ShortTextLabel ID="lblItemName" runat="server" ShowTextNum="10" />
                      
                    </td>
                    <th width="120">
                       项目周期：
                    </th>
                    <td colspan="3" >
                       <asp:Label ID="lblTime" runat="server" />
                    </td>
                    
                </tr>
                <tr>
                    <th>
                        学员总数：
                    </th>
                    <td>
                       <asp:Label ID="lblSingnNum" runat="server" />
                    </td>
                    <th>
                        已分班学员数：
                    </th>
                    <td >
                        <asp:Label ID="lblAssignNum" runat="server" />
                    </td>
                    <th >
                        未分班学员数：
                    </th>
                    <td style="text-align:left;">
                       <asp:Label ID="lblUnAssignNum" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel">
                <div class="dv_selectAll">
                    <input type="button" class="btn_Add" value="新增" onclick="javascript:showWindow('新增','<%=this.ActionHref(string.Format("ClassAdd.aspx?TrainingItemID={0}",TrainingItemID))%>',580,400)" />
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound="CustomGridView1_RowDataBound" OnRowCommand="CustomGridView1_RowCommand" >
                <Columns>
                    <asp:TemplateField HeaderText="班级名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                       <ItemTemplate>
                           <cc1:ShortTextLabel ID="lblClassName" runat="server" ShowTextNum="50" Text='<%#Eval("ClassName") %>' />
                       </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="DutyUser" HeaderText="负责人" HeaderStyle-Width="80" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft" />
                   
                    <asp:BoundField DataField="TelPhone" HeaderText="负责人电话" HeaderStyle-Width="120"/>
                    <asp:BoundField DataField="Email" HeaderText="负责人邮箱" HeaderStyle-Width="150" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft"/>                                     
                    <asp:BoundField DataField="AssignCount" HeaderText="学员数" HeaderStyle-Width="80" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright"/>
                    <asp:TemplateField HeaderText="操作" HeaderStyle-Width="180">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <a href="javascript:showWindow('编辑','<%#this.ActionHref(string.Format("ClassEdit.aspx?ClassID={0}",Eval("ClassID").ToString())) %>',580,400)">编辑</a>                           
                            <a href='<%#this.ActionHref(string.Format("SetsStudentList.aspx?ClassID={0}&TrainingItemID={1}",Eval("ClassID").ToString(),TrainingItemID)) %>'>设置学员</a>
                             <cc1:CustomLinkButton runat="server" ID="lbtn_Del" CommandArgument='<%# Eval("ClassID") %>'
                                CommandName="Del" Text="删除" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="确定要删除吗？" />
                            <a href='<%#this.ActionHref(string.Format("{0}/LearningManagement/GroupManager/GroupList.aspx?ClassID={1}&TrainingItemID={2}",WebUtility.AppPath,Eval("ClassID").ToString(),TrainingItemID)) %>' >群组管理</a>
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
    </div>
</asp:Content>
