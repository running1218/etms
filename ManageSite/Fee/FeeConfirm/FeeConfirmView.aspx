<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="FeeConfirmView.aspx.cs" Inherits="FeeConfirmView" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <!--导航路径-->
        <div class="dv_path">
            当前位置：费用管理>>课酬管理>>查看课时费用确认单
        </div>
        <!--功能标题-->
        <h2 class="dv_title">
            查看课时费用确认单
            <a href="FeeConfirmList.aspx" class="btn_Return" title="返回">&nbsp;</a>
        </h2>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0">
                 <tr>
                    <th>单据编号</th>
                    <td>
                        IT0001010101020120224001                
                    </td>                    
                    <th>单据名称</th>
                    <td>                
                        教育培训费用第二期（企业生存培训）                                                        
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
                        <div class="dv_pageControl">
                            <uc2:PageSet ID="PageSet1" runat="server" />
                        </div>
                    </div>
                    <!--列表 begin-->
                    <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="True"
                        CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0">
                        <Columns>                          
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

