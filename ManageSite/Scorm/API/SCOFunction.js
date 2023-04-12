/*******************************************************************************
**
** FileName: SCOFunction.js
**
*******************************************************************************/

/*******************************************************************************
** 本部分为课件调用SCORM接口的逻辑层，主要功能包括：
** 1、提供课程所需的统一接口，如获取学生信息，存取课程进度信息、成绩信息等。
** 2、判断是否存在SCORM接口，如果不存在SCORM接口，则用Cookie实现接口功能。
*******************************************************************************/

/****公共变量定义****/
var objectives = new Array();



/****私有变量定义****/
var _Initialized = false;  	//是否已经初始化
var _hasSCORM = false;  		//是否具有Scorm环境

var startDate;
var exitPageStatus;

function loadPage()
{
   if (LMSInitialize()=="true") _hasSCORM=true;
   /*var status = LMSGetValue( "cmi.core.lesson_status" );
   if (status == "not attempted")
   {
	  // the student is now attempting the lesson
	  LMSSetValue( "cmi.core.lesson_status", "incomplete" );
   }*/   
	 _Initialized = true;	 
   exitPageStatus = false;
}


function Initialize()
{
	if (!_Initialized){
		loadPage();
	}
}


function doContinue( status )
{
   // Reinitialize Exit to blank
   getValue( "cmi.core.exit", "" );

   var mode = getValue( "cmi.core.lesson_mode" );

   if ( mode != "review"  &&  mode != "browse" )
   {
      setValue( "cmi.core.lesson_status", status );
   }
 
   exitPageStatus = true;
   
   LMSCommit();

	// NOTE: LMSFinish will unload the current AU.  All processing
	//       relative to the current page must be performed prior
	//		 to calling LMSFinish.   

   LMSFinish();

}

function doQuit()
{
	 setValue( "cmi.core.exit", "logout" );

   exitPageStatus = true;
   
   LMSCommit();

	// NOTE: LMSFinish will unload the current AU.  All processing
	//       relative to the current page must be performed prior
	//		 to calling LMSFinish.   

   LMSFinish();
}

/*******************************************************************************
** The purpose of this function is to handle cases where the current AU may be 
** unloaded via some user action other than using the navigation controls 
** embedded in the content.   This function will be called every time an AU
** is unloaded.  If the user has caused the page to be unloaded through the
** preferred AU control mechanisms, the value of the "exitPageStatus" var
** will be true so we'll just allow the page to be unloaded.   If the value
** of "exitPageStatus" is false, we know the user caused to the page to be
** unloaded through use of some other mechanism... most likely the back
** button on the browser.  We'll handle this situation the same way we 
** would handle a "quit" - as in the user pressing the AU's quit button.
*******************************************************************************/
function unloadPage()
{
	if (exitPageStatus != true)
	{
		doQuit();
	}

	// NOTE:  don't return anything that resembles a javascript
	//		  string from this function or IE will take the
	//		  liberty of displaying a confirm message box.
	
}


function setCookie(name,value)
{
		var _expires = new Date(); 
		_expires.setTime(_expires.getTime() + (1000 * 86400 * 365)); 
		doSetCookie(name, value, _expires, '/');
}

/*******************************************************************************
**
** Function getValue(name)
** Inputs:  name - string representing the cmi data model defined category or
**             element (e.g. cmi.core.student_id)
** Return:  The value presently assigned by the LMS to the cmi data model
**       element defined by the element or category identified by the name
**       input value.
**
** Description:
** 调用LMSGetValue函数，如果没有Initialize，则Initialize
**
*******************************************************************************/
function getValue(name)
{
	Initialize();
	if (_hasSCORM!=true){
		return doGetCookie(name);
	}
	else{
		return LMSGetValue(name);		
	}
}

function setCookie(name,value)
{
		var _expires = new Date(); 
		_expires.setTime(_expires.getTime() + (1000 * 86400 * 365)); 
		doSetCookie(name, value, _expires, '/');
}

