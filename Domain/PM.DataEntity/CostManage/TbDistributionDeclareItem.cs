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
    /// 配送费用申报明细
    /// </summary>
    [Table("TbDistributionDeclareItem")]
    [Serializable]
    public partial class TbDistributionDeclareItem : Entity
    {
        #region Model
		private int _ID;
		private string _DistributionDeclareCode;
		private string _TypeCode;
		private string _SiteCode;
		private string _UsePart;
		private string _Unit;
		private decimal _DistributionCount;
		private decimal _Price;
		private decimal _Amount;
		private DateTime? _DistributionTime;
		private string _Remarks;

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
		[Field("DistributionDeclareCode")]
		public string DistributionDeclareCode
		{
			get{ return _DistributionDeclareCode; }
			set
			{
				this.OnPropertyValueChange("DistributionDeclareCode");
				this._DistributionDeclareCode = value;
			}
		}
		/// <summary>
		/// 订单类型编码
		/// </summary>
		[Field("TypeCode")]
		public string TypeCode
		{
			get{ return _TypeCode; }
			set
			{
				this.OnPropertyValueChange("TypeCode");
				this._TypeCode = value;
			}
		}
		/// <summary>
		/// 工点
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
		/// 使用部位
		/// </summary>
		[Field("UsePart")]
		public string UsePart
		{
			get{ return _UsePart; }
			set
			{
				this.OnPropertyValueChange("UsePart");
				this._UsePart = value;
			}
		}
		/// <summary>
		/// 单位
		/// </summary>
		[Field("Unit")]
		public string Unit
		{
			get{ return _Unit; }
			set
			{
				this.OnPropertyValueChange("Unit");
				this._Unit = value;
			}
		}
		/// <summary>
		/// 配送数量
		/// </summary>
		[Field("DistributionCount")]
		public decimal DistributionCount
		{
			get{ return _DistributionCount; }
			set
			{
				this.OnPropertyValueChange("DistributionCount");
				this._DistributionCount = value;
			}
		}
		/// <summary>
		/// 单价
		/// </summary>
		[Field("Price")]
		public decimal Price
		{
			get{ return _Price; }
			set
			{
				this.OnPropertyValueChange("Price");
				this._Price = value;
			}
		}
		/// <summary>
		/// 金额
		/// </summary>
		[Field("Amount")]
		public decimal Amount
		{
			get{ return _Amount; }
			set
			{
				this.OnPropertyValueChange("Amount");
				this._Amount = value;
			}
		}
		/// <summary>
		/// 配送日期
		/// </summary>
		[Field("DistributionTime")]
		public DateTime? DistributionTime
		{
			get{ return _DistributionTime; }
			set
			{
				this.OnPropertyValueChange("DistributionTime");
				this._DistributionTime = value;
			}
		}
		/// <summary>
		/// 备注
		/// </summary>
		[Field("Remarks")]
		public string Remarks
		{
			get{ return _Remarks; }
			set
			{
				this.OnPropertyValueChange("Remarks");
				this._Remarks = value;
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
				_.DistributionDeclareCode,
				_.TypeCode,
				_.SiteCode,
				_.UsePart,
				_.Unit,
				_.DistributionCount,
				_.Price,
				_.Amount,
				_.DistributionTime,
				_.Remarks,
			};
        }
        /// <summary>
        /// 获取值信息
        /// </summary>
        public override object[] GetValues()
        {
            return new object[] {
				this._ID,
				this._DistributionDeclareCode,
				this._TypeCode,
				this._SiteCode,
				this._UsePart,
				this._Unit,
				this._DistributionCount,
				this._Price,
				this._Amount,
				this._DistributionTime,
				this._Remarks,
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
			public readonly static Field All = new Field("*", "TbDistributionDeclareItem");
            /// <summary>
			/// ID
			/// </summary>
			public readonly static Field ID = new Field("ID", "TbDistributionDeclareItem", "ID");
            /// <summary>
			/// 申报编号
			/// </summary>
			public readonly static Field DistributionDeclareCode = new Field("DistributionDeclareCode", "TbDistributionDeclareItem", "申报编号");
            /// <summary>
			/// 订单类型编码
			/// </summary>
			public readonly static Field TypeCode = new Field("TypeCode", "TbDistributionDeclareItem", "订单类型编码");
            /// <summary>
			/// 工点
			/// </summary>
			public readonly static Field SiteCode = new Field("SiteCode", "TbDistributionDeclareItem", "工点");
            /// <summary>
			/// 使用部位
			/// </summary>
			public readonly static Field UsePart = new Field("UsePart", "TbDistributionDeclareItem", "使用部位");
            /// <summary>
			/// 单位
			/// </summary>
			public readonly static Field Unit = new Field("Unit", "TbDistributionDeclareItem", "单位");
            /// <summary>
			/// 配送数量
			/// </summary>
			public readonly static Field DistributionCount = new Field("DistributionCount", "TbDistributionDeclareItem", "配送数量");
            /// <summary>
			/// 单价
			/// </summary>
			public readonly static Field Price = new Field("Price", "TbDistributionDeclareItem", "单价");
            /// <summary>
			/// 金额
			/// </summary>
			public readonly static Field Amount = new Field("Amount", "TbDistributionDeclareItem", "金额");
            /// <summary>
			/// 配送日期
			/// </summary>
			public readonly static Field DistributionTime = new Field("DistributionTime", "TbDistributionDeclareItem", "配送日期");
            /// <summary>
			/// 备注
			/// </summary>
			public readonly static Field Remarks = new Field("Remarks", "TbDistributionDeclareItem", "备注");
        }
        #endregion
	}
}