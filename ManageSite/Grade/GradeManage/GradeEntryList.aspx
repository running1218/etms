<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="GradeEntryList.aspx.cs" Inherits="Grade_GradeManage_GradeEntryList" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
    <!--导航路径-->
    <div class="dv_path" id="dv_path">
        当前位置：测评系统&gt;&gt;考试与成绩&gt;&gt;<asp:Literal ID="Literal7" runat="server" Text="成绩管理"></asp:Literal>
    </div>
    <!--功能标题-->
    <h2 class="dv_title">
        成绩管理
    </h2>
    <!--查找条件-->
    <div class="dv_searchbox">
        <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" id="tableQueryControlList" runat="server">
            <tr>
                <th width="120">
                    项目编码：
                </th>
                <td  width="125">
                    <asp:TextBox ID="txt_Tr_Item999ItemCode" runat="server" CssClass="inputbox_120"></asp:TextBox>
                </td>
                <th  width="120">
                    项目名称：
                </th>
                <td  width="125">
                    <asp:TextBox ID="txt_Tr_Item999ItemName" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    <asp:Button ID="Button1" runat="server" CssClass="btn_Search" Text="查询" OnClick="btnSearch_Click" />   
                    <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a> 
                </td>  
            </tr>
            <tr>
                <th>
                    课程编码：
                </th>
                <td>
                    <asp:TextBox ID="txt_Res_Course999CourseCode" runat="server" CssClass="inputbox_120"></asp:TextBox>
                </td>
                <th>
                    课程名称：
                </th>
                <td>
                    <asp:TextBox ID="txt_Res_Course999CourseName" runat="server" CssClass="inputbox_120"></asp:TextBox>
                </td>
               <%-- <th  width="120">
                    已发布成绩：
                </th>
                <td>
                    <cc1:DictionaryDropDownList runat="server" ID="ddl_Tr_ItemCourse999IsIssueGrade" DictionaryType="Dic_TrueOrFalse" />
                </td>--%>
            </tr>           
        </table>
    </div>
    <!--查找结果-->
    <div class="dv_searchlist">
        <asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <!--翻页-->
                <div class="dv_pagePanel">
                    <div class="dv_selectAll" id="dv_select" runat="server">
                       <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" Visible="false" />
                     
                    </div>
                    <div class="dv_pageControl">
                        <uc2:PageSet ID="PageSet1" runat="server" />
                    </div>
                </div>
                <!--列表 begin-->
                <cc1:CustomGridView ID="CustomGridView1" runat="server" CssClass="GridviewGray" EmptyDataSourceTip="没有任何记录！"
                    AutoCreateColumnInsertIndex="0" AutoGenerateColumns="False" CurrentPageIndex="1"
                    OnRowDataBound="CustomGridView1_RowDataBound" CustomAllowPaging="False" IsEmpty="False">
                    <Columns>                       
                        <asp:TemplateField HeaderText="项目编码" >
                            <ItemStyle HorizontalAlign="Center" CssClass="alignleft"  />
                            <HeaderStyle HorizontalAlign="Center" CssClass="alignleft "  />
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblItemCode" runat="server" ShowTextNum="50" Text='<%# Eval("ItemCode")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="项目名称">
                            <ItemStyle HorizontalAlign="Center" CssClass="alignleft" />
                            <HeaderStyle HorizontalAlign="Center" CssClass="alignleft"  />
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblItemName" runat="server" ShowTextNum="50" Text='<%# Eval("ItemName")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="课程编码" >
                            <ItemStyle HorizontalAlign="Center" CssClass="alignleft"  />
                            <HeaderStyle HorizontalAlign="Center" CssClass="alignleft " />
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblCourseCode" runat="server" ShowTextNum="50" Text='<%# Eval("CourseCode")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="课程名称">
                            <ItemStyle HorizontalAlign="Center" CssClass="alignleft" />
                            <HeaderStyle HorizontalAlign="Center" CssClass="alignleft"  />
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="50" Text='<%# Eval("CourseName")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="授课方式" HeaderStyle-Width="60" >
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <cc1:DictionaryLabel ID="lblTeachModel" runat="server" DictionaryType="Dic_Sys_TeachModel"
                                    FieldIDValue='<%# Eval("TeachModelID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                        <asp:TemplateField HeaderText="测试数" HeaderStyle-Width="40" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                               <%# GetTrainingItemResourcesTotal(Eval("TrainingItemCourseID").ToGuid())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="学员数" HeaderStyle-Width="40" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright">
                            <ItemTemplate>
                                <asp:Label ID="lblStudentNum" runat="server" Text='<%#studentcourseLogic.GetItemCourseStudentNum(Eval("TrainingItemCourseID").ToGuid()) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="未录人数" HeaderStyle-Width="60" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright">
                            <ItemTemplate>
                                <asp:Label ID="lblInputGradeNumber" runat="server" Text='<%# GetUninputNumber(Eval("TrainingItemCourseID").ToGuid())  %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%-- <asp:TemplateField HeaderText="已发布">
                            <ItemTemplate>
                                <asp:Label ID="lblIsInputGrade" runat="server" Text='<%# Convert.ToBoolean(Eval("IsIssueGrade"))?"是":"否" %>' />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="操作" HeaderStyle-Width="120">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:LinkButton ID="lbnInput" runat="server" Text="录入成绩" CommandName="input" />
                                <asp:LinkButton ID="lbnImport" runat="server" Text="导入成绩" CommandName="import" />                                                      </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </cc1:CustomGridView>
                <!--列表 end-->
                <div class="dv_splitLine">
                </div>
                <!--翻页-->
                <div class="dv_pagePanel">
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>

</asp:Content>
