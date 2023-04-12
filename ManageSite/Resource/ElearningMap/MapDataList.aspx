<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="MapDataList.aspx.cs" Inherits="Resource_ElearningMap_MapDataList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<%@ Register Src="Controls/ElearningMapInfoView.ascx" TagName="ElearningMapInfoView"
    TagPrefix="uc3" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" runat="Server">
    <a href="ElearningMapList.aspx" class="btn_Return">返回</a>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--学习地图信息-->
    <uc3:ElearningMapInfoView ID="ElearningMapInfoView1" runat="server" />
    <!--课程列表-->
    <div class="dv_searchlist">
        <!--翻页-->
        <div class="dv_pagePanel">
            <div class="dv_selectAll">
                <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="gvList" />
                <asp:Button ID="lbtAdd" CssClass="btn_Add" runat="server" Text="新增"></asp:Button>
                <cc1:CustomButton CssClass="btn_Del" Text="删除" runat="server" ID="btnDeletes" EnableConfirm="true"
                    ConfirmMessage="确定要删除吗?" OnClick="btnDeletes_Click" />
            </div>
            <div class="dv_pageControl">
                <uc2:PageSet ID="PageSet1" runat="server" />
            </div>
        </div>
        <!--列表 begin-->
        <cc1:CustomGridView ID="gvList" runat="server" CssClass="GridviewGray" EmptyDataSourceTip="没有任何记录！"
            AutoCreateColumnInsertIndex="0" AutoGenerateColumns="False" CurrentPageIndex="1"
            CustomAllowPaging="False" IsEmpty="False" TotalRecordCount="0" DataKeyNames="Res_StudyMapReferIDPID">
            <Columns>
                <asp:TemplateField HeaderText="">
                    <ItemStyle HorizontalAlign="Center" Width="20" />
                    <HeaderStyle HorizontalAlign="Center"  Width="20"/>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="资料编码"  ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft field12">
                    <ItemTemplate>
                        <cc1:ShortTextLabel ID="lblDataCode" runat="server" ShowTextNum="10" Text='<%# Eval("DataCode")%>'></cc1:ShortTextLabel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="资料名称" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft" ItemStyle-Width="20%">
                    <ItemTemplate>
                        <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="10" Text='<%# Eval("DataName")%>'></cc1:ShortTextLabel>
                    </ItemTemplate>
                </asp:TemplateField>                
                <asp:TemplateField HeaderText="学习纲要" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft" ItemStyle-Width="20%">
                    <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblCourseType" runat="server" ShowTextNum="10" Text='<%# Eval("DataOutline")%>'></cc1:ShortTextLabel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="预计时长" HeaderStyle-Width="80" ItemStyle-CssClass="alignright" HeaderStyle-CssClass="alignright">
                    <ItemTemplate>
                        <%# Eval("TimeLength") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="学习方式" HeaderStyle-Width="80px">
                    <ItemTemplate>
                        <cc1:DictionaryLabel ID="lblStudyModel" DictionaryType="Dic_Sys_StudyModel" FieldIDValue='<%# Eval("StudyModelID") %>'
                            runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="资料状态" HeaderStyle-Width="80">
                    <ItemTemplate>
                        <cc1:DictionaryLabel ID="lblCourseStatus" DictionaryType="Dic_Status" FieldIDValue='<%# Eval("DataStatus") %>'
                            runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作" HeaderStyle-Width="80px">
                    <ItemTemplate>
                        <a href="javascript:showWindow('查看非课程资料','<%# this.ActionHref(string.Format("~/Resource/ElearningMap/DataView.aspx?NotCourseDataID={0}", Eval("IDP_NotCourseDataID"))) %>')">查看</a>
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
</asp:Content>
