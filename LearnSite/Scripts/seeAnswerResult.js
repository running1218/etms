var data;
var radioData = [];//单选题数据
var checkData = [];//多选题数据
var judgeData = [];//判断题数据
var num = [];
var testData = {};
//储存作答结果
function saveAnswerResults(questionID, answer) {
    $.each(data.Data.PaperQuestion, function (i, val) {
        if (questionID == val.QuestionID) {
            val.UserAnswer = answer;
        }
    })
}
//渲染试卷信息
function renderData() {
    $('.worktitle').text(data.Data.TestPaperName);
    $('.jobcount').text(data.Data.QuestionCount);
    $('.jobscore').text(data.Data.TotalScore);
    $('.duration').text(data.Data.BeginTime + '至' + data.Data.EndTime);
    $('.ftsanswer').text(data.Data.UserScore+'分');
    if (data.Data.TestType == 2) { //在线作业
        $('.times').hide();
    } else {//在线测试
        $('.times').show();
        $('.limittimes').text(data.Data.TestCount);
        $('.answertimes').text(data.Data.UserTestCount);
    }
    renderTest(data.Data.PaperQuestion);
}
//渲染试题信息
function renderTest(testInfo) {
    $.each(testInfo, function (i, val) {
        if (val.QuestionType == 4) {
            judgeData.push(val);
        } else if (val.QuestionType == 2) {
            checkData.push(val);
            num.push(0);
        } else {
            radioData.push(val);
        }
    })
    testData.radioData = radioData;
    testData.checkData = checkData;
    testData.judgeData = judgeData;
    Handlebars.registerHelper("addOne", function (index, options) {
        return parseInt(index) + 1;
    });
    Handlebars.registerHelper('Continue', function (items, userAnswer) {
        var out = "";
        for (var i = 0; i < items.length; i++) {
            if (userAnswer != null && userAnswer == items[i].OptionID) {
                out += '<li class="cur" flag="0" optionID="' + items[i].OptionID + '"><a href="javascript:void(0)"><i class="options">' + items[i].OptionCode + '</i><span>' + items[i].OptionContent + '</span></a></li>';
            } else {
                out += '<li flag="1" optionID="' + items[i].OptionID + '"><a href="javascript:void(0)"><i class="options">' + items[i].OptionCode + '</i><span>' + items[i].OptionContent + '</span></a></li>';
            }
        }
        return new Handlebars.SafeString(out);
    }); 
    Handlebars.registerHelper('ContinueJudge', function (items, userAnswer) {
        var out = "";
        for (var i = 0; i < items.length; i++) {
            if (userAnswer != null && userAnswer == items[i].OptionID) {
                out += '<li class="cur" flag="0" optionID="' + items[i].OptionID + '"><a href="javascript:void(0)"><i class="options">' + items[i].OptionCode + '</i><span>' + items[i].OptionContent + '</span></a></li>';
            } else {
                out += '<li flag="1" optionID="' + items[i].OptionID + '"><a href="javascript:void(0)"><i class="options">' + items[i].OptionCode + '</i><span>' + items[i].OptionContent + '</span></a></li>';
            }
        }
        return new Handlebars.SafeString(out);
    });
    Handlebars.registerHelper("renderAnswer", function (questionType, answer) {
        if (questionType != 2) {
            return new Handlebars.SafeString('<p>正确答案：' + JSON.parse(answer).OptionCode + '</p>');
        } else {
            var answers = '';
            $.each(JSON.parse(answer), function (i, val) {
                answers += val.OptionCode;
            })
            return new Handlebars.SafeString('<p>正确答案：' + answers + '</p>');
        }
    });
    if (radioData.length != 0) {
        var sore = 0;
        var template = Handlebars.compile($("#radioDataTPLT").html());
        var renderHTML = template(testData);
        $('.radio_list').html(renderHTML);
        $.each(radioData, function (i, val) {
            sore += val.QuestionScore;
            $('.radio-tab').append('<a  id="' + val.QuestionID + '_btn" href="#' + val.QuestionID + '">' + (i + 1) + '</a>');
            if (val.UserAnswer != '') {
                $('#' + val.QuestionID + '_btn').addClass('done');  
            } 
        })
        $('.radionums').text('(共' + radioData.length + '题，' + sore + '分)');
        $('.sigletopics').text('单选题（' + radioData.length + '题，' + sore + '分）');
    } else {
        $('.question-radio').hide();
        $('.answerguide dl').eq(0).hide();
        $('.checkques').text('一、多选题');
        $('.judgeques').text('二、判断题');
    }

    if (checkData.length != 0) {
        var sore = 0;
        var template = Handlebars.compile($("#checkDataTPLT").html());
        var renderHTML = template(testData);
        $('.check_list').html(renderHTML);
        $.each(checkData, function (i, val) {
            sore += val.QuestionScore;
            $('.check-tab').append('<a  id="' + val.QuestionID + '_btn" href="#' + val.QuestionID + '">' + (i + 1) + '</a>');
        })
        $.each(checkData, function (i, val) {
            if (val.UserAnswer != '') {
                $.each(val.QuestionOption, function (j, val1) {
                    var UserAnswer = val.UserAnswer.split(',');
                    $.each(UserAnswer, function (k, value) {
                        if (value == val1.OptionID) {
                            $('#' + val.QuestionID + ' li').eq(j).addClass('cur').attr('flag', 0);
                        }
                    })
                })
                $('#' + val.QuestionID + '_btn').addClass('done');
            }
        })


        $('.checknums').text('(共' + checkData.length + '题，' + sore + '分)');
        $('.checkbox').text('多选题（' + checkData.length + '题，' + sore + '分）');
    } else {
        $('.question-check').hide();
        $('.answerguide dl').eq(1).hide();
        $('.checkques').text('一、单选题');
        $('.judgeques').text('二、判断题');
    }

    if (judgeData.length != 0) {
        var sore = 0;
        var template = Handlebars.compile($("#judgeDataTPLT").html());
        var renderHTML = template(testData);
        $('.judge_list').html(renderHTML);
        $.each(judgeData, function (i, val) {
            sore += val.QuestionScore;
            $('.judge-tab').append('<a  id="' + val.QuestionID + '_btn" href="#' + val.QuestionID + '">' + (i + 1) + '</a>');
            if (val.UserAnswer != '') {
                $('#' + val.QuestionID + '_btn').addClass('done');
            }
        })
        $('.judgenums').text('(共' + judgeData.length + '题，' + sore + '分)');
        $('.judge').text('判断题（' + judgeData.length + '题，' + sore + '分）');
    } else {
        $('.question-judge').hide();
        $('.answerguide dl').eq(2).hide();
        $('.checkques').text('一、单选题');
        $('.judgeques').text('二、多选题');
    }
}

