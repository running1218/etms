<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CatalogInfo.ascx.cs" Inherits="QuestionDB_CatalogManage_Controls_CatalogInfo" %>
<!--功能标题-->
<h2 class="dv_title">
    试题分类信息
</h2>
<!--表单录入-->
<div class="dv_information">
    <table  class="GridviewGray">
         <tr>
            <th width="80">
                试题分类位置
            </th>
            <td>

                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
            </td>
      
        </tr>
        <tr>
            <th width="80">
                试题分类名称
            </th>
            <td>
                <asp:TextBox ID="TextBox1" runat="server" CssClass="inputbox_210"></asp:TextBox>
               
            </td>
      
        </tr>
        
    </table>
    <!--提交表单-->
    <div class="dv_submit">
        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn_Save" 
            onclick="LinkButton1_Click">保存</asp:LinkButton>
        <a href="javascript:closeWindow();" class="btn_Cancel padleft10">取消</a>
    </div>
</div>
