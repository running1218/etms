<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Activity.master" AutoEventWireup="true" CodeBehind="MyActivity.aspx.cs" Inherits="ETMS.Studying.Activity.MyActivity1" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultPlaceHolder" runat="server">
    <link href="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/myactivity.css" type="text/css" rel="stylesheet" />
    <div class="view-area">
        <div class="con_my_right">
            <div id="tab1">
                <div class="con_my_a">
                    <div class="con_my_a_l">
                        <asp:Image ID="imgPic" runat="server" />
                    </div>
                    <div class="con_my_a_r">
                        <div class="con_my_a_title">
                            <asp:Literal ID="ltlTitle" runat="server"></asp:Literal>
                        </div>
                        <div class="con_my_a_date">活动时间：<asp:Literal ID="ltlBeginTime" runat="server"></asp:Literal>~<asp:Literal ID="ltlEndTime" runat="server"></asp:Literal></div>
                        <div class="con_my_a_no">报名号：<asp:Literal ID="ltlSiginNo" runat="server"></asp:Literal></div>
                    </div>
                </div>
                <div>
                    <table class="con_my_opus">
                        <thead>
                            <tr>
                                <th class="th1">作品类型</th>
                                <th class="th2">格式</th>
                                <th class="th3">作品名称</th>
                                <th class="th4">上传时间</th>
                                <th class="th5">操作</th>
                            </tr>
                        </thead>
                        <tbody class="<%= CommitCount == 0 ? "hide":"" %>">
                            <tr>
                                <td class="td1"><asp:Literal ID="ltlType" runat="server"></asp:Literal></td>
                                <td><asp:Literal ID="ltlExtention" runat="server"></asp:Literal></td>
                                <td>
                                    <asp:HyperLink ID="ltlName" CssClass="product-name" runat="server" Target="_blank"></asp:HyperLink>
                                </td>
                                <td><asp:Literal ID="ltlTime" runat="server"></asp:Literal></td>
                                <td>   
                                    <p class="<%= ltlEndTime.Text.ToDateTime() < DateTime.Now ? "hide" : "" %>">
                                        <a id="lbnEidt" class="con_my_opus_edit" href="javascript:showLayerWindow('上传活动作品','Production.aspx?ID=<%=SiginUpID %>&ProductID=<%=hfID.Value %>', 730, 400)">修改</a>
                                        <cc1:CustomButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" CssClass="con_my_opus_del" ConfirmMessage="确认删除吗？" EnableConfirm="true" ConfirmTitle="删除提示" Text="删除" />
                                        <asp:HiddenField ID="hfID" runat="server" /> 
                                    </p>                                           
                                </td>
                            </tr>
                            <tr class="<%= CommitCount == 1 ? "hide":"" %>">
                                <td class="td1"><asp:Literal ID="ltlType2" runat="server"></asp:Literal></td>
                                <td><asp:Literal ID="ltlExtention2" runat="server"></asp:Literal></td>
                                <td>
                                    <asp:HyperLink ID="ltlName2" CssClass="product-name" runat="server" Target="_blank"></asp:HyperLink>
                                </td>
                                <td><asp:Literal ID="ltlTime2" runat="server"></asp:Literal></td>
                                <td>   
                                    <p class="<%= ltlEndTime.Text.ToDateTime() < DateTime.Now ? "hide" : "" %>">
                                        <a id="lbnEidt2" class="con_my_opus_edit" href="javascript:showLayerWindow('上传活动作品','Production.aspx?ID=<%=SiginUpID %>&ProductID=<%=hfID2.Value %>', 730, 400)">修改</a>
                                        <cc1:CustomButton ID="btnDelete2" runat="server" OnClick="btnDelete2_Click" CssClass="con_my_opus_del" ConfirmMessage="确认删除吗？" EnableConfirm="true" ConfirmTitle="删除提示" Text="删除" />
                                        <asp:HiddenField ID="hfID2" runat="server" /> 
                                    </p>                                           
                                </td>
                            </tr>
                        </tbody>
                        <tfoot class="<%= CommitCount > 1 ? "hide":"" %>">
                            <tr class="activity-upload-row">
                                <td colspan="5"><a id="lbnUpload" class="con_my_act_upload" href="javascript:showLayerWindow('上传活动作品','Production.aspx?ID=<%=SiginUpID %>', 730, 400)">上传作品</a></td>
                            </tr>
                        </tfoot>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <script>

    </script>
    <script src="<%=ETMS.Utility.WebUtility.AppPath %>/scripts/ymPromptYuan.js"></script>
</asp:Content>
