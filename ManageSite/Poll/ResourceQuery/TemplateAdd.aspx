<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageTree.Master" AutoEventWireup="true"
    CodeFile="TemplateAdd.aspx.cs" Inherits="Poll_ResourceQuery_TemplateAdd" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <div class="dv_information dv_marist">
    <div class="dv_searchlist">
        <!--翻页-->
        <div class="dv_pagePanel">
            <div class="dv_pageControl">
                <uc2:PageSet ID="PageSet1" runat="server" />
            </div>
        </div>
        <cc1:CustomGridView ID="GridViewList" runat="server" AutoGenerateColumns="False"
            DataKeyNames="QueryID">
            <Columns>
                <asp:TemplateField HeaderStyle-Width="20">
                    <ItemTemplate>
                        <asp:RadioButton runat="server" ID="rbIsTemplateSelected" GroupName="templateQuery"
                            onclick='SetRadioName(this)' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="调查模板名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <cc1:ShortTextLabel ID="ShortTextLabel1" runat="server" Text='<%#Eval("QueryName") %>'
                            ShowTextNum="40"></cc1:ShortTextLabel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作" HeaderStyle-Width="40">
                    <ItemTemplate>
                        <a href='<%# this.ActionHref(String.Format("../QueryPreView.aspx?QueryID={0}", new Object[]{Eval("QueryID")})) %>'
                            target="QueryPreView">预览</a>
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
    </div>
    </div>
    <!--提交表单-->
    <div class="dv_submit">
        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn_Save" ValidationGroup="Edit"
            OnClick="LinkButton1_Click">保存</asp:LinkButton>
        <a href="javascript:closeWindow();" class="btn_Cancel padleft10">取消</a>
    </div>
    <script type="text/javascript">
        function SetRadioName(obj) {
            var gv = document.getElementById('<%=this.GridViewList.ClientID%>'); //获取GridView的客户端ID
            var myradio = gv.getElementsByTagName("input"); //获取GridView的Inputhtml
            for (var i = 0; i < myradio.length; i++) {
                if (myradio[i].type == 'radio')//hidden
                {
                    myradio[i].checked = false;
                }
            }
            obj.checked = true;
        }
    </script>
</asp:Content>
