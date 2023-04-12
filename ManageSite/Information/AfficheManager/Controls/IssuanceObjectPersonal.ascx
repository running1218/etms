<%@ Control Language="C#" AutoEventWireup="true" CodeFile="IssuanceObjectPersonal.ascx.cs"
    Inherits="Information_AfficheManager_Controls_IssuanceObjectPersonal" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<!--查找条件-->
<div class="dv_searchbox">
    <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <th>
                所属<asp:Literal ID="ltlDepartment" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>
            </th>
            <td>
                <cc1:DictionaryDropDownList runat="server" ID="DictionaryDropDownList2" DictionaryType="Dic_DepartmentList"
                    IsShowChoose="true" />
            </td>
            <th>
                <asp:Literal ID="ltlRank" runat="server" Text="<%$ Resources:UIResource, ui_rank%>"></asp:Literal>
            </th>
            <td colspan="5">
                <cc1:DictionaryDropDownList runat="server" ID="DictionaryDropDownList3" DictionaryType="Dic_Rank"
                    IsShowChoose="true" />
                <input class="btn_Search" type="button" value="查询"/><a href="javascript:hideGridview()" class="dropdownico"
                    id="Highsearch">高级搜索</a>
            </td>
        </tr>
        <tr>
            <th>
                <asp:Literal ID="ltlPosition" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>
            </th>
            <td>
                <cc1:DictionaryDropDownList runat="server" ID="DictionaryDropDownList1" DictionaryType="Dic_PostType"
                    IsShowChoose="true" />
            </td>
            <th>
                职 务
            </th>
            <td>
                <cc1:DictionaryDropDownList runat="server" ID="DictionaryDropDownList4" DictionaryType="Dic_PossyType"
                    IsShowChoose="true" />
            </td>
            <th>
                姓 名
            </th>
            <td>
                <input type="text" name="textfield" class="inputbox_120" />
            </td>
            <th>
                工 号
            </th>
            <td>
                <input type="text" name="textfield" class="inputbox_120" />
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
            <input id="btn_Add" type="button" class="btn_Add" value="新增" runat="server" onclick="javascript:showWindow('新增群组','IssuanceObjectPersonalAdd.aspx',760,620)" />
            <input id="btn_Save" type="button" class="btn_Add" value="保存" runat="server" />
            <input id="btn_Del" type="button" class="btn_Del" value="删除" onclick="javascript:confirmInfo('确定删除吗？','提示')" runat="server" />
        </div>
        <div class="dv_pageControl">
            <uc2:pageset id="PageSet1" runat="server" />
        </div>
    </div>
    <!--列表 begin-->
    <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="True"
        CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="1">
        <Columns>
            <asp:TemplateField HeaderText="">
                <ItemStyle HorizontalAlign="Center" Width="40" />
                <HeaderStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="序号">
                <ItemStyle HorizontalAlign="Center" Width="60" />
                <HeaderStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="LabNo" runat="server" Text='<%#  ((this.PageSet1.PageIndex -1) * this.PageSet1.PageSize) + Container.DataItemIndex+1 %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton3" runat="server">详细信息</asp:LinkButton>
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
    <script language="javascript" type="text/javascript">

        divPage2.innerHTML = divPage1.innerHTML;
        //document.getElementById("ctl00_ContentPlaceHolder1_chkboxControler").attachEvent('onclikc', aa);
        
    </script>
</div>
