<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AppraisalInfo.ascx.cs" Inherits="Activity_Controls_AppraisalInfo" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Assembly="ETMS.Editor" Namespace="ETMS.Editor.UEditor" TagPrefix="wuc" %>
<%@ Register Src="~/Controls/MiniUpFile.ascx" TagName="uploader" TagPrefix="uc" %>
<!--表单录入-->
<div class="dv_information">
    <table class="GridviewGray fixedTable">
        <tr>
            <th>
                <font color="red">*</font>活动标题：
            </th>
            <td>
                <asp:TextBox ID="txtAppraisalName" runat="server" CssClass="inputbox_300" MaxLength="50"></asp:TextBox>                
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorName" runat="server" ControlToValidate="txtAppraisalName"
                    Display="None" ErrorMessage="请填写活动标题！" ValidationGroup="Error"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                <font color="red">*</font>活动类型：
            </th>
            <td>
                <cc1:DictionaryDropDownList runat="server" ID="ddlType" DictionaryType="Activity_Dic_Type"
                    IsShowChoose="false" IsShowAll="false" CssText="select_120" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorType" runat="server" ControlToValidate="ddlType"
                    Display="None" ErrorMessage="请选择活动类型！" ValidationGroup="Error"></asp:RequiredFieldValidator>
            </td>            
        </tr>
        <tr id="trArea" runat="server" style="display:none;">
            <th>活动地点：</th>
            <td>
                <asp:DropDownList ID="ddlProvince" runat="server" CssClass="select_120"></asp:DropDownList>
                <asp:DropDownList ID="ddlCity" runat="server" CssClass="select_120"></asp:DropDownList>
                <asp:TextBox ID="txtAddress" runat="server" CssClass="inputbox_300" MaxLength="50"></asp:TextBox>                                
            </td>
        </tr>
        <tr>
            <th><font color="red">*</font>活动形式：</th>            
            <td>
                <cc1:DictionaryDropDownList ID="ddlShape" runat="server" IsShowAll="false" IsShowChoose="false" CssClass="select_120" DictionaryType="Activity_Dic_Shape"></cc1:DictionaryDropDownList>
                <asp:RequiredFieldValidator ID="rfvShape" runat="server" ControlToValidate="ddlShape"
                    Display="None" ErrorMessage="请选择活动形式！" ValidationGroup="Error"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th><font color="red">*</font>活动时间：</th>
            <td>
                 <asp:TextBox ID="txtStartTime" runat="server" CssClass="inputbox_190 datetimepicker-icon"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfvStartTime" runat="server" ControlToValidate="txtStartTime" ErrorMessage="请输入开始时间！" Display="None" Text="*" ForeColor="Red" ValidationGroup="Error"></asp:RequiredFieldValidator>
                ~
                 <asp:TextBox ID="txtEndTime" runat="server" CssClass="inputbox_190 datetimepicker-icon"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfvEndTime" runat="server" ControlToValidate="txtEndTime" ErrorMessage="请输入结束时间！" Display="None" Text="*" ForeColor="Red" ValidationGroup="Error"></asp:RequiredFieldValidator>      
            </td>
        </tr>
        <tr>
            <th>
                <font color="red">*</font>活动区域：
            </th>
            <td>
                <asp:CheckBoxList RepeatLayout="UnorderedList" ID="cblRegions" runat="server"></asp:CheckBoxList>  
                <asp:customvalidator id="CustomValidator1" runat="server" ErrorMessage="请选择活动区域！" Display="None" ClientValidationFunction="checkRegions" ValidationGroup="Error"></asp:customvalidator>               
            </td>
        </tr>        
        <tr>
            <th>
                <font color="red">*</font>活动组别：
            </th>
            <td>
                <asp:CheckBoxList RepeatLayout="UnorderedList" ID="cblGroup" runat="server"></asp:CheckBoxList>    
                <asp:customvalidator id="CustomValidator2" runat="server" ErrorMessage="请选择活动组别！" Display="None" ClientValidationFunction="checkGroup" ValidationGroup="Error"></asp:customvalidator>                          
            </td>
        </tr>
        <tr>
            <th>
                活动海报：
            </th>
            <td valign="top">
                <div style="overflow: hidden;">
                    <asp:Image ID="imgActivity" runat="server" CssClass="imgCourseLogo" Width="320px"
                        Height="180px" /></div>
                <uc:uploader ID="uploader" runat="server" FunctionType="MediaLogo" CallBack="doCallBack" FileTypeIsDisplay="false" />
                <script type="text/javascript">
                    function doCallBack(imgName, imgUrl, imgSize) {
                        document.getElementById('<%=imgActivity.ClientID %>').src = imgUrl;
                    }
                </script>
                <span class="upload-img-standard">支持jpg、gif格式的深底色图片，最佳尺寸为480×270像素</span>
            </td>
        </tr>
        <tr>
            <th>活动人数：</th>
            <td>
                <cc1:CustomTextBox ID="txtActivityNum" runat="server" ContentType="Number" Text="0" CssClass="inputbox_80"></cc1:CustomTextBox><span style="color:#ff6a00; margin-left:20px;">0,表示不受限制</span>
            </td>
        </tr>
        <tr>
            <th>活动摘要：</th>
            <td>
                <asp:TextBox ID="txtAbstract" runat="server" TextMode="MultiLine" Height="80px" CssClass="abstract-area"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                详细内容：
            </th>
            <td>
                <wuc:UEditor ID="ueDetail" runat="server" Width="600" Height="200" ToolType="Basic" AutoHeightEnabled="false"></wuc:UEditor>
            </td>
        </tr>
        <tr>
            <th>
                评分规则：
            </th>
            <td>
                <wuc:UEditor ID="ueRule" runat="server" Width="600" Height="200" ToolType="Basic" AutoHeightEnabled="false"></wuc:UEditor>
            </td>
        </tr>         
    </table>
