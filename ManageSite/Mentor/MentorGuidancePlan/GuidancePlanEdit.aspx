<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="GuidancePlanEdit.aspx.cs" Inherits="Mentor_MentorGuidancePlan_GuidancePlanEdit" %>

<%@ Register src="Controls/GuidancePlanInfo.ascx" tagname="GuidancePlanInfo" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <!--功能标题-->
        <h2 class="dv_title">
            编辑辅导计划
        </h2>
    <uc1:GuidancePlanInfo ID="GuidancePlanInfo1" runat="server" />

</asp:Content>

