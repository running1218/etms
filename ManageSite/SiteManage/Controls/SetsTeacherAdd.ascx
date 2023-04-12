<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SetsTeacherAdd.ascx.cs" Inherits="SiteManage_Controls_SetsTeacherAdd" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<div>
    <!--查找条件-->
    <div class="dv_searchbox" runat="server" id="div_Query">
        <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
            id="tableQueryControlList">
            <tr>
                <th width="120">讲师名称：
                </th>
                <td width="120">
                    <asp:textbox id="txt_RealName" runat="server" cssclass="inputbox_120"></asp:textbox>
                </td>
                <th width="100">讲师类型：
                </th>
                <td>
                    <cc1:DictionaryDropDownList runat="server" ID="ddl_TeacherSourceID" DictionaryType="Dic_Sys_TeacherSource"/>
                    <asp:button id="btnSearch" cssclass="btn_Search" runat="server" text="查询" onclick="btnSearch_Click" />
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
                <asp:validationsummary runat="server" id="ValidationSummary1" validationgroup="Saves"
                    showmessagebox="true" showsummary="false" />
                <asp:button id="btnAdd" runat="server" text="确定" cssclass="btn_Ok" onclick="btnAdd_Click"
                    validationgroup="Saves" />
            </div>
            <div class="dv_pageControl">
                <uc2:PageSet ID="PageSet1" runat="server" />
            </div>
        </div>
        <!--列表 begin-->
        <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
            CustomAllowPaging="false" ShowFooter="false" IsRemeberChecks="true" AutoCreateColumnInsertIndex="0"
            DataKeyNames="TeacherID">
            <Columns>
                <asp:templatefield headertext="">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="20" />
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                </asp:templatefield>
                <asp:boundfield datafield="RealName" headertext="讲师姓名" headerstyle-cssclass="alignleft"
                    itemstyle-cssclass="alignleft" />
                <asp:templatefield headertext="讲师类型" headerstyle-width="60">
                <ItemTemplate>
                    <cc1:DictionaryLabel ID="lblTeacherSource" DictionaryType="Dic_Sys_TeacherSource"
                        FieldIDValue='<%# Eval("TeacherSourceID") %>' runat="server" />
                </ItemTemplate>
            </asp:templatefield>
                <asp:templatefield headertext="讲师级别" headerstyle-width="60">
                <ItemTemplate>
                    <cc1:DictionaryLabel ID="lblTeacherLevel" DictionaryType="Dic_Sys_TeacherLevel" FieldIDValue='<%# Eval("TeacherLevelID") %>'
                        runat="server" />
                </ItemTemplate>
            </asp:templatefield>
                <%-- <asp:TemplateField HeaderText="操作" HeaderStyle-Width="150">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <a href="javascript:showWindow('查看','<%# this.ActionHref(string.Format("{0}", Eval("CourseID").ToString()))%>','800','600')">查看</a>
                    </ItemTemplate>
                </asp:TemplateField>--%>
            </Columns>
        </cc1:CustomGridView>
        <!--列表 end-->
        <div class="dv_splitLine">
        </div>
        <!--翻页-->
        <div class="dv_pagePanel" id="divPage2">
        </div>
    </div>
</div>
