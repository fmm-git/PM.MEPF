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
    /// 实体类TbFormEarlyWarningNodePersonnel。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("TbFormEarlyWarningNodePersonnel")]
    [Serializable]
    public partial class TbFormEarlyWarningNodePersonnel : Entity
    {
        #region Model
		private int _ID;
		private string _MenuCode;
		private int _EWNodeCode;
		private string _PersonnelSource;
		private string _PersonnelCode;
		private string _DeptId;
		private string _RoleId;
		private string _ProjectId;
		private string _EarlyWarningCode;

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
		/// 菜单编号
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
		/// 预警节点编号
		/// </summary>
		[Field("EWNodeCode")]
		public int EWNodeCode
		{
			get{ return _EWNodeCode; }
			set
			{
				this.OnPropertyValueChange("EWNodeCode");
				this._EWNodeCode = value;
			}
		}
		/// <summary>
		/// 预警人员来源
		/// </summary>
		[Field("PersonnelSource")]
		public string PersonnelSource
		{
			get{ return _PersonnelSource; }
			set
			{
				this.OnPropertyValueChange("PersonnelSource");
				this._PersonnelSource = value;
			}
		}
		/// <summary>
		/// 预警人员编号
		/// </summary>
		[Field("PersonnelCode")]
		public string PersonnelCode
		{
			get{ return _PersonnelCode; }
			set
			{
				this.OnPropertyValueChange("PersonnelCode");
				this._PersonnelCode = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("DeptId")]
		public string DeptId
		{
			get{ return _DeptId; }
			set
			{
				this.OnPropertyValueChange("DeptId");
				this._DeptId = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("RoleId")]
		public string RoleId
		{
			get{ return _RoleId; }
			set
			{
				this.OnPropertyValueChange("RoleId");
				this._RoleId = value;
			}
		}
		/// <summary>
		/// 
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
		/// 预警编号
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
		#endregion

		#region Method
        /// <summary>
        /// 获取实体中的主键列
        /// </summary>
        public override Field[] GetPrimaryKeyFields()
        {
            return new Field[] {
				_.MenuCode,
				_.EWNodeCode,
				_.PersonnelCode,
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
				_.MenuCode,
				_.EWNodeCode,
				_.PersonnelSource,
				_.PersonnelCode,
				_.DeptId,
				_.RoleId,
				_.ProjectId,
				_.EarlyWarningCode,
			};
        }
        /// <summary>
        /// 获取值信息
        /// </summary>
        public override object[] GetValues()
        {
            return new object[] {
				this._ID,
				this._MenuCode,
				this._EWNodeCode,
				this._PersonnelSource,
				this._PersonnelCode,
				this._DeptId,
				this._RoleId,
				this._ProjectId,
				this._EarlyWarningCode,
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
			public readonly static Field All = new Field("*", "TbFormEarlyWarningNodePersonnel");
            /// <summary>
			/// ID
			/// </summary>
			public readonly static Field ID = new Field("ID", "TbFormEarlyWarningNodePersonnel", "ID");
            /// <summary>
			/// 菜单编号
			/// </summary>
			public readonly static Field MenuCode = new Field("MenuCode", "TbFormEarlyWarningNodePersonnel", "菜单编号");
            /// <summary>
			/// 预警节点编号
			/// </summary>
			public readonly static Field EWNodeCode = new Field("EWNodeCode", "TbFormEarlyWarningNodePersonnel", "预警节点编号");
            /// <summary>
			/// 预警人员来源
			/// </summary>
			public readonly static Field PersonnelSource = new Field("PersonnelSource", "TbFormEarlyWarningNodePersonnel", "预警人员来源");
            /// <summary>
			/// 预警人员编号
			/// </summary>
			public readonly static Field PersonnelCode = new Field("PersonnelCode", "TbFormEarlyWarningNodePersonnel", "预警人员编号");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field DeptId = new Field("DeptId", "TbFormEarlyWarningNodePersonnel", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field RoleId = new Field("RoleId", "TbFormEarlyWarningNodePersonnel", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field ProjectId = new Field("ProjectId", "TbFormEarlyWarningNodePersonnel", "");
            /// <summary>
			/// 预警编号
			/// </summary>
			public readonly static Field EarlyWarningCode = new Field("EarlyWarningCode", "TbFormEarlyWarningNodePersonnel", "预警编号");
        }
        #endregion
	}
}