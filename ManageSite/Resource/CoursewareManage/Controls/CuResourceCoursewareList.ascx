<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CuResourceCoursewareList.ascx.cs"
    Inherits="ETMS.WebApp.Manage.CuResourceCoursewareList" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<div>
    <!--导航路径-->
    <div class="dv_path" id="dv_path">
        当前位置：资源管理系统>>学习资源管理>>在线课件管理
    </div>
    <!--功能标题-->
    <h2 class="dv_title">
        在线课件管理
    </h2>
    <!--查找条件-->
    <div class="dv_searchbox">
        <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
            id="tableQueryControlList">
            <tr>
                <th width="15%">
                    课程编码：
                </th>
                <td width="20%">
                    <asp:Label ID="lblCourseCode" runat="server"></asp:Label>
                </td>
                <th width="15%">
                    课程名称：
                </th>
                <td>
                    <asp:Label ID="lblCourseName" runat="server"></asp:Label>
                    <span class="hide">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn_Search" Text="查询" OnClick="btnSearch_Click" />
                    </span>
                </td>               
            </tr>           
        </table>
    </div>
    <!--查找结果-->
    <asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="dv_searchlist">
                <!--翻页-->
                <div class="dv_pagePanel">
                    <div class="dv_selectAll" id="dv_selectall" runat="server">
                        <asp:Button ID="btnAddScorm" runat="server" CssClass="btn_AddNew" Text="SCORM标准" />
                        <asp:Button ID="btnAddNoScorm" runat="server" CssClass="btn_AddNew" Text="非SCORM标准" />                       
                    </div>
                    <div class="dv_pageControl">
                        <uc2:PageSet ID="PageSet1" runat="server" />
                    </div>
                </div>
                <!--列表 begin-->
                <cc1:CustomGridView ID="CustomGridView1" runat="server" CssClass="GridviewGray" EmptyDataSourceTip="没有任何记录！"
                    AutoCreateColumnInsertIndex="0" AutoGenerateColumns="False" CurrentPageIndex="1" OnRowDataBound="CustomGridView1_RowDataBound"
                    CustomAllowPaging="False" IsEmpty="False" TotalRecordCount="0" DataKeyNames="CoursewareID"
                    OnRowCommand="CustomGridView1_RowCommand">
                    <Columns>                                              
                        <asp:TemplateField HeaderText="课件名称" ItemStyle-CssClass="alignleft">
                           <ItemTemplate>
                              <cc1:ShortTextLabel ID="lblCoursewareName" runat="server" ShowTextNum="10" Text='<%# Eval("CoursewareName")%>' ></cc1:ShortTextLabel>
                           </ItemTemplate>
                        </asp:TemplateField>          
                        <asp:TemplateField HeaderText="课件类型">
                            <ItemTemplate>
                                <asp:Label ID="lblCoursewareType" runat="server" Text='<%# Eval("CoursewareTypeID").ToString()=="1"?"SCORM标准":"非SCORM标准" %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="课件状态">
                            <ItemTemplate>
                                <asp:Label ID="lblCoursewareStatus" runat="server" Text='<%# Eval("CoursewareStatus").ToString()=="1"?"启用":"停用" %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="创建时间">
                          <HeaderStyle HorizontalAlign="Center" />
                          <ItemStyle HorizontalAlign="Center" />
                          <ItemTemplate>
                             <asp:Label ID="lblCreateTime" runat="server" Text='<%# Eval("CreateTime").ToDate() %>' />
                          </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CreateUser" HeaderText="创建者" />
                        <asp:TemplateField HeaderText="操作">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                 <asp:LinkButton ID="lblScormEdit" Text="编辑" runat="server"/>
                                   <asp:LinkButton ID="lblScormView" Text="查看" runat="server"/>
                                   <asp:LinkButton ID="lblEdited" Text="编辑" runat="server"/>
                                   <asp:LinkButton ID="lblViewed" Text="查看" runat="server"/>     
                                <cc1:CustomLinkButton
                                    runat="server" ID="lbtn_Del" CommandArgument='<%# Eval("CourseResID") %>' CommandName="Del"
                                    Text="删除" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="确定要删除吗？" />
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
