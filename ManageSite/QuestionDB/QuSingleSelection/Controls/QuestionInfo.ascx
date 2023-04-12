<%@ Control Language="C#" AutoEventWireup="true" CodeFile="QuestionInfo.ascx.cs"
    Inherits="QuestionDB_QuSingleSelection_Controls_QuestionInfo" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/ChooseCourseDropdown.ascx" TagName="ChooseCourseDropdown"
    TagPrefix="uc1" %>

<script type="text/javascript">
    function checkTitle() {
        var p = $("#<%=RichTextTitle.ClientID %>").val();
        if (p.length == 0) {
            alert("请填写试题题目！");
            return false;
        }
        else {
            saveFunoption('single', "<%=hfldOptionList.ClientID %>");
            return true;
        }
    }

</script>
<!--表单录入-->
<div class="dv_information">
    <!--查找条件-->
    <table class="GridviewGray fixedTable">
        <tr>
            <th>
                课程名称：
            </th>
            <td>
                <asp:Literal ID="ltlQuestionBankName" runat="server"></asp:Literal>
            </td>
            <th>
                难度：
            </th>
            <td>
                <cc1:DictionaryDropDownList runat="server" ID="ddlDifficulty" DictionaryType="Dic_DegreeDifficulty"
                    CssClass="select_60" IsShowChoose="false" IsShowAll="false" />
            </td>
        </tr>
    </table>
    <table class="GridviewGray GridviewGrayboder">
        <tr>
            <th class="thleft">
                题目
            </th>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="RichTextTitle" runat="server" TextMode="MultiLine" CssClass="inputbox_area520"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table class="GridviewGray GridviewGrayboder">
        <tr>
            <th class="center">
                答案
            </th>
            <th class="center">
                选项
            </th>
            <th class="center">
                答案内容
            </th>
            <th class="center">
                操作
            </th>
        </tr>
        <%for (int i = 0; i < OptionCount; i++)
          {%>
        <tr id="tr_<%=i %>">
            <td class="center">
                <input type="radio" id="rbtnOption_<%=i %>" name="myradio" <%= GetQuestionChecked(i) %> />
                <input type="hidden" id="txtOptionID_<%=i %>" value="<%= GetQuestionOptionID(i) %>" />
            </td>
            <td class="center">
                <span>
                    <%= letterlist[i]%></span>
            </td>
            <td class="center">
                <textarea id="txtOptionContent_<%=i%>" class="inputbox_area360 inputbox_area360_hight"
                    rows="5"><%= GetQuestionOptionContent(i) %></textarea>
            </td>
            <td width="40">
                <% if (i > 1)
                   { %>
                <a href="javascript:deloption('tr_<%=i %>')">删除</a>
                <%} %>
            </td>
        </tr>
        <% }%>
    </table>
    <div style="padding-left: 10px!important; padding-left: 6px;">
        <input type="button" id="addansweroption" onclick="addoption('single')" class="btn_AddNew"
            value="增加答案选项" />
    </div>
    <asp:HiddenField ID="hfldOptionList" runat="server" />
</div>
<!--提交表单-->
<div class="dv_submit">
    <asp:LinkButton ID="lbtnSave" runat="server" CssClass="btn_Save" OnClientClick="return checkTitle()"
        OnClick="lbtnSave_Click" ValidationGroup="Error">保存</asp:LinkButton>
    <a href="javascript:closeWindow();" class="btn_Cancel padleft10">取消</a>
</div>
