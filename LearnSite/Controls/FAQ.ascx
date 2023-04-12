<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FAQ.ascx.cs" Inherits="ETMS.Studying.Controls.FAQ" %>

<%@ Register Assembly="ETMS.Editor" Namespace="ETMS.Editor.UEditor" TagPrefix="cc1" %>
<script src="<%=ETMS.Utility.WebUtility.AppPath%>/Scripts/faq.js"></script>
<script>
    var ContentID = '<%=ContentID%>';
    var TrainingItemCourseID = '<%=TrainingItemCourseID%>';
</script>
<div class="question_tab">
    <span type="1" class="cur">我的问题</span>
    <span type="0">大家的问题</span>
</div>
<div class="u-studying-faq-new">
    <span id="span_question_operating_add">新建问答</span>
    <div class="u-studying-faq-new-box">
        <div class="u-studying-faq-new-title">标题：<input type="text" id="Txt_QuestionTitle" /></div>
        <div class="u-studying-faq-new-editor">
        </div>
        <div class="u-studying-faq-new-submit">
            <input type="button" id="btn_question_item_add" value="保存" /><input type="button" id="btn_question_item_cancel" value="取消" />
        </div>
    </div>
</div>
<div id="divContext"></div>
<div class="question-more">
    <input id="btn_QuestionMore" type="button" value="显示更多问题" />
</div>
<div id="div_show" style="display: none">
    <cc1:UEditor ID="Editor1" runat="server" Width="338px" Height="80px" ToolType="NotesAndQuestion"></cc1:UEditor>
    <div id="div_Editor_operating">
        <div class="u-question-answer-operating-add">
            <input id="btn_answer_add" type="button" value="回复" />
        </div>
    </div>
</div>

<div id="div_TempEditor" style="display: none"></div>
<script id="QuestionItem" type="text/x-jquery-tmpl">
    <div class="u-studying-faq-box">
        <div class="u-studying-faq-text">
            <div class="u-studying-faq-top">{{if RealName}}${RealName}{{else}}${LoginName}{{/if}}<span class="u-studying-faq-time">${CreateTime}</span></div>
            <p class="u-studying-faq-text-title">{{html QuestionTitle}}</p>
            <div class="u-studying-faq-text-content-show">
                <span class="u-questioncontent_show_hide"><span class='span-show-pic'>&nbsp;&nbsp;&nbsp;</span>展开</span>
                {{html QuestionContent}}
            </div>
            <div class="u-studying-faq-bottom">
                <ul>
                    <li class="u-studying-faq-noborder-questionitem"><span id="span_question_answercount_${QuestionID}" class="u-studying-faq-reply">${AnswerCount}</span></li>
                    {{if UserID==PersonalUserID }}<li><span class="u-studying-faq-delete"></span></li>
                    {{/if}}
                </ul>
            </div>
            <div class="u-studying-faq-open" id="div_question_answer_list_${QuestionID}">
                <span class="u-studying-faq-up"></span>
                <div class="u-studying-faq-open-box">
                    <div id="div_question_answer_text_${QuestionID}"></div>
                    <div id="div_question_answer_item_${QuestionID}">{{tmpl($data) '#AnswerItem'}}</div>
                </div>
            </div>
        </div>
    </div>
</script>
<script id="AnswerItem" type="text/x-jquery-tmpl">
    {{each QAAnswers}}
    <div class="u-studying-faq-one  u-studying-faq-one-reply ">
        <div class="u-studying-faq-top">
            {{if RealName}}${RealName}{{else}}${LoginName}{{/if}}<span class="u-studying-faq-time">${CreateTime}</span>
        </div>
        <span>{{html AnswerContent}}</span>
        <div class="u-studying-faq-bottom">
            <ul>
                {{if UserID==PersonalUserID }}
                <li><span class="u-studying-faq-answer-delete" data-answerid="${AnswerID}" data-questionid="${QuestionID}"></span></li>
                {{/if}}
            </ul>
        </div>
    </div>
    {{/each}}
</script>
