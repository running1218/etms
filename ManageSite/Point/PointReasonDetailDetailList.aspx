<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="PointReasonDetailDetailList.aspx.cs" Inherits="Point_PointReasonDetailDetailList" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" Runat="Server">
<a id="aBack" runat="server" class="btn_Return">返回</a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <div class="dv_GradeviewList">
            <table >
                <tr  >
                    <th>
                         培训项目：
                    </th>
                    <td colspan="3">                      
                        <cc1:ShortTextLabel ID="lblItemName" runat="server" ShowTextNum="20"/>
                   </td>                   
                </tr>   
                <tr>
                   <th >
                         班&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;级：
                    </th>
                    <td >                      
                        <cc1:ShortTextLabel ID="lblClassName" runat="server" ShowTextNum="10"/>
                   </td>   
                   <th >
                        学习群组：
                    </th>
                    <td >                      
                       <cc1:ShortTextLabel ID="lblGroupName" runat="server" ShowTextNum="10"/>
                   </td>                    
                </tr>     
                <tr>
                   <th>
                         学员姓名：
                    </th>
                    <td >                      
                        <cc1:ShortTextLabel ID="lblRealName" runat="server" ShowTextNum="10"/>
                   </td>   
                   <th>
                       组织机构：
                    </th>
                    <td > 
                        <cc1:DictionaryLabel ID="lblOrgID" runat="server"  DictionaryType="vw_Dic_Sys_Organization" TextLength="10" />                     
                   </td>                    
                </tr> 
                <tr>
                   <th>部&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;门：</th>
                    <td >                      
                         <cc1:DictionaryLabel ID="lblDepartment" runat="server"  DictionaryType="vw_Dic_Sys_Department" TextLength="10" /> 
                   </td> 
                   <th>未发布积分：</th>
                    <td >                      
                         <cc1:ShortTextLabel ID="lblSumPoint" runat="server" ShowTextNum="10"/>
                         <div style="display:none">
                         <asp:Button ID="btnSearch" runat="server" CssClass="btn_Search" Text="查询" OnClick="btnSearch_Click" />   
                         </div>
                   </td>             
                </tr>            
            </table>
        </div>
      <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel">
                <div class="dv_selectAll">
                    <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" Visible="false" />
                    <input type="button" class="btn_Add" value="新增" onclick="javascript:showWindow('新增积分','<%=this.ActionHref( string.Format("PointReasonDetailDetailListInfo.aspx?TrainingItemID={0}&StudentSignupID={1}",TrainingItemID,StudentSignupID))%>')" />
                 <%-- <cc1:CustomButton CssClass="btn_Del" Text="删除" runat="server" ID="btnDeletes" EnableConfirm="true"
                        ConfirmMessage="确信要执行“批量删除”操作吗?" OnClick="btnDeletes_Click" /> --%>
                </div>
                <%--<div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>--%>
            </div>
            <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="StudentPointReasonDetailID"
                OnRowDataBound="CustomGridView1_RowDataBound" OnRowCommand="CustomGridView1_RowCommand">
                <columns>                    
                        <asp:TemplateField HeaderText="积分原因类型" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                             <ItemTemplate>
                                 <cc1:ShortTextLabel ID="lblStudentPointReasonRoleID" runat="server" ShowTextNum="10" Text='<%#Eval("PointReasonTypeName") %>'/>
                             </ItemTemplate>
                        </asp:TemplateField>                        
			        
                        <asp:TemplateField HeaderText="获得积分原因" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                             <ItemTemplate>
                                 <cc1:ShortTextLabel ID="lblPointReason" runat="server" ShowTextNum="10" Text='<%#Eval("PointReason") %>' />
                             </ItemTemplate>
                        </asp:TemplateField>
			        
                        <asp:BoundField DataField="AccessPoints" HeaderText="积分" SortExpression="AccessPoints" HeaderStyle-Width="60" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright"/>
                        			        
                        <asp:TemplateField HeaderText="创建时间" HeaderStyle-Width="80">
                             <ItemTemplate>
                                 <asp:Label ID="lblCreateTime" runat="server" ShowTextNum="10" Text='<%#Eval("CreateTime").ToDate() %>' />
                             </ItemTemplate>
                        </asp:TemplateField>	        
                       
                        <asp:BoundField DataField="CreateUser" HeaderText="创建人" SortExpression="CreateUser" HeaderStyle-CssClass="field8"/>
                        
                        <asp:TemplateField HeaderText="备注" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                             <ItemTemplate>
                                 <cc1:ShortTextLabel ID="lblRemark" runat="server" ShowTextNum="10" Text='<%#Eval("Remark") %>' />
                             </ItemTemplate>
                        </asp:TemplateField>                        
                         <asp:TemplateField HeaderText="操作" HeaderStyle-Width="80">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>                         
                             <asp:LinkButton ID="lblEdit" runat="server" Text="编辑"/>
                             <cc1:CustomLinkButton runat="server" ID="lblDel" CommandArgument='<%#Eval("StudentPointReasonDetailID") %>'
                            CommandName="del" Text="删除" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="确定要删除吗？" />                           
                        </ItemTemplate>
                    </asp:TemplateField>		           
                    
                </columns>
            </cc1:CustomGridView> 
            <!--列表 end-->
            <div class="dv_splitLine">
            </div>
            <!--翻页-->
            <div class="dv_pagePanel">
            </div>
        </div>        
</asp:Content>

