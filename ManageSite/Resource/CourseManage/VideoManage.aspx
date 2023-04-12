<%@ Page Title="" ValidateRequest="false" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="VideoManage.aspx.cs" Inherits="ETMS.WebApp.Manage.Resource.CourseManage.Content.VideoManage" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/MiniUpFile.ascx" TagName="uploader" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="dv_information">
        <table class="GridviewGray">
            <tr>
                <td colspan="2">支持flv,mp4,mpg,asf,wmv,mkv,mov,3gp,avi,pdf格式的文件上传，文件大小不能超过2GB</td>
            </tr>
            <tr>
                <th>名称</th>
                <td>
                    <asp:TextBox ID="txtVideoName" runat="server" CssClass="inputbox_210" MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="请输入名称！" ControlToValidate="txtVideoName" ValidationGroup="Error"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>选择文件</th>
                <td>
                    <uc:uploader ID="uploader" runat="server" FunctionType="MediaResourceVideo" FileTypeIsDisplay="false" CallBack="doCallBack" />
                    <script type="text/javascript">
                        function doCallBack(videoName, videoUrl, videoSize) {
                            $("#<%=hiddIsEdit.ClientID%>").val(1);
                        }
                    </script>
                    <%--<asp:HiddenField ID="VideoPath" runat="server" />--%>
                    <asp:HiddenField ID="hiddIsEdit" runat="server" />
                </td>
            </tr>
            <tr>
                <th>讲课老师</th>
                <td>
                    <asp:TextBox ID="txtTeacherName" runat="server" CssClass="inputbox_210"  MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>状态</th>
                <td>
                    <cc1:DictionaryRadioButtonList ID="radStatus" runat="server" CssClass="noborder"
                        RepeatDirection="Horizontal" DictionaryType="Dic_Status" CheckedValue="1">
                    </cc1:DictionaryRadioButtonList>
                </td>

            </tr>
            <tr class="hide">
                <th>是否开放</th>
                <td>
                    <cc1:DictionaryRadioButtonList ID="radIsOpen" runat="server" CssClass="noborder"
                        RepeatDirection="Horizontal" DictionaryType="Dic_TrueOrFalse" CheckedValue="0">
                    </cc1:DictionaryRadioButtonList>
                </td>

            </tr>
        </table>
    </div>
    <!--提交表单-->
    <div class="dv_submit">
        <asp:HiddenField ID="hiddSort" runat="server" />
        <asp:LinkButton ID="lbtnSave" runat="server" CssClass="btn_Save" OnClick="lbtnSave_Click" OnClientClick="return validate();"
            ValidationGroup="Error">保存</asp:LinkButton>
        <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Error"
            ShowMessageBox="false" ShowSummary="false" />
        <a href="javascript:closeWindow();" class="btn_Cancel padleft10">取消</a>
    </div>

    <script language="javascript" type="text/javascript">
        function validate()
        {
            var action = '<%=Action == ETMS.AppContext.OperationAction.Add%>';

            if (action == 'True')
            {
                if ($('#percent_b span').html() == null) {
                    popFailedMsg('请上传文件！');
                    return false;
                }
                else {
                    if ($('#percent_b span').html() != '100%')
                    {
                        popFailedMsg('请等待上传完毕后，再操作！');
                        return false;
                    }
                }
            }
            return true;
        }
    </script>
</asp:Content>

