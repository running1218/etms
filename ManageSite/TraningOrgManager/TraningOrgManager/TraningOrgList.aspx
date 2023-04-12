<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="TraningOrgList.aspx.cs" Inherits="TraningOrgManager_TraningOrgManager_TraningOrgList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--导航路径-->
        <div class="dv_path" id="dv_path">
            当前位置：资源管理系统&gt;&gt;外部机构管理&gt;&gt;外部培训机构管理
        </div>
        <!--功能标题-->
        <h2 class="dv_title">
            外部培训机构管理
        </h2>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <th width="120">
                        机构编码：
                    </th>
                    <td width="120">
                        <asp:TextBox ID="txtOuterOrgCode" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                    <th width="120">
                        机构名称：
                    </th>
                    <td width="120"><asp:TextBox ID="txtOuterOrgName" runat="server" CssClass="inputbox_120"></asp:TextBox> 
                    <asp:Button ID="btnSearch" runat="server" CssClass="btn_Search" Text="查询" OnClick="btnSearch_Click" />
                    <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
                    </td>
                    
                </tr>
                <tr>
                    <th>
                        服务内容：
                    </th>
                    <td>
                        <asp:TextBox ID="txtServiceContent" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                    <th>
                        状&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;态：
                    </th>
                    <td colspan="2">
                        <cc1:DictionaryDropDownList runat="server" ID="Dic_Status" DictionaryType="Dic_Status"
                            IsShowAll="true" />
                    </td>
                </tr>
            </table>
        </div>
     
        <div class="dv_searchlist">
           <!--查找结果-->
        <asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional">
          <ContentTemplate>
            <!--翻页-->
            <div class="dv_pagePanel" id="divPage1">
                <div class="dv_selectAll">
                    <input type="button" class="btn_Add" value="新增" onclick="javascript:showWindow('新增外部培训机构','<%= this.ActionHref(string.Format("TraningOrgInfo.aspx?op={0}&id={1}","add",0)) %>')" />
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="GridViewList" runat="server" AutoGenerateColumns="False" DataKeyNames="OuterOrgID" OnRowCommand="GridViewList_RowCommand" OnRowDataBound="GridViewList_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="OuterOrgCode"  HeaderText="机构编码" SortExpression="OuterOrgCode" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft field10"></asp:BoundField>			        
                    <asp:TemplateField  HeaderText="机构名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblOuterOrgName" runat="server" ShowTextNum="10" Text='<%# Eval("OuterOrgName")%>' ></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="机构状态" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblOuterOrgStatus" runat="server" DictionaryType="Dic_Status" FieldIDValue='<%#Eval("OuterOrgStatus") %>'></cc1:DictionaryLabel>
                        </ItemTemplate>
                    </asp:TemplateField>			        
                    <asp:BoundField DataField="LinkMan" HeaderText="联系人" HeaderStyle-CssClass="field8 alignleft" ItemStyle-CssClass="alignleft"  ></asp:BoundField>			        
                    <asp:BoundField DataField="LinkMode" HeaderText="联系方式" HeaderStyle-CssClass="field10 alignleft" ItemStyle-CssClass="alignleft"></asp:BoundField>		
                    <asp:TemplateField HeaderText="邮箱" HeaderStyle-Width="90" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                       <ItemTemplate>
                          <cc1:ShortTextLabel ID="lblEMAIL" runat="server" Text='<%#Eval("EMAIL") %>' ShowTextNum="10" />
                       </ItemTemplate>
                    </asp:TemplateField>	        
                    <%--<asp:BoundField DataField="EMAIL" HeaderText="邮箱" HeaderStyle-Width="90"></asp:BoundField>  --%>                  
                    <asp:TemplateField HeaderText="讲师数" HeaderStyle-Width="40">
                        <ItemTemplate>
                            <a href='<%#this.ActionHref(string.Format("TrainingOrgTeacherList.aspx?OuterOrgID={0}",Eval("OuterOrgID"))) %>'><%#Eval("TeacherNum")%></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作" HeaderStyle-Width="160">
                        <ItemTemplate>
                            <a href="javascript:showWindow('编辑培训机构','<%#this.ActionHref(string.Format("TraningOrgInfo.aspx?op={0}&id={1}","edit",Eval("OuterOrgID").ToString())) %>')">编辑</a>
                            <cc1:CustomLinkButton runat="server" ID="lbtn_Del" CommandArgument='<%# Eval("OuterOrgID") %>'
                             CommandName="Del" Text="删除" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="确定要删除吗？" />
                            <a href="javascript:showWindow('查看培训机构','<%#this.ActionHref(string.Format("TraningOrgView.aspx?id={0}",Eval("OuterOrgID").ToString())) %>')">查看</a>
                            <%--<a href='<%#this.ActionHref(string.Format("CousreManage.aspx?OuterOrgID={0}",Eval("OuterOrgID"))) %>'>课程管理</a>--%>
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
            <script language="javascript" type="text/javascript">
                divPage2.innerHTML = divPage1.innerHTML;
            </script>
             </ContentTemplate>
        </asp:UpdatePanel>
        </div>
       
    </div>
</asp:Content>

