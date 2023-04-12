<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="ValuationTeacher.aspx.cs" Inherits="Valuation_ValuationTeacher" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--查找条件-->
    <div class="dv_searchbox">
        <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
            id="tableQueryControlList">
            <tr>
                <th width="120">
                    讲师编码：
                </th>
                <td width="120">
                    <asp:TextBox ID="txt_TeacherCode" runat="server"></asp:TextBox>
                </td>
                <th width="120">
                    讲师姓名：
                </th>
                <td width="60">
                    <asp:TextBox ID="txt_Site_User999RealName" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
                </td>
              
            </tr>
        </table>
    </div>
    <!--查找结果-->
    <div class="dv_searchlist">
        <asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
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
                    DataKeyNames="TeacherID" OnRowDataBound="CustomGridView1_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="TeacherCode" HeaderText="讲师编码"  ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft field12" />
                        <asp:TemplateField HeaderText="讲师姓名" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblTeacherName" runat="server" ShowTextNum="10" Text='<%# Eval("RealName")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="讲师等级" HeaderStyle-Width="60">
                            <ItemTemplate>
                                <cc1:DictionaryLabel ID="DictionaryLabel0" DictionaryType="Dic_Sys_TeacherLevel"
                                    FieldIDValue='<%# Eval("TeacherLevelID") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="状态" HeaderStyle-Width="60">
                            <ItemTemplate>
                                <cc1:DictionaryLabel ID="DictionaryStatus" DictionaryType="Dic_Status" FieldIDValue='<%# Eval("IsUse") %>'
                                    runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="好评度" HeaderStyle-Width="60" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright">
                            <ItemTemplate>
                                <%# initEvaluation(Eval("TeacherID").ToString())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="打分" HeaderStyle-Width="60" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright">
                            <ItemTemplate>
                                <%# getScoreArgTeacher(Eval("TeacherID").ToString())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="人次" HeaderStyle-Width="60" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright">
                            <ItemTemplate>
                                <%# getUserCount(Eval("TeacherID").ToString())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="查看" HeaderStyle-Width="60" >
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtn_View" runat="server" CommandName="Views">查看</asp:LinkButton>
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
