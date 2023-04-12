<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ExerciseList.ascx.cs" Inherits="QuestionDB_ExOfflineHomework_Controls_ExerciseList" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/ChooseCourseDropdown.ascx" TagName="ChooseCourseDropdown"
    TagPrefix="uc1" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>


<div>
        <!--导航路径-->
        <div class="dv_path" id="dv_path">
            当前位置：资源管理系统&gt;&gt;学习资源管理&gt;&gt;离线作业管理
        </div>
        <!--功能标题-->
        <h2 class="dv_title">
            离线作业管理
        </h2>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <th>
                        课程名称：
                    </th>
                    <td>
                        <uc1:ChooseCourseDropdown ID="ChooseCourseDropdown1" runat="server" />
                 
                  
                        <span class="fontBold padleft10">作业名称：</span>
                   
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                              
                        
                       <span class="fontBold">状态：</span>
                  
                       <cc1:DictionaryDropDownList runat="server" ID="DictionaryDropDownList1" DictionaryType="Dic_Status" />
                        <input class="btn_Search" type="button" value="查询"/>
                    </td>
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel">
                <div class="dv_selectAll" id="dv_selectall" runat="server">
                    <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" />
                    <input type="button" class="btn_Add" value="新增" onclick="javascript:showWindow('新增离线作业','ExerciseAdd.aspx',600,320)" />
                    <input type="button" class="btn_Del" value="删除" onclick="popConfirmMsg('确信要删除么','提示','');" />
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="True"
                CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="1" OnRowDataBound="CustomGridView1_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="">
                        <ItemStyle HorizontalAlign="Center" Width="40" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="序号">
                        <ItemStyle HorizontalAlign="Center" Width="60" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="LabNo" runat="server" Text='<%#  ((this.PageSet1.PageIndex -1) * this.PageSet1.PageSize) + Container.DataItemIndex+1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                        <% if (!isReadOnly){ %>
                            <a href="javascript:showWindow('编辑离线作业','ExerciseEdit.aspx',600,320)">编辑</a> 
                            <%} %>
                            <a href="javascript:showWindow('查看离线作业','ExerciseView.aspx',600,330)">
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
    </div>