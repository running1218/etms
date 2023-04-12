Cookies.set("cookie_worker", 1);
//获取试卷所需参数
var TrainingItemCourseID = common.GetQueryString("TrainingItemCourseID");//培训项目课程ID
var TestPaperID = common.GetQueryString("TestPaperID");//试卷ID
var UserExamID = common.GetQueryString("UserExamID");//学生作答ID 
var TestType = common.GetQueryString("TestType");//试卷测试类型：2-在线作业；5-在线测试
var uid = Cookies.get('cookie_userid');//用户id
//获取试卷参数
var params = { "TrainingItemCourseID": TrainingItemCourseID, "TestPaperID": TestPaperID, "UserExamID": UserExamID, "TestType": TestType, "StudentID": uid };
//转换题型class
Handlebars.registerHelper("tfTopicType", function (value) {
    switch (value) {
        case 1: return "singletopic";
        case 2: return "multitopic";
        case 4: return "judgetopic";
    }
});
var tmpl_typeName = '';
Handlebars.registerHelper("typeName", function (value) {
    if (tmpl_typeName == value) {
        return '';
    } else {
        tmpl_typeName = value;
        switch (value) {
            case 1: return "【单选题】";
            case 2: return "【多选题】";
            case 4: return "【判断题】";
        }
    }
    
});
Handlebars.registerHelper("topicIndex", function (value) {
    return value + 1;
});
Handlebars.registerHelper("jsonObj", function (value) {
    if (typeof value == "string") {
        value = JSON.parse(value);
         if (value.constructor === Array) {
            var str = '';
            for (var i = 0; i < value.length; i++) {
                str += value[i].OptionCode;
            }
            return str;
        } else {
            return value.OptionCode;
        }
    }
   // return value;
});
function init() {
    common.call(AppPath + "/Paper/GetStudentPaperResult", params, 'get', function (data) {
        testResult(data);
    }, error)
}
init();

//主函数
function testResult(data) {
    // console.log(data)
    if (data.Status) {
        var _newpaper = topicGroups(data.Data.PaperQuestion);//对象数组
        var _newpaperObj = { "Data": _newpaper };
        var template = Handlebars.compile($("#testResult_templ").html());
        var renderHTML = template(_newpaperObj);
        $(".exerciselist").html(renderHTML);
        $(".test-name").html(data.Data.TestPaperName);
        $("#UserScore").html(data.Data.UserScore);
    }
    else {
        swal({ title: data.Message },
            function () {
              //  window.history.go(-1);
            });
    }
}

//试题分组
var sigleArr = [];
var multipleArr = [];
var judgeArr = [];
function topicGroups(questionsArr) {
    for (var i = 0; i < questionsArr.length;i++){
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