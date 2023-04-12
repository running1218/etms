<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="ArticleList.aspx.cs" Inherits="QuestionDB_GuidanceManager_ArticleList" %>

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
         <a id="aBack" runat="server" class="btn_Return">返回</a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div>        
      
        <!--查找结果-->
        <div class="dv_searchlist">
             <asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional">
          <ContentTemplate>
            <!--翻页-->
            <div class="dv_pagePanel" id="divPage1">
                <div class="dv_selectAll">
                    <asp:Button runat="server" ID="btnAdd" CssClass="btn_Add" Text="新增" />                  
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
             <cc1:CustomGridView ID="GridViewList" runat="server" AutoGenerateColumns="False" DataKeyNames="ArticleID" OnRowCommand="GridViewList_RowCommand"
                OnRowDataBound="GridViewList_RowDataBound">
                <columns>                   
                    <asp:TemplateField HeaderText="序号" HeaderStyle-Width="40">
                        <ItemStyle HorizontalAlign="Center" Width="60" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="LabNo" runat="server" Text='<%#((this.PageSet1.PageIndex -1) * this.PageSet1.PageSize) + Container.DataItemIndex+1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                     <asp:TemplateField HeaderText="标题" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">                        
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblMainHead" runat="server" ShowTextNum="50" Text='<%# Eval("MainHead")%>' ></cc1:ShortTextLabel>
                        </ItemTemplate>
                     </asp:TemplateField>                      

                        <asp:TemplateField HeaderText="资料简介" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">                          
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblBrief" runat="server" Text='<%#Eval("Brief") %>' ShowTextNum="100" />
                            </ItemTemplate>
                        </asp:TemplateField>                   
                        
                        
                         <asp:TemplateField HeaderText="状态" HeaderStyle-Width="60">
                           <ItemStyle HorizontalAlign="Center"  />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lbnIsUse" runat="server" Text='<%#Eval("IsUse").ToInt()==1?"启用":"停用" %>' />                              
                            </ItemTemplate>
                        </asp:TemplateField>                       
			        
                        <asp:BoundField DataField="CreateMan" HeaderText="创建人" SortExpression="CreateMan" HeaderStyle-Width="80">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>	                       
                        
                        <asp:TemplateField HeaderText="创建时间" HeaderStyle-Width="80" >
                           <ItemStyle HorizontalAlign="Center"  />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblCreateTime" runat="server" Text='<%#Eval("CreateTime").ToDate() %>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>                      
			           
                     <asp:TemplateField HeaderText="操作" HeaderStyle-Width="80">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:LinkButton ID="btnModify" runat="server" Text="编辑" />                         
                            <cc1:CustomLinkButton runat="server" ID="lbtn_Del" CommandArgument='<%# Eval("ArticleID") %>'
                             CommandName="Del" Text="删除" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="确定要删除吗？" />                           
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                </columns>
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

