<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="FeeConfirmAdd.aspx.cs" Inherits="FeeConfirmAdd" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <!--导航路径-->
        <div class="dv_path">
            当前位置：费用管理>>课酬管理>>课时费用确认单
        </div>
        <!--功能标题-->
        <h2 class="dv_title">
            编辑课时费用确认单
            <a href="FeeConfirmList.aspx" class="btn_Return"></a>
        </h2>
        <!--查找条件-->
        <div class="dv_pageInformation">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0">
                 <tr>
                    <th>单据编号：</th>
                    <td>
                        IT0001010101020120224001                
                    </td>                    
                    <th>单据名称：</th>
                    <td>                
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="inputbox_120"></asp:TextBox>                                                      
                    </td>
                </tr>   
                <tr>
                    <th>备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注：</th>
                    <td colspan="3">
                        <asp:TextBox ID="TextBox2" runat="server" CssClass="inputbox_490"></asp:TextBox> 
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
                            <input type="button" class="btn_SeclectTime" value="选择课时安排" onclick="javascript:showWindow('选择费用课时安排','FeeCourseDetails.aspx',750,550)" />
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
                            <asp:TemplateField HeaderText="实际标准">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtActualStandard" runat="server" Text="300" CssClass="inputbox_40"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="实际课时">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtActualHours" runat="server" Text="2" CssClass="inputbox_40"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>    
                            <asp:TemplateField HeaderText="实际费用">
                                <ItemTemplate>
                                    <asp:Label ID="lblActualFee" runat="server" Text="600"></asp:Label>
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
    <!--提交表单-->
<div class="dv_submit">
    <a href="javascript:history.go(-1);" class="btn_Save">保存</a> <a href="javascript:history.go(-1);"
        class="btn_2">取消</a>
</div>
</asp:Content>

