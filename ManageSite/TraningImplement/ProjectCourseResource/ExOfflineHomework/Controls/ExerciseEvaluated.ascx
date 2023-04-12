<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ExerciseEvaluated.ascx.cs" Inherits="TraningImplement_ProjectCourseResource_ExOfflineHomework_Controls_ExerciseEvaluated" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<!--查找结果-->
<div class="dv_searchlist ">
    <!--翻页-->
    <div class="dv_pagePanel">
        <div class="dv_pageControl">
            <uc2:PageSet ID="PageSet2" runat="server" />
        </div>
    </div>
    <!--列表 begin-->
    <cc1:CustomGridView ID="GridViewList" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridViewList_RowDataBound">
        <Columns>
            <asp:BoundField DataField="LoginName" HeaderText="帐号" SortExpression="JobName" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft" HeaderStyle-Width="60">
                <ItemStyle HorizontalAlign="Left" CssClass="alignleft" />
                <HeaderStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="RealName" HeaderText="学员姓名" SortExpression="JobName" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft" HeaderStyle-Width="80">
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
            <asp:TemplateField HeaderText="学员实践" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle HorizontalAlign="Center" />
                <ItemTemplate>
                   <asp:HyperLink ID="hlUploadFileName" runat="server" Text='<%#Eval("UploadFileName") %>' NavigateUrl='<%#ETMS.Utility.StaticResourceUtility.GetFullPathByFileType("OfflineJob", Eval("UploadFilePath").ToString()) %>' />
                </ItemTemplate>
            </asp:TemplateField> 
            <asp:TemplateField HeaderText="评阅时间" HeaderStyle-Width="80">
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="lblEvaluationTime" runat="server" Text='<%#Eval("EvaluationTime")==DBNull.Value?"":Eval("EvaluationTime").ToDateTime().ToString("yyyy-MM-dd") %>' />
                </ItemTemplate>
            </asp:TemplateField>          
            <asp:TemplateField HeaderText="批阅附件" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle HorizontalAlign="Center" />
                <ItemTemplate>
                   <asp:HyperLink ID="hlMarkFileName" runat="server" Text='<%#Eval("MarkFileName") %>' NavigateUrl='<%#ETMS.Utility.StaticResourceUtility.GetFullPathByFileType("OfflineJob", Eval("MarkFilePath").ToString()) %>' />
                </ItemTemplate>
            </asp:TemplateField>   
            <asp:TemplateField HeaderText="操作" HeaderStyle-Width="60">
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle HorizontalAlign="Center" />
                <ItemTemplate>
                      <asp:LinkButton ID="ltnEvaluation" runat="server" Text="查看" />
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