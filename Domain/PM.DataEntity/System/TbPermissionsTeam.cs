﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
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
    /// 实体类TbPermissionsTeam。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("TbPermissionsTeam")]
    [Serializable]
    public partial class TbPermissionsTeam : Entity
    {
        #region Model
		private int _ID;
		private string _TeamNumber;
		private string _TeamName;
		private string _MenuCode;
		private string _Remark;
		private DateTime _CreateDate;
		private string _CreateUser;

		/// <summary>
		/// 
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
		/// 
		/// </summary>
		[Field("TeamNumber")]
		public string TeamNumber
		{
			get{ return _TeamNumber; }
			set
			{
				this.OnPropertyValueChange("TeamNumber");
				this._TeamNumber = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("TeamName")]
		public string TeamName
		{
			get{ return _TeamName; }
			set
			{
				this.OnPropertyValueChange("TeamName");
				this._TeamName = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("MenuCode")]
		public string MenuCode
		{
			get{ return _MenuCode; }
			set
			{
				this.OnPropertyValueChange("MenuCode");
				this._MenuCode = value;
			}
		}
		/// <summary>
		/// 
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
		/// 
		/// </summary>
		[Field("CreateDate")]
		public DateTime CreateDate
		{
			get{ return _CreateDate; }
			set
			{
				this.OnPropertyValueChange("CreateDate");
				this._CreateDate = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("CreateUser")]
		public string CreateUser
		{
			get{ return _CreateUser; }
			set
			{
				this.OnPropertyValueChange("CreateUser");
				this._CreateUser = value;
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
				_.TeamNumber,
				_.TeamName,
				_.MenuCode,
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
				_.TeamNumber,
				_.TeamName,
				_.MenuCode,
				_.Remark,
				_.CreateDate,
				_.CreateUser,
			};
        }
        /// <summary>
        /// 获取值信息
        /// </summary>
        public override object[] GetValues()
        {
            return new object[] {
				this._ID,
				this._TeamNumber,
				this._TeamName,
				this._MenuCode,
				this._Remark,
				this._CreateDate,
				this._CreateUser,
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
			public readonly static Field All = new Field("*", "TbPermissionsTeam");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field ID = new Field("ID", "TbPermissionsTeam", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field TeamNumber = new Field("TeamNumber", "TbPermissionsTeam", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field TeamName = new Field("TeamName", "TbPermissionsTeam", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field MenuCode = new Field("MenuCode", "TbPermissionsTeam", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field Remark = new Field("Remark", "TbPermissionsTeam", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field CreateDate = new Field("CreateDate", "TbPermissionsTeam", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field CreateUser = new Field("CreateUser", "TbPermissionsTeam", "");
        }
        #endregion
	}
}