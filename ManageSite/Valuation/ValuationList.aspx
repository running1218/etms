<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="ValuationList.aspx.cs" Inherits="Valuation_ValuationList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--查找结果-->
    <div class="dv_searchlist">
        <!--翻页-->
        <div class="dv_pagePanel" style="display:none;">
            <div class="dv_selectAll">
            </div>
            <div class="dv_pageControl">
                <uc2:PageSet ID="PageSet1" runat="server" />
            </div>
        </div>
        <!--列表 begin-->
        <cc1:CustomGridView ID="CustomGridView1" runat="server" CssClass="GridviewGray" EmptyDataSourceTip="没有任何记录！"
            AutoCreateColumnInsertIndex="0" AutoGenerateColumns="False" CurrentPageIndex="1"
            CustomAllowPaging="False" IsEmpty="False" TotalRecordCount="0" DataKeyNames="PlateID">
            <Columns>
                <asp:BoundField DataField="PlateName" HeaderText="名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft" />
                
              <asp:TemplateField HeaderText="评价对象" HeaderStyle-Width="120" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <cc1:DictionaryLabel ID="lblObjectTypeName" DictionaryType="Evaluation_b_ObjectType" FieldIDValue='<%# Eval("ObjectTypeID") %>'
                            runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
               
                <asp:TemplateField HeaderText="内容" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <%# getEvaluationItems(Eval("PlateID").ToString())%>
                    </ItemTemplate>
                </asp:TemplateField>
               <asp:TemplateField HeaderText="状态" HeaderStyle-Width="60">
                    <ItemTemplate>
                        <cc1:DictionaryLabel ID="lblStatus" DictionaryType="Dic_Status" FieldIDValue='<%# Eval("IsUse") %>'
                            runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作" ItemStyle-Width="90" Visible="false">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <a href="javascript:showWindow('编辑评价量表','<%# this.ActionHref(string.Format("~/Valuation/ValuationEdit.aspx?PlateID={0}", Eval("PlateID")))%>')">
                            编辑</a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </cc1:CustomGridView>
        <!--列表 end-->
        <div class="dv_splitLine">
        </div>
    </div>
</asp:Content>
