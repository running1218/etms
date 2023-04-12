<%@ Page Title="请假申请审批" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="AskApplyAuditList.aspx.cs" Inherits="TraningImplement_AskApplyAudit_AskApplyAuditList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--导航路径-->
        <div class="dv_path" id="dv_path">
            当前位置：培训实施管理>>培训管理>>请假申请审批
        </div>
        <!--功能标题-->
        <h2 class="dv_title">
            请假申请审批
        </h2>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray th70" border="0" cellpadding="0" cellspacing="0" id="tableQueryControlList"
                runat="server">
                <tr>
                    <th width="120">
                        项目名称：
                    </th>
                    <td width="220">
                        <asp:TextBox ID="txt_d999ItemName" runat="server" CssClass="inputbox_120"></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn_Search" Text="查询" OnClick="btnSearch_Click" />
                         <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
                    </td>
                
                </tr>
                <tr>
                    <th>
                        学员姓名：
                    </th>
                    <td>
                        <asp:TextBox ID="txt_u999RealName" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                    <th width="120">
                        审核状态：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="ddl_a999AuditStatus" DictionaryType="Dic_TraningPlanAuditState"
                            IsShowAll="true" IsShowChoose="false" />
                    </td>
                </tr>
                <tr>  
                   <th>
                        课程名称：
                    </th>
                    <td>
                        <asp:TextBox ID="txt_e999CourseName" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>                  
                    <th>
                        申请时间：
                    </th>
                    <td >
                        <cc1:DateTimeTextBox ID="begin_a999LeaveTime" runat="server" EndTimeControlID="end_a999LeaveTime"></cc1:DateTimeTextBox>
                        至
                        <cc1:DateTimeTextBox ID="end_a999LeaveTime" runat="server" BeginTimeControlID="begin_a999LeaveTime"></cc1:DateTimeTextBox>
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
                            <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="GridViewList"  />

                            <asp:Button runat="server" ID="btnAgree" CssClass="btn_Agree" Text="审核通过" OnClick="btnAgree_Click" />
                            <asp:Button runat="server" ID="btnDeny" CssClass="btn_Deny" Text="审核不通过" OnClick="btnDeny_Click"/>                       
                        </div>
                        <div class="dv_pageControl">
                            <uc2:PageSet ID="PageSet1" runat="server" />
                        </div>
                    </div>
                    <!--列表 begin-->
                      <cc1:CustomGridView ID="GridViewList" runat="server" AutoGenerateColumns="False" DataKeyNames="ItemCourseHoursStudentID"  OnRowDataBound="GridViewList_RowDataBound" >
                        <Columns>
                            <asp:TemplateField HeaderText="">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center"  Width="20"/>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="序号" Visible="false" HeaderStyle-Width="30">
                                <ItemStyle HorizontalAlign="Center"  />
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="LabNo" runat="server" Text='<%#  ((this.PageSet1.PageIndex -1) * this.PageSet1.PageSize) + Container.DataItemIndex+1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="项目名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                               <ItemTemplate >
                                  <cc1:ShortTextLabel runat="server" ID="lblItemName" ShowTextNum="10" Text='<%#Eval("ItemName") %>' />
                               </ItemTemplate>
                            </asp:TemplateField>  
                            <asp:TemplateField HeaderText="课程名称" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                               <ItemTemplate >
                                  <cc1:ShortTextLabel runat="server" ID="lblCourseName" ShowTextNum="10" Text='<%#Eval("CourseName") %>' />
                               </ItemTemplate>
                            </asp:TemplateField>                           
                            <asp:BoundField DataField="RealName" HeaderText="学员姓名" HeaderStyle-Width="80" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft"/>
                            <asp:TemplateField HeaderText="组织机构" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <cc1:DictionaryLabel runat="server" ID="lblOrg" DictionaryType="vw_Dic_Sys_Organization" FieldIDValue='<%#Eval("OrganizationID").ToString() %>' TextLength="10" />  
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                            <asp:TemplateField HeaderText="培训日期" HeaderStyle-Width="80">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTrainingDate" runat="server" Text='<%#Eval("TrainingDate").ToDate()%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="培训时段" HeaderStyle-Width="80">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTime" runat="server" Text='<%#Eval("TrainingBeginTime").ToDateTime().ToString("HH:mm")+"-"+Eval("TrainingEndTime").ToDateTime().ToString("HH:mm")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="申请时间" HeaderStyle-Width="80">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblLeaveTime" Text='<%#Eval("LeaveTime").ToDateTime().ToString("yyyy-MM-dd") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="审核状态" HeaderStyle-Width="60">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <cc1:DictionaryLabel DictionaryType="Dic_TraningPlanAuditState" ID="lblAuditResult" runat="server" FieldIDValue='<%#Eval("AuditStatus").ToString() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="操作" HeaderStyle-Width="60">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbnCheck" runat="server" Text="审核" />
                                    <asp:LinkButton ID="lbnView" runat="server" Text="查看" />                                   
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <!--审核意见的弹出层-->
    <div id="dv_lay1" style="display: none">
        <table style="width: 90%;">
            <tr>
                <th>
                    意见
                </th>
                <td>
                    <textarea class="inputbox_area210" id='txt1'></textarea>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
