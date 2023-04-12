<%@ Control Language="C#" AutoEventWireup="true" CodeFile="IDPNotCourseDataInfo.ascx.cs" Inherits="IDP_IDPNotCourseDataInfo" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<cc1:CustomMuliView runat="server" ID="Views">    
    <asp:View runat="server" ID="View_Edit">
      <div class="dv_information">
        <table  class="GridviewGray th140">            
            <tr>
                <th >
                    资料编码：
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtDataCode" runat="server" CssClass="inputbox_300" MaxLength="20"></asp:TextBox>
                     <span style="color: Red;">*</span><asp:RequiredFieldValidator
                        ID="RequiredFieldValidatorDataCode" runat="server" ErrorMessage="请填写资料编码！" ControlToValidate="txtDataCode"
                        ValidationGroup="Edit" Display="None"></asp:RequiredFieldValidator> 
                </td>
            </tr>
            
            <tr>
                <th>
                    资料名称：
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtDataName" runat="server" CssClass="inputbox_300" MaxLength="50"></asp:TextBox>
                     <span style="color: Red;">*</span><asp:RequiredFieldValidator
                        ID="RequiredFieldValidatorDataName" runat="server" ErrorMessage="请填写资料名称！" ControlToValidate="txtDataName"
                        ValidationGroup="Edit" Display="None"></asp:RequiredFieldValidator> 
                </td>
            </tr>
            
            <tr>
                <th>
                    学习内容：
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtDataCotent" runat="server" CssClass="inputbox_300"></asp:TextBox>
                    <%-- <span style="color: Red;">*</span><asp:RequiredFieldValidator
                        ID="RequiredFieldValidatorDataCotent" runat="server" ErrorMessage="请填写学习内容！" ControlToValidate="txtDataCotent"
                        ValidationGroup="Edit"></asp:RequiredFieldValidator> --%>
                </td>
            </tr>
            
            <tr>
                <th>
                    学习纲要：
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtDataOutline" runat="server" CssClass="inputbox_300"></asp:TextBox>
                    <%-- <span style="color: Red;">*</span><asp:RequiredFieldValidator
                        ID="RequiredFieldValidatorDataOutline" runat="server" ErrorMessage="请填写学习纲要！" ControlToValidate="txtDataOutline"
                        ValidationGroup="Edit"></asp:RequiredFieldValidator> --%>
                </td>
            </tr>
            
            <tr>
                <th>
                    资料状态：
                </th>
                <td colspan="3">
                    <cc1:DictionaryRadioButtonList DictionaryType="Dic_Status" ID="rbnDataStatus" runat="server"  />
                </td>
            </tr>  
                      
            <tr>
                <th>
                    学习方式：
                </th>
                <td colspan="3">
                    <cc1:DictionaryRadioButtonList DictionaryType="Dic_Sys_TeachModel" ID="rbnTeachModelID" runat="server"  />                   
                </td>
            </tr>
            
            <tr>
                <th>
                    预计时长：
                </th>
                <td>
                    <asp:TextBox ID="txtTimeLength" runat="server" CssClass="inputbox_80" MaxLength="6"></asp:TextBox>小时
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtTimeLength" Display="None" ErrorMessage="预计时长格式错误！" ValidationExpression="^(0|[1-9]\d*)(\.\d*)?$" ValidationGroup="Edit"></asp:RegularExpressionValidator>
                  
                </td>
                <th>
                    次&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;数：
                </th>
                <td>
                    <asp:TextBox ID="txtStudyTimes" runat="server" CssClass="inputbox_120" MaxLength="5"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtStudyTimes" Display="None" ErrorMessage="次数格式错误！" ValidationExpression="^(0|[1-9]\d*)(\.\d*)?$" ValidationGroup="Edit"></asp:RegularExpressionValidator>
                </td>
            </tr>            
            <tr>
                <th>
                    实&nbsp;&nbsp;施&nbsp;&nbsp;人：
                </th>
                <td>
                    <asp:TextBox ID="txtImplementor" runat="server" CssClass="inputbox_120" MaxLength="30"></asp:TextBox>
                    <%-- <span style="color: Red;">*</span><asp:RequiredFieldValidator
                        ID="RequiredFieldValidatorImplementor" runat="server" ErrorMessage="请填写实施人！" ControlToValidate="txtImplementor"
                        ValidationGroup="Edit"></asp:RequiredFieldValidator> --%>
                </td>
                <th>
                    责&nbsp;&nbsp;任&nbsp;&nbsp;方：
                </th>
                <td>
                    <asp:TextBox ID="txtDutyMan" runat="server" CssClass="inputbox_120" MaxLength="30"></asp:TextBox>
                    <%-- <span style="color: Red;">*</span><asp:RequiredFieldValidator
                        ID="RequiredFieldValidatorDutyMan" runat="server" ErrorMessage="请填写责任方！" ControlToValidate="txtDutyMan"
                        ValidationGroup="Edit"></asp:RequiredFieldValidator> --%>
                </td>
            </tr>
            
            <tr>
                <th>
                    培训资料所在：
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtDataURL" runat="server" CssClass="inputbox_300" MaxLength="60"></asp:TextBox>
                    <%-- <span style="color: Red;">*</span><asp:RequiredFieldValidator
                        ID="RequiredFieldValidatorDataURL" runat="server" ErrorMessage="请填写培训资料所在！" ControlToValidate="txtDataURL"
                        ValidationGroup="Edit"></asp:RequiredFieldValidator> --%>
                </td>
            </tr>           
            <tr>
                <th>
                    学习效果评量方式：
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtEvaluationMode" runat="server" CssClass="inputbox_300" MaxLength="60"></asp:TextBox>
                    <%-- <span style="color: Red;">*</span><asp:RequiredFieldValidator
                        ID="RequiredFieldValidatorEvaluationMode" runat="server" ErrorMessage="请填写学习效果评量方式！" ControlToValidate="txtEvaluationMode"
                        ValidationGroup="Edit"></asp:RequiredFieldValidator> --%>
                </td>
            </tr>           
        </table>
        </div>
    </asp:View>
    <asp:View runat="server" ID="View_Browse">
        <div class="dv_information">
        <table  class="GridviewGray fixedTable th140">            
            <tr>
                <th >
                    资料编码：
                </th>
                <td colspan="3">
                    <asp:Label ID="lblDataCode" runat="server"></asp:Label>
                </td>                
            </tr>
            <tr>
                 <th>
                    资料名称：
                </th>
                <td colspan="3">
                    <asp:Label ID="lblDataName" runat="server"></asp:Label>
                </td>
            </tr>            
           
            <tr>
                <th>
                    学习内容：
                </th>
                <td colspan="3">
                    <asp:Label ID="lblDataCotent" runat="server"></asp:Label>
                </td>               
            </tr>

            <tr>
                <th>
                    学习纲要：
                </th>
                <td colspan="3">
                    <asp:Label ID="lblDataOutline" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table  class="GridviewGray fixedTable th140">
            <tr>
                <th>
                    预计时长：
                </th>
                <td>
                    <asp:Label ID="lblTimeLength" runat="server"></asp:Label>
                </td>
                 <th width="120">
                    次&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;数：
                </th>
                <td>
                    <asp:Label ID="lblStudyTimes" runat="server"></asp:Label>
                </td>
            </tr>
                        
            <tr>
                <th>
                    资料状态：
                </th>
                <td>
                    <cc1:DictionaryLabel ID="lblDataStatus" runat="server" DictionaryType="Dic_Status" />
                </td>
                 <th>
                    学习方式：
                </th>
                <td>
                   <cc1:DictionaryLabel ID="lblTeachModelID" runat="server" DictionaryType="Dic_Sys_TeachModel" />
                </td>
            </tr>
            
            <tr>
                <th>
                    实&nbsp;&nbsp;施&nbsp;&nbsp;人：
                </th>
                <td>
                    <asp:Label ID="lblImplementor" runat="server"></asp:Label>
                </td>
                 <th>
                    责&nbsp;&nbsp;任&nbsp;&nbsp;方：
                </th>
                <td>
                    <asp:Label ID="lblDutyMan" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table  class="GridviewGray fixedTable th140">
            <tr>
                <th>
                    培训资料所在：
                </th>
                <td colspan="3">
                    <asp:Label ID="lblDataURL" runat="server"></asp:Label>
                </td>
            </tr>
            
            <tr>
                <th>
                    学习效果评量方式：
                </th>
                <td colspan="3">
                    <asp:Label ID="lblEvaluationMode" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table  class="GridviewGray fixedTable th140">
            <tr>
                <th>
                    创建时间：
                </th>
                <td>
                    <asp:Label ID="lblCreateTime" runat="server"></asp:Label>
                </td>
                <th width="120">
                    创&nbsp;&nbsp;建&nbsp;&nbsp;人：
                </th>
                <td>
                    <asp:Label ID="lblCreateUser" runat="server"></asp:Label>
                </td>
            </tr>
            
            <tr>
                <th>
                    修改时间：
                </th>
                <td>
                    <asp:Label ID="lblModifyTime" runat="server"></asp:Label>
                </td>
                <th>
                    修&nbsp;&nbsp;改&nbsp;&nbsp;人：
                </th>
                <td>
                    <asp:Label ID="lblModifyUser" runat="server"></asp:Label>
                </td>
            </tr>           
        </table>
        </div>
    </asp:View> 
    </cc1:CustomMuliView>
