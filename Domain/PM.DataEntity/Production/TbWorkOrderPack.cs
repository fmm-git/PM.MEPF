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
    /// 实体类TbWorkOrderPack。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("TbWorkOrderPack")]
    [Serializable]
    public partial class TbWorkOrderPack : Entity
    {
        #region Model
		private int _ID;
		private string _OrderCode;
		private string _PackCode;
		private string _SystemType;
		private string _MaterialType;
		private string _ComponentName;
		private string _OrderDetialId;
		private int? _ThisPackNumber;
		private DateTime? _PackDate;
		private string _Remark;
		private string _PackUserCode;

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
		/// 包件号
		/// </summary>
		[Field("PackCode")]
		public string PackCode
		{
			get{ return _PackCode; }
			set
			{
				this.OnPropertyValueChange("PackCode");
				this._PackCode = value;
			}
		}
		/// <summary>
		/// 系统类型
		/// </summary>
		[Field("SystemType")]
		public string SystemType
		{
			get{ return _SystemType; }
			set
			{
				this.OnPropertyValueChange("SystemType");
				this._SystemType = value;
			}
		}
		/// <summary>
		/// 材料类型
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
		/// 构建名称
		/// </summary>
		[Field("ComponentName")]
		public string ComponentName
		{
			get{ return _ComponentName; }
			set
			{
				this.OnPropertyValueChange("ComponentName");
				this._ComponentName = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[Field("OrderDetialId")]
		public string OrderDetialId
		{
			get{ return _OrderDetialId; }
			set
			{
				this.OnPropertyValueChange("OrderDetialId");
				this._OrderDetialId = value;
			}
		}
		/// <summary>
		/// 本包数量
		/// </summary>
		[Field("ThisPackNumber")]
		public int? ThisPackNumber
		{
			get{ return _ThisPackNumber; }
			set
			{
				this.OnPropertyValueChange("ThisPackNumber");
				this._ThisPackNumber = value;
			}
		}
		/// <summary>
		/// 打包日期
		/// </summary>
		[Field("PackDate")]
		public DateTime? PackDate
		{
			get{ return _PackDate; }
			set
			{
				this.OnPropertyValueChange("PackDate");
				this._PackDate = value;
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
		/// 打包用户
		/// </summary>
		[Field("PackUserCode")]
		public string PackUserCode
		{
			get{ return _PackUserCode; }
			set
			{
				this.OnPropertyValueChange("PackUserCode");
				this._PackUserCode = value;
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
				_.OrderCode,
				_.PackCode,
				_.SystemType,
				_.MaterialType,
				_.ComponentName,
				_.OrderDetialId,
				_.ThisPackNumber,
				_.PackDate,
				_.Remark,
				_.PackUserCode,
			};
        }
        /// <summary>
        /// 获取值信息
        /// </summary>
        public override object[] GetValues()
        {
            return new object[] {
				this._ID,
				this._OrderCode,
				this._PackCode,
				this._SystemType,
				this._MaterialType,
				this._ComponentName,
				this._OrderDetialId,
				this._ThisPackNumber,
				this._PackDate,
				this._Remark,
				this._PackUserCode,
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
			public readonly static Field All = new Field("*", "TbWorkOrderPack");
            /// <summary>
			/// ID
			/// </summary>
			public readonly static Field ID = new Field("ID", "TbWorkOrderPack", "ID");
            /// <summary>
			/// 订单编号
			/// </summary>
			public readonly static Field OrderCode = new Field("OrderCode", "TbWorkOrderPack", "订单编号");
            /// <summary>
			/// 包件号
			/// </summary>
			public readonly static Field PackCode = new Field("PackCode", "TbWorkOrderPack", "包件号");
            /// <summary>
			/// 系统类型
			/// </summary>
			public readonly static Field SystemType = new Field("SystemType", "TbWorkOrderPack", "系统类型");
            /// <summary>
			/// 材料类型
			/// </summary>
			public readonly static Field MaterialType = new Field("MaterialType", "TbWorkOrderPack", "材料类型");
            /// <summary>
			/// 构建名称
			/// </summary>
			public readonly static Field ComponentName = new Field("ComponentName", "TbWorkOrderPack", "构建名称");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field OrderDetialId = new Field("OrderDetialId", "TbWorkOrderPack", "");
            /// <summary>
			/// 本包数量
			/// </summary>
			public readonly static Field ThisPackNumber = new Field("ThisPackNumber", "TbWorkOrderPack", "本包数量");
            /// <summary>
			/// 打包日期
			/// </summary>
			public readonly static Field PackDate = new Field("PackDate", "TbWorkOrderPack", "打包日期");
            /// <summary>
			/// 备注
			/// </summary>
			public readonly static Field Remark = new Field("Remark", "TbWorkOrderPack", "备注");
            /// <summary>
			/// 打包用户
			/// </summary>
			public readonly static Field PackUserCode = new Field("PackUserCode", "TbWorkOrderPack", "打包用户");
        }
        #endregion
	}
}