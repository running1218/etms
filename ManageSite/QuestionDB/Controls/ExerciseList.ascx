<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ExerciseList.ascx.cs"
    Inherits="QuestionDB_Controls_ExerciseList" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/ChooseCourseDropdown.ascx" TagName="ChooseCourseDropdown"
    TagPrefix="uc1" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<!--导航路径-->
<div class="dv_path" id="dv_path">
    当前位置：资源管理系统&gt;&gt;学习资源管理&gt;&gt;<asp:Literal ID="Literal2" runat="server"></asp:Literal>
</div>
<!--功能标题-->
<h2 class="dv_title">
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
</h2>
<!--查找条件-->
<div class="dv_searchbox">
    <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0">
        <tr>
        <th style="width:80px;">
                        课程编码：
                    </th>
                    <td style="width:130px;"><asp:TextBox ID="TextBox3" runat="server" CssClass="inputbox_120"></asp:TextBox></td>
             <th style="width:80px;">
                课程名称：
            </th>
            <td >
                <asp:TextBox ID="TextBox1" runat="server" CssClass="inputbox_120"></asp:TextBox>
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
            <input type="button" class="btn_Add" value="新增" onclick="javascript:window.open('ExerciseAdd.aspx','_self')" />
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
            <asp:TemplateField HeaderText="序号" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                <ItemStyle HorizontalAlign="Center" Width="60" />
                <HeaderStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="LabNo" runat="server" Text='<%#  ((this.PageSet1.PageIndex -1) * this.PageSet1.PageSize) + Container.DataItemIndex+1 %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <% if (!isReadOnly)
                       { %>
                    <a href="ExerciseEdit.aspx">编辑</a>
                    <%} %>
                    <a href="javascript:showWindow('查看<%= getExerciseName() %>','ExerciseView.aspx',600,400)">查看</a>
                    
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
