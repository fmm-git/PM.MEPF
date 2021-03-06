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
    /// 签收对账明细
    /// </summary>
    [Table("TbSignforDuiZhangDetail")]
    [Serializable]
    public partial class TbSignforDuiZhangDetail : Entity
    {
        #region Model
		private int _ID;
		private string _SigninNuber;
		private string _SignForNo;
		private DateTime _SignForDate;
		private decimal? _SteelWeight;
		private decimal? _GeGouZhongLiWeight;
		private decimal? _HSectionSteelWeight;
		private decimal? _GratingWeight;
		private string _Remark;
		private decimal? _AGrille;
		private string _OrderCode;
		private string _AddType;
		private DateTime? _DistributionTime;

		/// <summary>
		/// 对账明细表ID
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
		/// 对账单号，关联主表标识
		/// </summary>
		[Field("SigninNuber")]
		public string SigninNuber
		{
			get{ return _SigninNuber; }
			set
			{
				this.OnPropertyValueChange("SigninNuber");
				this._SigninNuber = value;
			}
		}
		/// <summary>
		/// 签收单号
		/// </summary>
		[Field("SignForNo")]
		public string SignForNo
		{
			get{ return _SignForNo; }
			set
			{
				this.OnPropertyValueChange("SignForNo");
				this._SignForNo = value;
			}
		}
		/// <summary>
		/// 签收日期
		/// </summary>
		[Field("SignForDate")]
		public DateTime SignForDate
		{
			get{ return _SignForDate; }
			set
			{
				this.OnPropertyValueChange("SignForDate");
				this._SignForDate = value;
			}
		}
		/// <summary>
		/// 钢筋重量
		/// </summary>
		[Field("SteelWeight")]
		public decimal? SteelWeight
		{
			get{ return _SteelWeight; }
			set
			{
				this.OnPropertyValueChange("SteelWeight");
				this._SteelWeight = value;
			}
		}
		/// <summary>
		/// 格构柱中立柱重量
		/// </summary>
		[Field("GeGouZhongLiWeight")]
		public decimal? GeGouZhongLiWeight
		{
			get{ return _GeGouZhongLiWeight; }
			set
			{
				this.OnPropertyValueChange("GeGouZhongLiWeight");
				this._GeGouZhongLiWeight = value;
			}
		}
		/// <summary>
		/// H型钢重量
		/// </summary>
		[Field("HSectionSteelWeight")]
		public decimal? HSectionSteelWeight
		{
			get{ return _HSectionSteelWeight; }
			set
			{
				this.OnPropertyValueChange("HSectionSteelWeight");
				this._HSectionSteelWeight = value;
			}
		}
		/// <summary>
		/// 格栅重量
		/// </summary>
		[Field("GratingWeight")]
		public decimal? GratingWeight
		{
			get{ return _GratingWeight; }
			set
			{
				this.OnPropertyValueChange("GratingWeight");
				this._GratingWeight = value;
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
		[Field("AGrille")]
		public decimal? AGrille
		{
			get{ return _AGrille; }
			set
			{
				this.OnPropertyValueChange("AGrille");
				this._AGrille = value;
			}
		}
		/// <summary>
		/// 订单编号
		/// </summary>
		[Field("OrderCode")]
		public string OrderCode
		{
			get{ return _OrderCode; }
			set
			{
				this.OnPropertyValueChange("OrderCode");
				this._OrderCode = value;
			}
		}
		/// <summary>
		/// 添加类型
		/// </summary>
		[Field("AddType")]
		public string AddType
		{
			get{ return _AddType; }
			set
			{
				this.OnPropertyValueChange("AddType");
				this._AddType = value;
			}
		}
		/// <summary>
		/// 配送时间
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
				_.SigninNuber,
				_.SignForNo,
				_.SignForDate,
				_.SteelWeight,
				_.GeGouZhongLiWeight,
				_.HSectionSteelWeight,
				_.GratingWeight,
				_.Remark,
				_.AGrille,
				_.OrderCode,
				_.AddType,
				_.DistributionTime,
			};
        }
        /// <summary>
        /// 获取值信息
        /// </summary>
        public override object[] GetValues()
        {
            return new object[] {
				this._ID,
				this._SigninNuber,
				this._SignForNo,
				this._SignForDate,
				this._SteelWeight,
				this._GeGouZhongLiWeight,
				this._HSectionSteelWeight,
				this._GratingWeight,
				this._Remark,
				this._AGrille,
				this._OrderCode,
				this._AddType,
				this._DistributionTime,
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
			public readonly static Field All = new Field("*", "TbSignforDuiZhangDetail");
            /// <summary>
			/// 对账明细表ID
			/// </summary>
			public readonly static Field ID = new Field("ID", "TbSignforDuiZhangDetail", "对账明细表ID");
            /// <summary>
			/// 对账单号，关联主表标识
			/// </summary>
			public readonly static Field SigninNuber = new Field("SigninNuber", "TbSignforDuiZhangDetail", "对账单号，关联主表标识");
            /// <summary>
			/// 签收单号
			/// </summary>
			public readonly static Field SignForNo = new Field("SignForNo", "TbSignforDuiZhangDetail", "签收单号");
            /// <summary>
			/// 签收日期
			/// </summary>
			public readonly static Field SignForDate = new Field("SignForDate", "TbSignforDuiZhangDetail", "签收日期");
            /// <summary>
			/// 钢筋重量
			/// </summary>
			public readonly static Field SteelWeight = new Field("SteelWeight", "TbSignforDuiZhangDetail", "钢筋重量");
            /// <summary>
			/// 格构柱中立柱重量
			/// </summary>
			public readonly static Field GeGouZhongLiWeight = new Field("GeGouZhongLiWeight", "TbSignforDuiZhangDetail", "格构柱中立柱重量");
            /// <summary>
			/// H型钢重量
			/// </summary>
			public readonly static Field HSectionSteelWeight = new Field("HSectionSteelWeight", "TbSignforDuiZhangDetail", "H型钢重量");
            /// <summary>
			/// 格栅重量
			/// </summary>
			public readonly static Field GratingWeight = new Field("GratingWeight", "TbSignforDuiZhangDetail", "格栅重量");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field Remark = new Field("Remark", "TbSignforDuiZhangDetail", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field AGrille = new Field("AGrille", "TbSignforDuiZhangDetail", "");
            /// <summary>
			/// 订单编号
			/// </summary>
			public readonly static Field OrderCode = new Field("OrderCode", "TbSignforDuiZhangDetail", "订单编号");
            /// <summary>
			/// 添加类型
			/// </summary>
			public readonly static Field AddType = new Field("AddType", "TbSignforDuiZhangDetail", "添加类型");
            /// <summary>
			/// 配送时间
			/// </summary>
			public readonly static Field DistributionTime = new Field("DistributionTime", "TbSignforDuiZhangDetail", "配送时间");
        }
        #endregion
	}
}