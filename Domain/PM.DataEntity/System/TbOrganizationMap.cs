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
    /// 实体类TbOrganizationMap。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("TbOrganizationMap")]
    [Serializable]
    public partial class TbOrganizationMap : Entity
    {
        #region Model
		private int _id;
		private string _Name;
		private int _Type;
		private string _CentrePoint;
		private string _LineArray;
		private string _Deviation;
		private string _ProjectId;
		private string _CompanyCode_F;
		private string _CompanyCode;
		private string _ParentCompanyCode;

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
		[Field("Name")]
		public string Name
		{
			get{ return _Name; }
			set
			{
				this.OnPropertyValueChange("Name");
				this._Name = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("Type")]
		public int Type
		{
			get{ return _Type; }
			set
			{
				this.OnPropertyValueChange("Type");
				this._Type = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("CentrePoint")]
		public string CentrePoint
		{
			get{ return _CentrePoint; }
			set
			{
				this.OnPropertyValueChange("CentrePoint");
				this._CentrePoint = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("LineArray")]
		public string LineArray
		{
			get{ return _LineArray; }
			set
			{
				this.OnPropertyValueChange("LineArray");
				this._LineArray = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("Deviation")]
		public string Deviation
		{
			get{ return _Deviation; }
			set
			{
				this.OnPropertyValueChange("Deviation");
				this._Deviation = value;
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
		/// 
		/// </summary>
		[Field("CompanyCode_F")]
		public string CompanyCode_F
		{
			get{ return _CompanyCode_F; }
			set
			{
				this.OnPropertyValueChange("CompanyCode_F");
				this._CompanyCode_F = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("CompanyCode")]
		public string CompanyCode
		{
			get{ return _CompanyCode; }
			set
			{
				this.OnPropertyValueChange("CompanyCode");
				this._CompanyCode = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("ParentCompanyCode")]
		public string ParentCompanyCode
		{
			get{ return _ParentCompanyCode; }
			set
			{
				this.OnPropertyValueChange("ParentCompanyCode");
				this._ParentCompanyCode = value;
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
				_.id,
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
				_.Name,
				_.Type,
				_.CentrePoint,
				_.LineArray,
				_.Deviation,
				_.ProjectId,
				_.CompanyCode_F,
				_.CompanyCode,
				_.ParentCompanyCode,
			};
        }
        /// <summary>
        /// 获取值信息
        /// </summary>
        public override object[] GetValues()
        {
            return new object[] {
				this._id,
				this._Name,
				this._Type,
				this._CentrePoint,
				this._LineArray,
				this._Deviation,
				this._ProjectId,
				this._CompanyCode_F,
				this._CompanyCode,
				this._ParentCompanyCode,
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
			public readonly static Field All = new Field("*", "TbOrganizationMap");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field id = new Field("id", "TbOrganizationMap", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field Name = new Field("Name", "TbOrganizationMap", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field Type = new Field("Type", "TbOrganizationMap", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field CentrePoint = new Field("CentrePoint", "TbOrganizationMap", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field LineArray = new Field("LineArray", "TbOrganizationMap", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field Deviation = new Field("Deviation", "TbOrganizationMap", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field ProjectId = new Field("ProjectId", "TbOrganizationMap", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field CompanyCode_F = new Field("CompanyCode_F", "TbOrganizationMap", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field CompanyCode = new Field("CompanyCode", "TbOrganizationMap", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field ParentCompanyCode = new Field("ParentCompanyCode", "TbOrganizationMap", "");
        }
        #endregion
	}
}