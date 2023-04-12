<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PaymentList.ascx.cs" Inherits="Fee_TeacherPay_Controls_PaymentList" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<!--查找条件-->
<div class="dv_searchbox">
    <table class="GridviewGray " border="0" cellpadding="0" cellspacing="0" runat="server"
        id="tableQueryControlList">
        <tr>
            <th style="width: 70px;">
                培训项目：
            </th>
            <td>
                <asp:TextBox ID="txt_f999ItemName" runat="server" CssClass="inputbox_120 floatleft"
                    MaxLength="50"></asp:TextBox>
            </td>
            <th style="width: 70px;">
                讲师姓名：
            </th>
            <td>
                <asp:TextBox ID="txt_d999RealName" runat="server" CssClass="inputbox_120 floatleft"
                    MaxLength="50"></asp:TextBox>
            </td>
            <th style="width: 70px;">
                课程名称：
            </th>
            <td>
                <asp:TextBox ID="txt_g999CourseName" runat="server" CssClass="inputbox_120 floatleft"
                    MaxLength="50"></asp:TextBox>
                <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>
</div>
<!--查找结果-->
<div class="dv_searchlist">
    <div class="dv_pageInformation">
        <!--翻页-->
        <div class="dv_pagePanel">
            <div class="dv_selectAll">
                <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" />
                <asp:Button ID="LinkButton1" runat="server" CssClass="btn_Addmoney" OnClientClick="CountRealCourseFee();return false;" Text="支付" ValidationGroup="Error"></asp:Button>
                <asp:Button ID="lbtnCancel" runat="server" CssClass="btn_Cancelmoney " OnClick="lbtnCancel_Click" Text="取消支付"></asp:Button>
            </div>
            <div class="dv_pageControl">
                <uc2:PageSet ID="PageSet1" runat="server" />
            </div>
        </div>
        <!--列表 begin-->
        <div id="Div_CourseInfo_<%=PayStatus %>">
            <asp:Repeater ID="rptResult" runat="server">
                <ItemTemplate>
                    <table class="GridviewGray GridviewPay">
                        <tr>
                            <td rowspan="4" style="width:20px">
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                <asp:HiddenField ID="hfdTrainingItemCourseID" Value='<%# Eval("ItemCourseHoursID") %>'
                                    runat="server" />
                            </td>
                            <th width="100" style="text-align:right;">
                                项目名称：
                            </th>
                            <td width="150">
                                <cc1:ShortTextLabel ID="lblTrainingItemCourse" runat="server" ShowTextNum="50" Text='<%# Eval("ItemName")%>'></cc1:ShortTextLabel>
                            </td>
                            <th width="100" style="text-align:right;">
                                课程名称：
                            </th>
                            <td width="100">
                                <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="50" Text='<%# Eval("CourseName")%>'></cc1:ShortTextLabel>
                            </td>
                            <th width="100" style="text-align:right;">
                                培训时段：
                            </th>
                            <td width="110">
                                <%# Eval("TrainingDate").ToDate() + "<br />" + Eval("TrainingBeginTime").ToDateTime().ToShortTimeString() + " - " + Eval("TrainingEndTime").ToDateTime().ToShortTimeString()%>
                            </td>
                        </tr>
                        <tr>
                            <th style="text-align:right;">
                                讲师工号：
                            </th>
                            <td>
                                <%# Eval("WorkerNo")%>
                            </td>
                            <th style="text-align:right;">
                                讲师姓名：
                            </th>
                            <td>
                                <%# Eval("TeacherName")%>
                            </td>
                            <th style="text-align:right;">
                                部&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;门：
                            </th>
                            <td>
                            <cc1:DictionaryLabel ID="lblDepartment" runat="server" DictionaryType="Site_DepartmentByOrgID" FieldIDValue='<%#  Eval("DepartmentID").ToString() %>'></cc1:DictionaryLabel>
                            </td>
                        </tr>
                        <tr>
                            <th style="text-align:right;">
                                培训时间说明：
                            </th>
                            <td>
                                <cc1:DictionaryLabel ID="lblTrainingTimeDesc" DictionaryType="Dic_Sys_TrainingTimeDesc"
                                    TextLength="10" FieldIDValue='<%# Eval("TrainingTimeDescID")%>' runat="server" />
                            </td>
                            <th style="text-align:right;">
                                课酬标准：
                            </th>
                            <td>
                                <%# string.Format("{0:N2}", Eval("CourseFee").ToString().ToDecimal())%>
                            </td>
                            <th style="text-align:right;">
                                计划课时：
                            </th>
                            <td>
                                <%# string.Format("{0:N1}", Eval("CourseHours").ToString().ToDecimal())%>
                            </td>
                        </tr>
                        <tr>
                            <th style="text-align:right;">
                                课时安排状态：
                            </th>
                            <td>
                                <cc1:DictionaryLabel ID="lblCourseHoursStatus" DictionaryType="Dic_Sys_CourseHoursStatus"
                                    TextLength="10" FieldIDValue='<%# Eval("CourseHoursStatusID")%>' runat="server" />
                            </td>
                            <th style="text-align:right;">
                                实际课时：
                            </th>
                            <td>
                             <%if (PayStatus == 0)
                              { %>
                                <asp:TextBox ID="txtRealCourseHours" Text='<%# string.Format("{0:N1}", Eval("RealCourseHours").ToString().ToDecimal()) %>'
                                    MaxLength="10" CssClass="inputbox_90" runat="server"  ></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="请填1位小数的正数"
                                    ValidationGroup="Error" Display="Dynamic" ControlToValidate="txtRealCourseHours"
                                    ValidationExpression="\d{1,6}(\.\d{1})?"></asp:RegularExpressionValidator>
                                    <%}
                              else
                              {%>
                                <font color="#C03219"><asp:Literal ID="Literal2" runat="server" Text='<%# Eval("RealCourseHours").ToString() %>'></asp:Literal></font>
                              <%}%>
                            </td>
                            <th style="text-align:right;">
                                实际费用：
                            </th>
                            <td>
                            <%if (PayStatus == 0)
                              { %>
                                <asp:TextBox ID="txtRealCourseFee" Text='<%# string.Format("{0:N0}", Eval("RealCourseFee").ToString().ToDecimal()) %>'
                                    MaxLength="6" CssClass="inputbox_90" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="请填2位小数的正数"
                                    ValidationGroup="Error" Display="Dynamic" ControlToValidate="txtRealCourseFee"
                                    ValidationExpression="\d{1,6}(\.\d{2})?"></asp:RegularExpressionValidator>
                                    <%}
                              else
                              {%>
                                <font color="#C03219"><asp:Literal ID="Literal1" runat="server" Text='<%# Eval("RealCourseFee").ToString() %>'></asp:Literal></font>
                              <%}%>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
                <SeparatorTemplate>
                    <div class="splitLine">
                    </div>
                </SeparatorTemplate>
            </asp:Repeater><div style="text-align:center"><asp:Literal ID="ltlNull" runat="server"></asp:Literal></div>
        </div>
        <!--列表 end-->
        
        <div class="dv_splitLine">
        </div>
        <!--翻页-->
        <div class="dv_pagePanel">
        </div>
    </div>
</div>
<!--提交表单-->
<div class="dv_submit">
    <asp:Button ID="lbtnSave" runat="server" CssClass="btn_Addmoney" OnClick="lbtnSave_Click"
        Style="display: none;" Text="支付2"></asp:Button>    
    <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Error"
        ShowMessageBox="true" ShowSummary="false" />    
</div>
