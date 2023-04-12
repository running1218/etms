var data;
var radioData = [];//单选题数据
var checkData = [];//多选题数据
var judgeData = [];//判断题数据
var num = [];//多选题选项标记选择数量集合
var radioAnswer = {};//单选题作答答案
var checkAnswer = [];//多选题作答答案
var judgeAnswer = {};//判断题作答答案
var num = [];
var testData = {};
var TrainingItemCourseID = GetQueryString('TrainingItemCourseID');
//倒计时
function countDown(intDiff) {
    var timer = window.setInterval(function () {
        var day = 0,
            hour = 0,
            minute = 0,
            second = 0;//时间默认值        
        if (intDiff > 0) {
            day = Math.floor(intDiff / (60 * 60 * 24));
            hour = Math.floor(intDiff / (60 * 60)) - (day * 24);
            minute = Math.floor(intDiff / 60) - (day * 24 * 60) - (hour * 60);
            second = Math.floor(intDiff) - (day * 24 * 60 * 60) - (hour * 60 * 60) - (minute * 60);
        }
        if (minute <= 9) minute = '0' + minute;
        if (second <= 9) second = '0' + second;
        $('#timer_hours').html(hour +':');
        $('#timer_minutes').html(minute + ':');
        $('#timer_seconds').html(second);
        intDiff--;
        if (intDiff < 0) {
            clearInterval(timer);
            var saveUrl = AppPath + '/PublicService/TestPaperHandler.ashx?Method=submitpaper';
            $.ajax({
                url: saveUrl,
                type: 'post',
                data: { PaperData: JSON.stringify(data.Data) },
                success: function (info) {
                    if (info.Status) {
                        layer.alert('已提交', { icon: 6 }, '提示信息', function () {
                            layer.closeAll();
                            location.href = AppPath + '/Study/Evaluation.aspx?trainingItemCourseID=' + TrainingItemCourseID;
                        })
                    } else {
                        layer.alert(info.Message);
                    }
                },
                error: function (data) {
                    console.log(data);
                }
            })
           
        }
    }, 1000);
}

