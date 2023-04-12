// JScript 文件
function $(e){return document.getElementById(e);}

var mplayer = null;

var mdivplay = null;
var mdivloading = null;
var mdivdownload = null;

var snplayerWidth = '100%';  //播放器宽度
var snplayerHeight = '100%'; //播放器高度
var mediaplaydownload ="http://hdcnc1.skycn.com/down/MP10Setup_skycn.exe";
var realplaydownload = "http://7.jsdx1.crsky.com/201110/realplayer-v14.0.7.zip";
var ScenicPlayerdownload ="http://support.collegesoft.com.cn/detail.aspx?classid=xpr";

var m_time = null;
var timerRunning = false;
var mmediatype =null;
var msrc = null;


//******************************** 显示函数Show Begin ********************************//

//显示播放画面
function showmdivplay(){
    mdivplay = document.getElementById('divplay');
    mdivloading = document.getElementById('divloading');
    mdivdownload = document.getElementById('divdownload');
    
    if (mdivplay.style.display == "none"){
        mdivloading.style.display = "none";
        mdivdownload.style.display = "none";
        mdivplay.style.display = "block";
    }
    
    mdivplay = null;
    mdivloading = null;
    mdivdownload = null;
}
//显示下载画面
function showmdivdownload(){
    mdivplay = document.getElementById('divplay');
    mdivloading = document.getElementById('divloading');
    mdivdownload = document.getElementById('divdownload');
    
    if (mdivdownload.style.display == "none"){
        mdivloading.style.display = "none";
        mdivplay.style.display = "none";
        mdivdownload.style.display = "block";
    }
    
    mdivplay = null;
    mdivloading = null;
    mdivdownload = null;
}

//显示Loading画面
function showmdivloading(){    
    mdivplay = document.getElementById('divplay');
    mdivloading = document.getElementById('divloading');
    mdivdownload = document.getElementById('divdownload');
   
    if (mdivloading.style.display == "none"){
        mdivdownload.style.display = "none";
        mdivplay.style.display = "none";
        mdivloading.style.display = "block";
    }
    
    mdivplay = null;
    mdivloading = null;
    mdivdownload = null;
}
//******************************** 显示函数Show  End  ********************************//

//加载mediaplay
function WriteMediaPlay(){
	var str ='';
	str += '<object id="player"  onerror="AlarmDownload();" width="'+snplayerWidth+'" height="'+snplayerHeight+'" classid="clsid:6BF52A52-394A-11d3-B153-00C04F79FAA6">';
	str += '    <param name="AutoStart" value="true">';
	str += '    <param NAME="Balance" VALUE="1">';
	str += '    <param name="enabled" value="1">';
	str += '    <param name="EnableContextMenu" value="true">';
	str += '    <param name="url" value="">';
	str += '    <param name="PlayCount" value="1">';
	str += '    <param name="rate" value="1">';
	str += '    <param name="currentPosition" value="0">';
	str += '    <param name="currentMarker" value="1">';
	str += '    <param name="defaultFrame" value="">';
	str += '    <param name="invokeURLs" value="0">';
	str += '    <param name="baseURL" value="">';
	str += '    <param name="stretchToFit" value="1">';
	str += '    <param name="volume" value="90">';
	str += '    <param name="mute" value="0">';
//	str += '    <param name="uiMode" value="None">';
	str += '    <param name="uiMode" value="all">';
	str += '    <param name="windowlessVideo" value="0">';
	str += '    <param name="fullScreen" value="0">';
	str += '    <param name="enableErrorDialogs" value="0">';
	str += '</object>';	
	
	mdivplay = document.getElementById('divplay');
	mdivplay.innerHTML = str;
	mdivplay = null;
}

