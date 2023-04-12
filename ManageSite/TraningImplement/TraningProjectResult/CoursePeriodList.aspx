<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="CoursePeriodList.aspx.cs" Inherits="TraningImplement_TraningProjectResult_CoursePeriodList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--导航路径-->
        <div class="dv_path" id="dv_path">
            当前位置：培训管理>>培训实施>>项目课时安排管理>>课时安排
        </div>
        <!--功能标题-->
        <h2 class="dv_title">
            课时安排<input type="button" class="btn_Return" value="返回" onclick="javascript:history.back()" />
        </h2>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <th>
                        课程编码：
                    </th>
                    <td>
                        <asp:TextBox ID="TextBox3" runat="server" CssClass="inputbox_120"></asp:TextBox>                        
                    </td>
                    <th>
                        课程名称：
                    </th>
                    <td>
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="inputbox_120"></asp:TextBox>
                        <input class="btn_Search" type="button" value="查询"/><a href="javascript:hideGridview()" class="dropdownico"
                            id="Highsearch">高级搜索</a>
                    </td>
                </tr>
                <tr>
                    <th>
                        课时安排状态：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="TraningDirectionType2" DictionaryType="Dic_TraningCoursePeriodState"
                            IsShowAll="true" />
                    </td>
                    <th>
                        讲&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;师：
                    </th>
                    <td>
                        <asp:TextBox ID="TextBox2" runat="server" CssClass="inputbox_120"></asp:TextBox>
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
                    <input type="button" class="btn_Add" value="修改" onclick="javascript:showWindow('修改','CoursePeriodEdit.aspx',680,400)" />
                    <input type="button" class="btn_Del" value="删除" onclick="" />
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
                        <ItemStyle HorizontalAlign="Center" Width="28" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <a href="javascript:showWindow('修改状态','CoursePeriodEdit.aspx',680,400)">修改状态</a>
                            <a href="#">查看</a> 
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
            </script>
        </div>
    </div>
</asp:Content>
