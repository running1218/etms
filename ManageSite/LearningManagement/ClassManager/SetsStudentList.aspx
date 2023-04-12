<%@ Page Title="设置学员" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="SetsStudentList.aspx.cs" Inherits="LearningManagement_ClassManager_SetsStudentList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" Runat="Server">
   <!--导航路径-->
        <div class="dv_path" id="dv_path">
            当前位置：学习管理>>设置学员
        </div>
        <!--功能标题-->
        <h2 class="dv_title">
            设置学员
        </h2>
        <a id="aBack" runat="server" class="btn_Return">返回</a>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">  
    <div>       
        <!--查找条件-->
       
        <table class="dv_GradeviewList" border="0" cellpadding="0" cellspacing="0" >
                <tr>
                    <th width="15%">
                        项目名称：
                    </th>
                    <td width="30%">
                        <asp:Literal ID="txtItemName" runat="server" />
                    </td>
                    <th width="15%">
                        班级名称：
                    </th>
                    <td width="40%">
                        <asp:Literal ID="txtClassName" runat="server" />
                    </td>
                </tr>
         </table>
         
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" id="tableQueryControlList"
                runat="server">
                
                <tr>
                    <th width="100">
                        姓&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;名：
                    </th>
                    <td >
                        <asp:TextBox ID="txtRealName" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                    <th width="100">
                        工&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;号：
                    </th>
                    <td >
                        <asp:TextBox ID="txtWorkerNo" runat="server" CssClass="inputbox_120"></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn_Search" Text="查询" OnClick="btnSearch_Click" />  
                        <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
                    </td>
                 
                </tr>
                <tr>
                    <th>
                        班级职务：
                    </th>
                    <td colspan="5">
                        <cc1:DictionaryDropDownList runat="server" ID="ddlStudentType" DictionaryType="Dic_Sys_StudentType"
                            IsShowAll="true" />
                    </td>                   
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
         <%--   <asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional">
                <ContentTemplate>--%>
                    <!--翻页-->
                    <div class="dv_pagePanel">
                        <div class="dv_selectAll">
                            <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" />
                            <asp:Button ID="btnAdd" runat="server" CssClass="btn_Add" Text="新增" />
                            <cc1:CustomButton runat="server" ID="cbtnDel" Text="删除" CssClass="btn_Del" EnableConfirm="true"
                                ConfirmTitle="提示" ConfirmMessage="确定删除吗？" OnClick="cbtnDel_Click" />                          
                           
                            <asp:Button ID="btnBanboo" CssClass="btn_Agree" runat="server" Text="设置版主" OnClick="btnBanboo_Click" Visible="false" />
                           
                        </div>
                        <div class="dv_pageControl">
                            <uc2:PageSet ID="PageSet1" runat="server" />
                        </div>
                    </div>
                    <!--列表 begin-->
                    <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="False"
                        DataKeyNames="ClassStudentID" OnRowDataBound="CustomGridView1_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="">
                                <ItemStyle HorizontalAlign="Center" Width="40" />
                                <HeaderStyle HorizontalAlign="Center" Width="20" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                    <input type="hidden" value='<%#Eval("ClassStudentID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="学员编码" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblClassStudentID" runat="server" Text='<%#Eval("ClassStudentID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="WorkerNo" HeaderText="工号" SortExpression="WorkerNo"  HeaderStyle-Width="60">
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="RealName" HeaderText="姓名" SortExpression="RealName" HeaderStyle-Width="60" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="组织机构" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                                <ItemTemplate>
                                    <cc1:DictionaryLabel runat="server" ID="lblOrg" DictionaryType="vw_Dic_Sys_Organization" FieldIDValue='<%#Eval("OrganizationID").ToString() %>' TextLength="10" />  
                                 </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                                <ItemTemplate>
                                     <cc1:DictionaryLabel runat="server" ID="lblDepartment" DictionaryType="vw_Dic_Sys_Department" FieldIDValue='<%#Eval("DepartmentID").ToString() %>' TextLength="10" />   
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_position%>" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                                <ItemTemplate>
                                    <cc1:DictionaryLabel runat="server" ID="lblPost" DictionaryType="vw_Dic_Sys_Post" FieldIDValue='<%#Eval("PostID").ToString() %>' TextLength="10" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_rank%>" HeaderStyle-Width="100">
                                <ItemTemplate>                                
                                    <cc1:DictionaryLabel runat="server" ID="lblRank" DictionaryType="vw_Dic_Sys_Rank" FieldIDValue='<%#Eval("RankID").ToString() %>' TextLength="10" />
                                </ItemTemplate>
                            </asp:TemplateField>                            
                            <asp:TemplateField HeaderText="班级职务" HeaderStyle-Width="100">
                                <ItemTemplate>
                                    <cc1:ShortTextLabel ID="lblPostion" runat="server" ShowTextNum="10"></cc1:ShortTextLabel>                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="是否版主" HeaderStyle-Width="60">
                                <ItemTemplate>                                    
                                    <%# Eval("IsBamboo").ToBoolean()?"是":"否" %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="操作" HeaderStyle-Width="100">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnPostion" runat="server" Text="设置班委/版主" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </cc1:CustomGridView>
                    <!--列表 end-->
                    <div class="dv_splitLine">
                    </div>
                    <!--翻页-->
                    <div class="dv_pagePanel">
                    </div><%--
                </ContentTemplate>
            </asp:UpdatePanel>--%>
        </div>
    </div>
</asp:Content>
