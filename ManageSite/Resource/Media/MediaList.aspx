<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="MediaList.aspx.cs" Inherits="Resource_Media_MediaList" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="dv_searchbox">
        <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" id="tableQueryControlList"
            runat="server">
            <tr>
                <th style="width:120px;">
                    名称：
                </th>
                <td>
                    <asp:TextBox ID="txt_MediaName" runat="server" CssClass="inputbox_210"></asp:TextBox>
                    <asp:Button ID="lbnSearch" runat="server" Text="查询" CssClass="btn_Search" OnClick="btnSearch_Click" ></asp:Button>
                </td>
                <th width="120px" class="hide">
                    类型：
                </th>
                <td class="hide">
                    <asp:DropDownList ID="ddl_MediaType" runat="server">
                        <asp:ListItem Text="全部" Value="-1" Selected="True">全部</asp:ListItem>
                        <asp:ListItem Text="视频" Value="1" >视频</asp:ListItem>
                        <asp:ListItem Text="音频" Value="2">音频</asp:ListItem>
                    </asp:DropDownList>
                    
                </td>
            </tr>
        </table>
    </div>
    <div class="dv_searchlist">
        <!--翻页-->
        <div class="dv_pagePanel">
            <div class="dv_selectAll">
                <input type="button" class="btn_Add" value="新增" onclick="javascript: showWindow('新增媒体', 'MediaAdd.aspx')" />
            </div>
            <div class="dv_pageControl">
                <uc2:PageSet ID="PageSet1" runat="server" />
            </div>
        </div>
        <div class="media-box">
            <asp:Repeater ID="gvList" runat="server" OnItemCommand="gvList_ItemCommand" >
                <ItemTemplate>
                    <div class="media-instance">
                <div class="media-img">
                    
                    <img alt="" src='<%#string.IsNullOrEmpty(Eval("ImagePath").ToString())?"":Eval("ImagePath").ToString() %>' />
                    <div class="media-time" style="display:none;"><%# SecondsStr(Convert.ToInt32(Eval("PlayTime"))) %></div>
                </div>
                <div class="media-name"><%# Eval("MediaName") %></div>
                <div class="media-action">
                    <cc1:CustomLinkButton runat="server" ID="CustomLinkButton1" CommandArgument='<%# Eval("MediaID") %>'
                                        CommandName="Recommend" Text='<%# Convert.ToBoolean(Eval("IsRecommend"))?"取消推荐":"推荐" %>' />
                   
                    <a href="javascript:showWindow('编辑课程','<%# this.ActionHref(string.Format("MediaEdit.aspx?MediaID={0}",Eval("MediaID").ToString())) %>')">
                                        编辑</a>               
                    <cc1:CustomLinkButton runat="server" ID="lbtn_Del" CommandArgument='<%# Eval("MediaID") %>'
                                        CommandName="Del" Text="删除" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="确定要删除吗？" />                    
                   <a href="javascript:void(0)" onclick="Preview('<%# this.ActionHref(System.Configuration.ConfigurationManager.AppSettings["CourseWareHost"]+"/Media/play.aspx?Media="+Eval("MediaPath").ToString()+"&MediaType="+Eval("MediaType").ToString()) %>')">预览</a>
                   
                </div>
            </div> 
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $('.media-instance').mouseover(function () {
                $(this).find('.media-name').hide();
                $(this).find('.media-action').show();
            });
            $('.media-instance').mouseout(function () {
                $(this).find('.media-name').show();
                $(this).find('.media-action').hide();
            });
        });
        function Preview(MediePath) {            
            $.layer({
                type: 2,
                title: false,
                area: ['744px', '421px'],
                shadeClose: false,
                closeBtn: [0, true],
                offset: [($(window).height() - 300) / 2 + 'px', ''], //上下垂直居中
                border: [0],
                shade: [0],
                bgcolor: '#000',
                iframe: { src: MediePath }
            });
        }
    </script>
</asp:Content>



