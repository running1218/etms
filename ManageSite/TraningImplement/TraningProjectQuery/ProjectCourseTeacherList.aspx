<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="ProjectCourseTeacherList.aspx.cs" Inherits="TraningImplement_TraningProjectQuery_ProjectCourseTeacherList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--查找条件-->
    <div class="dv_information">
        
            <table class="GridviewGray fixedTable" >
                <tr>
                    <th>
                        项目编码：
                    </th>
                    <td>
                        <asp:Label ID="lblItemCode" runat="server"></asp:Label>
                    </td>
                    <th>
                        项目名称：
                    </th>
                    <td>
                        <asp:Label ID="lblItemName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        项目周期：
                    </th>
                    <td colspan="3">
                        <asp:Label ID="lblItemDate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        课程编码：
                    </th>
                    <td>
                        <asp:Label ID="lblCourseCode" runat="server"></asp:Label>
                    </td>
                    <th>
                        课程名称：
                    </th>
                    <td>
                        <asp:Label ID="lblCourseName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        课程周期：
                    </th>
                    <td colspan="3">
                        <asp:Label ID="lblCourseDate" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
                <!--查找结果-->
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel" id="divPage1">
                <div class="dv_selectAll">
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
                CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0"
                DataKeyNames="ItemCourseTeacherID">
                <Columns>
                    <asp:BoundField DataField="TeacherCode" HeaderText="讲师编码" HeaderStyle-Width="60" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft"/>
                    <asp:BoundField DataField="RealName" HeaderText="讲师姓名" HeaderStyle-Width="60" HeaderStyle-CssClass="alignleft"  ItemStyle-CssClass="alignleft"/>
                    <asp:TemplateField HeaderText="讲师等级" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblTeacherLevel" DictionaryType="Dic_Sys_TeacherLevel" FieldIDValue='<%# Eval("TeacherLevelID") %>'
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="讲师状态" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblStatus" DictionaryType="Dic_Status" FieldIDValue='<%# Eval("Status") %>'
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="MobilePhone" HeaderText="手机" HeaderStyle-Width="80" HeaderStyle-CssClass="alignleft"  ItemStyle-CssClass="alignleft"/>
                    <asp:TemplateField HeaderText="邮箱"  HeaderStyle-CssClass="alignleft"  ItemStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblEmail" runat="server" ShowTextNum="12" Text='<%# Eval("Email")%>'></cc1:ShortTextLabel>
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
        </div>
    </div>
    <div class="dv_submit">
        <input type="button" class="btn_Close" onclick="javascript:closeWindow();" value="关闭" />
    </div>
</asp:Content>
