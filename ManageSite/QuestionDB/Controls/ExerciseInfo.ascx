<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ExerciseInfo.ascx.cs"
    Inherits="QuestionDB_Controls_ExerciseInfo" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/ChooseCourseDropdown.ascx" TagName="ChooseCourseDropdown"
    TagPrefix="uc1" %>
<!--导航路径-->
<div class="dv_path" id="dv_path">
        当前位置：资源管理系统&gt;&gt;学习资源管理&gt;&gt;<asp:Literal ID="Literal5" runat="server"></asp:Literal>
</div>
<!--功能标题-->
<h2 class="dv_title">
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
</h2>
<!--表单录入-->
<div class="dv_information" style=" width:90%">
    <table class="GridviewGray">
        <tr>
            <th width="80">
                课程名称：
            </th>
            <td colspan="3">
                <uc1:ChooseCourseDropdown ID="ChooseCourseDropdown1" runat="server" />
            </td>
           
        </tr>
        <tr>
            <th width="80">
                <asp:Literal ID="Literal2" runat="server"></asp:Literal>
            </th>
            <td width="300">
                <asp:TextBox ID="TextBox1" runat="server" CssClass="inputbox_210"></asp:TextBox>
            </td>
            <th width="80">
                状　　态：
            </th>
            <td>
               <cc1:DictionaryRadioButtonList runat = "server" ID="DictionaryRadioButtonList1" DictionaryType='Dic_Status' />
            </td>
        </tr>
        <tr>
            <th style="vertical-align: top">
               描　　述：
            </th>
            <td colspan="3">
                <asp:TextBox ID="TextBox2" runat="server" CssClass="inputbox_area490" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            
            <th>
                显示答案：
            </th>
            <td>
                <cc1:DictionaryRadioButtonList runat="server" ID="Dic_TrueOrFalse1" DictionaryType='Dic_TrueOrFalse' />
            </td>
            <th>
                及格分数：
            </th>
            <td>
                <asp:TextBox ID="TextBox4" runat="server" CssClass="inputbox_40"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
               题　　目：
            </th>
            <td colspan="3">
                <a href="<%=ETMS.Utility.WebUtility.AppPath %>/QuestionDB/Testpaper/TestpaperView.aspx"
                    target="_blank">查看题目</a>
                <asp:Literal ID="Literal6" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th>
                创建时间：
            </th>
            <td>
                <asp:Literal ID="Literal4" runat="server"></asp:Literal>
            </td>
            <th>
                创 建 者：
            </th>
            <td>
                <asp:Literal ID="Literal14" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
</div>
<!--提交表单-->
<div class="dv_submit">
    <a href="javascript:history.go(-1);" class="btn_Save">保存</a> <a href="javascript:history.go(-1);"
        class="btn_2">取消</a>
</div>
