<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="SetsCourseSort.aspx.cs" Inherits="TraningImplement_TraningProjectManager_SetsCourseSort" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        table.GridviewGrayboder table, table.GridviewGrayboder table tr td, table.GridviewGrayboder table tr th
        {
            border: none;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            var CourseList = $("#<%= lbCourseSort.ClientID %>")[0];
            $("#inpSave").click(function () {
                var ItemCourseIDs = "";
                for (var i = 0; i < CourseList.length; i++) {
                    ItemCourseIDs += CourseList.options[i].value + ",";
                }
                if (ItemCourseIDs == "") {
                    popAlertMsg('没有可排序的课程。', '提示');
                    return;
                }

                $.post("Controls/ItemCourseSort.ashx", {
                    ItemCourseIDName: ItemCourseIDs
                }, function (data) {
                    if (data == "success") {
                        popSuccessMsg('项目课程排序成功。', '提示', function () { window.parent.location = window.parent.location });
                    } else {
                        popFailedMsg('项目课程排序失败。原因如下：' + data, '提示');
                    }
                });
            });

            //默认选中第一行
            if (CourseList.length > 0) {
                CourseList.selectedIndex = 0;
            }

            var x = null;
            //将选中item向上
            function upListItem() {
                var selIndex = CourseList.selectedIndex;
                if (selIndex < 0) {
                    if (x != null) { clearTimeout(x); }
                    return;
                }
                if (selIndex == 0) {
                    if (x != null) { clearTimeout(x); }
                    return;
                }
                var selValue = CourseList.options[selIndex].value;
                var selText = CourseList.options[selIndex].text;
                CourseList.options[selIndex].value = CourseList.options[selIndex - 1].value;
                CourseList.options[selIndex].text = CourseList.options[selIndex - 1].text;
                CourseList.options[selIndex - 1].value = selValue;
                CourseList.options[selIndex - 1].text = selText;
                CourseList.selectedIndex = selIndex - 1;
                if (selIndex + 1 > 0) {
                    x = setTimeout(upListItem, 50)
                }
            }

            //将选中item向下
            function downListItem() {
                var selIndex = CourseList.selectedIndex;
                if (selIndex < 0) {
                    if (x != null) { clearTimeout(x); }
                    return;
                }
                if (selIndex == CourseList.options.length - 1) {
                    if (x != null) { clearTimeout(x); }
                    return;
                }
                var selValue = CourseList.options[selIndex].value;
                var selText = CourseList.options[selIndex].text;
                CourseList.options[selIndex].value = CourseList.options[selIndex + 1].value;
                CourseList.options[selIndex].text = CourseList.options[selIndex + 1].text;
                CourseList.options[selIndex + 1].value = selValue;
                CourseList.options[selIndex + 1].text = selText;
                CourseList.selectedIndex = selIndex + 1;
                if (selIndex + 1 < CourseList.options.length - 1) {
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
                    项目课程排序
                </td>
            </tr>
            <tr>
                <td style="text-align: center;">
                    <select id="lbCourseSort" runat="server" class="list-box" size="25" style="width: 300px;">
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
        <span style="display: none">
            <asp:Button ID="btn_Save" runat="server" Text="保存" CssClass="btnsave1" OnClick="Btn_Save_Click" /></span>
        <asp:Button ID="btnReturn" runat="server" SkinID="Return" Text="取消" OnClientClick="closeWindow()" />
    </div>
</asp:Content>
