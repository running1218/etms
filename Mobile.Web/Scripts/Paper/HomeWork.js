var login = login || {};
var singleName = 1// "单选题";
var multiName = 2// "多选题";
var judgeName = 4// "判断题";
var countSingle = 0; //单选题总题数
var countMulti = 0; //多选题总题数
var countJudge = 0; //判断题总题数
var nowCount = 0;
var noDoSingle = [];//未做的单选题
var noDoMulti = [];//未做的多选题
var noDoJudge = [];//未做的判断题
var singleAnswer = [];//单选题答题结果
var multipleAnswer = [];//多选题答题结果
var judgementAnswer = [];//判断题答题结果
var paperTips = "";
var _newpaper = [];//新的试卷
var UserExamID = '';
var commitPaper = {};
var commitStatus = '';//判断用户是否达到用户提交
var worknav = $(".worknav");
var uid = Cookies.get('cookie_userid');//用户id
var _totalTitle = common.decode(common.GetQueryString("CourseName"));//所属课程名称
var indexJupm = common.GetQueryString("indexJupm");
var MOrC = common.GetQueryString("MOrC");//判断从那个页面跳来1m-0c
//获取试卷所需参数
var TrainingItemCourseID = common.GetQueryString("TrainingItemCourseID");//培训项目课程ID
var CourseID = common.GetQueryString("CourseID");//课程ID
var TestPaperID = common.GetQueryString("TestPaperID");//试卷ID
var OnlineTestID = common.GetQueryString("OnlineTestID");//测评【考试ID或者作业ID】 
var StudentCourseID = common.GetQueryString("StudentCourseID");//学生选课ID 
var TestType = common.GetQueryString("TestType");//试卷测试类型：2-在线作业；5-在线测试
//获取试卷参数
var params = { "TrainingItemCourseID": TrainingItemCourseID, "TestPaperID": TestPaperID, "OnlineTestID": OnlineTestID, "StudentCourseID": StudentCourseID, "TestType": TestType, "StudentID": uid };
//console.log(TrainingItemCourseID + "=======---" + TestPaperID + "=======---" + OnlineTestID + "=======---" + StudentCourseID + "=======---" + TestType)
function MOrCFun() {
    if (MOrC == "1") {
        window.location.href = AppPath + '/MyTrain/Index';
    } else if (MOrC == "0") {
        window.location.href = AppPath + '/MyTrain/Detail?TrainingItemCourseID=' + TrainingItemCourseID;
    }
    else if (MOrC == "2")
    {
        window.location.href = AppPath + '/MyTrain/LivingStudy?TrainingItemCourseID=' + TrainingItemCourseID + '&CourseID=' + CourseID;
    }
}


