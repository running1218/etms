<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MPageAdmin.Master" CodeFile="ResourceQueryAreaAdd.aspx.cs" Inherits="QS_ResourceQueryAreaAdd" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content runat="server" ContentPlaceHolderID="cphBack" ID="Content2">
    <script type="text/javascript">
        $(function () {
            $(".dv_searchbox").find("table tr:gt(0)").show();
            $("#Highsearch").addClass("dropupico");
        })
       
    </script>
    <a class="btn_Return" onclick='window.location ="<%=this.ActionHref(string.Format("ResourceQueryArea.aspx?QueryID={0}",QueryID))%>"'>
        返回</a>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray">
                <tr>
                    <th>
                        机构范围：
                    </th>
                    <td>
                        <asp:RadioButtonList runat="server" ID="ddl_OrganizationID" RepeatLayout="Flow" RepeatDirection="Horizontal">
                            <asp:ListItem Text="本组织机构" Value="1" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="本组织机构及下级组织机构" Value="2"></asp:ListItem>
                            <asp:ListItem Text="仅所有下级组织机构" Value="3"></asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:Button ID="btnOrgSearch" runat="server" Text="查询" OnClick="btnSearchOrgClick"
                            SkinID="Search" />
                    </td>
                </tr>
                <tr>
                    <th>
                        机构编码：
                    </th>
                    <td>
                        <asp:TextBox ID="txtOrgCode" runat="server" CssClass="inputbox_120 floatleft"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        机构名称：
                    </th>
                    <td>
                        <asp:TextBox ID="txtOrgName" runat="server" CssClass="inputbox_120 floatleft"></asp:TextBox>
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
                    <asp:Button ID="btnAdd" runat="server" Text="确定" CssClass="btn_Ok" OnClick="btnAdd_Click" />
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="OrgGridView" runat="server" AutoGenerateColumns="false" DataKeyNames="OrganizationID"
                CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0">
                <Columns>
                    <asp:TemplateField HeaderStyle-Width="18">
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="OrganizationCode" HeaderText="机构编码" ItemStyle-CssClass="alignleft"
                        HeaderStyle-CssClass="alignleft" HeaderStyle-Width="80" />
                    <asp:BoundField DataField="OrganizationName" HeaderText="机构名称" ItemStyle-CssClass="alignleft"
                        HeaderStyle-CssClass="alignleft" HeaderStyle-Width="80" />
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

