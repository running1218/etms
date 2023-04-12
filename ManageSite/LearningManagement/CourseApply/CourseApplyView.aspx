<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="CourseApplyView.aspx.cs" Inherits="LearningManagement_CourseApply_CourseApplyView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--功能标题-->
    <h2 class="dv_title">
        查看课程申请
    </h2>
    <!--表单录入-->
    <div class="dv_information">
        <table class="GridviewGray">
            <tr>
                <th width="80">
                    学员姓名
                </th>
                <td>
                    学员姓名
                </td>
                <th>
                    所属<asp:Literal ID="ltlDepartment" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>
                </th>
                <td>
                    所属<asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                    岗 位
                </th>
                <td>
                    岗 位
                </td>
                <th>
                    <asp:Literal ID="ltlRank" runat="server" Text="<%$ Resources:UIResource, ui_rank%>"></asp:Literal>
                </th>
                <td>
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:UIResource, ui_rank%>"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                    项目名称：
                </th>
                <td>
                    飞鹰01
                </td>
                <th>
                    申请课程：
                </th>
                <td>
                    领导力培训
                </td>
            </tr>
            <tr>
                <th>
                    申请时间
                </th>
                <td>
                    2012-02-28
                </td>
                <th>
                    培训方式
                </th>
                <td>
                    非在线
                </td>
            </tr>
            <tr>
                <th>
                    最大人数限制
                </th>
                <td>
                    30
                </td>
                <th>
                    已申请人数
                </th>
                <td>
                    26
                </td>
            </tr>
            <tr>
                <th>
                    审核通过人数
                </th>
                <td>
                    24
                </td>
                <th>
                    审核状态
                </th>
                <td>
                    已审核
                </td>
            </tr>
            <tr>
                <th>
                    审核人
                </th>
                <td>
                    李老师
                </td>
                <th>
                    审核时间
                </th>
                <td>
                    2012-03-04
                </td>
            </tr>
            <tr>
                <th>
                    审核意见
                </th>
                <td colspan="3">
                    无
                </td>
            </tr>
        </table>
        <!--提交表单-->
        <div class="dv_submit">
            <a href="javascript:closeWindow();" class="btn_Close">关闭</a>
        </div>
    </div>
</asp:Content>
