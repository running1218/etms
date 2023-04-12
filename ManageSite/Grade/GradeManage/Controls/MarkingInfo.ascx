<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MarkingInfo.ascx.cs" Inherits="Grade_GradeManage_Controls_MarkingInfo" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/MiniUpFile.ascx" TagName="uploader" TagPrefix="uc" %>
<%@ Register Assembly="ETMS.Editor" Namespace="ETMS.Editor.UEditor" TagPrefix="wuc" %>
<cc1:CustomMuliView runat="server" ID="Views">
    <asp:View runat="server" ID="View_Edit">
        <div class="dv_information">
            <table class="GridviewGray fixedTable ">
                <tr>
                    <th>项目名称：
                    </th>
                    <td>
                        <asp:Label ID="lblItemIDEdit" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th>实践名称：
                    </th>
                    <td>
                        <asp:Label ID="lblJobIDEdit" runat="server" />
                    </td>
                    <th>起止时间：
                    </th>
                    <td>
                        <asp:Label ID="lblTimeEdit" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th>学生姓名：
                    </th>
                    <td>
                        <asp:Label ID="lblRealNameEdit" runat="server" />
                    </td>
                    <th>帐&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;号：
                    </th>
                    <td>
                        <asp:Label ID="lblWorkerNoEdit" runat="server" />
                    </td>
                </tr>
                <tr id="trOrgnization" runat="server">
                    <th>组织机构：</th>
                    <td colspan="3">
                        <cc1:DictionaryLabel ID="lblOrgnization" runat="server" DictionaryType='vw_Dic_Sys_Organization' TextLength="10"></cc1:DictionaryLabel>
                    </td>
                </tr>
                <tr style="display: none;">
                    <th>
                        <asp:Literal ID="ltlDepartment" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>：
                    </th>
                    <td>
                        <cc1:DictionaryLabel ID="lblDepartmentIDEdit" DictionaryType="vw_Dic_Sys_Department" TextLength="10" runat="server" FieldIDValue='<%#userInfo.DepartmentID %>' />
                    </td>
                    <th>
                        <asp:Literal ID="ltlPosition" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>：
                    </th>
                    <td>
                        <cc1:DictionaryLabel ID="lblPostIDEdit" DictionaryType="vw_Dic_Sys_Post" TextLength="10" runat="server" FieldIDValue='<%#student.PostID%>' />
                        <%--<asp:Label ID="lblPostIDEdit" runat="server" />--%>
                    </td>
                </tr>
                <tr>
                    <th>电&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;话：
                    </th>
                    <td>
                        <asp:Label ID="lblTelEdit" runat="server" />
                    </td>
                    <th>邮&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;箱：
                    </th>
                    <td>
                        <asp:Label ID="lblMailEdit" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th>实践内容：
                    </th>
                    <td colspan="3">
                        <asp:Label ID="lblJobContentEdit" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th>实践：
                    </th>
                    <td colspan="3">
                        <asp:Label ID="lblUploadEdit" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th>提交时间：
                    </th>
                    <td colspan="3">
                        <asp:Label ID="lblUpLoadTimeEdit" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th>分数：
                    </th>
                    <td colspan="3">
                        <%--<asp:TextBox runat="server"  ID="txtScoreEdit" MaxLength="4" style="ime-mode:disabled" onkeypress="if ((event.keyCode<48 || event.keyCode>57) && event.keyCode!=46) event.returnValue=false;"></asp:TextBox>--%>
                        <asp:TextBox runat="server" ID="txtScoreEdit" MaxLength="3" onkeypress='return /^\d$/.test(String.fromCharCode(event.keyCode||event.keycode||event.which))'
                            oninput='this.value = this.value.replace(/\D+/g, "")'
                            onpropertychange='if(!/\D+/.test(this.value)){return;};this.value=this.value.replace(/\D+/g, "")'
                            onblur='this.value = this.value.replace(/\D+/g, "")'></asp:TextBox>                     
                    </td>
                </tr>
                <tr>
                    <th>批阅文件：
                    </th>
                    <td colspan="3">
                        <asp:Label ID="lblFilePath" runat="server"></asp:Label><br />
                        <uc:uploader ID="uploader" runat="server" FunctionType="OfflineJob" CallBack="doCallBack" />
                        <script language="javascript">
                            function doCallBack(imgName, imgUrl, imgSize) {
                                $('#<%=lblFilePath.ClientID%>').html("<a href='" + imgUrl + "' target='_blank'>" + imgName + "</a>");
                            }
                        </script>
                    </td>
                </tr>
                <tr>
                    <th>评&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;语：
                    </th>
                    <td colspan="3">
                        <wuc:UEditor ID="EvaluationDescription" runat="server" Width="480" Height="100" CssClass="inputbox_area349" ToolType="Basic" AutoHeightEnabled="false"></wuc:UEditor>
                        <%-- <asp:TextBox id="lblEvaluationEdit" runat="server" TextMode="MultiLine" CssClass="inputbox_area349" />--%>
                        <%--<textarea id="lblEvaluationEdit" runat="server" rows="5" cols="8" class="inputbox_210" />--%>
                    </td>
                </tr>
            </table>
        </div>
    </asp:View>
    <asp:View runat="server" ID="View_Browse">
        <div class="dv_information">
            <table class="GridviewGray fixedTable ">
                <tr>
                    <th>项目名称：
                    </th>
                    <td>
                        <asp:Label ID="lblItemID" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th>实践名称：
                    </th>
                    <td>
                        <asp:Label ID="lblJobID" runat="server" />
                    </td>
                    <th>起止时间：
                    </th>
                    <td>
                        <asp:Label ID="lblTime" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th>学生姓名：
                    </th>
                    <td>
                        <asp:Label ID="lblRealName" runat="server" />
                    </td>
                    <th>帐&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;号：
                    </th>
                    <td>
                        <asp:Label ID="lblWorkerNo" runat="server" />
                    </td>
                </tr>
                <tr id="trOrg" runat="server">
                    <th>组织机构：</th>
                    <td colspan="3">
                        <cc1:DictionaryLabel ID="lblOrg" runat="server" DictionaryType='vw_Dic_Sys_Organization' TextLength="10"></cc1:DictionaryLabel>
                    </td>
                </tr>
                <tr style="display: none;">
                    <th>
                        <asp:Literal ID="ltlDepartment1" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>：
                    </th>
                    <td>
                        <cc1:DictionaryLabel ID="lblDept" DictionaryType="Site_DepartmentByOrgID" TextLength="10" runat="server" />
                    </td>
                    <th>
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>：
                    </th>
                    <td>
                        <cc1:DictionaryLabel ID="lblPost" DictionaryType="Dic_PostByOrgID" TextLength="10" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th>电&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;话：
                    </th>
                    <td>
                        <asp:Label ID="lblTel" runat="server" />
                    </td>
                    <th>邮&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;箱：
                    </th>
                    <td>
                        <asp:Label ID="lblMail" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th>实践内容：
                    </th>
                    <td colspan="3">
                        <asp:Label ID="lblJobContent" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th>实践：
                    </th>
                    <td colspan="3">
                        <asp:Label ID="lblUpload" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th>提交时间：
                    </th>
                    <td colspan="3">
                        <asp:Label ID="lblUpLoadTime" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th>分数：
                    </th>
                    <td colspan="3">
                        <asp:Label ID="lbScore" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th>批阅文件：
                    </th>
                    <td colspan="3">
                        <asp:Label ID="lblMarkFileName" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th>评&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;语：
                    </th>
                    <td colspan="3">
                        <asp:Literal ID="lblEvaluation" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:View>
</cc1:CustomMuliView>