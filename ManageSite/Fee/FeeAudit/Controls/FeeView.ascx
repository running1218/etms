<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FeeView.ascx.cs" Inherits="Fee_FeeAudit_Controls_FeeView" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>

<!--查找条件-->
<div class="dv_pageInformation">
    <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0">
            <tr>
            <th style="width:15%">单据编号：</th>
            <td>
                IT0001010101020120224001                
            </td>                    
            <th style="width:15%">当前状态：</th>
            <td>
                未审核                
            </td>
        </tr>
        <tr>
            <th>单据名称：</th>
            <td colspan="3">                
                教育培训费用第二期（企业生存培训）                                                        
            </td>
        </tr>  
        <tr>
            <th>备　　注：</th>
            <td colspan="3">                
                教育培训费用第二期（企业生存培训）                                                        
            </td>
        </tr>  
        <tr>
            <th>创建日期：</th>
            <td>
                2011-02-25                
            </td>                    
            <th>创 建 人：</th>
            <td>                
                王志                                                       
            </td>
        </tr>   
        <tr>
            <th>审核日期：</th>
            <td>
                2011-02-25              
            </td>                    
            <th>审 核 人：</th>
            <td>                
                李木然                                                         
            </td>
        </tr> 
        <tr>
            <th>审核意见：</th>
            <td colspan="3">                
                同意
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
                <div class="dv_selectAll" id="divAudit" runat="server">
                    <input type="button" class="btn_Verify" value="审核" onclick="showDiv('#dv_lay1','审核通过',function(){},340,200)" />
                </div>
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

            <!--审核意见的弹出层-->
            <div id="dv_lay1" style="display:none">
                <table style="width:90%;">
                    <tr>
                        <th>意见</th>
                        <td><textarea  class="inputbox_area210" id='txt1'></textarea></td>
                    </tr>
                </table>     

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>