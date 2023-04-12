var MOrC = common.GetQueryString("MOrC");//判断从那个页面跳来1m-0c
Cookies.set("cookie_worker", 1);//记录跳转
var answer = [];
var singleName = 1// "单选题";
var multiName = 2// "多选题";
var judgeName = 4// "判断题";
var TrainingItemCourseID = common.GetQueryString("TrainingItemCourseID");
var CourseID = common.GetQueryString("CourseID");//课程ID
var exercisecount = $(".exercisecount");
//点击选项
$(".swiper-wrapper").on("click", ".workoption", function () {
    var _this = $(this);
    setSelected(_this);
})
//转换题型class
Handlebars.registerHelper("tfTopicType", function (value) {
    switch (value) {
        case singleName: return "singletopic";
        case multiName: return "multitopic";
        case judgeName: return "judgetopic";
    }
});
//是否答题
Handlebars.registerHelper("EqualLength", function (v1, v2, options) {
    if (v1.length == v2) {
        //满足添加继续执行
        return options.fn(this);
    } else {
        //不满足条件执行{{else}}部分
        return options.inverse(this);
    }
});

Handlebars.registerHelper("typeName", function (value) {
    switch (value) {
        case singleName: return "单选题";
        case multiName: return "多选题";
        case judgeName: return "判断题";
    }
});
//显示小题数量
function showExerciseCount(_exercisetype) {
    if (_exercisetype == singleName) {
        exercisecount.text(countSingle);
        nowCount = countSingle;
    }
    if (_exercisetype == multiName) {
        exercisecount.text(countMulti);
        nowCount = countMulti;
    }
    if (_exercisetype == judgeName) {
        exercisecount.text(countJudge);
        nowCount = countJudge;
    }

}
//设置选择的样式
function setSelected(option) {
    var _exerciseType = option.parent().find(".exercisetypename").text();
    if (_exerciseType == "多选题") {
        //option.parent().find(".optionletter").removeClass("blue");

        if (option.find(".optionletter").hasClass("blue")) {
            option.find(".optionletter").removeClass("blue");
        }
        else {
            option.find(".optionletter").addClass("blue");
        }
    }
    else {
        if (option.find(".optionletter").hasClass("blue")) {
            option.find(".optionletter").removeClass("blue");
        }
        else {
            option.parent().find(".optionletter").removeClass("blue");
            option.find(".optionletter").addClass("blue");
        }

    }
}
//暂存成功的提醒
function saveHomeWork(data) {
    if (data.Status) {
        swal("暂存成功！", "", "success");
    }
    else {
        sweetAlert("暂存失败!", "接口错误", "error");
    }
}
//提交成功的提醒
function submitHomeWork(data) {
    if (data.Status) {
        swal("提交成功！", "", "success");
       // submitwork.remove();//移除提交按钮
        // savework.remove();//移除暂存按钮 
        setTimeout(function () {
            if (MOrC == 1) {
                window.location.href = AppPath + '/MyTrain/Index';
            } else if (MOrC == 0) {
                window.location.href = AppPath + '/MyTrain/Detail?TrainingItemCourseID=' + TrainingItemCourseID;
            }
            else if (MOrC == 2) {
                window.location.href = AppPath + '/MyTrain/LivingStudy?TrainingItemCourseID=' + TrainingItemCourseID + '&CourseID=' + CourseID;
            }
        }, 2000)
    }
    else {
        sweetAlert("提交失败!", "接口错误", "error");
    }
    
}
//function tfNumber(value) {
//    switch (value) {
//        case "A": return 0;
//        case "B": return 1;
//        case "C": return 2;
//        case "D": return 3;
//        case "E": return 4;
//        case "F": return 5;
//        case "G": return 6;
//        case "H": return 7;
//    }
//}

