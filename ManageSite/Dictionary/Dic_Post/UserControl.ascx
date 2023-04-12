<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserControl.ascx.cs" Inherits="Dic_Post_UserControl" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<cc1:CustomMuliView runat="server" ID="Views">
    <asp:View runat="server" ID="View_Manager">
        <div class="dv_searchbox">
            <table class="GridviewGray th80">
                <tr>
                    <th width="100" >
                        <asp:Literal ID="ltlPosition" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>编码：
                    </th>
                    <td width="165">
                        <asp:TextBox ID="txtPostCode_Query" runat="server" CssClass="inputbox_160"></asp:TextBox>
                    </td>
                    <th width="120">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>名称：
                    </th>
                    <td>
                        <asp:TextBox ID="txtPostName_Query" runat="server" CssClass="inputbox_160"></asp:TextBox>
                        <asp:Button ID="btn_Search" runat="server" Text="查询" CssClass="btn_Search" OnClick="btn_Search_Click" /><a
                            href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>类别：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="ddlPostType_Query" DictionaryType="Dic_SYS_PostType"
                            IsShowAll="true">
                        </cc1:DictionaryDropDownList>
                    </td>
                    <th>
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>状态：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="ddlStatus_Query" DictionaryType="Dic_Status"
                            IsShowAll="true">
                        </cc1:DictionaryDropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel">
                <div class="dv_selectAll">
                   <%-- <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="GridViewList" />--%>
                    <input type="button" class="btn_Add" value="新增" onclick="javascript:showWindow('新增信息','<%=this.ActionHref("View.aspx?op=add&id=0")%>')" />
                    <%--   <cc1:CustomButton CssClass="btn_Del" Text="删除" runat="server" ID="btnDeletes" EnableConfirm="true"
                        ConfirmMessage="确信要执行“批量删除”操作吗?" OnClick="btnDeletes_Click" />--%>
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <cc1:CustomGridView ID="GridViewList" runat="server" AutoGenerateColumns="False"
                DataKeyNames="PostID">
                <Columns>
                   <%-- <asp:TemplateField HeaderText="" >
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" Width="18" />
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft field12">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <HeaderTemplate>
                            <asp:Literal ID="ltlPosition" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>编码
                        </HeaderTemplate>
                        <ItemTemplate>                            
                            <asp:Label ID="lblPostCode1" runat="server" Text='<%# Eval("PostCode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <HeaderTemplate>
                            <asp:Literal ID="ltlPositionName" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>名称
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblPostName" runat="server" Text='<%# Eval("PostName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-Width="60">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderTemplate>
                            <asp:Literal ID="ltlPosition" runat="server" Text='<%$ Resources:UIResource, ui_position%>'></asp:Literal>类别
                        </HeaderTemplate>
                        <ItemTemplate>
                            <cc1:DictionaryLabel runat="server" ID="LabPostTypeCode" DictionaryType="Dic_SYS_PostType"
                                FieldIDValue='<%#Eval("PostTypeID") %>' IsShowAll="true" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="状态" HeaderStyle-Width="60">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <cc1:DictionaryLabel runat="server" ID="lblStatus" DictionaryType="Dic_Status" FieldIDValue='<%#Eval("Status") %>'
                                IsShowAll="true" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作" HeaderStyle-Width="120">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <a href="javascript:showWindow('编辑','<%# this.ActionHref(String.Format("View.aspx?op=edit&id={0}", new Object[]{Eval("PostID")})) %>')">
                                编辑</a> <a href="javascript:showWindow('删除','<%# this.ActionHref(String.Format("View.aspx?op=delete&id={0}", new Object[]{Eval("PostID")})) %>',600,330)">
                                    删除</a> <a href="javascript:showWindow('查看','<%# this.ActionHref(String.Format("View.aspx?op=view&id={0}", new Object[]{Eval("PostID")})) %>',600,330)">
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
        </div>
    </asp:View>
    <asp:View runat="server" ID="View_Edit">
        <div class="dv_information">
            <table class="GridviewGray">
                <tr>
                    <th>
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>编码：
                    </th>
                    <td>
                        <asp:TextBox ID="txtPostCode" runat="server" CssClass="inputbox_210" MaxLength="10"></asp:TextBox>
                        <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidatorPostCode"
                            runat="server" ErrorMessage="请填写编码！" ControlToValidate="txtPostCode" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>名称：
                    </th>
                    <td>
                        <asp:TextBox ID="txtPostName" runat="server" CssClass="inputbox_210" MaxLength="50"></asp:TextBox>
                        <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidatorPostName"
                            runat="server" ErrorMessage="请填写名称！" ControlToValidate="txtPostName" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>类别：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="ddlPostTypeCode" DictionaryType="Dic_SYS_PostType"
                            IsShowChoose="true">
                        </cc1:DictionaryDropDownList>
                        <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidatorPostTypeCode"
                            runat="server" ErrorMessage="请填写类别！" ControlToValidate="ddlPostTypeCode" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <th>
                        状&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;态：
                    </th>
                    <td>
                        <cc1:DictionaryRadioButtonList runat="server" ID="rbStatus" DictionaryType="Dic_Status"
                            CheckedValue="1" />
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>描述：
                    </th>
                    <td>
                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" SkinID="textarea"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>职责：
                    </th>
                    <td>
                        <asp:TextBox ID="txtLiability" runat="server" TextMode="MultiLine" SkinID="textarea"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
    </asp:View>
    <asp:View runat="server" ID="View_Browse">
        <div class="dv_information">
            <table class="GridviewGray th120">
                <tr>
                    <th>
                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>编码：
                    </th>
                    <td>
                        <asp:Label ID="lblPostCode" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>名称：
                    </th>
                    <td>
                        <asp:Label ID="lblPostName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>类别：
                    </th>
                    <td>
                        <cc1:DictionaryLabel runat="server" ID="lblPostTypeCode" DictionaryType="Dic_SYS_PostType"
                            IsShowAll="true" />
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>描述：
                    </th>
                    <td>
                        <asp:Label ID="lblDescription" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>职责：
                    </th>
                    <td>
                        <asp:Label ID="lblLiability" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        状态：
                    </th>
                    <td>
                        <cc1:DictionaryLabel runat="server" ID="lblStatus" DictionaryType="Dic_Status" IsShowAll="true" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:View>
</cc1:CustomMuliView>
