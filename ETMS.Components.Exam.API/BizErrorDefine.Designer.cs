﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ETMS.Components.Exam.API {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class BizErrorDefine {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal BizErrorDefine() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ETMS.Components.Exam.API.BizErrorDefine", typeof(BizErrorDefine).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 该节点缩进后超过三层，不能添加缩进！.
        /// </summary>
        internal static string ItemBank_CatAddIndent {
            get {
                return ResourceManager.GetString("ItemBank.CatAddIndent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 该节点是第一层，不能减少缩进！.
        /// </summary>
        internal static string ItemBank_CatReduceIndent {
            get {
                return ResourceManager.GetString("ItemBank.CatReduceIndent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 判断题信息有误，请检查题目、选项和答案信息是否填写正确。.
        /// </summary>
        internal static string ItemBank_JudgementQuestion_Invalid_Data {
            get {
                return ResourceManager.GetString("ItemBank.JudgementQuestion.Invalid.Data", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 试题类型非判断题.
        /// </summary>
        internal static string ItemBank_JudgementQuestion_Invalid_Type {
            get {
                return ResourceManager.GetString("ItemBank.JudgementQuestion.Invalid.Type", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 试题已不存在，无法更新.
        /// </summary>
        internal static string ItemBank_JudgementQuestion_Not_Found {
            get {
                return ResourceManager.GetString("ItemBank.JudgementQuestion.Not.Found", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 多选题信息有误，请检查题目、选项和答案信息是否填写正确。.
        /// </summary>
        internal static string ItemBank_MultipleChoiceQuestion_Invalid_Data {
            get {
                return ResourceManager.GetString("ItemBank.MultipleChoiceQuestion.Invalid.Data", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 试题类型非多选题.
        /// </summary>
        internal static string ItemBank_MultipleChoiceQuestion_Invalid_Type {
            get {
                return ResourceManager.GetString("ItemBank.MultipleChoiceQuestion.Invalid.Type", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 试题已不存在，无法更新.
        /// </summary>
        internal static string ItemBank_MultipleChoiceQuestion_Not_Found {
            get {
                return ResourceManager.GetString("ItemBank.MultipleChoiceQuestion.Not.Found", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 选项组中数据错误.
        /// </summary>
        internal static string ItemBank_OptionGroup_Data_Invalid {
            get {
                return ResourceManager.GetString("ItemBank.OptionGroup.Data.Invalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 未正确加载试题选项组实例数据.
        /// </summary>
        internal static string ItemBank_OptionGroup_Instance_Invalid {
            get {
                return ResourceManager.GetString("ItemBank.OptionGroup.Instance.Invalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 新增加的选项组中数据不正确.
        /// </summary>
        internal static string ItemBank_OptionGroup_New_Invalid {
            get {
                return ResourceManager.GetString("ItemBank.OptionGroup.New.Invalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 没有找到指定选项组.
        /// </summary>
        internal static string ItemBank_OptionGroup_Not_Found {
            get {
                return ResourceManager.GetString("ItemBank.OptionGroup.Not.Found", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 指定的试题选项组已不存在.
        /// </summary>
        internal static string ItemBank_OptionGroupService_Not_Found {
            get {
                return ResourceManager.GetString("ItemBank.OptionGroupService.Not.Found", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 指定的试题选项已不存在.
        /// </summary>
        internal static string ItemBank_OptionService_Not_Found {
            get {
                return ResourceManager.GetString("ItemBank.OptionService.Not.Found", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 选项分组不能少于两组，请添加选项分组！.
        /// </summary>
        internal static string ItemBank_Question_LessOptionGroups {
            get {
                return ResourceManager.GetString("ItemBank.Question.LessOptionGroups", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 归类题应该有两个选项分组，请查看！.
        /// </summary>
        internal static string ItemBank_Question_OptionGroupsInvalid {
            get {
                return ResourceManager.GetString("ItemBank.Question.OptionGroupsInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 选项组下不能没有选项，请查看！.
        /// </summary>
        internal static string ItemBank_Question_WithoutGroups {
            get {
                return ResourceManager.GetString("ItemBank.Question.WithoutGroups", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 参数错误.
        /// </summary>
        internal static string ItemBank_QuestionBaseLogic_Invalid_Parameter {
            get {
                return ResourceManager.GetString("ItemBank.QuestionBaseLogic.Invalid.Parameter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 试题不存在.
        /// </summary>
        internal static string ItemBank_QuestionBaseLogic_Not_Found {
            get {
                return ResourceManager.GetString("ItemBank.QuestionBaseLogic.Not.Found", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 要增加的选项的数据不正确.
        /// </summary>
        internal static string ItemBank_QuestionOption_New_Invalid {
            get {
                return ResourceManager.GetString("ItemBank.QuestionOption.New.Invalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 试题数据不合法，请检查.
        /// </summary>
        internal static string ItemBank_QuestionService_Invalid_Data {
            get {
                return ResourceManager.GetString("ItemBank.QuestionService.Invalid.Data", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 选中的节点无效！.
        /// </summary>
        internal static string ItemBank_SelectedNodeIsInvalid {
            get {
                return ResourceManager.GetString("ItemBank.SelectedNodeIsInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 单选题信息有误，请检查题目、选项和答案信息是否填写正确。.
        /// </summary>
        internal static string ItemBank_SingleChoiceQuestion_Invalid_Data {
            get {
                return ResourceManager.GetString("ItemBank.SingleChoiceQuestion.Invalid.Data", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 试题类型非单选题.
        /// </summary>
        internal static string ItemBank_SingleChoiceQuestion_Invalid_Type {
            get {
                return ResourceManager.GetString("ItemBank.SingleChoiceQuestion.Invalid.Type", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 试题已不存在，无法更新.
        /// </summary>
        internal static string ItemBank_SingleChoiceQuestion_Not_Found {
            get {
                return ResourceManager.GetString("ItemBank.SingleChoiceQuestion.Not.Found", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 未交卷，无法得到试卷反馈.
        /// </summary>
        internal static string Test_Exam_NotTestOver {
            get {
                return ResourceManager.GetString("Test.Exam.NotTestOver", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 试卷已提交，无法再进入考试.
        /// </summary>
        internal static string Test_UserExam_ExamCompleted {
            get {
                return ResourceManager.GetString("Test.UserExam.ExamCompleted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 指定的考试不存在，无法得到结果.
        /// </summary>
        internal static string Test_UserExam_ExamNotExist {
            get {
                return ResourceManager.GetString("Test.UserExam.ExamNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 答卷已开始做答，不再允许此操作.
        /// </summary>
        internal static string Test_UserExam_ExamStarted {
            get {
                return ResourceManager.GetString("Test.UserExam.ExamStarted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 未正确加载数据，请正确加载试题选项数据加载.
        /// </summary>
        internal static string Test_UserExam_Instance_Invalid {
            get {
                return ResourceManager.GetString("Test.UserExam.Instance.Invalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 试题不在该答卷中，请确认是否正确.
        /// </summary>
        internal static string Test_UserExam_NotExistQuestion {
            get {
                return ResourceManager.GetString("Test.UserExam.NotExistQuestion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 答卷还没有开始作答.
        /// </summary>
        internal static string Test_UserExam_NotStart {
            get {
                return ResourceManager.GetString("Test.UserExam.NotStart", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 超过试卷允许最大考试次数，无法进行该考试。.
        /// </summary>
        internal static string Test_UserExam_OverExamTimes {
            get {
                return ResourceManager.GetString("Test.UserExam.OverExamTimes", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 指定的试卷定义不存在，无法进行该考试.
        /// </summary>
        internal static string Test_UserExam_PaperNotExist {
            get {
                return ResourceManager.GetString("Test.UserExam.PaperNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 试题答案与试题类型不匹配.
        /// </summary>
        internal static string Test_UserExam_QuestionAnswerTypeErr {
            get {
                return ResourceManager.GetString("Test.UserExam.QuestionAnswerTypeErr", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 要添加的试题已存在于试卷中，不允许添加重复的试题.
        /// </summary>
        internal static string Test_UserExam_QuestionExisted {
            get {
                return ResourceManager.GetString("Test.UserExam.QuestionExisted", resourceCulture);
            }
        }
    }
}
