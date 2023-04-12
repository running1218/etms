<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="TraningProjectView.aspx.cs" Inherits="TraningImplement_UpProjectManager_TraningProjectView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <!--功能标题-->
        <h2 class="dv_title">
            培训项目查看
        </h2>
        <div class="dv_information">
            <table class="GridviewGray">
                <tr>
                    <th>
                        项目名称
                    </th>
                    <td colspan="3">
                        android应用开发实践
                    </td>
                    <th>
                        专业类别
                    </th>
                    <td colspan="3">
                        技术
                    </td>
                </tr>
                <tr>
                    <th>
                        组织机构
                    </th>
                    <td colspan="3">
                        研发中心
                    </td>
                    <th>
                        发布状态
                    </th>
                    <td colspan="3">
                        已发布
                    </td>
                </tr>
                <tr>
                    <th>
                        所属计划
                    </th>
                    <td colspan="3">
                        移动终端应用
                    </td>
                    <th>
                        预算
                    </th>
                    <td colspan="3">
                        2500
                    </td>
                </tr>
                <tr>
                    <th>
                        创建日期
                    </th>
                    <td colspan="3">
                        2012-2-29
                    </td>
                    <th>
                        负责人
                    </th>
                    <td colspan="3">
                        高蕾
                    </td>
                </tr>
                <tr>
                    <th>
                        报名方式
                    </th>
                    <td colspan="3">
                        学员报名
                    </td>
                    <th>
                        报名时间段
                    </th>
                    <td colspan="3">
                        2012/2/26-2012/03/31
                    </td>
                </tr>
            </table>
        </div>
        <div class="center">
            <a href="javascript:closeWindow();" class="btn_Close">关闭</a></div>
    </div>
</asp:Content>
