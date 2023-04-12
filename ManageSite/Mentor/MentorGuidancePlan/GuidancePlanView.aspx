<%@ Page Title="查看反馈信息" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="GuidancePlanView.aspx.cs" Inherits="Mentor_MentorGuidancePlan_GuidancePlanView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="dv_information">
        <!--功能标题-->
        <h2 class="dv_title">
            查看反馈信息
        </h2>
        <div class="">
            <div>
                <font color="red">辅导计划详细信息</font></div>
            <table class="GridviewGray">
                <tr>
                    <th width="20%">
                        辅导计划标题：
                    </th>
                    <td>
                        新员工辅导01
                    </td>
                </tr>
                <tr>
                    <th>
                        任务描述：
                    </th>
                    <td>
                        任务描述
                    </td>
                </tr>
                <tr>
                    <th>
                        辅导期间：
                    </th>
                    <td>
                        2010-5-6
                    </td>
                </tr>
                <tr>
                    <th>
                        创建时间：
                    </th>
                    <td>
                        2010-3-6
                    </td>
                </tr>
            </table>
        </div>
        <div class="">
            <div>
                <a href="#A0">提交新反馈</a>
            </div>
            <div>
                辅导计划反馈信息</div>
            <table class="GridviewGray">
                <tr>
                    <th width="20%">
                        学&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;员：
                    </th>
                    <td width="30%">
                        刘勇
                    </td>
                    <th width="20%">
                        反馈时间：
                    </th>
                    <td width="30%">
                        2010-5-6
                    </td>
                </tr>
                <tr>
                    <th>
                        反馈信息：
                    </th>
                    <td colspan="3">
                    </td>
                </tr>
                <tr>
                    <th>
                        导&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;师：
                    </th>
                    <td>
                        王冰
                    </td>
                    <th>
                        反馈时间：
                    </th>
                    <td>
                        2010-5-6
                    </td>
                </tr>
                <tr>
                    <th>
                        反馈信息：
                    </th>
                    <td colspan="3">
                        反馈信息反馈信息反馈信息反馈信息
                    </td>
                </tr>
            </table>
        </div>
        <div class="">
            <div>
                <a name="A0" id="A0"></a>提交新反馈</div>
            <table class="GridviewGray">
                <tr>
                    <th width="20%">
                        反馈信息：
                    </th>
                    <td colspan="3" width="80%">
                        反馈信息反馈信息反馈信息反馈信息
                    </td>
                </tr>
                <tr>
                    <th width="20%">
                        导&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;师：
                    </th>
                    <td width="30%">
                        王冰
                    </td>
                    <th width="20%">反馈时间：
                    </th>
                    <td width="30%">
                        2010-5-6
                    </td>
                </tr>
                <tr>
                    <th>
                        反馈信息：
                    </th>
                    <td colspan="3"><input type="text" name="textfield" class="inputbox_area300" /></td>
                </tr>
            </table>
        </div>
    </div>
    <div class="dv_submit">
        <input type="button" class="btn_Ok" value="提交" onclick="javascript:popSuccessMsg('恭喜你保存成功！','提示',closeWindow);" />
        <input type="button" class="btn_Cancel" value="取消" onclick="javascript:closeWindow();" /></div>
</asp:Content>
