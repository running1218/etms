<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="MapCourseList.aspx.cs" Inherits="Resource_ElearningMap_MapCourseList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<%@ Register Src="Controls/ElearningMapInfoView.ascx" TagName="ElearningMapInfoView"
    TagPrefix="uc3" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" runat="Server">
    <a href="ElearningMapList.aspx" class="btn_Return">返回</a>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--学习地图信息-->
    <uc3:ElearningMapInfoView ID="ElearningMapInfoView1" runat="server" />
    <!--课程列表-->
    <div class="dv_searchlist">       
        <%--<asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional">
            <ContentTemplate>--%>
                <!--翻页-->
                <div class="dv_pagePanel">
                    <div class="dv_selectAll">
                        <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="gvList" />
                        <asp:Button ID="lbtAdd" CssClass="btn_Add" runat="server" Text="新增"></asp:Button>
                        <cc1:CustomButton CssClass="btn_Del" Text="删除" runat="server" ID="btnDeletes" EnableConfirm="true"
                            ConfirmMessage="确定要删除吗?" OnClick="btnDeletes_Click" />
                    </div>
                    <div class="dv_pageControl">
                        <uc2:PageSet ID="PageSet1" runat="server" />
                    </div>
                </div>
                <!--列表 begin-->
                <cc1:CustomGridView ID="gvList" runat="server" CssClass="GridviewGray" EmptyDataSourceTip="没有任何记录！"
                    AutoCreateColumnInsertIndex="0" AutoGenerateColumns="False" CurrentPageIndex="1"
                    CustomAllowPaging="False" IsEmpty="False" TotalRecordCount="0" DataKeyNames="StudyMapReferCourseID"
                    OnRowUpdating="gvList_RowUpdating" OnRowEditing="gvList_RowEditing"
                    OnRowCancelingEdit="gvList_RowCancelingEdit">
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <ItemStyle HorizontalAlign="Center"  />
                            <HeaderStyle HorizontalAlign="Center" Width="20"/>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="课程编码"  HeaderStyle-CssClass="alignleft field12"  ItemStyle-CssClass="alignleft">
                            <ItemTemplate>
                                <%# Eval("CourseCode")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <%# Eval("CourseCode")%>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="课程名称" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="10" Text='<%# Eval("CourseName")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="10" Text='<%# Eval("CourseName")%>'></cc1:ShortTextLabel>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="课程状态" HeaderStyle-Width="80">
                            <ItemTemplate>
                                <cc1:DictionaryLabel ID="lblCourseStatus" DictionaryType="Dic_Status" FieldIDValue='<%# Eval("CourseStatus") %>'
                                    runat="server" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <cc1:DictionaryLabel ID="lblCourseStatus" DictionaryType="Dic_Status" FieldIDValue='<%# Eval("CourseStatus") %>'
                                    runat="server" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="学习纲要" HeaderStyle-Width="80px" ItemStyle-CssClass="visibleT">
                            <ItemTemplate>
                                <div style="position: relative" class="studyhover">
                                    <div class="studymessgebox" style="display: none;"><%# Eval("CourseOutline") %></div>
                                    <a href="#">学习纲要</a>
                                </div>                                                                                            
                            </ItemTemplate>                                                       
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="学习方式" HeaderStyle-Width="80px">
                            <ItemTemplate>
                                <cc1:DictionaryLabel ID="lblStudyModel" DictionaryType="Dic_Sys_StudyModel" FieldIDValue='<%# Eval("StudyModelID") %>'
                                    runat="server" />
                            </ItemTemplate>
                            <EditItemTemplate>                                
                                <asp:DropDownList ID="ddlStudyModel" CssClass="select_60" runat="server" DataSourceID="odsStudyModel"
                                    DataTextField="ColumnNameValue" DataValueField="ColumnCodeValue" SelectedValue='<%# Eval("StudyModelID") %>'>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="实施人" HeaderStyle-Width="70px">
                            <ItemTemplate>
                                <asp:Label ID="lblActualMan" runat="server" Text='<%# Eval("ChargeMan") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtActualMan" runat="server" CssClass="inputbox_60" Text='<%# Eval("ChargeMan") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="操作" HeaderStyle-Width="80px">
                            <ItemTemplate>
                                <asp:LinkButton ID="lblEdit" runat="server" Text="编辑" CommandName="edit"></asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="lbnSave" runat="server" CommandName="Update" CssClass="btn_savedata" Text="保存"></asp:LinkButton>
                                <asp:LinkButton ID="lbnCancel" runat="server" CommandName="Cancel" CssClass="btn_canceldata" Text="取消"></asp:LinkButton>
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </cc1:CustomGridView>
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
                <!--列表 end-->
                <div class="dv_splitLine">
                </div>
                <!--翻页-->
                <div class="dv_pagePanel">
                </div>
                <asp:ObjectDataSource ID="odsStudyModel" runat="server" SelectMethod="GetCommonSysDictionary"
                    TypeName="ETMS.Components.Basic.Implement.BLL.Dictionary.SysDict">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="Dic_Sys_StudyModel" Name="tableName" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
           <%-- </ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
</asp:Content>
