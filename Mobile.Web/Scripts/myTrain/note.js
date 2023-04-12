Cookies.set("cookie_worker", 3);
function GetNote() {
    common.call(AppPath+"/MyTrain/GetNote", { "NoteID": NoteID }, 'get', function (data) {
        if (data.Status) {
            $(".note_title").text(data.Data.Title);
            $(".note_content").text(data.Data.NoteContent);
        } else {
            layer.msg("获取数据失败");
        }
    }, error);
}

GetNote();