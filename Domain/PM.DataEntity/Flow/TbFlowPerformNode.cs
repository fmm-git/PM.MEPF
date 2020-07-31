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
    /// 实体类TbFlowPerformNode。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("TbFlowPerformNode")]
    [Serializable]
    public partial class TbFlowPerformNode : Entity
    {
        #region Model
		private int _id;
		private string _FlowPerformID;
		private string _FlowCode;
		private string _FlowNodeCode;
		private string _FlowNodeName;
		private int _AllApproval;
		private int _FreeCandidates;
		private int _AddSignature;
		private int _AllowedSkip;
		private int? _LimitDay;
		private int? _LimitHour;
		private int? _LimitMinutes;
		private int _AllowedRejected;
		private int? _FreeRejected;
		private string _RejectedToNodeCode;
		private int _FlowNodeState;
		private int _BlankNode;
		private string _FlowNodeEvent;

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
		/// 流程节点名称
		/// </summary>
		[Field("FlowNodeName")]
		public string FlowNodeName
		{
			get{ return _FlowNodeName; }
			set
			{
				this.OnPropertyValueChange("FlowNodeName");
				this._FlowNodeName = value;
			}
		}
		/// <summary>
		/// 汇签(0表示非汇签1表示汇签)
		/// </summary>
		[Field("AllApproval")]
		public int AllApproval
		{
			get{ return _AllApproval; }
			set
			{
				this.OnPropertyValueChange("AllApproval");
				this._AllApproval = value;
			}
		}
		/// <summary>
		/// 自由选人(0不能自由选人1允许自由选人)
		/// </summary>
		[Field("FreeCandidates")]
		public int FreeCandidates
		{
			get{ return _FreeCandidates; }
			set
			{
				this.OnPropertyValueChange("FreeCandidates");
				this._FreeCandidates = value;
			}
		}
		/// <summary>
		/// 允许加签(0不允许加签1允许加签)
		/// </summary>
		[Field("AddSignature")]
		public int AddSignature
		{
			get{ return _AddSignature; }
			set
			{
				this.OnPropertyValueChange("AddSignature");
				this._AddSignature = value;
			}
		}
		/// <summary>
		/// 允许跳过（0不允许跳过1允许跳过)
		/// </summary>
		[Field("AllowedSkip")]
		public int AllowedSkip
		{
			get{ return _AllowedSkip; }
			set
			{
				this.OnPropertyValueChange("AllowedSkip");
				this._AllowedSkip = value;
			}
		}
		/// <summary>
		/// 限制天数(允许跳过时可设置)
		/// </summary>
		[Field("LimitDay")]
		public int? LimitDay
		{
			get{ return _LimitDay; }
			set
			{
				this.OnPropertyValueChange("LimitDay");
				this._LimitDay = value;
			}
		}
		/// <summary>
		/// 限制小时数(允许跳过时可设置)
		/// </summary>
		[Field("LimitHour")]
		public int? LimitHour
		{
			get{ return _LimitHour; }
			set
			{
				this.OnPropertyValueChange("LimitHour");
				this._LimitHour = value;
			}
		}
		/// <summary>
		/// 限制分钟数(允许跳过时可设置)
		/// </summary>
		[Field("LimitMinutes")]
		public int? LimitMinutes
		{
			get{ return _LimitMinutes; }
			set
			{
				this.OnPropertyValueChange("LimitMinutes");
				this._LimitMinutes = value;
			}
		}
		/// <summary>
		/// 允许驳回(0不允许驳回1允许驳回)
		/// </summary>
		[Field("AllowedRejected")]
		public int AllowedRejected
		{
			get{ return _AllowedRejected; }
			set
			{
				this.OnPropertyValueChange("AllowedRejected");
				this._AllowedRejected = value;
			}
		}
		/// <summary>
		/// 自由驳回(0不允许自由驳回1允许自由驳回)
		/// </summary>
		[Field("FreeRejected")]
		public int? FreeRejected
		{
			get{ return _FreeRejected; }
			set
			{
				this.OnPropertyValueChange("FreeRejected");
				this._FreeRejected = value;
			}
		}
		/// <summary>
		/// 驳回到指定节点(不为空表示只能驳回到本节点)
		/// </summary>
		[Field("RejectedToNodeCode")]
		public string RejectedToNodeCode
		{
			get{ return _RejectedToNodeCode; }
			set
			{
				this.OnPropertyValueChange("RejectedToNodeCode");
				this._RejectedToNodeCode = value;
			}
		}
		/// <summary>
		/// 流程节点状态(0未开始1执行中2已驳回3已完成4已终止5已发起子流程6子流程已完成7已跳过8已禁止)
		/// </summary>
		[Field("FlowNodeState")]
		public int FlowNodeState
		{
			get{ return _FlowNodeState; }
			set
			{
				this.OnPropertyValueChange("FlowNodeState");
				this._FlowNodeState = value;
			}
		}
		/// <summary>
		/// _0非空节点，1空节点
		/// </summary>
		[Field("BlankNode")]
		public int BlankNode
		{
			get{ return _BlankNode; }
			set
			{
				this.OnPropertyValueChange("BlankNode");
				this._BlankNode = value;
			}
		}
		/// <summary>
		/// 流程事件
		/// </summary>
		[Field("FlowNodeEvent")]
		public string FlowNodeEvent
		{
			get{ return _FlowNodeEvent; }
			set
			{
				this.OnPropertyValueChange("FlowNodeEvent");
				this._FlowNodeEvent = value;
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
				_.FlowNodeName,
				_.AllApproval,
				_.FreeCandidates,
				_.AddSignature,
				_.AllowedSkip,
				_.LimitDay,
				_.LimitHour,
				_.LimitMinutes,
				_.AllowedRejected,
				_.FreeRejected,
				_.RejectedToNodeCode,
				_.FlowNodeState,
				_.BlankNode,
				_.FlowNodeEvent,
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
				this._FlowNodeName,
				this._AllApproval,
				this._FreeCandidates,
				this._AddSignature,
				this._AllowedSkip,
				this._LimitDay,
				this._LimitHour,
				this._LimitMinutes,
				this._AllowedRejected,
				this._FreeRejected,
				this._RejectedToNodeCode,
				this._FlowNodeState,
				this._BlankNode,
				this._FlowNodeEvent,
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
			public readonly static Field All = new Field("*", "TbFlowPerformNode");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field id = new Field("id", "TbFlowPerformNode", "");
            /// <summary>
			/// 流程执行流水号
			/// </summary>
			public readonly static Field FlowPerformID = new Field("FlowPerformID", "TbFlowPerformNode", "流程执行流水号");
            /// <summary>
			/// 流程代码
			/// </summary>
			public readonly static Field FlowCode = new Field("FlowCode", "TbFlowPerformNode", "流程代码");
            /// <summary>
			/// 流程节点代码
			/// </summary>
			public readonly static Field FlowNodeCode = new Field("FlowNodeCode", "TbFlowPerformNode", "流程节点代码");
            /// <summary>
			/// 流程节点名称
			/// </summary>
			public readonly static Field FlowNodeName = new Field("FlowNodeName", "TbFlowPerformNode", "流程节点名称");
            /// <summary>
			/// 汇签(0表示非汇签1表示汇签)
			/// </summary>
			public readonly static Field AllApproval = new Field("AllApproval", "TbFlowPerformNode", "汇签(0表示非汇签1表示汇签)");
            /// <summary>
			/// 自由选人(0不能自由选人1允许自由选人)
			/// </summary>
			public readonly static Field FreeCandidates = new Field("FreeCandidates", "TbFlowPerformNode", "自由选人(0不能自由选人1允许自由选人)");
            /// <summary>
			/// 允许加签(0不允许加签1允许加签)
			/// </summary>
			public readonly static Field AddSignature = new Field("AddSignature", "TbFlowPerformNode", "允许加签(0不允许加签1允许加签)");
            /// <summary>
			/// 允许跳过（0不允许跳过1允许跳过)
			/// </summary>
			public readonly static Field AllowedSkip = new Field("AllowedSkip", "TbFlowPerformNode", "允许跳过（0不允许跳过1允许跳过)");
            /// <summary>
			/// 限制天数(允许跳过时可设置)
			/// </summary>
			public readonly static Field LimitDay = new Field("LimitDay", "TbFlowPerformNode", "限制天数(允许跳过时可设置)");
            /// <summary>
			/// 限制小时数(允许跳过时可设置)
			/// </summary>
			public readonly static Field LimitHour = new Field("LimitHour", "TbFlowPerformNode", "限制小时数(允许跳过时可设置)");
            /// <summary>
			/// 限制分钟数(允许跳过时可设置)
			/// </summary>
			public readonly static Field LimitMinutes = new Field("LimitMinutes", "TbFlowPerformNode", "限制分钟数(允许跳过时可设置)");
            /// <summary>
			/// 允许驳回(0不允许驳回1允许驳回)
			/// </summary>
			public readonly static Field AllowedRejected = new Field("AllowedRejected", "TbFlowPerformNode", "允许驳回(0不允许驳回1允许驳回)");
            /// <summary>
			/// 自由驳回(0不允许自由驳回1允许自由驳回)
			/// </summary>
			public readonly static Field FreeRejected = new Field("FreeRejected", "TbFlowPerformNode", "自由驳回(0不允许自由驳回1允许自由驳回)");
            /// <summary>
			/// 驳回到指定节点(不为空表示只能驳回到本节点)
			/// </summary>
			public readonly static Field RejectedToNodeCode = new Field("RejectedToNodeCode", "TbFlowPerformNode", "驳回到指定节点(不为空表示只能驳回到本节点)");
            /// <summary>
			/// 流程节点状态(0未开始1执行中2已驳回3已完成4已终止5已发起子流程6子流程已完成7已跳过8已禁止)
			/// </summary>
			public readonly static Field FlowNodeState = new Field("FlowNodeState", "TbFlowPerformNode", "流程节点状态(0未开始1执行中2已驳回3已完成4已终止5已发起子流程6子流程已完成7已跳过8已禁止)");
            /// <summary>
			/// _0非空节点，1空节点
			/// </summary>
			public readonly static Field BlankNode = new Field("BlankNode", "TbFlowPerformNode", "_0非空节点，1空节点");
            /// <summary>
			/// 流程事件
			/// </summary>
			public readonly static Field FlowNodeEvent = new Field("FlowNodeEvent", "TbFlowPerformNode", "流程事件");
        }
        #endregion
	}
}