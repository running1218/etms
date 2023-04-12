<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeBehind="CourseCenter.aspx.cs" Inherits="ETMS.Studying.Public.CourseCenter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultPlaceHolder" runat="server">
    <link href="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/default.css" type="text/css" rel="stylesheet" />

    <!--点播课程-->
    <div class="view-area">
        <div class="onCourse-container">
            <dl class="course-search">
                <dt>
                    <input type="text" class="searchtext" value=""/>
                    <input type="button" class="searchbtn" value="" />
                </dt>
            </dl>

            <dl class="course-type">
                <dt>
                    <div style="margin: 40px auto; width: 20px; line-height: 20px;">全部分类</div>
                </dt>
                <dd id="courseClassSpan"></dd>
                <dd style="height: 1px; background: #efefef;"></dd>
                <dd id="courseTypeSpan" style="border-top: 0px solid #efefef;">
                    <span id="0" class="curType">全部</span>
                    <span id='1'>点播课</span>
                    <span id='2'>直播课</span>
                </dd>
            </dl>
            <div class="list-sort">
               <%-- <span class="cur" style="cursor: pointer;">学习人数
                    <em class="triangle">
                        <!--默认关注人数多的显示在上面-->
                        <i class="triangleTop triangleCur" id="focusdesc"></i>
                        <i class="triangleBottom" id="focusasc"></i>
                    </em>
                </span>--%>
                <%--<span>开课时间
                    <em class="triangle">
                        <i class="triangleTop"></i>
                        <i class="triangleBottom triangleCur"></i>
                    </em>
                </span>--%>
            </div>
            <div class="course-list">
                <div class="list-block cur-block" id="coursecenter_courselist">
                </div>
                <div class="loadMore">加载更多</div>
                <img class="no_content hide" src="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/images/common/no_content.png" />
            </div>
        </div>
    </div>
    <script id="coursecenter_courselist_tmpl" type="text/x-jquery-tmpl">
        {{each(i,Course) data}}
            <dl id="${Course.CourseID}" data-moudle="${Course.IsLiving}" data-status="${Course.LivingStatus}">
                <dt style="position: relative;">
                    <img src="${Course.ThumbnailURL}">
                    {{if Course.IsLiving==1}}
                    <div class="zbline">
                        <div class="zbleft">${Course.TimeString}</div>
                        {{if Course.LivingStatus==1}}<div class="zb">正在直播</div>
                        {{else Course.LivingStatus==2}}<div class="zb">直播预告</div>
                        {{else Course.LivingStatus==3}}<div class="zb">精彩回放</div>
                        {{/if}}
                    </div>
                    {{/if}}
                </dt>
                <dd>
                    <p class="h1 ellipsis">${Course.CourseName}</p>
                    <p><i>${Course.TeacherNameLimit}</i></p>
                    <p>
                        <i>${Course.CourseHours}</i>课时
                                    <span><i>${Course.FocusCount}</i>学习</span>
                    </p>
                </dd>
            </dl>
        {{/each}}
    </script>

    <script>
        //检索关键字
        var searchContent = unescape(GetQueryString("searchCont"));
        if (searchContent != "null") {
            $(".searchtext").val(searchContent);
        }

        $("#courseClassSpan").append('<span id="0" class="curType">全部</span>');
        //$("#courseTypeSpan").append('<span id="0" class="curType">全部</span>');
        /*点播课程-选择分类*/
        $("#courseClassSpan").on("click", "span", function () {
            $(this).addClass("curType").siblings("span").removeClass("curType");
            pageIndex = 1;
            BindCourseList();

        })
        /*点播课程-选择分类*/
        $("#courseTypeSpan").on("click", "span", function () {
            $(this).addClass("curType").siblings("span").removeClass("curType");
            pageIndex = 1;
            BindCourseList();

        })
        /*排序*/
        $(".list-sort span").on("click", function () {
            $(this).find('i').eq(0).toggleClass("triangleCur");
            $(this).find('i').eq(1).toggleClass("triangleCur");
            BindCourseList();
        })
        
        function LivingOpen(userID, nikeName) {
            if (nikeName == '') { nikeName = userID };
            //enterLiving(liveID, userID, nikeName);
            //window.location.href = window.location.href;
            liveUserID = userID;
            liveNikeName = nikeName;
        }
        var liveID = "";
        var liveUserID = "";
        var liveNikeName = "";
        var livingStatus = "";
        /*点播课程点击跳转*/
        $("#coursecenter_courselist").on("click", "dl", function () {
            liveID = $(this).attr("id");
            livingStatus = $(this).data("status");
            if ($(this).data("moudle") == 1) {
                window.location.href = AppPath +"/Public/CourseLivingView.aspx?courseid=" + $(this).attr("id");
                <%--var isAuthenticated = '<%=ETMS.Studying.BaseUtility.IsLogin%>';

                if (isAuthenticated == "True") {
                    userID = '<%=University.Mooc.AppContext.UserContext.Current.LoginName%>';
                    nikeName = '<%=University.Mooc.AppContext.UserContext.Current.RealName%>';
                    if (nikeName == '') { nikeName = userID };
                    enterLiving(liveID, userID, nikeName, livingStatus);
                } else {
                    var root = '<%=ETMS.Utility.WebUtility.AppPath%>';
                    layer.open({
                        type: 2,
                        title: '登录',
                        skin: 'layui-layer-rim',
                        area: ['360px', '325px'],
                        content: root + '/Login2.aspx?callbackJS=LivingOpen&LivingStatus=' + livingStatus + '&objId=' + liveID,
                        end: function () {
                            //enterLiving(liveID, liveUserID, liveNikeName);
                            window.location.href = window.location.href;
                        }
                    });
                }--%>
            } else {
                window.location.href = "CourseView.aspx?courseid=" + $(this).attr("id");
            }

        })
        $(".searchbtn").click(function () {
            pageIndex = 1;
            BindCourseList();
        })
        $(".searchtext").keydown(function (e) {
            if (e.keyCode == 13) {
                pageIndex = 1;
                BindCourseList();
                return false;
            }
        });
        /*加载更多*/
        var pageIndex = 1;
        var pageSize = 20;
        var totalPage = 1;
        $('.loadMore').click(function () {
            var $this = $(this);
            if (pageIndex <= totalPage) {
                pageIndex++;
                BindCourseList();                
            }
        });
        //绑定课程分类
        function BindCourseClass() {
            $.ajax({
                url: AppPath + "/PublicService/Course.ashx",
                type: "get",
                data: { Method: "coursetype" },
                contentType: "application/json",
                dataType: "json",
                async: false,
                success: function (result) {
                    $(result).each(function (index) {
                        var courseTypeHtml;
                        if (index == 0) {
                            courseTypeHtml = "<span id='" + result[index].CourseTypeID + "' >" + result[index].CourseTypeName + "</span>";
                        }
                        else {
                            courseTypeHtml = "/<span id='" + result[index].CourseTypeID + "' >" + result[index].CourseTypeName + "</span>";
                        }
                        $("#courseClassSpan").append(courseTypeHtml);
                    });
                }
            });
        }
        //绑定点播课程列表
        function BindCourseList() {
            //pageIndex = 1;
            var focusExp = "";
            var sortExpression = "DESC";
            if ($(".list-sort .triangle i").hasClass("triangleCur")) {
                focusExp = $(".triangleCur").attr("id");
            }
            if (focusExp == "focusasc") {
                sortExpression = "ASC";
            }

            var courseClassID = $("#courseClassSpan span[class='curType']").attr("id");
            var classID = 0;
            if (courseClassID != undefined && courseClassID > 0) {
                classID = courseClassID;
            }
            var courseTypeID = $("#courseTypeSpan span[class='curType']").attr("id");
            var typeID = 0;
            if (courseTypeID != undefined && courseTypeID > 0) {
                typeID = courseTypeID;
            }
            var key = unescape($(".searchtext").val() == "搜索课程" ? "" : $(".searchtext").val());

            $.ajax({
                url: AppPath + "/PublicService/Course.ashx",
                type: "get",
                data: { Method: "demandcourselist", PageSize: pageSize, PageIndex: pageIndex, ClassID: classID, TypeID: typeID, SearchKey: key, SortExpression: sortExpression },
                contentType: "application/json",
                dataType: "json",
                async: false,
                success: function (result) {
                    if (pageIndex == 1) {
                        $("#coursecenter_courselist").html("");
                    }
                    //console.log(result);
                    if (result.data.length == 0) {
                        $('.loadMore').hide();
                    } else {
                        $('.no_content').addClass('hide');
                        $("#coursecenter_courselist_tmpl").tmpl(result).appendTo("#coursecenter_courselist");
                    }
                    totalPage = result.PageTotal;
                    if (pageIndex == totalPage) {
                        $(".loadMore").html("加载完成");
                    }
                    else {
                        $(".loadMore").html("加载更多");
                    }
                }
            });
        }

        function enterLiving(livingID, userID, nikeName, livingStatus) {
            if (livingStatus == 3) {
                tohistory(livingID, userID, nikeName);
            }
            else {
                validLiving(livingID, userID, nikeName);
            }
        }

        //获取直播信息，并进入直播页面
        function validLiving(livingID, userID, nikeName) {
            $.ajax({
                url: AppPath + "/PublicService/LivingHandler.ashx",
                type: "get",
                data: { Method: "getlivinginfo", LivingID: livingID, UserID: userID, NikeName: nikeName },
                contentType: "application/json",
                dataType: "json",
                async: false,
                success: function (result) {
                    if (result.code == 0) {
                        var url = result.data.liveUrl;
                        window.open(url, '_blank');
                    }
                }
            });
        }
        function tohistory(livingID, userID, nikeName) {
            $.ajax({
                url: AppPath + "/PublicService/LivingHandler.ashx",
                type: "get",
                data: { Method: "getplaybackinfo", LivingID: livingID, UserID: userID, NikeName: nikeName },
                contentType: "application/json",
                dataType: "json",
                async: false,
                success: function (result) {
                    if (result.code == 0) {
                        var url = result.data.playbackUrl;
                        window.open(url, '_blank');
                    }
                    else {
                        layer.alert(result.msg);
                    }
                }
            });
        }
        //初始化页面
        $(function () {
            $('.header-menu #menu-course').addClass('cur').siblings().removeClass('cur');
            //课程分类
            BindCourseClass();
            //绑定点播课程列表
            BindCourseList();
        });
    </script>
</asp:Content>
