﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18408
//     Website: http://ITdos.com/Dos/ORM/Index.html
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using Dos.ORM;

namespace PM.DataEntity
{
    /// <summary>
    /// 实体类TbFlowPerformNodeJudgeCriteria。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("TbFlowPerformNodeJudgeCriteria")]
    [Serializable]
    public partial class TbFlowPerformNodeJudgeCriteria : Entity
    {
        #region Model
		private int _id;
		private string _FlowPerformID;
		private string _FlowCode;
		private string _FlowNodeCode;
		private string _PhysicalTableName;
		private string _FieldCode;
		private string _JudgeSymbol;
		private string _BeforeBrackets;
		private string _JudgeValue;
		private string _LastBrackets;
		private string _JudgeRelation;

		/// <summary>
		/// 
		/// </summary>
		[Field("id")]
		public int id
		{
			get{ return _id; }
			set
			{
				this.OnPropertyValueChange("id");
				this._id = value;
			}
		}
		/// <summary>
		/// 流程执行流水号
		/// </summary>
		[Field("FlowPerformID")]
		public string FlowPerformID
		{
			get{ return _FlowPerformID; }
			set
			{
				this.OnPropertyValueChange("FlowPerformID");
				this._FlowPerformID = value;
			}
		}
		/// <summary>
		/// 流程代码
		/// </summary>
		[Field("FlowCode")]
		public string FlowCode
		{
			get{ return _FlowCode; }
			set
			{
				this.OnPropertyValueChange("FlowCode");
				this._FlowCode = value;
			}
		}
		/// <summary>
		/// 流程节点代码
		/// </summary>
		[Field("FlowNodeCode")]
		public string FlowNodeCode
		{
			get{ return _FlowNodeCode; }
			set
			{
				this.OnPropertyValueChange("FlowNodeCode");
				this._FlowNodeCode = value;
			}
		}
		/// <summary>
		/// 物理表名
		/// </summary>
		[Field("PhysicalTableName")]
		public string PhysicalTableName
		{
			get{ return _PhysicalTableName; }
			set
			{
				this.OnPropertyValueChange("PhysicalTableName");
				this._PhysicalTableName = value;
			}
		}
		/// <summary>
		/// 字段代码
		/// </summary>
		[Field("FieldCode")]
		public string FieldCode
		{
			get{ return _FieldCode; }
			set
			{
				this.OnPropertyValueChange("FieldCode");
				this._FieldCode = value;
			}
		}
		/// <summary>
		/// 判断符号（&gt;-大于,&lt;-小于,=-等于）
		/// </summary>
		[Field("JudgeSymbol")]
		public string JudgeSymbol
		{
			get{ return _JudgeSymbol; }
			set
			{
				this.OnPropertyValueChange("JudgeSymbol");
				this._JudgeSymbol = value;
			}
		}
		/// <summary>
		/// 前括号（&#39; &#39; , ( , (( , (((  ）
		/// </summary>
		[Field("BeforeBrackets")]
		public string BeforeBrackets
		{
			get{ return _BeforeBrackets; }
			set
			{
				this.OnPropertyValueChange("BeforeBrackets");
				this._BeforeBrackets = value;
			}
		}
		/// <summary>
		/// 条件值
		/// </summary>
		[Field("JudgeValue")]
		public string JudgeValue
		{
			get{ return _JudgeValue; }
			set
			{
				this.OnPropertyValueChange("JudgeValue");
				this._JudgeValue = value;
			}
		}
		/// <summary>
		/// 后括号（&#39; &#39; , ) , )) ,  )))  ）
		/// </summary>
		[Field("LastBrackets")]
		public string LastBrackets
		{
			get{ return _LastBrackets; }
			set
			{
				this.OnPropertyValueChange("LastBrackets");
				this._LastBrackets = value;
			}
		}
		/// <summary>
		/// 条件关系
		/// </summary>
		[Field("JudgeRelation")]
		public string JudgeRelation
		{
			get{ return _JudgeRelation; }
			set
			{
				this.OnPropertyValueChange("JudgeRelation");
				this._JudgeRelation = value;
			}
		}
		#endregion

