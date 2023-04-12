<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TestpaperInfoSenior.ascx.cs"
    Inherits="QuestionDB_TestPaper_Controls_TestpaperInfoSenior" %>
<div style="background-color: #fbf1ef; padding: 10px; border: 1px solid #c03218;">
    <div style="font-weight: bold; padding: 5px;">
        【设置策略】</div>
    <div>
        <table border="1">
            <tr>
                <td style="width: 8%" align="center">
                    <b>题型</b>
                </td>
                <td colspan="2" style="width: 22%" align="center">
                    易
                </td>
                <td colspan="2" style="width: 22%" align="center">
                    中
                </td>
                <td colspan="2" style="width: 22%" align="center">
                    难
                </td>
                <td colspan="2" align="center" style="width: 24%">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 10%" align="center">
                    &nbsp;
                </td>
                <td style="width: 10%" align="center">
                    试题数量
                </td>
                <td style="width: 10%" align="center">
                    抽取数
                </td>
                <td style="width: 10%" align="center">
                    试题数量
                </td>
                <td style="width: 10%" align="center">
                    抽取数
                </td>
                <td style="width: 10%" align="center">
                    试题数量
                </td>
                <td style="width: 10%" align="center">
                    抽取数
                </td>
                <td align="center" style="width: 15%">
                    <b>抽取总数</b>
                </td>
                <td align="center" style="width: 15%">
                    <b>分数</b>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <b>单选题</b>
                </td>
                <td align="right">
                    <span id ="ltlSingleChoiceEasySum"><%=SingleChoiceEasySum%></span>
                </td>
                <td align="right">
                    <input type="text" id="txtSingleChoiceEasy" class="inputbox_60 alignright" value='<%=SingleChoiceLowSelectQty %>' onchange="javascript:setQuestionCount();" />
                </td>
                <td align="right">
                    <span id ="ltlSingleChoiceMediumSum"><%=SingleChoiceMediumSum%></span>
                </td>
                <td align="right">
                    <input type="text" id="txtSingleChoiceMedium" class="inputbox_60 alignright" value='<%=SingleChoiceMediumSelectQty %>' onchange="javascript:setQuestionCount();"/>
                </td>
                <td align="right">
                    <span id ="ltlSingleChoiceHardSum"><%=SingleChoiceHardSum%></span>
                </td>
                <td align="right">
                    <input type="text" id="txtSingleChoiceHard" class="inputbox_60 alignright" value='<%=SingleChoiceHighSelectQty %>'   onchange="javascript:setQuestionCount();"/>
                </td>
                <td align="right">
                    <span id ="ltlSingleChoiceSum"><%=SingleChoiceSum%></span>
                </td>
                <td align="right">
                    <input type="text" id="txtSingleChoiceScore" class="inputbox_60 alignright" value='<%=SingleChoiceQuestionScore %>'   onchange="javascript:setQuestionCount();"/>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <b>多选题</b>
                </td>
                <td align="right">
                    <span id ="ltlMultipleChoiceEasySum"><%=MultipleChoiceEasySum%></span>
                </td>
                <td align="right">
                    <input type="text" id="txtMultipleChoiceEasy" class="inputbox_60 alignright" value='<%=MultipleChoiceLowSelectQty %>' onchange="javascript:setQuestionCount();"/>
                </td>
                <td align="right">
                    <span id ="ltlMultipleChoiceMediumSum"><%=MultipleChoiceMediumSum%></span>
                </td>
                <td align="right">
                    <input type="text" id="txtMultipleChoiceMedium" class="inputbox_60 alignright" value='<%=MultipleChoiceMediumSelectQty %>'  onchange="javascript:setQuestionCount();"/>

                </td>
                <td align="right">
                    <span id ="ltlMultipleChoiceHardSum"><%=MultipleChoiceHardSum%></span>
                </td>
                <td align="right">
                    <input type="text" id="txtMultipleChoiceHard" class="inputbox_60 alignright" value='<%=MultipleChoiceHighSelectQty %>' onchange="javascript:setQuestionCount();"/>
                </td>
                <td align="right">
                    <span id ="ltlMultipleChoiceSum"><%=MultipleChoiceSum%></span>
                </td>
                <td align="right">
                    <input type="text" id="txtMultipleChoiceScore" class="inputbox_60 alignright" value='<%=MultipleChoiceQuestionScore %>'  onchange="javascript:setQuestionCount();"/>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <b>判断题</b>
                </td>
                <td align="right">
                    <span id ="ltlJudgementEasySum"><%=JudgementEasySum%></span>
                </td>
                <td align="right">
                    <input type="text" id="txtJudgementEasy" class="inputbox_60 alignright" value='<%=JudgementLowSelectQty %>' onchange="javascript:setQuestionCount();"/>
                </td>
                <td align="right">
                    <span id ="ltlJudgementMediumSum"><%=JudgementMediumSum%></span>
                </td>
                <td align="right">
                    <input type="text" id="txtJudgementMedium" class="inputbox_60 alignright" value='<%=JudgementMediumSelectQty %>' onchange="javascript:setQuestionCount();"/>
                </td>
                <td align="right">
                    <span id ="ltlJudgementHardSum"><%=JudgementHardSum%></span>
                </td>
                <td align="right">
                    <input type="text" id="txtJudgementHard" class="inputbox_60 alignright" value='<%=JudgementHighSelectQty %>' onchange="javascript:setQuestionCount();"/>
                </td>
                <td align="right">
                    <span id ="ltlJudgementSum"><%=JudgementSum%></span>
                </td>
                <td align="right">
                    <input type="text" id="txtJudgementScore" class="inputbox_60 alignright" value='<%=JudgementQuestionScore %>' onchange="javascript:setQuestionCount();"/>
                </td>
            </tr>
            <tr style="background-color:#fcebe7;">
                <td align="center" colspan="7">
                    <b>合计</b>
                </td>
                <td align="right">
                    <span id ="ltlQuestionSum" style="color:Red;"><%=QuestionSum%></span>
                </td>
                <td align="right">
                    <span id ="ltlQuestionScoreSum" style="color:Red;"><%=QuestionScoreSum%></span>
                </td>
            </tr>
        </table>
        <p style="text-align: center;">
            <asp:Button ID="lbtnSave"  OnClick="lbtnSave_Click" runat="server" CssClass="btn_SaveFour" Text="保存策略"></asp:Button>
            <asp:HiddenField ID="hfldTestPaperRule" runat="server" />
            </p>
    </div>
</div>
