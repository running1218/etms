/*
* =========================================================================
* Author:chenzw, AddDate:2012-02-17
* ========================================================================
* 框架集left.aspx页面的菜单树初始化js,分为一级，二级，三级菜单
* 初始化菜单树函数：writeMenu(true);
* 一级菜单展开函数：menuClick(this);
* 二级菜单展开函数：subMenuClick(this);
* 关闭菜单函数：closeMenu();
* 打开内容页面函数：gotoUrl(url,target);
*/

var SubImg = 'App_Themes/ThemeAdmin/Images/menu_arrow_close.gif';
var SubImgOpen = 'App_Themes/ThemeAdmin/Images/menu_arrow_open.gif';
var alreadyOpenTableId = null;
var hrefBaseValue = null;
/* 关闭菜单函数*/
function closeMenu() {
    if (alreadyOpenTableId == null) return;
    alreadyOpenTable = document.getElementById(alreadyOpenTableId);
    targetTableId = alreadyOpenTableId + "d";
    targetTable = document.getElementById(targetTableId);
    targetTable.style.display = "none";
    alreadyOpenTableId = null;
    $(alreadyOpenTable).removeClass("level1SelectedStyle");
    $(alreadyOpenTable).find(".lefve1SelectArrow").remove();
}
/* 一级菜单展开函数*/
function menuClick(tableSrc) {
    var currentTableSrc = tableSrc;
    if (currentTableSrc.id != alreadyOpenTableId) closeMenu();
    targetTableId = currentTableSrc.id + "d";
    targetTable = document.getElementById(targetTableId);
    if (targetTable.style.display == "none") {        
        $(targetTable).fadeIn("normal");
        alreadyOpenTableId = currentTableSrc.id;
        $(currentTableSrc).addClass("level1SelectedStyle");
        var str="<div class='lefve1SelectArrow'></div>";      
        if  ($(currentTableSrc).find(".lefve1SelectArrow").length==0)
            $(currentTableSrc).append(str);
    } else {
        $(targetTable).slideUp("normal");
    }        
}
/* 二级菜单展开函数*/
function subMenuClick(tableSrc) {
    subTableId = tableSrc.id + "d";
    subTable = document.getElementById(subTableId);
    var tableSrcImgId = tableSrc.id + "_img";
    var tableSrcImg = document.getElementById(tableSrcImgId);
    if (subTable != null) {
        if (subTable.style.display == "none") {          
            $(subTable).fadeIn("normal");
            tableSrcImg.src = SubImgOpen;
        } else {           
            $(subTable).slideUp("normal");
            tableSrcImg.src = SubImg;
        }
    }   
}
/* 打开内容页面函数*/
function gotoUrl(url, target,obj) {   
    url = UpdateUrlWithParam(url, '', parseInt(Math.random() * 10000));
    window.open(url, target);
    $("li.level3Head,li.level33,li.level32").removeClass("sectedSubmenu");
    $(obj).addClass("sectedSubmenu");
}

(function () {
   $(function(){
    
    if ($("#dv_Menu").find(".level1").length <=2){
        $("#MenuUl").find(".MenuLevel2,.level2").show();
       $("#dv_Menu").find(".level1>div").addClass("level1SelectedStyle");
       $("#dv_Menu").find(".level1>div").append("<div class=\"lefve1SelectArrow\"></div>");
       $("#MenuUl").find(".level2Style").find("img").attr("src",SubImgOpen);
       }

    $(".level2").find("li").hover(function () {
        $(this).addClass("Submenuhover");
    }, function () {
        $(this).removeClass("Submenuhover");
    })
   })
   
})()

