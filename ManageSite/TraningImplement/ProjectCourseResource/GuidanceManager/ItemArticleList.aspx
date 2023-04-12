<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="ItemArticleList.aspx.cs" Inherits="QuestionDB_GuidanceManager_ItemArticleList" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" Runat="Server">
 <!--导航路径-->
        <div class="dv_path">
            当前位置：资源管理系统>>学习资源管理>>导学资料管理
        </div>
        <!--功能标题-->
        <h2 class="dv_title">
            导学资料管理
        </h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="dv_searchlist">
        <!--翻页-->
        <div class="dv_pagePanel">           
            <div class="dv_pageControl">
                <uc2:PageSet ID="PageSet1" runat="server" />
            </div>
        </div>
   <cc1:CustomGridView ID="GridViewList" runat="server" AutoGenerateColumns="False" DataKeyNames="TrainingItemID,CourseID">
            <Columns>
                <asp:TemplateField HeaderText="序号" HeaderStyle-Width="40">
                    <ItemStyle HorizontalAlign="Center" Width="60" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="LabNo" runat="server" Text='<%#((this.PageSet1.PageIndex -1) * this.PageSet1.PageSize) + Container.DataItemIndex+1 %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
              
                <asp:TemplateField HeaderText="项目名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <cc1:ShortTextLabel ID="lblItemName" runat="server" ShowTextNum="10" Text='<%# Eval("ItemName")%>' ></cc1:ShortTextLabel>
                    </ItemTemplate>
                </asp:TemplateField>            
                <asp:TemplateField HeaderText="课程名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="10" Text='<%# Eval("CourseName")%>' ></cc1:ShortTextLabel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="课程培训开始" HeaderStyle-Width="100">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblBeginDate" runat="server" Text='<%#Eval("CourseBeginTime")==DBNull.Value?"":Eval("CourseBeginTime").ToDate()%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>  
                 <asp:TemplateField HeaderText="课程培训结束"  HeaderStyle-Width="100">
                    <ItemStyle HorizontalAlign="Center"/>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblEndDate" runat="server" Text='<%#Eval("CourseEndTime")==DBNull.Value?"":Eval("CourseEndTime").ToDate()%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>                 
                <asp:TemplateField HeaderText="导学资料数" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright" HeaderStyle-Width="80">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="hlArticleNumber" runat="server" Text='<%#  bulletinLogic.GetMontorDataNumbyItemCourse(Eval("TrainingItemCourseID").ToGuid()) %>' />
                    </ItemTemplate>
                </asp:TemplateField>                
                  <asp:TemplateField HeaderText="操作" HeaderStyle-Width="100">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:HyperLink ID="hlOfflinkNumber" runat="server" Text="导学资料管理" NavigateUrl='<%#this.ActionHref(String.Format("ArticleList.aspx?TrainingItemCourseID={0}&CourseID={1}&TrainingItemID={2}",Eval("TrainingItemCourseID").ToGuid(),Eval("CourseID").ToGuid(),Eval("TrainingItemID").ToGuid())) %>' />
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
</asp:Content>

