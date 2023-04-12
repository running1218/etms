﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CourseProjectPeriodView.ascx.cs"
    Inherits="TraningImplement_TraningProjectManager_Controls_CourseProjectPeriodView" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<!--查找结果-->
<div class="dv_searchlist">
    <!--翻页-->
    <div class="dv_pagePanel" id="divPage1">
        <div class="dv_pageControl">
            <uc2:PageSet ID="PageSet1" runat="server" />
        </div>
    </div>
    <!--列表 begin-->
    <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="True"
        CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0">
        <Columns>
            <asp:TemplateField HeaderText="序号" Visible="false">
                <ItemStyle HorizontalAlign="Center" Width="30" />
                <HeaderStyle HorizontalAlign="Center" />
                <ItemTemplate>
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