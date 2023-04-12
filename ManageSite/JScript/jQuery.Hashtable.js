jQuery.Hashtable = function () {
    this.items = new Array();
    this.itemsCount = 0;
    this.setValue = function (key, value) {
        if (!this.containsKey(key)) {           
            this.items[key] = value;
            this.itemsCount++;
            this.items.length = this.itemsCount;
        }
        else {
            //throw "key '" + key + "' allready exists."
            this.items[key] = value;
        }
    }

    this.getValue = function (key) {
        if (this.containsKey(key))
            return this.items[key];
        else
            return null;
    }

    this.remove = function (key) {
        if (this.containsKey(key)) {
            delete this.items[key];
            this.itemsCount--;
            this.items.length = this.itemsCount;
        }
        else
            throw "key '" + key + "' does not exists."

    }

    this.containsKey = function (key) {
        return typeof (this.items[key]) != "undefined";
    }

    this.containsValue = function containsValue(value) {
        for (var item in this.items) {
            if (this.items[item] == value)
                return true;
        }

        return false;
    }

    this.contains = function (keyOrValue) {
        return this.containsKey(keyOrValue) || this.containsValue(keyOrValue);
    }

    this.clear = function () {
        this.items = new Array();
        itemsCount = 0;
    }

    this.size = function () {
        return this.itemsCount;
    }

    this.isEmpty = function () {
        return this.size() == 0;
    }
    //把hashtable序列化,一定要序列化,否则hashtable不能直接存入cookie
    this.Serializable = function () {
        var result = new Array();
        result.length = this.itemsCount;
        var i = 0;
        for (var item in this.items) {
            result[i] = { k: item, v: this.items[item] };
            i++;
        }
        return JSON.stringify(result);
    }
    //把hashtable反序列化
    this.Deserialize = function (jsonString) {
        var array = JSON.parse(jsonString);
        for (var i = 0; i < array.length; i++) {
            this.setValue(array[i].k, array[i].v);
        }
    }
};