<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master"
    AutoEventWireup="true" CodeFile="ImportSignInInfo.aspx.cs" Inherits="TraningImplement_SignInManager_ImportSignInInfo" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--���ܱ���-->
    <h2 class="dv_title">
        ����ǩ��
    </h2>
    <!--��¼��-->
    <div class="dv_information">
        <table class="GridviewGray">
            <tr>
                    <th style=" width:15%;">
                        ��Ŀ���룺
                    </th>
                    <td >
                        <asp:Label ID="lblItemCode" runat="server"></asp:Label>
                    </td>
                    <th style=" width:15%;">
                        ��Ŀ���ƣ�
                    </th>
                    <td>
                        <cc1:ShortTextLabel ShowTextNum="10" ID="lblItemName" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th>
                        �γ̱��룺
                    </th>
                    <td>
                        <asp:Label ID="lblCourseCode" runat="server"></asp:Label>
                    </td>
                    <th>
                        �γ����ƣ�
                    </th>
                    <td>
                        <cc1:ShortTextLabel ShowTextNum="10" ID="lblCourseName" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th>
                        ��ѵ���ڣ�
                    </th>
                    <td>
                        <asp:Label ID="lblTrainingDate" runat="server" />
                    </td>
                    <th>
                        ��&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ʦ��
                    </th>
                    <td>
                        <asp:Label ID="lblTeacherName" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th>
                        ��ѵʱ�Σ�
                    </th>
                    <td colspan="3">
                        <asp:Label ID="lblTrainingTime" runat="server" />
                    </td>                    
                </tr>
            <tr>
                <th colspan="4">
                    ����˴�<a href="#">����ѧ��ǩ����</a>���ڵ�����ѧԱǩ�������޸�ѧԱǩ����Ϣ���ϴ���
                </th>                
            </tr>
            <tr>
                <th>
                    �����ļ���
                </th>
                <td colspan="3">
                    <asp:Label ID="lblFilePath" runat="server" ></asp:Label>
                    <cc1:FileUpload runat="server" ID="fileUpload1" FunctionType="OfflineJob" CallBack="callback" />
                    <script language="javascript">
                        function callback(imgName, imgUrl) {
                            $('#<%=lblFilePath.ClientID%>').html("<a href='" + imgUrl + "' target='_blank'>" + imgName + "</a>");
                        }
                    </script>
                </td>
            </tr>
        </table>
    </div>
    <div class="dv_submit">
          <input type="button" class="btn_Save" value="����" onclick="javascript:popSuccessMsg('��ϲ�㱣��ɹ���','��ʾ',closeWindow);" />
          <input type="button" class="btn_Cancel" value="ȡ��" onclick="javascript:closeWindow();" />
    </div>
</asp:Content>
