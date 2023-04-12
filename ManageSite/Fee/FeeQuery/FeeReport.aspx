<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="FeeReport.aspx.cs" Inherits="FeeReport" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
                id="tableQueryControlList">
                <tr>
                    <th width="120">
                        项目编码：
                    </th>
                    <td width="125">
                        <asp:TextBox ID="txt_f999ItemCode" runat="server" CssClass="inputbox_120 floatleft"
                            MaxLength="50"></asp:TextBox>
                    </td>
                    <th width="120">
                        项目名称：
                    </th>
                    <td>
                        <asp:TextBox ID="txt_f999ItemName" runat="server" CssClass="inputbox_120 floatleft"
                            MaxLength="50"></asp:TextBox>
                        <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click"
                            ValidationGroup="Saves" />
                        <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
                       
                    </td>
                </tr>
                <tr>
                    <th>
                        课程名称：
                    </th>
                    <td>
                        <asp:TextBox ID="txt_g999CourseName" runat="server" CssClass="inputbox_120 floatleft"
                            MaxLength="50"></asp:TextBox>
                    </td>
                    <th>
                        讲师姓名：
                    </th>
                    <td>
                        <asp:TextBox ID="txt_d999RealName" runat="server" CssClass="inputbox_120 floatleft"
                            MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        支付时间：
                    </th>
                    <td colspan="3">
                        <cc1:DateTimeTextBox ID="begin_a999ModifyTime" runat="server" EndTimeControlID="end_a999ModifyTime"></cc1:DateTimeTextBox><asp:RequiredFieldValidator
                            ID="RequiredFieldValidator3" Text="*" Display="None" runat="server" ErrorMessage="请填写开始时间！"
                            ControlToValidate="begin_a999ModifyTime" ValidationGroup="Saves"></asp:RequiredFieldValidator>
                        至
                        <cc1:DateTimeTextBox ID="end_a999ModifyTime" runat="server" BeginTimeControlID="begin_a999ModifyTime"></cc1:DateTimeTextBox><asp:RequiredFieldValidator
                            ID="RequiredFieldValidator1" Text="*" Display="None" runat="server" ErrorMessage="请填写结束时间！"
                            ControlToValidate="end_a999ModifyTime" ValidationGroup="Saves"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel" id="divPage1">
                <div class="dv_selectAll">
                    <asp:Button ID="btnExport" runat="server" CssClass="btn_Export" Text="导出" OnClick="btnExport_Click" />
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" CssClass="GridviewGray" EmptyDataSourceTip="没有任何记录！"
                AutoCreateColumnInsertIndex="0" AutoGenerateColumns="False" CurrentPageIndex="1"
                CustomAllowPaging="False" IsEmpty="False" TotalRecordCount="0" DataKeyNames="ItemCourseHoursID">
                <Columns>
                    <%--<asp:BoundField DataField="ItemCode" HeaderText="项目编码" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft" />--%>
                    <asp:TemplateField HeaderText="项目名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblItemName" runat="server" ShowTextNum="50" Text='<%# Eval("ItemName")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                   <asp:TemplateField HeaderText="课程名称" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="50" Text='<%# Eval("CourseName")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="TeacherName" HeaderText="讲师姓名" HeaderStyle-Width="60" />
                    <asp:TemplateField HeaderText="培训日期" HeaderStyle-Width="70">
                        <ItemTemplate>
                            <%# Eval("TrainingDate").ToDate()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="培训时间说明" HeaderStyle-Width="80">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblTrainingTimeDesc" DictionaryType="Dic_Sys_TrainingTimeDesc"
                                TextLength="10" FieldIDValue='<%# Eval("TrainingTimeDescID")%>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                    <asp:TemplateField HeaderText="课酬标准" HeaderStyle-Width="60" ItemStyle-CssClass="alignright" HeaderStyle-CssClass="alignright">
                        <ItemTemplate>
                            <%# string.Format("{0:N2}", Eval("CourseFee").ToString().ToDecimal())%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="实际课时" HeaderStyle-Width="60" ItemStyle-CssClass="alignright" HeaderStyle-CssClass="alignright">
                        <ItemTemplate>
                            <%# string.Format("{0:N1}", Eval("RealCourseHours").ToString().ToDecimal())%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="实际费用" HeaderStyle-Width="60" ItemStyle-CssClass="alignright" HeaderStyle-CssClass="alignright">
                        <ItemTemplate>
                            <%# string.Format("{0:N2}", Eval("RealCourseFee").ToString().ToDecimal())%>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="支付时间" HeaderStyle-Width="70">
                        <ItemTemplate>
                            <%# Eval("ModifyTime").ToDate()%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="操作" HeaderStyle-Width="50">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <a href='<%# getUrl(Eval("ItemCourseHoursID").ToString()) %>'>查看</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </cc1:CustomGridView>
            <!--列表 end-->
            <div class="dv_splitLine">
            </div>
            <!--翻页-->
            <div class="dv_pagePanel" id="divPage2">
            </div>
        </div>
    </div>
</asp:Content>
