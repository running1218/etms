<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuestionSort.aspx.cs" MasterPageFile="~/MasterPages/MPagePop.Master"
    Inherits="QS_QuestionSort" %>

<asp:Content runat="server" ID="PageContent" ContentPlaceHolderID="ContentPlaceHolder1">
    <style type="text/css">
        table.GridviewGrayboder table, table.GridviewGrayboder table tr td, table.GridviewGrayboder table tr th
        {
            border: none;
        }
    </style>
    <script type="text/javascript">
        /**   
        *   使选中的项目上移   
        *   
        *   oSelect:   源列表框   
        *   isToTop:   是否移至选择项到顶端，其它依次下移，   
        *                     true为移动到顶端，false反之，默认为false   
        */
        function moveUp(oSelect, isToTop) {
            //默认状态不是移动到顶端   
            if (isToTop == null)
                var isToTop = false;

            //如果是多选------------------------------------------------------------------   
            if (oSelect.multiple) {
                for (var selIndex = 0; selIndex < oSelect.options.length; selIndex++) {
                    //如果设置了移动到顶端标志   
                    if (isToTop) {
                        if (oSelect.options[selIndex].selected) {
                            var transferIndex = selIndex;
                            while (transferIndex > 0 && !oSelect.options[transferIndex - 1].selected) {
                                oSelect.options[transferIndex].swapNode(oSelect.options[transferIndex - 1]);
                                transferIndex--;
                            }
                        }
                    }
                    //没有设置移动到顶端标志   
                    else {
                        if (oSelect.options[selIndex].selected) {
                            if (selIndex > 0) {
                                if (!oSelect.options[selIndex - 1].selected)
                                    oSelect.options[selIndex].swapNode(oSelect.options[selIndex - 1]);
                            }
                        }
                    }
                }
            }
            //如果是单选--------------------------------------------------------------------   
            else {
                var selIndex = oSelect.selectedIndex;
                if (selIndex <= 0)
                    return;
                //如果设置了移动到顶端标志   
                if (isToTop) {
                    while (selIndex > 0) {
                        oSelect.options[selIndex].swapNode(oSelect.options[selIndex - 1]);
                        selIndex--;
                    }
                }
                //没有设置移动到顶端标志   
                else
                    oSelect.options[selIndex].swapNode(oSelect.options[selIndex - 1]);
            }
        }

        /**   
        *   使选中的项目下移   
        *   
        *   oSelect:   源列表框   
        *   isToTop:   是否移至选择项到底端，其它依次上移，   
        *                     true为移动到底端，false反之，默认为false   
        */
        function moveDown(oSelect, isToBottom) {
            //默认状态不是移动到顶端   
            if (isToBottom == null)
                var isToBottom = false;

            var selLength = oSelect.options.length - 1;

            //如果是多选------------------------------------------------------------------   
            if (oSelect.multiple) {
                for (var selIndex = oSelect.options.length - 1; selIndex >= 0; selIndex--) {
                    //如果设置了移动到顶端标志   
                    if (isToBottom) {
                        if (oSelect.options[selIndex].selected) {
                            var transferIndex = selIndex;
                            while (transferIndex < selLength && !oSelect.options[transferIndex + 1].selected) {
                                oSelect.options[transferIndex].swapNode(oSelect.options[transferIndex + 1]);
                                transferIndex++;
                            }
                        }
                    }
                    //没有设置移动到顶端标志   
                    else {
                        if (oSelect.options[selIndex].selected) {
                            if (selIndex < selLength) {
                                if (!oSelect.options[selIndex + 1].selected)
                                    oSelect.options[selIndex].swapNode(oSelect.options[selIndex + 1]);
                            }
                        }
                    }
                }
            }
            //如果是单选--------------------------------------------------------------------   
            else {
                var selIndex = oSelect.selectedIndex;
                if (selIndex >= selLength - 1)
                    return;
                //如果设置了移动到顶端标志   
                if (isToBottom) {
                    while (selIndex < selLength - 1) {
                        oSelect.options[selIndex].swapNode(oSelect.options[selIndex + 1]);
                        selIndex++;
                    }
                }
                //没有设置移动到顶端标志   
                else
                    oSelect.options[selIndex].swapNode(oSelect.options[selIndex + 1]);
            }
        }

        function saveSortReault() {
            var CourseList = $("#<%= selQuestionSort.ClientID %>")[0];
            var queryTitleList = new Array();
            for (var i = 0; i < CourseList.length; i++) {
                var QS_QueryTitle = { TitleID: "", TitleNo: "" };
                QS_QueryTitle.TitleID = CourseList.options[i].value;
                QS_QueryTitle.TitleNo = i + 1;
                queryTitleList.push(QS_QueryTitle);
            }
            if (queryTitleList.length == 0) {
                popAlertMsg('没有可排序的试题。', '提示');
                return;
            }
            $.post("QsItemSort.ashx", { ActionName: "qsItemSort", queryTitleList: JSON.stringify(queryTitleList) }, function (ReturnValue) {
                if (ReturnValue.error == true) {
                    popSuccessMsg('试题排序成功。', '提示', function () { window.parent.location = window.parent.location });
                } else {
                    popFailedMsg('试题排序失败。原因如下：' + data, '提示');
                }
            }, "json");
        }
    </script>
    <div class="dv_information">
        <table class="GridviewGray GridviewGrayboder">
            <tr>
                <td colspan="2">
                    试题题目名称
                </td>
            </tr>
            <tr>
                <td style="text-align: center;">
                    <select id="selQuestionSort" runat="server" class="list-box" size="25" style="width: 300px;">
                    </select>
                </td>
                <td valign="middle">
                    <table style="width: 100%;">
                        <tr>
                            <td align="center">
                                <input id="firstCmd" type="button" class="btn move-up" onclick="moveUp($('#<%=selQuestionSort.ClientID %>')[0],true)"
                                    title="置顶" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <input id="upCmd" type="button" class="btn move-top" onclick="moveUp($('#<%=selQuestionSort.ClientID %>')[0],false)"
                                    title="上移" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <input id="downCmd" type="button" class="btn move-down" onclick="moveDown($('#<%=selQuestionSort.ClientID %>')[0],false)"
                                    title="下移" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <input id="lastCmd" type="button" onclick="moveDown($('#<%=selQuestionSort.ClientID %>')[0],true)"
                                    class="btn move-bottom" title="置底" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div class="dv_submit">
        <input type="button" class="btn_Add" value="保存" onclick="saveSortReault()" />
        <%--       <asp:Button ID="btnAdd" runat="server" Text="保存" CommandName="add" ValidationGroup="Edit"
            SkinID="Insert" OnClientClick="saveSortReault()" />--%>
        <asp:Button ID="btnReturn" runat="server" SkinID="Return" Text="取消" OnClientClick="closeWindow()" />
    </div>
</asp:Content>
