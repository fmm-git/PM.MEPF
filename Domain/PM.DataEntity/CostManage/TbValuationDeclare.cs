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
using PM.DataEntity.Base;

namespace PM.DataEntity
{
    /// <summary>
    /// 辅料计划申报
    /// </summary>
    [Table("TbValuationDeclare")]
    [Serializable]
    public partial class TbValuationDeclare : BaseEntity
    {
        #region Model
		private int _ID;
		private string _ValuationDeclareCode;
		private string _ProcessFactoryCode;
		private string _DeclareUserCode;
		private DateTime? _DeclareTime;
		private string _BranchCode;
		private string _SiteCode;
		private decimal _TotalAmount;
		private string _ContractName;
		private string _Remark;
		private string _Examinestatus;
		private string _Enclosure;
		private string _InsertUserCode;
		private string _InsertTime;

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
		/// 申报编号
		/// </summary>
		[Field("ValuationDeclareCode")]
		public string ValuationDeclareCode
		{
			get{ return _ValuationDeclareCode; }
			set
			{
				this.OnPropertyValueChange("ValuationDeclareCode");
				this._ValuationDeclareCode = value;
			}
		}
		/// <summary>
		/// 加工厂编号
		/// </summary>
		[Field("ProcessFactoryCode")]
		public string ProcessFactoryCode
		{
			get{ return _ProcessFactoryCode; }
			set
			{
				this.OnPropertyValueChange("ProcessFactoryCode");
				this._ProcessFactoryCode = value;
			}
		}
		/// <summary>
		/// 申报人
		/// </summary>
		[Field("DeclareUserCode")]
		public string DeclareUserCode
		{
			get{ return _DeclareUserCode; }
			set
			{
				this.OnPropertyValueChange("DeclareUserCode");
				this._DeclareUserCode = value;
			}
		}
		/// <summary>
		/// 申报时间
		/// </summary>
		[Field("DeclareTime")]
		public DateTime? DeclareTime
		{
			get{ return _DeclareTime; }
			set
			{
				this.OnPropertyValueChange("DeclareTime");
				this._DeclareTime = value;
			}
		}
		/// <summary>
		/// 分部编号
		/// </summary>
		[Field("BranchCode")]
		public string BranchCode
		{
			get{ return _BranchCode; }
			set
			{
				this.OnPropertyValueChange("BranchCode");
				this._BranchCode = value;
			}
		}
		/// <summary>
		/// 站点编号
		/// </summary>
		[Field("SiteCode")]
		public string SiteCode
		{
			get{ return _SiteCode; }
			set
			{
				this.OnPropertyValueChange("SiteCode");
				this._SiteCode = value;
			}
		}
		/// <summary>
		/// 合计金额
		/// </summary>
		[Field("TotalAmount")]
		public decimal TotalAmount
		{
			get{ return _TotalAmount; }
			set
			{
				this.OnPropertyValueChange("TotalAmount");
				this._TotalAmount = value;
			}
		}
		/// <summary>
		/// 合同名称
		/// </summary>
		[Field("ContractName")]
		public string ContractName
		{
			get{ return _ContractName; }
			set
			{
				this.OnPropertyValueChange("ContractName");
				this._ContractName = value;
			}
		}
		/// <summary>
		/// 备注
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
		/// 审批状态
		/// </summary>
		[Field("Examinestatus")]
		public string Examinestatus
		{
			get{ return _Examinestatus; }
			set
			{
				this.OnPropertyValueChange("Examinestatus");
				this._Examinestatus = value;
			}
		}
		/// <summary>
		/// 附件
		/// </summary>
		[Field("Enclosure")]
		public string Enclosure
		{
			get{ return _Enclosure; }
			set
			{
				this.OnPropertyValueChange("Enclosure");
				this._Enclosure = value;
			}
		}
		/// <summary>
		/// 录入人
		/// </summary>
		[Field("InsertUserCode")]
		public string InsertUserCode
		{
			get{ return _InsertUserCode; }
			set
			{
				this.OnPropertyValueChange("InsertUserCode");
				this._InsertUserCode = value;
			}
		}
		/// <summary>
		/// 录入时间
		/// </summary>
		[Field("InsertTime")]
		public string InsertTime
		{
			get{ return _InsertTime; }
			set
			{
				this.OnPropertyValueChange("InsertTime");
				this._InsertTime = value;
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
				_.ID,
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
				_.ValuationDeclareCode,
				_.ProcessFactoryCode,
				_.DeclareUserCode,
				_.DeclareTime,
				_.BranchCode,
				_.SiteCode,
				_.TotalAmount,
				_.ContractName,
				_.Remark,
				_.Examinestatus,
				_.Enclosure,
				_.InsertUserCode,
				_.InsertTime,
                _.ProjectId
			};
        }
        /// <summary>
        /// 获取值信息
        /// </summary>
        public override object[] GetValues()
        {
            return new object[] {
				this._ID,
				this._ValuationDeclareCode,
				this._ProcessFactoryCode,
				this._DeclareUserCode,
				this._DeclareTime,
				this._BranchCode,
				this._SiteCode,
				this._TotalAmount,
				this._ContractName,
				this._Remark,
				this._Examinestatus,
				this._Enclosure,
				this._InsertUserCode,
				this._InsertTime,
                this._ProjectId
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
        public class _ : BaseField<TbValuationDeclare>
        {
			/// <summary>
			/// * 
			/// </summary>
			public readonly static Field All = new Field("*", "TbValuationDeclare");
            /// <summary>
			/// ID
			/// </summary>
			public readonly static Field ID = new Field("ID", "TbValuationDeclare", "ID");
            /// <summary>
			/// 申报编号
			/// </summary>
			public readonly static Field ValuationDeclareCode = new Field("ValuationDeclareCode", "TbValuationDeclare", "申报编号");
            /// <summary>
			/// 加工厂编号
			/// </summary>
			public readonly static Field ProcessFactoryCode = new Field("ProcessFactoryCode", "TbValuationDeclare", "加工厂编号");
            /// <summary>
			/// 申报人
			/// </summary>
			public readonly static Field DeclareUserCode = new Field("DeclareUserCode", "TbValuationDeclare", "申报人");
            /// <summary>
			/// 申报时间
			/// </summary>
			public readonly static Field DeclareTime = new Field("DeclareTime", "TbValuationDeclare", "申报时间");
            /// <summary>
			/// 分部编号
			/// </summary>
			public readonly static Field BranchCode = new Field("BranchCode", "TbValuationDeclare", "分部编号");
            /// <summary>
			/// 站点编号
			/// </summary>
			public readonly static Field SiteCode = new Field("SiteCode", "TbValuationDeclare", "站点编号");
            /// <summary>
			/// 合计金额
			/// </summary>
			public readonly static Field TotalAmount = new Field("TotalAmount", "TbValuationDeclare", "合计金额");
            /// <summary>
			/// 合同名称
			/// </summary>
			public readonly static Field ContractName = new Field("ContractName", "TbValuationDeclare", "合同名称");
            /// <summary>
			/// 备注
			/// </summary>
			public readonly static Field Remark = new Field("Remark", "TbValuationDeclare", "备注");
            /// <summary>
			/// 审批状态
			/// </summary>
			public readonly static Field Examinestatus = new Field("Examinestatus", "TbValuationDeclare", "审批状态");
            /// <summary>
			/// 附件
			/// </summary>
			public readonly static Field Enclosure = new Field("Enclosure", "TbValuationDeclare", "附件");
            /// <summary>
			/// 录入人
			/// </summary>
			public readonly static Field InsertUserCode = new Field("InsertUserCode", "TbValuationDeclare", "录入人");
            /// <summary>
			/// 录入时间
			/// </summary>
			public readonly static Field InsertTime = new Field("InsertTime", "TbValuationDeclare", "录入时间");
        }
        #endregion
	}
}