//加载realplay
function WriteRealPlay(){
	var str ='';
	str += '<object ID="player"  onerror="AlarmDownload();" width="'+snplayerWidth+'" height="'+snplayerHeight+'" CLASSID="clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA"> ';
	str += '<param name="AUTOSTART" value="0">  ';
	str += '<param name="SHUFFLE" value="0">  ';
	str += '<param name="PREFETCH" value="0">  ';
	str += '<param name="NOLABELS" value="0">  ';
	str += '<param name="SRC" value="' +  msrc + '">  ';
	str += '<param name="CONTROLS" value="ImageWindow,ControlPanel"> ';
	str += '<param name="CONSOLE" value="Clip1">  ';
	str += '<param name="LOOP" value="0">  ';
	str += '<param name="NUMLOOP" value="0"> ';
	str += '<param name="CENTER" value="0"> ';
	str += '<param name="MAINTAINASPECT" value="0"> ';
	str += '<param name="all" value="1"> ';
	str += '</object> ';	
	
	mdivplay = document.getElementById('divplay');
	mdivplay.innerHTML = str;
	mdivplay = null;
}
//加载ScenicPlayer
function WriteScenicPlayer(url){
    var str ='';
    //str += '<object id="player" onerror="AlarmDownload();" codebase="player/xplayer.exe" height="0" width="0" classid="CLSID:B77FC152-E7AC-45D2-B803-19B026C83911" viewastext></object> ';
    str +='<object id=player onerror="AlarmDownload();" classid="CLSID:8EF11386-FCAF-426D-88B0-62C68E9B5770" width=100% height=100% >'
    str +='<param name="ValidationCode" value="">'
	str +='<param name="Url" value="' + url +'">'
	str +='<param name="ShowToolbar" value="1">'
	str +='<param name="BufferTime" value="5">'
	str +='<param name="AutoPlay" value="1">'
	str +='<param name="AutoReplay" value="0">'
	str +='<param name="AutoFullScreen" value="0">'
	str +='<param name="AutoScreenStretch" value="0">'
	str +='<param name="DisableVideoAccel" value="0">'
	str +='<param name="DisableOverlay" value="0">'
	str +='<param name="UseMoreMonitor" value="0">'
	str +='<param name="ConnectStyle" value="1">'
	str +='<param name="MonitorIndex" value="0">'
	str +='<param name="MainMonitorMode" value="1">'
	str +='<param name="MaxVideoNumPerMonitor" value="4">'
    str +='</object>'
    
    
    mdivplay = document.getElementById('divplay');
	mdivplay.innerHTML = str;
	mdivplay = null;
}

//加载Mediaplay安装提示
function WriteDownload(){
    var str = '';
    if(mmediatype =="0"){
        str ='<br /><br /><br /><br /><br /><a href="' + mediaplaydownload + '">请下载 Windows Media Player 10 ！谢谢！<br /></a><br /><a href="#"  onclick="window.location.reload();">确定安装完毕</a>';
    }
    if(mmediatype =="1"){
        str ='<br /><br /><br /><br /><br /><a href="' + realplaydownload + '">请下载 Real Player 10 ！谢谢！<br /></a><br /><a href="#"  onclick="window.location.reload();">确定安装完毕</a>';
    }
    if(mmediatype =="2"){
        str ='<br /><br /><br /><br /><br /><a href="' + ScenicPlayerdownload + '">请下载 ScenicPlayer ！谢谢！<br /></a><br /><a href="#"  onclick="window.location.reload();">确定安装完毕</a>';
    }

    mdivdownload = document.getElementById('divdownload');
	mdivdownload.innerHTML = str;
	mdivdownload = null;
}


//提示安装Mediaplay
function AlarmDownload(){
    WriteDownload();
    showmdivdownload();
    if (timerRunning == true){
        clearInterval(m_time);
        m_time = null;
        timerRunning = false;
    }
}

