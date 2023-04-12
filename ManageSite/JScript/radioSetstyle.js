var r = new Array();
var classArray = new Array("radioSlectedstyle", "radiostyle", "checkboxStyle", "checkboxSelectedStyle", "checkboxDisableStyle");
(function ($) {
    $.fn.extend({
        radioStyle: function () {            
            return this.each(function (i, obj) {               
                if (this.type == "radio")
                    var rId = this.id + "_radio";                             
                else
                    var rId = this.id + "_checkbox";
                if (this.type=="text")
                {
                   $(this).addClass("inputtext_bj1");                   
                   $(this).hover(function(){
                      $(this).addClass("inputtext_over");
                   },function(){
                       $(this).removeClass("inputtext_over");
                   })
                   $(this).bind("focus",function(){
                      $(this).addClass("inputtext_focus");
                   })
                   $(this).bind("blur",function(){
                      $(this).removeClass("inputtext_focus");
                   })
                }
                if (this.type == "radio" || this.type == "checkbox") {                 
                 
                    var classStr = this.parentNode.className;
                    var duibiStr = classArray.join(",");
                    if (duibiStr.indexOf(classStr) <= 0)
                       {                               
                          $(this).before("<span onfocus='blur()' id='" + rId + "'></span>").prependTo($("#" + rId));                               
                                
                        }
                    this.style.opacity = 0;this.style.filter = "alpha(opacity=0)";
                    this.style.border = 0;

                    this.onclick = function () {
                        $.fn.select_element(this);
                        r= document.getElementsByTagName("input");
                        $.fn.unfocus();
                    }
                    this.onfocus = function () {
                        this.blur();
                    }
                    if (this.checked == true) {
                        $.fn.select_element(this);
                    }
                    else {                        
                        $.fn.select_element(this, 1);
                    }
                }
            })
        },
        select_element: function (obj, type) {
            //单选按钮选中的样式         
            obj.parentNode.className = "radioSlectedstyle";
            if (obj.type == "checkbox") {
                //复选框选中             
                obj.parentNode.className = "checkboxSelectedStyle";
            }
            if (type) {
                //单选按钮没有选中后的样式            
                obj.parentNode.className = 'radiostyle';
                //复选框没有选中  
                if (obj.type == "checkbox" && obj.disabled ==false) {
                    obj.parentNode.className = 'checkboxStyle';
                }
                else{
                   if (obj.type=="checkbox")
                    obj.parentNode.className = 'checkboxDisableStyle';
                 
                }
            }
        },
        unfocus: function () {
            for (var i = 0; i < r.length; i++) {                
                if (r[i].type == "radio" || r[i].type == "checkbox") { if (r[i].checked == false) { $.fn.select_element(r[i], 1) } }

            }
        }
    })
})(jQuery);
