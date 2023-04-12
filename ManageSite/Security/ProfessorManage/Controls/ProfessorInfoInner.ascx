<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProfessorInfoInner.ascx.cs"
    Inherits="ETMS.WebApp.Manage.Resource.ProfessorManage.Controls.ProfessorInfoInner" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<div >
    <h2 class="dv_title">
        内部讲师
    </h2>
    <br />
    <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray"  border="0" cellpadding="0" cellspacing="0">
            <tr>
             <th style="width: 80px;">
                    工&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;号：
                </th>
                <td width="120">
                    <asp:TextBox ID="txtWorkerNo" runat="server" CssClass="inputbox_90"></asp:TextBox>
                </td>
                <th style="width: 80px;">
                    讲师姓名：
                </th>
                <td>
                    <asp:TextBox ID="txtTeacherName" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="btn_Search" OnClick="btnSearch_Click" />
                </td>
            </tr>
   
        </table>
    </div>
    <div class="dv_searchlist">
        <!--翻页-->
        <div class="dv_pagePanel" id="divPage1">
            <div class="dv_selectAll">
                <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" />
                <asp:Button ID="btnSave" runat="server" CssClass="btn_Ok" OnClick="btnSave_Click" Text="确定" />
            </div>
            <div class="dv_pageControl" style="float: right;">
                <uc2:PageSet ID="PageSet1" runat="server" />
            </div>
        </div>
        <!--列表 begin-->
        <cc1:CustomGridView ID="GridViewList" runat="server" AutoGenerateColumns="False" DataKeyNames="UserID" >
            <Columns>
                <asp:TemplateField HeaderText="">
                    <ItemStyle HorizontalAlign="Center"  />
                    <HeaderStyle HorizontalAlign="Center" Width="18"/>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="账户" HeaderStyle-Width="100" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <cc1:ShortTextLabel ID="lblLoginName" runat="server" Text='<%#Eval("LoginName") %>' ShowTextNum="10" />
                    </ItemTemplate>
                </asp:TemplateField>          
                <asp:BoundField DataField="WorkerNo" HeaderText="工号" HeaderStyle-Width="80" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft" />
                <asp:BoundField DataField="RealName" HeaderText="学员姓名"  HeaderStyle-Width="80" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft" />
                <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft" >
                   <ItemStyle HorizontalAlign="Center" />
                   <HeaderStyle HorizontalAlign="Center" />
                   <ItemTemplate>
                        <cc1:DictionaryLabel ID="lblDepartment" DictionaryType="Site_DepartmentByOrgID" runat="server" FieldIDValue='<%#Eval("DepartmentID") %>' />  
                   </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="性别"  HeaderStyle-Width="40">
                   <ItemStyle HorizontalAlign="Center" />
                   <HeaderStyle HorizontalAlign="Center" />
                   <ItemTemplate>
                        <cc1:DictionaryLabel ID="lblSex" DictionaryType="Dic_Sys_Sex" runat="server" FieldIDValue='<%#Eval("SexTypeID") %>' />  
                   </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="LastEducation" HeaderText="最高学历"  HeaderStyle-Width="80"/>
                <asp:BoundField DataField="Specialty" HeaderText="专业"   HeaderStyle-Width="80" />
                <asp:BoundField DataField="Telphone" HeaderText="电话"  HeaderStyle-Width="80"/>
            </Columns>
        </cc1:CustomGridView>
        <!--列表 end-->
        <div class="dv_splitLine">
        </div>
            <div class="dv_pagePanel" id="divPage2"></div>
    </div>
</div>
<%--<div class="dv_submit">
    <asp:Button ID="btnSave" runat="server" CssClass="btn_Save" OnClick="btnSave_Click" Text="保存" />
    <a href="javascript:closeWindow();" class="btn_Cancel padleft10">取消</a>
</div>--%>
