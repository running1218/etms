var totalscore = 100

//删除试题方法 obj为tr的ID号
function deloption(obj) {

    var delObj = $("#" + obj);
    //判断是否为最后一个  

    if (obj == delObj.parent().find("tr:last").attr("id"))
        delObj.remove();
    else
        popAlertMsg("请从最后一个选项开始删除！", "友情提示");

}
//增加试题的方法,参数type参数值single-单选,multi-多选
function addoption(type) {
    var letterlist = new Array("A", "B", "C", "D", "E", "F", "G", "H", "I", "J");
    if (type == "single") {
        var totalNumbers = $("input[type='radio']").length;
    }
    else if (type == "multi") {
        var totalNumbers = $("input[type='checkbox']").length;
    }

    if (totalNumbers >= letterlist.length) {
        popAlertMsg("最多只能添加10个值!不能再增加了！", "友情提示");
        return
    }
    if (type == "single") {
        var trhtml = " <tr id='tr_" + totalNumbers.toString() + "'>"
           + " <td class='center'>"
             + " <span id='rbtnOption_" + totalNumbers.toString() + "_radio'" + "onfocus='blur()' class='radiostyle'><input type='radio' name='myradio' id='rbtnOption_" + totalNumbers.toString() + "' style='opacity: 0; border: none;'></span>"
                + "<input type='hidden' value='' id='txtOptionID_" + totalNumbers.toString() + "'>"
           + " </td>"
          + "  <td class='center'>"
           + "  <span>" + letterlist[parseInt(totalNumbers)]
                + "</span>"
            + "</td>"
            + "<td class='center'>"
                 + "<textarea rows='5' class='inputbox_area360 inputbox_area360_hight' id='txtOptionContent_" + totalNumbers.toString() + "'></textarea>"
            + "</td><td width='40'><a href=\"javascript:deloption('tr_" + totalNumbers.toString() + "')\">删除</a></td></tr>";
        $("input[type='radio']:last").parent().parent().parent().parent().append(trhtml);
    }
    if (type == "multi") {
        var trhtml = " <tr id='tr_" + totalNumbers.toString() + "'>"
           + " <td class='center'>"
             + " <span id='checkOption_" + totalNumbers.toString() + "_checkbox'" + "onfocus='blur()' class='checkboxstyle'><input type='checkbox' name='myradio' id='checkOption_" + totalNumbers.toString() + "' style='opacity: 0; border: none;'></span>"
                + "<input type='hidden' value='' id='txtOptionID_" + totalNumbers.toString() + "'>"
           + " </td>"
          + "  <td class='center'>"
           + "  <span>" + letterlist[parseInt(totalNumbers)]
                + "</span>"
            + "</td>"
            + "<td class='center'>"
                 + "<textarea rows='5' class='inputbox_area360 inputbox_area360_hight' id='txtOptionContent_" + totalNumbers.toString() + "'></textarea>"
            + "</td><td width='40'><a href=\"javascript:deloption('tr_" + totalNumbers.toString() + "')\">删除</a></td></tr>";
        $("input[type='checkbox']:last").parent().parent().parent().parent().append(trhtml);

    }
    $("input").radioStyle();
}
//保存答案type参数值single-单选,multi-多选
function saveFunoption(type, id) {
    var strResult = "";
    var havingAnswer = false;  //是否有答案
    var isNull = false;    //是否有空的选项未填
    var isLong = false;    //字符长度小于500中文

    if (type == "single") {
        $("input[type='radio']").each(function () {
            if ($(this).parent().parent().parent().find("input[type='radio']").attr("checked") == "checked" || $(this).parent().parent().parent().find("input[type='radio']").attr("checked") == true) {
                strResult += "true" + "Ω";
                havingAnswer = true;
            }
            else
                strResult += "false" + "Ω";
            strResult += $(this).parent().parent().parent().find("input[type='hidden']").val() + "Ω";
            strResult += $(this).parent().parent().parent().find("textarea").val() + "Φ";

            if ($(this).parent().parent().parent().find("textarea").val() == "") {
                isNull = true;
            }
            if ($(this).parent().parent().parent().find("textarea").val().length > 500) {
                isLong = true;
            }

        });
    }
    else if (type == "multi") {
        $("input[type='checkbox']").each(function () {
            if ($(this).parent().parent().parent().find("input[type='checkbox']").attr("checked") == "checked" || $(this).parent().parent().parent().find("input[type='checkbox']").attr("checked") == true) {
                strResult += "true" + "Ω";
                havingAnswer = true;
            }
            else
                strResult += "false" + "Ω";
            strResult += $(this).parent().parent().parent().find("input[type='hidden']").val() + "Ω";
            strResult += $(this).parent().parent().parent().find("textarea").val() + "Φ";

            if ($(this).parent().parent().parent().find("textarea").val() == "") {
                isNull = true;
            }
            if ($(this).parent().parent().parent().find("textarea").val().length > 500) {
                isLong = true;
            }

        });
    }
    //alert("传入的id:" + id + "返回结果strResult:" + strResult);

    $("#" + id).val(strResult.substring(0, strResult.length - 1));

    if (havingAnswer == false) {
        alert("请选择一个正确答案！");
        return false;
    }
    if (isNull == true) {
        alert("选项内容不能为空！");
        return false;
    }
    if (isLong == true) {
        alert("选项内容长度不能超过500个中文！");
        return false;
    }

    return true;
}

