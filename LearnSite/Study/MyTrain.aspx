<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeBehind="MyTrain.aspx.cs" Inherits="ETMS.Studying.Public.MyTrain" %>

<%@ Import Namespace="ETMS.Utility" %>
<%@ Import Namespace="ETMS.Utility.Service.FileUpload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultPlaceHolder" runat="server">
    <link href="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/myTrain.css" type="text/css" rel="stylesheet" />
    <asp:Panel ID="NoContentPanel" runat="server">
        <img class="no_content" src="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/images/common/no_content.png" />
    </asp:Panel>
    <asp:Panel ID="ContentPanel" runat="server">
        <div class="view-area">
            <div class="iteam">
                <%-- <asp:UpdatePanel runat="server" ID="Up1" UpdateMode="Conditional">
                <ContentTemplate>--%>
                    项目名称：
            <asp:DropDownList ID="ddlItem" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged"></asp:DropDownList>
                <p class="time">
                    起止时间：<span><asp:Literal ID="lit_Start" runat="server"></asp:Literal>
                        至
                        <asp:Literal ID="lit_End" runat="server"></asp:Literal></span>
                    <%--  <input type="text" id="txt1" />--%>
                </p>
                <%--  </ContentTemplate>
            </asp:UpdatePanel>--%>
            </div>
            <div style="border: 1px solid #cbcbcb; margin-top: 10px;">
                <div class="itemoption_check">
                    <ul>
                        <li class="cur">我的学习</li>
                        <li>选修课程库 <span id="cue" class="<%= OptionalCount ==-1?"hide":"" %>">提醒：该项目下您的可选课程数量为<%=OptionalCount %>门, 还可以选课数量<%=OptionalCount - SelectedCount >0? OptionalCount - SelectedCount: 0 %>门</span></li>
                    </ul>
                </div>
                <div class="option_check">
                    <ul>
                        <li class="cur">必修课程</li>
                        <li>自选课程</li>
                    </ul>
                </div>
                <div class="tab_check">
                    <div class="course-list tab">
                     <%--   <asp:UpdatePanel runat="server" ID="Up2" UpdateMode="Conditional">
                            <ContentTemplate>--%>
                                <asp:Repeater ID="CourseList" runat="server" OnItemDataBound="CourseList_ItemDataBound">
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="myCourse_block">
                                            <img class="course_img" src="<%# StaticResourceUtility.GetFullPathByFileType(FileUploadFunctionType.CourseLogo, string.IsNullOrEmpty(Eval("ThumbnailURL").ToString())?"default.jpg":Eval("ThumbnailURL").ToString())%>">
                                            <div class="course_info">
                                                <h1><%# Eval("CourseName") %></h1>
                                                <div class="teacherBox">
                                                    <h2 class="teacherName">讲师：</h2>
                                                    <ul class="teacher">
                                                        <asp:Repeater ID="TeacherList" runat="server">
                                                            <ItemTemplate>
                                                                <li>
                                                                    <p><%# Eval("RealName") %></p>
                                                                </li>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </ul>
                                                </div>
                                                <div class="study_time">
                                                    <p>开课时间：<span><%# Eval("CourseBeginTime").ToDateTime().ToString("yyyy-MM-dd") %> 至 <%# Eval("CourseEndTime").ToDateTime().ToString("yyyy-MM-dd") %></span></p>
                                                    <p>报名人数：<span><%# Eval("FocusCount") %>人</span></p>
                                                    <p>所属项目：<span><%# Eval("ItemName") %></span></p>
                                                </div>
                                                <%--<div class="progress">
                                                    <span class="scaleNum"><%# GetStudyProgress(Eval("TrainingItemCourseID").ToGuid()) %></span>
                                                </div>--%>
                                                <a starttime="<%# Eval("CourseBeginTime").ToDateTime().ToString("yyyy-MM-dd") %>" endtime="<%# Eval("CourseEndTime").ToDateTime().ToString("yyyy-MM-dd") %>" href="../Study/CourseStudy.aspx?TrainingItemCourseID=<%# Eval("TrainingItemCourseID") %>" class="study">学习</a>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                    </FooterTemplate>
                                </asp:Repeater>
                       <%--     </ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </div>
                    <div class="train_content tab" hidden>
         <%--               <asp:UpdatePanel runat="server" ID="Up3" UpdateMode="Conditional">
                            <ContentTemplate>--%>
                                <asp:Repeater ID="CourseListZiXuan" runat="server" OnItemDataBound="CourseList_ItemDataBound">
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="myCourse_block">
                                            <img class="course_img" src="<%# StaticResourceUtility.GetFullPathByFileType(FileUploadFunctionType.CourseLogo, string.IsNullOrEmpty(Eval("ThumbnailURL").ToString())?"default.jpg":Eval("ThumbnailURL").ToString())%>">
                                            <div class="course_info">
                                                <h1><%# Eval("CourseName") %></h1>
                                                <div class="teacherBox">
                                                    <h2 class="teacherName">讲师：</h2>
                                                    <ul class="teacher">
                                                        <asp:Repeater ID="TeacherList" runat="server">
                                                            <ItemTemplate>
                                                                <li>
                                                                    <p><%# Eval("RealName") %></p>
                                                                </li>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </ul>
                                                </div>
                                                <div class="study_time">
                                                    <p>开课时间：<span><%# Eval("CourseBeginTime").ToDateTime().ToString("yyyy-MM-dd") %> 至 <%# Eval("CourseEndTime").ToDateTime().ToString("yyyy-MM-dd") %></span></p>
                                                    <p>报名人数：<span><%# GetStudentCount(Guid.Parse(Eval("TrainingItemCourseID").ToString())) %>人</span></p>
                                                    <p>所属项目：<span><%# Eval("ItemName") %></span></p>
                                                </div>
                                                <div class="progress">
                                                    <span class="scaleNum"><%# GetStudyProgress(Guid.Parse(Eval("TrainingItemCourseID").ToString())) %></span>
                                                </div>
                                                <a starttime="<%# Eval("CourseBeginTime").ToDateTime().ToString("yyyy-MM-dd") %>" endtime="<%# Eval("CourseEndTime").ToDateTime().ToString("yyyy-MM-dd") %>" href="../Study/CourseStudy.aspx?TrainingItemCourseID=<%# Eval("TrainingItemCourseID") %>" class="study">学习</a>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                    </FooterTemplate>
                                </asp:Repeater>
                          <%--  </ContentTemplate>
                        </asp:UpdatePanel> --%>                      
                    </div>
                </div>
                <div class="tab_xuanxiuCheck" hidden>
                  <%--  <asp:UpdatePanel runat="server" ID="UP4" UpdateMode="Conditional">
                        <ContentTemplate>--%>
                            <asp:Repeater ID="CourseListXuanXiu" runat="server" OnItemDataBound="CourseList_ItemDataBound">
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div class="myCourse_block">
                                        <img class="course_img" src="<%# StaticResourceUtility.GetFullPathByFileType(FileUploadFunctionType.CourseLogo, string.IsNullOrEmpty(Eval("ThumbnailURL").ToString())?"default.jpg":Eval("ThumbnailURL").ToString())%>">
                                        <div class="course_info">
                                            <h1><%# Eval("CourseName") %></h1>
                                            <div class="teacherBox">
                                                <h2 class="teacherName">讲师：</h2>
                                                <ul class="teacher">
                                                    <asp:Repeater ID="TeacherList" runat="server">
                                                        <ItemTemplate>
                                                            <li>
                                                                <p><%# Eval("RealName") %></p>
                                                            </li>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ul>
                                            </div>
                                            <div class="study_time">
                                                <p>开课时间：<span><%# Eval("CourseBeginTime").ToDateTime().ToString("yyyy-MM-dd") %> 至 <%# Eval("CourseEndTime").ToDateTime().ToString("yyyy-MM-dd") %></span></p>
                                                <p>报名人数：<span><%# GetStudentCount(Guid.Parse(Eval("TrainingItemCourseID").ToString())) %>人</span></p>
                                            </div>
                                            <a starttime="<%# Eval("CourseBeginTime").ToDateTime().ToString("yyyy-MM-dd") %>" endtime="<%# Eval("CourseEndTime").ToDateTime().ToString("yyyy-MM-dd") %>" href="javascript:void(0);" onclick="UserElective(this,'<%# Eval("TrainingItemID") %>','<%# Eval("TrainingItemCourseID") %>','<%# Eval("StudentSignupID") %>')" class="study <%# GetElectiveStatus(Eval("StudentCourse").ToString())%>">选修</a>
                                        </div>
                                    </div>
                                </ItemTemplate>
                                <FooterTemplate>
                                </FooterTemplate>
                            </asp:Repeater>
                      <%--  </ContentTemplate>
                    </asp:UpdatePanel>--%>
                </div>
            </div>
        </div>
        <script>
            function UserElective(obj, TrainingItemID, TrainingItemCourseID, StudentSignupID) {
                if (!$(obj).hasClass("prohibit")) {
                    $.ajax({
                        url: AppPath + "/Study/CourseStudyHandler.ashx",
                        type: "get",
                        data: { Method: "addCourse", "TrainingItemCourseID": TrainingItemCourseID, "SignupID": StudentSignupID },
                        contentType: "application/json",
                        dataType: "json",
                        async: false,
                        success: function (data) {
                            if (data.Status) {
                                layer.alert('选课成功', { icon: 6 }, function () {
                                    $(obj).addClass('prohibit');
                                    layer.closeAll();
                                    location.href = AppPath + '/Study/MyTrain.aspx?TrainingItemID=' + TrainingItemID + "&show=zixuan";
                                })
                            } else {
                                layer.alert(data.Message);
                            }
                        }
                    });
                }
            }
            $(function () {
                $('.header-menu a').eq(5).addClass('cur').siblings().removeClass('cur');

                $('.itemoption_check li').click(function () {
                    var index = $(this).index();
                    $(this).addClass('cur').siblings().removeClass('cur');
                    if (index == 1) {
                        $("#cue").show();
                        $('.option_check').hide();
                        $('.tab_check').hide();
                        $('.tab_xuanxiuCheck').show();
                    }
                    else {
                        $("#cue").hide();
                        $('.tab_xuanxiuCheck').hide();
                        $('.option_check').show();
                        $('.tab_check').show();
                        //('.tab_check .tab').eq(0).show().siblings().hide();
                    }
                })
                $('.option_check li').click(function () {
                    var index = $(this).index();
                    $(this).addClass('cur').siblings().removeClass('cur');
                    $('.tab_check .tab').eq(index).show().siblings().hide();
                })

                var showtab = GetQueryString("show");
                if (showtab == "zixuan") {
                    $(".option_check li").eq(1).addClass('cur').siblings().removeClass('cur');
                    $('.tab_check .tab').eq(1).show().siblings().hide();
                }               
            })

            var doumentHeight = $(window).height();
            $('.content-container').css({ 'min-height': doumentHeight - 242 + 'px' });
            $('.study').mouseover(function () {
                var startTime = $(this).attr('startTime');
                var endTime = $(this).attr('endTime');
                var nowDate = formatDate(new Date(), "yyyy-MM-dd");
                if (nowDate < startTime) {
                    $(this).attr('title', '课程还没有开始哦');
                    $(this).attr('href', '#');
                }
                if (nowDate > endTime) {
                    $(this).attr('title', '课程已结束');
                    $(this).attr('href', '#');
                }
            })
            for (var i = 0; i < $('.study').length; i++) {
                var startTime = $('.study').eq(i).attr('startTime');
                var endTime = $('.study').eq(i).attr('endTime');
                var nowDate = formatDate(new Date(), "yyyy-MM-dd");
                if (nowDate >= startTime && nowDate <= endTime) {
                    $('.study').eq(i).addClass('allow');
                } else {
                    $('.study').eq(i).addClass('prohibit');
                }
            }
        </script>
    </asp:Panel>
</asp:Content>
