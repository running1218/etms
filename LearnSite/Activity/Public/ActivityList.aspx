<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Activity.master" AutoEventWireup="true" CodeBehind="ActivityList.aspx.cs" Inherits="ETMS.Studying.Activity.Public.ActivityList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultPlaceHolder" runat="server">
    <link href="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/default.css" type="text/css" rel="stylesheet" />
    <link href="<%= ETMS.Utility.WebUtility.AppPath %>/styles/activityList.css" rel="stylesheet" />
    <div class="activity-box">
        <div class="activity-list-box"></div>
        <img class="no_content hide"  src="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/images/common/no_content.png" />
        <div class="loadMore">加载更多</div>
    </div>
    <script lang="javascript">
        var pageIndex = 1;
        var pageSize = 12;
        var totalPage = 1;

        $(function () {
            loadData();
            $('.activity-list-box').on('click', 'img', function () {
                var webRoot = '<%=ETMS.Utility.WebUtility.AppPath%>';
                var url = webRoot + '/activity/activity.aspx?id=' + $(this).attr("id");
                window.open(url, '_blank');
            });
        });

        function loadData()
        {
            if (pageIndex > totalPage) {
                return;
            }
            else {
                $.ajax({
                    url: AppPath + "/PublicService/Appraisal.ashx",
                    type: "get",
                    data: { Method: "getappraisallist", PageSize: pageSize, PageIndex: pageIndex },
                    dataType: "json",
                    async: false,
                    success: function (result) {
                        totalPage = result.Data.PageTotal;
                        if (result.Data.Result.length == 0 && pageIndex == 1) {
                            $(".activity-list-box").html("");
                            $('.no_content').removeClass('hide');
                            $('.loadMore').hide();
                        }
                        else {
                            if (pageIndex == totalPage) {
                                $(".loadMore").html("加载完成");
                            }
                        }
                        $(result.Data.Result).each(function (index) {
                            var html = '<div class="activity-block">'
                                        + '<div class="activity-image">'
                                        + '<img id="' + result.Data.Result[index].AppraisalID + '" src="' + result.Data.Result[index].ImageUrl + '" />'
                                        + '</div>'
                                        + '<div class="activity-title">'
                                        + '<span title="' + result.Data.Result[index].AppraisalTitle + '">' + result.Data.Result[index].AppraisalTitle + '</span>'
                                        + '</div>'
                                        + '<div class="activity-time">'
                                        + '<span>' + result.Data.Result[index].BeginTime + '~' + result.Data.Result[index].EndTime + '</span>'
                                        + '</div>'
                                        + '</div>';
                            $(".activity-list-box").append(html);
                        })
                    }
                });
            }
        }

        $('.loadMore').click(function () {
            if (pageIndex <= totalPage) {
                pageIndex++;
                loadData();
            }
        });
    </script>
</asp:Content>
