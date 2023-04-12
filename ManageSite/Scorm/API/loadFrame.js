var timeIframe=setInterval("GetIframeStatus()",10);
function GetIframeStatus(){
  var mFrame=document.getElementById("mFrame");
  var iframeWindow=mFrame.contentWindow;
  //内容是否加载完
  if(iframeWindow.document.readyState=="complete"  ){
    var iframeHeight;
    //获取Iframe的内容实际高度
    var leftFrame=iframeWindow.frames["leftFrame"];  
    var iframeHeight1=leftFrame.document.body.scrollHeight;
    var rightFrame=iframeWindow.frames["rightFrame"]; 
    var iframeHeight2=rightFrame.document.body.scrollHeight;
    iframeHeight=Math.max(iframeHeight1,iframeHeight2);
    //设置Iframe的高度
    mFrame.height=iframeHeight;
    clearInterval(timeIframe);
  }
} 