<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="Marking.aspx.cs" Inherits="QuestionDB_Marking_Marking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <!--导航路径-->
    <div class="dv_path" id="dv_path">
        当前位置：测评系统>>考试与成绩>>试卷评阅
    </div>
    <!--功能标题-->
    <h2 class="dv_title">
        试卷评阅
    </h2>
     <div class="dv_pageInformation">  
        <table  class="GridviewGray">
    
            <tr>
                <th class="colorYellow">
           请简述xxxxxxxxxxxxxxxxxxxxx(最少200字)
                </th>
            </tr>
              <tr>
                <td>
           <asp:TextBox ID="TextBox6" runat="server" TextMode="MultiLine" CssClass="inputbox_area300"></asp:TextBox>
                </td>
            </tr>
            </table>
            <table  class="GridviewGray">
            <tr>
            <th class="thleft" style="width:80px;">答案反馈：</th>
                <td style="width:500px;">
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
            <th class="thleft">解题思路：</th>
                <td>
                    <asp:Literal ID="Literal5" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
            <th class="thleft">评语：</th>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine" CssClass="inputbox_area300"></asp:TextBox>
                </td>
            </tr>
            <tr>
            <th class="thleft">得分：</th>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        </div>
        <!--提交表单-->
        <div class="dv_submit">
           <a href="javascript:history.go(-1);" class="btn_Ok">提交</a>
            <a href="javascript:history.go(-1);" class="btn_Cancel padleft10">返回</a>
        </div>
    
</asp:Content>

