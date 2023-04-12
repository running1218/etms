<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="SetsCourse.aspx.cs" Inherits="TraningImplement_TraningProjectManager_SetsCourse" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" runat="Server">
    <asp:LinkButton ID="lbtnreturn" runat="server" Text="返回" CssClass="btn_Return" ></asp:LinkButton>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <th width="100">
                        项目编码：
                    </th>
                    <td width="300">
                        <asp:Label ID="Lbl_ItemCode" runat="server" Text=""></asp:Label>
                    </td>
                    <th width="100">
                        项目名称：
                    </th>
                    <td>
                        <asp:Label ID="Lbl_ItemName" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
            <div style="display: none">
                <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
            </div>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel" id="divPage1">
                <div class="dv_selectAll">
                    <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" />
                    <asp:Button ID="btnAdd" runat="server" CssClass="btn_Add" Text="新增" />
                    <input id="btnSort" type="button" class="btn_Sort" value="排序" onclick="javascript:showWindow('项目课程排序','<%= SortUrl %>',500,450)" />
                    <cc1:CustomButton runat="server" ID="cbtnDel" Text="删除" CssClass="btn_Del" EnableConfirm="true"
                        ConfirmTitle="提示" ConfirmMessage="确定删除吗？" OnClick="cbtnDel_Click" />
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
                CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0"
                DataKeyNames="TrainingItemCourseID" OnRowDataBound="CustomGridView1_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="">
                        <ItemStyle HorizontalAlign="Center"  />
                        <HeaderStyle HorizontalAlign="Center" Width="20" />
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="CourseCode" HeaderText="课程编码"  HeaderStyle-CssClass="field12 alignleft"  ItemStyle-CssClass="alignleft" />
                    <asp:TemplateField HeaderText="课程名称" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="50" Text='<%# Eval("CourseName")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程类型" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="DictionaryLabel0" DictionaryType="Dic_Sys_CourseType" FieldIDValue='<%# Eval("CourseTypeID") %>'
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程属性" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="DictionaryCourseAttr" DictionaryType="Dic_Sys_CourseAttr"
                                FieldIDValue='<%# Eval("CourseAttrID") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="授课方式" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="DictionaryLabel1" DictionaryType="Dic_Sys_TeachModel" FieldIDValue='<%# Eval("TeachModelID") %>'
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="培训开始日期" HeaderStyle-Width="80">
                        <ItemTemplate>
                            <%# Eval("CourseBeginTime").ToDate()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="培训截止日期" HeaderStyle-Width="80">
                        <ItemTemplate>
                            <%# Eval("CourseEndTime").ToDate()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="CourseHours" HeaderText="课程学时" HeaderStyle-Width="60"/>
                    <asp:TemplateField HeaderText="状态" HeaderStyle-Width="40">
                        <ItemTemplate>                            
                            <cc1:DictionaryLabel ID="lblCourseStatus" DictionaryType="Dic_Status" FieldIDValue='<%# Eval("CourseStatus") %>'
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="查看" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <asp:LinkButton ID="Lbtn_Edit" runat="server" CommandName="Edits">编辑</asp:LinkButton><asp:LinkButton
                                ID="lbtn_View" runat="server" CommandName="Views">查看</asp:LinkButton>
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
            <script type="text/javascript">
                function showPage(title, url) {
                    showWindow(title, url);
                    return false;
                }
            </script>
        </div>
    </div>
</asp:Content>
