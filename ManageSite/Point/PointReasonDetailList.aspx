<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="PointReasonDetailList.aspx.cs" Inherits="Point_PointReasonDetailList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
            <div class="dv_searchbox">
                            
                <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
                    id="tableQueryControlList">
                    <tr>
                        <th width="120">
                            培训项目：
                        </th>
                        <td width="130">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
               <ContentTemplate>                     
                            <asp:DropDownList ID="ddl_ItemName" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="ddl_ItemName_SelectedIndexChanged" />
                            </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <th width="120">
                            班&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;级：
                        </th>
                        <td style="width:120px">
                             <asp:UpdatePanel ID="updataPannel" runat="server" UpdateMode="Conditional" >
               <ContentTemplate> 
                            <asp:DropDownList ID="ddlClassName" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlClassName_SelectedIndexChanged" />
                            </ContentTemplate>
                            <Triggers>
                              <asp:AsyncPostBackTrigger ControlID="ddl_ItemName" EventName="SelectedIndexChanged" />
                            </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td class="Search_Area">
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn_Search" Text="查询" OnClick="btnSearch_Click" />
                            <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            学习群组：
                        </th>
                        <td>
                             <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" >
               <ContentTemplate> 
                            <asp:DropDownList runat="server" ID="ddlGroupName" />
                            </ContentTemplate>
                            <Triggers>
                              <asp:AsyncPostBackTrigger ControlID="ddlClassName" EventName="SelectedIndexChanged" />
                            </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <th>
                            学员姓名：
                        </th>
                        <td colspan="2">
                            <asp:TextBox ID="txtRealName" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            学员工号：
                        </th>
                        <td colspan="4">
                            <asp:TextBox ID="txtWorkerNo" runat="server" />
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
                        <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" />
                      <%--  <input type="button" class="btn_Add" value="新增" onclick="javascript:showWindow('批量增加','<%=this.ActionHref(string.Format("PointReasonDetailListAdd.aspx?StudentCourse={0}")) %>')" />--%>
                        <asp:Button ID="btnAdd" runat="server" Text="批量新增" CssClass="btn_Batchnew" OnClick="btnAdd_Click" />
                    </div>                   
                    <div class="dv_pageControl">
                        <uc2:PageSet ID="PageSet1" runat="server" />
                    </div>                    
                </div>
                <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="False"
                    DataKeyNames="StudentSignupID" OnRowDataBound="CustomGridView1_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="" HeaderStyle-Width="20">
                            <ItemStyle HorizontalAlign="Center"  />
                            <HeaderStyle HorizontalAlign="Center" Width="20" />
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="学员姓名" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft field8" >
                            <ItemTemplate>
                                <asp:Label ID="lblRealName" runat="server" Text='<%#Eval("RealName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="组织机构" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                            <ItemTemplate>
                                <cc1:DictionaryLabel runat="server" ID="lblOrg" DictionaryType="vw_Dic_Sys_Organization"
                                    FieldIDValue='<%#Eval("OrganizationID").ToString() %>' TextLength="10" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                            <ItemTemplate>
                                <cc1:DictionaryLabel runat="server" ID="lblDepartment" DictionaryType="vw_Dic_Sys_Department"
                                    TextLength="10" FieldIDValue='<%#Eval("DepartmentID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_position%>" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                            <ItemTemplate>
                                <cc1:DictionaryLabel runat="server" ID="lblPost" DictionaryType="vw_Dic_Sys_Post"
                                    TextLength="10" FieldIDValue='<%#Eval("PostID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="班级">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblClassName" runat="server" Text='<%#Eval("ClassName") %>'
                                    ShowTextNum="10" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="学习群组" >
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblGroupName" runat="server" ShowTextNum="10" Text='<%#Eval("ClassSubgroupName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="未发布积分" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright" HeaderStyle-Width="80">
                            <ItemTemplate>
                               <%-- --%>
                                <asp:HyperLink ID="lblPoint" runat="server" Text='<%# pointReasonDetailLogic.StatStudentInputPointByStudentSignupID(Eval("StudentSignupID").ToGuid())%>' NavigateUrl='<%#this.ActionHref(string.Format("PointReasonDetailDetailList.aspx?StudentSignupID={0}&TrainingItemID={1}",Eval("StudentSignupID"),ddl_ItemName.SelectedValue.ToGuid())) %>' />
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
</asp:Content>
