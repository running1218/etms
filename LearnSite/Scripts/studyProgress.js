$(function () {

    function LoadEcharts(ContentComplete, ContentNotstarted, ContentUnfinished, TestjobComplete, TestjobNotstarted, Percentage) {

        //圆环统计图
        var chart1 = echarts.init(document.getElementById('chart1'));
        var labelTop = {
            normal: {
                label: {
                    formatter: function (params) {
                        return params.value + '%'
                    },
                    show: true,
                    position: 'center'
                },
                labelLine: {
                    show: false
                }
            }
        };
        var labelFromatter = {
            normal: {
                color: '#0dbc83',
                label: {
                    show: false,
                    textStyle: {
                        baseline: 'top',
                        color: '#333',
                        fontSize: '26'
                    }
                }
            },
        }
        var labelBottom = {
            normal: {
                color: '#ccc',
                label: {
                    show: false,
                },
                labelLine: {
                    show: false
                }
            }
        };
        var radius = [50, 55];
        option1 = {
            series: [
                {
                    type: 'pie',
                    center: ['30%', '50%'],
                    radius: radius,
                    x: '0%',
                    itemStyle: labelFromatter,
                    data: [
                        { name: 'other', value: 100 - Percentage, itemStyle: labelBottom },
                        { name: '学习进度', value: Percentage, itemStyle: labelTop }
                    ]
                }
            ]
        };
        chart1.setOption(option1);
        //柱状图
        var chart2 = echarts.init(document.getElementById('chart2'));
        option2 = {
            tooltip: {
                show: true,
                trigger: 'item'
            },
            legend: {
                data: ['未学习', '未完成', '已完成']
            },
            xAxis: [
                {
                    type: 'category',
                    data: ['学习内容', '测评']
                }
            ],
            yAxis: [
                {
                    type: 'value'
                }
            ],
            series: [
                {
                    name: '未学习',
                    barWidth: 30,
                    type: 'bar',
                    itemStyle: {                // 系列级个性化
                        normal: {
                            color: '#fb9986'
                        }
                    },
                    data: [ContentNotstarted, TestjobNotstarted]
                },
                {
                    name: '未完成',
                    barWidth: 30,
                    type: 'bar',
                    itemStyle: {                // 系列级个性化
                        normal: {
                            color: '#ffd473'
                        }
                    },
                    data: [ContentUnfinished, 0]
                },
                {
                    name: '已完成',
                    barWidth: 30,
                    itemStyle: {                // 系列级个性化
                        normal: {
                            color: '#b3e288'
                        }
                    },
                    type: 'bar',
                    data: [ContentComplete, TestjobComplete]
                }
            ]
        };
        chart2.setOption(option2);
    }



    // '学习进度'选中状态
    $('.study_modular li').removeClass('cur').eq(6).addClass('cur');

    var TrainingItemCourseID = GetQueryString('TrainingItemCourseID');

    //加载数据
    function LoadCourseProgress() {
        $.ajax({
            url: AppPath + "/PublicService/Course.ashx",
            type: 'POST',
            data: {
                Method: "GetCourseProgress",
                TrainingItemCourseID: TrainingItemCourseID,
            },
            dataType: "json",
            success: function (data) {
                if (data.Status) {

                    //加载统计
                    LoadEcharts(data.Data.ContentComplete, data.Data.ContentNotstarted, data.Data.ContentUnfinished, data.Data.TestjobComplete, data.Data.TestjobNotstarted, data.Data.Percentage);

                    //清空数据
                    $("#content_list").html("");
                    $("#test_job_list").html("");

                    //资源
                    for (var i = 0; i < data.Data.Content.length; i++) {
                        var html = "<li>" +
                                        "<p>" + data.Data.Content[i].Name + "</p>" +
                                        "<p>" + ConvertTime(data.Data.Content[i].Type, data.Data.Content[i].Progress) + " / " + data.Data.Content[i].ContentTime + "</p>" +
                                        "<p>" + data.Data.Content[i].StatusWord + "</p>" +
                                        "<p><a target='_blank' href='" + data.Data.Content[i].BtnUrl + "'>" + data.Data.Content[i].BtnWord + "</a></p>" +
                                   "</li>";
                        $("#content_list").append(html);
                    }

                    //测试
                    for (var i = 0; i < data.Data.Test.length; i++) {
                        var html = "<li>" +
                                        "<p>" + data.Data.Test[i].TestName + "</p>" +
                                        "<p>" + data.Data.Test[i].AlreadyUseCount + " / " + data.Data.Test[i].MaxUseCount + "</p>" +
                                        "<p>" + data.Data.Test[i].MaxScore + "</p>" +
                                        "<p><a target='_blank' href='" + data.Data.Test[i].BtnUrl + "'>" + data.Data.Test[i].BtnWord + "</a></p>" +
                                   "</li>";
                        $("#test_job_list").append(html);
                    }

                    //作业
                    for (var i = 0; i < data.Data.Job.length; i++) {
                        var html = "<li>" +
                                        "<p>" + data.Data.Job[i].JobName + "</p>" +
                                        "<p>" + (data.Data.Job[i].JobStatus == '未提交' ? 0 : 1) + " / 1</p>" +
                                        "<p>" + data.Data.Job[i].Score + "</p>" +
                                        "<p><a target='_blank' href='" + data.Data.Job[i].BtnUrl + "'>" + data.Data.Job[i].BtnWord + "</a></p>" +
                                   "</li>";
                        $("#test_job_list").append(html);
                    }

                    //完成状态
                    //if (data.Data.ContentComplete != 0 && (data.Data.ContentNotstarted > 0 || data.Data.ContentUnfinished > 0))
                    //    $("#content_status").removeClass("default").addClass("studying");
                    //if (data.Data.ContentUnfinished == 0 && data.Data.ContentNotstarted == 0) {
                    //    $("#content_status").removeClass("default").addClass("study_end");
                    //}
                    if (data.Data.ContentNotstarted > 0 && data.Data.ContentUnfinished > 0) {
                        $("#content_status").removeClass("default").addClass("studying");
                    } else if (data.Data.Content.length == data.Data.ContentComplete) {
                        $("#content_status").removeClass("default").addClass("study_end");
                    }

                    //if (data.Data.TestjobNotstarted > 0)
                    //    $("#test_job_status").removeClass("default").addClass("studying");
                    //if (data.Data.TestjobNotstarted == 0)
                    //    $("#test_job_status").removeClass("default").addClass("study_end");
                    if (data.Data.TestjobNotstarted>0 && (data.Data.Job.length + data.Data.Test.length) > data.Data.TestjobNotstarted) {
                        $("#test_job_status").removeClass("default").addClass("studying");
                    } else if ((data.Data.Job.length + data.Data.Test.length) == data.Data.TestjobComplete) {
                        $("#test_job_status").removeClass("default").addClass("study_end");
                    }
                }
            }
        });
    }
    LoadCourseProgress();

    //毫秒时间转换为时分秒
    function ConvertTime(type, s) {
        if (type != 1) {
            return s;
        }
        if (s == 0) {
            return "00:00:00";
        } else {
            var second = Math.floor(s / 1000);
            var min = Math.floor(second / 60);
            second = second % 60;
            var hour = Math.floor(min / 60);
            min = min % 60;
            if (hour == 0) {
                hour = "00";
            } else if (hour < 10 && hour > 0) {
                hour = "0" + hour;
            }
            if (min == 0) {
                min = "00";
            } else if (min < 10 && min > 0) {
                min = "0" + min;
            }
            if (second == 0) {
                second = "00";
            } else if (second < 10 && second > 0) {
                second = "0" + second;
            }
            return hour + ":" + min + ":" + second;
        }
    }
});