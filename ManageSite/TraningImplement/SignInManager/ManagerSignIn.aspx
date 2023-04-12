<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="ManagerSignIn.aspx.cs" Inherits="TraningImplement_SignInManager_ManagerSignIn" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" Runat="Server">
 <!--导航路径-->
        <div class="dv_path" id="dv_path">
            当前位置：培训实施管理>>培训管理>>管理签到
        </div>
        <!--功能标题-->
        <h2 class="dv_title">
            管理签到
        </h2>
         <a id="aBack" runat="server" class="btn_Return">返回</a>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
       
        <!--表单录入-->
        <div class="dv_pageInformation">
            <table class="GridviewGray">
                <tr>
                    <th  width="100">
                        项目编码：
                    </th>
                    <td width="200">
                        <asp:Label ID="lblItemCode" runat="server"></asp:Label>
                    </td>
                    <th width="100">
                        项目名称：
                    </th>
                    <td>
                        <asp:Label ID="lblItemName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        课程编码：
                    </th>
                    <td>
                        <asp:Label ID="lblCourseCode" runat="server"></asp:Label>
                    </td>
                    <th>
                        课程名称：
                    </th>
                    <td>
                        <asp:Label ID="lblCourseName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        培训日期：
                    </th>
                    <td>
                        <asp:Label ID="lblTrainingDate" runat="server" />
                    </td>
                    <th>
                        讲&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;师：
                    </th>
                    <td>
                        <asp:Label ID="lblTeacherName" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th>
                        培训时段：
                    </th>
                    <td colspan="3">
                        <asp:Label ID="lblTrainingTime" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray th80" border="0" cellpadding="0" cellspacing="0" id="tableQueryControlList"
                runat="server">
                <tr>
                    <th >
                        姓&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;名：
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txt_u999RealName" runat="server" CssClass="inputbox_210"></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn_Search" Text="查询" OnClick="btnSearch_Click" />
                        <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
                    </td>
              
                </tr>
                <tr>
                    <th>
                        签到信息：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="ddl_a999SigninTypeID" DictionaryType="Dic_Sys_SigninType"
                            IsShowAll="true" />
                    </td>
                    <th>
                        工&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;号：
                    </th>
                    <td>
                        <asp:TextBox ID="txt_s999WorkerNo" runat="server" CssClass="inputbox_120"></asp:TextBox>
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
                            <cc1:CheckBoxsController runat="server" ID="CheckBoxsController1" ContainerID="CustomGridView1" />
                            <asp:Button ID="btnImport" runat="server" Text="导入签到" CssClass="btn_4" OnClick="btnImport_Click" Visible="false" />
                            <asp:Button ID="btnMoreSign" runat="server" Text="批量签到" CssClass="btn_batch" OnClick="btnMoreSign_Click" />
                            <cc1:CustomLinkButton runat="server" ID="btnSignOn" Text="全部签到" CssClass="btn_Allsignedin" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="您确认将需参加此授课安排的学员全部设为正常签到吗？" OnClick="btnSignOn_Click" />  
                            <cc1:CustomLinkButton runat="server" ID="btnSignOff" Text="清除签到" CssClass="btn_Clearsignin" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="您确认将此授课安排的学员签到信息全部清除，置为初始状态？" OnClick="btnSignOff_Click" />  
                        </div>
                        <div class="dv_pageControl">
                            <uc2:PageSet ID="PageSet1" runat="server" />
                        </div>
                    </div>
                    <!--列表 begin-->
                    <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ItemCourseHoursStudentID" OnRowDataBound="CustomGridView1_RowDataBound">
                        <Columns>                             
                            <asp:TemplateField HeaderText="">
                                <ItemStyle HorizontalAlign="Center"  />
                                <HeaderStyle HorizontalAlign="Center" Width="20"/>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="工号" HeaderStyle-Width="60" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                                <ItemTemplate>
                                    <cc1:ShortTextLabel Text='<%#Eval("WorkerNo") %>' runat="server" ShowTextNum="10" ID="lblWorkerNo" />
                                </ItemTemplate>
                            </asp:TemplateField>                            
                            <asp:BoundField DataField="RealName" HeaderStyle-Width="80" HeaderText="姓名" SortExpression="RealName" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft"/>
                            <asp:TemplateField HeaderText="组织机构" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                                <ItemTemplate>
                                    <cc1:DictionaryLabel runat="server" ID="lblOrg" DictionaryType="vw_Dic_Sys_Organization" TextLength="10"
                                        FieldIDValue='<%#Eval("OrganizationID").ToString() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                                <ItemTemplate>
                                    <cc1:DictionaryLabel runat="server" ID="lblDepartment" DictionaryType="vw_Dic_Sys_Department" TextLength="10"
                                        FieldIDValue='<%#Eval("DepartmentID").ToString() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_rank%>" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                                <ItemTemplate>
                                    <cc1:DictionaryLabel runat="server" ID="lblRank" DictionaryType="vw_Dic_Sys_Rank" TextLength="10"
                                        FieldIDValue='<%#Eval("RankID").ToString() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_position%>" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                                <ItemTemplate>
                                    <cc1:DictionaryLabel runat="server" ID="lblPost" DictionaryType="vw_Dic_Sys_Post" TextLength="10"
                                        FieldIDValue='<%#Eval("PostID").ToString() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="签到信息" HeaderStyle-Width="60">
                                <ItemTemplate>
                                    <cc1:DictionaryLabel runat="server" ID="lblSigninType" DictionaryType="Dic_Sys_SigninType" TextLength="10"
                                        FieldIDValue='<%#Eval("SigninTypeID").ToString() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="签到时间" HeaderStyle-Width="90">
                                <ItemTemplate>
                                    <%#string.IsNullOrEmpty(Eval("SigninTime").ToDate()) ? "--" : Eval("SigninTime").ToDate()%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="操作" HeaderStyle-Width="60">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblModify" runat="server" Text="修改" />                                 
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
</asp:Content>
