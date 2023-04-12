//注册一个翻译用的Helper，
Handlebars.registerHelper("tfDateTime", function (value) {
    var datetemp = formatDate(value, "yyyy-MM-dd HH:mm:ss");
    if (datetemp == null) {
        datetemp = "";
    }
    return datetemp;
});
//注册一个翻译用的Helper，
Handlebars.registerHelper("tfDate", function (value) {
    var datetemp = formatDate(value, "yyyy-MM-dd");
    if (datetemp == null) {
        datetemp = "";
    }
    return datetemp;
});
//转换选项编号
Handlebars.registerHelper("tfLetter", function (value) {
    switch (value) {
        case 0: return "A";
        case 1: return "B";
        case 2: return "C";
        case 3: return "D";
        case 4: return "E";
        case 5: return "F";
        case 6: return "G";
        case 7: return "H";
    }
});
//判断相等
Handlebars.registerHelper("Equal", function (v1, v2, options) {
    if (_array.isArray(v1) && _array.isArray(v2)) {
        if (v1.join(',') == v2.join(',')) {
            //满足添加继续执行
            return options.fn(this);
        } else {
            //不满足条件执行{{else}}部分
            return options.inverse(this);
        }
    }
    else {
        if (v1 == v2) {
            //满足添加继续执行
            return options.fn(this);
        } else {
            //不满足条件执行{{else}}部分
            return options.inverse(this);
        }
    }
});
Handlebars.registerHelper("Equa2", function (v1, v2, value) {
    var timeOne = (new Date(v1)).getTime();
    var timeTwo = (new Date(v2)).getTime();
    var nowTime = new Date().getTime();
    if (nowTime < timeOne) {
        return 0;
    } else if (nowTime > timeTwo) {
        return 1;
    } else {
        return value;
    }
});
//注册翻译用的Helper 关于学习评测，
Handlebars.registerHelper("TrainTestName", function (value) {
    var datetemp = '';
    if (value == 2) {
        datetemp = "作业";
    } else if (value == 5) {
        datetemp = "测试";
    }
    return datetemp;
});
Handlebars.registerHelper("TrainTestClass", function (value) {
    var datetemp = '';
    if (value == 2) {
        datetemp = "icon iconfont  icon-iconfont-zy";
    } else if (value == 5) {
        datetemp = "test icon iconfont  icon-iconfont-cs";
    }
    return datetemp;
});
Handlebars.registerHelper("Equal", function (v1, v2, options) {
    if (v1 == v2) {
        //满足添加继续执行
        return options.fn(this);
    } else {
        //不满足条件执行{{else}}部分
        return options.inverse(this);
    }
});
Handlebars.registerHelper("StudyStatusJudge", function (value,v2) {
    var datetemp = '';
    if (v2 == 1) {
        if (value == 1) {
            datetemp = "endStatus  icon-iconfont-videoq";
        } else if (value == 0) {
            datetemp = "studyingStatus   icon-iconfont-videob";
        } else {
            datetemp = 'icon-iconfont-video';
        };
    } else if (v2 == 2) {
        if (value == 1) {
            datetemp = "endStatus  icon-iconfont-Filevq";
        } else if (value == 0) {
            datetemp = "studyingStatus  icon-iconfont-Filevb";
        } else {
            datetemp = 'icon-iconfont-File';
        };
    }
    return datetemp;
});
//范围比较
Handlebars.registerHelper("EqualNumber", function (v1, v2, options) {
    if (v1 >= v2) {
        //满足添加继续执行
        return options.fn(this);
    } else {
        //不满足条件执行{{else}}部分
        return options.inverse(this);
    }
});
//绑定作答结果
Handlebars.registerHelper("answerClass", function (v1, v2, v3) {
    var answewrArr = v1.split(",");
    if (answewrArr.length==1) {
        if (answewrArr[0] == v2) {
            return "optionletter blue";
        }
    } else {
        for (var i = 0; i < answewrArr.length; i++) {
            if (answewrArr[i] == v2) {
                return "optionletter blue";
            }
        }
       
    }
    return "optionletter";
});
//Handlebars区分视频，PDF
Handlebars.registerHelper("TypeIcon", function (value) {
    var datetemp = "";
    if (value == 1) {
        datetemp = "icon-iconfont-video"
    } else if (value == 2) {
        datetemp = "icon-iconfont-File"
    }
    return datetemp;
});
//做作业和查看作业是否显示
Handlebars.registerHelper("compare", function (v1, v2, options) {
    var nowDate = common.formatDate(new Date(), "yyyy-MM-dd");
    if (nowDate > v1) {
        //满足添加继续执行
        return options.fn(this);
    } else {
        if (v2 != "已提交") {
            //不满足条件执行{{else}}部分
            return options.inverse(this);
        }
    }
});
//开始测试和查看测试是否显示
Handlebars.registerHelper("compare1", function (v1, v2, v3, options) {
    var nowDate = common.formatDate(new Date(), "yyyy-MM-dd");
    if (nowDate > v1) {
        //满足添加继续执行
        return options.fn(this);
    } else {
        if (v2 > v3) {
            //不满足条件执行{{else}}部分
            return options.inverse(this);
        }
    }
});

Handlebars.registerHelper("IsOpenJudge", function (value, v2) {
    var datetemp = '';
    if (v2 == 1) {
        if (value == 1) {
            datetemp = "endStatus icon-iconfont-videoq";
        } else {
            datetemp = 'icon-iconfont-video';
        };
    } else if (v2 == 2) {
        if (value == 1) {
            datetemp = "endStatus  icon-iconfont-Filevq";
        } else if (value == 0) {
            datetemp = "studyingStatus  icon-iconfont-Filevb";
        } else {
            datetemp = 'icon-iconfont-File';
        };
    }
    return datetemp;
});