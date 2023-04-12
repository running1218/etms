<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="FeeTeacherSettingList.aspx.cs" Inherits="Fee_CourseFeeSetting_FeeTeacherSettingList" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div>
        <!--导航路径-->
        <div class="dv_path">
            当前位置：费用管理>>课酬管理>>课酬标准管理>>设置讲师
        </div>
        <!--功能标题-->
        <h2 class="dv_title">
            课酬标准讲师设置
            <a class="btn_Return" href="FeeSettingList.aspx" title="返回">&nbsp;</a>
        </h2>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0">
                 <tr>
                    <th>讲师姓名：</th>
                    <td>
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="inputbox_210"></asp:TextBox>       <input class="btn_Search" type="button" value="查询"/>                 
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
                            <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" />
                            <input type="button" class="btn_Add" value="新增" onclick="javascript:showWindow('新增课酬标准讲师','FeeTeacherAdd.aspx',680,580)" />
                            <input type="button" class="btn_Del" value="删除" onclick="popConfirmMsg('确信要删除么','提示','');" />
                        </div>
                        <div class="dv_pageControl">
                            <uc2:PageSet ID="PageSet1" runat="server" />
                        </div>
                    </div>
                    <!--列表 begin-->
                    <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="True"
                        CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0">
                        <Columns>
                            <asp:TemplateField HeaderText="">
                                <ItemStyle HorizontalAlign="Center" Width="40" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
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
    </div>
</asp:Content>

