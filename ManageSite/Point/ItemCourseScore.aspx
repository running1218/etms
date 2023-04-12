<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="ItemCourseScore.aspx.cs" Inherits="Score_ItemCourseScore" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--查找条件-->
    <div class="dv_searchbox">
        <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
            id="tableQueryControlList">
            <tr>
                <th>
                    课程属性：
                </th>
                <td class="Search_Area">
                    <cc1:DictionaryDropDownList ID="ddl_CourseAttrID" runat="server" DictionaryType="Dic_Sys_CourseAttr"
                        IsShowChoose="true" IsShowAll="false" />
                    <asp:Button ID="btnSearch" runat="server" CssClass="btn_Search" Text="查询" OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
    </div>
    <!--查找结果-->
    <div class="dv_searchlist">
        <!--翻页-->
        <div class="dv_pagePanel">
            <div class="dv_selectAll">
                <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="新增" CssClass="btn_Add" />
            </div>
            <div class="dv_pageControl">
                <uc2:PageSet ID="PageSet1" runat="server" />
            </div>
        </div>
        <!--列表 begin-->
        <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="False"
            ShowFooter=" false" DataKeyNames="StudentCoursePointRoleID" OnRowCommand="CustomGridView1_RowCommand" OnRowDataBound="CustomGridView1_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="序号">
                    <ItemStyle HorizontalAlign="Center" Width="60" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="LabNo" runat="server" Text='<%#((this.PageSet1.PageIndex -1) * this.PageSet1.PageSize) + Container.DataItemIndex+1 %>' />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblNo" runat="server" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="课程属性">
                   <ItemTemplate>
                       <cc1:DictionaryLabel ID="lblCourseAttrID" runat="server" DictionaryType="Dic_Sys_CourseAttr" FieldIDValue='<%#Eval("CourseAttrID") %>' />
                   </ItemTemplate>
                   <FooterTemplate>
                       <asp:DropDownList ID="ddlFootCourseAttrID" runat="server" />
                   </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="培训课时（小时）">
                    <ItemTemplate>
                        <asp:Label ID="lblMinNum" runat="server" Text='<%#Eval("MinNum") %>' />
                        -
                        <asp:Label ID="lblMaxNum" runat="server" Text='<%#Eval("MaxNum") %>' />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtFootMinNum" runat="server" CssClass="inputbox_60"  />
                        -
                        <asp:TextBox ID="txtFootMaxNum" runat="server" CssClass="inputbox_60" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="积分">
                    <ItemTemplate>
                        <asp:Label ID="lblScore" runat="server" Text='<%#Eval("GivePoints") %>' />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtFootScore" runat="server" CssClass="inputbox_60" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <cc1:CustomLinkButton runat="server" ID="lbtn_Del" CommandArgument='<%#Eval("StudentCoursePointRoleID") %>'
                            CommandName="del" Text="删除" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="确定要删除吗？" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:LinkButton ID="lbnSave" runat="server" Text="保存" CommandName="save" ValidationGroup="Edit"
                            CommandArgument='<%#Eval("ID") %>' />
                        <asp:LinkButton ID="btnCancel" runat="server" Text="取消" CommandName="cancel" />
                    </FooterTemplate>
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
</asp:Content>