/*******************************************************************************
**
** Function setValue(name, value)
** Inputs:  name -string representing the data model defined category or element
**          value -the value that the named element or category will be assigned
** Return:  CMIBoolean true if successful
**          CMIBoolean false if failed.
**
** Description:
** 					调用doSetValue函数，如果没有Initialize，则Initialize
**
*******************************************************************************/
function setValue(name,value)
{
	Initialize();
	if (_hasSCORM!=true){
		setCookie(name, value);
		
	}else{
		LMSSetValue(name,value);
	}
}

/*******************************************************************************
**
** Function GetLearnerName()
** Return:  string 学生姓名 if successful
**          null if failed.
**
** Description:
** 					获取当前学生姓名
**
*******************************************************************************/
function getLearnerName()
{
	var s = getValue("cmi.core.student_name");
	if (s==null) s="学员";   //如果在Cookie状态下，该信息一开始不会存在
	return s;	
}

/*******************************************************************************
**
** Function GetLearnerID()
** Return:  string 学生ID if successful
**          null if failed.
**
** Description:
** 					获取当前学生唯一编号
**
*******************************************************************************/
function getLearnerID()
{
	var s = getValue("cmi.core.student_id");
	if (s==null) s="S01";		//如果在Cookie状态下，该信息一开始不会存在
	return s;
}

/*******************************************************************************
**
** Function getLesson_Status()
** Return:  return "completed", "incomplete", "not attempted", "browsed", "passed", "failed"  if successful.
**          null if failed.
**
** Description:
** 					completion_status表示该SCO的整体学习进度
**
*******************************************************************************/
function getLesson_Status()
{
	return getValue("cmi.core.lesson_status");
}

function setLesson_Status(sStatus)
{
	return setValue("cmi.core.lesson_status",sStatus);
}

/*******************************************************************************
**
** Function getLesson_Score()
** Return:  return a real number between 0 to 100.
**          null if failed.
**
** Description:
** 					获得课程整体的成绩
**
*******************************************************************************/
function getLesson_Score()
{
	return getValue("cmi.core.score.raw");
}

/*******************************************************************************
**
** Function setLesson_Score(score)
** Inputs:  score must between 0..100
**
** Description:
** 					设置课程整体的成绩
**
*******************************************************************************/
function setLesson_Score(score)
{
	return setValue("cmi.core.score.raw",score);
}


/*******************************************************************************
**
** Function getLesson_Location()
** Return:  返回当前学生的学习断点
**          null if failed.
**
** Description:
** 					获得学生学习断点
**
*******************************************************************************/
function getLesson_Location()
{
	return getValue("cmi.core.lesson_location");
}
/*******************************************************************************
**
** Function setLesson_Location(location)
** Inputs:  255字符以内的字符串，表示当前学生的学习断点，具体取值和解释由课件定义。如"C010101"表示"第一章第一节知识点1"
**
** Description:
** 					设置学生的学习断点
**
*******************************************************************************/
function setLesson_Location(location)
{
	return setValue("cmi.core.lesson_location",location);
}

/*******************************************************************************
**
** Function getObjectiveScore()
** Inputs:  id，课程知识点ID，如Obj1
** return： 返回某个知识点的成绩，如果为空返回0
** Description:
** 					获得某个知识点的成绩
**
*******************************************************************************/
function getObjectiveScore(id)
{
	var index = _getObjectiveIndexByID(id);
	var score = getValue("cmi.objectives."+index+".score.raw");
	if (score == null) score=0;
	return score;
}

function setObjectiveScore(id,score)
{
	var count=0;
	var index = _getObjectiveIndexByID(id);
	setValue("cmi.objectives."+index+".score.raw",score);
}

/*******************************************************************************
**
** Function getObjectiveStatus()
** Inputs:  id，课程知识点ID，如Obj1
** return： 返回某个知识点的状态，"completed", "incomplete", "not attempted", "browsed", "passed", "failed"
** Description:
** 					获得某个知识点的状态
**
*******************************************************************************/
function getObjectiveStatus(id)
{
	var index = _getObjectiveIndexByID(id);
	var status = getValue("cmi.objectives."+index+".status");
	if (status == null) status="not attempted";
	return status;
}

