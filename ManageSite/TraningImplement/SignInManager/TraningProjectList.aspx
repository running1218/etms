<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="TraningProjectList.aspx.cs" Inherits="TraningImplement_SignInManager_TraningProjectList" %>

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
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" >
    <div>        
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray th80" border="0" cellpadding="0" cellspacing="0" id="tableQueryControlList"
                runat="server">
                <tr>
                    <th width="120">
                        项目名称：
                    </th>
                    <td width="220">
                        <asp:TextBox ID="txt_f999ItemName" runat="server" CssClass="inputbox_210"></asp:TextBox>  
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn_Search" Text="查询" OnClick="btnSearch_Click" /> 
                        <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>                    
                    </td>
                   
                </tr>
                <tr>
                    <th >
                        项目编码：
                    </th>
                    <td >
                        <asp:TextBox ID="txt_f999ItemCode" runat="server" CssClass="inputbox_120"></asp:TextBox>
                        </td>
                    <th >
                        课程编码：
                    </th>
                    <td >
                        <asp:TextBox ID="txt_g999CourseCode" runat="server" CssClass="inputbox_120"></asp:TextBox>
                        </td>
                </tr>
                <tr>
                    <th>
                        课程名称：
                    </th>
                    <td>
                        <asp:TextBox ID="txt_g999CourseName" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                    <th>
                        讲　　师：
                    </th>
                    <td>
                        <asp:TextBox ID="txt_d999RealName" runat="server" CssClass="inputbox_120"></asp:TextBox>
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
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
             <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="False" >
                <Columns>  
                           <asp:TemplateField HeaderText="项目名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                                <ItemTemplate>
                                    <cc1:ShortTextLabel ShowTextNum="50" Text='<%#Eval("ItemName") %>' runat="server" ID="lblItemName" />
                                </ItemTemplate>
                            </asp:TemplateField>     
                             <asp:TemplateField HeaderText="课程名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                                <ItemTemplate>
                                    <cc1:ShortTextLabel ID="lblCourseName" ShowTextNum="10" Text='<%#Eval("CourseName") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>               
                            <asp:TemplateField HeaderText="培训日期" HeaderStyle-Width="80">
                                <ItemTemplate>
                                    <asp:Label ID="lblTrainingDate" runat="server" Text='<%#Eval("TrainingDate").ToDate() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="培训时段" HeaderStyle-Width="80">
                                <ItemTemplate>
                                   <asp:Label ID="lblTime" runat="server" Text='<%#Eval("TrainingBeginTime").ToDateTime().ToString("HH:mm")+"-"+Eval("TrainingEndTime").ToDateTime().ToString("HH:mm") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="TeacherName" HeaderText="讲师" HeaderStyle-Width="60" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft"/>
                             <asp:TemplateField HeaderText="培训地点" HeaderStyle-Width="80" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                                <ItemTemplate>
                                    <cc1:ShortTextLabel ID="lblClassRoomName" ShowTextNum="10" Text='<%#Eval("ClassRoomName") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>   
                            <asp:TemplateField HeaderText="学员数" HeaderStyle-Width="60">
                                <ItemTemplate>
                                   <asp:Label ID="lblStudentNum" runat="server" Text='<%#itemCourseHoursStudentLogic.GetItemCourseHoursStudentNumByItemCourseHoursID(Eval("ItemCourseHoursID").ToGuid()) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作" HeaderStyle-Width="60">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <a href="<%#this.ActionHref(string.Format("ManagerSignIn.aspx?ItemCourseHoursID={0}&TrainingItemID={1}&TrainingItemCourseID={2}&TeacherID={3}",Eval("ItemCourseHoursID"),Eval("TrainingItemID"),Eval("TrainingItemCourseID"),Eval("TeacherID"))) %>">管理签到</a>
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
