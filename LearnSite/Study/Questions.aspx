<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Study.master" AutoEventWireup="true" CodeBehind="Questions.aspx.cs" Inherits="ETMS.Studying.Study.Questions" %>
<%@ Register Src="~/Controls/Course/LeftChapterTree.ascx" TagPrefix="uc1" TagName="LeftChapterTree" %>
<%@ Register Assembly="ETMS.Editor" Namespace="ETMS.Editor.UEditor" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="StudyPlaceHolder" runat="server">
    <script src="<%=ETMS.Utility.WebUtility.AppPath%>/Scripts/library/jquery.livequery.js"></script>
    <script src="<%=ETMS.Utility.WebUtility.AppPath%>/Scripts/library/jquery.tmpl.js"></script>
    <script src="<%=ETMS.Utility.WebUtility.AppPath%>/Scripts/Questions.js"></script>
    <script type="text/ecmascript">
         var TrainingItemCourseID = '<%=TrainingItemCourseID%>';
    </script>
    <div class="Notes-Context">
        <div class="Notes-Context-left">
            <uc1:LeftChapterTree runat="server" ID="LeftChapterTree" />
        </div>
        <div class="Notes-Context-right">
            <div class="Notes-coursmenu">
                <ul>
                    <li>
                        <a id="btn_PersonalQuestion" class="activemenu" data-userid="<%= University.Mooc.AppContext.UserContext.Current.UserID %>" href="javascript:void(0)">我的问题</a></li>
                    <li class="Notes-split"></li>
                    <li>
                        <a id="btn_QuestionAll" href="javascript:void(0)">大家的问题</a></li>
                </ul>
                <a href="javascript:void(0)" class="question-operating-add">新建</a>
            </div>
            <div class="question-item-add">
                <div class="title">
                    <span>&nbsp;标题：<input id="Txt_QuestionTitle" type="text" /></span>
                </div>
                <div class="content"></div>
                <div class="operating"><input id="btn_question_item_add" value="保存" type="button" /><input id="btn_question_item_cancel" value="取消" type="button" /></div>
            </div>
            <div class="question-item-sort">排序：
                <span id="question_list_datesort" class="date-sort-desc">日期</span>
                <span id="question_list_answercountsort" class="answer-count-sort-none">回复数</span></div>
            <div id="divContext" class="Question-list">
            </div>
            <div class="question-more">
                <input id="btn_QuestionMore" type="button" value="显示更多问题" />
            </div>
        </div>
    </div>
    <div id="div_show" style="display: none">
        <cc1:UEditor ID="Editor1" runat="server" Width="790px" Height="80px" ToolType="NotesAndQuestion"></cc1:UEditor>
        <div id="div_Editor_operating">
            <div class="question-answer-operating-add"><input id="btn_answer_add" type="button" value="回复" /></div>
        </div>
    </div>
    <div id="div_TempEditor" style="display: none" ></div>
    <script id="QuestionItem" type="text/x-jquery-tmpl">
        <div class='QuestionTmpl-item'>
            <div class="QuestionTmpl-item-title">{{html QuestionTitle}}</div>
            <div class='QuestionTmpl-item-content-show'><span class="span_show_hide"><span class='span-show-pic'>&nbsp;&nbsp;&nbsp;</span>展开</span>
                {{html QuestionContent}}
            </div>
            <div class='QuestionTmpl-item-operating'>
                <span class='title'>{{if RealName}}${RealName}{{else}}${LoginName}{{/if}}&nbsp;&nbsp;&nbsp;&nbsp;${CreateTime}</span>
                <span class="answer" title="回复">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span id="question_answer_count_${QuestionID}" class="answer-count">${AnswerCount}</span></span>
                {{if UserID==PersonalUserID }}
                <span class='verticalbar'></span>
                <span class='remove' title="删除"></span>
                {{/if}}
            </div>
            <div id='div_question_answer_${QuestionID}' class="QuestionTmpl-AnswerList">
                <div id="div_question_answer_text_${QuestionID}"class="div_question_answer_text" style="margin:10px 0px 0px 10px;">
                </div>
                <div id="div_question_answer_list_${QuestionID}">
                {{tmpl($data) '#AnswerItem'}}</div>
            </div>
        </div>
    </script>
    <script id="AnswerItem" type="text/x-jquery-tmpl">
        {{each QAAnswers}}
        <div class='QuestionTmpl-Answer-item'>
            <div class='QuestionTmpl-Answer-item-content'>
                {{html AnswerContent}}
            </div>
            <div class='QuestionTmpl-Answer-item-operating'>
                <span class='title'>{{if RealName}}${RealName}{{else}}${LoginName}{{/if}}&nbsp;&nbsp;&nbsp;&nbsp;${CreateTime}</span>
                {{if UserID==PersonalUserID }}
                <span class='verticalbar' style="background:none;"></span>
                <span class='remove' title="删除" data-answerid="${AnswerID}" data-questionid="${QuestionID}"></span>
                {{/if}}
            </div>
        </div>
        {{/each}}
    </script>
    <script>
        $('.study_modular li').removeClass('cur').eq(4).addClass('cur');
    </script>
</asp:Content>
