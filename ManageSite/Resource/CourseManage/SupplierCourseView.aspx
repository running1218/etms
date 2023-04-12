<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="SupplierCourseView.aspx.cs" Inherits="Resource_CourseManage_SupplierCourseView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--表单录入-->
    <div class="dv_information">
        <table class="GridviewGray">
            <tr>
                <th style="width: 20%">
                    供应商编码：
                </th>
                <td style="width: 80%">
                    <asp:Literal ID="Literal2" runat="server" Text="GYS00100123"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                    供应商名称：
                </th>
                <td>
                    <asp:Literal ID="Literal1" runat="server">新东方</asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                    课程编码：
                </th>
                <td>
                    <asp:Literal ID="Literal3" runat="server" Text="1001"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                    课程名称：
                </th>
                <td>
                    <asp:Literal ID="Literal4" runat="server" Text="4＋1电影听说(新东方)"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                    课程类型：
                </th>
                <td>
                    <asp:Literal ID="Literal5" runat="server" Text="专业技能"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                    课程缩略图：
                </th>
                <td>
                    <asp:Literal ID="Literal6" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                    试用地址：
                </th>
                <td>
                    <asp:Literal ID="Literal7" runat="server" Text="www.xxx.com.cn"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                    适用对象：
                </th>
                <td>
                    <asp:Literal ID="Literal8" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                    课程介绍：
                </th>
                <td>
                    <asp:Literal ID="Literal9" runat="server" Text="本片由数个现实的故事组成，取材于一线客户服务人员的实际经历，故事中塑造了几种不同类型的客户代表：强势型、挑剔型、吵嚷型和犹豫型。在反面类型的情节中学员将领悟到：如果处理不当，很容易就会惹恼这些难对付的客户，生意就会泡汤。随后，当员工运用了“完美(PERFECT)”的技巧：礼貌(Polite)、高效(Efficient)、尊重(Respectful)、友好(Friendly)、热情(Enthusiastic)、快乐(Cheerful)、灵活(Tactful)，工作成效得以大为改观。故事情节简单但令人印象深刻。教授的技巧非常实用，无论对什么样的组织、何种级别的员工，只要涉及到与客户打交道的工作都能有所收获。 "></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                    课程大纲：
                </th>
                <td>
                    <asp:Literal ID="Literal10" runat="server" Text="为从事客户服务工作的人员讲述客户服务的技术要领，使他们能够针对不同类型的客户开展相应的服务工作 "></asp:Literal>
                </td>
            </tr>
        </table>
    </div>
    <div class="dv_submit">
        <a href="javascript:closeWindow();" class="btn_Close">关闭</a>
    </div>
</asp:Content>
