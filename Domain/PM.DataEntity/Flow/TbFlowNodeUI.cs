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
    /// 实体类TbFlowNodeUI。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("TbFlowNodeUI")]
    [Serializable]
    public partial class TbFlowNodeUI : Entity
    {
        #region Model
		private int _id;
		private string _FlowCode;
		private string _FlowNodeCode;
		private string _processData;
		private string _icon;
		private string _NodeLeft;
		private string _NodeTop;

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
		/// 下级节点列表
		/// </summary>
		[Field("processData")]
		public string processData
		{
			get{ return _processData; }
			set
			{
				this.OnPropertyValueChange("processData");
				this._processData = value;
			}
		}
		/// <summary>
		/// 节点图标
		/// </summary>
		[Field("icon")]
		public string icon
		{
			get{ return _icon; }
			set
			{
				this.OnPropertyValueChange("icon");
				this._icon = value;
			}
		}
		/// <summary>
		/// 节点Left(px值)
		/// </summary>
		[Field("NodeLeft")]
		public string NodeLeft
		{
			get{ return _NodeLeft; }
			set
			{
				this.OnPropertyValueChange("NodeLeft");
				this._NodeLeft = value;
			}
		}
		/// <summary>
		/// 节点Top(px值)
		/// </summary>
		[Field("NodeTop")]
		public string NodeTop
		{
			get{ return _NodeTop; }
			set
			{
				this.OnPropertyValueChange("NodeTop");
				this._NodeTop = value;
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
				_.FlowCode,
				_.FlowNodeCode,
				_.processData,
				_.icon,
				_.NodeLeft,
				_.NodeTop,
			};
        }
        /// <summary>
        /// 获取值信息
        /// </summary>
        public override object[] GetValues()
        {
            return new object[] {
				this._id,
				this._FlowCode,
				this._FlowNodeCode,
				this._processData,
				this._icon,
				this._NodeLeft,
				this._NodeTop,
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
			public readonly static Field All = new Field("*", "TbFlowNodeUI");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field id = new Field("id", "TbFlowNodeUI", "");
            /// <summary>
			/// 流程代码
			/// </summary>
			public readonly static Field FlowCode = new Field("FlowCode", "TbFlowNodeUI", "流程代码");
            /// <summary>
			/// 流程节点代码
			/// </summary>
			public readonly static Field FlowNodeCode = new Field("FlowNodeCode", "TbFlowNodeUI", "流程节点代码");
            /// <summary>
			/// 下级节点列表
			/// </summary>
			public readonly static Field processData = new Field("processData", "TbFlowNodeUI", "下级节点列表");
            /// <summary>
			/// 节点图标
			/// </summary>
			public readonly static Field icon = new Field("icon", "TbFlowNodeUI", "节点图标");
            /// <summary>
			/// 节点Left(px值)
			/// </summary>
			public readonly static Field NodeLeft = new Field("NodeLeft", "TbFlowNodeUI", "节点Left(px值)");
            /// <summary>
			/// 节点Top(px值)
			/// </summary>
			public readonly static Field NodeTop = new Field("NodeTop", "TbFlowNodeUI", "节点Top(px值)");
        }
        #endregion
	}
}