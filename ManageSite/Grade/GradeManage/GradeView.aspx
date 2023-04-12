<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="GradeView.aspx.cs" Inherits="Grade_GradeManage_GradeView" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<!--功能标题-->
<h2 class="dv_title">
   查看在线测试成绩
</h2>
<!--表单录入-->
<div class="dv_information">
    <table class="GridviewGray">
        <tr>
            <th >
                课程名称：
            </th>
            <td > 
               <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="10" />                
            </td>
        
            <th >
                学员姓名：
            </th>
            <td>
                <cc1:ShortTextLabel ID="lblStudentName" runat="server" ShowTextNum="10" />
            </td>
        </tr>
        <tr class="hide">
            <th>
                <asp:Literal ID="ltlDepartment" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>：
            </th>
            <td>
                <cc1:DictionaryLabel ID="lblDept" runat="server" TextLength="10" DictionaryType='vw_Dic_Sys_Department'></cc1:DictionaryLabel>            
            </td>
            <th>
                <asp:Literal ID="ltlPosition" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>：
            </th>
            <td >
               <cc1:DictionaryLabel ID="lblPost" runat="server"  TextLength="10" DictionaryType='vw_Dic_Sys_Post' FieldIDValue='<%#Eval("PostID") %>'></cc1:DictionaryLabel>
            </td>
        </tr>

        <tr>
            <th>课程成绩：</th>
            <td><asp:Label ID="lblScore" runat="server" ForeColor="Red"></asp:Label></td>
            <td colspan="2">
                <span>课程学习（<asp:Label ID="lblCourseRate" runat="server"></asp:Label>%）：</span><asp:Label ID="lblCourse" runat="server" ForeColor="Blue"></asp:Label>
                <br />
                <span>在线测试（<asp:Label ID="lblTestingRate" runat="server"></asp:Label>%）：</span><asp:Label ID="lblTesting" runat="server" ForeColor="Blue"></asp:Label>
                <br />
                <span>在线作业（<asp:Label ID="lblJobRate" runat="server"></asp:Label>%）：</span><asp:Label ID="lblJob" runat="server" ForeColor="Blue"></asp:Label>
            </td>
        </tr>
    </table>

 <!--查找结果-->
        <div class="dv_searchlist hide" style="width:96%">
            <!--翻页-->
            <div class="dv_pagePanel">
                <div class="dv_selectAll">
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" CssClass="GridviewGray" EmptyDataSourceTip="没有任何记录！"
                    AutoCreateColumnInsertIndex="0" AutoGenerateColumns="False" CurrentPageIndex="1" CustomAllowPaging="False" IsEmpty="False">
                <Columns> 
                   <asp:BoundField DataField="OnLineTestName" HeaderText="在线测试名称" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft" />
                   <asp:TemplateField HeaderText="考试时间" HeaderStyle-CssClass="field8" HeaderStyle-Width="150">
                      <ItemTemplate>
                          <%#Eval("ExamTime").ToDateTime().ToString("yyyy-MM-dd HH:mm:ss") %>
                      </ItemTemplate>
                   </asp:TemplateField>
                  
                   <asp:TemplateField HeaderText="成绩"  HeaderStyle-Width="60">
                      <ItemTemplate>
                          <%#Eval("Score")%>
                      </ItemTemplate>
                   </asp:TemplateField>
                </Columns>
            </cc1:CustomGridView>
            <!--列表 end-->
            <div class="dv_splitLine">
            </div>
        </div>

</div>
    <!--提交表单-->
    <div class="dv_submit">
        <input type="button" value="关闭" onclick="javascript:closeWindow();" class="btn_Close">
    </div>

</asp:Content>