//单选题答题集合
function getSingleAnswer($this) {
    var _topicFlag = 0;
    var QuestionID = $this.attr("topicid");
    var obj = {};
    obj.QuestionID = QuestionID;
    obj.OptionID = '';
    $this.find(".optionletter").each(function (index,val) {
        var option = $(this);
        if (option.hasClass("blue")) {
            _topicFlag++;
            obj.OptionID = option.attr("data-OptionID");
           // singleAnswer.push(obj);
        }
    })
    answer.push(obj);
    if (_topicFlag > 0) {
        $this.addClass("haveanswer");
    }
    else {
        $this.removeClass("haveanswer");
    }
}
//多选题答题集合
function getMultipleAnswer($this) {
    var _topicFlag = 0;
    var QuestionID = $this.attr("topicid");
    var obj = {};
    var answ ='';
    obj.QuestionID = QuestionID;
    obj.OptionID = '';
    $this.find(".optionletter").each(function (index, val) {
        var option = $(this);
        if (option.hasClass("blue")) {
            _topicFlag++;
            answ += option.attr("data-OptionID")+",";
        }           
    })
    answ = answ.substr(0, answ.length - 1);
    obj.OptionID = answ;
   // multipleAnswer.push(obj);
    answer.push(obj);
    if (_topicFlag > 0) {
        $this.addClass("haveanswer");
    }
    else {
        $this.removeClass("haveanswer");
    }
}
//判断题答题集合
function getJudgementAnswer($this) {
    var _topicFlag = 0;
    var QuestionID = $this.attr("topicid");
    var obj = {};
    obj.QuestionID = QuestionID;
    obj.OptionID = '';
    $this.find(".optionletter").each(function (index, val) {
        var option = $(this);
        if (option.hasClass("blue")) {
            _topicFlag++;
            obj.OptionID = option.attr("data-OptionID");
          //  judgementAnswer.push(obj);
        }

    })
    answer.push(obj);
    if (_topicFlag > 0) {
        $this.addClass("haveanswer");
    }
    else {
        $this.removeClass("haveanswer");
    }
}
//获取答题结果
function getAnswer(swiperslide) {
    var _resourceType = swiperslide.find(".exercisetypename").text();
    switch (_resourceType) {
        case "单选题": getSingleAnswer(swiperslide); break;
        case "多选题": getMultipleAnswer(swiperslide); break;
        case "判断题": getJudgementAnswer(swiperslide); break;
    }
}
//获取提交和暂存的作答结果
function getParameter() {
    singleAnswer = [];//单选题答题结果
    multipleAnswer = [];//多选题答题结果
    judgementAnswer = [];//判断题答题结果
    $(".swiper-slide").each(function () {
        $this = $(this);
        getAnswer($this);
    });
 
   // console.log(singleAnswer);
    //console.log(multipleAnswer);
    //console.log(judgementAnswer);
    console.log(answer);
    console.log(_newpaper);
}
//获取未做题序号
function getNoDoTopicIndex(topic, i, type) {
    if (!topic.hasClass("haveanswer")) {
        if (type == singleName) {
            noDoSingle.push(i + 1);
        }
        if (type == multiName) {
            noDoMulti.push(i + 1);
        }
        if (type == judgeName) {
            noDoJudge.push(i + 1);
        }
        
    }
}
//提示还有哪些题目未做
function getNoDo() {
    paperTips = "";
    noDoSingle = [];//未做的单选题
    noDoMulti = [];//未做的多选题
    noDoJudge = [];//未做的判断题
    var _singleTip = "";
    var _multiTip = "";
    var _judgeTip = "";
    $(".singletopic").each(function (i) {
        var _topic = $(this);
        getNoDoTopicIndex(_topic, i, singleName);
    })
    $(".multitopic").each(function (i) {
        var _topic = $(this);
        getNoDoTopicIndex(_topic, i, multiName);
    })
    $(".judgetopic").each(function (i) {
        var _topic = $(this);
        getNoDoTopicIndex(_topic, i, judgeName);
    })
     
    if (noDoSingle.length > 0) {
        _singleTip = "第" + noDoSingle.join("、") + "道单选题";
    }
    if (noDoMulti.length > 0) {
        _multiTip = "第" + noDoMulti.join("、") + "道多选题";
    }
    if (noDoJudge.length > 0) {
        _judgeTip = "第" + noDoJudge.join("、") + "道判断题";
    }
     
    var _newTips = [];
    _newTips.push(_singleTip); _newTips.push(_multiTip); _newTips.push(_judgeTip); 
    _newTips = _.compact(_newTips); //过滤空值
    if (noDoSingle.length == 0 && noDoMulti.length == 0 && noDoJudge.length == 0 ) {

    }
    else {
        paperTips = "您还有：" + _newTips.join("，") + "未答。";
    }
}
//提交保存的作答后的新试卷
function SaveOrCommitPaper() {
        for (var i = 0; i < answer.length; i++) {
            for (var j = 0; j < _newpaper.length; j++) {
                if (_newpaper[j].QuestionID == answer[i].QuestionID) {
                    _newpaper[j].UserAnswer = answer[i].OptionID;
                }  
            }
        }
      //  console.log(_newpaper);
}
