<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MReport.master" AutoEventWireup="true"
    CodeFile="OnlineStudyCourseDetail.aspx.cs" Inherits="Reporting_OnlineStudyCourseDetail" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContentPlaceHolder" runat="Server">
    <title>在线学习情况监控</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="dv_HeaderTitle">
        <h2 class="h_titleName1">
            在线学习情况监控</h2>
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
                        课程开始时间：
                    </th>
                    <td>
                        <cc1:DateTimeTextBox ID="ttbCourseBeginTime" runat="server" EndTimeControlID="ttbCourseEndTime"
                            ValidationGroup="Saves"></cc1:DateTimeTextBox>至<cc1:DateTimeTextBox ID="ttbCourseEndTime"
                                runat="server" BeginTimeControlID="ttbCourseBeginTime" ValidationGroup="Saves"></cc1:DateTimeTextBox>
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
                    <td >
                        <asp:TextBox ID="txtRealName" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                    <th>学员状态：</th>
                    <td>
                       <asp:DropDownList ID="ddlUserStatus" runat="server" CssClass="select_120">
                       <asp:ListItem Value="-1" Selected="True">全部</asp:ListItem>
                       <asp:ListItem Value="1">启用</asp:ListItem>
                       <asp:ListItem Value="0">停用</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist" style="width: 98%;">
            <!--翻页-->
            <div class="dv_pagePanel" id="divPage1">
                <div class="dv_selectAll">
                    <%-- <asp:Button ID="btnExport" runat="server" Text="导出" CssClass="btn_Export" OnClick="btnExport_Click"/>--%>
                    <input type="button" class="btn_Export" value="导出" />
                    <label id="lblMsg">
                    </label>
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <div style="overflow-x: auto; overflow-y: hidden">
                <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
                    CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0"
                    DataKeyNames="TrainingItemID" Width="2500px">
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
                        <asp:TemplateField HeaderText="课程标准课时（小时）" HeaderStyle-Width="130">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblItemCourseHours" runat="server" ShowTextNum="6" Text='<%# Eval("ItemCourseHours")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="课程成绩" HeaderStyle-Width="70">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblItemCourseSumGrade" runat="server" ShowTextNum="6" Text='<%# Eval("ItemCourseSumGrade")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="在线测试名称">
                            <ItemTemplate>
                                <%# Eval("ItemCourseTestName").ToString().Replace("#","<br/>") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="在线测试成绩" HeaderStyle-Width="80">
                            <ItemTemplate>
                                <%# Eval("ItemCourseTestScore").ToString().Replace("#","<br/>")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="课件名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblCoursewareName" runat="server" ShowTextNum="6" Text='<%# Eval("CoursewareName")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="课件时长" HeaderStyle-Width="70">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblShowHoures" runat="server" ShowTextNum="6" Text='<%# Eval("ShowHoures")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="课件学习进度%" HeaderStyle-Width="90">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblStudyProcess" runat="server" ShowTextNum="6" Text='<%# Eval("StudyProcess")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="实际在线学习时间（小时）" HeaderStyle-Width="160">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblActualStudyTime" runat="server" ShowTextNum="6" Text='<%# Eval("ActualStudyTime")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="课件完成状态" HeaderStyle-Width="80">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblCompleteStatus" runat="server" ShowTextNum="6" Text='<%# Eval("CompleteStatus")%>'></cc1:ShortTextLabel>
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
        $(document).ready(function () {
            $(".btn_Export").click(function () {
                var scrollHeight = document.documentElement.scrollHeight;
                $(".shadowBj").height(scrollHeight);
                $(".shadowBj").show();
                $(".loadfile").show();

                $.post("ExportFile.ashx", {
                    ExportType: "online",
                    ItemCode: $("#<%=txtItemNameCode.ClientID %>").val(),
                    ItemName: $("#<%=txtItemNameCode.ClientID %>").val(),
                    ItemCourseCode: $("#<%=txtCourseNameCode.ClientID %>").val(),
                    ItemCourseName: $("#<%=txtCourseNameCode.ClientID %>").val(),
                    ItemCourseAttrID: $("#<%=ddlCourseAttrID.ClientID %>").val(),
                    ItemCourseTypeID: $("#<%=ddl_CourseTypeID.ClientID %>").val(),
                    ItemCourseBeginTime: $("#<%=ttbCourseBeginTime.ClientID %>").val(),
                    ItemCourseEndTime: $("#<%=ttbCourseEndTime.ClientID %>").val(),
                    ItemCourseScoreStatus: $("#<%=ddlItemCourseScoreStatus.ClientID %>").val(),
                    OrganizationID: $("#<%=ddl_OrganizationID.ClientID %>").val(),
                    DepartmentID: $("#<%=ddl_DepartmentID.ClientID %>").val(),
                    PostID: $("#<%=ddl_PostID.ClientID %>").val(),
                    PostTypeID: $("#<%=ddlPostType.ClientID %>").val(),
                    RankID: $("#<%=ddlRank.ClientID %>").val(),
                    RealName: $("#<%=txtRealName.ClientID %>").val(),
                    Status: $("#<%=ddlUserStatus.ClientID %>").val(),
                    ItemCourseTeachModelID: -1
                }, function (data) {
                    if (data != "") {
                        //alert(data);
                        $("#lblMsg").html("<a href='" + data + "'>下载文件</a>");
                    }
                    $(".shadowBj").hide();
                    $(".loadfile").hide();
                });
            });
        });
        function hideLodfileDiv() {
            $(".shadowBj").hide();
            $(".loadfile").hide();
        }
    </script>
</asp:Content>
