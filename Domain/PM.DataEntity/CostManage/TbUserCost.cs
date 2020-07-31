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
    /// 人员费用核算
    /// </summary>
    [Table("TbUserCost")]
    [Serializable]
    public partial class TbUserCost : BaseEntity
    {
        #region Model
		private int _ID;
		private string _CheckCode;
		private string _OrderCode;
		private string _SiteCode;
		private string _TypeCode;
		private string _TypeName;
		private string _UsePart;
		private string _ProcessFactoryCode;
		private decimal? _TotalAmount;
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
		/// 核算编号
		/// </summary>
		[Field("CheckCode")]
		public string CheckCode
		{
			get{ return _CheckCode; }
			set
			{
				this.OnPropertyValueChange("CheckCode");
				this._CheckCode = value;
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
		/// 类型编号
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
		/// 类型名称
		/// </summary>
		[Field("TypeName")]
		public string TypeName
		{
			get{ return _TypeName; }
			set
			{
				this.OnPropertyValueChange("TypeName");
				this._TypeName = value;
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
		/// 合计金额
		/// </summary>
		[Field("TotalAmount")]
		public decimal? TotalAmount
		{
			get{ return _TotalAmount; }
			set
			{
				this.OnPropertyValueChange("TotalAmount");
				this._TotalAmount = value;
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
				_.CheckCode,
				_.OrderCode,
				_.SiteCode,
				_.TypeCode,
				_.TypeName,
				_.UsePart,
				_.ProcessFactoryCode,
				_.TotalAmount,
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
				this._CheckCode,
				this._OrderCode,
				this._SiteCode,
				this._TypeCode,
				this._TypeName,
				this._UsePart,
				this._ProcessFactoryCode,
				this._TotalAmount,
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
        public class _ : BaseField<TbUserCost>
        {
			/// <summary>
			/// * 
			/// </summary>
			public readonly static Field All = new Field("*", "TbUserCost");
            /// <summary>
			/// ID
			/// </summary>
			public readonly static Field ID = new Field("ID", "TbUserCost", "ID");
            /// <summary>
			/// 核算编号
			/// </summary>
			public readonly static Field CheckCode = new Field("CheckCode", "TbUserCost", "核算编号");
            /// <summary>
			/// 订单编号
			/// </summary>
			public readonly static Field OrderCode = new Field("OrderCode", "TbUserCost", "订单编号");
            /// <summary>
			/// 站点编号
			/// </summary>
			public readonly static Field SiteCode = new Field("SiteCode", "TbUserCost", "站点编号");
            /// <summary>
			/// 类型编号
			/// </summary>
			public readonly static Field TypeCode = new Field("TypeCode", "TbUserCost", "类型编号");
            /// <summary>
			/// 类型名称
			/// </summary>
			public readonly static Field TypeName = new Field("TypeName", "TbUserCost", "类型名称");
            /// <summary>
			/// 使用部位
			/// </summary>
			public readonly static Field UsePart = new Field("UsePart", "TbUserCost", "使用部位");
            /// <summary>
			/// 加工厂编号
			/// </summary>
			public readonly static Field ProcessFactoryCode = new Field("ProcessFactoryCode", "TbUserCost", "加工厂编号");
            /// <summary>
			/// 合计金额
			/// </summary>
			public readonly static Field TotalAmount = new Field("TotalAmount", "TbUserCost", "合计金额");
            /// <summary>
			/// 备注
			/// </summary>
			public readonly static Field Remark = new Field("Remark", "TbUserCost", "备注");
            /// <summary>
			/// 审批状态
			/// </summary>
			public readonly static Field Examinestatus = new Field("Examinestatus", "TbUserCost", "审批状态");
            /// <summary>
			/// 附件
			/// </summary>
			public readonly static Field Enclosure = new Field("Enclosure", "TbUserCost", "附件");
            /// <summary>
			/// 录入人
			/// </summary>
			public readonly static Field InsertUserCode = new Field("InsertUserCode", "TbUserCost", "录入人");
            /// <summary>
			/// 录入时间
			/// </summary>
			public readonly static Field InsertTime = new Field("InsertTime", "TbUserCost", "录入时间");
        }
        #endregion
	}
}