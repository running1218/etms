<%@ Page Title="修改课时安排状态" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master"
    AutoEventWireup="true" CodeFile="CoursePeriodEdit.aspx.cs" Inherits="TraningImplement_TraningProjectResult_CoursePeriodEdit" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--功能标题-->
    <h2 class="dv_title">
        修改课时安排状态
    </h2>
    <!--表单录入-->
    <div class="dv_information">
        <table class="GridviewGray">
            <tr>
                <th>
                    课程名称：
                </th>
                <td>
                    世界精品课程
                </td>
                <th>
                    讲师姓名：
                </th>
                <td>
                    张老师
                </td>
            </tr>
            <tr>
                <th>
                    培训时间：
                </th>
                <td>
                    2012-2-28 9：00-12：00
                </td>
                <th>
                    培训时间说明：
                </th>
                <td>
                    正班时间
                </td>
            </tr>
            <tr>
                <th>
                    课时安排状态：
                </th>
                <td colspan="3">
                    <cc1:DictionaryDropDownList runat="server" ID="DictionaryDropDownList3" DictionaryType="Dic_TraningCoursePeriodState"
                        IsShowChoose="true" />
                </td>
            </tr>
            <tr>
                <th>
                    备&nbsp;&nbsp;&nbsp;&nbsp;注：
                </th>
                <td colspan="3">
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="inputbox_area300"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div class="dv_submit">
        <input type="button" class="btn_Save" onclick="javascript:popSuccessMsg('恭喜你保存成功！','提示',closeWindow);" value="保存"/>
            <input type="button" class="btn_Cancel" value="取消" onclick="javascript:closeWindow();" /></div>
</asp:Content>
