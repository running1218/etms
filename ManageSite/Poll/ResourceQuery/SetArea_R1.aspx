<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="SetArea_R1.aspx.cs" Inherits="Poll_ResourceQuery_SetArea_R1" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input type="hidden" id="hidAwserUserCount" runat="server" />
    <script type="text/javascript">
        $(function () {
            if ($("#<%=hidAwserUserCount.ClientID %>").val() > 0) {
                $("#<%=ddlProject.ClientID %>__jQSelect0").attr("disabled", "disabled");
                $("#<%=ddlCourse.ClientID %>__jQSelect1").attr("disabled", "disabled");
                $("#<%=ddlTeacher.ClientID %>__jQSelect2").attr("disabled", "disabled");
                $("#<%=LinkButton1.ClientID %>").attr("disabled", "disabled");
            }
        })
    </script>
    <div class="dv_information">
        <table class="GridviewGray">
            <tr>
                <th>
                    培训项目：
                </th>
                <td>
                    <asp:DropDownList runat="server" ID="ddlProject" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged"
                        AutoPostBack="true" CssClass="select_390">
                    </asp:DropDownList>
                    <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidatorLoginName"
                        Text="&nbsp;" Display="None" runat="server" ErrorMessage="请选择培训项目！" ControlToValidate="ddlProject"
                        ValidationGroup="Edit"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>
                    课程：
                </th>
                <td>
                    <asp:DropDownList runat="server" ID="ddlCourse" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged"
                        AutoPostBack="true" CssClass="select_390">
                        <asp:ListItem Text="请先选择培训项目！" Value="-1"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>
                    讲师：
                </th>
                <td>
                    <asp:DropDownList runat="server" ID="ddlTeacher" CssClass="select_390">
                        <asp:ListItem Text="请先选择课程！" Value="-1"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <asp:ValidationSummary runat="server" ID="validationSummary1" ValidationGroup="Edit"
            ShowMessageBox="true" ShowSummary="false" />
    </div>
    <!--提交表单-->
    <div class="dv_submit">
        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn_Save" ValidationGroup="Edit"
            OnClick="LinkButton1_Click">保存</asp:LinkButton>
        <a href="javascript:closeWindow();" class="btn_Cancel padleft10">取消</a>
    </div>
</asp:Content>
