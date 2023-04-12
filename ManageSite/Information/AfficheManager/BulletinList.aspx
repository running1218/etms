<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="BulletinList.aspx.cs" Inherits="Information_AfficheManager_BulletinList" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" Runat="Server">
<!--导航路径-->
        <div class="dv_path">
            当前位置：信息公告系统>>公告管理>><asp:Literal ID="Literal12" runat="server"></asp:Literal>
        </div>
        <!--功能标题-->
        <h2 class="dv_title">
            公告管理
        </h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
<div class="dv_searchbox">
            <table class="GridviewGray" id="tableQueryControlList" runat="server">
                <tr>
                    <th width="120">
                        公告标题：
                    </th>
                    <td width="125"><asp:TextBox ID="txt_MainHead" runat="server" CssClass="inputbox_120"></asp:TextBox></td>
                     <th style=" width:15%;">
                        状　　态：
                    </th>
                    <td style="width:120px;">
                        <cc1:DictionaryDropDownList runat="server" ID="Dic_IsUse" DictionaryType="Dic_Status" />
                    </td>                    
                    <td style="width:60px">
                         <asp:UpdatePanel ID="upQuery" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                 <asp:Button ID="btnSearch" runat="server" CssClass="btn_Search" Text="查询" OnClick="btnSearch_Click" />
                           </ContentTemplate>
                         </asp:UpdatePanel>
                    </td>
                    <td >
                       <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
                    </td>
                </tr>
                 <tr>                    
                   <th>
                        创建日期：</th>
                        <td colspan="3">
                        <cc1:DateTimeTextBox ID="begin_CreateTime" runat="server" CssClass="inputbox_120" EndTimeControlID="end_CreateTime"></cc1:DateTimeTextBox>
                        至
                        <cc1:DateTimeTextBox ID="end_CreateTime" runat="server" CssClass="inputbox_120" BeginTimeControlID="begin_CreateTime"></cc1:DateTimeTextBox>
                    </td>
                </tr>
            </table>
        </div>
      <div class="dv_searchlist">
       <asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional">
          <ContentTemplate>
            <!--翻页-->
            <div class="dv_pagePanel">
                <div class="dv_selectAll">   
                    <input type="button" class="btn_Add" value="新增" onclick="javascript:showWindow('新增公告','<%=this.ActionHref(string.Format("BulletinInfo.aspx?op={0}&id={1}","add",0)) %>')" />
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <cc1:CustomGridView ID="GridViewList" runat="server" AutoGenerateColumns="False" DataKeyNames="ArticleID" OnRowCommand="GridViewList_RowCommand"
                OnRowDataBound="GridViewList_RowDataBound">
                <columns>
                    <asp:TemplateField HeaderText="序号" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft" HeaderStyle-Width="40">
                        <ItemStyle HorizontalAlign="Center" Width="60" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="LabNo" runat="server" Text='<%#((this.PageSet1.PageIndex -1) * this.PageSet1.PageSize) + Container.DataItemIndex+1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                    
                    <asp:TemplateField HeaderText="公告标题" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemStyle HorizontalAlign="Center" CssClass="alignleft"/>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblMainHead" runat="server" ShowTextNum="100" Text='<%# Eval("MainHead")%>' ></cc1:ShortTextLabel>
                        </ItemTemplate>
                     </asp:TemplateField>        

                        <asp:TemplateField HeaderText="开始时间" HeaderStyle-Width="80" >
                           <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblBeginDate" runat="server" Text='<%#Eval("BeginDate").ToDate() %>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                   
                         <asp:TemplateField HeaderText="结束时间" HeaderStyle-Width="80" >
                           <ItemStyle HorizontalAlign="Center"  />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblEndDate" runat="server" Text='<%#Eval("EndDate").ToDate() %>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>  
                        
                         <asp:TemplateField HeaderText="状态" HeaderStyle-Width="40" >
                           <ItemStyle HorizontalAlign="Center"  />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lbnIsUse" runat="server" Text='<%#Eval("IsUse").ToInt()==1?"启用":"停用" %>'/>
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
			           
                     <asp:TemplateField HeaderText="操作" HeaderStyle-Width="100">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <a href="javascript:showWindow('编辑公告','<%#this.ActionHref( String.Format("BulletinInfo.aspx?op={0}&id={1}","edit", Eval("ArticleID").ToString()) )%>')" >
                            编辑</a>  
                            <cc1:CustomLinkButton runat="server" ID="lbtn_Del" CommandArgument='<%# Eval("ArticleID") %>'
                             CommandName="Del" Text="删除" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="确定要删除吗？" />
                          
                            <a href="<%# this.ActionHref( String.Format("AfficheView.aspx?ArticleID={0}", Eval("ArticleID").ToInt() ))%>" target="_blank" >
                            预览</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </columns>
            </cc1:CustomGridView> 
            <!--列表 end-->
            <div class="dv_splitLine">
            </div>
            <!--翻页-->
            <div class="dv_pagePanel">
            </div>
            </ContentTemplate>
            </asp:UpdatePanel>
        </div>        
</asp:Content>

