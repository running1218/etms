function checkMobile(str) {
    var re = /^1\d{10}$/ 
    return re.test(str);      
}
function checkEmail(str) {
    var re = /^(\w-*\.*)+@(\w-?)+(\.\w{2,})+$/
    return re.test(str);
}


