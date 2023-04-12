<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="SetsCourseEdit.aspx.cs" Inherits="ETMS.WebApp.Manage.TraningPlan.TraningPlan.SetsCourseEdit" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--功能标题-->
        <h2 class="dv_title">
            编辑课程
        </h2>
        <div class="dv_information">
            <table class="GridviewGray">
                <tr>
                    <th>
                        计划名称：
                    </th>
                    <td colspan="3">
                        2012培训计划名称
                    </td>
                </tr>
                <tr>
                    <th>
                        课程名称：
                    </th>
                    <td>
                        领导力培训
                    </td>
                    <th>
                        所属组织机构：
                    </th>
                    <td>
                    </td>
                </tr>
                <tr>
                    <th width="20%">
                        课程类型：
                    </th>
                    <td width="30%">
                        管理发展
                    </td>
                    <th width="20%">
                        课程等级：
                    </th>
                    <td width="30%">
                        中级
                    </td>
                </tr>
                <tr>
                    <th>
                        课程状态：
                    </th>
                    <td>
                        启用
                    </td>
                    <th>
                        优秀课程：
                    </th>
                    <td>
                        是
                    </td>
                </tr>
                <tr>
                    <th>
                        课程属性：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="DictionaryDropDownList2" DictionaryType="Dic_CourseAttributeType"
                            IsShowChoose="true" />
                    </td>
                    <th>
                        授课方式：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="DictionaryDropDownList4" DictionaryType="Dic_CourseTrainingMethods"
                            IsShowChoose="true" />
                    </td>
                </tr>
                <tr>
                    <th>
                        培训方式：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="DictionaryDropDownList3" DictionaryType="Dic_CourseTrainingMode"
                            IsShowChoose="true" />
                    </td>
                    <th>
                        外训机构：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="DictionaryDropDownList1" DictionaryType="Dic_OutsideOrganization"
                            IsShowChoose="true" />
                    </td>
                </tr>
                <tr>
                    <th>
                        讲师来源：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="DictionaryDropDownList5" DictionaryType="Dic_ProfessorSource"
                            IsShowChoose="true" />
                    </td>
                    <th>
                        课&nbsp;&nbsp;&nbsp;&nbsp;时：
                    </th>
                    <td>
                        <input type="text" name="textfield6" class="inputbox_120" />
                    </td>
                </tr>
                <tr>
                    <th>
                        预&nbsp;&nbsp;&nbsp;&nbsp;算：
                    </th>
                    <td colspan="3">
                        <input type="text" name="textfield6" class="inputbox_120" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="dv_submit">
        <a href="#" class="btn_Save" onclick="javascript:popSuccessMsg('恭喜你保存成功！','提示',closeWindow);">
            保存</a><a href="javascript:closeWindow();" class="btn_Close">关闭</a></div>
</asp:Content>
