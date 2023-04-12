// JScript 文件

function LMSInitialize(parm) {
    //alert("01 LMSInitialize" + parm);
    var resourceId = document.getElementById("resId").value;
    var _return = CallWebServiceMethod('WebService/Conversation.aspx', "parm=" + parm + "&id=" + resourceId + "&t=0&UserName=" + UserName);

    return _return;
}

function LMSFinish(parm) {
    //alert("02 LMSFinish" + parm);
    var resourceId=document.getElementById("resId").value;
    var _return = CallWebServiceMethod('WebService/Conversation.aspx',"parm="+parm+"&id="+resourceId+"&t=1&UserName=" + UserName);

    return _return;
}

function LMSGetValue(parm) {
    //alert("03 LMSGetValue" + parm);
    var resourceId=document.getElementById("resId").value;
    var _return =  CallWebServiceMethod('WebService/DataTransfer.aspx',"parm="+parm+"&id="+resourceId+"&t=2&UserName=" + UserName);

    return _return;
}

function LMSSetValue(parm,value) {
    //alert("04 LMSSetValue" + parm);
    var resourceId=document.getElementById("resId").value;    
    var _return = CallWebServiceMethod("WebService/DataTransfer.aspx","parm="+parm+"&value="+value+"&id="+resourceId+"&t=3&UserName=" + UserName);
    
    //如果(存储的用户状态信息为完成时)在前台给对应标题打勾
    if(_return=="true" && parm =="cmi.core.lesson_status" && value=="completed")
    {
       setTreeViewNodeTitle(resourceId);
    } 
    return _return;
}

function LMSCommit(parm) {
    //alert("05 LMSCommit" + parm);
    var resourceId=document.getElementById("resId").value;
    var _return =  CallWebServiceMethod('WebService/DataTransfer.aspx',"parm="+parm+"&id="+resourceId+"&t=4&UserName=" + UserName);

    return _return;
}

function LMSGetLastError () {
    //alert("06 LMSGetLastError");
    var resourceId=document.getElementById("resId").value;
    var _return = CallWebServiceMethod('WebService/ErrorManage.aspx','id='+resourceId+"&t=5&UserName=" + UserName);

    return _return;
}

function LMSGetErrorString (parm) {
    //alert("07 LMSGetErrorString" + parm);
    var resourceId=document.getElementById("resId").value;
    var _return =  CallWebServiceMethod('WebService/ErrorManage.aspx',"parm="+parm+"&id="+resourceId+"&t=6&UserName=" + UserName);

    return _return;
}

function LMSGetDiagnostic (parm) {
    //alert("08 LMSGetDiagnostic" + parm);
    var resourceId=document.getElementById("resId").value;
    var _return =  CallWebServiceMethod('WebService/ErrorManage.aspx',"parm="+parm+"&id="+resourceId+"&t=7&UserName=" + UserName);

    return _return;
}