</div>
<!--提交表单-->
<div class="dv_submit">
    <asp:LinkButton ID="lbtnSave" runat="server" CssClass="btn_Save" OnClick="lbtnSave_Click"
        ValidationGroup="Error">保存</asp:LinkButton>
    <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Error"
        ShowMessageBox="true" ShowSummary="false" />
    <a href="javascript:closeWindow();" class="btn_Cancel padleft10">取消</a>
</div>

<script lang="javascript" type="text/javascript">
    $(document).ready(function(){
        $('#'+ '<%=ddlProvince.ClientID%>').change(function () {
            var parentID = $('#'+ '<%=ddlProvince.ClientID%>').val();
            $.ajax({
                url: '<%=ETMS.Utility.WebUtility.AppPath%>' + '/services/Activity.ashx',
                type: 'POST',
                data: { ParentID: parentID },
                dataType: "json",
                async: false,
                success: function (result) {
                    var ddlCity = $('#' + '<%=ddlCity.ClientID%>');   
                    $(ddlCity).empty();
                    $(ddlCity).append($("<option value=''>地区 / 市</option>"));
                    for (var i=0; i < result.Data.length; i++) {
                        $(ddlCity).append($("<option value='" + result.Data[i].AreaCode + "'>" + result.Data[i].AreaName + "</option>"));
                    } 
                },
                error: function (err) {}
            });
        });

        $('#' + '<%=ddlType.ClientID%>').change(function () {
            if ($('#' + '<%=ddlType.ClientID%>').val() == 1) {
                $('#' + '<%=trArea.ClientID%>').hide();
            }
            else {
                $('#' + '<%=trArea.ClientID%>').show();
            }
        });
    });

    //日期事件
    $('#<%=txtStartTime.ClientID%>').datetimepicker({ lang: 'ch', step: 30 });
    $('#<%=txtEndTime.ClientID%>').datetimepicker({ lang: 'ch', step: 30 });

    $(function () {
        isLoadFish();
    });

    function checkRegions(source, args)
    {        
        var chkListaTipoModificaciones= document.getElementById ('<%= cblRegions.ClientID %>');
        var chkLista= chkListaTipoModificaciones.getElementsByTagName("input");
        for(var i=0;i<chkLista.length;i++)
        {  
            if(chkLista[i].checked)
            {
                args.IsValid = true;
                return;
            }
        }
        args.IsValid = false;
    }
    function checkGroup(source, args)
    {        
        var chkListaTipoModificaciones= document.getElementById ('<%= cblGroup.ClientID %>');
        var chkLista= chkListaTipoModificaciones.getElementsByTagName("input");
        for(var i=0;i<chkLista.length;i++)
        {  
            if(chkLista[i].checked)
            {
                args.IsValid = true;
                return;
            }
        }
        args.IsValid = false;
    }
</script>
<style>
    #appraisal_cblRegions li, #appraisal_cblGroup li{float:left;margin-right:15px;}
    .abstract-area{width:600px !important;}
</style>

