<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="TrainingOrgTeacherList.aspx.cs" Inherits="TraningOrgManager_TraningOrgManager_TrainingOrgTeacherList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" runat="Server">
   <a id="aBack" runat="server" class="btn_Return">返回</a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="dv_GradeviewList">
        <table >
            <tr>
                <th>
                    培训机构编码：
                </th>
                <td>
                    <cc1:ShortTextLabel ID="lblOuterOrgCode" runat="server" ShowTextNum="10" />               
                </td>
            </tr>
            <tr>
                <th>
                    培训机构名称：
                </th>
                <td>
                    <cc1:ShortTextLabel ID="lblOuterOrgName" runat="server" ShowTextNum="10" />    
                </td>
            </tr>
            <tr>
                <th>
                    讲&nbsp;&nbsp;&nbsp;&nbsp;师&nbsp;&nbsp;&nbsp;&nbsp;数：
                </th>
                <td>
                    <asp:Label ID="lblTeachNum" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div class="dv_searchlist">
        <!--翻页-->
        <div class="dv_pagePanel" id="divPage1">
            <div class="dv_pageControl" style="float: right;">
                <uc2:PageSet ID="PageSet1" runat="server" />
            </div>
        </div>
        <!--列表 begin-->
        <cc1:CustomGridView ID="GridViewList" runat="server" AutoGenerateColumns="False"
            DataKeyNames="TeacherID" >
            <Columns>
                <asp:TemplateField HeaderText="序号">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="40"  />
                    <ItemTemplate>
                        <asp:Label ID="LabNo" runat="server" Text='<%#((this.PageSet1.PageIndex -1) * this.PageSet1.PageSize) + Container.DataItemIndex+1 %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="TeacherCode" HeaderStyle-Width="80" HeaderText="讲师编码" SortExpression="TeacherCode"
                    ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="TeacherName" HeaderText="讲师姓名" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft" SortExpression="TeacherName">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="讲师状态" HeaderStyle-Width="60">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblTeacherStatus" runat="server" Text='<%# Eval("IsUse").ToInt() == 1 ? "启用":"停用" %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="讲师等级" HeaderStyle-Width="60">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <cc1:DictionaryLabel ID="lblTeacherLevelID" runat="server" DictionaryType="Dic_Sys_TeacherLevel"
                            FieldIDValue='<%#Eval("TeacherLevelID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ClassReward" HeaderText="课酬" SortExpression="ClassReward" HeaderStyle-Width="60" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
        </cc1:CustomGridView>
        <!--列表 end-->
        <div class="dv_splitLine">
        </div>
        <!--翻页-->
        <div class="dv_pagePanel" id="divPage2">
        </div>
        <script language="javascript" type="text/javascript">
            divPage2.innerHTML = divPage1.innerHTML;
        </script>
    </div>
</asp:Content>
