
var Doc = null;
var xmlhttp = null;
function LoadAnswerXML() {
    Doc = document.getElementById("AnswerXml").documentElement;
    //    return;
    //    if (Doc != null)
    //        return;
    //    var xmlDoc;
    //    xmlhttp = GetMSXmlHttp();
    //    xmlhttp.open("GET", URL, true);
    //    xmlhttp.onreadystatechange = OnGetReadyStateChanged;
    //    xmlhttp.send();
}
function GetMSXmlHttp() {
    var xmlHttp = null;
    var clsids = ["Msxml2.XMLHTTP.6.0", "Msxml2.XMLHTTP.5.0", "Msxml2.XMLHTTP.4.0", "Msxml2.XMLHTTP.3.0", "Msxml2.XMLHTTP.2.6", "Microsoft.XMLHTTP.1.0", "Microsoft.XMLHTTP.1", "Microsoft.XMLHTTP"]; for (var i = 0; i < clsids.length && xmlHttp == null; i++) { xmlHttp = CreateXmlHttp(clsids[i]); }
    return xmlHttp;
}
function CreateXmlHttp(clsid) {
    var xmlHttp = null;
    try {
        xmlHttp = new ActiveXObject(clsid);
        lastclsid = clsid;
        return xmlHttp;
    }
    catch (e) { }
}
function OnGetReadyStateChanged() {
    if (xmlhttp.readyState == 4) {
        xmlDoc = new ActiveXObject('Microsoft.XMLDOM');
        xmlDoc.loadXML(xmlhttp.responseText);
        Doc = xmlDoc.documentElement;
    }
}
function save(obj) {
    //修改了答案为空时，在预览试卷模版时报的JS错误
    if (Doc == null) {
        return;
    }
    switch (obj.TitleType) {
        case "1":
            SigleSelectSave(obj);
            break;
        case "2":
            MutilSelectSave(obj);
            break;
        case "3":
            MatrixSelectSave(obj);
            break;
        case "4":
            TextSelectSave(obj);
            break;
    }
}
function SigleSelectSave(obj) {
    var answer = GetAnswerNodeListByTitleID(obj.TitleID)[0];
    if (obj.IsOther == 0) {
        answer.childNodes[0].text = obj.OptionID;
        answer.childNodes[1].text = "";
    }
    else {
        answer.childNodes[0].text = "";
        answer.childNodes[1].text = obj.OptionID + "|" + document.getElementById('txtoption_' + obj.OptionID).value;
    }
    answer = GetAnswerNodeListByTitleID(obj.TitleID)[0];
}
function MutilSelectSave(obj) {
    var answer = GetAnswerNodeListByTitleID(obj.TitleID)[0];
    if (answer.childNodes[0].text == null) {
        answer.childNodes[0].text = "";
    }
    if (obj.IsOther == 0) {
        if (obj.checked) {
            if (answer.childNodes[0].text.indexOf(obj.OptionID) < 0) {
                answer.childNodes[0].text += obj.OptionID + ",";
            }
        }
        else {
            if (answer.childNodes[0].text.indexOf(obj.OptionID) > 0) {
                answer.childNodes[0].text = answer.childNodes[0].text.replace(obj.OptionID + ",", "");
            }
        }
    }
    else {
        if (obj.checked) {
            answer.childNodes[1].text = obj.OptionID + "|" + document.getElementById('txtoption_' + obj.OptionID).value;
        }
        else {
            answer.childNodes[1].text = "";
        }
    }
}
function MatrixSelectSave(obj) {
    var answer = GetAnswerNodeByTitleIdAndOptionId(obj.TitleID, obj.OptionID);
    answer.childNodes[1].text = obj.HeaderID;
}
function TextSelectSave(obj) {
    var answer = GetAnswerNodeListByTitleID(obj.TitleID)[0];
    answer.childNodes[1].text = obj.value;
}
function GetAnswerNodeListByTitleID(TitleID) {
    return Doc.selectNodes("//Title[TitleID='" + TitleID + "']/Answers/*");
}
function GetAnswerNodeByTitleIdAndOptionId(TitleID, OptionID) {
    return Doc.selectSingleNode("//Title[TitleID='" + TitleID + "']/Answers/Answer[OptionID='" + OptionID + "']");
}

