<%@ Page Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="ChooseData.aspx.cs" Inherits="Resource_ElearningMap_ChooseData" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" runat="Server">
    <a href='<%= this.ActionHref(string.Format("~/Resource/ElearningMap/MapDataList.aspx?StudyMapID={0}", StudyMapID))%>' class="btn_Return">返回</a>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <!--查找条件-->
    <div class="dv_searchbox">
        <table class="GridviewGray th120" border="0" cellpadding="0" cellspacing="0" runat="server"
            id="tableQueryControlList">
            <tr>
                <th >
                    资料编码：
                </th>
                <td >
                    <asp:TextBox ID="txt_DataCode" runat="server" CssClass="inputbox_120"></asp:TextBox>
                </td>
                <th >
                    资料名称：
                </th>
                <td class="Search_Area">
                    <asp:TextBox ID="txt_DataName" runat="server" CssClass="inputbox_120 floatleft"></asp:TextBox>
                    <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
                    <a href="javascript:hideGridview()" class="dropdownico " id="Highsearch">高级搜索</a>
                </td>
            </tr>
            <tr>
                <th>
                    学习内容：
                </th>
                <td>
                    <asp:TextBox ID="txtDataCotent" runat="server" CssClass="inputbox_120"></asp:TextBox>
                </td>
                <th>
                    学习纲要：
                </th>
                <td>
                    <asp:TextBox ID="txtDataOutline" runat="server" CssClass="inputbox_120"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div class="dv_searchlist">
        <%--<asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional">
            <ContentTemplate>--%>
                <!--翻页-->
                <div class="dv_pagePanel" id="divPage1">
                    <div class="dv_selectAll">
                        <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" />
                        <asp:Button ID="lbtnSave" runat="server" CssClass="btn_Ok" OnClick="lbtnSave_Click" Text="确定" />
                    </div>
                    <div class="dv_pageControl" style="float: right;">
                        <uc2:PageSet ID="PageSet1" runat="server" />
                    </div>
                </div>
                <!--列表 begin-->
                <cc1:CustomGridView ID="CustomGridView1" runat="server" CssClass="GridviewGray" EmptyDataSourceTip="没有任何记录！"
                    AutoCreateColumnInsertIndex="0" AutoGenerateColumns="False" CurrentPageIndex="1"
                    CustomAllowPaging="False" IsEmpty="False" TotalRecordCount="0" DataKeyNames="IDP_NotCourseDataID">
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <ItemStyle HorizontalAlign="Center" Width="20" />
                            <HeaderStyle HorizontalAlign="Center" Width="20" />
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="资料编码" HeaderStyle-Width="80px" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                            <ItemTemplate>
                                <%# Eval("DataCode")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="资料名称" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft" ItemStyle-Width="20%">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="10" Text='<%# Eval("DataName")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>                        
                        <asp:TemplateField HeaderText="学习纲要" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                            <ItemTemplate>
                                    <cc1:ShortTextLabel ID="lblCourseType" runat="server" ShowTextNum="10" Text='<%# Eval("DataOutline")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="预计时长" HeaderStyle-Width="60" ItemStyle-CssClass="alignright" HeaderStyle-CssClass="alignright">
                            <ItemTemplate>
                                <%# Eval("TimeLength") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="学习方式" HeaderStyle-Width="60px" >
                            <ItemTemplate>
                                <cc1:DictionaryLabel ID="lblStudyModel" DictionaryType="Dic_Sys_StudyModel" FieldIDValue='<%# Eval("StudyModelID") %>'
                                    runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="资料状态" HeaderStyle-Width="60">
                            <ItemTemplate>
                                <cc1:DictionaryLabel ID="lblCourseStatus" DictionaryType="Dic_Status" FieldIDValue='<%# Eval("DataStatus") %>'
                                    runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </cc1:CustomGridView>
                <!--列表 end-->
                <div class="dv_splitLine">
                </div>
                <div class="dv_pagePanel" id="divPage2">
                </div>
                <asp:ObjectDataSource ID="odsStudyModel" runat="server" 
                    SelectMethod="GetCommonSysDictionary" 
                    TypeName="ETMS.Components.Basic.Implement.BLL.Dictionary.SysDict">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="Dic_Sys_StudyModel" Name="tableName" 
                            Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
       <%--     </ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
    <!--提交表单-->

</asp:Content>

