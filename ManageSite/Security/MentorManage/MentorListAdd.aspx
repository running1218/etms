<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="MentorListAdd.aspx.cs" Inherits="Security_MentorManage_MentorListAdd" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" runat="Server">
    <a id="aBack" runat="server" class="btn_Return">返回</a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--查找条件-->
    <div class="dv_searchbox">
        <table class="GridviewGray th70" border="0" cellpadding="0" cellspacing="0" id="tableQueryControlList"
            runat="server">
            <tr>
                <th >
                    姓&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;名：
                </th>
                <td >
                    <asp:TextBox ID="txtTeacherName" runat="server" ></asp:TextBox>
                    <asp:Button ID="Button1" runat="server" CssClass="btn_Search" Text="查询" OnClick="btnSearch_Click" />
                    <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a> 
                </td> 
            </tr>
            <tr>
                 <th>
                    <asp:Literal ID="ltlDepartment" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>：
                </th>
                <td >
                    <asp:TextBox ID="txtDepartment" runat="server" />
                </td>
                <th >
                    <asp:Literal ID="ltlPosition" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>：
                </th>
                <td >
                    <asp:TextBox ID="txtPost" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <!--查找结果-->
    <div class="dv_searchlist">
        <!--翻页-->
        <div class="dv_pagePanel" id="divPage1">
            <div class="dv_selectAll">
                <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="GridViewList" />
                <asp:Button runat="server" ID="btnAdd" CssClass="btn_Ok" Text="确定" OnClick="btnAdd_Click" />
            </div>
            <div class="dv_pageControl">
                <uc2:PageSet ID="PageSet1" runat="server" />
            </div>
        </div>
        <!--列表 begin-->
        <cc1:CustomGridView ID="GridViewList" runat="server" AutoGenerateColumns="False"
            DataKeyNames="UserID">
            <Columns>
                <asp:TemplateField HeaderText="">
                    <ItemStyle HorizontalAlign="Center" Width="40" />
                    <HeaderStyle HorizontalAlign="Center" Width="20" />
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="RealName" HeaderText="姓名" HeaderStyle-CssClass="alignleft field8" ItemStyle-CssClass="alignleft"  />
                <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <cc1:DictionaryLabel runat="server" ID="lblDepartment" TextLength="10" DictionaryType="Site_DepartmentByOrgID"
                            FieldIDValue='<%#Eval("DepartmentID").ToString() %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_position%>" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <cc1:DictionaryLabel runat="server" ID="ddlPost" TextLength="10" DictionaryType="Dic_PostByOrgID"
                            FieldIDValue='<%#Eval("PostID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_rank%>" HeaderStyle-Width="100">
                    <ItemTemplate>
                        <cc1:DictionaryLabel runat="server" ID="ddlRank" DictionaryType="vw_Dic_Sys_Rank"
                            TextLength="10" FieldIDValue='<%#Eval("RankID") %>' />
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
</asp:Content>
