<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="FeeSettingList.aspx.cs" Inherits="Fee_CourseFeeSetting_FeeSettingList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div style="visibility:hidden;"> 
<asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click"/></div>
    <!--查找结果-->
    <div class="dv_searchlist">
        <!--列表 begin-->
       
         <asp:GridView ID="CustomGridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="CourseFeeSettingID">
            <Columns>
                <asp:TemplateField HeaderText="讲师等级" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft" HeaderStyle-Width="60">
                    <ItemTemplate>
                        <cc1:DictionaryLabel ID="lblTeacherLevel" DictionaryType="Dic_Sys_TeacherLevel"  FieldIDValue='<%# Eval("TeacherLevelID") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="培训时间" HeaderStyle-Width="80">
                    <ItemTemplate>
                        <cc1:DictionaryLabel ID="lblTrainingTimeDesc" DictionaryType="Dic_Sys_TrainingTimeDesc"  FieldIDValue='<%# Eval("TrainingTimeDescID") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>

               <asp:TemplateField HeaderText="课酬标准" HeaderStyle-CssClass="field18">
                    <ItemTemplate>
                        <asp:TextBox ID="txtCourseFee"  runat="server" Text='<%# Eval("CourseFee") %>'  MaxLength="8"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="请填非负整数"
                                    ValidationGroup="Error" Display="Dynamic" ControlToValidate="txtCourseFee"
                                    ValidationExpression="^\d+$"></asp:RegularExpressionValidator>

                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="备注" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                    <ItemTemplate>
                         <asp:TextBox ID="txtRemark" TextMode="MultiLine" runat="server" Text='<%# Eval("Remark") %>' CssClass="inputbox_area190" Width="340" Height="39"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
        <!--列表 end-->
        <div class="dv_splitLine">
        </div>
    </div>
    <p style="text-align: center;">
            <asp:Button ID="lbtnSave" Text="保存" runat="server" ValidationGroup="Error" CssClass="btn_Save" OnClick="lbtnSave_Click"></asp:Button>
        </p>
</asp:Content>
