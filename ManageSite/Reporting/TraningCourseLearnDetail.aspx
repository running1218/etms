<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MReport.master" AutoEventWireup="true"
    CodeFile="TraningCourseLearnDetail.aspx.cs" Inherits="Reporting_TraningCourseLearnDetail" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    <title>培训课程学习情况汇总</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="dv_HeaderTitle">
        <h2 class="h_titleName1">
            培训课程学习情况汇总</h2>
    </div>
    <div class="dv_reports">
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
                id="tableQueryControlList">
                <tr>
                    <th style="width: 120px">
                        项目名称（编码）：
                    </th>
                    <td style="width: 300px">
                        <asp:TextBox ID="txtItemNameCode" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                    <th style="width: 120px">
                        课程名称（编码）：
                    </th>
                    <td class="Search_Area">
                        <asp:TextBox ID="txtCourseNameCode" runat="server" CssClass="inputbox_120 floatleft"></asp:TextBox>
                        <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
                        <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
                    </td>
                </tr>
                <tr>
                    <th>
                        课程属性：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="ddlCourseAttrID" DictionaryType="Dic_Sys_CourseAttr"
                            IsShowAll="true" CssClass="select_120" />
                    </td>
                    <th>
                        课程类型：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="ddl_CourseTypeID" DictionaryType="Dic_Sys_CourseType"
                            IsShowAll="true" CssClass="select_120" />
                    </td>
                </tr>
                <tr>
                    <th>
                        授课方式：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="ddl_TeachModelID" DictionaryType="Dic_Sys_TeachModel"
                            IsShowAll="true" CssClass="select_120" />
                    </td>
                    <th>
                        课程成绩：
                    </th>
                    <td>
                        <asp:DropDownList ID="ddlItemCourseScoreStatus" runat="server" CssClass="select_120">
                            <asp:ListItem Value="-2">全部</asp:ListItem>
                            <asp:ListItem Value="-1">未考试</asp:ListItem>
                            <asp:ListItem Value="0">不及格</asp:ListItem>
                            <asp:ListItem Value="1">及格</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>
                        课程开始时间：
                    </th>
                    <td colspan="3">
                        <cc1:DateTimeTextBox ID="ttbCourseBeginTime" runat="server" EndTimeControlID="ttbCourseEndTime"
                            ValidationGroup="Saves"></cc1:DateTimeTextBox>至<cc1:DateTimeTextBox ID="ttbCourseEndTime"
                                runat="server" BeginTimeControlID="ttbCourseBeginTime" ValidationGroup="Saves"></cc1:DateTimeTextBox>
                    </td>
                </tr>
                <tr runat="server" id="trOrg">
                    <th>
                        组织机构：
                    </th>
                    <td colspan="3">
                        <cc1:DictionaryDropDownList runat="server" ID="ddl_OrganizationID" DictionaryType="Dic_CurrentAndSubOrganization"
                            IsShowAll="true" AutoPostBack="True" CssClass="select_390" OnSelectedIndexChanged="ddl_OrganizationID_SelectedIndexChanged" />
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltlDepartment" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>：
                    </th>
                    <td>
                        <asp:DropDownList runat="server" ID="ddl_DepartmentID" CssClass="select_120">
                        </asp:DropDownList>
                    </td>
                    <th>
                        <asp:Literal ID="ltlPosition" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>：
                    </th>
                    <td>
                        <asp:DropDownList runat="server" ID="ddl_PostID" CssClass="select_120">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>类别：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="ddlPostType" DictionaryType="Dic_SYS_PostType"
                            IsShowAll="true" CssClass="select_120" />
                    </td>
                    <th>
                        <asp:Literal ID="ltlRank" runat="server" Text="<%$ Resources:UIResource, ui_rank%>"></asp:Literal>：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="ddlRank" DictionaryType="vw_Dic_Sys_Rank"
                            IsShowAll="true" CssClass="select_120" />
                    </td>
                </tr>
                <tr>
                    <th>
                        学员姓名：
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txtRealName" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist" style="width: 98%;">
            <!--翻页-->
            <div class="dv_pagePanel" id="divPage1">
                <div class="dv_selectAll">
                    <asp:Button ID="btn_Export" runat="server" Text="导出" CssClass="btn_Export" 
                        onclick="btn_Export_Click"/>
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <div style="overflow-x: auto; overflow-y: hidden">
                <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
                    CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0"
                    DataKeyNames="TrainingItemID" Width="3900px">
                    <Columns>
                        <asp:TemplateField HeaderText="组织机构" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblOrganizationName" runat="server" ShowTextNum="6" Text='<%# Eval("OrganizationName")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblDepartmentName" runat="server" ShowTextNum="6" Text='<%# Eval("DepartmentName")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="学员姓名" HeaderStyle-Width="60">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblRealName" runat="server" ShowTextNum="6" Text='<%# Eval("RealName")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="工号" HeaderStyle-Width="70">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblWorkNo" runat="server" ShowTextNum="6" Text='<%# Eval("WorkNo")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_position%>" HeaderStyle-Width="75">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblPostName" runat="server" ShowTextNum="6" Text='<%# Eval("PostName")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="75">
                            <HeaderTemplate>
                                <asp:Literal ID="ltlPosition" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>类别
                            </HeaderTemplate>
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblPostTypeName" runat="server" ShowTextNum="6" Text='<%# Eval("PostTypeName")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_rank%>" HeaderStyle-Width="85">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblRankName" runat="server" ShowTextNum="6" Text='<%# Eval("RankName")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="项目名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblItemName" runat="server" ShowTextNum="6" Text='<%# Eval("ItemName")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="课程编码" HeaderStyle-Width="70">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblItemCourseCode" runat="server" ShowTextNum="6" Text='<%# Eval("ItemCourseCode")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="课程名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblItemCourseName" runat="server" ShowTextNum="6" Text='<%# Eval("ItemCourseName")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="课程类型" HeaderStyle-Width="70">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblItemCourseTypeName" runat="server" ShowTextNum="6" Text='<%# Eval("ItemCourseTypeName")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="课程属性" HeaderStyle-Width="70">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblItemCourseAttrName" runat="server" ShowTextNum="6" Text='<%# Eval("ItemCourseAttrName")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="课程讲师" HeaderStyle-Width="70">
                            <ItemTemplate>
                                <%# Eval("ItemCourseTeacher").ToString().Replace("#","<br/>")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="培训方式" HeaderStyle-Width="70">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblItemCourseTrainingModelName" runat="server" ShowTextNum="6"
                                    Text='<%# Eval("ItemCourseTrainingModelName")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="课程开始时间" HeaderStyle-Width="80">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblItemCourseBeginTime" runat="server" ShowTextNum="6" Text='<%# Eval("ItemCourseBeginTime").ToDate()%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="课程结束时间" HeaderStyle-Width="80">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblItemCourseEndTime" runat="server" ShowTextNum="6" Text='<%# Eval("ItemCourseEndTime").ToDate()%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="授课方式" HeaderStyle-Width="80">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblItemCourseTeachModelName" runat="server" ShowTextNum="6"
                                    Text='<%# Eval("ItemCourseTeachModelName")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="非SCORM课件标准课时（小时）" HeaderStyle-Width="190">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblDisScormStandardTime" runat="server" ShowTextNum="6" Text='<%# Eval("DisScormStandardTime")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SCORM课件标准课时（小时）" HeaderStyle-Width="180">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblScormStandardTime" runat="server" ShowTextNum="6" Text='<%# Eval("ScormStandardTime")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SCORM课件实际学习课时（小时）" HeaderStyle-Width="200">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblScormActualTime" runat="server" ShowTextNum="6" Text='<%# Eval("ScormActualTime") %>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SCORM课件完成率（％）" HeaderStyle-Width="160">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblScormFinishRate" runat="server" ShowTextNum="6" Text='<%# Eval("ScormFinishRate")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="计划面授时长（小时）" HeaderStyle-Width="140">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblFaceTrainingPlanTime" runat="server" ShowTextNum="6" Text='<%# Eval("FaceTrainingPlanTime")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="实际参加面授时长（小时）" HeaderStyle-Width="160">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblFaceTrainingActualTime" runat="server" ShowTextNum="6"
                                    Text='<%# Eval("FaceTrainingActualTime")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="学员面授完成率（％）" HeaderStyle-Width="130">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblFaceTrainingFinishRate" runat="server" ShowTextNum="6"
                                    Text='<%# Eval("FaceTrainingFinishRate")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="可计算总时长（小时）" HeaderStyle-Width="130">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblAvalableTotalStandardTime" runat="server" ShowTextNum="6"
                                    Text='<%# Eval("AvalableTotalStandardTime")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="可计算实际完成时长（小时）" HeaderStyle-Width="170">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblAvalableTotalActualTime" runat="server" ShowTextNum="6"
                                    Text='<%# Eval("AvalableTotalActualTime")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="可计算总完成率（％）" HeaderStyle-Width="130">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblAvalableTotalFinishRate" runat="server" ShowTextNum="6"
                                    Text='<%# Eval("AvalableTotalFinishRate")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="课程完成状态" HeaderStyle-Width="80">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblItemStudentCourseFinishStatus" runat="server" ShowTextNum="6"
                                    Text='<%# Eval("ItemStudentCourseFinishStatus")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="课程成绩" HeaderStyle-Width="60">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblItemCourseSumGrade" runat="server" ShowTextNum="6" Text='<%# Eval("ItemCourseSumGrade")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="在线测试名称">
                            <ItemTemplate>
                                <%# Eval("ItemCourseTestName").ToString().Replace("#","<br/>")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="在线测试成绩" HeaderStyle-Width="80">
                            <ItemTemplate>
                                <%# Eval("ItemCourseTestScore").ToString().Replace("#","<br/>")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </cc1:CustomGridView>
            </div>
            <!--列表 end-->
            <div class="dv_splitLine">
            </div>
            <!--翻页-->
            <div class="dv_pagePanel" id="divPage2">
            </div>
        </div>
        <div class='shadowBj' id="shadowBj" style="display: none;">
        </div>
        <div class="loadfile" id="loadfile" style="display: none;">
            <img src="../App_Themes/ThemeAdmin/Images/waiter.gif" alt="正在执行后台操作，请稍候..." align="absmiddle" />
            正在执行后台操作，请稍候...
        </div>
    </div>
    <script type="text/javascript">
        function hideLodfileDiv() {
            $(".shadowBj").hide();
            $(".loadfile").hide();
        }
    </script>
</asp:Content>
