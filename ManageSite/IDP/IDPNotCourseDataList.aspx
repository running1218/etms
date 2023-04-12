<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="IDPNotCourseDataList.aspx.cs" Inherits="IDP_IDPNotCourseDataList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--查找条件-->
    <div class="dv_searchbox">
        <table class="GridviewGray th80" border="0" cellpadding="0" cellspacing="0" id="tableQueryControlList"
            runat="server">
            <tr>
                <th width="120">
                    资料编码：
                </th>
                <td width="120">
                    <asp:TextBox ID="txt_DataCode" runat="server" />
                </td>
                <th width="120">
                    资料名称：
                </th>
                <td width="135">
                    <asp:TextBox ID="txt_DataName" runat="server" />
                    <asp:Button ID="btnSearch" runat="server" CssClass="btn_Search" Text="查询" OnClick="btnSearch_Click" />
                    <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
                 </td>
            </tr>
            <tr>
                <th>
                    学习内容：
                </th>
                <td>
                    <asp:TextBox ID="txt_DataCotent" runat="server"  />
                </td>
                <th>
                    学习纲要：
                </th>
                <td colspan="2">
                    <asp:TextBox ID="txt_DataOutline" runat="server" />
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
                        <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="GridViewList" Visible="false" />
                        <input type="button" class="btn_Add" value="新增" onclick="javascript:showWindow('非课程资料基本信息','<%=this.ActionHref("IDPNotCourseDataInfo.aspx?Operation=1") %>')" />
                        <cc1:CustomButton CssClass="btn_Del" Text="删除" runat="server" ID="btnDeletes" EnableConfirm="true"
                            ConfirmMessage="确信要执行“批量删除”操作吗?" Visible="false" />
                    </div>
                    <div class="dv_pageControl">
                        <uc2:PageSet ID="PageSet1" runat="server" />
                    </div>
                </div>
                <cc1:CustomGridView ID="GridViewList" runat="server" AutoGenerateColumns="False" OnRowCommand="GridViewList_RowCommand"
                    DataKeyNames="IDP_NotCourseDataID">
                    <Columns>
                        <asp:TemplateField HeaderText="资料编码" HeaderStyle-CssClass="alignleft field12" ItemStyle-CssClass="alignleft">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblDataCode" runat="server" ShowTextNum="10" Text='<%#Eval("DataCode")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="资料名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblDataName" runat="server" ShowTextNum="5" Text='<%#Eval("DataName")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>                        
                        <asp:TemplateField HeaderText="学习内容"  ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblDataCotent" runat="server" ShowTextNum="5" Text='<%#Eval("DataCotent")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="学习纲要" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft" >
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblDataOutline" runat="server" ShowTextNum="10" Text='<%#Eval("DataOutline")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="学习方式" HeaderStyle-Width="60" >
                            <ItemTemplate>
                                <cc1:DictionaryLabel DictionaryType="Dic_Sys_StudyModel" ID="lblStudyModelID" runat="server"
                                    FieldIDValue='<%#Eval("StudyModelID")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="TimeLength" HeaderText="预计时长" SortExpression="TimeLength" HeaderStyle-Width="60" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright" />
                        <asp:BoundField DataField="StudyTimes" HeaderText="次数" SortExpression="StudyTimes"  HeaderStyle-Width="40" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright"/>
                        <asp:TemplateField HeaderText="资料状态" HeaderStyle-Width="60">
                            <ItemTemplate>
                                <cc1:DictionaryLabel DictionaryType="Dic_Status" ID="lblDataStatus" runat="server"
                                    FieldIDValue='<%#Eval("DataStatus")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="操作" HeaderStyle-Width="100" >
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <a href="javascript:showWindow('非课程资料管理','<%# this.ActionHref(string.Format("IDPNotCourseDataInfo.aspx?Operation=2&IDP_NotCourseDataID={0}", new Object[]{Eval("IDP_NotCourseDataID")})) %>')">
                                    编辑</a>                                 
                                 <cc1:CustomLinkButton runat="server" ID="lbtn_Del" CommandArgument='<%# Eval("IDP_NotCourseDataID") %>'
                                CommandName="del" Text="删除" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="确定要删除吗？" />
                                    <a href="javascript:showWindow('非课程资料表管理','<%# this.ActionHref(string.Format("IDPNotCourseDataInfo.aspx?Operation=3&IDP_NotCourseDataID={0}", new Object[]{Eval("IDP_NotCourseDataID")})) %>')">查看</a>
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
