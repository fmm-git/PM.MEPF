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
    /// 实体类TbFlowState。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("TbFlowState")]
    [Serializable]
    public partial class TbFlowState : Entity
    {
        #region Model
		private int _id;
		private string _StateClassification;
		private int _StateCode;
		private string _StateDescription;

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
		/// 
		/// </summary>
		[Field("StateClassification")]
		public string StateClassification
		{
			get{ return _StateClassification; }
			set
			{
				this.OnPropertyValueChange("StateClassification");
				this._StateClassification = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("StateCode")]
		public int StateCode
		{
			get{ return _StateCode; }
			set
			{
				this.OnPropertyValueChange("StateCode");
				this._StateCode = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("StateDescription")]
		public string StateDescription
		{
			get{ return _StateDescription; }
			set
			{
				this.OnPropertyValueChange("StateDescription");
				this._StateDescription = value;
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
				_.StateClassification,
				_.StateCode,
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
				_.StateClassification,
				_.StateCode,
				_.StateDescription,
			};
        }
        /// <summary>
        /// 获取值信息
        /// </summary>
        public override object[] GetValues()
        {
            return new object[] {
				this._id,
				this._StateClassification,
				this._StateCode,
				this._StateDescription,
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
			public readonly static Field All = new Field("*", "TbFlowState");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field id = new Field("id", "TbFlowState", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field StateClassification = new Field("StateClassification", "TbFlowState", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field StateCode = new Field("StateCode", "TbFlowState", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field StateDescription = new Field("StateDescription", "TbFlowState", "");
        }
        #endregion
	}
}