function postDataToServer() {
    if (!DataValidTest()) return;
    Doc.selectSingleNode("/Query/ResourceType").text = QueryString("ResourceType") == null ? "" : QueryString("ResourceType");
    Doc.selectSingleNode("/Query/ResourceCode").text = QueryString("ResourceCode") == null ? "" : QueryString("ResourceCode");
    var postUrl = "QueryResultSave.aspx";
    xmlhttp = GetMSXmlHttp();
    xmlhttp.open("POST", postUrl, true);
    xmlhttp.onreadystatechange = OnPostReadyStateChanged;
    xmlhttp.send(Doc.xml);
}
function OnPostReadyStateChanged() {
    if (xmlhttp.readyState == 4) {
        var message = xmlhttp.responseXML.selectSingleNode("/Result[IsSuccess=0]");
        if (message == null) {
            SwitchDivVisble("divFooter");
        }
        else {
            DisplayMsg(message.childNodes[1].text);
        }
    }
    else {
        DisplayMsg("正在保存问卷，请稍等....");
    }
}
function SwitchDivVisble(divName) {
    var divQ = document.getElementById("divQuery");
    var divM = document.getElementById("divMsg");
    var divF = document.getElementById("divFooter");
    divQ.style.display = "none";
    divM.style.display = "none";
    divF.style.display = "none";
    switch (divName) {
        case "divQuery":
            divQ.style.display = "block";
            break;
        case "divMsg":
            divM.style.display = "block";
            break;
        case "divFooter":
            divF.style.display = "block";
            break;
    }
}
function DisplayMsg(msg) {
    SwitchDivVisble("divMsg");
    var msgWin = document.getElementById("msgWin");
    msgWin.innerHTML = msg;
}
function DataValidTest() {
    var titles = Doc.selectNodes("//Title");
    var emptyCount = 0;
    for (var i = 0; i < titles.length; i++) {
        var title = titles[i];
        var titleID = title.selectSingleNode("TitleID").text;
        var titleTypeID = title.selectSingleNode("TitleTypeID").text;
        var answers = title.selectNodes(".//Answer");
        var IsValid = false;
        switch (titleTypeID) {
            case "1":
            case "2":
                if (answers[0].childNodes[0].text.length > 0 || answers[0].childNodes[1].text.length > 0) {
                    IsValid = true;
                }
                break;
            case "3":
                IsValid = true;
                for (var j = 0; j < answers.length; j++) {
                    var answer = answers[j];
                    if (answer.childNodes[1].text.length < 1) {
                        IsValid = false; break;
                    }
                }
                break;
            case "4":
                if (answers[0].childNodes[1].text.length > 0) {
                    IsValid = true;
                }
                break;
        }
        if (IsValid) {
            document.getElementById('lblTitle_' + titleID).style.display = "none";
        }
        else {
            emptyCount++;
            document.getElementById('lblTitle_' + titleID).style.display = "block";
        }
    }
    if (emptyCount != 0) {
        popAlertMsg("您尚未完全答完问卷,请您继续...", "提示：");
        return false;
    }
    return true;
}
function QueryString(fieldName) {
    var urlString = document.location.search; if (urlString != null) {
        var typeQu = fieldName + "="; var urlEnd = urlString.indexOf(typeQu); if (urlEnd != -1) {
            var paramsUrl = urlString.substring(urlEnd + typeQu.length); var isEnd = paramsUrl.indexOf('&'); if (isEnd != -1)
            { return paramsUrl.substring(0, isEnd); }
            else
            { return paramsUrl; }
        }
        else
            return null;
    }
    else
        return null;
}
function ViewStatResult() {
    var URL = 'QueryStatResult.aspx?QueryID={0}&ResourceType={1}&ResourceCode={2}&Token={3}';
    URL = URL.replace("{0}", QueryString("QueryID"));
    URL = URL.replace("{1}", QueryString("ResourceType"));
    URL = URL.replace("{2}", QueryString("ResourceCode"));
    URL = URL.replace("{3}", QueryString("token"));
    window.location.href = URL;
}
function SwitchPreViewMode() {
    if (window.location.href.toLowerCase().indexOf('querypreview.aspx') !== -1) {
        document.getElementById("divButton").style.display = "none";
        return true;
    }
    else {
        document.getElementById("divButton").style.display = "block";
        return false;
    }
}

