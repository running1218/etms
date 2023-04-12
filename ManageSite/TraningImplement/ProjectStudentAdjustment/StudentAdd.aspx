<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="StudentAdd.aspx.cs" Inherits="TraningImplement_ProjectStudentAdjustment_StudentAdd" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" runat="Server">
    <asp:LinkButton ID="lbtnReturn" runat="server" Text="返回" CssClass="btn_Return"></asp:LinkButton>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray th80" border="0" cellpadding="0" cellspacing="0" runat="server"
                id="tableQueryControlList">
                <tr>
                    <th width="100">
                        工&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;号：
                    </th>
                    <td width="200">
                        <asp:TextBox ID="txt_WorkerNo" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                    <th width="100">
                        姓&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;名：
                    </th>
                    <td class="Search_Area">
                        <asp:TextBox ID="txt_RealName" runat="server" CssClass="inputbox_120 floatleft"></asp:TextBox>
                        <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
                        <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
                    </td>
                </tr>
                <tr runat="server" id="trOrg">
                    <th width="100">
                        组织机构：
                    </th>
                    <td colspan="3">
                        <cc1:DictionaryDropDownList runat="server" ID="ddl_OrganizationID" DictionaryType="Dic_CurrentAndSubOrganization"
                            IsShowAll="true" AutoPostBack="True" CssClass="select_390" OnSelectedIndexChanged="ddl_OrganizationID_SelectedIndexChanged" />
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        <asp:Literal ID="ltlDepartment" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>：
                    </th>
                    <td>
                        <asp:DropDownList runat="server" ID="ddl_DepartmentID" CssClass="select_190">
                        </asp:DropDownList>
                    </td>
                    <th width="100">
                        <asp:Literal ID="ltlPosition" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>：
                    </th>
                    <td>
                        <asp:DropDownList runat="server" ID="ddl_PostID" CssClass="select_190">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        <asp:Literal ID="ltlRank" runat="server" Text="<%$ Resources:UIResource, ui_rank%>"></asp:Literal>：
                    </th>
                    <td colspan="3">
                        <cc1:DictionaryDropDownList runat="server" ID="ddl_RankID" DictionaryType="vw_Dic_Sys_Rank"
                            IsShowAll="true" CssClass="select_190" />
                    </td>
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel" id="divPage1">
                <div class="dv_selectAll">
                    <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" />
                    <input type="button" id="btnSelect" value="确定" class="btn_Ok" />
                    <div style="display:none">
                    <asp:Label ID="labTrainingItemCourseIDs" runat="server" Text="" CssClass="ItemCourseIDs"></asp:Label>
                    <asp:HiddenField ID="hfItemCourseIDs" runat="server" />
                    <input type="button" id="btnAddHid" class="btnAddHid" onclick="showMsg()" />
                    <asp:Button ID="btnAdd" runat="server" Text="确定" CssClass="" OnClick="btnAdd_Click" />
                    <asp:Button ID="btnAdd1" runat="server" Text="确定" CssClass="" OnClick="btnAdd1_Click" /></div>
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
                CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0"
                DataKeyNames="UserID" OnRowDataBound="CustomGridView1_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" Width="20" />
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="WorkerNo" HeaderText="工号" HeaderStyle-CssClass="field10 alignleft"
                        ItemStyle-CssClass="alignleft" />
                    <asp:BoundField DataField="RealName" HeaderText="姓名" HeaderStyle-CssClass="alignleft field8"
                        ItemStyle-CssClass="alignleft" />
                    <asp:TemplateField HeaderText="组织机构" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblOrganization" DictionaryType="Dic_CurrentAndSubOrganization"
                                FieldIDValue='<%# Eval("OrganizationID") %>' runat="server" TextLength="10" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblDepartmentID" DictionaryType="vw_Dic_Sys_Department"
                                FieldIDValue='<%# Eval("DepartmentID") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_position%>" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblPost" DictionaryType="vw_Dic_Sys_Post" FieldIDValue='<%# Eval("PostID") %>'
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_rank%>" HeaderStyle-Width="120" HeaderStyle-CssClass="alignleft"
                        ItemStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblRank" DictionaryType="vw_Dic_Sys_Rank" FieldIDValue='<%# Eval("RankID") %>'
                                runat="server" />
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
    <script type="text/javascript">
        $(document).ready(function () {
            $(".btn_Ok").click(function () {
                var arrCheck = $("input[type='checkbox']").is(':checked');
                if (arrCheck) {
                    showWindow('请选择课程信息', '<%=SelectCourseUrl %>');
                } else {
                    popAlertMsg("请选择要添加的学员", "提示", null);
                }
            });
        });
        function showMsg() {
            if ($("#<%=labTrainingItemCourseIDs.ClientID %>").html().trim() != "") {            
                document.getElementById("<%=hfItemCourseIDs.ClientID %>").value = $("#<%=labTrainingItemCourseIDs.ClientID %>").html();

                document.getElementById("<%= btnAdd.ClientID %>").click();
            } else {
                document.getElementById("<%= btnAdd1.ClientID %>").click();
            }
        }
    </script>
</asp:Content>
