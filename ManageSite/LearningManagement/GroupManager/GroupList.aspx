<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="GroupList.aspx.cs" Inherits="LearningManagement_GroupManager_GroupList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" Runat="Server">
   <!--导航路径-->
        <div class="dv_path" id="dv_path">
            当前位置：学习管理>>学习群组管理
        </div>
        <!--功能标题-->
        <h2 class="dv_title">
            学习群组管理
        </h2>
        <a id="aBack" runat="server" class="btn_Return">返回</a>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        
<!--查找条件-->
        <div class="dv_GradeviewList ">
            <table class="fixedTable">
                <tr>
                    <th >
                        项目名称：
                    </th>
                    <td >
                       <cc1:ShortTextLabel ID="lblItemName" runat="server" ShowTextNum="10" />
                      
                    </td>
                    <th >
                       项目周期：
                    </th>
                    <td style="width:150px;">
                       <asp:Label ID="lblTime" runat="server" />
                    </td>
                    <th s>
                       班级名称：
                    </th>
                    <td >
                       <cc1:ShortTextLabel ID="lblGrade" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th>
                        班级人数：
                    </th>
                    <td>
                       <asp:Label ID="lblSingnNum" runat="server" />
                    </td>
                    <th>
                        已分组学员数：
                    </th>
                    <td >
                        <asp:Label ID="lblAssignNum" runat="server" />
                    </td>
                    <th>
                        未分组学员数：
                    </th>
                    <td>
                       <asp:Label ID="lblUnAssignNum" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel" id="divPage1">
                <div class="dv_selectAll">
                    <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" Visible="false" />
                    <input type="button" class="btn_Add" value="新增" onclick="javascript:showWindow('新增群组','<%=this.ActionHref(string.Format("GroupAdd.aspx?ClassID={0}",ClassID)) %>',570,350)" />
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
             <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ClassSubgroupID,CreateUserID" OnRowDataBound="CustomGridView1_RowDataBound" OnRowCommand="CustomGridView1_RowCommand" >
                <Columns>                   
                     <asp:TemplateField HeaderText="群组名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                         <ItemTemplate>
                             <cc1:ShortTextLabel ID="lblClassSubgroupName" runat="server" Text='<%#Eval("ClassSubgroupName") %>' ShowTextNum="10" />
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="创建时间" HeaderStyle-Width="80">
                        <ItemTemplate>
                           <asp:Label ID="lblCreateTime" runat="server" Text='<%#Eval("CreateTime").ToDate() %>' />
                        </ItemTemplate>
                     </asp:TemplateField>
                     <asp:BoundField DataField="CreateUser" HeaderText="创建人" HeaderStyle-Width="80" />
                     <asp:BoundField DataField="GroupStudentNum" HeaderText="学员数" ItemStyle-CssClass="alignright" HeaderStyle-CssClass="alignright" HeaderStyle-Width="80" />                     
                    <asp:TemplateField HeaderText="操作" HeaderStyle-Width="150">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <a href="javascript:showWindow('编辑群组','<%#this.ActionHref(string.Format("GroupEdit.aspx?ClassSubgroupID={0}",Eval("ClassSubgroupID").ToGuid())) %>',570,350)">编辑</a>                           
                             <cc1:CustomLinkButton runat="server" ID="lbtn_Del" CommandArgument='<%# Eval("ClassSubgroupID") %>'
                                CommandName="Del" Text="删除" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="确定要删除吗？" />
                            <a href="<%#this.ActionHref(string.Format("SetsStudentList.aspx?ClassSubgroupID={0}&ClassID={1}&TrainingItemID={2}&ClassSubgroupName={3}",Eval("ClassSubgroupID").ToGuid(),ClassID,TrainingItemID,Eval("ClassSubgroupName")!=null?Eval("ClassSubgroupName").ToString():"")) %>">设置组员</a></ItemTemplate>
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

