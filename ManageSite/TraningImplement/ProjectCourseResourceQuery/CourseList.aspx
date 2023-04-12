<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="CourseList.aspx.cs" Inherits="TraningImplement_ProjectCourseResourceQuery_CourseList" %>

<%--<%@ Register Assembly="HampWebControl" Namespace="HampWebControl.AjaxTextBox" TagPrefix="cc2" %>--%>
<%@ Register Src="~/Controls/ChooseItemDropdown.ascx" TagName="ChooseItemDropdown"
    TagPrefix="uc1" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
                id="tableQueryControlList">
                <tr>
                    <th width="120">
                        项目名称：
                    </th>
                    <th width="390">
                        <asp:DropDownList ID="ddl_Item" runat="server" CssClass="select_390">
                        </asp:DropDownList>
                    </th>
                    <td width="200">
                        <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel" id="divPage1">
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <div id="dourse-content">
                <asp:Repeater ID="rptCourseList" runat="server" OnItemDataBound="rptCourseList_OnItemDataBound">
                    <HeaderTemplate>
                      <div class="dv_splitLine"></div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="dv_courseList">
                            <div class="dv_userphoto">
                                <asp:Image ID="imgLogo" runat="server" ImageAlign="AbsMiddle" BorderWidth="0" ImageUrl='<%# Eval("ThumbnailURL") %>' />
                            </div>
                            <div class="dv_curseInformation">
                                <div class="dv_lcoright">
                                </div>
                                <div class="dv_lcoleft">
                                </div>
                                <div class="dv_lcorepeat">
                                    <table>
                                        <tr>
                                            <th class="courseName" style="width:60px;text-align:left;" >
                                                课程名称：
                                            </th>
                                            <td class="courseName alignleft" >
                                                <cc1:ShortTextLabel ID="lblItemName" runat="server" ShowTextNum="10" Text='<%# Eval("CourseName")%>'></cc1:ShortTextLabel>
                                            </td>
                                            <th class="courseName"  style="width:70px;" >
                                                课程编号：
                                            </th>
                                            <td class="courseName"  style="width:80px;" >
                                                <%# Eval("CourseCode")%>
                                            </td>
                                            <th class="courseName" style="width:60px;">
                                                状态：
                                            </th>
                                            <td class="courseName" style="width:60px;">
                                                <cc1:DictionaryLabel ID="lblStatus" runat="server" DictionaryType="Dic_Status" FieldIDValue='<%# Eval("CourseStatus") %>'></cc1:DictionaryLabel><asp:HiddenField
                                                    ID="hfTrainingItemCourseID" runat="server" Value='<%# Eval("TrainingItemCourseID") %>' />
                                                <asp:HiddenField ID="hfCourseID" runat="server" Value='<%# Eval("CourseID") %>' />
                                            </td>
                                        </tr>
                                    </table>
                                    <div id="dv_JobType">
                                        <asp:Label ID="lblCourseID" runat="server" Text='<%# Eval("CourseID") %>' CssClass="hide"></asp:Label>
                                        <asp:DataList ID="dltJobList" runat="server" RepeatDirection="Horizontal" RepeatColumns="3">
                                            <ItemTemplate>
                                                <div class="dv_job_item">
                                                    <div class="dv_job_item_title">
                                                        <asp:Label ID="lblResourceName" runat="server" Text='<%# Eval("ResourceName")%>'></asp:Label>
                                                        <asp:LinkButton ID="lbtnFunctionUrl" runat="server" PostBackUrl='<%# Eval("FunctionUrl") %>'
                                                            Text='<%# Eval("ItemResourceNum")%>'></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater><div style="text-align:center"><asp:Literal ID="ltlNull" runat="server"></asp:Literal></div>
            </div>
            <!--列表 end-->
            <div class="dv_splitLine">
            </div>
            <!--翻页-->
            <div class="dv_pagePanel" id="divPage2">
            </div>
        </div>
    </div>
</asp:Content>
