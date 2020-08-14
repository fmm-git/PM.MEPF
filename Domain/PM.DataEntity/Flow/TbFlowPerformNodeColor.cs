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
    /// 实体类TbFlowPerformNodeColor。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("TbFlowPerformNodeColor")]
    [Serializable]
    public partial class TbFlowPerformNodeColor : Entity
    {
        #region Model
		private int _id;
		private string _FlowNodeState;
		private string _background;
		private string _StateDetail;

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
		/// 流程节点状态(0未开始1执行中2已驳回3已完成4已终止5已发起子流程6子流程已完成7已跳过8已禁止)
		/// </summary>
		[Field("FlowNodeState")]
		public string FlowNodeState
		{
			get{ return _FlowNodeState; }
			set
			{
				this.OnPropertyValueChange("FlowNodeState");
				this._FlowNodeState = value;
			}
		}
		/// <summary>
		/// 节点状态背景颜色
		/// </summary>
		[Field("background")]
		public string background
		{
			get{ return _background; }
			set
			{
				this.OnPropertyValueChange("background");
				this._background = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("StateDetail")]
		public string StateDetail
		{
			get{ return _StateDetail; }
			set
			{
				this.OnPropertyValueChange("StateDetail");
				this._StateDetail = value;
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
				_.FlowNodeState,
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
				_.FlowNodeState,
				_.background,
				_.StateDetail,
			};
        }
        /// <summary>
        /// 获取值信息
        /// </summary>
        public override object[] GetValues()
        {
            return new object[] {
				this._id,
				this._FlowNodeState,
				this._background,
				this._StateDetail,
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
			public readonly static Field All = new Field("*", "TbFlowPerformNodeColor");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field id = new Field("id", "TbFlowPerformNodeColor", "");
            /// <summary>
			/// 流程节点状态(0未开始1执行中2已驳回3已完成4已终止5已发起子流程6子流程已完成7已跳过8已禁止)
			/// </summary>
			public readonly static Field FlowNodeState = new Field("FlowNodeState", "TbFlowPerformNodeColor", "流程节点状态(0未开始1执行中2已驳回3已完成4已终止5已发起子流程6子流程已完成7已跳过8已禁止)");
            /// <summary>
			/// 节点状态背景颜色
			/// </summary>
			public readonly static Field background = new Field("background", "TbFlowPerformNodeColor", "节点状态背景颜色");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field StateDetail = new Field("StateDetail", "TbFlowPerformNodeColor", "");
        }
        #endregion
	}
}