//手工选题层控制
function ItemSelectDiv() {
    if ($("#aItemSelectDiv").text() == "展开") {
        $("#ItemSelectDiv").show();
        $("#aItemSelectDiv").text("收缩");
    }
    else {
        $("#ItemSelectDiv").hide();
        $("#aItemSelectDiv").text("展开");
    }
}

//手工出题，批量设置分数
function setScoreBatch() {
    var questionType = $("#select_QuestionType").val();
    var scoreBatch = $("#txtScoreBatch").val();

    $("#div_itemSelected").find("input[type='text']").each(function () {
        if ($(this).attr("title").indexOf(questionType) > -1) {
            $(this).val(scoreBatch);
        }
    });
    SumScore();
}

//合计总分
function SumScore() {
    var scoreSum = 0;

    $("#div_itemSelected").find("input[type='text']").each(function () {
        if ($(this).val() == "")
            $(this).val("0");

        var strVal = $(this).val();

        if (checkNum(strVal) == false) {
            popAlertMsg("分数必须为数字或带一位小数的数值！", "友情提示");
            $(this).val($(this).val().substr(0, $(this).val().length - 1))
            return;
        }
        scoreSum += parseFloat($(this).val());

    });
    $("#spanScoreCount").text(returnFloat1(scoreSum));
}

function returnFloat1(value) { //保留一位小数点
    value = Math.round(parseFloat(value) * 10) / 10;
    if (value.toString().indexOf(".") < 0)
        value = value.toString() + ".0";
    return value;
}



//判断是否是数字
function checkNum(str) {
    var reg = /^[0-9]{0,6}(\.\d{1})?$/;
    return reg.test(str);
    return true;
}

//满分是否 totalscore
function checkHundredScore() {
    if ($("#spanScoreCount").text() != totalscore) {
        popAlertMsg("满分必须是" + totalscore + "分！", "友情提示");
        return false;
    }
    return true;
}

//策略出题保存按钮：满分是否 totalscore
function checkltlQuestionScoreSum(obj) {
    if ($("#ltlQuestionScoreSum").text() != totalscore) {
        popAlertMsg("满分必须是" + totalscore + "分！", "友情提示");
        return false;
    }
    var SingleChoice = "SingleChoice," + $("#txtSingleChoiceEasy").val() + "," + $("#txtSingleChoiceMedium").val() + "," + $("#txtSingleChoiceHard").val() + "," + $("#txtSingleChoiceScore").val();
    var MultipleChoice = "MultipleChoice," + $("#txtMultipleChoiceEasy").val() + "," + $("#txtMultipleChoiceMedium").val() + "," + $("#txtMultipleChoiceHard").val() + "," + $("#txtMultipleChoiceScore").val();
    var Judgement = "Judgement," + $("#txtJudgementEasy").val() + "," + $("#txtJudgementMedium").val() + "," + $("#txtJudgementHard").val() + "," + $("#txtJudgementScore").val();

    $("#" + obj).val(SingleChoice + ";" + MultipleChoice + ";" + Judgement);
    //SingleChoice,2,1,1,10;MultipleChoice,2,1,1,10;Judgement,1,1,0,10

    return true;
}

//策略出题，设置题目数
function setQuestionCount() {
    checkChooseNum();
    checkSingleChoice();
    checkMultipleChoice();
    checkJudgement();

    checkScore();
    QuestionScoreSum();

}

//合计总分
function QuestionScoreSum() {
    var ltlQuestionSum = Number($("#ltlSingleChoiceSum").text()) + Number($("#ltlMultipleChoiceSum").text()) + Number($("#ltlJudgementSum").text());

    $("#ltlQuestionSum").text(ltlQuestionSum);

    var ltlQuestionScoreSum = Number($("#ltlSingleChoiceSum").text()) * Number($("#txtSingleChoiceScore").val()) + Number($("#ltlMultipleChoiceSum").text()) * Number($("#txtMultipleChoiceScore").val()) + Number($("#ltlJudgementSum").text()) * Number($("#txtJudgementScore").val())

    $("#ltlQuestionScoreSum").text(ltlQuestionScoreSum);

}

