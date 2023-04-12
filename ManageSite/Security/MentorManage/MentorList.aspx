<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="MentorList.aspx.cs" Inherits="Security_TutorManage_TutorList" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" Runat="Server">
 <!--导航路径-->
        <div class="dv_path" id="dv_path">
            当前位置：基础管理系统>>基本管理>>导师管理
        </div>
        <!--功能标题-->
        <h2 class="dv_title">
            导师管理
        </h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>       
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" id="tableQueryControlList"
                runat="server">
                <tr>
                    <th width="120">
                        姓&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;名：
                    </th>
                    <td width="130">
                        <asp:TextBox ID="txtTeacherName" runat="server" />
                    </td>
                    <th width="120">
                        <asp:Literal ID="ltlDepartment" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>：
                    </th>
                    <td width="125">
                        <asp:TextBox ID="txtDepartment" runat="server" />   
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn_Search" Text="查询" OnClick="btnSearch_Click" />  
                        <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>                  
                    </td>
                </tr>
                <tr>
                    <th>
                        状&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;态：
                    </th>
                    <td>
                       <cc1:DictionaryDropDownList runat="server" ID="Dic_Status" DictionaryType="Dic_Status" />
                    </td>
                    <th>
                        <asp:Literal ID="ltlPosition" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>：
                    </th>
                    <td>
                        <asp:TextBox ID="txtPost" runat="server" />
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
                            <asp:Button ID="btnAdd" CssClass="btn_Add" runat="server" PostBackUrl="~/Security/MentorManage/MentorListAdd.aspx" Text="新增" />
                        </div>
                        <div class="dv_pageControl">
                            <uc2:PageSet ID="PageSet1" runat="server" />
                        </div>
                    </div>
                    <!--列表 begin-->
                      <cc1:CustomGridView ID="GridViewList" runat="server" AutoGenerateColumns="False" DataKeyNames="MentorID"  OnRowDataBound="GridViewList_RowDataBound" OnRowCommand="GridViewList_RowCommand" >
                        <Columns>                                                   
                            <asp:BoundField DataField="RealName" HeaderText="姓名" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft" HeaderStyle-Width="80"/>                            
                            <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                                <ItemTemplate>
                                   <cc1:DictionaryLabel runat="server" ID="lblDepartment" TextLength="10" DictionaryType="Site_DepartmentByOrgID" FieldIDValue='<%#Eval("DepartmentID").ToString() %>' />   
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_position%>" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                                <ItemTemplate>
                                      <cc1:DictionaryLabel runat="server" ID="ddlPost" TextLength="10" DictionaryType="Dic_PostByOrgID" FieldIDValue='<%#Eval("PostID").ToString() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_rank%>" HeaderStyle-Width="100" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                                <ItemTemplate>
                                    <cc1:DictionaryLabel runat="server" ID="ddlRank" DictionaryType="vw_Dic_Sys_Rank" TextLength="10" FieldIDValue='<%#Eval("RankID").ToString() %>'/>
                                </ItemTemplate>
                            </asp:TemplateField>                           
                            <asp:TemplateField HeaderText="状态" HeaderStyle-Width="60">
                                <ItemTemplate>
                                   <cc1:DictionaryLabel runat="server" ID="Dic_Status" DictionaryType="Dic_Status" FieldIDValue='<%#Eval("IsUse").ToString() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="操作" HeaderStyle-Width="100">
                                <ItemTemplate>
                                    <a id="lbnView" href="javascript:showWindow('查看导师','<%#this.ActionHref(string.Format("MentorView.aspx?MentorID={0}",Eval("MentorID"))) %>')">查看</a>
                                    <asp:LinkButton ID="lbnStart" runat="server" Text="启用" CommandName="start" CommandArgument='<%#Eval("MentorID") %>' />
                                    <asp:LinkButton ID="lbnStop" runat="server" Text="停用" CommandName="stop" CommandArgument='<%#Eval("MentorID") %>'/> 
                                   <cc1:CustomLinkButton runat="server" ID="lbtn_Del" CommandArgument='<%# Eval("MentorID") %>'
                                        CommandName="Del" Text="删除" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="确定要删除吗？" />                              
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

