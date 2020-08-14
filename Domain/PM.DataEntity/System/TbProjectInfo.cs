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
    /// 实体类TbProjectInfo。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("TbProjectInfo")]
    [Serializable]
    public partial class TbProjectInfo : Entity
    {
        #region Model
		private int _ID;
		private string _ProjectId;
		private string _ProjectName;
		private DateTime? _InsertTime;
		private int? _ProSource;

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
		/// 项目id
		/// </summary>
		[Field("ProjectId")]
		public string ProjectId
		{
			get{ return _ProjectId; }
			set
			{
				this.OnPropertyValueChange("ProjectId");
				this._ProjectId = value;
			}
		}
		/// <summary>
		/// 项目名称
		/// </summary>
		[Field("ProjectName")]
		public string ProjectName
		{
			get{ return _ProjectName; }
			set
			{
				this.OnPropertyValueChange("ProjectName");
				this._ProjectName = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("InsertTime")]
		public DateTime? InsertTime
		{
			get{ return _InsertTime; }
			set
			{
				this.OnPropertyValueChange("InsertTime");
				this._InsertTime = value;
			}
		}
		/// <summary>
		/// 项目来源 1：系统添加，0：BIM获取
		/// </summary>
		[Field("ProSource")]
		public int? ProSource
		{
			get{ return _ProSource; }
			set
			{
				this.OnPropertyValueChange("ProSource");
				this._ProSource = value;
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
				_.ProjectId,
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
				_.ProjectId,
				_.ProjectName,
				_.InsertTime,
				_.ProSource,
			};
        }
        /// <summary>
        /// 获取值信息
        /// </summary>
        public override object[] GetValues()
        {
            return new object[] {
				this._ID,
				this._ProjectId,
				this._ProjectName,
				this._InsertTime,
				this._ProSource,
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
			public readonly static Field All = new Field("*", "TbProjectInfo");
            /// <summary>
			/// ID
			/// </summary>
			public readonly static Field ID = new Field("ID", "TbProjectInfo", "ID");
            /// <summary>
			/// 项目id
			/// </summary>
			public readonly static Field ProjectId = new Field("ProjectId", "TbProjectInfo", "项目id");
            /// <summary>
			/// 项目名称
			/// </summary>
			public readonly static Field ProjectName = new Field("ProjectName", "TbProjectInfo", "项目名称");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field InsertTime = new Field("InsertTime", "TbProjectInfo", "");
            /// <summary>
			/// 项目来源 1：系统添加，0：BIM获取
			/// </summary>
			public readonly static Field ProSource = new Field("ProSource", "TbProjectInfo", "项目来源 1：系统添加，0：BIM获取");
        }
        #endregion
	}
}