function checkScore() {
    //分数 数字校验
    if (checkNum($("#txtSingleChoiceScore").val()) == false) {
        popAlertMsg("单选题分数必须为数字！", "友情提示");
        $("#txtSingleChoiceScore").val($("#txtSingleChoiceScore").val().substr(0, $("#txtSingleChoiceScore").val().length - 1));
    }

    if (checkNum($("#txtMultipleChoiceScore").val()) == false) {
        popAlertMsg("多选题分数必须为数字！", "友情提示");
        $("#txtMultipleChoiceScore").val($("#txtMultipleChoiceScore").val().substr(0, $("#txtMultipleChoiceScore").val().length - 1));
    }

    if (checkNum($("#txtJudgementScore").val()) == false) {
        popAlertMsg("判断题分数必须为数字！", "友情提示");
        $("#txtJudgementScore").val($("#txtJudgementScore").val().substr(0, $("#txtJudgementScore").val().length - 1));
    }
}

//数字校验
function checkChooseNum() {

    //单选题数字校验
    if (checkNum($("#txtSingleChoiceEasy").val()) == false) {
        popAlertMsg("单选题（易）抽取数必须为数字！", "友情提示");
        $("#txtSingleChoiceEasy").val($("#txtSingleChoiceEasy").val().substr(0, $("#txtSingleChoiceEasy").val().length - 1));
    }

    if (checkNum($("#txtSingleChoiceMedium").val()) == false) {
        popAlertMsg("单选题（中）抽取数必须为数字！", "友情提示");
        $("#txtSingleChoiceMedium").val($("#txtSingleChoiceMedium").val().substr(0, $("#txtSingleChoiceMedium").val().length - 1));
    }

    if (checkNum($("#txtSingleChoiceHard").val()) == false) {
        popAlertMsg("单选题（难）抽取数必须为数字！", "友情提示");
        $("#txtSingleChoiceHard").val($("#txtSingleChoiceHard").val().substr(0, $("#txtSingleChoiceHard").val().length - 1));
    }

    //多选题数字校验
    if (checkNum($("#txtMultipleChoiceEasy").val()) == false) {
        popAlertMsg("多选题（易）抽取数必须为数字！", "友情提示");
        $("#txtMultipleChoiceEasy").val($("#txtMultipleChoiceEasy").val().substr(0, $("#txtMultipleChoiceEasy").val().length - 1));
    }

    if (checkNum($("#txtMultipleChoiceMedium").val()) == false) {
        popAlertMsg("多选题（中）抽取数必须为数字！", "友情提示");
        $("#txtMultipleChoiceMedium").val($("#txtMultipleChoiceMedium").val().substr(0, $("#txtMultipleChoiceMedium").val().length - 1));
    }

    if (checkNum($("#txtMultipleChoiceHard").val()) == false) {
        popAlertMsg("多选题（难）抽取数必须为数字！", "友情提示");
        $("#txtMultipleChoiceHard").val($("#txtMultipleChoiceHard").val().substr(0, $("#txtMultipleChoiceHard").val().length - 1));
    }

    //判断题数字校验
    if (checkNum($("#txtJudgementEasy").val()) == false) {
        popAlertMsg("判断题（易）抽取数必须为数字！", "友情提示");
        $("#txtJudgementEasy").val($("#txtJudgementEasy").val().substr(0, $("#txtJudgementEasy").val().length - 1));
    }

    if (checkNum($("#txtJudgementMedium").val()) == false) {
        popAlertMsg("判断题（中）抽取数必须为数字！", "友情提示");
        $("#txtJudgementMedium").val($("#txtJudgementMedium").val().substr(0, $("#txtJudgementMedium").val().length - 1));
    }

    if (checkNum($("#txtJudgementHard").val()) == false) {
        popAlertMsg("多选题（难）抽取数必须为数字！", "友情提示");
        $("#txtJudgementHard").val($("#txtJudgementHard").val().substr(0, $("#txtJudgementHard").val().length - 1));
    }

}

