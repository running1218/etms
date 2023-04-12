
this.tablecloth = function () {

    var highlightCols = true;
    var highlightRows = true;
    var selectable = true;
    this.clickAction = function (obj) {

    };

    var tableover = false;
    this.start = function () {
        var tables = $(".dv_searchlist table.GridviewGray");
        for (var i = 0; i < tables.length; i++) {
            tables[i].onmouseover = function () { tableover = true };
            tables[i].onmouseout = function () { tableover = false };
            rows(tables[i]);
        };
    };

    this.rows = function (table) {
        var css = "";
        var tr = table.getElementsByTagName("tr");
        for (var i = 0; i < tr.length; i++) {
            css = (css == "odd") ? "even" : "odd";
            tr[i].className = css;
            var arr = new Array();
            for (var j = 0; j < tr[i].childNodes.length; j++) {
                if (tr[i].childNodes[j].nodeType == 1) arr.push(tr[i].childNodes[j]);
            };
            for (var j = 0; j < arr.length; j++) {
                arr[j].row = i;
                arr[j].col = j;
                if (arr[j].innerHTML == "&nbsp;" || arr[j].innerHTML == "") arr[j].className += " empty";
                arr[j].css = arr[j].className;
                arr[j].onmouseover = function () {
                    over(table, this, this.row, this.col);
                };
                arr[j].onmouseout = function () {
                    out(table, this, this.row, this.col);
                };
                arr[j].onmousedown = function () {
                    down(table, this, this.row, this.col);
                };
                arr[j].onmouseup = function () {
                    up(table, this, this.row, this.col);
                };
                arr[j].onclick = function () {
                    click(table, this, this.row, this.col);
                };
            };
        };
    };


    this.over = function (table, obj, row, col) {
        if (!highlightCols && !highlightRows) obj.className = obj.css + " over";
        if (check1(obj, col)) {
            if (highlightCols) highlightCol(table, obj, col);
            if (highlightRows) highlightRow(table, obj, row);
        };
    };

    this.out = function (table, obj, row, col) {
        if (!highlightCols && !highlightRows) obj.className = obj.css;
        unhighlightCol(table, col);
        unhighlightRow(table, row);
    };

    this.down = function (table, obj, row, col) {
        obj.className = obj.css + " down";
    };

    this.up = function (table, obj, row, col) {
        obj.className = obj.css + " over";
    };

    this.click = function (table, obj, row, col) {
        if (check1) {
            if (selectable) {
                unselect(table);
                if (highlightCols) highlightCol(table, obj, col, true);
                if (highlightRows) highlightRow(table, obj, row, true);
                document.onclick = unselectAll;
            }
        };
        clickAction(obj);
    };

    this.highlightCol = function (table, active, col, sel) {
        var css = (typeof (sel) != "undefined") ? "selected" : "over";
        var tr = table.getElementsByTagName("tr");
        for (var i = 0; i < tr.length; i++) {
            var arr = new Array();
            for (j = 0; j < tr[i].childNodes.length; j++) {
                if (tr[i].childNodes[j].nodeType == 1) arr.push(tr[i].childNodes[j]);
            };
            var obj = arr[col];
            if (obj == undefined) return;
            if (check2(active, obj) && check3(obj)) obj.className = obj.css + " " + css;
        };
    };
    this.unhighlightCol = function (table, col) {
        var tr = table.getElementsByTagName("tr");
        for (var i = 0; i < tr.length; i++) {
            var arr = new Array();
            for (j = 0; j < tr[i].childNodes.length; j++) {
                if (tr[i].childNodes[j].nodeType == 1) arr.push(tr[i].childNodes[j])
            };
            var obj = arr[col];
            if (obj == undefined) return;
            if (check3(obj)) obj.className = obj.css;
        };
    };
    this.highlightRow = function (table, active, row, sel) {
        var css = (typeof (sel) != "undefined") ? "selected" : "over";
        var tr = table.getElementsByTagName("tr")[row];
        for (var i = 0; i < tr.childNodes.length; i++) {
            var obj = tr.childNodes[i];
            if (obj == undefined) return;
            if (check2(active, obj) && check3(obj)) obj.className = obj.css + " " + css;
        };
    };
    this.unhighlightRow = function (table, row) {
        var tr = table.getElementsByTagName("tr")[row];
        for (var i = 0; i < tr.childNodes.length; i++) {
            var obj = tr.childNodes[i];
            if (obj == undefined) return;
            if (check3(obj)) obj.className = obj.css;
        };
    };
    this.unselect = function (table) {
        tr = table.getElementsByTagName("tr")
        for (var i = 0; i < tr.length; i++) {
            for (var j = 0; j < tr[i].childNodes.length; j++) {
                var obj = tr[i].childNodes[j];
                if (obj == undefined) return;
                if (obj.className) obj.className = obj.className.replace("selected", "");
            };
        };
    };
    this.unselectAll = function () {
        if (!tableover) {
            tables = document.getElementsByTagName("table");
            for (var i = 0; i < tables.length; i++) {
                unselect(tables[i])
            };
        };
    };
    this.check1 = function (obj, col) {
        if (obj == undefined)
            return;
        else
            return (!(col == 0 && obj.className.indexOf("empty") != -1));
    }
    this.check2 = function (active, obj) {
        if (obj == undefined)
            return;
        else
            return (!(active.tagName == "TH" && obj.tagName == "TH"));
    };
    this.check3 = function (obj) {
        if (obj == undefined)
            return;
        else
            return (obj.className) ? (obj.className.indexOf("selected") == -1) : true;
    };

    start();

};

window.onload = tablecloth;
