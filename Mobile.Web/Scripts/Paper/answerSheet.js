var uid = Cookies.get('cookie_userid');//用户id
Cookies.set("cookie_worker", 1);//记录跳转
var commitPaper = '';
var historyUrl = document.referrer;
//获取试卷所需参数
var TrainingItemCourseID = common.GetQueryString("TrainingItemCourseID");//培训项目课程ID
var TestPaperID = common.GetQueryString("TestPaperID");//试卷ID
var UserExamID = common.GetQueryString("UserExamID");//学生作答ID 
var TestType = common.GetQueryString("TestType");//试卷测试类型：2-在线作业；5-在线测试
var MOrC = common.GetQueryString("MOrC");//判断从那个页面跳来1m-0c

function MOrCFun() {
    if (MOrC == 1) {
        window.location.href = AppPath + '/MyTrain/Index';
    } else if (MOrC == 0) {
        window.location.href = AppPath + '/MyTrain/Detail?TrainingItemCourseID=' + TrainingItemCourseID;
    }
}



if (historyUrl.indexOf("&indexJupm")>0) {
    historyUrl = historyUrl.substring(0, historyUrl.indexOf("&indexJupm"))
}

//获取试卷参数
var params = { "TrainingItemCourseID": TrainingItemCourseID, "TestPaperID": TestPaperID, "UserExamID": UserExamID, "TestType": TestType, "StudentID": uid };
//console.log(TrainingItemCourseID + "=======---" + TestPaperID + "=======---" + OnlineTestID + "=======---" + StudentCourseID + "=======---" + TestType)
var countSingle = 0, countMulti = 0, countJudge = 0;
function answerSheet(data) {
    commitPaper = data.Data;
    var _paper = data.Data.PaperQuestion;//数组
    if (!_.isEmpty(_paper)) {
        for (i = 0; i < _paper.length; i++) {
            var _exerciseType = _paper[i]['QuestionType'];//1，2，4
            if (_exerciseType == 1) {
                $(".answerSheet>div").eq(0).show();
                countSingle += 1;
                if (!_.isEmpty(_paper[i]['UserAnswer'])) {
                    $("#answerSheet_single").append("<li><a href='" + historyUrl +"&indexJupm="+i+"'><span class='blure'>" + countSingle + "</span></a></li>");

                } else {
                    $("#answerSheet_single").append("<li><a href='" + historyUrl + "&indexJupm=" + i + "'><span>" + countSingle + "</span></a></li>");

                }
            }

            if (_exerciseType == 2) {
                $(".answerSheet>div").eq(1).show();
                countMulti += 1;
                if (!_.isEmpty(_paper[i]['UserAnswer'])) {
                    $("#answerSheet_multi").append("<li><a href='" + historyUrl + "&indexJupm=" + i + "'><span class='blure'>" + countMulti + "</span></a></li>");

                } else {
                    $("#answerSheet_multi").append("<li><a href='" + historyUrl + "&indexJupm=" + i + "'><span>" + countMulti + "</span></a></li>");

                }
            }

            if (_exerciseType == 4) {
                $(".answerSheet>div").eq(2).show();
                countJudge += 1;
                if (!_.isEmpty(_paper[i]['UserAnswer'])) {
                    $("#answerSheet_judge").append("<li><a href='" + historyUrl + "&indexJupm=" + i + "'><span class='blure'>" + countJudge + "</span></a></li>");

                } else {
                    $("#answerSheet_judge").append("<li><a href='" + historyUrl + "&indexJupm=" + i + "'><span>" + countJudge + "</span></a></li>");

                }
            }



        }
    }
}
function init(){ 
    common.call(AppPath+"/Paper/GetStudentPaperResult", params, 'get', function (data) {
        answerSheet(data);
    }, error)}
init();
//提交成功的提醒
function submitHomeWork(data) {
    if (data.Status) {
        swal("提交成功！", "", "success");
        setInterval(function () {
            //window.location.href = AppPath+ "/Paper/TestResult?TrainingItemCourseID=" + TrainingItemCourseID + "&TestPaperID=" + TestPaperID + "&UserExamID=" + UserExamID + "&TestType=" + TestType;
            //window.location.href = AppPath+ "/MyTrain/Index";
            //window.history.go(-2);
            MOrCFun();
        }, 1000);
      
    }
    else {
        sweetAlert("提交失败!", "接口错误", "error");
    }

}
//提交 
$("#commitbtn").click(function () {
    commitPaper = JSON.stringify(commitPaper);
    var saveParams = { "JsonPaperData": commitPaper, "StudentID": uid }
    common.call(AppPath+'/Paper/PostSubmitStudentPaper', saveParams, 'post', submitHomeWork, error);
})