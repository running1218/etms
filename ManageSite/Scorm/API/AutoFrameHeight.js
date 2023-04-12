
var time = "";
function setFrameHeight() {

    clearInterval(time);
    time = setInterval("autoHeight()", 50);
}

function autoHeight() {
    
  
    var bodyHeight = 0;    
    var mainbody = window.parent.document.body; 
    var rightFrame = document.getElementById("rightFrame");
    var mainFrame = parent.document.getElementById("mFrame"); 
  

    if (leftFrame == undefined || leftFrame==null)
        return;
    else {

        if (rightFrame == undefined || rightFrame == null)
            return;
        else {
            bodyHeight = mainbody.clientHeight;
            rightFrame.style.height = bodyHeight + "px";
            mainFrame.style.height = bodyHeight + "px";
           
            
        }

    }
}


  
