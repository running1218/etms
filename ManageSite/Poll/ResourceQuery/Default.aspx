<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Poll_ResourceQuery_Default"
    MasterPageFile="~/MasterPages/MPageAdmin.master" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content runat="server" ID="PageContent" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="dv_searchbox">
        <table class="GridviewGray th70">
            <tr>
                <th>
                    ��������:
                </th>
                <td colspan="3">
                    <cc1:CustomTextBox ButtonControlId="btn_Search" ID="txtQueryName" runat="server"
                        CssClass="inputbox_300"></cc1:CustomTextBox>
                    <asp:Button ID="btn_Search" runat="server" Text="��ѯ" OnClick="btn_Search_Click" SkinID="Search" />
                    <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">�߼�����</a>
                </td>
            </tr>
            <tr>
                <th>
                    �鿴���:
                </th>
                <td>
                    <cc1:DictionaryDropDownList runat="server" ID="ddlIsDiplayResult" DictionaryType="Dic_TrueOrFalse"
                        IsShowAll="true" />
                </td>
                <th>
                    �Ƿ�ģ��:
                </th>
                <td>
                    <cc1:DictionaryDropDownList runat="server" ID="ddlIsTemplate" DictionaryType="Dic_TrueOrFalse" />
                </td>
            </tr>
            <tr>
                <th>
                    �ʾ�״̬:
                </th>
                <td>
                    <cc1:DictionaryDropDownList runat="server" ID="ddlIsStatus" DictionaryType="Dic_Status" />
                </td>
                <th>
                    ����״̬:
                </th>
                <td>
                    <cc1:DictionaryDropDownList runat="server" ID="ddlIsPublish" DictionaryType="Dic_PublishStatus"
                        IsShowAll="true" />
                </td>
            </tr>
            <tr>
                <th>
                    ����ʱ��:
                </th>
                <td>
                    <cc1:DateTimeTextBox ID="txtCreateBeginTime" runat="server" EndTimeControlID="txtCreateEndTime"></cc1:DateTimeTextBox>��<cc1:DateTimeTextBox
                        ID="txtCreateEndTime" runat="server" MaxLength="10" BeginTimeControlID="txtCreateBeginTime"></cc1:DateTimeTextBox>
                </td>
                <th>
                    ����ʱ��:
                </th>
                <td>
                    <cc1:DateTimeTextBox ID="txtQueryBeginTime" runat="server" EndTimeControlID="txtQueryEndTime"></cc1:DateTimeTextBox>��<cc1:DateTimeTextBox
                        ID="txtQueryEndTime" runat="server" BeginTimeControlID="txtQueryBeginTime"></cc1:DateTimeTextBox>
                </td>
            </tr>
        </table>
        <div class="center">
        </div>
    </div>
    <div class="dv_searchlist">
        <!--��ҳ-->
        <div class="dv_pagePanel">
            <div class="dv_selectAll">
                <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="GridViewList" />
                <input type="button" class="btn_Add" value="����" onclick="showWindow('���������ʾ�','<%=this.ActionHref(string.Format("View.aspx?op=add&id=0&ResourceType={0}&ResourceCode={1}",this.ResourceType,this.ResourceCode))%>',650,500)" />
                <input type="button" class="btn_Addmodel" value="��ģ������" onclick="showWindow('��ģ�����������ʾ�','<%=this.ActionHref(string.Format("TemplateAdd.aspx?ResourceType={0}&ResourceCode={1}",this.ResourceType,this.ResourceCode))%>',650,500)" />
                <cc1:CustomButton CssClass="btn_Deploy" Text="����" runat="server" ID="btnPublish"
                    EnableConfirm="true" ConfirmMessage="ȷ��Ҫ������?" OnClick="btnPublish_Click" />
                <cc1:CustomButton CssClass="btn_Del" Text="ɾ��" runat="server" ID="btnDelete" EnableConfirm="true"
                    ConfirmMessage="ȷ��Ҫɾ����?" OnClick="btnDelete_Click" />
            </div>
            <div class="dv_pageControl">
                <uc2:PageSet ID="PageSet1" runat="server" />
            </div>
        </div>
        <cc1:CustomGridView ID="GridViewList" runat="server" AutoGenerateColumns="False"
            OnRowDataBound="GridViewList_RowDataBound" DataKeyNames="QueryID">
            <Columns>
                <asp:TemplateField HeaderStyle-Width="18">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" Enabled='<%#!(bool)Eval("IsPublish")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="��������" HeaderStyle-CssClass="alignleft">
                    <ItemStyle CssClass="alignleft" />
                    <ItemTemplate>
                        <cc1:ShortTextLabel runat="server" Text='<%#Eval("QueryName") %>'></cc1:ShortTextLabel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="����ʱ��" HeaderStyle-Width="140">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <cc1:DateTimeLabel runat="server" DateTimeValue=' <%#Eval("BeginTime")%>'></cc1:DateTimeLabel>~
                        <cc1:DateTimeLabel runat="server" DateTimeValue=' <%#Eval("EndTime")%>'></cc1:DateTimeLabel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="�ʾ�״̬" HeaderStyle-Width="60">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <cc1:DictionaryLabel runat="server" DictionaryType="Dic_Status" FieldIDValue='<%#Eval("Status") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="����״̬" HeaderStyle-Width="60">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <cc1:DictionaryLabel runat="server" DictionaryType="Dic_PublishStatus" FieldIDValue='<%#Convert.ToInt32(Eval("IsPublish")) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="����������" HeaderStyle-CssClass="alignleft" HeaderStyle-Width="100">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <cc1:ShortTextLabel runat="server" ID="lblTotalCount" Text='<%#Eval("InvestNumber") %>'></cc1:ShortTextLabel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="�鿴���" HeaderStyle-Width="60">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <cc1:DictionaryLabel runat="server" DictionaryType="Dic_TrueOrFalseBool" FieldIDValue='<%#Eval("IsDisplayResult")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ģ��" HeaderStyle-Width="40">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <cc1:DictionaryLabel runat="server" DictionaryType="Dic_TrueOrFalseBool" FieldIDValue='<%#Eval("IsTemplate")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False" HeaderText="����" HeaderStyle-Width="170">
                    <ItemTemplate>
                        <%--<a href='javascript:showWindow("�Ծ���Ŀ����","<%#this.ActionHref(string.Format("../QuestionManage/QuestionSort.aspx?QueryID={0}", Eval("QueryID"))) %>")'>
                            ��Ŀ����</a>--%>
                        <a href='javascript:showWindow("�༭�����ʾ�","<%# this.ActionHref(String.Format("View.aspx?op=edit&id={0}", new Object[]{Eval("QueryID")})) %>",650,500)'>
                            �༭�ʾ�</a> <a href='javascript:showWindow("�鿴�����ʾ�","<%# this.ActionHref(String.Format("View.aspx?op=view&id={0}", new Object[]{Eval("QueryID")})) %>",650,500)'>
                                �鿴�ʾ�</a> <a href='<%# this.ActionHref(String.Format("../QueryPreView.aspx?QueryID={0}", new Object[]{Eval("QueryID")})) %>'
                                    target="QueryPreView">Ԥ���ʾ�</a><br />
                        <%--      <a href='<%#  %>'>
                            ��Ŀ����</a>--%>
                        <asp:HyperLink runat="server" ID="HyperLink1" Text="��Ŀ����"></asp:HyperLink>
                        <asp:HyperLink runat="server" ID="hySetArea" Text="���鷶Χ"></asp:HyperLink>
                        <cc1:CustomLinkButton runat="server" ID="lkSwitchStatus" Text='<%#(((bool)Eval("IsTemplate"))?"ȡ��ģ��":"��Ϊģ��") %>'
                            OnCommand="UserOpeator_Command" EnableConfirm="true" ConfirmMessage='<%#string.Format("ȷ��Ҫ{0}��",(((bool)Eval("IsTemplate"))?"ȡ��ģ��":"��Ϊģ��")) %>'
                            CommandName="SwitchTemplate" CommandArgument='<%#Eval("QueryID") %>'></cc1:CustomLinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </cc1:CustomGridView>
        <!--�б� end-->
        <div class="dv_splitLine">
        </div>
        <!--��ҳ-->
        <div class="dv_pagePanel">
        </div>
    </div>
</asp:Content>
