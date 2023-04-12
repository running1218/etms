<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="EvaluationApproveList.aspx.cs" Inherits="Valuation_EvaluationApproveList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" runat="Server">
    <asp:LinkButton ID="lbtnreturn" runat="server" Text="返回" CssClass="btn_Return" ></asp:LinkButton>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--查找条件-->
    <div class="dv_searchbox">
        <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
            id="tableQueryControlList">
            <tr>
                <th style="width: 100px">点评内容：
                </th>
                <td style="width: 200px">
                    <asp:TextBox ID="txt_Content" runat="server" CssClass="inputbox_120"></asp:TextBox>
                </td>
                <th style="width: 100px">审批状态</th>
                <td style="width: 200px">
                    <cc1:DictionaryDropDownList runat="server" ID="ddl_TrainingLevelID" DictionaryType="ApproveStatus"
                        IsShowAll="true" />
                </td>
                <td style="width: 100px">
                    <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
                </td>
                <td></td>
            </tr>
        </table>
    </div>
    <div class="dv_searchlist">
        <!--翻页-->
        <div class="dv_pagePanel">
            <div class="dv_selectAll">
                <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" />
                <cc1:CustomButton runat="server" ID="btnApproveAll" Text="审批通过" ToolTip="审批通过" CssClass="btn_Verify_4" EnableConfirm="true"
                        ConfirmTitle="提示" ConfirmMessage="确定审批通过吗？" OnClick="btnApproveAll_Click" />
            </div>
            <div class="dv_pageControl">
                <uc2:PageSet ID="PageSet1" runat="server" />
            </div>
        </div>
        <!--列表 begin-->
        <cc1:CustomGridView ID="CustomGridView1" runat="server" CssClass="GridviewGray" EmptyDataSourceTip="没有任何记录！"
            AutoCreateColumnInsertIndex="0" AutoGenerateColumns="False" DataKeyNames="ResultSubID"
            CustomAllowPaging="False" IsEmpty="False" TotalRecordCount="0" OnRowDataBound="CustomGridView1_RowDataBound" OnRowCommand="CustomGridView1_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="20" />
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" Enabled="false"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="姓名" HeaderStyle-Width="60" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <cc1:ShortTextLabel ID="lblUserName" runat="server" ShowTextNum="15" Text='<%# getUserName(Eval("UserID").ToInt())%>'></cc1:ShortTextLabel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="时间" HeaderStyle-Width="120">
                    <ItemTemplate>
                        <%#Eval("CreateTime")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="综合点评" HeaderStyle-Width="120">
                    <ItemTemplate>
                        <%# getScore(Eval("UserID").ToInt())%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="点评内容" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <cc1:ShortTextLabel ID="lblContent" runat="server" Text='<%#Eval("EvaluationContent")%>' ShowTextNum="100" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="审批状态" HeaderStyle-Width="80">
                    <ItemTemplate>
                        <cc1:DictionaryLabel ID="DictionaryLabel1" DictionaryType="ApproveStatus" FieldIDValue='<%# Eval("ApproveStatus") %>'
                            runat="server" />
                        <asp:HiddenField ID="hf_ApproveStatus" runat="server" Value='<%# Eval("ApproveStatus") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作" HeaderStyle-Width="80">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtn_Approve" runat="server" CommandName="Approve" CommandArgument='<%# Eval("ResultSubID") %>'>审批</asp:LinkButton>
                        <asp:LinkButton ID="lbtn_CancelApprove" runat="server" CommandName="CancelApprove" CommandArgument='<%# Eval("ResultSubID") %>' Visible="false">取消审批</asp:LinkButton>
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
</asp:Content>

