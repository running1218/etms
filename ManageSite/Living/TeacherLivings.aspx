<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="TeacherLivings.aspx.cs" Inherits="Living_TeacherLivings" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
                id="tableQueryControlList">
                <tr>
                    <th width="120px">
                        直播时间：
                    </th>
                    <td width="340px">                        
                        <cc1:DateTimeTextBox ID="txtBeginTime" runat="server" EndTimeControlID="txtEndTime"></cc1:DateTimeTextBox>
                            至
                        <cc1:DateTimeTextBox ID="txtEndTime" runat="server" BeginTimeControlID="txtBeginTime"></cc1:DateTimeTextBox>
                    </td>
                    <th width="80px">
                        直播名称：
                    </th>
                    <td class="Search_Area" style="width:170px;">
                        <asp:TextBox ID="txtLivingName" runat="server" CssClass="inputbox_160"></asp:TextBox>                                       
                    </td>
                    <td style="width:200px; vertical-align:middle; line-height:28px;" class="floatleft">
                        <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />  
                        &nbsp;
                        <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
                    </td>
                </tr>
                <tr>
                    <th width="120px">
                        课程名称：
                    </th>
                    <td class="Search_Area" style="width:340px;">
                        <asp:TextBox ID="txtCourseName" runat="server" CssClass="inputbox_160"></asp:TextBox>                                       
                    </td>
                    <th width="80px">
                        直播类型：
                    </th>
                    <td class="Search_Area" style="width:170px;">
                        <cc1:DictionaryDropDownList runat="server" ID="ddlLivingType" DictionaryType="Dic_Sys_LivingType"
                    IsShowChoose="true" CssText="select_100" />                                       
                    </td>
                    <td style="width:200px;">&nbsp;</td>
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel">
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="gvList" runat="server" CssClass="GridviewGray" EmptyDataSourceTip="没有任何记录！"
                AutoCreateColumnInsertIndex="0" AutoGenerateColumns="False" CurrentPageIndex="1"
                CustomAllowPaging="False" IsEmpty="False" TotalRecordCount="0" DataKeyNames="LivingID" OnRowDataBound="gvList_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="课程名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="stlCourseName" runat="server" ShowTextNum="100" Text='<%# Eval("CourseName")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="直播名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="ShortTextLabel1" runat="server" ShowTextNum="100" Text='<%# Eval("LivingName")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="直播类型" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="stlLivingType" runat="server" DictionaryType="Dic_Sys_LivingType" FieldIDValue='<%# Eval("LivingType")%>'></cc1:DictionaryLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="开始时间" HeaderStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="lblTeacherName" runat="server" Text='<%# Eval("StartTime").ToDateTime().ToString("yyyy-MM-dd HH:mm:ss") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="结束时间" HeaderStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="lblVideoTime" runat="server" Text='<%# Eval("EndTime").ToDateTime().ToString("yyyy-MM-dd HH:mm:ss") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="直播讲师" HeaderStyle-Width="100">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("TeacherName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                    
                    <asp:TemplateField HeaderText="操作" HeaderStyle-Width="200px">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>                            
                            <cc1:CustomLinkButton runat="server" ID="lbtLiving" CommandArgument='<%# Eval("LivingID") %>'
                                CommandName="living" Text="直播间"  />                                                        
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </cc1:CustomGridView>
            <!--列表 end-->
            <div class="dv_splitLine"></div>
            <!--翻页-->
            <div class="dv_pagePanel"></div>
        </div>
    </div>
    <script>
        function nostart(id)
        {            
            layer.alert('暂无回放信息');
        }
    </script>
</asp:Content>

