<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="CourseResource.aspx.cs" Inherits="ETMS.WebApp.Manage.Resource_CourseResource" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" id="tableQueryControlList"
                runat="server">
                <tr>
                    <th width="120">
                        课程编码：
                    </th>
                    <td width="125">
                        <asp:TextBox ID="txt_CourseCode" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                    <th width="120">
                        课程名称：
                    </th>
                    <td  colspan="2">
                        <asp:TextBox ID="txt_CourseName" runat="server" CssClass="inputbox_120"></asp:TextBox>
                        <asp:Button ID="lbnSearch" runat="server" Text="查询" CssClass="btn_Search" OnClick="btnSearch_Click"></asp:Button>
                    </td>
                   
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel">
                <div class="dv_selectAll">
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <div class="dv_splitLine"></div>
            <div id="dourse-content">
                <asp:Repeater ID="rptCourseList" runat="server" OnItemDataBound="rptCourseList_OnItemDataBound">
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
                                            <th class="courseName" width="70" style="text-align: left">
                                                课程名称：
                                            </th>
                                            <td class="courseName">
                                                <cc1:ShortTextLabel ID="lblCourseName" runat="server" Text='<%# Eval("CourseName")%>' ToolTip='<%# Eval("CourseName")%>' CssClass="textover" ShowTextNum="12"></cc1:ShortTextLabel>
                                            </td>
                                            <th class="courseName" width="70" style="text-align: left">
                                                课程编号：
                                            </th>
                                            <td width="100" class="courseName">
                                                <%# Eval("CourseCode")%>
                                            </td>
                                            <th class="courseName" width="40" style="text-align: left">
                                                状态：
                                            </th>
                                            <td width="40" class="courseName">
                                                <cc1:DictionaryLabel ID="lblStatus" runat="server" DictionaryType="Dic_Status" FieldIDValue='<%# Eval("CourseStatus") %>'></cc1:DictionaryLabel>
                                            </td>
                                        </tr>
                                    </table>
                                    <div id="dv_JobType">
                                        <asp:Label ID="lblCourseID" runat="server" Text='<%# Eval("CourseID") %>' CssClass="hide"></asp:Label>
                                        <asp:DataList ID="dltJobList" runat="server" RepeatColumns="3">
                                            <ItemTemplate>
                                                <div class="dv_job_item">
                                                    <div class="dv_job_item_title">
                                                        <asp:Label ID="lblResourceName" runat="server" Text='<%# Eval("ResourceName")%>'></asp:Label>
                                                        <%--<asp:LinkButton ID="lbtnFunctionUrl"  runat="server" PostBackUrl='<%# Eval("FunctionUrl") %>' Text='<%# Eval("ResourceTotalNum")%>'></asp:LinkButton>--%>
                                                        <span class="colorYellow"><%# Eval("ResourceNum") %></span>&nbsp;/&nbsp;<span class="colorBlue"><%# Eval("ResourceTotalNum")%></span>&nbsp;&nbsp;(启用/总数)                                                        
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

                <div id="divMsg" runat="server" visible="false" style="text-align:center;">
                    没有任何记录！
                </div>
            </div>
            <div class="dv_splitLine">
            </div>
            <!--翻页-->
            <div class="dv_pagePanel">
            </div>
        </div>
    </div>
</asp:Content>
