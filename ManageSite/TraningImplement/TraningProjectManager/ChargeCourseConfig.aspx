<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MPagePop.Master"
    CodeFile="ChargeCourseConfig.aspx.cs" Inherits="TraningImplement_TraningProjectManager_ChargeCourseConfig" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(function () {
            $(".GridviewGray tr").show();
        })
        //手工出题，批量设置分数
        function setScoreBatch() {
            var scoreBatch = parseFloat($("#<%=txtScoreBatch.ClientID %>").val());
            if (isNaN(scoreBatch)) {
                alert("请填写合法的收费标准！");
                return;
            }
            var obj = $('#<%=CustomGridView1.ClientID %>' + " tbody tr");
            if (obj.find("td input[type='checkbox']:checked").length == 0) {
                alert("请选择设置的课程！");
                return;
            }
            obj.each(function (i) {
                if (i > 0) {
                    if ($(this).find("td input[type='checkbox']").attr("checked") == "checked") {
                        $(this).find("td input[type='text']").val(scoreBatch);
                    }
                }
            });
        }
    </script>
    <div class="dv_information">
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <th>
                        项目编码：
                    </th>
                    <td>
                        <asp:Label ID="Lbl_ItemCode" runat="server" Text=""></asp:Label>
                    </td>
                    <th>
                        项目名称：
                    </th>
                    <td>
                        <asp:Label ID="Lbl_ItemName" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        按选择批量设置：
                    </th>
                    <td colspan="3">
                        <cc1:CustomTextBox ID="txtScoreBatch" runat="server" Text="0" CssClass="inputbox_60"
                            ContentType="Decimal" MaxLength="6"></cc1:CustomTextBox>
                        <input type="button" onclick="setScoreBatch()" value="设置" class="btn_Configer" />
                    </td>
                </tr>
                <tr>
                    <th>
                        付费方：
                    </th>
                    <td colspan="3">
                        <asp:RadioButtonList runat="server" ID="Dic_PayMode" RepeatLayout="Flow" RepeatDirection="Horizontal">
                            <asp:ListItem Text="集体" Value="1" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="个人" Value="2"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel" id="divPage1">
                <div class="dv_selectAll">
                    <cc1:CheckBoxsController runat="server" ID="CheckBox1" ContainerID="CustomGridView1" />
                    <asp:Button ID="btnAdd" runat="server" CssClass="btn_Save" Text="保存" OnClick="btnAdd_Click" />
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
                CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0"
                DataKeyNames="TrainingItemCourseID">
                <Columns>
                    <asp:TemplateField HeaderText="">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" Width="20" />
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="CourseCode" HeaderText="课程编码" HeaderStyle-CssClass="field12 alignleft"
                        ItemStyle-CssClass="alignleft" />
                    <asp:TemplateField HeaderText="课程名称" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="8" Text='<%# Eval("CourseName")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="状态" HeaderStyle-Width="40">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblCourseStatus" DictionaryType="Dic_Status" FieldIDValue='<%# Eval("CourseStatus") %>'
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="CourseHours" HeaderText="课程学时" HeaderStyle-Width="60" />
                    <asp:TemplateField HeaderText="收费标准（元/人）" HeaderStyle-Width="110">
                        <ItemTemplate>
                            <cc1:CustomTextBox ID="ctbPayMoney" runat="server" Text='<%# string.Format("{0:N2}",Eval("BudgetFee").ToString().ToDecimal())  %>'
                                CssClass="inputbox_60" ContentType="Decimal" MaxLength="50"></cc1:CustomTextBox>
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
