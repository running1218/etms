<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GradeEntry.ascx.cs" Inherits="Grade_GradeManage_Controls_GradeEntry" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<script language="javascript">
    function checkTextValue(obj) {
        var str_text = $.trim($(obj).val());
        var correct = vaildNumber(str_text);
        if (!correct) {
            popAlertMsg("请输入整数!", "提示");            
            obj.focus();
            $(obj).val("0");
        }

    };
    function vaildNumber(str1) {
        var patrn = /^(0|[1-9]\d*)$/; //^(0|[1-9]\d*)$,^[+-]?[0-9]+$
        if (!patrn.exec(str1)) return false
        return true;
    }

</script>

    <!--导航路径-->
    <div class="dv_path" id="dv_path">
        当前位置：测评系统&gt;&gt;考试与成绩&gt;&gt;<asp:Literal ID="Literal7" runat="server" Text="成绩管理"></asp:Literal>
    </div>
    <!--功能标题-->
    <h2 class="dv_title">
        成绩管理
    </h2>
    <div class="dv_GradeviewList">
        <table>
            <tr>
                <th width="120">
                    项目编码：
                </th>
                <td width="220">
                    <cc1:ShortTextLabel ID="lblItemCode" runat="server" ShowTextNum="10" />
                </td>
                <th width="120">
                    项目名称：
                </th>
                <td >
                    <cc1:ShortTextLabel ID="lblItemName" runat="server" ShowTextNum="10">
                    </cc1:ShortTextLabel>
                </td>
            </tr>
            <tr>
                <th>
                    课程编码：
                </th>
                <td>
                    <cc1:ShortTextLabel ID="lblCourseCode" runat="server" ShowTextNum="10">
                    </cc1:ShortTextLabel>
                </td>
                <th>
                    课程名称：
                </th>
                <td>
                    <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="10" />
                </td>
            </tr>
            <tr>
                <th>
                    授课方式：
                </th>
                <td>
                    <cc1:DictionaryLabel ID="lblTeachModel" runat="server" DictionaryType="Dic_Sys_TeachModel" />
                </td>
                <th>
                    学员人数：
                </th>
                <td>
                    <asp:Literal ID="lblStudentNum" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
    </div>

        <!--查找条件-->
    <div class="dv_searchbox">
        <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" id="tableQueryControlList" runat="server">
            <tr>
                <th style="width:120px;">
                    学员姓名：
                </th>
                <td style="width:125px;">
                    <asp:TextBox ID="txt_u999RealName" runat="server" CssClass="inputbox_120"></asp:TextBox>
                </td>
                <th style="width:120px;">
                    学员工号：
                </th>
                <td style="width:125px;">
                    <asp:TextBox ID="txt_s999WorkerNo" runat="server" CssClass="inputbox_120"></asp:TextBox>
                </td>
                <td colspan="2">
                    <asp:UpdatePanel ID="upQuery" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Button ID="Button1" runat="server" CssClass="btn_Search" Text="查询" OnClick="btnSearch_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>               
            </tr>                 
        </table>        
    </div>
    <div  class="dv_searchbox">
        <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <th>
                    按选择批量设置成绩：
                </th>
                <td style="width:120px;">
                    <asp:TextBox ID="txtInputGrade" runat="server" Text="0" MaxLength="6" onchange="checkTextValue(this)"  CssClass="inputbox_60" /> 
                    <asp:RequiredFieldValidator
                    ID="RequiredFieldValidatorJobName" runat="server" ErrorMessage="请填写成绩！" ControlToValidate="txtInputGrade"
                    ValidationGroup="input" Display="None"></asp:RequiredFieldValidator> 
                </td>
                <td>
                    <%--<input type="button" value="设置" class="btn_update" onclick="IsSelectRecord('请选择学员!')" />--%>
                    <asp:Button ID="btnRefresh" runat="server" Text="设置" CssClass="btn_update" OnClick="btnUpdate_Click" 
                        CommandName="input" ValidationGroup="input" />
                    <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="input"
                        ShowMessageBox="true" ShowSummary="false" />
                </td>
            </tr>  
        </table>
    </div>

    <!--查找结果-->
    <div class="dv_searchlist">
        <asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <!--翻页-->
                <div class="dv_pagePanel">
                    <div class="dv_selectAll">
                        <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" />
                        <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn_Save" OnClick="btnSave_Click" />  
                    </div>
                    <div class="dv_pageControl">
                        <uc2:PageSet ID="PageSet1" runat="server" />
                    </div>
                </div>
                <!--列表 begin-->
                <cc1:CustomGridView ID="CustomGridView1" runat="server" CssClass="GridviewGray" EmptyDataSourceTip="没有任何记录！"
                    AutoCreateColumnInsertIndex="0" AutoGenerateColumns="False" CurrentPageIndex="1"
                    DataKeyNames="StudentCourse" OnRowDataBound="CustomGridView1_RowDataBound" CustomAllowPaging="False"
                    IsEmpty="False">
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" Width="20" />
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                      
                        <asp:BoundField DataField="WorkerNo" HeaderText="工号" SortExpression="JobName" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft field8" >
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="RealName" HeaderText="学员姓名" SortExpression="JobName" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft field8">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                         <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_org%>" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                            <ItemTemplate>
                                <cc1:DictionaryLabel ID="lblOrgID" runat="server" DictionaryType='vw_Dic_Sys_Organization' FieldIDValue='<%#Eval("OrganizationID") %>'></cc1:DictionaryLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                            <ItemTemplate>
                                <cc1:DictionaryLabel runat="server" ID="lblDepartment" DictionaryType="vw_Dic_Sys_Department" TextLength="10"
                                    FieldIDValue='<%#Eval("DepartmentID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_position%>"  HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                            <ItemTemplate>
                                <cc1:DictionaryLabel runat="server" ID="lblPost" DictionaryType="vw_Dic_Sys_Post" TextLength="10"
                                    FieldIDValue='<%#Eval("PostID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="成绩" HeaderStyle-Width="140">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate> 
                                <asp:TextBox ID="txtSumGrade" runat="server"  MaxLength="6" onchange="checkTextValue(this)"  CssClass="inputbox_60"  />                                                    
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="学员状态" HeaderStyle-Width="60">
                           <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <cc1:DictionaryLabel ID="lblIsUse" DictionaryType="Dic_Status" runat="server" FieldIDValue='<%#Eval("Status") %>'  />                             
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="操作" HeaderStyle-Width="60">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:LinkButton ID="lblView" runat="server" Text="查看" />                                
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
            </ContentTemplate>
        </asp:UpdatePanel>           
    </div>
