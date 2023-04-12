<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="CourseRoleList.aspx.cs" Inherits="Point_CourseRoleList" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">   
 <!--查找条件-->
    
    <div class="dv_searchbox">
        <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
            id="tableQueryControlList">
            <tr>
                <th width="120">
                    课程属性：
                </th>
                <th width="120">
                    <asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional" >
                     <ContentTemplate>  
                        <cc1:DictionaryDropDownList ID="ddl_CourseAttrID" runat="server" DictionaryType="Dic_Sys_CourseAttr"
                           IsShowAll="true" />   
                    </ContentTemplate>
                  </asp:UpdatePanel>                 
                </th>
                <td >                      
                   <asp:Button ID="btnSearch" runat="server" CssClass="btn_Search" Text="查询" OnClick="btnSearch_Click" />                   
                </td>
            </tr>
        </table>
    </div>
    
    <!--查找结果-->
     <asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
    <div class="dv_searchlist">
        <!--翻页-->
        <div class="dv_pagePanel">
            <div class="dv_selectAll">
                <%-- <input type="button" class="btn_Add" value="设置" onclick="javascript:showWindow('课程积分规则设置','<%=this.ActionHref(string.Format("CourseRoleSetting.aspx?CourseAttrID={0}", this.ddl_CourseAttrID.SelectedValue.ToInt())) %>')" /> --%>                
                <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="设置" CssClass="btn_Configer"  />
                <%--<input type="button" onclick="OpenPointWindow()" class="btn_Add" value="设置" />--%>
            </div>
            <%--<div class="dv_pageControl">
                <uc2:PageSet ID="PageSet1" runat="server" />
            </div>--%>
        </div>
        <!--列表 begin-->
        <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="False"
            ShowFooter=" false" DataKeyNames="StudentCoursePointRoleID" >
            <Columns>
                <asp:TemplateField HeaderText="序号" HeaderStyle-Width="40">
                    <ItemStyle HorizontalAlign="Center" Width="60" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="LabNo" runat="server" Text='<%#Container.DataItemIndex+1 %>' />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblNo" runat="server" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="课程属性" HeaderStyle-Width="60" >
                   <ItemTemplate>
                       <cc1:DictionaryLabel ID="lblCourseAttrID" runat="server" DictionaryType="Dic_Sys_CourseAttr" FieldIDValue='<%#Eval("CourseAttrID") %>' />
                   </ItemTemplate>                   
                </asp:TemplateField>                
                <asp:TemplateField HeaderText="培训课时（小时）" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright">
                    <ItemTemplate>
                        <asp:Label ID="lblMinNum" runat="server" Text='<%#Eval("MinNum") %>' />
                        -
                        <asp:Label ID="lblMaxNum" runat="server" Text='<%#Eval("MaxNum") %>' />（含）
                    </ItemTemplate>                   
                </asp:TemplateField>
                <asp:TemplateField HeaderText="积分" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright" HeaderStyle-Width="60">
                    <ItemTemplate>
                        <asp:Label ID="lblScore" runat="server" Text='<%#Eval("GivePoints") %>' />
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
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