//初始化方法
login.init = function (params) {
    common.call(AppPath+"/Paper/GetStudentPaperData", params, 'get', function (data) {
        courseExercise(data);
    }, error)
}
//执行初始化
login.init(params);
//主函数
function courseExercise(data) {
    // console.log(data)

    if (data.Status) {
        commitStatus = data.Data.TestStatus;
        if (commitStatus) {
            MOrCFun();
        }
        commitPaper = data.Data;
        worknav.show();//头部
        var _paper =topicGroups( data.Data.PaperQuestion);//数组
        UserExamID = data.Data.UserExamID;
        //console.log(_paper)     
        var _totalScore = 0;//总分
        var _totalCount = data.Data.QuestionCount;//总题数
        //组合成新的试卷
        _newpaper = _paper;
        if (_paper.length != undefined) {
            for (i = 0; i < _paper.length; i++) {
                var _exerciseType = _paper[i]['QuestionType'];//1，2，4
                if (_exerciseType == singleName) {
                    countSingle += 1;
                }

                if (_exerciseType == multiName) {
                    countMulti += 1;
                }

                if (_exerciseType == judgeName) {
                    countJudge += 1;
                }
                var _score = parseFloat(_paper[i].QuestionScore);
                _totalScore += _score;
            }
        }

        // console.log(_newpaper);
        var _newpaperObj = { "Data": _newpaper };
        console.log(_newpaperObj);
        var template = Handlebars.compile($("#exerciselistTPLT").html());
        var renderHTML = template(_newpaperObj);
        $(".exerciselist").html(renderHTML);

        $(".swiper-slide").eq(0).removeClass("hide-item");
        if (_newpaper.length == undefined) {
            $(".exercisecount").text("1");
        }
        else {
            $(".exercisecount").text(_newpaper.length);
        }
        $(".exercisetotalscore").text(_totalScore);//显示试卷总分
        $(".topicNum").text(_totalCount)//显示试卷总题数
        $(".courseName").text(_totalTitle);//课程名
        var firstExercise = $(".swiper-slide").eq(0).find(".exercisetypename").text();//第一个试题的类型
        var _exercisetypePrve = $(".swiper-slide").eq(0).find(".exercisetypename").text();//上一个试题的类型
        var _exercisePrevCount = 0;//当滑动试题时计算前面的试题总数
        $(".exercisetype").text(firstExercise);//显示第一个试题的类型
        showExerciseCount(firstExercise);//显示第一组试题的个数
        var groupCountPrev = nowCount; //前面题型和现在题型的总题数,默认为第一个题型的总题数
        var smallOrder;//各题型的小序号
        //初始化swiper
        var swiper = new Swiper('.swiper-container', {
            onSlideNextStart: function () { //滑下一个移动时触发的事件
                var _index = swiper.activeIndex;
                showSmallTopic(_index);
                if (groupCountPrev == _index) {
                    groupCountPrev = groupCountPrev + nowCount;
                }
                showSmallOrder();
            },

            onSlidePrevStart: function () {//滑上一个移动时触发的事件
                var _index = swiper.activeIndex;
                showSmallTopic(_index);
                if (groupCountPrev == (_exercisePrevCount + nowCount)) {
                    groupCountPrev = _exercisePrevCount;
                }
                showSmallOrder();
            }
        });
        swiper.slideTo(indexJupm);
        //显示小题序号（这个是核心） smallOrder = nowCount - (groupCountPrev - _exercisePrevCount)
        function showSmallOrder() {
            smallOrder = nowCount - (groupCountPrev - _exercisePrevCount);
            $(".exerciseindex").text(smallOrder);
        }

        //显示小题题型
        function showSmallTopic(_index) {
            _exercisePrevCount = _index + 1;
            var _exercisetype = $(".swiper-slide").eq(_index).find(".exercisetypename").text();
            $(".exerciseindex").text(_exercisePrevCount);
            $(".exercisetype").text(_exercisetype);
            showExerciseCount(_exercisetype);

        }

        //判断是否已选
        function isHaveSelected(swiperslide, type) {
            var isSelected = false;
            if (type == singleName || type == multiName || type == judgeName) {
                swiperslide.find(".optionletter").each(function () {
                    if ($(this).hasClass("blue")) {
                        isSelected = true;
                    }
                })
            }
            if (isSelected) {
                swiperslide.addClass("haveanswer");
            }
        }

        //标记已作答
        $(".swiper-slide").each(function () {
            swiperslide = $(this);
            var _resourceType = $(this).find(".exercisetypename").text();
            switch (_resourceType) {
                case singleName: isHaveSelected(swiperslide, _resourceType); break;
                case multiName: isHaveSelected(swiperslide, _resourceType); break;
                case judgeName: isHaveSelected(swiperslide, _resourceType); break;
            }
        })
    }
    else {
        worknav.hide();
        swal({ title: data.Message },
            function () {
                MOrCFun();
            });
    }
}
//提交答题
$("#submitwork").click(function () {
    /// 提交试卷  
    getParameter();
    getNoDo();
    SaveOrCommitPaper();
    swal({
        title: "确认提交?",
        text: paperTips,
        type: "warning",
        showCancelButton: true,
        cancelButtonText: "取消",
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "提交",
        closeOnConfirm: false
    },
        function () {
            commitPaper.PaperQuestion = _newpaper;
            var jsonPaperData = JSON.stringify(commitPaper);
            var saveParams = { "JsonPaperData": jsonPaperData, "StudentID": uid }
            common.call(AppPath+'/Paper/PostSubmitStudentPaper', saveParams, 'post', submitHomeWork, error);
        });
})
//答题卡
$("#paperSheet").click(function () {
    getParameter();
    SaveOrCommitPaper();
    commitPaper.PaperQuestion = _newpaper;
    var jsonPaperData = JSON.stringify(commitPaper);
    var saveParams = { "JsonPaperData": jsonPaperData, "StudentID": uid }
    common.call(AppPath+'/Paper/PostSaveStudentPaper', saveParams, 'post', saveFun, error);
    function saveFun(data) {
        if (data.Status) {
            window.location.href = AppPath + "/Paper/answerSheet?TrainingItemCourseID=" + TrainingItemCourseID + "&TestPaperID=" + TestPaperID + "&UserExamID=" + UserExamID + "&TestType=" + TestType + "&MOrC=" + MOrC;

        }
        else {
            sweetAlert("暂存失败!", "接口错误", "error");
        }
    }
})

//暂存答题
$("#savework").click(function () {
    /// 暂存试卷  
    getParameter();
    getNoDo();
    SaveOrCommitPaper();
    swal({
        title: "确认暂存?",
        text: paperTips,
        type: "warning",
        showCancelButton: true,
        cancelButtonText: "取消",
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "确定",
        closeOnConfirm: false
    },
    function () {
        commitPaper.PaperQuestion = _newpaper;
        var jsonPaperData = JSON.stringify(commitPaper);
        var saveParams = { "JsonPaperData": jsonPaperData, "StudentID": uid };
        common.call(AppPath+'/Paper/PostSaveStudentPaper', saveParams, 'post', saveHomeWork, error);

    });
})

//试题分组
var sigleArr = [];
var multipleArr = [];
var judgeArr = [];
function topicGroups(questionsArr) {
    for (var i = 0; i < questionsArr.length; i++) {
        switch (questionsArr[i]["QuestionType"]) {
            case 1:
                sigleArr.push(questionsArr[i])
                break;
            case 2:
                multipleArr.push(questionsArr[i])
                break;
            case 4:
                judgeArr.push(questionsArr[i])
                break;
        }

    }
    questionsArr = [];
    questionsArr = questionsArr.concat(sigleArr, multipleArr, judgeArr);
    return questionsArr;
    // console.log(questionsArr);
}
