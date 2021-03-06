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
    /// 实体类TbPositionUser。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("TbPositionUser")]
    [Serializable]
    public partial class TbPositionUser : Entity
    {
        #region Model
		private int _Id;
		private string _PositionCode;
		private string _UserCode;

		/// <summary>
		/// 
		/// </summary>
		[Field("Id")]
		public int Id
		{
			get{ return _Id; }
			set
			{
				this.OnPropertyValueChange("Id");
				this._Id = value;
			}
		}
		/// <summary>
		/// 岗位编码
		/// </summary>
		[Field("PositionCode")]
		public string PositionCode
		{
			get{ return _PositionCode; }
			set
			{
				this.OnPropertyValueChange("PositionCode");
				this._PositionCode = value;
			}
		}
		/// <summary>
		/// 用户编码
		/// </summary>
		[Field("UserCode")]
		public string UserCode
		{
			get{ return _UserCode; }
			set
			{
				this.OnPropertyValueChange("UserCode");
				this._UserCode = value;
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
				_.Id,
			};
        }
		/// <summary>
        /// 获取实体中的标识列
        /// </summary>
        public override Field GetIdentityField()
        {
            return _.Id;
        }
        /// <summary>
        /// 获取列信息
        /// </summary>
        public override Field[] GetFields()
        {
            return new Field[] {
				_.Id,
				_.PositionCode,
				_.UserCode,
			};
        }
        /// <summary>
        /// 获取值信息
        /// </summary>
        public override object[] GetValues()
        {
            return new object[] {
				this._Id,
				this._PositionCode,
				this._UserCode,
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
			public readonly static Field All = new Field("*", "TbPositionUser");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field Id = new Field("Id", "TbPositionUser", "");
            /// <summary>
			/// 岗位编码
			/// </summary>
			public readonly static Field PositionCode = new Field("PositionCode", "TbPositionUser", "岗位编码");
            /// <summary>
			/// 用户编码
			/// </summary>
			public readonly static Field UserCode = new Field("UserCode", "TbPositionUser", "用户编码");
        }
        #endregion
	}
}