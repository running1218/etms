<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="ElearningMapList.aspx.cs" Inherits="ETMS.WebApp.Manage.ElearningMapList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
                id="tableQueryControlList">
                <tr>
                    <th width="120" class="hide">
                        学习地图类型：
                    </th>
                    <td width="120" class="hide">
                        <cc1:DictionaryDropDownList runat="server" ID="ddl_ELearningMapTypeID" DictionaryType="Dic_Sys_ELearningMapType" />
                    </td>
                    <th width="120">
                        学习地图名称：
                    </th>
                    <td>
                        <asp:TextBox ID="txt_StudyMapName" runat="server" CssClass="inputbox_120 floatleft"
                            MaxLength="50"></asp:TextBox>
                        <%--<asp:UpdatePanel ID="upQuery" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>--%>
                        <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
                        <%--  </ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </td>
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <%-- <asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional">
                <ContentTemplate>--%>
            <!--翻页-->
            <div class="dv_pagePanel">
                <div class="dv_selectAll">
                    <input type="button" class="btn_Add" value="新增" onclick="javascript:showWindow('新增学习地图','ElearningMapAdd.aspx')" />
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="gvList" runat="server" CssClass="GridviewGray" EmptyDataSourceTip="没有任何记录！"
                AutoCreateColumnInsertIndex="0" AutoGenerateColumns="False" CurrentPageIndex="1"
                CustomAllowPaging="False" IsEmpty="False" TotalRecordCount="0" DataKeyNames="StudyMapID"
                OnRowCommand="gvList_RowCommand">
                <Columns>
                    <asp:BoundField DataField="StudyMapCode" HeaderText="学习地图编码" HeaderStyle-Width="110" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft" />
                    <asp:TemplateField HeaderText="学习地图名称" ItemStyle-CssClass="alignleft"  HeaderStyle-CssClass="alignleft" >
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblStudyMapName" runat="server" ShowTextNum="10" Text='<%# Eval("StudyMapName")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="部门" ItemStyle-CssClass="alignleft"  HeaderStyle-CssClass="alignleft" >
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblDept" DictionaryType="Site_DepartmentByOrgID" TextLength="10"
                                FieldIDValue='<%# Eval("DeptID") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_position%>"  ItemStyle-CssClass="alignleft"  HeaderStyle-CssClass="alignleft" >
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblPost" DictionaryType="Dic_PostByOrgID" TextLength="8"
                                FieldIDValue='<%# Eval("PostID") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_rank%>" HeaderStyle-Width="60" ItemStyle-CssClass="alignleft"  HeaderStyle-CssClass="alignleft" >
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblRank" DictionaryType="vw_Dic_Sys_Rank" TextLength="8"
                                FieldIDValue='<%# Eval("RankID") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程数" HeaderStyle-Width="50px">
                        <ItemTemplate>
                            <%# Eval("CourseNum") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="资料数" HeaderStyle-Width="50px">
                        <ItemTemplate>
                            <%# Eval("DataNum")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作" HeaderStyle-Width="140px">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <a href="javascript:showWindow('编辑学习地图','<%# this.ActionHref(string.Format("~/Resource/ElearningMap/ElearningMapEdit.aspx?StudyMapID={0}", Eval("StudyMapID")))%>')">编辑</a>
                            <cc1:CustomLinkButton runat="server" ID="lbtn_Del" CommandArgument='<%# Eval("StudyMapID") %>' CommandName="Del" Text="删除" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="确定要删除吗？" />
                            <a href='<%# this.ActionHref(string.Format("~/Resource/ElearningMap/MapCourseList.aspx?StudyMapID={0}", Eval("StudyMapID")))%>'>课程</a>
                            <a href='<%# this.ActionHref(string.Format("~/Resource/ElearningMap/MapDataList.aspx?StudyMapID={0}", Eval("StudyMapID")))%>'>资料</a>
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
            <%--</ContentTemplate>
            </asp:UpdatePanel>--%>
        </div>
    </div>
</asp:Content>
