<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ElearningMapTypeInfo.ascx.cs" Inherits="ETMS.WebApp.Manage.Resource.ElearningMap.Controls.ElearningMapTypeInfo" %>
<!--功能标题-->
<h2 class="dv_title">
    学习地图类型基本信息
</h2>
<!--表单录入-->
<div class="dv_information">
    <table  class="GridviewGray">
        <tr>
            <th style="width:200px;">
                学习地图类型编码
            </th>
            <td colspan="5">
                <asp:TextBox ID="TextBox1" runat="server" CssClass="inputbox_210"></asp:TextBox>
               
            </td>
      </tr>
        <tr>
            <th>
                学习地图类型名称
            </th>
            <td colspan="5">
                <asp:TextBox ID="TextBox2" runat="server" CssClass="inputbox_210"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                学习地图纬度选择
            </th>
            <td colspan="5" >&nbsp;</td>
        </tr>
        <tr>
            <th>
                纬度1
            </th>
            <td>
                <asp:CheckBox ID="CheckBox1" runat="server" Text="部门" />
               
            </td>          
       
            <th>
                纬度2
            </th>
            <td>
             <asp:CheckBox ID="CheckBox2" runat="server" Text="职级" />
            </td>
            <th>
                纬度3
            </th>
            <td>
               <asp:CheckBox ID="CheckBox3" runat="server" Text="岗位" />
            </td>
          
        </tr>
    </table>
    <br /><br /><br />
    <!--提交表单-->
    <div class="dv_submit">
        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn_Save" 
            onclick="LinkButton1_Click">保存</asp:LinkButton>
        <a href="javascript:closeWindow();" class="btn_Cancel padleft10">取消</a>
    </div>
</div>