function findControl(tag, optionID, headerID) {
    var ctls = document.getElementsByTagName(tag);
    if (ctls.length == 0)
        return null;
    for (var i = 0; i < ctls.length; i++) {
        var ctl = ctls[i];
        if (headerID != null && ctl.HeaderID != headerID) {
            continue;
        }
        if (ctl.OptionID == optionID) {
            return ctl;
        }
    }
    return null;
}
function findOtherControl(id) {
    return document.getElementById("txtoption_" + id);
}
function findTextControl(tag, titleID) {
    var ctls = document.getElementsByTagName(tag);
    if (ctls.length == 0)
        return null;
    for (var i = 0; i < ctls.length; i++) {
        var ctl = ctls[i];
        if (ctl.TitleID == titleID) {
            return ctl;
        }
    }
    return null;
}
function LoadAnswerXMLForDisplay() {
    if (Doc != null)
        return;
    var xmlDoc;
    xmlhttp = GetMSXmlHttp();
    xmlhttp.open("GET", URL, true);
    xmlhttp.onreadystatechange = OnGetReadyStateChangedForDisplay;
    xmlhttp.send();
}
function OnGetReadyStateChangedForDisplay() {
    if (xmlhttp.readyState == 4) {
        xmlDoc = new ActiveXObject('Microsoft.XMLDOM');
        xmlDoc.loadXML(xmlhttp.responseText);
        Doc = xmlDoc.documentElement;
        LoadUserAnswer();
    }
}

function GetMSXmlHttp() {
    var xmlHttp = null; var clsids = ["Msxml2.XMLHTTP.6.0", "Msxml2.XMLHTTP.5.0", "Msxml2.XMLHTTP.4.0", "Msxml2.XMLHTTP.3.0", "Msxml2.XMLHTTP.2.6", "Microsoft.XMLHTTP.1.0", "Microsoft.XMLHTTP.1", "Microsoft.XMLHTTP"]; for (var i = 0; i < clsids.length && xmlHttp == null; i++) { xmlHttp = CreateXmlHttp(clsids[i]); }
    return xmlHttp;
}
function CreateXmlHttp(clsid) {
    var xmlHttp = null; try { xmlHttp = new ActiveXObject(clsid); lastclsid = clsid; return xmlHttp; }
    catch (e) { }
}
function LoadUserAnswer() {
    var titles = Doc.selectNodes("//Title");
    var emptyCount = 0;
    for (var i = 0; i < titles.length; i++) {
        var title = titles[i];
        var titleID = title.selectSingleNode("TitleID").text;
        var titleTypeID = title.selectSingleNode("TitleTypeID").text;
        var answers = title.selectNodes(".//Answer");
        if (answers.length == 0)//如果没有找到答题记录，则跳过
            continue;

        var OptionID;
        var HeadID;
        var OtherText;
        switch (titleTypeID) {
            //单选题                                                     
            case "1":
                //多选题
            case "2":
                for (var j = 0; j < answers.length; j++) {
                    OptionID = answers[j].selectSingleNode("OptionID").text;
                    OtherText = answers[j].selectSingleNode("OtherText").text;
                    var findctl = findControl("input", OptionID, null);
                    if (OtherText.length == 0) {
                        if (findctl != null) {
                            findctl.checked = true;
                        }
                    }
                    else//其他选项文本
                    {
                        if (findctl != null) {
                            findctl.checked = true;
                        }
                        var findOtherctl = findOtherControl(OptionID);
                        if (findOtherctl != null) {
                            findOtherctl.value = OtherText;
                        }
                    }
                }
                break;
            case "3":
                for (var j = 0; j < answers.length; j++) {
                    OptionID = answers[j].selectSingleNode("OptionID").text;
                    HeadID = answers[j].selectSingleNode("HeadID").text;
                    var findctl = findTextControl("input", OptionID, HeadID);
                    if (findctl != null) {
                        findctl.checked = true;
                    }
                }
                break;
            case "4":
                for (var j = 0; j < answers.length; j++) {
                    OtherText = answers[j].selectSingleNode("OtherText").text;
                    var findctl = findTextControl("textarea", titleID);
                    if (findctl != null) {
                        findctl.value = OtherText;
                    }
                }
                break;
        }
    }
}