function setObjectiveStatus(id,status)
{
	var count=0;
	var index = _getObjectiveIndexByID(id);
	setValue("cmi.objectives."+index+".status",status);
}

/*******************************************************************************
**
** Function _getObjectiveIndexByID()
** Inputs:  知识点的id
** Return： 该Objective在平台或者Cookie中的index
**				  如果在已有Objectives没有找到该id，则建立一个新的Objective.
** Description:
** 					内部函数，通过知识点id获得cmi.objectives.n.id中的n
**
*******************************************************************************/

function _getObjectiveIndexByID(id)
{
	var i=0;
	var index=-1;
	var count = getValue("cmi.objectives._count");
	if (count==null) count=0;
	
	for (i=0;i<count;i++){
		if (getValue("cmi.objectives."+i+".id")==id){
			index=i;
			break;
		}
	}
	//如果没有找到，则建立新的
	if (index==-1){
		setValue("cmi.objectives."+count+".id",id)
		index = count;
		doSetCookie("cmi.objectives._count",++count);		
	}
	return index;
}

/*******************************************************************************
**
** Function getQuesStuResponse()
** Inputs:  id，试题ID，如Ques1_1
** return： 返回某个试题的学生答案，如果为空则返回null
** Description:
** 					获得某个试题的学生答案
**
*******************************************************************************/
function getQuesStuResponse(id)
{
	var index = _getQuesIndexByID(id);
	//var answer = getValue("cmi.interactions."+index+".student_response");  Scorm中，该数据为Write-Only
	var answer = doGetCookie("cmi.interactions."+index+".student_response");
	return answer;
}

function setQuesStuResponse(id,answer)
{
	var count=0;
	var index = _getQuesIndexByID(id);
	//setValue("cmi.interactions."+index+".student_response",answer);
	doSetCookie("cmi.interactions."+index+".student_response",answer);
}

/*******************************************************************************
**
** Function getQuesResult()
** Inputs:  id，试题ID，如Ques1_1
** return： 返回某个试题的学生回答结果，如果为空则返回null，
**          可以返回的值包括"correct","wrong","unanticipated","neutral","X.X"（具体数值）
** Description:
** 					获得某个试题的学生得分
**
*******************************************************************************/
function getQuesResult(id)
{
	var index = _getQuesIndexByID(id);
	//var result = getValue("cmi.interactions."+index+".result");
	var result = doGetCookie("cmi.interactions."+index+".result");
	return result;
}

function setQuesResult(id,result)
{
	var count=0;
	var index = _getQuesIndexByID(id);
	//setValue("cmi.interactions."+index+".result",result);
	doSetCookie("cmi.interactions."+index+".result",result);
}

/*******************************************************************************
**
** Function _getQuesIndexByID(id)
** Inputs:  试题的id
** Return： 该试题在平台或者Cookie中的index
**				  如果在已有试题中没有找到该id，则建立一个新的试题ID.
** Description:
** 					内部函数，通过知识点id获得cmi.interactions.n.id中的n
**
*******************************************************************************/

function _getQuesIndexByID(id)
{
	var i=0;
	var index=-1;
	//var count = getValue("cmi.interactions._count");
	var count = doGetCookie("cmi.interactions._count");
	if (count==null) count=0;
	
	for (i=0;i<count;i++){
		//if (getValue("cmi.interactions."+i+".id")==id){
		if (doGetCookie("cmi.interactions."+i+".id")==id){
			index=i;
			break;
		}
	}
	//如果没有找到，则建立新的
	if (index==-1){
		//setValue("cmi.interactions."+count+".id",id)
		doSetCookie("cmi.interactions."+count+".id",id)
		index = count;
		doSetCookie("cmi.interactions._count",++count);		
	}
	return index;
}



