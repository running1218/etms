<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Study.master" AutoEventWireup="true" CodeBehind="Evaluation.aspx.cs" Inherits="ETMS.Studying.Study.Evaluation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="StudyPlaceHolder" runat="server">
    <link href="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/evaluation.css" type="text/css" rel="stylesheet" />
    <div class="view-area">
        <ul class="text_list">
               <asp:Repeater ID="rptEvaluationHomework" runat="server">
                    <ItemTemplate>
                         <li>
                            <div class="text_info">
                                <p class="text_name"><i class="homework">作业</i><span><%# Eval("TestName")%></span></p>
                                <div class="additional_info">
                                    <p>时间：<span><span class="start_time"><%# Eval("ResBeginTime")%></span>至<span class="end_time"><%# Eval("ResEndTime")%></span></span></p>              
                                    <p>状态：<span class="status"><%# Eval("TestStatus")%></span></p>              
                                    <p>分数：<span><%# Eval("Score").ToInt()==0?"--":Eval("Score")%></span></p>
                                </div>
                            </div>
                            <a style="display:none;" href='<%# getUrl(Eval("TestPaperID").ToGuid(), Eval("TrainingItemCourseID").ToGuid(), Eval("TestID").ToGuid(), Eval("StudentCourseID").ToGuid(), 2)%>' class="text_btn do_homework">做作业</a>
                            <a target="_blank" style="border:1px solid #999;color:#999;display:none;" href='<%# getViewUrl(Eval("TestPaperID").ToGuid(), Eval("TrainingItemCourseID").ToGuid(), Eval("TestID").ToGuid(), Eval("StudentCourseID").ToGuid(), 2, Eval("UserExamID").ToString())%>' class="text_btn see_homework">查看作业</a>
                        </li>
                    </ItemTemplate>
               </asp:Repeater>
              <asp:Repeater ID="rptEvaluationTest" runat="server">
                    <ItemTemplate>
                         <li>
                            <div class="text_info">
                                <p class="text_name"><i class="evaluation">测试</i><span><%# Eval("TestName")%></span></p>
                                <div class="additional_info">
                                    <p>时间：<span><span class="start_time"><%# Eval("ResBeginTime")%></span>至<span class="end_time"><%# Eval("ResEndTime")%></span></span></p>              
                                    <p>时长：<span><%# Eval("limitTime")%>分钟</span></p>   
                                    <p>允许提交次数：<span class="testCount"><%# Eval("TestCount")%></span>次</p> 
                                    <p>提交次数：<a href='javascript:void(0);'><span class="userTestCount"><%# Eval("UserTestCount")%></span>次</a></p>                
                                    <p>分数：<span><%# Eval("Score").ToInt()==0?"--":Eval("Score")%></span></p>
                                </div>
                            </div>
                            <a target="_blank" href='<%# getViewUrl(Eval("TestPaperID").ToGuid(), Eval("TrainingItemCourseID").ToGuid(), Eval("TestID").ToGuid(), Eval("StudentCourseID").ToGuid(),5)%>' style="border:1px solid #999;color:#999;display:none;" class="text_btn see_evaluation">查看测试</a>
                            <a style="display:none;" href='<%# getUrl(Eval("TestPaperID").ToGuid(), Eval("TrainingItemCourseID").ToGuid(), Eval("TestID").ToGuid(), Eval("StudentCourseID").ToGuid(), 5)%>' class="text_btn do_evaluation">开始测试</a>
                        </li>
                    </ItemTemplate>
               </asp:Repeater>
               <asp:Repeater ID="rptOffLineJob" runat="server">
                    <ItemTemplate>
                        <li>
                            <div class="text_info">
                                <p class="text_name"><i class="offlinejob">离线</i><span><%# Eval("JobName")%></span></p>
                                <div class="additional_info">
                                    <p>时间：<span><span class="start_time"><%# Eval("BeginTime").ToDate()%></span>至<span class="end_time"><%# Eval("EndTime").ToDate()%></span></span></p>              
                                    <p>状态：<span class="status"><%# Eval("StudentJobID") == DBNull.Value ? "未提交" : Eval("StudentJobStatus").ToString().ToInt() == 0? "已提交":"已批阅"%></span></p>              
                                    <%--<p>分数：<span><%# Eval("Score") == DBNull.Value ? "--" : Eval("Score").ToInt()==0?"--":Eval("Score")%></span></p>--%>
                                </div>                                
                            </div>
                            <a id="doOfflineJob" class="text_btn do_offlinejob" href="javascript:showLayerWindow('上传离线作业', '<%# string.Format("UpOffLineJob.aspx?JobID={0}&ItemCourseOffLineJobID={1}&StudentJobID={2}", Eval("JobID"),Eval("ItemCourseOffLineJobID"),Eval("StudentJobID"))%>', 600, 365)">做作业</a>
                            <div class="off_line_oper">
                                <p class="additional_info">
                                    <span class="offline_title">评语：<span>
                                    <span style="color:black"><%# Eval("JobDescription") %></span>
                                </p>
                            </div>
                            <div class="off_line_oper">
                                <div class="text_info">
                                    <span class="offline_title">作业附件：<a href="<%# string.Format("{0}/ExOfflineHomework/{1}", WebUtility.FileUrlRoot, Eval("JobFileURL")) %>"><%# Eval("JobFileName") %></a></span>
                                </div>                                
                                <div class="text_info">
                                    <span class="offline_title ">作答日期：<span><%# Eval("UploadTime") == DBNull.Value ? "" : Eval("UploadTime")%></span></span>
                                    <span class="offline_title padding_left_50">作答附件：<a href="<%# string.Format("{0}/ExOfflineHomework/{1}", WebUtility.FileUrlRoot, Eval("UploadFilePath")) %>"><%# Eval("UploadFileName") %></a></span>                                    
                                </div>
                                <div class="text_info">
                                    <span class="offline_title">批阅日期：<span><%# Eval("EvaluationTime") == DBNull.Value ? "" : Eval("EvaluationTime")%></span></span>
                                    <span class="offline_title padding_left_50">批阅附件：<a href="<%# string.Format("{0}/ExOfflineHomework/{1}", WebUtility.FileUrlRoot, Eval("MarkFilePath")) %>"><%# Eval("MarkFileName") %></a></span>                                    
                                </div>
                            </div>
                            <div class="off_line_oper">
                                <p class="off_line_inst" style="color:#ee4242">
                                    <%# Eval("Evaluation") %>
                                </p>
                            </div>
                        </li>                                
                    </ItemTemplate>
               </asp:Repeater>
        </ul>
         <script>
             $('.study_modular li').removeClass('cur').eq(3).addClass('cur');
             var nowDate = formatDate(new Date(), "yyyy-MM-dd");
             for (var i = 0; i < $('.text_list li').length; i++) {
                 if ($('.text_list li').eq(i).find('.text_name i').hasClass('homework') && $('.text_list li').eq(i).find('.end_time').text() < nowDate) {
                     $('.text_list li').eq(i).find('.see_homework').show();
                     $('.text_list li').eq(i).find('.do_homework').hide();
                 } else {
                     $('.text_list li').eq(i).find('.see_homework').hide();
                     $('.text_list li').eq(i).find('.do_homework').show();
                     if ($.trim($('.text_list li').eq(i).find('.status').text()) == "已提交")
                     {
                         $('.text_list li').eq(i).find('.do_homework').hide();
                         $('.text_list li').eq(i).find('.see_homework').show();
                     }
                 }
                 if ($('.text_list li').eq(i).find('.text_name i').hasClass('evaluation') && $('.text_list li').eq(i).find('.end_time').text() < nowDate) {
                     $('.text_list li').eq(i).find('.see_evaluation').show();
                     $('.text_list li').eq(i).find('.do_evaluation').hide();
                 } else {
                     var testCount = $('.text_list li').eq(i).find('.userTestCount').text();
                     var allowCount = $('.text_list li').eq(i).find('.testCount').text();
                     var remainCount = allowCount - testCount;
                     if (remainCount == 0) {
                         $('.text_list li').eq(i).find('.see_evaluation').show();
                         $('.text_list li').eq(i).find('.do_evaluation').hide();
                     }
                     else {

                         $('.text_list li').eq(i).find('.do_evaluation').show();
                         $('.text_list li').eq(i).find('.see_evaluation').hide();
                     }
                 }

                 if ($('.text_list li').eq(i).find('.text_name i').hasClass('offlinejob') && $('.text_list li').eq(i).find('.end_time').text() < nowDate) {
                     $('.text_list li').eq(i).find('.do_offlinejob').hide();
                 } else {
                     $('.text_list li').eq(i).find('.do_offlinejob').show();
                     if ($.trim($('.text_list li').eq(i).find('.status').text()) == "已批阅")
                     {
                         $('.text_list li').eq(i).find('.do_offlinejob').hide();
                     }
                 }
             }
             if ($('.text_list li').length == 0) {
                 $('.no_content').removeClass('hide');
             }

             function doOfflineJob(itemCourseOffLineJobID)
             {

             }
        </script>
    </div>
</asp:Content>
