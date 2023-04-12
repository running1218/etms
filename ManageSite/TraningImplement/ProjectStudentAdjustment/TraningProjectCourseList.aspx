<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="TraningProjectCourseList.aspx.cs" Inherits="TraningImplement_ProjectStudentAdjustment_TraningProjectCourseList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    </div>
    <div class="dv_information">
        <!--查找结果-->
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel" id="divPage1">
                <div class="dv_selectAll">
                    <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" />
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" Visible="false" />
                    总记录数：<span style="color: #C03218"><asp:Label ID="labDataCount" runat="server" Text="0"></asp:Label></span>
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
                CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0"
                DataKeyNames="TrainingItemCourseID">
                <Columns>
                    <asp:TemplateField HeaderText="">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" Width="20" />
                        <ItemTemplate>
                           <asp:CheckBox ID="CheckBox1" runat="server" CssClass="cssName" Text='<%# Eval("TrainingItemCourseID")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="CourseCode" HeaderText="课程编码" ItemStyle-CssClass="alignleft"
                        HeaderStyle-CssClass="alignleft" />
                    <asp:TemplateField HeaderText="课程名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="10" Text='<%# Eval("CourseName")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程类型" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="DictionaryLabel0" DictionaryType="Dic_Sys_CourseType" FieldIDValue='<%# Eval("CourseTypeID") %>'
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="授课方式" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="DictionaryLabel1" DictionaryType="Dic_Sys_TeachModel" FieldIDValue='<%# Eval("TeachModelID") %>'
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="开始日期" HeaderStyle-Width="70">
                        <ItemTemplate>
                            <%# Eval("CourseBeginTime").ToDate()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="截止日期" HeaderStyle-Width="70">
                        <ItemTemplate>
                            <%# Eval("CourseEndTime").ToDate()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="状态" HeaderStyle-Width="40">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblCourseStatus" DictionaryType="Dic_Status" FieldIDValue='<%# Eval("CourseStatus") %>'
                                runat="server" />
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
        <input id="btnSave" type="button" class="btn_Save" value="确定" />
        <input id="btnClose" type="button" class="btn_Cancel" value="取消" /></div>
    <script type="text/javascript">
        $(document).ready(function () {
            $(parent.document).find(".ymPrompt_close").hide();

            $("#btnClose").click(function () {
                $(window.parent.document).find(".ItemCourseIDs").html("");
                $(window.parent.document).find(".btnAddHid").click();
            });

            $("#btnSave").click(function () {
                var ItemCourseIDs = "";
                var arrRe = $("input[type='checkbox']:checked");
                $(arrRe).each(function () {
                    if ($("label[for^='" + this.id + "']").html() != "全选" && $("label[for^='" + this.id + "']").html() != null) {
                        ItemCourseIDs += $("label[for^='" + this.id + "']").html() + ",";
                    }
                });
                //alert(ItemCourseIDs);
                $(window.parent.document).find(".ItemCourseIDs").html(ItemCourseIDs);
                $(window.parent.document).find(".btnAddHid").click();
            });
            $("label[for^='ctl00_ContentPlaceHolder1_CustomGridView1']").css("display", "none");
        });
    </script>
</asp:Content>
