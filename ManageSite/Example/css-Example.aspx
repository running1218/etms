<%@ Page Language="C#" AutoEventWireup="true" CodeFile="css-Example.aspx.cs" Inherits="css_Example" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="GridviewGray">
        <tr>
            <th>
                类别
            </th>
            <th>
                样式名
            </th>
        </tr>
        <tr>
            <td class="center">
                文字标签
            </td>
            <td>
                <p>
                    padleft10 <span class="padleft10">左偏移10个像素</span></p>
                <p>
                    padright10 <span class="padright10">右偏移10个像素</span></p>
                <p>
                    center <span class="center">文字居中</span></p>
                <p>
                    font-14 <span class="font-14">14号文字</span></p>
                <p>
                    fontBold <span class="fontBold">文字加粗</span></p>
                <p>
                    colorWhite <span class="colorWhite">白色文字</span>
                </p>
                <p>
                    colorRed <span class="colorRed">红色文字</span>
                </p>
                <p>
                    colorYellow <span class="colorYellow">桔红色文字</span>
                </p>
                <p>
                    colorGray<span class="colorGray">灰色文字</span></p>
                <p>
                    colorBlue <span class="colorBlue">蓝色文字</span>
                </p>
                <p>
                    colorGreen <span class="colorGreen">绿色文字</span>
                </p>
            </td>
        </tr>
        <tr>
            <td class="center">
                文字超链接
            </td>
            <td>
                <p>
                    link_colorWhite <a href="#" class="link_colorWhite">文字超链接</a></p>
                <p>
                    link_colorRed <a href="#" class="link_colorRed">文字超链接</a></p>
                <p>
                    link_colorYellow <a href="#" class="link_colorYellow">文字超链接</a></p>
                <p>
                    link_colorGray <a href="#" class="link_colorGray">文字超链接</a></p>
                <p>
                    link_colorBlue<a href="#" class="link_colorBlue">文字超链接</a></p>
                <p>
                    link_colorGreen<a href="#" class="link_colorGreen">文字超链接</a></p>
            </td>
        </tr>
        <tr>
            <td class="center">
                按钮
            </td>
            <td>
                <p>
                    btn_Add
                    <input type="button" class="btn_Add" value="增加" /></p>
                <p>
                    btn_Del
                    <input type="button" class="btn_Del" value="删除" /></p>
                <p>
                    btn_Search
                    <input type="button" class="btn_Search" value="查找" /></p>
                <p>
                    btn_Edit
                    <input type="button" class="btn_Edit" value="" /></p>
                <p>
                    btn_Export
                    <input type="button" class="btn_Export" value="导出" /></p>
                <p>
                    btn_Import
                    <input type="button" class="btn_Import" value="导入" /></p>
                <p>
                    btn_Move
                    <input type="button" class="btn_Move" value="" /></p>
                <p>
                    btn_Ok
                    <input type="button" class="btn_Ok" value="确定" /></p>
                <p>
                <p>
                    btn_Agree
                    <input type="button" class="btn_Agree" value="审核通过" /></p>
                <p>
                <p>
                    btn_Deny
                    <input type="button" class="btn_Deny" value="审核不通过" /></p>
                <p>
                <p>
                    btn_SubmetPj
                    <input type="button" class="btn_SubmetPj" value="提交审核" /></p>
                <p>
                <p>
                    btn_Unapproved
                    <input type="button" class="btn_Unapproved" value="取消审核" /></p>
                <p>
                <p>
                    btn_Nopass
                    <input type="button" class="btn_Nopass" value="审核不通过" /></p>
                <p>
                <p>
                    btn_Repassword
                    <input type="button" class="btn_Repassword" value="密码重置" /></p>
                <p>
                
                <p>
                    btn_Save
                    <input type="button" class="btn_Save" value="保存" /></p>
                <p>
                <p>
                    btn_Close
                    <input type="button" class="btn_Close" value="关闭" /></p>
                <p>
                    btn_Deploy
                    <input type="button" class="btn_Deploy" value="发布" /></p>
                <p>
                    btn_Cancel
                    <input type="button" class="btn_Cancel" value="取消" /></p>
                <p>
                    btn_Exit
                    <input type="button" class="btn_Exit" value="退出" /></p>
                <p>
                    btn_examination
                    <input type="button" class="btn_examination" value="选择试卷" /></p>
                <p>
                    btn_Stop
                    <input type="button" class="btn_Stop" value="" /></p>
                <p>
                    btn_Return <a href="#" class="btn_Return">返回</a></p>
                <p>
                    btn_2
                    <input type="button" class=" btn_2" value="2个文字" /></p>
                <p>
                    btn_4
                    <input type="button" class="btn_4" value="4个文字" /></p>
                <p>
                    btn_6
                    <input type="button" class="btn_6" value="6个文字" /></p>
                <p>
                    btn_8
                    <input type="button" class="btn_8" value="8个文字" /></p>
                <p>
                    btn <span class="btn"><a href="#"><span class="bj">不限字数按钮</span></a></span></p>
            </td>
        </tr>
        <tr>
            <td class="center">
                文本框
            </td>
            <td>
                <p>
                    inputbox_40
                    <input type="text" class="inputbox_40 " /></p>
                <p>
                    inputbox_60
                    <input type="text" class="inputbox_60 " /></p>
                <p>
                    inputbox_70
                    <input type="text" class="inputbox_70 " /></p>
                <p>
                    inputbox_80
                    <input type="text" class="inputbox_80 " /></p>
                <p>
                    inputbox_90
                    <input type="text" class="inputbox_90 " /></p>
                <p>
                    inputbox_100
                    <input type="text" class="inputbox_100 " /></p>
                <p>
                    inputbox_120
                    <input type="text" class="inputbox_120 " /></p>
                <p>
                    inputbox_140
                    <input type="text" class="inputbox_140 " /></p>
                <p>
                    inputbox_160
                    <input type="text" class="inputbox_160 " /></p>
                <p>
                    inputbox_190
                    <input type="text" class="inputbox_190 " /></p>
                <p>
                    inputbox_210
                    <input type="text" class="inputbox_210 " /></p>
                <p>
                    inputbox_300
                    <input type="text" class="inputbox_300 " /></p>
                <p>
                    inputbox_490
                    <input type="text" class="inputbox_490 " /></p>
            </td>
        </tr>
        <tr>
            <td class="center">
                留言备注文本框
            </td>
            <td>
                <p>
                    inputbox_area190
                    <textarea class="inputbox_area190"></textarea></p>
                <p>
                    inputbox_area300
                    <textarea class="inputbox_area300"></textarea></p>
                <p>
                    inputbox_area440
                    <textarea class="inputbox_area440"></textarea></p>
            </td>
        </tr>
        <tr>
            <td class="center">
                下拉列表框
            </td>
            <td>
                <p>
                    select_60
                    <select class="select_60">
                        <option>--请选择--</option>
                    </select></p>
                <p>
                    select_100
                    <select class="select_100">
                        <option>--请选择--</option>
                    </select></p>
                <p>
                    select_120
                    <select class="select_120">
                        <option>--请选择--</option>
                    </select></p>
                <p>
                    select_150<select class="select_150"><option>--请选择--</option>
                    </select></p>
                <p>
                    select_190<select class="select_190"><option>--请选择--</option>
                    </select></p>
                <p>
                    select_390<select class="select_390"><option>--请选择--</option>
                    </select></p>
            </td>
        </tr>
        <tr>
            <td class="center">
                功能标题
            </td>
            <td>
                <p>
                    dv_title
                    <h2 class="dv_title">
                        功能标题</h2>
                </p>
            </td>
        </tr>
    </table>
</body>
</html>
