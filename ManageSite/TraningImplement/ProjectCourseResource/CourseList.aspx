<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="CourseList.aspx.cs" Inherits="TraningImplement_ProjectCourseResource_CourseList" %>

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
                        <asp:DropDownList ID="ddl_Item" runat="server" CssClass="easyui-combobox">
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
            <div class="dv_pagePanel" id="divPage1" style="margin:0px">
                <div class="dv_selectAll">
                    <asp:Button ID="btnAdd" runat="server" CssClass="btn_Add" Text="新增" ToolTip="新增课程"/>
                    <input id="btnSort" type="button" class="btn_Sort" value="排序" title="课程排序" onclick="javascript:showWindow('项目课程排序','<%= SortUrl %>    ',500,450)" />
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <div id="dourse-content">
                <asp:Repeater ID="rptCourseList" runat="server" OnItemDataBound="rptCourseList_OnItemDataBound" OnItemCommand="rptCourseList_ItemCommand">
                    <HeaderTemplate>
                      <div class="dv_splitLine" style="margin:5px 0px"></div>
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
                                            <th class="courseName notline" style="width:70px;" >
                                                课程编号：
                                            </th>
                                            <td class="courseName notline" style="width:70px;">
                                                <%# Eval("CourseCode")%>
                                            </td>
                                            <th class="courseName notline" style="width:60px;text-align:left;" >
                                                课程名称：
                                            </th>
                                            <td class="courseName alignleft notline" >
                                                <cc1:ShortTextLabel ID="lblItemName" runat="server" ShowTextNum="100" Text='<%# Eval("CourseName")%>'></cc1:ShortTextLabel>
                                            </td>                                            
                                            <th class="courseName notline" style="width:60px;text-align:left;" >
                                                课程属性：
                                            </th>
                                            <td class="courseName alignleft notline" style="width:60px;" >
                                                <cc1:DictionaryLabel ID="DictionaryCourseAttr" DictionaryType="Dic_Sys_CourseAttr" FieldIDValue='<%# Eval("CourseAttrID") %>' runat="server" />
                                            </td>
                                            <th class="courseName notline" style="width:60px;" >
                                                状态：
                                            </th>
                                            <td class="courseName notline" style="width:180px;">
                                                <cc1:DictionaryLabel ID="lblStatus" runat="server" DictionaryType="Dic_Status" FieldIDValue='<%# Eval("CourseStatus") %>'></cc1:DictionaryLabel><asp:HiddenField
                                                    ID="hfTrainingItemCourseID" runat="server" Value='<%# Eval("TrainingItemCourseID") %>' />
                                                <asp:HiddenField ID="hfCourseID" runat="server" Value='<%# Eval("CourseID") %>' />
                                            </td>
                                            <th class="courseName notline" style="width:120px;" >
                                                <asp:LinkButton ID="lbnAnalysis" runat="server" CommandName="Analysis">课程实施进度</asp:LinkButton>
                                            </th>
                                        </tr>
                                        <tr>
                                            <th class="courseName" style="width:70px;" >
                                                课程类型：
                                            </th>
                                            <td class="courseName" style="width:70px;">
                                                <cc1:DictionaryLabel ID="DictionaryLabel0" DictionaryType="Dic_Sys_CourseType" FieldIDValue='<%# Eval("CourseTypeID") %>' runat="server" />
                                            </td>
                                            <th class="courseName" style="width:60px;text-align:left;" >
                                                课程周期：
                                            </th>
                                            <td class="courseName alignleft" >
                                                <%# Eval("CourseBeginTime").ToDate()%>至<%# Eval("CourseEndTime").ToDate()%></td>                                            
                                            <th class="courseName" style="width:60px;text-align:left;" >
                                                授课方式：
                                            </th>
                                            <td class="courseName alignleft" style="width:60px;" >
                                                <cc1:DictionaryLabel ID="DictionaryLabel2" DictionaryType="Dic_Sys_TeachModel" FieldIDValue='<%# Eval("TeachModelID") %>' runat="server" />
                                            </td>
                                            <th class="courseName" style="width:60px;" >
                                                课程学时：
                                            </th>
                                            <td class="courseName" style="width:60px;">
                                                 <%# Eval("CourseHours")%>
                                            </td>
                                            <th class="courseName" style="width:120px;" >
                                                <asp:LinkButton ID="lbtn_SetTeacher" runat="server" CommandName="SetTeacher">讲师</asp:LinkButton>&nbsp;&nbsp;
                                                <asp:LinkButton ID="Lbtn_Edit" runat="server" CommandName="Edits">编辑</asp:LinkButton>&nbsp;&nbsp;
                                                <cc1:CustomLinkButton runat="server" ID="Lbtn_Del" Text="删除" CommandName="Dels" CommandArgument='<%# Eval("TrainingItemCourseID") %>' EnableConfirm="true"
                        ConfirmTitle="提示" ConfirmMessage="确定删除吗？" />
                                            </th>
                                        </tr>
                                    </table>
                                    <div id="dv_JobType">
                                        <asp:Label ID="lblCourseID" runat="server" Text='<%# Eval("CourseID") %>' CssClass="hide"></asp:Label>
                                        <asp:DataList ID="dltJobList" runat="server" RepeatDirection="Horizontal" RepeatColumns="5">
                                            <ItemTemplate>
                                                <div class="dv_job_item">
                                                    <div class="dv_job_item_title">
                                                        <asp:Label ID="lblResourceName" runat="server" Text='<%# Eval("ResourceName")%>'></asp:Label>
                                                        <asp:LinkButton ID="lbtnFunctionUrl" runat="server" PostBackUrl='<%# Eval("FunctionUrl") %>'
                                                            Text='<%# Eval("ItemResourceNum")%>' CssClass="dv_job_item_ResourceNum"></asp:LinkButton><%# Eval("ResourceNum").ToString() == "-1" ? "" : "/"+Eval("ResourceNum")%></div>
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
    <script type="text/javascript">
        //for(var i=0;i<$('#dourse-content .dv_courseList').length;i++){
        //    $('#dourse-content .dv_courseList').eq(i).find('table tr td:nth-child(3)').hide();
        //}
    </script>
</asp:Content>
