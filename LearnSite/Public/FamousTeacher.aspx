<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeBehind="FamousTeacher.aspx.cs" Inherits="ETMS.Studying.Public.FamousTeacher" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultPlaceHolder" runat="server">
    <link href="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/default.css" type="text/css" rel="stylesheet" />
    <!--名师简介-->
    <div class="view-area">
        <div class="famousTeacher-container">
            <div class="teacher-list" id="famousteacher_teacherlist">
            </div>
            <img class="no_content hide"  src="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/images/common/no_content.png" />
            <div class="loadMore">加载更多</div>
        </div>
    </div>
    <script id="famousteacher_teacherlist_tmpl" type="text/x-jquery-tmpl">
        {{each(i,Teacher) data}}
         <dl class="teacher-block" id="${Teacher.TeacherID}">
                    <dt><img src="${Teacher.PhotoUrl}"></dt>
                    <dd>
                        <p>${Teacher.RealName}<span>${Teacher.TeacherLevelName}</span></p>
                        <p>${Teacher.TeacherBrief}</p>
                    </dd>
           </dl>
        {{/each}}
    </script>

    <script>
        /*跳转名师详情*/
        $("#famousteacher_teacherlist").on("click", "dl", function () {
            window.location.href = "TeacherInfo.aspx?teacherid=" + $(this).attr("id");
        })
        /*加载更多*/
        var pageIndex = 1;
        var pageSize = 6;
        var totalPage = 1;
        $('.loadMore').click(function () {
            var $this = $(this);
            if (pageIndex <= totalPage) {
                pageIndex++;
                $.ajax({
                    url: AppPath + "/PublicService/Teacher.ashx",
                    type: "get",
                    data: { Method: "famousteacherlist", PageSize: pageSize, PageIndex: pageIndex },
                    dataType: "json",
                    async: false,
                    success: function (result) {
                        totalPage = result.PageTotal;
                        if (pageIndex == totalPage)
                        {
                            $this.html("加载完成");
                        }
                        $(result.data).each(function (index) {
                            var teacherHtml = '<dl class="teacher-block" id="' + result.data[index].TeacherID + '">'
                                            + '<dt><img src="' + result.data[index].PhotoUrl + '"></dt>'
                                            + '<dd>'
                                            + '<p>' + result.data[index].RealName + '<span>' + result.data[index].TeacherLevelName + '</span></p>'
                                            + '<p>' + result.data[index].TeacherBrief + '</p>'
                                            + '</dd></dl>';
                            $("#famousteacher_teacherlist").append(teacherHtml);
                        })
                    }
                });
            }
        });
        //绑定名师风采列表
        function BindTeacherList() {
            $.ajax({
                url: AppPath + "/PublicService/Teacher.ashx",
                type: "get",
                data: { Method: "famousteacherlist", PageSize: pageSize, PageIndex: pageIndex },
                contentType: "application/json",
                dataType: "json",
                async: false,
                success: function (result) {
                    if (result.data.length == 0) {
                        $("#famousteacher_teacherlist").html("");
                        $('.no_content').removeClass('hide');
                        $('.loadMore').hide();
                    } else {
                        $("#famousteacher_teacherlist").html("");
                        $("#famousteacher_teacherlist_tmpl").tmpl(result).appendTo("#famousteacher_teacherlist");
                    }
                    totalPage = result.PageTotal;
                    if (pageIndex == totalPage) {
                        $(".loadMore").html("加载完成");
                    }
                }
            });
        }
        //页面初始化
        $(function () {
            $('.header-menu #menu-teacher').addClass('cur').siblings().removeClass('cur');
            //绑定名师风采列表
            BindTeacherList();
        })
    </script>
</asp:Content>
