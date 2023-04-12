<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="SelectUser.aspx.cs" Inherits="TraningImplement_TraningProjectManager_Selected_SelectUser" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--查找条件-->
    <div class="dv_searchbox">
        <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
            id="tableQueryControlList">
            <tr>
                <th style="width: 60px">
                    姓名：
                </th>
                <td class="Search_Area">
                    <asp:TextBox ID="txt_u999RealName" runat="server" CssClass="inputbox_120 floatleft"
                        MaxLength="100"></asp:TextBox>
                    <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div class="dv_information">
        <!--查找结果-->
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel" id="divPage1" style="display: none">
                <div class="dv_selectAll">
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
                CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0"
                DataKeyNames="UserID">
                <Columns>
                    <asp:TemplateField HeaderText="">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" Width="20" />
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Text='<%# Eval("RealName")%>' Visible="false" />
                            <asp:RadioButton ID="Radio1" runat="server" Text='<%# string.Format("{0}${1}${2}", Eval("RealName"), Eval("Telphone"), Eval("Email")) %>' ValidationGroup="Radio" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="姓名" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblRealName" runat="server" ShowTextNum="6" Text='<%# Eval("RealName")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:DictionaryLabel runat="server" ID="lblDepartment" DictionaryType="vw_Dic_Sys_Department"
                                TextLength="10" FieldIDValue='<%#Eval("DepartmentID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="手机" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <asp:Label ID="lblPhone" runat="server" Text='<%# Eval("Telphone") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="邮箱" HeaderStyle-Width="160">
                        <ItemTemplate>
                            <asp:Label ID="lblMail" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
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
    <div class="dv_submit">
        <input id="btnSave" type="button" class="btn_Save" value="保存" />
        <input type="button" class="btn_Cancel" value="取消" onclick="javascript:closeWindow();" /></div>
    <script type="text/javascript">
        $(document).ready(function () {
            //            $("#btnSave").click(function () {
            //                var userIDs = "";
            //                var arrRe = $("input[type='checkbox']:checked");
            //                $(arrRe).each(function () {
            //                    if ($("label[for^='" + this.id + "']").html() != "全选" && $("label[for^='" + this.id + "']").html() != null) {
            //                        userIDs += $("label[for^='" + this.id + "']").html() + ",";
            //                    }
            //                });
            //                userIDs = userIDs.substring(0, userIDs.length - 1);
            //                $(window.parent.document).find(".inputbox_80").val(userIDs);
            //                closeWindow();
            //            });
            $("label[for^='ctl00_ContentPlaceHolder1_CustomGridView1']").css("display", "none");
            var name = "";
            var phone = "";
            var email = "";
            $("input[type='radio']").click(function () {
                var arrRe = $("input[type='radio']:checked");
                var value = $("label[for^='" + this.id + "']").html();
                var strs = value.split('$');
                name = strs[0];
                phone = strs[1];
                email = strs[2];
                $(arrRe).each(function () {
                    this.checked = false;
                });
            });

            $("#btnSave").click(function () {
                $(window.parent.document).find(".inputdutyuser").val(name);
                $(window.parent.document).find(".inputmobile").val(phone);
                $(window.parent.document).find(".inputemail").val(email);
                closeWindow();
            });
        });    
    </script>
</asp:Content>