		#region Method
        /// <summary>
        /// 获取实体中的主键列
        /// </summary>
        public override Field[] GetPrimaryKeyFields()
        {
            return new Field[] {
				_.FlowPerformID,
				_.FlowCode,
				_.FlowNodeCode,
				_.PhysicalTableName,
				_.FieldCode,
				_.JudgeSymbol,
				_.JudgeValue,
			};
        }
		/// <summary>
        /// 获取实体中的标识列
        /// </summary>
        public override Field GetIdentityField()
        {
            return _.id;
        }
        /// <summary>
        /// 获取列信息
        /// </summary>
        public override Field[] GetFields()
        {
            return new Field[] {
				_.id,
				_.FlowPerformID,
				_.FlowCode,
				_.FlowNodeCode,
				_.PhysicalTableName,
				_.FieldCode,
				_.JudgeSymbol,
				_.BeforeBrackets,
				_.JudgeValue,
				_.LastBrackets,
				_.JudgeRelation,
			};
        }
        /// <summary>
        /// 获取值信息
        /// </summary>
        public override object[] GetValues()
        {
            return new object[] {
				this._id,
				this._FlowPerformID,
				this._FlowCode,
				this._FlowNodeCode,
				this._PhysicalTableName,
				this._FieldCode,
				this._JudgeSymbol,
				this._BeforeBrackets,
				this._JudgeValue,
				this._LastBrackets,
				this._JudgeRelation,
			};
        }
        /// <summary>
        /// 是否是v1.10.5.6及以上版本实体。
        /// </summary>
        /// <returns></returns>
        public override bool V1_10_5_6_Plus()
        {
            return true;
        }
        #endregion

		#region _Field
        /// <summary>
        /// 字段信息
        /// </summary>
        public class _
        {
			/// <summary>
			/// * 
			/// </summary>
			public readonly static Field All = new Field("*", "TbFlowPerformNodeJudgeCriteria");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field id = new Field("id", "TbFlowPerformNodeJudgeCriteria", "");
            /// <summary>
			/// 流程执行流水号
			/// </summary>
			public readonly static Field FlowPerformID = new Field("FlowPerformID", "TbFlowPerformNodeJudgeCriteria", "流程执行流水号");
            /// <summary>
			/// 流程代码
			/// </summary>
			public readonly static Field FlowCode = new Field("FlowCode", "TbFlowPerformNodeJudgeCriteria", "流程代码");
            /// <summary>
			/// 流程节点代码
			/// </summary>
			public readonly static Field FlowNodeCode = new Field("FlowNodeCode", "TbFlowPerformNodeJudgeCriteria", "流程节点代码");
            /// <summary>
			/// 物理表名
			/// </summary>
			public readonly static Field PhysicalTableName = new Field("PhysicalTableName", "TbFlowPerformNodeJudgeCriteria", "物理表名");
            /// <summary>
			/// 字段代码
			/// </summary>
			public readonly static Field FieldCode = new Field("FieldCode", "TbFlowPerformNodeJudgeCriteria", "字段代码");
            /// <summary>
			/// 判断符号（&gt;-大于,&lt;-小于,=-等于）
			/// </summary>
			public readonly static Field JudgeSymbol = new Field("JudgeSymbol", "TbFlowPerformNodeJudgeCriteria", "判断符号（&gt;-大于,&lt;-小于,=-等于）");
            /// <summary>
			/// 前括号（&#39; &#39; , ( , (( , (((  ）
			/// </summary>
			public readonly static Field BeforeBrackets = new Field("BeforeBrackets", "TbFlowPerformNodeJudgeCriteria", "前括号（&#39; &#39; , ( , (( , (((  ）");
            /// <summary>
			/// 条件值
			/// </summary>
			public readonly static Field JudgeValue = new Field("JudgeValue", "TbFlowPerformNodeJudgeCriteria", "条件值");
            /// <summary>
			/// 后括号（&#39; &#39; , ) , )) ,  )))  ）
			/// </summary>
			public readonly static Field LastBrackets = new Field("LastBrackets", "TbFlowPerformNodeJudgeCriteria", "后括号（&#39; &#39; , ) , )) ,  )))  ）");
            /// <summary>
			/// 条件关系
			/// </summary>
			public readonly static Field JudgeRelation = new Field("JudgeRelation", "TbFlowPerformNodeJudgeCriteria", "条件关系");
        }
        #endregion
	}
}