//储存作答结果
function saveAnswerResults(questionID, answer, answerType) { //answerType:答案类型 1字符串 0需把数组转成字符串
    console.log(answer)
    $.each(data.Data.PaperQuestion, function (i, val) {
        if (questionID == val.QuestionID) {
            if (answerType) {
                val.UserAnswer = answer;
            } else {
                val.UserAnswer = answer.join(',');
            }
        }
    })
}
//渲染试卷信息
function renderData() {
    $('.worktitle').text(data.Data.TestPaperName);
    $('.jobcount').text(data.Data.QuestionCount);
    $('.jobscore').text(data.Data.TotalScore);
    $('.duration').text(data.Data.BeginTime + '至' + data.Data.EndTime);
    if (data.Data.TestType == 2) { //在线作业
        $('.times,.countdown_time').hide();
    } else {//在线测试
        $('.times,.countdown_time').show();
        $('.limittimes').text(data.Data.TestCount);
        $('.answertimes').text(data.Data.UserTestCount);
    }
    if (data.Data.TestCount == data.Data.UserTestCount && data.Data.TestCount>0) {
        //console.log(data.Data.TestCount);
        location.href = AppPath + '/Study/Evaluation.aspx?trainingItemCourseID=' + TrainingItemCourseID;
    }

    console.log(data.Data.PaperQuestion);

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
    //单选续作（判断选项是否为选中状态）
    Handlebars.registerHelper('Continue', function (items, userAnswer) {
        var out = "";
        for (var i = 0; i < items.length;  i++) {
            if (userAnswer != '' && userAnswer == items[i].OptionID) {
                out += '<li class="cur" flag="0" optionID="' + items[i].OptionID + '" optionCode="' + items[i].OptionCode + '"><a href="javascript:void(0)"><i class="options">' + items[i].OptionCode + '</i><span>' + items[i].OptionContent + '</span></a></li>';
            } else {
                out += '<li flag="1" optionID="' + items[i].OptionID + '" optionCode="' + items[i].OptionCode + '"><a href="javascript:void(0)"><i class="options">' + items[i].OptionCode + '</i><span>' + items[i].OptionContent + '</span></a></li>';
            }
        }
        return new Handlebars.SafeString(out);
    });
    //判断续作（判断选项是否为选中状态）
    Handlebars.registerHelper('ContinueJudge', function (items, userAnswer) {
        var out = "";
        for (var i = 0; i < items.length; i++) {
            if (userAnswer != '' && userAnswer == items[i].OptionID) {
                out += '<label flag="0" optionID="' + items[i].OptionID + '" optionCode="' + items[i].OptionCode + '"><li class="cur"><input checked="checked" class="radios" type="radio" name="' + items[i].QuestionID + '">&nbsp;&nbsp;<span>' + items[i].OptionContent + '</span></li></label>'
            } else {
                out += '<label flag="1" optionID="' + items[i].OptionID + '" optionCode="' + items[i].OptionCode + '"><li><input class="radios" type="radio" name="' + items[i].QuestionID + '">&nbsp;&nbsp;<span>' + items[i].OptionContent + '</span></li></label>'
            }
        }
        return new Handlebars.SafeString(out);
    });
    //单选题渲染
    if (radioData.length != 0) {
        var sore = 0;
        var template = Handlebars.compile($("#radioDataTPLT").html());
        var renderHTML = template(testData);
        $('.radio_list').html(renderHTML);
        $.each(radioData, function (i,val) {
            sore += val.QuestionScore;
            $('.radio-tab').append('<a  id="' + val.QuestionID + '_btn" href="#' + val.QuestionID + '">' + (i + 1) + '</a>');
            if (val.UserAnswer != '' && val.UserAnswer.OptionID == val.OptionID) {
                $('#'+ val.QuestionID + '_btn').addClass('done');
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
    //多选题渲染
    if (checkData.length != 0) {
        var sore = 0;
        var template = Handlebars.compile($("#checkDataTPLT").html());
        var renderHTML = template(testData);
        $('.check_list').html(renderHTML);
        $.each(checkData, function (i, val) {
            sore += val.QuestionScore;
            $('.check-tab').append('<a  id="' + val.QuestionID + '_btn" href="#' + val.QuestionID + '">' + (i + 1) + '</a>');
        })
        //多选续作（判断选项是否为选中状态）
        $.each(checkData, function (i, val) {
            checkAnswer.push([]);
            if (val.UserAnswer != '') {
                var UserAnswers = val.UserAnswer.split(',');
                $.each(val.QuestionOption, function (j, val1) {
                    $.each(UserAnswers, function (k, value) {
                        if (value == val1.OptionID) {
                            $('#' + val.QuestionID + ' li').eq(j).addClass('cur').attr('flag', 0);
                            checkAnswer[i][j] = value;
                        } 
                    })
                })
                $('#' + val.QuestionID + '_btn').addClass('done');
            }
        })
        $('.checknums').text('(共' + checkData.length + '题，' + sore + '分)');
        $('.checkbox').text('多选题（' + checkData.length + '题，' + sore + '分）');
    }else{
        $('.question-check').hide();
        $('.answerguide dl').eq(1).hide();
        $('.checkques').text('一、单选题');
        $('.judgeques').text('二、判断题');
    }
    //判断题渲染
    if (judgeData.length != 0) {
        var sore = 0;
        var template = Handlebars.compile($("#judgeDataTPLT").html());
        var renderHTML = template(testData);
        $('.judge_list').html(renderHTML);
        $.each(judgeData, function (i, val) {
            sore += val.QuestionScore;
            $('.judge-tab').append('<a  id="' + val.QuestionID + '_btn" href="#' + val.QuestionID + '">' + (i + 1) + '</a>');
            if (val.UserAnswer != '' && val.UserAnswer.OptionID == val.OptionID) {
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
    doWork();
}
//做作业
function doWork() {
    //单选题
    $('.question-radio').delegate('li', 'click', function () {
        var $this = $(this);
        var questionId = $this.parents('.question-cont-right').attr('iteamId');
        choiceClick($this, 1, questionId);
    });
    //多选题
    $('.question-check').delegate('li', 'click', function () {
        var $this = $(this);
        var questionId = $this.parents('.question-cont-right').attr('iteamId');
        choiceClick($this, 2, questionId);
    });
    //判断题
    $('.question-judge').delegate('label', 'change', function () {
        var $this = $(this);
        var questionId = $this.parents('.question-cont-right').attr('iteamId');
        choiceClick($this, 3, questionId)
    });
}

//选项保存
function choiceClick($this, type, questionId) {
    //把正在做的题目标记已做完
    if ($('.doing').length > 0) {
        $('.doing').removeClass('doing').addClass('done');
    }
     var itemIndex = $this.parents('.question-cont').index();
     var optionID = $this.attr('optionid');//选项id
     var flag = $this.attr('flag');//判断选项状态（1：选中；0：未选中）
     switch (type) {
         case 1:
             if(flag == '1'){
                 $this.addClass('cur').siblings().removeClass('cur');
                 radioAnswer = optionID;
                 $('#' + questionId + '_btn').removeClass('doing done').addClass('doing');
                 $this.attr('flag', 0).siblings().attr('flag',1);
             } else {
                 $this.removeClass('cur');
                 radioAnswer= '';
                 $('#' + questionId + '_btn').removeClass('doing done');
                 $this.attr('flag', 1).siblings().attr('flag', 1);
             }
             saveAnswerResults(questionId, radioAnswer,1);
             break;
         case 2:
             var optionIndex = $this.index();
             if (flag == '1') {
                 $this.addClass('cur');
                 num[itemIndex] += 1;
                 checkAnswer[itemIndex][optionIndex] = optionID;
                 $('#' + questionId + '_btn').removeClass('doing done').addClass('doing');
                 $this.attr('flag', 0);
             } else {
                 num[itemIndex] -= 1;
                 checkAnswer[itemIndex][optionIndex] = '';
                 $this.removeClass('cur');
                 if (num[itemIndex] == 0) {
                     $('#' + questionId + '_btn').removeClass('doing done');
                 } else {
                     $('#' + questionId + '_btn').removeClass('doing done').addClass('doing');
                 }
                 $this.attr('flag', 1);
             }
             saveAnswerResults(questionId, checkAnswer[itemIndex], 0);
             break;
         case 3:
             if (flag == '1') {
                 $this.find('li').addClass('cur').parent().siblings().find('li').removeClass('cur');
                 judgeAnswer = optionID;
                 $('#' + questionId + '_btn').removeClass('doing done').addClass('doing');
                 $this.attr('flag', 0).siblings().attr('flag', 1);
             } else {
                 $this.find('li').removeClass('cur');
                 judgeAnswer = '';
                 $('#' + questionId + '_btn').removeClass('doing done');
                 $this.attr('flag', 1).siblings().attr('flag', 1);
             }
             saveAnswerResults(questionId, judgeAnswer,1);
             break;
     }
}
$(window).on('unload', function () {
    var saveUrl = AppPath + '/PublicService/TestPaperHandler.ashx?Method=savepaper';
    $.ajax({
        url: saveUrl,
        type: 'post',
        data: { PaperData: JSON.stringify(data.Data) },
        success: function (info) {
            if (info.Status) {
                layer.alert('保存成功', { icon: 6 });
            } else {
                layer.alert(info.Message);
            }
        },
        error: function (data) {
            console.log(data)
        }
    })
})
$(function () {
    
    var TestPaperID = GetQueryString('TestPaperID'); 
    var OnlineTestID = GetQueryString('OnlineTestID');
    var StudentCourseID = GetQueryString('StudentCourseID');
    var TestType = GetQueryString('TestType');//试卷类型（5：测试；2：作业）
    var dataUrl = AppPath+'/PublicService/TestPaperHandler.ashx?Method=getpaper&TrainingItemCourseID=' + TrainingItemCourseID + '&TestPaperID=' + TestPaperID + '&OnlineTestID=' + OnlineTestID + '&StudentCourseID=' + StudentCourseID + '&TestType=' + TestType;
   
    //获取试卷
    $.ajax({
        url: dataUrl,
        type: 'get',
        data: 'json',
        success: function (info) {
            if (info.Status) {
                data = info;
                if (info.Data.TestType != 2) {
                    countDown(info.Data.LimitTime);
                }
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
    //提交试卷
    $('#submitwork').click(function () {
        var saveUrl = AppPath+'/PublicService/TestPaperHandler.ashx?Method=submitpaper';
        $.ajax({
            url: saveUrl,
            type: 'post',
            data: { PaperData: JSON.stringify(data.Data) },
            success: function (info) {
                if (info.Status) {
                    //$('.ftsanswer').text(JSON.parse(data.Data) + '分');
                    location.href = AppPath + '/Study/Evaluation.aspx?trainingItemCourseID=' + TrainingItemCourseID;
                } else {
                    layer.alert(info.Message);
                }
            },
            error: function (data) {
                console.log(data)
            }
        })
    })
    //保存试卷
    $('#savework').click(function () {
        var saveUrl = AppPath+'/PublicService/TestPaperHandler.ashx?Method=savepaper';
        $.ajax({
            url: saveUrl,
            type: 'post',
            data: { PaperData: JSON.stringify(data.Data)},
            success: function (info) {
                if (info.Status) {
                    layer.alert('保存成功', { icon: 6 });
                } else {
                    layer.alert(info.Message);
                }
            },
            error: function (data) {
                console.log(data)
            }
        })
    })
   
})
