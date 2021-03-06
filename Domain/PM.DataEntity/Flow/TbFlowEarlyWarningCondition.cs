﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.36460
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
    /// 实体类TbFlowEarlyWarningCondition。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("TbFlowEarlyWarningCondition")]
    [Serializable]
    public partial class TbFlowEarlyWarningCondition : Entity
    {
        #region Model
		private int _ID;
		private string _EarlyWarningCode;
		private string _FlowCode;
		private string _FlowNodeCode;
		private string _EarlyWarningName;
		private int? _EarlyWarningTime;
		private string _EarlyWarningTimeType;
		private string _Remark;
		private int? _EarlyWarningOrder;
		private string _FlowType;
		private int? _App;
		private int? _Pc;
		private int? _IsStart;

		/// <summary>
		/// ID
		/// </summary>
		[Field("ID")]
		public int ID
		{
			get{ return _ID; }
			set
			{
				this.OnPropertyValueChange("ID");
				this._ID = value;
			}
		}
		/// <summary>
		/// 预警条件编号
		/// </summary>
		[Field("EarlyWarningCode")]
		public string EarlyWarningCode
		{
			get{ return _EarlyWarningCode; }
			set
			{
				this.OnPropertyValueChange("EarlyWarningCode");
				this._EarlyWarningCode = value;
			}
		}
		/// <summary>
		/// 流程编号
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
		/// 流程节点编号
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
		/// 预警名称
		/// </summary>
		[Field("EarlyWarningName")]
		public string EarlyWarningName
		{
			get{ return _EarlyWarningName; }
			set
			{
				this.OnPropertyValueChange("EarlyWarningName");
				this._EarlyWarningName = value;
			}
		}
		/// <summary>
		/// 预警时间
		/// </summary>
		[Field("EarlyWarningTime")]
		public int? EarlyWarningTime
		{
			get{ return _EarlyWarningTime; }
			set
			{
				this.OnPropertyValueChange("EarlyWarningTime");
				this._EarlyWarningTime = value;
			}
		}
		/// <summary>
		/// 预警时间类型
		/// </summary>
		[Field("EarlyWarningTimeType")]
		public string EarlyWarningTimeType
		{
			get{ return _EarlyWarningTimeType; }
			set
			{
				this.OnPropertyValueChange("EarlyWarningTimeType");
				this._EarlyWarningTimeType = value;
			}
		}
		/// <summary>
		/// 说明
		/// </summary>
		[Field("Remark")]
		public string Remark
		{
			get{ return _Remark; }
			set
			{
				this.OnPropertyValueChange("Remark");
				this._Remark = value;
			}
		}
		/// <summary>
		/// 预警序号
		/// </summary>
		[Field("EarlyWarningOrder")]
		public int? EarlyWarningOrder
		{
			get{ return _EarlyWarningOrder; }
			set
			{
				this.OnPropertyValueChange("EarlyWarningOrder");
				this._EarlyWarningOrder = value;
			}
		}
		/// <summary>
		/// 流程预警类型
		/// </summary>
		[Field("FlowType")]
		public string FlowType
		{
			get{ return _FlowType; }
			set
			{
				this.OnPropertyValueChange("FlowType");
				this._FlowType = value;
			}
		}
		/// <summary>
		/// App
		/// </summary>
		[Field("App")]
		public int? App
		{
			get{ return _App; }
			set
			{
				this.OnPropertyValueChange("App");
				this._App = value;
			}
		}
		/// <summary>
		/// Pc
		/// </summary>
		[Field("Pc")]
		public int? Pc
		{
			get{ return _Pc; }
			set
			{
				this.OnPropertyValueChange("Pc");
				this._Pc = value;
			}
		}
		/// <summary>
		/// 是否启用
		/// </summary>
		[Field("IsStart")]
		public int? IsStart
		{
			get{ return _IsStart; }
			set
			{
				this.OnPropertyValueChange("IsStart");
				this._IsStart = value;
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
				_.EarlyWarningCode,
			};
        }
		/// <summary>
        /// 获取实体中的标识列
        /// </summary>
        public override Field GetIdentityField()
        {
            return _.ID;
        }
        /// <summary>
        /// 获取列信息
        /// </summary>
        public override Field[] GetFields()
        {
            return new Field[] {
				_.ID,
				_.EarlyWarningCode,
				_.FlowCode,
				_.FlowNodeCode,
				_.EarlyWarningName,
				_.EarlyWarningTime,
				_.EarlyWarningTimeType,
				_.Remark,
				_.EarlyWarningOrder,
				_.FlowType,
				_.App,
				_.Pc,
				_.IsStart,
			};
        }
        /// <summary>
        /// 获取值信息
        /// </summary>
        public override object[] GetValues()
        {
            return new object[] {
				this._ID,
				this._EarlyWarningCode,
				this._FlowCode,
				this._FlowNodeCode,
				this._EarlyWarningName,
				this._EarlyWarningTime,
				this._EarlyWarningTimeType,
				this._Remark,
				this._EarlyWarningOrder,
				this._FlowType,
				this._App,
				this._Pc,
				this._IsStart,
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
			public readonly static Field All = new Field("*", "TbFlowEarlyWarningCondition");
            /// <summary>
			/// ID
			/// </summary>
			public readonly static Field ID = new Field("ID", "TbFlowEarlyWarningCondition", "ID");
            /// <summary>
			/// 预警条件编号
			/// </summary>
			public readonly static Field EarlyWarningCode = new Field("EarlyWarningCode", "TbFlowEarlyWarningCondition", "预警条件编号");
            /// <summary>
			/// 流程编号
			/// </summary>
			public readonly static Field FlowCode = new Field("FlowCode", "TbFlowEarlyWarningCondition", "流程编号");
            /// <summary>
			/// 流程节点编号
			/// </summary>
			public readonly static Field FlowNodeCode = new Field("FlowNodeCode", "TbFlowEarlyWarningCondition", "流程节点编号");
            /// <summary>
			/// 预警名称
			/// </summary>
			public readonly static Field EarlyWarningName = new Field("EarlyWarningName", "TbFlowEarlyWarningCondition", "预警名称");
            /// <summary>
			/// 预警时间
			/// </summary>
			public readonly static Field EarlyWarningTime = new Field("EarlyWarningTime", "TbFlowEarlyWarningCondition", "预警时间");
            /// <summary>
			/// 预警时间类型
			/// </summary>
			public readonly static Field EarlyWarningTimeType = new Field("EarlyWarningTimeType", "TbFlowEarlyWarningCondition", "预警时间类型");
            /// <summary>
			/// 说明
			/// </summary>
			public readonly static Field Remark = new Field("Remark", "TbFlowEarlyWarningCondition", "说明");
            /// <summary>
			/// 预警序号
			/// </summary>
			public readonly static Field EarlyWarningOrder = new Field("EarlyWarningOrder", "TbFlowEarlyWarningCondition", "预警序号");
            /// <summary>
			/// 流程预警类型
			/// </summary>
			public readonly static Field FlowType = new Field("FlowType", "TbFlowEarlyWarningCondition", "流程预警类型");
            /// <summary>
			/// App
			/// </summary>
			public readonly static Field App = new Field("App", "TbFlowEarlyWarningCondition", "App");
            /// <summary>
			/// Pc
			/// </summary>
			public readonly static Field Pc = new Field("Pc", "TbFlowEarlyWarningCondition", "Pc");
            /// <summary>
			/// 是否启用
			/// </summary>
			public readonly static Field IsStart = new Field("IsStart", "TbFlowEarlyWarningCondition", "是否启用");
        }
        #endregion
	}
}