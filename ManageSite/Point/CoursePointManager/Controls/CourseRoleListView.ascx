<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CourseRoleListView.ascx.cs" Inherits="Point_CoursePointManager_Controls_CourseRoleListView" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
    <div class="dv_searchlist">
        <!--翻页-->
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
                <asp:TemplateField HeaderText="课程属性" HeaderStyle-Width="60">
                   <ItemTemplate>
                       <cc1:DictionaryLabel ID="lblCourseAttrID" runat="server" DictionaryType="Dic_Sys_CourseAttr" FieldIDValue='<%#Eval("CourseAttrID") %>' />
                   </ItemTemplate>                   
                </asp:TemplateField>                
                <asp:TemplateField HeaderText="培训课时（小时）" ItemStyle-CssClass="alignright" HeaderStyle-CssClass="alignright">
                    <ItemTemplate>
                        <asp:Label ID="lblMinNum" runat="server" Text='<%#Eval("MinNum") %>' />
                        -
                        <asp:Label ID="lblMaxNum" runat="server" Text='<%#Eval("MaxNum") %>' />（含）
                    </ItemTemplate>                   
                </asp:TemplateField>
                <asp:TemplateField HeaderText="积分" ItemStyle-CssClass="alignright" HeaderStyle-CssClass="alignright" HeaderStyle-Width="60">
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