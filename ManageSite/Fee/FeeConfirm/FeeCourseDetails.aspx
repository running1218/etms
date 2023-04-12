<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="FeeCourseDetails.aspx.cs" Inherits="FeeCourseDetails" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="dv_information">

       <!--查找条件-->
        <div>
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0">
                 <tr>
                    <th>培训项目：</th>
                    <td>
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="inputbox_120"></asp:TextBox>                        
                    </td>                    
                    <th>课程名称：</th>
                    <td>                
                        <asp:TextBox ID="TextBox2" runat="server" CssClass="inputbox_120"></asp:TextBox>                                                        
                        <input class="btn_Search" type="button" value="查询"/>
                       
                    </td>
                </tr>
                <tr>
                    <th>讲师姓名：</th>
                    <td>
                        <asp:TextBox ID="TextBox3" runat="server" CssClass="inputbox_120"></asp:TextBox>                        
                    </td>                    
                    <th>状&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;态：</th>
                    <td>                
                        <asp:DropDownList ID="ddlCourseStatus" runat="server">
                            <asp:ListItem Value= "0" Text="请选择">请选择</asp:ListItem>
                            <asp:ListItem Value= "1" Text="未设置">未设置</asp:ListItem>
                            <asp:ListItem Value= "2" Text="正常结束">正常结束</asp:ListItem>
                            <asp:ListItem Value= "3" Text="异常结束">异常结束</asp:ListItem>
                        </asp:DropDownList>                                                        
                    </td>
                </tr>                 
            </table>
        </div>

        <!--查找结果-->
        <div class="dv_searchlist">
            <asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <!--翻页-->
                    <div class="dv_pagePanel" id="divPage1">
                        <div class="dv_selectAll">
                            <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" />
                           
                        </div>
                        <div class="dv_pageControl">
                            <uc2:PageSet ID="PageSet1" runat="server" />
                        </div>
                    </div>
                    <!--列表 begin-->
                    <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="True"
                        CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0">
                        <Columns>
                            <asp:TemplateField HeaderText="">
                                <ItemStyle HorizontalAlign="Center" Width="40" /> 
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>                            
                        </Columns>
                    </cc1:CustomGridView>
                    <!--列表 end-->
                    <div class="dv_splitLine">
                    </div>
                    <!--翻页-->
                    <div class="dv_pagePanel" id="divPage2">
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <!--提交表单-->
    <div class="dv_submit">
        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn_Ok" >确定</asp:LinkButton>
        <a href="javascript:closeWindow();" class="btn_Cancel padleft10">取消</a>
    </div>
</asp:Content>