$(function () {
    var TrainingItemCourseID = GetQueryString('TrainingItemCourseID');
    var TestPaperID = GetQueryString('TestPaperID');
    var OnlineTestID = GetQueryString('OnlineTestID');
    var StudentCourseID = GetQueryString('StudentCourseID');
    var TestType = GetQueryString('TestType');

    var UserExamID = GetQueryString('UserExamID');
    var dataUrl = AppPath + '/PublicService/TestPaperHandler.ashx?Method=getstudentpaper&TrainingItemCourseID=' + TrainingItemCourseID + '&TestPaperID=' + TestPaperID + '&OnlineTestID=' + OnlineTestID + '&StudentCourseID=' + StudentCourseID + '&TestType=' + TestType + '&UserExamID=' + UserExamID;
    //获取试卷
    $.ajax({
        url: dataUrl,
        type: 'get',
        data: 'json',
        success: function (info) {
            console.log(info);
            if (info.Status) {
                data = info;
                renderData();
                //标记
                $('.question-mark').click(function () {
                    var questionID = $(this).attr('questionID');
                    $('#' + questionID + '_btn').toggleClass('mark_btn');
                    $(this).find('i').toggleClass('marked');
                })
            }
        },
        error: function (data) {
            console.log(data)
        }
    })
    //智能定位
    $(document).scroll(function () {
        var nScrollTop = $(this).scrollTop();
        if (nScrollTop > 180) {
            $('.right_content').css({ 'top': nScrollTop - 260 });
        } else {
            $('.right_content').css({ 'top': 0 });
        }
    });
    //关闭
    $('#close').click(function () {
        window.close();
    })
    //console.log(_.difference([1, 2, 3, 4], [4, 2]))
})