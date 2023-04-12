<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MarkingNoSubmit.ascx.cs" Inherits="Grade_GradeManage_Controls_MarkingNoSubmit" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/ChooseCourseDropdown.ascx" TagName="ChooseCourseDropdown"
    TagPrefix="uc1" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc3" %>
<!--查找结果-->
<div class="dv_searchlist ">
    <!--翻页-->
    <div class="dv_pagePanel">
        <div class="dv_pageControl">
            <uc3:PageSet ID="PageSet3" runat="server" />
        </div>
    </div>
    <!--列表 begin-->
    <cc1:CustomGridView ID="GridViewList" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="LoginName" HeaderText="帐号" SortExpression="WorkerNo" HeaderStyle-Width="120"
                HeaderStyle-CssClass="alignleft">
                <ItemStyle HorizontalAlign="Left" CssClass="alignleft" />
                <HeaderStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="RealName" HeaderText="学员姓名" SortExpression="JobName" HeaderStyle-CssClass="alignleft"
                ItemStyle-CssClass="alignleft" HeaderStyle-Width="120">
                <ItemStyle HorizontalAlign="Left" CssClass="alignleft" />
                <HeaderStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="组织机构" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                <ItemTemplate>
                    <cc1:DictionaryLabel ID="lblOrganization" DictionaryType="vw_Dic_Sys_Organization"
                        FieldIDValue='<%# Eval("OrganizationID") %>' runat="server" TextLength="10" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="MobilePhone" HeaderText="电话" SortExpression="MobilePhone"
                HeaderStyle-CssClass="field12">
                <ItemStyle HorizontalAlign="Left" />
                <HeaderStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="邮箱" HeaderStyle-Width="200" HeaderStyle-CssClass="alignleft"
                ItemStyle-CssClass="alignleft">
                <ItemTemplate>
                    <cc1:ShortTextLabel ShowTextNum="20" Text='<%#Eval("Email") %>' runat="server" ID="lblEmail" />
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
