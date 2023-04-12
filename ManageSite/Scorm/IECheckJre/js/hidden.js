//Div弹出窗口 绝对定位
var head="display:''"
function doit(header){
var head=header.style
if (head.display=="none")
head.display=""
else
head.display="none"
}


//Div弹出窗口 相对定位
function getPosition() {
      var top    = document.documentElement.scrollTop;
      var left   = document.documentElement.scrollLeft;
      var height = document.documentElement.clientHeight;
      var width  = document.documentElement.clientWidth;
      return {top:top,left:left,height:height,width:width};
}

function showPop(pop){
	
	var obj    = document.getElementById(pop);
	var widthStr  = obj.style.width;  //Ŀ
	var heightStr = obj.style.height;  //ĸ߶
	var width = widthStr.substring(0, widthStr.length-2);
	var height = heightStr.substring(0, heightStr.length-2);
	
	obj.style.display  = "block";
	obj.style.position = "absolute";
	obj.style.zindex   = "999";
	//obj.style.width    = width + "px";
	//obj.style.height   = height + "px";
	
	var Position = getPosition();
	leftadd = (Position.width-width)/2;
	topadd  = (Position.height-height)/2;
	obj.style.top  = (Position.top  + topadd-100)  + "px";//相对页面顶部的距离比如topadd-100
	obj.style.left = (Position.left + leftadd) + "px";
	
	window.onscroll = function (){
		var Position   = getPosition();
		obj.style.top  = (Position.top  + topadd-100)  +"px";//相对页面顶部的距离比如topadd-100
		obj.style.left = (Position.left + leftadd) +"px";
	};
}

 

