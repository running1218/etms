<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    CodeFile="OnlineTestList.aspx.cs" Inherits="Grade_GradeManage_OnLineTestList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <!--导航路径-->
        <div class="dv_path" id="dv_path">
            当前位置：测评系统&gt;&gt;考试与成绩&gt;&gt;<asp:Literal ID="Literal7" runat="server" Text="在线测试异常监控"></asp:Literal>
        </div>
        <!--功能标题-->
        <h2 class="dv_title">
            在线测试异常监控
        </h2>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" id="tableQueryControlList"
                runat="server">
                <tr>
                    <th style="width: 100">
                        项目名称：
                    </th>
                    <td>
                        <asp:TextBox ID="txt_i999ItemName" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                    <th>
                        课程名称：
                    </th>
                    <td style="width: 300">
                        <asp:TextBox ID="txt_c999CourseName" runat="server" CssClass="inputbox_120"></asp:TextBox>
                        <asp:Button ID="Button1" runat="server" CssClass="btn_Search" Text="查询" OnClick="btnSearch_Click" />
                        <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
                    </td>
                </tr>
                <tr>
                    <th>
                        学员姓名：
                    </th>
                    <td>
                        <asp:TextBox ID="txt_u999RealName" runat="server" CssClass="inputbox_120" />
                    </td>
                    <th>
                        学员账号：
                    </th>
                    <td>
                        <asp:TextBox runat="server" ID="txt_u999LoginName" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        在线测试名称：
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txt_ot999OnLineTestName" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th rowspan="2">
                        测试日期：
                    </th>
                    <td colspan="3">
                        <table>
                            <tr>
                                <td style="width: 400px;">
                                    <asp:RadioButtonList runat="server" ID="rdoTestDateList" AutoPostBack="True" RepeatDirection="Horizontal"
                                        onclick="OnSelectedIndexChanged()" Width="400px">
                                        <asp:ListItem Value="OneWeek">最近一周</asp:ListItem>
                                        <asp:ListItem Value="TwoWeek">最近两周</asp:ListItem>
                                        <asp:ListItem Value="OneMonth">最近一个月</asp:ListItem>
                                        <asp:ListItem Value="ThreeMonth">最近三个月</asp:ListItem>
                                        <asp:ListItem Value="All" Selected="True">全部</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <cc1:DateTimeTextBox runat="server" ID="begin_st999BeginTime"></cc1:DateTimeTextBox>至<cc1:DateTimeTextBox
                            runat="server" ID="end_st999BeginTime"></cc1:DateTimeTextBox>
                    </td>
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel">
                <div class="dv_selectAll">
                    <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" />
                    <cc1:CustomButton CssClass="btn_Del" Text="删除" runat="server" ID="btnDelete" EnableConfirm="true"
                        ConfirmMessage="确定要删除吗?" OnClick="btnDelete_Click" />
                    <cc1:CustomButton CssClass="btn_DelAll" Text="删除全部结果" runat="server" ID="btnDelAll"
                        EnableConfirm="true" ConfirmMessage="确定要删除全部吗?" OnClick="btnDelAll_Click" />
                    <asp:Button runat="server" ID="btn_Export" CssClass="btn_ExportAll" Text="导出全部结果"
                        OnClick="btn_Export_Click" />
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" CssClass="GridviewGray" EmptyDataSourceTip="没有任何记录！"
                AutoCreateColumnInsertIndex="0" AutoGenerateColumns="False" CurrentPageIndex="1"
                CustomAllowPaging="False" IsEmpty="False" DataKeyNames="StudentOnlineTestID">
                <Columns>
                    <asp:TemplateField HeaderText="" HeaderStyle-Width="18">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="False" HeaderText="学员ID">
                        <ItemTemplate>
                            <cc1:ShortTextLabel runat="server" ID="StudentOnlineTestID" Text='<%#Eval("StudentOnlineTestID")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="项目名称" ItemStyle-CssClass="alignCenter" HeaderStyle-CssClass="alignleft">
                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblItemName" runat="server" ShowTextNum="10" Text='<%# Eval("ItemName")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程名称" HeaderStyle-CssClass="alignCenter">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="10" Text='<%# Eval("CourseName")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="在线测试名称" HeaderStyle-CssClass="alignCenter">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblOnLineTestName" runat="server" Text='<%#Eval("OnLineTestName") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="测试日期" HeaderStyle-CssClass="alignCenter" HeaderStyle-Width="140">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <cc1:DateTimeLabel ID="dtlTestDate" runat="server" Text='<%#Eval("BeginExamTime")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="测试成绩" HeaderStyle-CssClass="alignCenter">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblTestGrade" runat="server" Text='<%#Eval("ExamScore") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="学员姓名" HeaderStyle-CssClass="alignCenter">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblStudentName" runat="server" Text='<%#Eval("RealName") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="学员账号" HeaderStyle-CssClass="alignCenter">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblStudentAccount" runat="server" Text='<%#Eval("LoginName") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="手机" HeaderStyle-CssClass="alignCenter">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblMobilePhone" runat="server" Text='<%#Eval("MobilePhone") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="邮箱" HeaderStyle-CssClass="alignCenter">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblEmail" runat="server" Text='<%#Eval("Email") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </cc1:CustomGridView>
            <!--列表 end-->
            <!--隐藏列表 begin-->
            <cc1:CustomGridView ID="HideCustomGridView" runat="server" AutoGenerateColumns="false"
                    CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="1"
                DataKeyNames="StudentOnlineTestID">
                <Columns>
                    <asp:TemplateField Visible="False" HeaderText="学员ID">
                        <ItemTemplate>
                            <cc1:ShortTextLabel runat="server" ID="StudentOnlineTestID" Text='<%#Eval("StudentOnlineTestID")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="项目名称" ItemStyle-CssClass="alignCenter" HeaderStyle-CssClass="alignleft">
                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblItemName" runat="server" ShowTextNum="10" Text='<%# Eval("ItemName")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程名称" HeaderStyle-CssClass="alignCenter">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="10" Text='<%# Eval("CourseName")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="在线测试名称" HeaderStyle-CssClass="alignCenter">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblOnLineTestName" runat="server" Text='<%#Eval("OnLineTestName") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="测试日期" HeaderStyle-CssClass="alignCenter" HeaderStyle-Width="140">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <cc1:DateTimeLabel ID="dtlTestDate" runat="server" Text='<%#Eval("BeginExamTime")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="测试成绩" HeaderStyle-CssClass="alignCenter">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblTestGrade" runat="server" Text='<%#Eval("ExamScore") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="学员姓名" HeaderStyle-CssClass="alignCenter">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblStudentName" runat="server" Text='<%#Eval("RealName") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="学员账号" HeaderStyle-CssClass="alignCenter">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblStudentAccount" runat="server" Text='<%#Eval("LoginName") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="手机" HeaderStyle-CssClass="alignCenter">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblMobilePhone" runat="server" Text='<%#Eval("MobilePhone") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="邮箱" HeaderStyle-CssClass="alignCenter">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblEmail" runat="server" Text='<%#Eval("Email") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </cc1:CustomGridView>
            <!--列表 end-->
            <div class="dv_splitLine">
            </div>
            <!--翻页-->
            <div class="dv_pagePanel">
            </div>
        </div>
        <script type="text/javascript">
            function OnSelectedIndexChanged() {
                var rdoList = document.getElementById('<%=rdoTestDateList.ClientID %>');
                var rodAll = rdoList.getElementsByTagName("input");
                for (var i = 0; i < rodAll.length; i++) {
                    if (rodAll[i].checked) {

                        var selectValue = rodAll[i].value;
                        switch (selectValue) {
                            case "OneWeek":
                                document.getElementById('<%=begin_st999BeginTime.ClientID %>').value = '<%=DateTime.Now.AddDays(-6).ToDate() %>';
                                document.getElementById('<%=end_st999BeginTime.ClientID %>').value = '<%=DateTime.Now.ToDate() %>';
                                document.getElementById('<%=begin_st999BeginTime.ClientID %>').disabled = true;
                                document.getElementById('<%=end_st999BeginTime.ClientID %>').disabled = true;
                                break;
                            case "TwoWeek":
                                document.getElementById('<%=begin_st999BeginTime.ClientID %>').value = '<%=DateTime.Now.AddDays(-13).ToDate() %>';
                                document.getElementById('<%=end_st999BeginTime.ClientID %>').value = '<%=DateTime.Now.ToDate() %>';
                                document.getElementById('<%=begin_st999BeginTime.ClientID %>').disabled = true;
                                document.getElementById('<%=end_st999BeginTime.ClientID %>').disabled = true;
                                break;
                            case "OneMonth":
                                document.getElementById('<%=begin_st999BeginTime.ClientID %>').value = '<%=DateTime.Now.AddMonths(-1).AddDays(+1).ToDate() %>';
                                document.getElementById('<%=end_st999BeginTime.ClientID %>').value = '<%=DateTime.Now.ToDate() %>';
                                document.getElementById('<%=begin_st999BeginTime.ClientID %>').disabled = true;
                                document.getElementById('<%=end_st999BeginTime.ClientID %>').disabled = true;
                                break;
                            case "ThreeMonth":
                                document.getElementById('<%=begin_st999BeginTime.ClientID %>').value = '<%=DateTime.Now.AddMonths(-3).AddDays(+1).ToDate() %>';
                                document.getElementById('<%=end_st999BeginTime.ClientID %>').value = '<%=DateTime.Now.ToDate() %>';
                                document.getElementById('<%=begin_st999BeginTime.ClientID %>').disabled = true;
                                document.getElementById('<%=end_st999BeginTime.ClientID %>').disabled = true;
                                break;
                            default:
                                document.getElementById('<%=begin_st999BeginTime.ClientID %>').disabled = false;
                                document.getElementById('<%=end_st999BeginTime.ClientID %>').disabled = false;
                                document.getElementById('<%=begin_st999BeginTime.ClientID %>').value = "";
                                document.getElementById('<%=end_st999BeginTime.ClientID %>').value = "";
                                break;
                        }
                    }
                }
            }
        </script>
    </div>
</asp:Content>
