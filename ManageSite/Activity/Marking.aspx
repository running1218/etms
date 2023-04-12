<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Marking.aspx.cs" Inherits="ActivityMarking" %>
<%@ Register Src="~/Tools/pdf/web/PdfViewer.ascx" TagName="Viewer" TagPrefix="Pdf" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <script lang="javascript" src="<%=WebUtility.AppPath %>/JScript/jquery-3.1.1.min.js"></script>
    <script lang="javascript" src="<%=WebUtility.AppPath %>/JScript/layer/layer.js"></script>
    <script lang="javascript" type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/Tools/VideoPlayer/ckplayer/ckplayer.js"></script>
    <script lang="javascript" type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/JScript/screenfull.js"></script>
    <script lang="javascript" type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/JScript/umVideo.js"></script>
    <script lang="javascript" type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/JScript/making.js"></script>

    <form id="form1" runat="server">
    <div class="admin-marking-box">
        <div class="admin-marking-content">
            <p>
                <span class="marking-group-type">小学--微诗歌组</span>                
            </p>
            <div class="admin-marking-production">
                <div class="production-left">
                    <ul>
                        <asp:Repeater ID="rptProduction" runat="server">
                            <ItemTemplate>
                                <li id="<%# Eval("ProductID") %>"><span class="siginup-no"><%# Eval("SiginupNo") %></span><span class="<%# Eval("AppraiseStatus").ToInt() == 0? "":"marking-completed" %>"></span></li>  
                            </ItemTemplate>
                        </asp:Repeater>                                             
                    </ul>
                </div>
                <div class="production-right">
                    <div class="production-title"><span>作品名称：</span><a onclick="openRule()">查看评分规则</a><a id="downloadfile" style="margin: 0px 20px;">下载</a></div>
                    <div class="production-content">                        
                        <iframe id="fmfile" src="" height="500" width="1050"></iframe>
                        <div class="marking-video-box play_file" style="width:100%"></div>
                    </div>
                    <div class="production-action">
                        <table>
                            <tr>
                                <td class="comment-title">最终得分：</td>
                                <td class="comment-score"><input id="txtScore" type="text" /></td>
                                <td class="comment-title">评语：</td>
                                <td class="comment-content"><input id="txtComment" type="text" /></td>
                                <td class="comment-submit"><input id="btnSubmit" type="button" value="提交" onclick="return sava()" /></td>
                            </tr>
                        </table>
                    </div>
                    <p class="production-next-pre"></p>
                </div>
            </div>
        </div>
    </div>
    </form>
    <script lang="javascript">
        var AppPath = '<%=WebUtility.AppPath%>';
        var FileUrlRoot = '<%=WebUtility.FileUrlRoot%>';
        var curProductID = '';
        function openRule(url)
        {
            var url = '<%=RuleUrl%>';
            layer.open({
                title: '活动规则',
                area: ['800px', '600px'],
                type: 2,
                content: url
            });
        }

        $('.production-left ul li').click(function () {
            var index = layer.load();
            var id = $(this).attr('id');
            $('.selected').removeClass('selected');
            $(this).addClass('selected');
           
            $.ajax({
                url: AppPath + "/Services/Produciton.ashx",
                type: 'POST',
                dataType: "json",
                data: { Method: "getproductioninfo", ID: id},
                success: function (result) {
                    if (result.Status == true) {
                        var row = result.Data;
                        SetData(row);
                    }
                    else {
                        alert(result.Message);
                    }

                    layer.close(index);
                },
                error: function (err) {
                    console.log(err);
                    layer.close(index);
                }
            });
        });

        function SetData(row)
        {
            var pdfViewer = AppPath + '/tools/pdf/web/viewer.html?file=';

            $('.production-title span').html('作品名称：' + row.ProductName);

            if (row.Extention == '.mp4') {
                $('.marking-video-box').html('');
                $('#fmfile').addClass('hide');
                $('.marking-video-box').removeClass('hide');
                loadvideo(FileUrlRoot + '/ExOfflineHomework/' + row.TransFilePath);
                $('#downloadfile').attr('href', FileUrlRoot + '/ExOfflineHomework/' + row.TransFilePath)
            }
            else {
                $('#fmfile').removeClass('hide');
                $('.marking-video-box').addClass('hide');
                $('#fmfile').attr("src", pdfViewer + FileUrlRoot + '/ExOfflineHomework/' + row.TransFilePath);
                $('#downloadfile').attr('href', FileUrlRoot + '/ExOfflineHomework/' + row.TransFilePath)
                //$('#fmfile').attr("src", pdfViewer + "http://localhost:8102/tools/pdf/web/compressed.tracemonkey-pldi-09.pdf");
            }
            

            if (row.AppraiseStatus == 1) {
                $('#txtScore').val(row.Score);
                $('#txtComment').val(row.Comment);
            }
            else {
                $('#txtScore').val('');
                $('#txtComment').val('');
            }

            curProductID = row.ProductID;
        }

        function sava()
        {
            if (curProductID == '')
            {
                layer.msg("请选择要评阅的作品！");
                return false;
            }
            if ($('#txtScore').val().trim() == '')
            {
                layer.msg('请输入最终得分！');
                return false;
            }
            if ($('#txtComment').val().trim() == "")
            {
                layer.msg('请输入评语！');
                return false;
            }

            $.ajax({
                url: AppPath + "/Services/Produciton.ashx",
                type: 'POST',
                dataType: "json",
                data: { Method: "approveactivity", ID: curProductID, Score: $('#txtScore').val(), Comment: $('#txtComment').val()},
                success: function (result) {
                    if (result.Status == true) {
                        $('#' + curProductID).find('span:eq(1)').addClass('marking-completed');
                        layer.msg("批阅完成！");
                    }
                    else {
                        alert(result.Message);
                    }
                },
                error: function (err) {
                    console.log(err);
                }
            });
        }

        $(document).ready(function () {
            $('.production-left ul').find('li:eq(0)').click();
        });
    </script>
</body>
</html>
