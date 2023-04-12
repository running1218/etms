<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeBehind="CourseLivingView.aspx.cs" Inherits="ETMS.Studying.Public.CourseLivingView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultPlaceHolder" runat="server">
    <link href="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/default.css" type="text/css" rel="stylesheet" />
    <style>
        .courseView-list li p:hover{
            background:#e8f2fe;
        }
    </style>
    <!--课程大纲-->
    <div class="view-area">
        <div class="courseView-container">
            <div class="myCourse-list courseView-box">
                <dl class="myCourse-block">
                    <dt>
                        <img src="<%= CourseImageUrl %>"></dt>
                    <dd class="myCourse-info">
                        <div class="h1 ellipsis"><%= CourseName %></div>
                        <div>
                            <asp:Repeater ID="CourseTeacherList" runat="server">
                                <ItemTemplate>
                                    <dl>
                                        <%--<dt>
                                            <img src="<%# StaticResourceUtility.GetFullPathByFileType("UserIcon", string.IsNullOrEmpty(Eval("PhotoUrl").ToString()) ? "default.gif" : Eval("PhotoUrl").ToString()) %>""></dt>--%>
                                        <dd class="ellipsis"><%# Eval("RealName") %></dd>
                                    </dl>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <div class="p">
                            学习人数：<span><%= FocusCount %></span>
                        </div>
                    </dd>
                </dl>

                <div class="courseview-nav">
                    <span>课程详情</span>
                    <span class="courseview-nav-cur">课程目录</span>
                </div>

                <div class="content-box">
                    <div class="courseview-content courseView-right">
                        <div class="course-view-teahcerinfo">
                            <h1>--讲师介绍--</h1>
                            <asp:Repeater ID="rptTeacher" runat="server">
                                <ItemTemplate>
                                    <div class="teacher-item-info">
                                       <img id="imgTeacher" src="<%# Eval("PhotoUrl") %>" align="Left" />
                                       <span class="teacher-name"><%# Eval("RealName") %></span>，<%# Eval("TeacherBrief") %>                                        
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <div class="right-block">
                            <h1>--课程介绍--</h1>
                            <p><asp:Literal ID="lblCourseOutline" runat="server" /></p>
                        </div>
                        <div class="right-block hide">
                            <h1>--适用对象--</h1>
                            <p><asp:Literal ID="lblForObject" runat="server" /></p>
                        </div>
                    </div>
                    <div class="courseview-content courseView-left cur-box">
                        <p class="h1"><i class="circleIcon"></i>教学内容</p>
                        <ul class="courseView-list">
                            <asp:Repeater ID="CourseResourceList" runat="server">
                                <ItemTemplate>
                                    <li isopen="<%# Eval("IsOpen") %>">
                                        <i class="circleIcon"></i>
                                        <p>
                                            <span class="ellipsis"><%# Eval("LivingName") %> </span>
                                            <span><%# Eval("TeacherName") %></span>
                                            <span><%# string.Format("{0} {1} - {2}",Eval("Date"), Eval("SHHMM"), Eval("EHHMM")) %> </span>                                        
                                            <span class="content-action"><a onclick="javascript:goLivingRoom('<%# string.Format("{0}/public/LivingGuide.aspx?LivingID={1}", WebUtility.AppPath, Eval("LivingID")) %>')">试学</a></span>
                                        </p>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                            <img class="no_content hide"  src="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/images/common/no_content.png" />
                        </ul>
                    </div>                    
                </div>
            </div>
        </div>
    </div>
    <script>
        $('.header-menu #menu-course').addClass('cur').siblings().removeClass('cur');
        if ($('.courseView-list li').length == 0) {
            $('.no_content').removeClass('hide');
        }
        for (var i = 0; i < $('.courseView-list li').length;i++){
            var isopen = $('.courseView-list li').eq(i).attr('isopen');
            if (isopen != 1) {
                //$('.courseView-list li').eq(i).find('a').css({ 'cursor': 'not-allowed', 'background': '#d7d7d7', 'border': '1px solid #d7d7d7' });
                $('.courseView-list li').eq(i).find('a').css({ 'display': 'none' });
                $('.courseView-list li').eq(i).find('.content-action').addClass('lock');
            }
        }

        $(".courseview-nav span").on("click", function () {
            var index = $(this).index();
            $(this).addClass("courseview-nav-cur").siblings("span").removeClass("courseview-nav-cur");
            $(".content-box .courseview-content").eq(index).addClass("cur-box").siblings(".courseview-content").removeClass("cur-box");
        })

        function goLivingRoom(url)
        {
            var isAuthenticated = '<%=ETMS.Studying.BaseUtility.IsLogin%>';
            if (isAuthenticated == "True") {
                var tempwindow = window.open('_blank');
                tempwindow.location.href = url;
            }
            else {
                layer.open({
                    type: 2,
                    title: '登录',
                    skin: 'layui-layer-rim',
                    area: ['360px', '325px'],
                    content: AppPath + '/Login2.aspx?callbackJS=LivingOpen',
                    end: function () {
                        window.location.href = window.location.href;
                    }
                });
            }
        }
    </script>
</asp:Content>
