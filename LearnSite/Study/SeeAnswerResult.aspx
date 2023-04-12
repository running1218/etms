<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeBehind="SeeAnswerResult.aspx.cs" Inherits="ETMS.Studying.Study.SeeAnswerResult" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultPlaceHolder" runat="server">
    <link href="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/doHomeWork.css" type="text/css" rel="stylesheet" />
    <script src="<%=ETMS.Utility.WebUtility.AppPath%>/Scripts/library/lodash/dist/lodash.min.js"></script>
    <script src="<%=ETMS.Utility.WebUtility.AppPath%>/Scripts/library/handlebars/handlebars.js"></script>
    <script src="<%=ETMS.Utility.WebUtility.AppPath%>/Scripts/seeAnswerResult.js"></script>
    <div class="container">
         <%-- 作业信息 --%>
         <div class="title-content-style workontop">
                <div class="homework_info">
                    <div class="worktitle"></div>
                    <p class="score">
                        <span class="marginr30">共<span class="jobcount"></span>题，<span class="jobscore"></span>分</span>
                        <span class="marginr30">时间：<span class="duration"></span></span>
                        <span class="marginr30 times">允许提交次数：<span class="limittimes"></span>次</span>
                        <span class="times">已提交：<span class="answertimes"></span>次</span>
                    </p>
                </div>
                <div class="stunews-right">
                    <p class="ftsanswer">0分</p>
                </div>
            </div>
         <%-- 试题信息 --%>
         <div class="test_mainContent">
             <div class="left_content">
                 <%-- 单选题 --%>
                 <div class="question question-radio">     
                     <div class="question-type">
                         <span class="item-type radioques">一、单选题</span>
                         <span class="quesnum radionums"></span>
                     </div>    
                      <div class="radio_list">                  
                            
                      </div> 
                 </div>
                  <%-- 多选题 --%>
                 <div class="question question-check">     
                     <div class="question-type">
                         <span class="item-type checkques">二、多选题</span>
                         <span class="quesnum checknums">(共1题，4分)</span>
                     </div>    
                      <div class="check_list">                  
                            
                      </div> 
                 </div>
                  <%-- 判断题 --%>
                 <div class="question question-judge">     
                     <div class="question-type">
                         <span class="item-type judgeques">三、判断题</span>
                         <span class="quesnum judgenums">(共1题，4分)</span>
                     </div>    
                      <div class="judge_list">                  
                            
                      </div> 
                 </div>
             </div>
             <div class="right_content">
                 <div class="resultshow">
                        <div class="chioce-btn answerguide">
                            <dl class="question-result-item clearfix "> 
                                    <dt class="question-resul-title sigletopics "></dt>
                                    <dd class="question-resul-tab  radio-tab">
                                        
                                    </dd>
                            </dl>
                            <dl class="question-result-item clearfix">              
                                <dt class="question-resul-title checkbox "></dt>  
                                <dd class="question-resul-tab  check-tab">

                                </dd>
                            </dl>
                            <dl class="question-result-item clearfix">
                                <dt class="question-resul-title judge"></dt>
                                <dd class="question-resul-tab   judge-tab">

                                </dd>
                            </dl>
                        </div>
                        <ul class="question-result-explain">
                            <li><i class="icon-explain marki"></i>标记</li>
                            <li><i class="icon-explain donei"></i>已做</li>
                            <li><i class="icon-explain undo"></i>未做</li>
                        </ul>
                        <div class="right-bottom">
                            <input type="button" id="close" class="hm_btn_ash" value="关闭">
                        </div>
                  </div>
             </div>
         </div>
     </div>
     <script id="radioDataTPLT" type="text/x-handlebars-template">
        {{#each radioData}}
        <div class="question-cont" name="{{QuestionID}}" id="{{QuestionID}}">             
            <div class="question-cont-left">                
                <span class="question-num">{{addOne @index}}</span>
                <span class="question-score">{{QuestionScore}}分</span>
                <%--<a  href="javascript:void(0)" class="question-mark" questionID="{{QuestionID}}"><i class="mark"></i>标记</a>--%>            
            </div>             
            <div class="question-cont-right" index="{{@index}}" iteamId="{{QuestionID}}">                 
                <div class="question-text">
                    {{QuestionTitle}}
                </div>                                
                <ul class="question-options"> 
                    {{Continue QuestionOption UserAnswer}}
                   <%-- {{#each QuestionOption}}
                        <li flag="1" optionID="{{OptionID}}"><a href="javascript:void(0)"><i class="options">{{OptionCode}}</i><span>{{OptionContent}}</span></a></li>
                    {{/each}}--%>
                </ul>     
                {{renderAnswer QuestionType QuestionAnswer}}                                         
            </div>         
        </div>
        {{/each}}
    </script>
    <script id="checkDataTPLT" type="text/x-handlebars-template">
        {{#each checkData}}
        <div class="question-cont" name="{{QuestionID}}" id="{{QuestionID}}">             
            <div class="question-cont-left">                
                <span class="question-num">{{addOne @index}}</span>
                <span class="question-score">{{QuestionScore}}分</span>
                <%--<a  href="javascript:void(0)" class="question-mark" questionID="{{QuestionID}}"><i class="mark"></i>标记</a>--%>            
            </div>             
            <div class="question-cont-right" index="{{@index}}" iteamId="{{QuestionID}}">                 
                <div class="question-text">
                    {{QuestionTitle}}
                </div>                                
                <ul class="question-options">  
                    {{#each QuestionOption}}
                        <li flag="1" optionID="{{OptionID}}"><a href="javascript:void(0)"><i class="options-more">{{OptionCode}}</i><span>{{OptionContent}}</span></a></li>  
                    {{/each}}                                                            
                </ul>   
                {{renderAnswer QuestionType QuestionAnswer}}                                              
            </div>         
        </div>
        {{/each}}
    </script>
     <script id="judgeDataTPLT" type="text/x-handlebars-template">
        {{#each judgeData}}
        <div class="question-cont" name="{{QuestionID}}" id="{{QuestionID}}">             
            <div class="question-cont-left">                
                <span class="question-num">{{addOne @index}}</span>
                <span class="question-score">{{QuestionScore}}分</span>
                <%--<a  href="javascript:void(0)" class="question-mark" questionID="{{QuestionID}}"><i class="mark"></i>标记</a>--%>             
            </div>             
            <div class="question-cont-right" index="{{@index}}" iteamId="{{QuestionID}}">                 
                <div class="question-text">
                    {{QuestionTitle}}
                </div>                                
                <ul class="question-options"> 
                    {{ContinueJudge QuestionOption UserAnswer}} 
                  <%--  {{#each QuestionOption}}
                        <label flag="1" optionID="{{OptionID}}"><li><input class="radios" type="radio" name="{{QuestionID}}">&nbsp;&nbsp;<span>{{OptionContent}}</span></li></label>
                    {{/each}}     --%>                                                       
                </ul>   
                {{renderAnswer QuestionType QuestionAnswer}}                                              
            </div>         
        </div>
        {{/each}}
    </script>
</asp:Content>
