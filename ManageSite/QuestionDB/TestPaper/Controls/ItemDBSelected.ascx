<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemDBSelected.ascx.cs"
    Inherits="QuestionDB_TestPaper_Controls_ItemDBSelected" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<div style="background-color: #fbf1ef; padding: 10px; border: 1px solid #c03218;">
    <div style="text-indent: 45px; font-weight: bold; padding: 5px;">
        【试题汇总及设置分值】</div>
    <div>
        <%--已添加的试题列表--%>
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <th style="width: 60px;">
                        题型：
                    </th>
                    <td style="width: 120px;">
                        <select id="select_QuestionType" class="select_100">
                            <option selected="selected" value="">全部</option>
                            <option value="单选题">单选题</option>
                            <option value="多选题">多选题</option>
                            <option value="判断题">判断题</option>
                        </select>
                    </td>
                    <th style="width: 100px;">
                        批量设置分值：
                    </th>
                    <td>
                        <input type="text" id="txtScoreBatch" class="inputbox_40" />
                        <input type="button" onclick="javascript:setScoreBatch();"  value="设置" class="btn_Configer"/>
                    </td>
                </tr>
            </table>
        </div>
        <div class="dv_searchlist" id="div_itemSelected">
            <!--列表 begin-->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" CssClass="GridviewGray" EmptyDataSourceTip="没有任何记录！"
                AutoCreateColumnInsertIndex="0" AutoGenerateColumns="False" CurrentPageIndex="1"
                CustomAllowPaging="False" IsEmpty="False" TotalRecordCount="0" DataKeyNames="QuestionID"
                OnRowCommand="CustomGridView1_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="序号" HeaderStyle-CssClass="aligncenter">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" Width="40" />
                        <ItemTemplate>
                            <asp:Label ID="LabNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="题目名称" HeaderStyle-CssClass="alignleft widthauto" ItemStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblQuestionTitle" runat="server" ShowTextNum="25" Text='<%# Eval("QuestionTitle")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="题型" HeaderStyle-Width="60" HeaderStyle-CssClass="aligncenter" ItemStyle-CssClass="aligncenter" >
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblQuestionType" DictionaryType="Dic_QuestionType" FieldIDValue='<%# (int)((ETMS.Components.Exam.API.Entity.ItemBank.QuestionType)Eval("QuestionType")) %>'
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="难度" HeaderStyle-Width="60"  HeaderStyle-CssClass="aligncenter">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblDifficulty" DictionaryType="Dic_DegreeDifficulty" FieldIDValue='<%# Eval("Difficulty") %>'
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="分数" HeaderStyle-Width="60" HeaderStyle-CssClass="aligncenter">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:TextBox ID="txtScore" ToolTip='<%# getQuestionTypeName(Eval("QuestionType").ToString()) %>'
                                runat="server" CssClass="inputbox_40" Text='<%# string.Format("{0:N1}",Eval("QuestionScore").ToString().ToDecimal())  %>'
                                onchange="javascript:SumScore();"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作" HeaderStyle-Width="60"  HeaderStyle-CssClass="aligncenter" >
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <cc1:CustomLinkButton runat="server" ID="lbtn_Del" CommandArgument='<%# Eval("QuestionID") %>'
                                CommandName="Del" Text="删除" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="确定要删除吗？" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </cc1:CustomGridView>
            <!--列表 end-->
            <div class="dv_splitLine"> </div>
            <p style="font-weight: bold;">
                合计：共<span style="color: Red;"><asp:Literal ID="ltlItemCount" runat="server"></asp:Literal></span>道题，总分<span
                    style="color: Red;" id="spanScoreCount">0</span>分</p>
        </div>
        <p style="text-align: center;">
            <asp:LinkButton ID="lbtnSave" runat="server" CssClass="btn_Save" OnClick="lbtnSave_Click"
                OnClientClick="javascript:return checkHundredScore();">保存</asp:LinkButton>
        </p>
        <script language="javascript" type="text/javascript">            SumScore();</script>
    </div>
</div>
