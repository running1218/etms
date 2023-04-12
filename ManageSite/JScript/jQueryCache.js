//get current url path
function getCurrentPath() {
    var index = location.href.indexOf('?');
    if (index == -1) {
        return location.href;
    }
    else {
        return location.href.substring(0, index);
    }
}

//get string hashcode
function getHashCode(str) {
    var hash = 0;
    if (str.length == 0) return hash;
    for (i = 0; i < str.length; i++) {
        char = str.charCodeAt(i);
        hash = ((hash << 5) - hash) + char;
        hash = hash & hash; // Convert to 32bit integer  ( javascript中的int值和java中有区别 )
    }
    return hash;
}


var jqueryCacheCookieKey = "jqueryCache.Cookie";

function getCacheValue(key) { 
    var jsonString = $.cookie(jqueryCacheCookieKey);
    if (jsonString == null) {
        return null;
    }
    else {
        var hashtable = new jQuery.Hashtable();
        hashtable.Deserialize(jsonString);
        // path for key
        var urlKey = key+ getHashCode(getCurrentPath());
        return hashtable.getValue(urlKey);
    }
}

function setCacheValue(key, value) {    
    var hashtable = new jQuery.Hashtable();
    var jsonString = $.cookie(jqueryCacheCookieKey);
    if (jsonString != null) {
        hashtable.Deserialize(jsonString);
    }
    //get url hashcode
    var urlKey = key + getHashCode(getCurrentPath());
    //add to hashtable
    hashtable.setValue(urlKey, value);
    //save cookie
    $.cookie(jqueryCacheCookieKey, hashtable.Serializable(), { path: '/' });
}
