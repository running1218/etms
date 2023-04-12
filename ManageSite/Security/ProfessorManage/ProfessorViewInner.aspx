<%@ Page Title="员工详情" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master"
    AutoEventWireup="true" CodeFile="ProfessorViewInner.aspx.cs" Inherits="ETMS.WebApp.Manage.Resource.ProfessorManage.ProfessorViewInner" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="dv_title">
        内部讲师
    </h2>
    <!--表单录入-->
    <div class="dv_information">
       <table class="GridviewGray">
        <tr>
            <th width="80">
                工　　号：
            </th>
            <td>
                TW10010112 
            </td>
            <th width="80">
                姓　　名：
            </th>
            <td>
               王冰 
            </td>
        </tr>
        <tr>
            <th>
                部　　门：
            </th>
            <td colspan=3">
                102集团总部战略与投资管理中心
            </td>
            </tr>
        <tr>
            <th>
                岗　　位：
            </th>
              <td colspan=3">
                通信技术开发及应用
            </td>
        </tr>

    <tr>
            
            <th>
                地　　址：
            </th>
            <td>
                北京市西城区xxx大厦6层
            </td>
            <th>
                邮政编码：
            </th>
            <td>
                100120
            </td>
        </tr>
        <tr>
        
            <th>
                邮　　箱：
            </th>
            <td>
               cuiyan@mail.com
            </td>
            <th>
                联系电话：
            </th>
            <td>
               123456789
            </td>
        </tr>
        <tr>
            <th>
                讲师分类：
            </th>
            <td>
               公司级
            </td>
            <th>
                 状　　态：
            </th>
            <td>
                启用
            </td>
        </tr>
        <tr>
            <th>
                讲师等级：
            </th>
            <td>
                高级
            </td>
            <th>
                
            </th>
            <td>
               
            </td>
        </tr>       
    </table>
    </div>
    <!--提交表单-->
    <div class="dv_submit">
        <a href="javascript:closeWindow();" class="btn_Close">关闭</a>
    </div>
</asp:Content>
