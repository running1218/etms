//删除试题方法 obj为tr的ID号
function pointdeloption(obj) {
    var delObj = $("#" + obj);
    delObj.remove();
    tablecloth();
}

//添加行
function pointaddoption() {
   //debugger;
    var totalNumbers = $("input[type='text']").length / 2;

    //  var textobj = $("input[type='text']:last").parent().parent().parent();
    var tableobj = $("#poinTable");
    var trHtml = '<tr id="">'
               + '<td class="center">'
                   + '<span>'
                        + "<input type='text'"

                        + ' value="200" name="MaxNum" id=' + totalNumbers
+ ' class="inputtext_bj1" maxlength="6"> </span>'
                + '</td>'
+ '<td class="center">'

                     + '<span>'
                        + "<input type='text'"

                        + ' value="200" name="GivePoints" id=' + totalNumbers
+ ' class="inputtext_bj1" maxlength="6"> </span>'

                + '</td>'
                + '<td width="40" class="">'
                    + '<a href="javascript:pointdeloption(tr_9)">删除</a>'
+ '</td>'
            + '</tr>';

    ///textobj.insertAfter($("input[type='text']:last").parent().parent());
    tableobj.append(trHtml);
    var textobj = $("input[type='text']:last").parent().parent().parent().parent();
    textobj.find("tr:last").attr("id", "tr_" + parseInt(totalNumbers));
    textobj.find("tr:last").find("input:first").change(function () {
        maxCheck("MaxNum_" + totalNumbers);
    })
    textobj.find("tr:last").find("input:last").change(function () {
        pointcheck("GivePoints_" + totalNumbers);
    })
    textobj.find("tr:last").find("input:first").attr("id", "MaxNum_" + parseInt(totalNumbers));
    textobj.find("tr:last").find("input:last").attr("id", "GivePoints_" + parseInt(totalNumbers));
    textobj.find("tr:last").find("input[type=text]").val("");
    textobj.find("tr:last").find("td:nth-child(3)").html("<a href=\"javascript:pointdeloption('tr_" + parseInt(totalNumbers) + "')\">删除</a>");
    $("input").radioStyle();
    setFrameHeight();
    tablecloth();
}

//判断是否是数字
function pointcheckNum(str) {
    var patrn = /^(-?[1-9]\d*|0)$/; //^(-?[1-9]\d*|0)$   ^[1-9]\d*$
    if (!patrn.exec(str)) return false
    return true;
}

//判断是否是数字
function maxcheckNum(str) {
    var patrn = /^[1-9]\d*$/; //^(-?[1-9]\d*|0)$   ^[1-9]\d*$
    if (!patrn.exec(str)) return false
    return true;
}

function maxCheck(obj) {
    var temp = $.trim($("#" + obj).val());
    if (maxcheckNum($("#" + obj).val()) == false) {
        popAlertMsg("课时临界点（含临界点）必须为正整数！", "友情提示");
        $("#" + obj).val("");
    }
}
function pointcheck(obj) {
    var temp = $.trim($("#" + obj).val());  
    if (pointcheckNum($("#" + obj).val()) == false) {
        popAlertMsg("积分必须为整数！", "友情提示");
        $("#" + obj).val("");
    }
}

//保存
function pointsaveFunoption(objID) {

    var strResult = "";
    $("#poinTable tr:gt(0)").each(function () {
        strResult += $(this).find("input:first").val() + "Φ";
        strResult += $(this).find("input:last").val() + "Ω";
    });
    //alert("传入的id:" + objID + "返回结果strResult:" + strResult);
    $("#" + objID).val(strResult.substring(0, strResult.length - 1));

}

   