/*******************************************************************************
**
** Function getSuccess_Status()
** Return:  return "passed","failed" or "unknown" if successful.
**          null if failed.
**
** Description:
** 					sucess_status表示该SCO的整体学习情况，是否通过考核，Scorm不支持
**
*******************************************************************************/
/*function getSuccess_Status()
{
	return getValue("cmi.success_status");
}

function setSuccess_Status(sStatus)
{
	return setValue("cmi.success_status",sStatus);
}*/

/*******************************************************************************
**
** Function initObjectives(objArr)
** Inputs:  An array of objective IDs. For examle: objArr[0]="obj-1-1"
** Return:  
**
** Description:
** 					初始化知识点ID
**
*******************************************************************************/
/*function initObjectives(objArr)
{
	var i,count;
	count = objArr.length;
	if (_hasSCORM){
		for (i=0;i<count;i++){
			findObjective(objArr[i]);
		}
	}else{ //设置 Cookies
		setCookie("cmi.objectives._count",count);
		for (i=0;i<count;i++){
			setCookie("cmi.objectives."+i+".id",objArr[i]);
		}
	}
}

/*******************************************************************************
**
** Function getObjIndexByID(sID)
** Inputs:  Get Objective index by it's ID. For examle: sID="obj-1-1"
** Return:  
**					index of the Objective or -1 if cannot find.
** Description:
** 					通过知识点ID得到它的index。index是指“cmi.objectives.n.id”中的n，知识点sID是指其中的id
**
*******************************************************************************/
/*function getObjIndexByID(sID)
{
	var i=0,count=0;
	if (_hasSCORM) {
		return findObjective(sID);
	}
	else{
		var count = doGetCookie("cmi.objectives._count");
		if (count==null) return -1;
		for (i=0;i<count;i++){
			if (doGetCookie("cmi.objectives."+i+".id")==sID) return i;
		}
	}
	return -1;
}


/*******************************************************************************
**
** Function getObjAttr(objID,attrName)
** Inputs:  
				objID: objective ID. For examle: "obj-1-1"
				attrName: attrName can be: 
											"score.scale": 当前知识点的得分，范围-1至1的实数，如本单元考试得分为60分，则返回0.6
											"success_status": 学习成功的状态，passed/failed/unknown
										 	"completion_status": 完成状态，"completed","incomplete","not attempted" or "unknown"
										 	"description": 描述信息，任意字符串
										 	"progress_measure": 学习进度，范围0-1之间的实数,如学完了80%的内容，则返回0.8
** Return:  
**			The value of a Objective's attribute.
**      return null if it hasn't been stored.
** Description:
** 			得到某个知识点的信息，可以是得分、状态、描述等。
**
*******************************************************************************/
/*function getObjAttr(objID,attrName)
{
	var index = getObjIndexByID(objID);
	if (index<0) return null;
	return getValue("cmi.objectives."+index+"."+attrName);
}

/*******************************************************************************
**
** Function setObjAttr(objID,attrName,attrValue)
** Inputs:  
				objID: objective ID. For examle: "obj-1-1"
				attrName: attrName can be: 
											"score.scale": 当前知识点的得分，范围-1至1的实数，如本单元考试得分为60分，则返回0.6
											"success_status": 学习成功的状态，passed/failed/unknown
										 	"completion_status": 完成状态，"completed","incomplete","not attempted" or "unknown"
										 	"description": 描述信息，任意字符串
										 	"progress_measure": 学习进度，范围0-1之间的实数,如学完了80%的内容，则返回0.8
				attrValue：为attrName设置相应的取值。
** Return:  
**		  0： 赋值成功
**			-1：没有找到相应的objID
** Description:
** 			得到某个知识点的信息，可以是得分、状态、描述等。
**
*******************************************************************************/
/*function setObjAttr(objID,attrName,attrValue)
{
	var index = getObjIndexByID(objID);
	if (index<0) return -1;
	setValue("cmi.objectives."+index+"."+attrName,attrValue);
	return 0;
}*/