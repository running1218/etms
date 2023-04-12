<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="GradeEntryImport.aspx.cs" Inherits="Grade_GradeManage_GradeEntryImport" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <div class="dv_information">
            <table class="GridviewGray">
                <tr>
                    <th>
                        项目编码：
                    </th>
                    <td>
                        <cc1:ShortTextLabel ID="lblItemCode" runat="server" ShowTextNum="100" />
                        <asp:Label ID="lblItemCodeHide" runat="server" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <th>
                        项目名称：
                    </th>
                    <td>
                        <cc1:ShortTextLabel ID="lblItemName" runat="server" ShowTextNum="100" />
                        <asp:Label ID="lblItemNameHide" runat="server" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <th>
                        课程名称：
                    </th>
                    <td>
                        <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="100" />
                        <asp:Label ID="lblCourseNameHide" runat="server" Visible="false" />
                        <asp:Label ID="lblCourseCodeHide" runat="server" Visible="false" />                        
                    </td>
                </tr>
                <tr>
                    <th>
                        学员人数：
                    </th>
                    <td>
                        <asp:Label ID="lblStudentNum" runat="server" />
                        ，<asp:LinkButton ID="lbnExportStudentList" runat="server" Text="点击下载导入学员成绩模板，并填写成绩后进行上传。" OnClick="lbnExportStudentList_Click" />
                        <%--<a href="abc.xlsx">点击下载导入学员成绩模板，并填写成绩后进行上传。</a>--%>
                    </td>
                </tr>
                <tr>
                    <th>
                        上传成绩：
                    </th>
                    <td>
                        <asp:Label ID="lbltemp" runat="server" Text="支持Excel文件" /><br />
                        <asp:Label ID="lblFilePath" runat="server"></asp:Label>
                        <cc1:FileUpload runat="server" ID="fileUpload1" FunctionType="ImportStudentInfo"
                            CallBack="callback" />
                        <script language="javascript">
                            function callback(imgName, imgUrl) {
                                $('#<%=lblFilePath.ClientID%>').html("<a href='" + imgUrl + "' target='_blank'>" + imgName + "</a>");
                            }
                        </script>
                    </td>
                </tr>
                <tr>
                    <th>
                        说&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;明：
                    </th>
                    <td>                        
                        <asp:Label ID="lblDescription" runat="server"  />
                    </td>
                </tr>
            </table>
        </div>
        <!--提交表单-->
        <div class="dv_submit">
            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn_Save" OnClick="LinkButton1_Click">保存</asp:LinkButton>
            <a href="javascript:closeWindow();" class="btn_Cancel padleft10">取消</a>
        </div>
    </div>
</asp:Content>
