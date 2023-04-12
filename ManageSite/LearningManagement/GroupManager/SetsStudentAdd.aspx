<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="SetsStudentAdd.aspx.cs" Inherits="LearningManagement_GroupManager_SetsStudentAdd" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" Runat="Server">
        <!--导航路径-->
        <div class="dv_path" id="dv_path">
            当前位置：学习管理>>群组管理>>设置学员>>新增学员
        </div>
        <!--功能标题-->
        <h2 class="dv_title">
            新增学员
        </h2>
        <a id="aBack" runat="server" class="btn_Return">返回</a>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0">               
                <tr>
                    <th width="120">
                       工&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;号：
                    </th>
                    <td width="120">
                        <asp:TextBox ID="txtWorkerNo" runat="server" CssClass="inputbox_120"/>
                    </td>
                    <th width="120">
                        姓&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;名：
                    </th>
                    <td width="130">
                        <asp:TextBox ID="txtRealName" runat="server" CssClass="inputbox_120"/>
                    </td>
                    <td>
                       <asp:Button ID="btnSearch" runat="server" CssClass="btn_Search" Text="查询" OnClick="btnSearch_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel" id="divPage1">
                <div class="dv_selectAll">
                    <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" />
                    <asp:Button ID="btnSave" runat="server" Text="确定" CssClass="btn_Add" OnClick="btnSave_Click" />
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ClassStudentID"  OnRowDataBound="CustomGridView1_RowDataBound" >
                <Columns>
                    <asp:TemplateField HeaderText="">
                        <ItemStyle HorizontalAlign="Center" Width="28" />
                        <HeaderStyle HorizontalAlign="Center" Width="20" />
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:BoundField DataField="WorkerNo" HeaderText="工号" SortExpression="WorkerNo" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft" HeaderStyle-Width="60">
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RealName" HeaderText="姓名" SortExpression="RealName" HeaderStyle-Width="60">
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="组织机构" HeaderStyle-Width="60">
                                <ItemTemplate>
                                    <cc1:DictionaryLabel runat="server" ID="lblOrg" DictionaryType="vw_Dic_Sys_Organization" FieldIDValue='<%#Eval("OrganizationID").ToString() %>' TextLength="10"/>  
                                 </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                                <ItemTemplate>
                                     <cc1:DictionaryLabel runat="server" ID="lblDepartment" DictionaryType="vw_Dic_Sys_Department" FieldIDValue='<%#Eval("DepartmentID").ToString() %>' TextLength="10"/>   
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_position%>" HeaderStyle-Width="80">
                                <ItemTemplate>
                                    <cc1:DictionaryLabel runat="server" ID="lblPost" DictionaryType="vw_Dic_Sys_Post" FieldIDValue='<%#Eval("PostID").ToString() %>' TextLength="10"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_rank%>" HeaderStyle-Width="100">
                                <ItemTemplate>                                
                                    <cc1:DictionaryLabel runat="server" ID="lblRank" DictionaryType="vw_Dic_Sys_Rank" FieldIDValue='<%#Eval("RankID").ToString() %>' TextLength="10" />
                                </ItemTemplate>
                            </asp:TemplateField>                                                     
                    <asp:TemplateField HeaderText="班级职务" HeaderStyle-Width="80">
                        <ItemTemplate>
                            <asp:Label ID="lblPostion" runat="server" />
                            <%--<%#string.IsNullOrEmpty(Eval("ClassPostion").ToString()) ? "学员" : Eval("ClassPostion")%>--%>
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
        </div>
    </div>
    <div class="center">
        
        <%--<asp:Button ID="btnCancel" runat="server" Text="返回" CssClass="btn_Cancel" OnClick="btnCancel_Click" />--%>
        <%--<input type="button" class="btn_Ok" value="确定" onclick="javascript:history.back();" />
        <input type="button" class="btn_Cancel" value="返回" onclick="javascript:history.back();" />--%>
    </div>
</asp:Content>
