<%@ Page Title="开放范围" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master"
    AutoEventWireup="true" CodeFile="OpenRange.aspx.cs" Inherits="ETMS.WebApp.Manage.Resource.CourseManage.OpenRange" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--功能标题-->
   <%-- <h2 class="dv_title">
        组织机构
    </h2>--%>
    <!--表单录入-->
    <div class="dv_information">
        <table class="GridviewGray">
            <tr>
                <th colspan="3" style="text-align:left;">
                    组织机构
                </th>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="TextBox1" TextMode="MultiLine" CssClass="inputbox_area190 " runat="server"></asp:TextBox>
                </td>
                <td>
                    >><br />
                    &gt;<br />
                    &lt;<br />
                    <<
                </td>
                <td>
                    <asp:TextBox ID="TextBox2" TextMode="MultiLine" CssClass="inputbox_area190 " runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <!--提交表单-->
    <div class="dv_submit">
        <a href="javascript:closeWindow();" class="btn_Ok">提交</a> <a href="javascript:closeWindow();"
            class="btn_Cancel padleft10">取消</a>
    </div>
</asp:Content>
