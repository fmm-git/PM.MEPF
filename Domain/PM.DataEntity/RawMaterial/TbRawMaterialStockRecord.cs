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
    ///原材料库存记录
    /// </summary>
    [Table("TbRawMaterialStockRecord")]
    [Serializable]
    public partial class TbRawMaterialStockRecord : Entity
    {
        #region Model
		private int _ID;
		private string _SiteCode;
		private string _MaterialCode;
		private string _MaterialName;
		private string _SpecificationModel;
		private decimal _Count;
		private decimal _UseCount;
        private decimal? _LockCount;
        private decimal _HistoryCount;
		private string _Factory;
		private string _BatchNumber;
		private string _TestReportNo;
		private string _ProjectId;
		private string _StorageCode;
		private DateTime _InsertTime;
		private string _WorkAreaCode;
		private int? _RawMaterialStockId;
		private int? _InOrderitemId;
		private string _MeasurementUnit;
		private string _MaterialType;
		private string _RebarType;
		private string _InsertUserCode;
		private string _Enclosure;
		private int? _ChackState;

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
		/// 原材料编号
		/// </summary>
		[Field("MaterialCode")]
		public string MaterialCode
		{
			get{ return _MaterialCode; }
			set
			{
				this.OnPropertyValueChange("MaterialCode");
				this._MaterialCode = value;
			}
		}
		/// <summary>
		/// 原材料名称
		/// </summary>
		[Field("MaterialName")]
		public string MaterialName
		{
			get{ return _MaterialName; }
			set
			{
				this.OnPropertyValueChange("MaterialName");
				this._MaterialName = value;
			}
		}
		/// <summary>
		/// 规格
		/// </summary>
		[Field("SpecificationModel")]
		public string SpecificationModel
		{
			get{ return _SpecificationModel; }
			set
			{
				this.OnPropertyValueChange("SpecificationModel");
				this._SpecificationModel = value;
			}
		}
		/// <summary>
		/// 库存数量
		/// </summary>
		[Field("Count")]
		public decimal Count
		{
			get{ return _Count; }
			set
			{
				this.OnPropertyValueChange("Count");
				this._Count = value;
			}
		}
		/// <summary>
		/// 使用数量
		/// </summary>
		[Field("UseCount")]
		public decimal UseCount
		{
			get{ return _UseCount; }
			set
			{
				this.OnPropertyValueChange("UseCount");
				this._UseCount = value;
			}
		}
		/// <summary>
		/// 锁定数量
		/// </summary>
		[Field("LockCount")]
		public decimal? LockCount
		{
			get{ return _LockCount; }
			set
			{
				this.OnPropertyValueChange("LockCount");
				this._LockCount = value;
			}
		}
        /// <summary>
        /// 初始数量
        /// </summary>
        [Field("HistoryCount")]
        public decimal HistoryCount
        {
            get { return _HistoryCount; }
            set
            {
                this.OnPropertyValueChange("HistoryCount");
                this._HistoryCount = value;
            }
        }
		/// <summary>
		/// 厂家
		/// </summary>
		[Field("Factory")]
		public string Factory
		{
			get{ return _Factory; }
			set
			{
				this.OnPropertyValueChange("Factory");
				this._Factory = value;
			}
		}
		/// <summary>
		/// 炉批号
		/// </summary>
		[Field("BatchNumber")]
		public string BatchNumber
		{
			get{ return _BatchNumber; }
			set
			{
				this.OnPropertyValueChange("BatchNumber");
				this._BatchNumber = value;
			}
		}
		/// <summary>
		/// 质检报告
		/// </summary>
		[Field("TestReportNo")]
		public string TestReportNo
		{
			get{ return _TestReportNo; }
			set
			{
				this.OnPropertyValueChange("TestReportNo");
				this._TestReportNo = value;
			}
		}
		/// <summary>
		/// 项目ID
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
		/// 仓库编号
		/// </summary>
		[Field("StorageCode")]
		public string StorageCode
		{
			get{ return _StorageCode; }
			set
			{
				this.OnPropertyValueChange("StorageCode");
				this._StorageCode = value;
			}
		}
		/// <summary>
		/// 录入时间
		/// </summary>
		[Field("InsertTime")]
		public DateTime InsertTime
		{
			get{ return _InsertTime; }
			set
			{
				this.OnPropertyValueChange("InsertTime");
				this._InsertTime = value;
			}
		}
		/// <summary>
		/// 工区
		/// </summary>
		[Field("WorkAreaCode")]
		public string WorkAreaCode
		{
			get{ return _WorkAreaCode; }
			set
			{
				this.OnPropertyValueChange("WorkAreaCode");
				this._WorkAreaCode = value;
			}
		}
		/// <summary>
		/// 期初库存id
		/// </summary>
		[Field("RawMaterialStockId")]
		public int? RawMaterialStockId
		{
			get{ return _RawMaterialStockId; }
			set
			{
				this.OnPropertyValueChange("RawMaterialStockId");
				this._RawMaterialStockId = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("InOrderitemId")]
		public int? InOrderitemId
		{
			get{ return _InOrderitemId; }
			set
			{
				this.OnPropertyValueChange("InOrderitemId");
				this._InOrderitemId = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("MeasurementUnit")]
		public string MeasurementUnit
		{
			get{ return _MeasurementUnit; }
			set
			{
				this.OnPropertyValueChange("MeasurementUnit");
				this._MeasurementUnit = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("MaterialType")]
		public string MaterialType
		{
			get{ return _MaterialType; }
			set
			{
				this.OnPropertyValueChange("MaterialType");
				this._MaterialType = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("RebarType")]
		public string RebarType
		{
			get{ return _RebarType; }
			set
			{
				this.OnPropertyValueChange("RebarType");
				this._RebarType = value;
			}
		}
		/// <summary>
		/// 
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
		/// 
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
		/// 检测结果（0未检测 1合格 2不合格）
		/// </summary>
		[Field("ChackState")]
		public int? ChackState
		{
			get{ return _ChackState; }
			set
			{
				this.OnPropertyValueChange("ChackState");
				this._ChackState = value;
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
				_.SiteCode,
				_.MaterialCode,
				_.MaterialName,
				_.SpecificationModel,
				_.Count,
				_.UseCount,
				_.LockCount, 
				_.HistoryCount,
				_.Factory,
				_.BatchNumber,
				_.TestReportNo,
				_.ProjectId,
				_.StorageCode,
				_.InsertTime,
				_.WorkAreaCode,
				_.RawMaterialStockId,
				_.InOrderitemId,
				_.MeasurementUnit,
				_.MaterialType,
				_.RebarType,
				_.InsertUserCode,
				_.Enclosure,
				_.ChackState,
			};
        }
        /// <summary>
        /// 获取值信息
        /// </summary>
        public override object[] GetValues()
        {
            return new object[] {
				this._ID,
				this._SiteCode,
				this._MaterialCode,
				this._MaterialName,
				this._SpecificationModel,
				this._Count,
				this._UseCount,
				this._LockCount,
				this._HistoryCount,
				this._Factory,
				this._BatchNumber,
				this._TestReportNo,
				this._ProjectId,
				this._StorageCode,
				this._InsertTime,
				this._WorkAreaCode,
				this._RawMaterialStockId,
				this._InOrderitemId,
				this._MeasurementUnit,
				this._MaterialType,
				this._RebarType,
				this._InsertUserCode,
				this._Enclosure,
				this._ChackState,
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
			public readonly static Field All = new Field("*", "TbRawMaterialStockRecord");
            /// <summary>
			/// ID
			/// </summary>
			public readonly static Field ID = new Field("ID", "TbRawMaterialStockRecord", "ID");
            /// <summary>
			/// 站点编号
			/// </summary>
			public readonly static Field SiteCode = new Field("SiteCode", "TbRawMaterialStockRecord", "站点编号");
            /// <summary>
			/// 原材料编号
			/// </summary>
			public readonly static Field MaterialCode = new Field("MaterialCode", "TbRawMaterialStockRecord", "原材料编号");
            /// <summary>
			/// 原材料名称
			/// </summary>
			public readonly static Field MaterialName = new Field("MaterialName", "TbRawMaterialStockRecord", "原材料名称");
            /// <summary>
			/// 规格
			/// </summary>
			public readonly static Field SpecificationModel = new Field("SpecificationModel", "TbRawMaterialStockRecord", "规格");
            /// <summary>
			/// 库存数量
			/// </summary>
			public readonly static Field Count = new Field("Count", "TbRawMaterialStockRecord", "库存数量");
            /// <summary>
			/// 使用数量
			/// </summary>
			public readonly static Field UseCount = new Field("UseCount", "TbRawMaterialStockRecord", "使用数量");
            /// <summary>
			/// 锁定数量
			/// </summary>
			public readonly static Field LockCount = new Field("LockCount", "TbRawMaterialStockRecord", "锁定数量");
            /// <summary>
			/// 厂家
			/// </summary>
			public readonly static Field Factory = new Field("Factory", "TbRawMaterialStockRecord", "厂家");
            /// <summary>
			/// 炉批号
			/// </summary>
			public readonly static Field BatchNumber = new Field("BatchNumber", "TbRawMaterialStockRecord", "炉批号");
            /// <summary>
			/// 质检报告
			/// </summary>
			public readonly static Field TestReportNo = new Field("TestReportNo", "TbRawMaterialStockRecord", "质检报告");
            /// <summary>
			/// 项目ID
			/// </summary>
			public readonly static Field ProjectId = new Field("ProjectId", "TbRawMaterialStockRecord", "项目ID");
            /// <summary>
			/// 仓库编号
			/// </summary>
			public readonly static Field StorageCode = new Field("StorageCode", "TbRawMaterialStockRecord", "仓库编号");
            /// <summary>
			/// 录入时间
			/// </summary>
			public readonly static Field InsertTime = new Field("InsertTime", "TbRawMaterialStockRecord", "录入时间");
            /// <summary>
			/// 工区
			/// </summary>
			public readonly static Field WorkAreaCode = new Field("WorkAreaCode", "TbRawMaterialStockRecord", "工区");
            /// <summary>
			/// 期初库存id
			/// </summary>
			public readonly static Field RawMaterialStockId = new Field("RawMaterialStockId", "TbRawMaterialStockRecord", "期初库存id");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field InOrderitemId = new Field("InOrderitemId", "TbRawMaterialStockRecord", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field MeasurementUnit = new Field("MeasurementUnit", "TbRawMaterialStockRecord", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field MaterialType = new Field("MaterialType", "TbRawMaterialStockRecord", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field RebarType = new Field("RebarType", "TbRawMaterialStockRecord", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field InsertUserCode = new Field("InsertUserCode", "TbRawMaterialStockRecord", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field Enclosure = new Field("Enclosure", "TbRawMaterialStockRecord", "");
            /// <summary>
            ///  初始数量
			/// </summary>
            public readonly static Field HistoryCount = new Field("HistoryCount", "TbRawMaterialStockRecord", "初始数量");
            /// <summary>
			/// 检测结果（0未检测 1合格 2不合格）
			/// </summary>
			public readonly static Field ChackState = new Field("ChackState", "TbRawMaterialStockRecord", "检测结果（0未检测 1合格 2不合格）");
        }
        #endregion

        #region 扩展字段
        public int IndexNum { get; set; }
        public string BranchCode { get; set; }
        public decimal? UseCountS { get; set; }

        //是否工区抵扣
        public bool IsGQDK { get; set; }

        #endregion
	}
}