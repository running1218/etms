<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EvaluationScore.ascx.cs" Inherits="Valuation_Controls_EvaluationScore" %>

<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>

<div class="dv_searchlist">
    <!--翻页-->
    <div class="dv_pagePanel">
        <div class="dv_selectAll">
        </div>
        <div class="dv_pageControl">
            <uc2:pageset id="PageSet1" runat="server" />
        </div>
    </div>
    <!--列表 begin-->
    <cc1:CustomGridView ID="CustomGridView1" runat="server" CssClass="GridviewGray" EmptyDataSourceTip="没有任何记录！"
        AutoCreateColumnInsertIndex="0" AutoGenerateColumns="False" 
        CustomAllowPaging="False" IsEmpty="False" TotalRecordCount="0" >
        <Columns>
            <asp:TemplateField HeaderText="姓名" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft field10">
                <ItemTemplate>
                    <cc1:ShortTextLabel ID="lblUserName" runat="server" ShowTextNum="15" Text='<%# getUserName(Eval("UserID").ToInt())%>'></cc1:ShortTextLabel>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="时间"  HeaderStyle-Width="120">
                <ItemTemplate>
                    <%#Eval("CreateTime")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="评价项" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft" >
                <ItemTemplate>
                    <cc1:ShortTextLabel ID="lblItemName" runat="server" Text='<%#Eval("ItemName")%>' ShowTextNum="100" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="打分" HeaderStyle-Width="40">
                <ItemTemplate>
                    <%#Eval("Score")%>
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