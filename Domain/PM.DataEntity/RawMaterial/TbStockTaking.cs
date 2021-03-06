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
    /// 库存盘点
    /// </summary>
    [Table("TbStockTaking")]
    [Serializable]
    public partial class TbStockTaking : Entity
    {
        #region Model
		private int _ID;
		private string _WarehouseType;
		private string _TakNum;
		private DateTime? _TakDay;
		private decimal? _TotalInventory;
		private decimal? _TotalTak;
		private decimal? _TotalEarnOrLos;
		private string _Remarks;
		private string _Enclosure;
		private string _InsertUserCode;
		private DateTime? _InsertTime;
		private int? _EarnOrLos;
		private string _ProcessFactoryCode;

		/// <summary>
		/// id
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
		/// 仓库类型
		/// </summary>
		[Field("WarehouseType")]
		public string WarehouseType
		{
			get{ return _WarehouseType; }
			set
			{
				this.OnPropertyValueChange("WarehouseType");
				this._WarehouseType = value;
			}
		}
		/// <summary>
		/// 盘点单编号
		/// </summary>
		[Field("TakNum")]
		public string TakNum
		{
			get{ return _TakNum; }
			set
			{
				this.OnPropertyValueChange("TakNum");
				this._TakNum = value;
			}
		}
		/// <summary>
		/// 盘点日期
		/// </summary>
		[Field("TakDay")]
		public DateTime? TakDay
		{
			get{ return _TakDay; }
			set
			{
				this.OnPropertyValueChange("TakDay");
				this._TakDay = value;
			}
		}
		/// <summary>
		/// 库存总量
		/// </summary>
		[Field("TotalInventory")]
		public decimal? TotalInventory
		{
			get{ return _TotalInventory; }
			set
			{
				this.OnPropertyValueChange("TotalInventory");
				this._TotalInventory = value;
			}
		}
		/// <summary>
		/// 盘点总量
		/// </summary>
		[Field("TotalTak")]
		public decimal? TotalTak
		{
			get{ return _TotalTak; }
			set
			{
				this.OnPropertyValueChange("TotalTak");
				this._TotalTak = value;
			}
		}
		/// <summary>
		/// 盘盈/盘亏
		/// </summary>
		[Field("TotalEarnOrLos")]
		public decimal? TotalEarnOrLos
		{
			get{ return _TotalEarnOrLos; }
			set
			{
				this.OnPropertyValueChange("TotalEarnOrLos");
				this._TotalEarnOrLos = value;
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
		/// 
		/// </summary>
		[Field("EarnOrLos")]
		public int? EarnOrLos
		{
			get{ return _EarnOrLos; }
			set
			{
				this.OnPropertyValueChange("EarnOrLos");
				this._EarnOrLos = value;
			}
		}
		/// <summary>
		/// 
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
				_.WarehouseType,
				_.TakNum,
				_.TakDay,
				_.TotalInventory,
				_.TotalTak,
				_.TotalEarnOrLos,
				_.Remarks,
				_.Enclosure,
				_.InsertUserCode,
				_.InsertTime,
				_.EarnOrLos,
				_.ProcessFactoryCode,
			};
        }
        /// <summary>
        /// 获取值信息
        /// </summary>
        public override object[] GetValues()
        {
            return new object[] {
				this._ID,
				this._WarehouseType,
				this._TakNum,
				this._TakDay,
				this._TotalInventory,
				this._TotalTak,
				this._TotalEarnOrLos,
				this._Remarks,
				this._Enclosure,
				this._InsertUserCode,
				this._InsertTime,
				this._EarnOrLos,
				this._ProcessFactoryCode,
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
			public readonly static Field All = new Field("*", "TbStockTaking");
            /// <summary>
			/// id
			/// </summary>
			public readonly static Field ID = new Field("ID", "TbStockTaking", "id");
            /// <summary>
			/// 仓库类型
			/// </summary>
			public readonly static Field WarehouseType = new Field("WarehouseType", "TbStockTaking", "仓库类型");
            /// <summary>
			/// 盘点单编号
			/// </summary>
			public readonly static Field TakNum = new Field("TakNum", "TbStockTaking", "盘点单编号");
            /// <summary>
			/// 盘点日期
			/// </summary>
			public readonly static Field TakDay = new Field("TakDay", "TbStockTaking", "盘点日期");
            /// <summary>
			/// 库存总量
			/// </summary>
			public readonly static Field TotalInventory = new Field("TotalInventory", "TbStockTaking", "库存总量");
            /// <summary>
			/// 盘点总量
			/// </summary>
			public readonly static Field TotalTak = new Field("TotalTak", "TbStockTaking", "盘点总量");
            /// <summary>
			/// 盘盈/盘亏
			/// </summary>
			public readonly static Field TotalEarnOrLos = new Field("TotalEarnOrLos", "TbStockTaking", "盘盈/盘亏");
            /// <summary>
			/// 备注
			/// </summary>
			public readonly static Field Remarks = new Field("Remarks", "TbStockTaking", "备注");
            /// <summary>
			/// 附件
			/// </summary>
			public readonly static Field Enclosure = new Field("Enclosure", "TbStockTaking", "附件");
            /// <summary>
			/// 录入人
			/// </summary>
			public readonly static Field InsertUserCode = new Field("InsertUserCode", "TbStockTaking", "录入人");
            /// <summary>
			/// 录入时间
			/// </summary>
			public readonly static Field InsertTime = new Field("InsertTime", "TbStockTaking", "录入时间");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field EarnOrLos = new Field("EarnOrLos", "TbStockTaking", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field ProcessFactoryCode = new Field("ProcessFactoryCode", "TbStockTaking", "");
        }
        #endregion
	}
}