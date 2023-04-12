<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="ImportScormFiles.aspx.cs" Inherits="Resource_CoursewareManage_ImportScormFiles" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <!--表单录入-->
    <div class="dv_information">

     <table class="GridviewGray th120">

            <tr>
                <th>
                    课件名称：
                </th>
                <td colspan="3">
                    <asp:Literal ID="ltlCoursewareName" runat="server"></asp:Literal>
                </td>
            </tr>            
        </table>
        <br />
         <table class="GridviewGray th120">
          
            <tr>
                <th style="width:20%;">
                    上传SCORM：
                </th>
                <td style="width:80%;">
                    <asp:Button ID="btnUpFile" runat="server" Text="上传课件" CssClass="btn_upload" Visible="false" />
                    <asp:Button ID="btnUpFile2" runat="server" Text="上传课件" CssClass="btn_upload" />
                    <asp:Label ID="lblFileName" runat="server" CssClass="lblFileName"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblState" runat="server" CssClass="lblState"></asp:Label>
                    <span style="display:none"><asp:TextBox ID="txtFileName" runat="server" CssClass="txtFileName"></asp:TextBox></span>
                </td>
            </tr>          
        </table>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lbtnSave" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <div id="div_Import_Status" runat="server">
                    <div class="import_status">
                        &nbsp;</div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>        
    </div>

    <div class="dv_submit">
        <asp:Button ID="lbtnSave" runat="server" CssClass="btn_Ok" OnClick="lbtnSave_Click"
            ValidationGroup="Error" Text="导入"></asp:Button>
        <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Error"
            ShowMessageBox="true" ShowSummary="false" />
        <input onclick="javascript:closeWindow();" value="取消" type="button" class="btn_Cancel"/>
    </div>
</asp:Content>

