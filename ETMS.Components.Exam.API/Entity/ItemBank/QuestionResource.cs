using System;

namespace ETMS.Components.Exam.API.Entity.ItemBank
{
    /// <summary>
    /// 从资源模块返回的试题相关数据
    /// </summary>
    [Serializable]
    public class QuestionResource
    {
        /// <summary>
        /// 容器ID
        /// </summary>
        public Guid ContainerID { get; set; }

        /// <summary>
        /// 资源ID
        /// </summary>
        public Guid ResourceID { get; set; }

        /// <summary>
        /// 容器类型
        /// </summary>
        public EnumContainerType ContainerType { get; set; }

        /// <summary>
        /// 资源大小
        /// </summary>
        public int ResourceSize { get; set; }

        /// <summary>
        /// 位置
        /// </summary>
        public EnumPosition Position { get; set; }
    }

    /// <summary>
    /// 容器类型
    /// 0：null；1：试题；2：试卷；
    /// </summary>
    public enum EnumContainerType
    {
        Null =0,
        Question =1,
        Testpaper=2,
    }

    /// <summary>
    /// 素材在试题的位置
    /// 0:null 1:题面 2:选项 3:选项反馈 4:试题反馈 5:解题思路 6:试卷反馈 7:其他
    /// </summary>
    public enum EnumPosition
    {
        /// <summary>
        /// 空
        /// </summary>
        Null =0,
        /// <summary>
        /// 题面
        /// </summary>
        QuestionName=1,
        /// <summary>
        /// 选项
        /// </summary>
        QuestionOption =2,
        /// <summary>
        /// 选项反馈
        /// </summary>
        OptionFeedback = 3,
        /// <summary>
        /// 试题反馈
        /// </summary>
        QuestionFeedback = 4,
        /// <summary>
        /// 解题思路
        /// </summary>
        QuestionSolution = 5,
        /// <summary>
        /// 试卷反馈
        /// </summary>
        TestpaperFeedback = 6,
        /// <summary>
        /// 其他
        /// </summary>
        Other = 7,
    }
}
