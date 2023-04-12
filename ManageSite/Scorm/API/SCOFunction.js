/*******************************************************************************
**
** FileName: SCOFunction.js
**
*******************************************************************************/

/*******************************************************************************
** ������Ϊ�μ�����SCORM�ӿڵ��߼��㣬��Ҫ���ܰ�����
** 1���ṩ�γ������ͳһ�ӿڣ����ȡѧ����Ϣ����ȡ�γ̽�����Ϣ���ɼ���Ϣ�ȡ�
** 2���ж��Ƿ����SCORM�ӿڣ����������SCORM�ӿڣ�����Cookieʵ�ֽӿڹ��ܡ�
*******************************************************************************/

/****������������****/
var objectives = new Array();



/****˽�б�������****/
var _Initialized = false;  	//�Ƿ��Ѿ���ʼ��
var _hasSCORM = false;  		//�Ƿ����Scorm����

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
** ����LMSGetValue���������û��Initialize����Initialize
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
** 					����doSetValue���������û��Initialize����Initialize
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
** Return:  string ѧ������ if successful
**          null if failed.
**
** Description:
** 					��ȡ��ǰѧ������
**
*******************************************************************************/
function getLearnerName()
{
	var s = getValue("cmi.core.student_name");
	if (s==null) s="ѧԱ";   //�����Cookie״̬�£�����Ϣһ��ʼ�������
	return s;	
}

/*******************************************************************************
**
** Function GetLearnerID()
** Return:  string ѧ��ID if successful
**          null if failed.
**
** Description:
** 					��ȡ��ǰѧ��Ψһ���
**
*******************************************************************************/
function getLearnerID()
{
	var s = getValue("cmi.core.student_id");
	if (s==null) s="S01";		//�����Cookie״̬�£�����Ϣһ��ʼ�������
	return s;
}

/*******************************************************************************
**
** Function getLesson_Status()
** Return:  return "completed", "incomplete", "not attempted", "browsed", "passed", "failed"  if successful.
**          null if failed.
**
** Description:
** 					completion_status��ʾ��SCO������ѧϰ����
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
** 					��ÿγ�����ĳɼ�
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
** 					���ÿγ�����ĳɼ�
**
*******************************************************************************/
function setLesson_Score(score)
{
	return setValue("cmi.core.score.raw",score);
}


/*******************************************************************************
**
** Function getLesson_Location()
** Return:  ���ص�ǰѧ����ѧϰ�ϵ�
**          null if failed.
**
** Description:
** 					���ѧ��ѧϰ�ϵ�
**
*******************************************************************************/
function getLesson_Location()
{
	return getValue("cmi.core.lesson_location");
}
/*******************************************************************************
**
** Function setLesson_Location(location)
** Inputs:  255�ַ����ڵ��ַ�������ʾ��ǰѧ����ѧϰ�ϵ㣬����ȡֵ�ͽ����ɿμ����塣��"C010101"��ʾ"��һ�µ�һ��֪ʶ��1"
**
** Description:
** 					����ѧ����ѧϰ�ϵ�
**
*******************************************************************************/
function setLesson_Location(location)
{
	return setValue("cmi.core.lesson_location",location);
}

/*******************************************************************************
**
** Function getObjectiveScore()
** Inputs:  id���γ�֪ʶ��ID����Obj1
** return�� ����ĳ��֪ʶ��ĳɼ������Ϊ�շ���0
** Description:
** 					���ĳ��֪ʶ��ĳɼ�
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
** Inputs:  id���γ�֪ʶ��ID����Obj1
** return�� ����ĳ��֪ʶ���״̬��"completed", "incomplete", "not attempted", "browsed", "passed", "failed"
** Description:
** 					���ĳ��֪ʶ���״̬
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
** Inputs:  ֪ʶ���id
** Return�� ��Objective��ƽ̨����Cookie�е�index
**				  ���������Objectivesû���ҵ���id������һ���µ�Objective.
** Description:
** 					�ڲ�������ͨ��֪ʶ��id���cmi.objectives.n.id�е�n
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
	//���û���ҵ��������µ�
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
** Inputs:  id������ID����Ques1_1
** return�� ����ĳ�������ѧ���𰸣����Ϊ���򷵻�null
** Description:
** 					���ĳ�������ѧ����
**
*******************************************************************************/
function getQuesStuResponse(id)
{
	var index = _getQuesIndexByID(id);
	//var answer = getValue("cmi.interactions."+index+".student_response");  Scorm�У�������ΪWrite-Only
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
** Inputs:  id������ID����Ques1_1
** return�� ����ĳ�������ѧ���ش��������Ϊ���򷵻�null��
**          ���Է��ص�ֵ����"correct","wrong","unanticipated","neutral","X.X"��������ֵ��
** Description:
** 					���ĳ�������ѧ���÷�
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
** Inputs:  �����id
** Return�� ��������ƽ̨����Cookie�е�index
**				  ���������������û���ҵ���id������һ���µ�����ID.
** Description:
** 					�ڲ�������ͨ��֪ʶ��id���cmi.interactions.n.id�е�n
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
	//���û���ҵ��������µ�
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
** 					sucess_status��ʾ��SCO������ѧϰ������Ƿ�ͨ�����ˣ�Scorm��֧��
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
** 					��ʼ��֪ʶ��ID
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
	}else{ //���� Cookies
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
** 					ͨ��֪ʶ��ID�õ�����index��index��ָ��cmi.objectives.n.id���е�n��֪ʶ��sID��ָ���е�id
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
											"score.scale": ��ǰ֪ʶ��ĵ÷֣���Χ-1��1��ʵ�����籾��Ԫ���Ե÷�Ϊ60�֣��򷵻�0.6
											"success_status": ѧϰ�ɹ���״̬��passed/failed/unknown
										 	"completion_status": ���״̬��"completed","incomplete","not attempted" or "unknown"
										 	"description": ������Ϣ�������ַ���
										 	"progress_measure": ѧϰ���ȣ���Χ0-1֮���ʵ��,��ѧ����80%�����ݣ��򷵻�0.8
** Return:  
**			The value of a Objective's attribute.
**      return null if it hasn't been stored.
** Description:
** 			�õ�ĳ��֪ʶ�����Ϣ�������ǵ÷֡�״̬�������ȡ�
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
											"score.scale": ��ǰ֪ʶ��ĵ÷֣���Χ-1��1��ʵ�����籾��Ԫ���Ե÷�Ϊ60�֣��򷵻�0.6
											"success_status": ѧϰ�ɹ���״̬��passed/failed/unknown
										 	"completion_status": ���״̬��"completed","incomplete","not attempted" or "unknown"
										 	"description": ������Ϣ�������ַ���
										 	"progress_measure": ѧϰ���ȣ���Χ0-1֮���ʵ��,��ѧ����80%�����ݣ��򷵻�0.8
				attrValue��ΪattrName������Ӧ��ȡֵ��
** Return:  
**		  0�� ��ֵ�ɹ�
**			-1��û���ҵ���Ӧ��objID
** Description:
** 			�õ�ĳ��֪ʶ�����Ϣ�������ǵ÷֡�״̬�������ȡ�
**
*******************************************************************************/
/*function setObjAttr(objID,attrName,attrValue)
{
	var index = getObjIndexByID(objID);
	if (index<0) return -1;
	setValue("cmi.objectives."+index+"."+attrName,attrValue);
	return 0;
}*/