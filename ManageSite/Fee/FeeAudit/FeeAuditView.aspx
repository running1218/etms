<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="FeeAuditView.aspx.cs" Inherits="FeeAuditView" %>
<%@ Register Src="~/Fee/FeeAudit/Controls/FeeView.ascx" TagName="FeeView" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <!--导航路径-->
        <div class="dv_path">
            当前位置：费用管理>>课酬管理>>查看课时费用确认单明细
        </div>
        <!--功能标题-->
        <h2 class="dv_title">
            课时费用确认单明细
            <a href="FeeAuditList.aspx" class="btn_Return" title="返回">&nbsp;</a>
        </h2>
        <uc:FeeView ID="fv" Action="View" runat="server" />
    </div>
</asp:Content>

