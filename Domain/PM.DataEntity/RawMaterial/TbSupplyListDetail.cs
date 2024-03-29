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
    /// 供应清单明细
    /// </summary>
    [Table("TbSupplyListDetail")]
    [Serializable]
    public partial class TbSupplyListDetail : Entity
    {
        #region Model
		private int _ID;
		private string _RawMaterialNum;
		private string _Standard;
		private string _MeasurementUnit;
		private decimal _BatchPlanQuantity;
		private string _TechnicalRequirement;
		private decimal? _HasSupplier;
		private string _Remarks;
		private string _BatchPlanNum;
		private string _MaterialName;
		private decimal? _ThisTimeCount;
		private int? _BatchPlanItemId;

		/// <summary>
		/// 加工厂批次需求计划 明细表标识ID
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
		/// 原材料编号
		/// </summary>
		[Field("RawMaterialNum")]
		public string RawMaterialNum
		{
			get{ return _RawMaterialNum; }
			set
			{
				this.OnPropertyValueChange("RawMaterialNum");
				this._RawMaterialNum = value;
			}
		}
		/// <summary>
		/// 规格
		/// </summary>
		[Field("Standard")]
		public string Standard
		{
			get{ return _Standard; }
			set
			{
				this.OnPropertyValueChange("Standard");
				this._Standard = value;
			}
		}
		/// <summary>
		/// 计量单位
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
		/// 批次计划数量
		/// </summary>
		[Field("BatchPlanQuantity")]
		public decimal BatchPlanQuantity
		{
			get{ return _BatchPlanQuantity; }
			set
			{
				this.OnPropertyValueChange("BatchPlanQuantity");
				this._BatchPlanQuantity = value;
			}
		}
		/// <summary>
		/// 技术要求
		/// </summary>
		[Field("TechnicalRequirement")]
		public string TechnicalRequirement
		{
			get{ return _TechnicalRequirement; }
			set
			{
				this.OnPropertyValueChange("TechnicalRequirement");
				this._TechnicalRequirement = value;
			}
		}
		/// <summary>
		/// 已供货量
		/// </summary>
		[Field("HasSupplier")]
		public decimal? HasSupplier
		{
			get{ return _HasSupplier; }
			set
			{
				this.OnPropertyValueChange("HasSupplier");
				this._HasSupplier = value;
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
		/// 批次计划编号
		/// </summary>
		[Field("BatchPlanNum")]
		public string BatchPlanNum
		{
			get{ return _BatchPlanNum; }
			set
			{
				this.OnPropertyValueChange("BatchPlanNum");
				this._BatchPlanNum = value;
			}
		}
		/// <summary>
		/// 
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
		/// 本次供货量
		/// </summary>
		[Field("ThisTimeCount")]
		public decimal? ThisTimeCount
		{
			get{ return _ThisTimeCount; }
			set
			{
				this.OnPropertyValueChange("ThisTimeCount");
				this._ThisTimeCount = value;
			}
		}
		/// <summary>
		/// 批次计划明细Id
		/// </summary>
		[Field("BatchPlanItemId")]
		public int? BatchPlanItemId
		{
			get{ return _BatchPlanItemId; }
			set
			{
				this.OnPropertyValueChange("BatchPlanItemId");
				this._BatchPlanItemId = value;
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
				_.RawMaterialNum,
				_.Standard,
				_.MeasurementUnit,
				_.BatchPlanQuantity,
				_.TechnicalRequirement,
				_.HasSupplier,
				_.Remarks,
				_.BatchPlanNum,
				_.MaterialName,
				_.ThisTimeCount,
				_.BatchPlanItemId,
			};
        }
        /// <summary>
        /// 获取值信息
        /// </summary>
        public override object[] GetValues()
        {
            return new object[] {
				this._ID,
				this._RawMaterialNum,
				this._Standard,
				this._MeasurementUnit,
				this._BatchPlanQuantity,
				this._TechnicalRequirement,
				this._HasSupplier,
				this._Remarks,
				this._BatchPlanNum,
				this._MaterialName,
				this._ThisTimeCount,
				this._BatchPlanItemId,
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
			public readonly static Field All = new Field("*", "TbSupplyListDetail");
            /// <summary>
			/// 加工厂批次需求计划 明细表标识ID
			/// </summary>
			public readonly static Field ID = new Field("ID", "TbSupplyListDetail", "加工厂批次需求计划 明细表标识ID");
            /// <summary>
			/// 原材料编号
			/// </summary>
			public readonly static Field RawMaterialNum = new Field("RawMaterialNum", "TbSupplyListDetail", "原材料编号");
            /// <summary>
			/// 规格
			/// </summary>
			public readonly static Field Standard = new Field("Standard", "TbSupplyListDetail", "规格");
            /// <summary>
			/// 计量单位
			/// </summary>
			public readonly static Field MeasurementUnit = new Field("MeasurementUnit", "TbSupplyListDetail", "计量单位");
            /// <summary>
			/// 批次计划数量
			/// </summary>
			public readonly static Field BatchPlanQuantity = new Field("BatchPlanQuantity", "TbSupplyListDetail", "批次计划数量");
            /// <summary>
			/// 技术要求
			/// </summary>
			public readonly static Field TechnicalRequirement = new Field("TechnicalRequirement", "TbSupplyListDetail", "技术要求");
            /// <summary>
			/// 已供货量
			/// </summary>
			public readonly static Field HasSupplier = new Field("HasSupplier", "TbSupplyListDetail", "已供货量");
            /// <summary>
			/// 备注
			/// </summary>
			public readonly static Field Remarks = new Field("Remarks", "TbSupplyListDetail", "备注");
            /// <summary>
			/// 批次计划编号
			/// </summary>
			public readonly static Field BatchPlanNum = new Field("BatchPlanNum", "TbSupplyListDetail", "批次计划编号");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field MaterialName = new Field("MaterialName", "TbSupplyListDetail", "");
            /// <summary>
			/// 本次供货量
			/// </summary>
			public readonly static Field ThisTimeCount = new Field("ThisTimeCount", "TbSupplyListDetail", "本次供货量");
            /// <summary>
			/// 批次计划明细Id
			/// </summary>
			public readonly static Field BatchPlanItemId = new Field("BatchPlanItemId", "TbSupplyListDetail", "批次计划明细Id");
        }
        #endregion
	}
}