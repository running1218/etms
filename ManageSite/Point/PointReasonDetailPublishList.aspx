<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="PointReasonDetailPublishList.aspx.cs" Inherits="Point_ItemPointReasonDetailList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function showMessageBox(obj) {
             var num=0;
             $("#<%=CustomGridView1.ClientID %> input[type='checkbox']:checked").each(function () {
                num++;
             });
             if(num==0){
                popAlertMsg("请选择要发布的项目！", "提示");
                return false;
             }
            popConfirmMsgForControl(obj, '确定积分发布吗？', '提示');

            if (obj.isCalled == undefined)
                return false;
            else
                return true;
        }        
    </script>
    <div class="dv_searchbox">
        <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
            id="tableQueryControlList">
            <tr>
                <th  width="120">
                    项目名称：
                </th>
                <th  width="130">
                    <asp:DropDownList ID="ddl_ItemName" runat="server" />
                </th>
                <td class="Search_Area">
                    <asp:Button ID="btnSearch" runat="server" CssClass="btn_Search" Text="查询" OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div class="dv_searchlist">
        <!--翻页-->
        <div class="dv_pagePanel">
            <div class="dv_selectAll">
                <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" />
               <%-- <asp:Button ID="btnPublish" runat="server" Text="积分发布" CssClass="btn_integral" OnClick="btnPublish_Click" />--%>
                <cc1:CustomButton runat="server" ID="btnPublish" Text="积分发布" CssClass="btn_integral" EnableConfirm="false"
                                ConfirmTitle="提示" ConfirmMessage="确定积分发布吗？" OnClick="btnPublish_Click" OnClientClick="return showMessageBox(this);" />                 
            </div>
            <%-- <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
            </div>--%>
        </div>
        <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="False"
            DataKeyNames="TrainingItemID">
            <Columns>
                <asp:TemplateField HeaderText="">
                    <ItemStyle HorizontalAlign="Center" Width="40" />
                    <HeaderStyle HorizontalAlign="Center" Width="20" />
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="项目名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <asp:Label ID="lblItemName" runat="server" Text='<%#Eval("ItemName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="项目开始时间" HeaderStyle-Width="100">
                    <ItemTemplate>
                        <asp:Label ID="lblTrainBeginDate" runat="server" Text='<%#Eval("ItemBeginTime").ToDate()%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="项目结束时间" HeaderStyle-Width="100">
                    <ItemTemplate>
                        <asp:Label ID="lblTrainEndDate" runat="server" Text='<%#Eval("ItemEndTime").ToDate() %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="未发布记录数" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright" HeaderStyle-Width="90">
                    <ItemTemplate>
                        <asp:HyperLink ID="lblPoint" runat="server" Text='<%# Eval("StudentNum")%>' NavigateUrl='<%#this.ActionHref(string.Format("PointReasonDetailUnpublishList.aspx?TrainingItemID={0}",Eval("TrainingItemID"))) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="未发布总积分值" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright" HeaderStyle-Width="100">
                    <ItemTemplate>
                        <asp:Label ID="lblUnpublishPoint" runat="server" Text='<%#Eval("TotalPoints") %>' />
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
        <div>
            说明：<br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;列表显示项目下成绩已发布，积分已计算但未发布的课程列表。<br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;积分发布操作不可逆，发布后，学员课程积分将正式加入学员的总积分中，不能重新计算已发布的学员课程积分，请慎重操作！
        </div>
    </div>
</asp:Content>
