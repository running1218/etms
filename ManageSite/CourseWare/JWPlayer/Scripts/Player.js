
function player(obj) {
    if (obj != "" || obj != null) {
        jwplayer("container").setup({
            flashplayer: "Player/player.swf",
            file: obj,
            height: 540,
            width: 960,
            autostart: true,
            skin: "JwPlayerSkin/skin.zip"
        });
    } else {
        alert("文件路径错误");
    }
}