//$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$    播放区 点击频道播放重点   $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$//
//mNSC 地址,mediatype播放类型
function callplay(mNSC,mediatype){
    //初始化 begin
    //showmdivloading();
    showmdivplay(); 
    mmediatype= mediatype;
    
    if (timerRunning == true){
        clearInterval(m_time);
        m_time = null;
        timerRunning=false;
    }
    //初始化 end
    
    try{
        //mediaplay
        if (mediatype=="0"){
            WriteMediaPlay();
            mplayer = document.getElementById("player");
            mplayer.url=UnStrCode(mNSC);
            try{ mplayer.controls.play(); }
            catch(err){}
            mplayer = null;
        }
        //realplay
        if (mediatype=="1"){
            msrc =UnStrCode(mNSC);
            WriteRealPlay();
            mplayer = document.getElementById("player");
            //mplayer.SRC=UnStrCode(mNSC);
            try{ mplayer.DoPlay(); }
            catch(err){}
            mplayer = null;
        }
        //ScenicPlayer
        if(mediatype=="2"){
            WriteScenicPlayer(mNSC);
        }
    }
    catch(err){}
}


//编码解码
function StrCode(str){if(encodeURIComponent) return encodeURIComponent(str);if(escape) return escape(str);}
function UnStrCode(str){if(decodeURIComponent ) return decodeURIComponent (str);if(unescape) return unescape(str);} 
function UrlEncode(str){ 
    var ret=""; 
    var strSpecial="!\"#$%&'()*+,/:;<=>?[]^`{|}~%"; 
    for(var i=0;i<str.length;i++){ 
        var chr = str.charAt(i); 
        var c=str2asc(chr); 
        tt += chr+":"+c+"n"; 
        if(parseInt("0x"+c) > 0x7f){ 
            ret+="%"+c.slice(0,2)+"%"+c.slice(-2); 
        }else{ 
        if(chr==" ") 
            ret+="+"; 
        else if(strSpecial.indexOf(chr)!=-1) 
            ret+="%"+c.toString(16); 
        else 
            ret+=chr; 
        } 
    } 
    return ret; 
} 
function UrlDecode(str){ 
    var ret=""; 
    for(var i=0;i<str.length;i++){ 
        var chr = str.charAt(i); 
        if(chr == "+"){ 
            ret+=" "; 
        }else if(chr=="%"){ 
            var asc = str.substring(i+1,i+3); 
            if(parseInt("0x"+asc)>0x7f){ 
                ret+=asc2str(parseInt("0x"+asc+str.substring(i+4,i+6))); 
                i+=5; 
            }else{ 
                ret+=asc2str(parseInt("0x"+asc)); 
                i+=2; 
            } 
        }else{ 
            ret+= chr; 
        } 
    } 
    return ret; 
} 

//********************** AJAX BEGIN ************************//
function ProcessAjaxData(data){eval(data);}
function SetHTML(id,data){var obj = $(id);obj.innerHTML = data;}
function InitRequest()
{
    var C_req = null;
    try{C_req = new ActiveXObject("Msxml2.XMLHTTP");}
    catch(e){
        try{C_req = new ActiveXObject("Microsoft.XMLHTTP");}
        catch(oc){C_req = null;}
    }
    if (!C_req && typeof XMLHttpRequest != "undefined"){
        try{C_req = new XMLHttpRequest();}
        catch(fa){
            alert("对不起!您的浏览器不支持该功能,请使用Internet Explorer 6.0或FireFox浏览器!");
            C_req = null;
        }
    }
    return C_req;
}
function PostRequest(url, rid)
{
    var AjaxRequestObj = InitRequest();
    if (AjaxRequestObj != null){
        AjaxRequestObj.onreadystatechange = function (){
            if (AjaxRequestObj.readyState == 4 && AjaxRequestObj.responseText)
            {
                ProcessAjaxData(AjaxRequestObj.responseText);
             }
        };
        AjaxRequestObj.open("POST", url, true);
        AjaxRequestObj.setRequestHeader("Content-Type","application/x-www-form-urlencoded");
        var SendData = 'id='+rid;
        AjaxRequestObj.send(SendData);
    }
}

//**********************  AJAX  END  ************************//
