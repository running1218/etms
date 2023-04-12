<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="CompetitionAdd.aspx.cs" Inherits="TraningImplement_ProjectCourseResource_CompetitionAdd" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="dv_information">
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <th>
                        项目名称：
                    </th>
                    <td>
                        飞鹰01
                    </td>
                    <th>
                        课程名称：
                    </th>
                    <td>
                        世界精品课程
                    </td>
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel" id="divPage1">
                <div class="dv_selectAll">
                    <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" />
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="True"
                CustomAllowPaging="false" ShowFooter="false" 
                AutoCreateColumnInsertIndex="1">
                <Columns>
                    <asp:TemplateField HeaderText="">
                        <ItemStyle HorizontalAlign="Center" Width="28" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="序号">
                        <ItemStyle HorizontalAlign="Center" Width="28" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="LabNo" runat="server" Text='<%#  ((this.PageSet1.PageIndex -1) * this.PageSet1.PageSize) + Container.DataItemIndex+1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <a href="#">预览</a>
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
    </div>
        <div class="dv_submit">
            <input type="button" class="btn_Add" value="确定" onclick="javascript:popSuccessMsg('恭喜你保存成功！','提示',closeWindow);" />
            <input type="button" class="btn_Cancel" value="取消" onclick="javascript:closeWindow();" />
        </div>
</asp:Content>

