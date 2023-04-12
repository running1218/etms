Cookies.set("cookie_worker", 2);//问答
var isFirst = true;
var LastAnswerID = null;

function GetAnswerList(LastAnswerID) {
    var params = { "QuestionID": QuestionID, "PageSize": 5, "LastAnswerID": LastAnswerID };
    common.call(AppPath+"/MyTrain/GetAnswerList", params, 'get', function (data) {
        if (data.Status) {
            Handlebars.registerHelper("formatDate", function (date, options) {
                return common.formatDate(date, 'yyyy-MM-dd')
            });
            Handlebars.registerHelper("removeLabel", function (str, options) {
                return str.replace(/<.*?>/ig, "")
            });
            var template = Handlebars.compile($("#Answer_List").html());
            var renderHTML = template(data.Data);
            if (LastAnswerID) {
                $('.bottom_content').append(renderHTML);
            }
            else {
                $('.bottom_content').html(renderHTML);
                $('.bottom_content').prepend('<li><div class="tit_content"></div></li>');
                template = Handlebars.compile($("#Question_Detail").html());
                renderHTML = template(data.Data);
                $('.tit_content').html(renderHTML);
            }
            if ($('#question_list  .discuss_question').length == 0) {
                $('.question_nocontent').show();
            } else {
                $('.question_nocontent').hide();
            }
        } else {
            if (isFirst) {
                $('.searchNone').show();
                isFirst = false;
            }
            layer.msg(data.Message);
        }
    }, error);
}

function Up() {
    setTimeout(function () {
        isFirst = false;
        LastAnswerID = $('.bottom_content li:last').attr('AnswerID');
        GetAnswerList(LastAnswerID);
        setTimeout(function () {
            wrapper.refresh();
        }, 200)
    }, 500);
}

function Down() {
    setTimeout(function () {
        isFirst = false;
        LastAnswerID = null;
        GetAnswerList(LastAnswerID);
        wrapper.refresh();
    }, 500);
}

$(function () {

    setTimeout(function () {
        if (isFirst) {
            refresher.init({
                id: "wrapper",
                pullDownAction: Down,
                pullUpAction: Up
            });
        }
    }, 500);

    GetAnswerList(null);

    $('.reply').click(function () {
        $('.reply_theme').show();
        $(".reply_content").val("");
    });

    $(".send").on("click", function () {
        var content = $.trim($("#RepContent").val());
        if (_.isEmpty(content)) {
            layer.msg('内容不能为空');
            return;
        }
        if (!QuestionID || !UserID) {
            layer.msg('问题ID或者用户ID不能为空');
        }
        var params = { "QuestionID": QuestionID, "UserID": UserID, "AnswerContent": content };
        common.call(AppPath+"/MyTrain/PostInsertAnswer", params, 'post', function (data) {
            if (data.Status == true) {
                $('.reply_theme').hide();
                //Up();
                Down();
                $(".reply_content").val("");
            }
        }, error);
    });

    $('.reply_theme .cancel').click(function () {
        $('.reply_theme').hide();
    });
});