<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ChooseCourse.ascx.cs"
    Inherits="StudyMap_ChooseCourse" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<!--查找条件-->
<div class="dv_searchbox">
    <table class="GridviewGray th100" border="0" cellpadding="0" cellspacing="0" runat="server"
        id="tableQueryControlList">
        <tr>
            <th style="width:100px;">
                课程编码：
            </th>
            <td style="width:150px;">
                <asp:TextBox ID="txt_CourseCode" runat="server" CssClass="inputbox_120"></asp:TextBox>
            </td>
            <th style="width:100px;">
                课程名称：
            </th>
            <td class="Search_Area">
                <asp:TextBox ID="txt_CourseName" runat="server" CssClass="inputbox_160 floatleft"></asp:TextBox>
               <%-- <asp:UpdatePanel ID="upQuery" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>--%>
                        <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
            <%--        </ContentTemplate>
                </asp:UpdatePanel>--%>
                <a href="javascript:hideGridview()" class="dropdownico " id="Highsearch">高级搜索</a>
            </td>
        </tr>
        <tr>
            <th>
                课程类型：
            </th>
            <td>
                <cc1:DictionaryDropDownList runat="server" ID="ddl_CourseTypeID" DictionaryType="Dic_Sys_CourseType"
                    CssText="select_120" />
            </td>
            <th>
                课程等级：
            </th>
            <td>
                <cc1:DictionaryDropDownList runat="server" ID="ddl_CourseLevelID" DictionaryType="Dic_Sys_CourseLevel"
                    CssText="select_150" />
            </td>
        </tr>
    </table>
</div>
<div class="dv_searchlist">
    <%--<asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>
            <!--翻页-->
            <div class="dv_pagePanel" id="divPage1">
                <div class="dv_selectAll">
                    <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" />
                    <asp:Button ID="LinkButton1" runat="server" CssClass="btn_Ok" OnClick="lbtnSave_Click" Text="确定" />
                </div>
                <div class="dv_pageControl" style="float: right;">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" 
        CssClass="GridviewGray" EmptyDataSourceTip="没有任何记录！"
                AutoCreateColumnInsertIndex="0" AutoGenerateColumns="False" CurrentPageIndex="1"
                CustomAllowPaging="False" IsEmpty="False" TotalRecordCount="0" 
        DataKeyNames="CourseID" onrowdatabound="CustomGridView1_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="">
                        <ItemStyle HorizontalAlign="Center"  />
                        <HeaderStyle HorizontalAlign="Center" Width="20" />
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="CourseCode"  HeaderStyle-CssClass="alignleft field12" ItemStyle-CssClass="alignleft" HeaderText="课程编码" SortExpression="CourseCode" />
                    <asp:TemplateField HeaderText="课程名称" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="10" Text='<%# Eval("CourseName")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程状态" HeaderStyle-Width="80px">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblCourseStatus" DictionaryType="Dic_Status" FieldIDValue='<%# Eval("CourseStatus") %>'
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField > 
                    <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-CssClass="visibleT" HeaderText="学习纲要">
                        <ItemTemplate>
                            <a style="position:relative" class="studyhover"><div class="studymessgebox" style="display:none;"><%# Eval("CourseOutline") %></div>学习纲要</a>                            
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="预计时长" HeaderStyle-Width="80px" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright">
                        <ItemTemplate>
                            <%# Eval("CourseHours") %>
                        </ItemTemplate>
                    </asp:TemplateField>                   
                    <asp:TemplateField HeaderText="学习方式" HeaderStyle-Width="80px">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlStudyModel" CssClass="select_60" runat="server" DataSourceID="odsStudyModel" DataTextField="ColumnNameValue" DataValueField="ColumnCodeValue"></asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="实施人" HeaderStyle-Width="70px">
                        <ItemTemplate>
                            <asp:TextBox ID="txtActualMan" runat="server" CssClass="inputbox_60"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </cc1:CustomGridView>
            <!--列表 end-->
            <div class="dv_splitLine">
            </div>
            <div class="dv_pagePanel" id="divPage2">
            </div>
            <asp:ObjectDataSource ID="odsStudyModel" runat="server" 
                SelectMethod="GetCommonSysDictionary" 
                TypeName="ETMS.Components.Basic.Implement.BLL.Dictionary.SysDict">
                <SelectParameters>
                    <asp:Parameter DefaultValue="Dic_Sys_StudyModel" Name="tableName" 
                        Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <script language="javascript">
                $(function () {
                    $(".studyhover").hover(function () {
                        showMessgebox(this);
                    }, function () {
                        $(".studymessgebox:visible").hide();
                    })
                })
                function showMessgebox(obj) {
                    $(".studymessgebox:visible").hide();
                    if ($(obj).find(".studymessgebox").html().length > 0) {
                        $(obj).find(".studymessgebox").show();
                        $(obj).find(".studymessgebox").css({ "left": $(obj).width() + "px", "top": "0px" });
                    }

                }
            </script>
   <%--     </ContentTemplate>
    </asp:UpdatePanel>--%>
</div>
<!--提交表单-->

