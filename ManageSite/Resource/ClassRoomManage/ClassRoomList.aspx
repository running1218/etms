<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="ClassRoomList.aspx.cs" Inherits="ETMS.WebApp.Manage.Resource.ClassRoomManage.ClassRoomList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--导航路径-->
        <div class="dv_path" id="dv_path">
            当前位置：资源管理系统&gt;&gt;教室管理&gt;&gt;教室管理
        </div>
        <!--功能标题-->
        <h2 class="dv_title">
            教室管理
        </h2>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray th70" border="0" cellpadding="0" cellspacing="0" runat="server"
                id="tableQueryControlList">
                <tr>
                    <th width="80">
                        教室编码：
                    </th>
                    <td width="120">
                        <asp:TextBox ID="txt_ClassRoomCode" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                    <th width="80">
                        教室名称：
                    </th>
                    <td width="120">
                        <asp:TextBox ID="txt_ClassRoomName" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                    <th width="80">
                        状&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;态：
                    </th >
                    <th width="60">
                        <cc1:DictionaryDropDownList runat="server" CssClass="select_60" ID="ddl_ClassRoomStatus" DictionaryType="Dic_Status" />
                    </th>
                    <td class="Search_Area">
                        <asp:UpdatePanel ID="upQuery" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                              <asp:Button ID="btnSearch" runat="server" CssClass="btn_Search" Text="查询" OnClick="btnSearch_Click" />
                          </ContentTemplate>
                        </asp:UpdatePanel>
                       <%-- <a href="javascript:hideGridview()" class="dropdownico" id="A1">高级搜索</a>--%>
                     </td>
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
         <asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional">
          <ContentTemplate>
            <!--翻页-->
            <div class="dv_pagePanel">
                <div class="dv_selectAll">
                    <input type="button" class="btn_Add" value="新增" onclick="javascript:showWindow('新增教室','ClassRoomAdd.aspx')" />
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="CustomGridView1_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="教室编码"   ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft field12">
                       <ItemTemplate>
                          <cc1:ShortTextLabel ShowTextNum="10" runat="server" ToolTip='<%#Eval("ClassRoomName") %>' Text='<%#Eval("ClassRoomCode") %>' ID="lblClassRoomCode" />
                       </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="教室名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                       <ItemTemplate>
                          <cc1:ShortTextLabel ShowTextNum="10" ToolTip='<%#Eval("ClassRoomName") %>' runat="server" Text='<%#Eval("ClassRoomName") %>' ID="lblClassRoomName" />
                       </ItemTemplate>
                    </asp:TemplateField>                  
                    <asp:TemplateField HeaderText="状态" HeaderStyle-Width="50">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("ClassRoomStatus").ToString()=="True"?"启用":"停用" %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Capacity" HeaderText="容量" HeaderStyle-Width="50" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright"/>
                    <asp:BoundField DataField="Price" HeaderText="价格" HeaderStyle-Width="50" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright"/>
                    <asp:BoundField DataField="DutyUser" HeaderText="负责人" HeaderStyle-CssClass="field8"/>
                    <asp:BoundField DataField="Phone" HeaderText="联系电话"  HeaderStyle-CssClass="field12 alignright" ItemStyle-CssClass="alignright"/>
                    <asp:TemplateField HeaderText="操作" HeaderStyle-Width="100">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <a href="javascript:showWindow('编辑教室','<%# this.ActionHref(string.Format("ClassRoomEdit.aspx?ClassRoomID={0}",Eval("ClassRoomID").ToGuid())) %>')">
                                编辑</a>
                             <cc1:CustomLinkButton runat="server" ID="lbtn_Del" CommandArgument='<%# Eval("ClassRoomID") %>'
                                CommandName="Del" Text="删除" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="确定要删除吗？" />
                            <a href="javascript:showWindow('查看教室','<%#this.ActionHref(string.Format("ClassRoomView.aspx?ClassRoomID={0}",Eval("ClassRoomID").ToGuid())) %>')">
                                查看</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </cc1:CustomGridView>
            <!--列表 end-->
            <div class="dv_splitLine">
            </div>
            <!--翻页-->
            <div class="dv_pagePanel">
            </div>
            </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
