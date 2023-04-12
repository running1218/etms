<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChargeListConfig.aspx.cs"
    MasterPageFile="~/MasterPages/MPageAdmin.Master" Inherits="TraningImplement_TraningProjectManager_ChargeListConfig" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function showpointdeloption(methodVal) {
            if (methodVal == "1" || methodVal == "3") {
                $(this).parent().eq()
            }
            else {

            }
        }
    </script>
    <div>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
                id="tableQueryControlList">
                <tr>
                    <th width="120">
                        项目编码：
                    </th>
                    <td width="130">
                        <asp:TextBox ID="txt_ItemCode" runat="server" CssClass="inputbox_120" MaxLength="50"></asp:TextBox>
                    </td>
                    <th width="100">
                        项目名称：
                    </th>
                    <td class="Search_Area">
                        <asp:TextBox ID="txt_ItemName" runat="server" CssClass="inputbox_120 floatleft" MaxLength="100"></asp:TextBox>
                        <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
                        <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        报名方式：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="ddl_SignupModeID" DictionaryType="Dic_SignupMode"
                            IsShowAll="true" />
                    </td>
                    <th width="100">
                        项目状态：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="ddl_ItemStatus" DictionaryType="Dic_TraningProjectType"
                            IsShowAll="true" />
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        发布状态：
                    </th>
                    <td colspan="3">
                        <cc1:DictionaryDropDownList runat="server" ID="ddl_IsIssue" DictionaryType="Dic_TraningProjectReleaseState"
                            IsShowAll="true" />
                    </td>
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel" id="divPage1">
                <div class="dv_selectAll">
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
                CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0"
                DataKeyNames="TrainingItemID" OnRowDataBound="CustomGridView1_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="项目编码" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft ">
                        <ItemTemplate>
                            <%# Eval("ItemCode")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="项目名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <%# Eval("ItemName")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="开始时间" HeaderStyle-Width="70">
                        <ItemTemplate>
                            <%# Eval("ItemBeginTime").ToDate()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="结束时间" HeaderStyle-Width="70">
                        <ItemTemplate>
                            <%# Eval("ItemEndTime").ToDate() %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="项目状态" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="DictionaryLabel1" DictionaryType="Dic_TraningProjectType"
                                FieldIDValue='<%# Eval("ItemStatus") %>' runat="server" />
                            <asp:HiddenField ID="Hf_ItemStatus" runat="server" Value='<%# Eval("ItemStatus") %>' />
                            <asp:HiddenField ID="hf_IsIssue" runat="server" Value='<%# Eval("IsIssue") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="报名方式" HeaderStyle-Width="100">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="dicEnrollmentMethod" DictionaryType="Dic_SignupMode" FieldIDValue='<%# Eval("SignupModeID") %>'
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="付费方" HeaderStyle-Width="110">
                        <ItemTemplate>
                            <%# Eval("PayFrom").ToString() == "1" ? "集体" : (Eval("PayFrom").ToString()=="2" ? "个人":"未设置")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="收费标准（元/人）" HeaderStyle-Width="110">
                        <ItemTemplate>
                            <cc1:CustomTextBox ID="ctbPayMoney" runat="server" Text='<%# string.Format("{0:N2}",Eval("BudgetFee").ToString().ToDecimal())  %>'
                                Visible="false" CssClass="inputbox_60" ContentType="Decimal" MaxLength="6"></cc1:CustomTextBox>
                            <asp:Literal ID="LitPayMoney" runat="server" Text='<%# Eval("BudgetFee") %>'></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="发布状态" HeaderStyle-Width="100">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="DictionaryReleaseState" DictionaryType="Dic_TraningProjectReleaseStateBool"
                                FieldIDValue='<%# Eval("IsIssue") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作" HeaderStyle-Width="145">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:LinkButton ID="Lbtn_Edit" runat="server" Enabled="false" Text="设置" />
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