//抽取数数量校验（单选题）
function checkSingleChoice() {
    //抽取数数量校验（单选题）
    var txtSingleChoiceEasy = $("#txtSingleChoiceEasy").val();
    var ltlSingleChoiceEasySum = $("#ltlSingleChoiceEasySum").text();

    if (Number(ltlSingleChoiceEasySum) < Number(txtSingleChoiceEasy)) {
        popAlertMsg("单选题（易）抽取数不允许大于试题数量！", "友情提示");
        $("#txtSingleChoiceEasy").val(ltlSingleChoiceEasySum);
    }

    var txtSingleChoiceMedium = $("#txtSingleChoiceMedium").val();
    var ltlSingleChoiceMediumSum = $("#ltlSingleChoiceMediumSum").text();

    if (Number(ltlSingleChoiceMediumSum) < Number(txtSingleChoiceMedium)) {
        popAlertMsg("单选题（中）抽取数不允许大于试题数量！", "友情提示");
        $("#txtSingleChoiceMedium").val(ltlSingleChoiceMediumSum);
    }

    var txtSingleChoiceHard = $("#txtSingleChoiceHard").val();
    var ltlSingleChoiceHardSum = $("#ltlSingleChoiceHardSum").text();

    if (Number(ltlSingleChoiceHardSum) < Number(txtSingleChoiceHard)) {
        popAlertMsg("单选题（难）抽取数不允许大于试题数量！", "友情提示");
        $("#txtSingleChoiceHard").val(ltlSingleChoiceHardSum);
    }

    var ltlSingleChoiceSum = Number($("#txtSingleChoiceEasy").val()) + Number($("#txtSingleChoiceMedium").val()) + Number($("#txtSingleChoiceHard").val());

    //抽取总数（单选题）
    $("#ltlSingleChoiceSum").text(ltlSingleChoiceSum);

}

function checkMultipleChoice() {
    //抽取数数量校验（多选题）
    var txtMultipleChoiceEasy = $("#txtMultipleChoiceEasy").val();
    var ltlMultipleChoiceEasySum = $("#ltlMultipleChoiceEasySum").text();

    if (Number(ltlMultipleChoiceEasySum) < Number(txtMultipleChoiceEasy)) {
        popAlertMsg("多选题（易）抽取数不允许大于试题数量！", "友情提示");
        $("#txtMultipleChoiceEasy").val(ltlMultipleChoiceEasySum);
    }

    var txtMultipleChoiceMedium = $("#txtMultipleChoiceMedium").val();
    var ltlMultipleChoiceMediumSum = $("#ltlMultipleChoiceMediumSum").text();

    if (Number(ltlMultipleChoiceMediumSum) < Number(txtMultipleChoiceMedium)) {
        popAlertMsg("多选题（中）抽取数不允许大于试题数量！", "友情提示");
        $("#txtMultipleChoiceMedium").val(ltlMultipleChoiceMediumSum);
    }

    var txtMultipleChoiceHard = $("#txtMultipleChoiceHard").val();
    var ltlMultipleChoiceHardSum = $("#ltlMultipleChoiceHardSum").text();

    if (Number(ltlMultipleChoiceHardSum) < Number(txtMultipleChoiceHard)) {
        popAlertMsg("多选题（难）抽取数不允许大于试题数量！", "友情提示");
        $("#txtMultipleChoiceHard").val(ltlMultipleChoiceHardSum);
    }

    var ltlMultipleChoiceSum = Number($("#txtMultipleChoiceEasy").val()) + Number($("#txtMultipleChoiceMedium").val()) + Number($("#txtMultipleChoiceHard").val());

    //抽取总数（多选题）
    $("#ltlMultipleChoiceSum").text(ltlMultipleChoiceSum);
}

function checkJudgement() {
    //    //抽取数数量校验（判断题）
    var txtJudgementEasy = $("#txtJudgementEasy").val();
    var ltlJudgementEasySum = $("#ltlJudgementEasySum").text();

    if (Number(ltlJudgementEasySum) < Number(txtJudgementEasy)) {
        popAlertMsg("判断题（易）抽取数不允许大于试题数量！", "友情提示");
        $("#txtJudgementEasy").val(ltlJudgementEasySum);
    }

    var txtJudgementMedium = $("#txtJudgementMedium").val();
    var ltlJudgementMediumSum = $("#ltlJudgementMediumSum").text();

    if (Number(ltlJudgementMediumSum) < Number(txtJudgementMedium)) {
        popAlertMsg("判断题（中）抽取数不允许大于试题数量！", "友情提示");
        $("#txtJudgementMedium").val(ltlJudgementMediumSum);
    }

    var txtJudgementHard = $("#txtJudgementHard").val();
    var ltlJudgementHardSum = $("#ltlJudgementHardSum").text();

    if (Number(ltlJudgementHardSum) < Number(txtJudgementHard)) {
        popAlertMsg("判断题（难）抽取数不允许大于试题数量！", "友情提示");
        $("#txtJudgementHard").val(ltlJudgementHardSum);
    }

    var ltlJudgementSum = Number($("#txtJudgementEasy").val()) + Number($("#txtJudgementMedium").val()) + Number($("#txtJudgementHard").val());

    //抽取总数（判断题）
    $("#ltlJudgementSum").text(ltlJudgementSum);
}