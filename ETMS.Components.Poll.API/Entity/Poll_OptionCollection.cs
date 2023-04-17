//==================================================================================================
//Version 1.0, auto-generated.
//Generated By running1218
//Date: 2007-6-26 10:36:00.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Collections.Generic;

namespace ETMS.Components.Poll.API.Entity
{
    /// <summary>
    /// 当前问题下所有选项集合业务实体
    /// </summary>
    [Serializable]
	public class Poll_OptionCollection
	{
		#region Constructor
		/// <summary>
		///前问题下所有选项集合构造函数--默认
		/// </summary>
		public Poll_OptionCollection()
		{
		}
		
		/// <summary>
		/// 前问题下所有选项集合构造函数--所有属性
		/// </summary>
        public Poll_OptionCollection(Int32 titleType, List<Poll_Option> options, Int32 expandParm)
		{
            this.TitleType = titleType;
            this.optionCollection = options;
            this.ExpandParm = expandParm;
		}
		
		#endregion Constructor
	
		#region Fields, Properties
        private Int32 expandParmField;
		/// <summary>
		/// 扩展参数
        /// 说明：不同题型扩展参数的含义不同
        /// 题型    含义
        /// 选择题  0 不包含“其他”（默认）、1 包含“其他” 
        /// 矩阵题  评分范围ID
		/// </summary>
        public Int32 ExpandParm
		{
			get
			{
                //if (TitleType == 2 && this.expandParmField==0)
                //{
                //    throw new ArgumentException("矩阵题没有指定评分范围");
                //}
                return this.expandParmField;
			}
			set
			{
                this.expandParmField = value;
			}
		}
		
		private Int32 titleTypeField;
		/// <summary>
		/// 题型
        /// 1：选择题（单/多）
        /// 2：矩阵题
		/// </summary>
		public Int32 TitleType
		{
			get
			{
                return this.titleTypeField;
			}
			set
			{
                this.titleTypeField = value;
			}
		}

        private List<Poll_Option> optionCollection;
		/// <summary>
		/// 选项列表
		/// </summary>
        public List<Poll_Option> Options
		{
			get
			{
                if (this.optionCollection == null)
                {
                    this.optionCollection = new List<Poll_Option>();
                }
                //if(this.HeaderCollection ==null || this.headerCollection.Count==0)
                //{
                //     throw new ArgumentException("题选项不允许为空");
                //}
                return this.optionCollection;
			}
			set
			{
                this.optionCollection = value;
			}
		}
		
		#endregion Fields, Properties
	}
}
