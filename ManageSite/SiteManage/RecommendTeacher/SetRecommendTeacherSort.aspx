<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MPagePop.Master" CodeFile="SetRecommendTeacherSort.aspx.cs" Inherits="SiteManage_RecommendTeacher_SetRecommendTeacherSort" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        table.GridviewGrayboder table, table.GridviewGrayboder table tr td, table.GridviewGrayboder table tr th
        {
            border: none;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            var TeacherList = $("#<%= lbTeacherSort.ClientID %>")[0];
            $("#inpSave").click(function () {
                var ItemTeacherIDs = "";
                for (var i = 0; i < TeacherList.length; i++) {
                    ItemTeacherIDs += TeacherList.options[i].value + ",";
                }
                if (ItemTeacherIDs == "") {
                    popAlertMsg('没有可排序的推荐讲师。', '提示');
                    return;
                }

                $.post("../Controls/ItemTeacherRecommendSort.ashx", {
                    ItemTeacherIDs: ItemTeacherIDs
                }, function (data) {
                    if (data == "success") {
                        popSuccessMsg('推荐讲师排序成功。', '提示',
                            function () { window.parent.location = window.parent.location });
                    } else {
                        popFailedMsg('推荐讲师排序失败。原因如下：' + data, '提示');
                    }
                });
            });

            //默认选中第一行
            if (TeacherList.length > 0) {
                TeacherList.selectedIndex = 0;
            }

            var x = null;
            //将选中item向上
            function upListItem() {
                var selIndex = TeacherList.selectedIndex;
                if (selIndex < 0) {
                    if (x != null) { clearTimeout(x); }
                    return;
                }
                if (selIndex == 0) {
                    if (x != null) { clearTimeout(x); }
                    return;
                }
                var selValue = TeacherList.options[selIndex].value;
                var selText = TeacherList.options[selIndex].text;
                TeacherList.options[selIndex].value = TeacherList.options[selIndex - 1].value;
                TeacherList.options[selIndex].text = TeacherList.options[selIndex - 1].text;
                TeacherList.options[selIndex - 1].value = selValue;
                TeacherList.options[selIndex - 1].text = selText;
                TeacherList.selectedIndex = selIndex - 1;
                if (selIndex + 1 > 0) {
                    x = setTimeout(upListItem, 50)
                }
            }

            //将选中item向下
            function downListItem() {
                var selIndex = TeacherList.selectedIndex;
                if (selIndex < 0) {
                    if (x != null) { clearTimeout(x); }
                    return;
                }
                if (selIndex == TeacherList.options.length - 1) {
                    if (x != null) { clearTimeout(x); }
                    return;
                }
                var selValue = TeacherList.options[selIndex].value;
                var selText = TeacherList.options[selIndex].text;
                TeacherList.options[selIndex].value = TeacherList.options[selIndex + 1].value;
                TeacherList.options[selIndex].text = TeacherList.options[selIndex + 1].text;
                TeacherList.options[selIndex + 1].value = selValue;
                TeacherList.options[selIndex + 1].text = selText;
                TeacherList.selectedIndex = selIndex + 1;
                if (selIndex + 1 < TeacherList.options.length - 1) {
                    x = setTimeout(downListItem, 50)
                }
            }

            //置顶
            $("#firstCmd").click(function () {
                x = setTimeout(upListItem, 50);
            });

            //往上移动
            $("#upCmd").click(function () {
                upListItem();
                clearTimeout(x);
            });
            //超过0.3秒启动连续的向上的操作
            $("#upCmd").mousedown(function () {
                x = setTimeout(upListItem, 300);
            });
            $("#upCmd").mouseup(function () {
                clearTimeout(x);
            });

            //往下移动
            $("#downCmd").click(function () {
                downListItem();
                clearTimeout(x);
            });
            //超过0.3秒启动连续的向下的操作
            $("#downCmd").mousedown(function () {
                x = setTimeout(downListItem, 300);
            });
            $("#downCmd").mouseup(function () {
                clearTimeout(x);
            });

            //置底
            $("#lastCmd").click(function () {
                x = setTimeout(downListItem, 50);
            });
        });
    </script>
    <div class="dv_information">
        <table class="GridviewGray GridviewGrayboder">
            <tr>
                <td colspan="2">
                    推荐讲师排序
                </td>
            </tr>
            <tr>
                <td style="text-align: center;">
                    <select id="lbTeacherSort" runat="server" class="list-box" size="25" style="width: 300px;">
                    </select>
                    <asp:Label ID="labItemID" runat="server"></asp:Label>
                </td>
                <td valign="middle">
                    <table style="width: 100%;">
                        <tr>
                            <td align="center">
                                <input id="firstCmd" type="button" class="btn move-up" title="置顶" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <input id="upCmd" type="button" class="btn move-top" title="上移（按下鼠标超过0.3秒将连续上移）" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <input id="downCmd" type="button" class="btn move-down" title="下移（按下鼠标超过0.3秒将连续下移）" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <input id="lastCmd" type="button" class="btn move-bottom" title="置底" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div class="dv_submit">
        <input type="button" id="inpSave" value="保存" class="btn_Save" />
        <asp:Button ID="btnReturn" runat="server" SkinID="Return" Text="取消" OnClientClick="closeWindow()" />
    </div>
</asp:Content>

