<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeBehind="LiveCourse.aspx.cs" Inherits="ETMS.Studying.Public.LiveCourse" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultPlaceHolder" runat="server">
    <link href="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/liveCourse.css" type="text/css" rel="stylesheet" />
    <div class="view-area">
        <div class="now_video">
            <p class="title">正在直播</p>
             <div class="course_list">
                <ul class="list-block cur-block" id="nowLiving">
                    
                </ul>
                <div id="loadNowMore" class="loadMore">加载更多</div>
                 <img id="now-living-no" class="no_content hide" style="padding:5px 0px;" src="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/images/common/no_content.png" />
            </div>
        </div>
        <div class="liveing_video">
            <p class="title">直播预告</p>
             <div class="course_list">
                <ul class="list-block cur-block" id="validLiving">
                    
                </ul>
                <div id="loadMore" class="loadMore">加载更多</div>
                 <img id="valid-living-no" class="no_content hide" style="padding:5px 0px;" src="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/images/common/no_content.png" />
            </div>
        </div>
        <%-- 历史直播 --%>
         <div class="history_video">
            <p class="title">精彩回放</p>
             <div class="course_list">
                <ul class="list-block cur-block" id="historyLiving">
                    
                </ul>
                <div id="loadHistoryMore" class="loadMore">加载更多</div>
                <img id="history-living-no" class="no_content hide" src="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/images/common/no_content.png" />
            </div>
        </div>
    </div>
    <script>
        $('.header-menu a').eq(2).addClass('cur').siblings().removeClass('cur');
        $('.list-sort span').click(function () {
            $(this).addClass('cur').siblings().removeClass('cur');
            $(this).find('i').toggleClass('triangleCur');
        })
    </script>
    <script id="living_now_tmpl" type="text/x-jquery-tmpl">
        {{each(i,row) data}}
            <li onclick="validClick('${row.LivingID}')">
                <img src="<%=ETMS.Utility.WebUtility.FileUrlRoot %>/CourseLogo/${row.ThumbnailURL}">
                <p class="living-time">${row.Date} ${row.SHHMM}-${row.EHHMM}</p>
                <div class="course_info">
                    <p class="course_name" title="${row.LivingName}">${row.LivingName}</p>
                    <p class="teacher_name">${row.TeacherName}<span>主讲</span></p>
                </div>
            </li>
        {{/each}}
    </script>
    <script id="living_valid_tmpl" type="text/x-jquery-tmpl">
        {{each(i,row) data}}
            <li onclick="validClick('${row.LivingID}')">
                <img src="<%=ETMS.Utility.WebUtility.FileUrlRoot %>/CourseLogo/${row.ThumbnailURL}">
                <p class="living-time">${row.Date} ${row.SHHMM}-${row.EHHMM}</p>
                <div class="course_info">
                    <p class="course_name" title="${row.LivingName}">${row.LivingName}</p>
                    <p class="teacher_name">${row.TeacherName}<span>主讲</span></p>
                </div>
            </li>
        {{/each}}
    </script>
    <script id="living_history_tmpl" type="text/x-jquery-tmpl">
        {{each(i,row) data}}
            <li onclick="historyClick('${row.LivingID}')">
                <img src="<%=ETMS.Utility.WebUtility.FileUrlRoot %>/CourseLogo/${row.ThumbnailURL}">
                <p class="living-time">${row.Date} ${row.SHHMM}-${row.EHHMM}</p>
                <div class="course_info">
                    <p class="course_name" title="${row.LivingName}">${row.LivingName}</p>
                    <p class="teacher_name">${row.TeacherName}<span>主讲</span></p>
                </div>
            </li>
        {{/each}}
    </script>
    <script lang="javascript" type="text/javascript">    
        var pageIndex = 1;
        var pageSize = 20;
        var totalPage = 1;

        //加载正在直播列表
        function BindNowLivingList() {
            pageIndex = 1;
            $.ajax({
                url: AppPath + "/PublicService/LivingHandler.ashx",
                type: "get",
                data: { Method: "nowValidliving", PageSize: pageSize, PageIndex: pageIndex },
                contentType: "application/json",
                dataType: "json",
                async: false,
                success: function (result) {
                    debugger;
                    if (result.data.length == 0) {
                        $("#nowLiving").html("");
                        $('#loadNowMore').hide();
                    } else {
                        $('#now-living-no').addClass('hide');
                        $("#nowLiving").html("");
                        $("#living_now_tmpl").tmpl(result).appendTo("#nowLiving");
                    }
                    totalPage = result.PageTotal;
                    if (pageIndex == totalPage) {
                        $("#loadNowMore").hide();
                    }

                    if (result.total == 0) {
                        $('#now-living-no').removeClass('hide');
                    }
                }
            });
        }

        //加载直播列表
        function BindLivingList()
        {
            pageIndex = 1;
            $.ajax({
                url: AppPath + "/PublicService/LivingHandler.ashx",
                type: "get",
                data: { Method: "validliving", PageSize: pageSize, PageIndex: pageIndex},
                contentType: "application/json",
                dataType: "json",
                async: false,
                success: function (result) {
                    if (result.data.length == 0) {
                        $("#validliving").html("");
                        $('#loadMore').hide();
                    } else {
                        $('#valid-living-no').addClass('hide');
                        $("#validLiving").html("");
                        $("#living_valid_tmpl").tmpl(result).appendTo("#validLiving");
                    }
                    totalPage = result.PageTotal;
                    if (pageIndex == totalPage) {
                        $("#loadMore").hide();
                    }

                    if (result.total == 0)
                    {
                        $('#valid-living-no').removeClass('hide');
                    }
                }
            });
        }

        //进入直播点击事件
        function validClick(livingID)
        {
            var isAuthenticated = '<%=ETMS.Studying.BaseUtility.IsLogin%>';
            if (isAuthenticated == 'True') {
                userID = '<%=University.Mooc.AppContext.UserContext.Current.LoginName%>';
                nikeName = '<%=University.Mooc.AppContext.UserContext.Current.RealName%>';
                if (nikeName == '') { nikeName = userID };
                enterLiving(livingID, userID, nikeName);
            }
            else {
                layer.open({
                    title:'进入直播',
                    type: 1,
                    skin: 'layui-layer-demo', //样式类名
                    closeBtn: 1, //不显示关闭按钮
                    anim: 2,
                    shadeClose: true, //开启遮罩关闭
                    content: '<div class="living-login-box">'
                            +'<p class="living-name-box"><input class="living-nikename" type="text" id="txtNikeName" placeholder="请输入昵称" /><span class="living-inqueired hide">*</span></p>'
                            +'<p><input class="living-enter" type="button" id="btnEnter" onclick="return guessEnter(\'' + livingID +'\')" value="确定"/>'
                            + '<input class="living-cancel" type="button" id="btnCancel" onclick="layer.closeAll();" value="取消"/>'
                            +'</p></div>'
                });
            }
        }

        //游客进入直播
        function guessEnter(livingID)
        {
            var userID = '<%=Guid.NewGuid()%>';
            var nikeName = $('#txtNikeName').val();

            if ($('#txtNikeName').val().trim() == '')
            {
                $('.living-inqueired').removeClass('hide');
                return false;
            }
            enterLiving(livingID, userID, nikeName);
            layer.closeAll();
        }

        //获取直播信息，并进入直播页面
        function enterLiving(livingID,userID, nikeName)
        {             
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

        $('#loadMore').click(function () {
            var $this = $(this);
            pageIndex++;
            if (pageIndex <= totalPage) {
                $.ajax({
                    url: AppPath + "/PublicService/LivingHandler.ashx",
                    type: "get",
                    data: { Method: "validliving", PageSize: pageSize, PageIndex: pageIndex },
                    dataType: "json",
                    async: false,
                    success: function (result) {
                        totalPage = result.PageTotal;
                        if (pageIndex == totalPage) {
                            $this.hide();
                        }
                        $(result.data).each(function (index, row) {
                                var content ='<li onclick="validClick(\'' + row.LivingID + '\')">'
                                    + '<img src="<%=ETMS.Utility.WebUtility.FileUrlRoot %>/CourseLogo/' + row.ThumbnailURL + '\">'
                                    + '<p class="living-time">' + row.Date + ' ' + row.SHHMM + '-' + row.EHHMM + '</p>'
                                    + '<div class="course_info">'
                                    + '<p class="course_name" title="' + row.LivingName + '">' + row.LivingName + '</p>'
                                    + '<p class="teacher_name">' + row.TeacherName + '<span>主讲</span></p>'
                                    + '</div>'
                                    + '</li>'
                            $("#validLiving").append(content);
                        })
                    }
                });
            }
        });

        var HpageIndex = 1;
        var HtotalPage = 1;
        //加载直播历史列表
        function BindHistoryLivingList() {
            HpageIndex = 1;
            $.ajax({
                url: AppPath + "/PublicService/LivingHandler.ashx",
                type: "get",
                data: { Method: "historyliving", PageSize: pageSize, PageIndex: HpageIndex },
                contentType: "application/json",
                dataType: "json",
                async: false,
                success: function (result) {
                    if (result.data.length == 0) {
                        $("#historyLiving").html("");
                        $('#loadHistoryMore').hide();
                    } else {
                        $("#historyLiving").html("");
                        $("#living_history_tmpl").tmpl(result).appendTo("#historyLiving");
                    }
                    HtotalPage = result.PageTotal;
                    if (HpageIndex == HtotalPage) {
                        $("#loadHistoryMore").hide();
                    }

                    if (result.total == 0) {
                        $('#history-living-no').removeClass('hide');
                    }
                }
            });
        }

        function historyClick(livingID)
        {
            var userID = '';
            var nikeName = '';
            var isAuthenticated = '<%=ETMS.Studying.BaseUtility.IsLogin%>';
            if (isAuthenticated == 'True') {
                userID = '<%=University.Mooc.AppContext.UserContext.Current.LoginName%>';
                nikeName = '<%=University.Mooc.AppContext.UserContext.Current.RealName%>';
                if (nikeName == '') { nikeName = userID };
            }
            else {
                userID = '<%=Guid.NewGuid()%>';
                nikeName = 'guest';
            }

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

        $('#loadHistoryMore').click(function () {
            var $this = $(this);
            HpageIndex++;
            if (HpageIndex <= HtotalPage) {
                $.ajax({
                    url: AppPath + "/PublicService/LivingHandler.ashx",
                    type: "get",
                    data: { Method: "historyliving", PageSize: pageSize, PageIndex: HpageIndex },
                    dataType: "json",
                    async: false,
                    success: function (result) {
                        HtotalPage = result.PageTotal;
                        if (HpageIndex == HtotalPage) {
                            $this.hide();
                        }
                        $(result.data).each(function (index, row) {
                            var content = '<li onclick="historyClick(\'' + row.LivingID + '\')">'
                                    + '<img src="<%=ETMS.Utility.WebUtility.FileUrlRoot %>/CourseLogo/' + row.ThumbnailURL + '\">'  
                                    + '<p class="living-time">' + row.Date + ' ' + row.SHHMM + '-' + row.EHHMM + '</p>'
                                    + '<div class="course_info">'
                                    + '<p class="course_name" title="' + row.LivingName + '">' + row.LivingName + '</p>'
                                    + '<p class="teacher_name">' + row.TeacherName + '<span>主讲</span></p>'
                                    + '</div>'
                                    + '</li>'
                            $("#historyLiving").append(content);
                        })
                    }
                });
            }
        });

        $(function () {
            BindNowLivingList();
            BindLivingList();
            BindHistoryLivingList();
        });
    </script>
    <script lang="javascript" type="text/javascript" src='<%=ETMS.Utility.WebUtility.AppPath %>/Scripts/library/layer/layer-2.4.min.js'></script>
</asp:Content>
