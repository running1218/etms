<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MPageAdmin.Master" CodeFile="BannerList.aspx.cs" Inherits="SiteManage_BannerManager_BannerList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
                id="tableQueryControlList">
                <tr>
                    <th width="120">推广名称：
                    </th>
                    <td width="130">
                        <asp:TextBox ID="txt_SpreadName" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                    <th>状态：
                    </th>
                    <td colspan="3">
                        <cc1:DictionaryDropDownList runat="server" ID="ddl_ReleaseStatus" DictionaryType="Dic_Status"
                            CssText="select_120" />
                         <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <!--翻页-->
                    <div class="dv_pagePanel">
                        <div class="dv_selectAll">
                             <asp:Button runat="server" ID="btnAdd" CssClass="btn_Add" Text="新增" />
                             <input id="btnSort" type="button" class="btn_Sort" value="排序" title="推荐课程排序" onclick="javascript: showWindow('Banner排序', 'BannerSort.aspx', 500, 450)" />
                             <input  type="button" class="btn_Search" onclick="preview()" value="预览"/>
                            
                        </div>
                        <div class="dv_pageControl">
                            <uc2:PageSet ID="PageSet1" runat="server" />
                        </div>
                    </div>
                    <!--列表 begin-->
                    <cc1:CustomGridView ID="gvList" runat="server" CssClass="GridviewGray" EmptyDataSourceTip="没有任何记录！"
                        AutoCreateColumnInsertIndex="0" AutoGenerateColumns="False" CurrentPageIndex="1"
                        CustomAllowPaging="False" IsEmpty="False" TotalRecordCount="0" DataKeyNames="BannerSpreadID"
                        OnRowCommand="CustomGridView1_RowCommand" OnRowDataBound="GridViewList_RowDataBound">
                        <Columns>             
                            <asp:TemplateField HeaderText="推广名称" HeaderStyle-Width="150">
                                <ItemTemplate>
                                    <cc1:ShortTextLabel ID="lblSpreadName" runat="server" ShowTextNum="100" Text='<%# Eval("SpreadName")%>'></cc1:ShortTextLabel>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SEO关键字" HeaderStyle-Width="180">
                                <ItemTemplate>
                                   <cc1:ShortTextLabel ID="lblKeyWords" runat="server" Text='<%# Eval("KeyWords")%>'></cc1:ShortTextLabel>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="PC轮换图" HeaderStyle-Width="80">
                                <ItemTemplate>                                 
                                     <asp:HyperLink ID="lblPCImageName" runat="server" Text='<%#Eval("PCImageName") %>'  />
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="移动轮换图" HeaderStyle-Width="80">
                                <ItemTemplate>                                 
                                    <asp:HyperLink ID="lblMobileImageName" runat="server" Text='<%#Eval("MobileImageName") %>'  />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="状态" HeaderStyle-Width="30">
                                <ItemTemplate>
                                    <asp:Label ID="lblReleaseStatus" runat="server" Text='<%# Eval("ReleaseStatus").ToString()=="0"?"停用":"启用" %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="操作" HeaderStyle-Width="30px">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnModify" runat="server" Text="编辑" />
                                    <cc1:CustomLinkButton runat="server" ID="lbtn_Del" CommandArgument='<%# Eval("BannerSpreadID") %>'
                                        CommandName="Del" Text="删除" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="确定要删除吗？" />
                                    <%-- <a href='<%# this.ActionHref(string.Format("CourseView.aspx?CourseID={0}",Eval("CourseID").ToString())) %>'>
                                            预览</a>--%>
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <script>
          
        function preview()
        {  
            var StudyingUrl='<%=StudyingUrl%>';
             //var StudyingUrl='http://10.96.142.126:9001/Admin';          
            if(StudyingUrl.indexOf("Admin")>0)
            {
                StudyingUrl=StudyingUrl.replace("Admin","Studying");
            }
            window.open(StudyingUrl+'/Index.aspx');       
        }

    </script>
</asp:Content>
 
