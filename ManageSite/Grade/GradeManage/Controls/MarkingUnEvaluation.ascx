<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MarkingUnEvaluation.ascx.cs" Inherits="Grade_GradeManage_Controls_MarkingUnEvaluation" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc1" %>
<!--查找结果-->
<div class="dv_searchlist">
    <!--翻页-->
    <div class="dv_pagePanel">
        <div class="dv_pageControl">
            <uc1:PageSet ID="PageSet1" runat="server" />
        </div>
    </div>
    <!--列表 begin-->
    <cc1:CustomGridView ID="GridViewList" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridViewList_RowDataBound">
        <Columns>
            <asp:BoundField DataField="LoginName" HeaderText="帐号" SortExpression="LoginName" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft" HeaderStyle-Width="120">
                <ItemStyle HorizontalAlign="Left" CssClass="alignleft" />
                <HeaderStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="RealName" HeaderText="学员姓名" SortExpression="JobName" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft" HeaderStyle-Width="120">
                <ItemStyle HorizontalAlign="Left" CssClass="alignleft" />
                <HeaderStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="上传时间" HeaderStyle-Width="80">
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="lblUploadTime" runat="server" Text='<%#Eval("UploadTime")==DBNull.Value?"":Eval("UploadTime").ToDateTime().ToString("yyyy-MM-dd") %>' />
                </ItemTemplate>
            </asp:TemplateField>           
            <asp:TemplateField HeaderText="所属项目" HeaderStyle-Width="190">
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="lblItem" runat="server" Text='<%#Eval("ItemName")==DBNull.Value?"":Eval("ItemName").ToString() %>' />
                </ItemTemplate>
            </asp:TemplateField> 
             <asp:TemplateField HeaderText="实践名称" HeaderStyle-Width="230">
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="lblPracticeName" runat="server" Text='<%#Eval("JobName")==DBNull.Value?"":Eval("JobName").ToString() %>' />
                </ItemTemplate>
            </asp:TemplateField>   
            <asp:TemplateField HeaderText="实践方案" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle HorizontalAlign="Center" />
                <ItemTemplate>
                   <asp:HyperLink ID="hlUploadFileName" runat="server" Text='<%#Eval("UploadFileName") %>' NavigateUrl='<%#ETMS.Utility.StaticResourceUtility.GetFullPathByFileType("OfflineJob", Eval("UploadFilePath").ToString()) %>' />
                </ItemTemplate>
            </asp:TemplateField>   
            <asp:TemplateField HeaderText="操作" HeaderStyle-Width="60" HeaderStyle-CssClass="aligncenter">
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle HorizontalAlign="Center" />
                <ItemTemplate>
                      <asp:LinkButton ID="ltnEvaluation" runat="server" Text="批阅" />
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