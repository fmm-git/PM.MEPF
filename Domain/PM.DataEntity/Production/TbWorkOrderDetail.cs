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
using PM.Common.Extension;

namespace PM.DataEntity
{
    /// <summary>
    /// 实体类TbWorkOrderDetail。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("TbWorkOrderDetail")]
    [Serializable]
    public partial class TbWorkOrderDetail : Entity
    {
        #region Model
		private int _ID;
		private string _OrderCode;
		private string _StationCode;
		private string _ComponentMajor;
		private string _BigSystem;
		private string _SonSystem;
		private string _SystemType;
		private string _MaterialType;
		private string _ComponentCode;
		private string _ComponentName;
		private string _MaterialQuality;
		private string _SpecificationModel;
		private string _Length;
		private string _Area;
		private string _Material;
		private string _MxGjBm;
		private string _MxGjId;
		private string _Remark;
		private string _LargePattern;
		private string _ComponentStrat;
		private string _IsPack;
		private DateTime? _PackDate;
		private string _PackUserCode;
		private string _IsSignFor;
		private DateTime? _SignForDate;
		private string _SignForUserCode;
		private string _IsInstall;
		private DateTime? _InstallDate;
		private string _InstallUserCode;
		private string _PackCode;
		private string _RevokeStart;
		private string _SiteCode;

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
		/// 车站编号
		/// </summary>
		[Field("StationCode")]
		public string StationCode
		{
			get{ return _StationCode; }
			set
			{
				this.OnPropertyValueChange("StationCode");
				this._StationCode = value;
			}
		}
		/// <summary>
		/// 构件专业
		/// </summary>
		[Field("ComponentMajor")]
		public string ComponentMajor
		{
			get{ return _ComponentMajor; }
			set
			{
				this.OnPropertyValueChange("ComponentMajor");
				this._ComponentMajor = value;
			}
		}
		/// <summary>
		/// 大系统
		/// </summary>
		[Field("BigSystem")]
		public string BigSystem
		{
			get{ return _BigSystem; }
			set
			{
				this.OnPropertyValueChange("BigSystem");
				this._BigSystem = value;
			}
		}
		/// <summary>
		/// 子系统
		/// </summary>
		[Field("SonSystem")]
		public string SonSystem
		{
			get{ return _SonSystem; }
			set
			{
				this.OnPropertyValueChange("SonSystem");
				this._SonSystem = value;
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
		/// 构件编号
		/// </summary>
		[Field("ComponentCode")]
		public string ComponentCode
		{
			get { return _ComponentCode; }
			set
			{
                this.OnPropertyValueChange("ComponentCode");
                //this._ComponentCode = value;
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
		/// 材质
		/// </summary>
		[Field("MaterialQuality")]
		public string MaterialQuality
		{
			get{ return _MaterialQuality; }
			set
			{
				this.OnPropertyValueChange("MaterialQuality");
				this._MaterialQuality = value;
			}
		}
		/// <summary>
		/// 规格型号
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
		/// 长度
		/// </summary>
		[Field("Length")]
		public string Length
		{
			get{ return _Length; }
			set
			{
				this.OnPropertyValueChange("Length");
				this._Length = value;
			}
		}
		/// <summary>
		/// 面积
		/// </summary>
		[Field("Area")]
		public string Area
		{
			get{ return _Area; }
			set
			{
				this.OnPropertyValueChange("Area");
				this._Area = value;
			}
		}
		/// <summary>
		/// 材料名称
		/// </summary>
		[Field("Material")]
		public string Material
		{
			get{ return _Material; }
			set
			{
				this.OnPropertyValueChange("Material");
				this._Material = value;
			}
		}
		/// <summary>
		/// 模型构件编码
		/// </summary>
		[Field("MxGjBm")]
		public string MxGjBm
		{
			get{ return _MxGjBm; }
			set
			{
				this.OnPropertyValueChange("MxGjBm");
				this._MxGjBm = value;

				//父级编码
				string pcode = this.MxGjBm;
				this._ComponentCode= StringEx.GetCodeSub(pcode);
			}
		}
		/// <summary>
		/// 模型构建id
		/// </summary>
		[Field("MxGjId")]
		public string MxGjId
		{
			get{ return _MxGjId; }
			set
			{
				this.OnPropertyValueChange("MxGjId");
				this._MxGjId = value;
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
		/// 大样图
		/// </summary>
		[Field("LargePattern")]
		public string LargePattern
		{
			get{ return _LargePattern; }
			set
			{
				this.OnPropertyValueChange("LargePattern");
				this._LargePattern = value;
			}
		}
		/// <summary>
		/// 构件状态
		/// </summary>
		[Field("ComponentStrat")]
		public string ComponentStrat
		{
			get{ return _ComponentStrat; }
			set
			{
				this.OnPropertyValueChange("ComponentStrat");
				this._ComponentStrat = value;
			}
		}
		/// <summary>
		/// 是否打包
		/// </summary>
		[Field("IsPack")]
		public string IsPack
		{
			get{ return _IsPack; }
			set
			{
				this.OnPropertyValueChange("IsPack");
				this._IsPack = value;
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
		/// <summary>
		/// 是否签收
		/// </summary>
		[Field("IsSignFor")]
		public string IsSignFor
		{
			get{ return _IsSignFor; }
			set
			{
				this.OnPropertyValueChange("IsSignFor");
				this._IsSignFor = value;
			}
		}
		/// <summary>
		/// 签收时间
		/// </summary>
		[Field("SignForDate")]
		public DateTime? SignForDate
		{
			get{ return _SignForDate; }
			set
			{
				this.OnPropertyValueChange("SignForDate");
				this._SignForDate = value;
			}
		}
		/// <summary>
		/// 签收用户
		/// </summary>
		[Field("SignForUserCode")]
		public string SignForUserCode
		{
			get{ return _SignForUserCode; }
			set
			{
				this.OnPropertyValueChange("SignForUserCode");
				this._SignForUserCode = value;
			}
		}
		/// <summary>
		/// 是否安装
		/// </summary>
		[Field("IsInstall")]
		public string IsInstall
		{
			get{ return _IsInstall; }
			set
			{
				this.OnPropertyValueChange("IsInstall");
				this._IsInstall = value;
			}
		}
		/// <summary>
		/// 安装时间
		/// </summary>
		[Field("InstallDate")]
		public DateTime? InstallDate
		{
			get{ return _InstallDate; }
			set
			{
				this.OnPropertyValueChange("InstallDate");
				this._InstallDate = value;
			}
		}
		/// <summary>
		/// 安装用户
		/// </summary>
		[Field("InstallUserCode")]
		public string InstallUserCode
		{
			get{ return _InstallUserCode; }
			set
			{
				this.OnPropertyValueChange("InstallUserCode");
				this._InstallUserCode = value;
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
		/// 撤销状态
		/// </summary>
		[Field("RevokeStart")]
		public string RevokeStart
		{
			get{ return _RevokeStart; }
			set
			{
				this.OnPropertyValueChange("RevokeStart");
				this._RevokeStart = value;
			}
		}
		/// <summary>
		/// 站点
		/// </summary>
		[Field("SiteCode")]
		public string SiteCode
		{
			get { return _SiteCode; }
			set
			{
				this.OnPropertyValueChange("SiteCode");
				this._SiteCode = value;
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
				_.StationCode,
				_.ComponentMajor,
				_.BigSystem,
				_.SonSystem,
				_.SystemType,
				_.MaterialType,
				_.ComponentCode,
				_.ComponentName,
				_.MaterialQuality,
				_.SpecificationModel,
				_.Length,
				_.Area,
				_.Material,
				_.MxGjBm,
				_.MxGjId,
				_.Remark,
				_.LargePattern,
				_.ComponentStrat,
				_.IsPack,
				_.PackDate,
				_.PackUserCode,
				_.IsSignFor,
				_.SignForDate,
				_.SignForUserCode,
				_.IsInstall,
				_.InstallDate,
				_.InstallUserCode,
				_.PackCode,
				_.RevokeStart,
				_.SiteCode,
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
				this._StationCode,
				this._ComponentMajor,
				this._BigSystem,
				this._SonSystem,
				this._SystemType,
				this._MaterialType,
				this._ComponentCode,
				this._ComponentName,
				this._MaterialQuality,
				this._SpecificationModel,
				this._Length,
				this._Area,
				this._Material,
				this._MxGjBm,
				this._MxGjId,
				this._Remark,
				this._LargePattern,
				this._ComponentStrat,
				this._IsPack,
				this._PackDate,
				this._PackUserCode,
				this._IsSignFor,
				this._SignForDate,
				this._SignForUserCode,
				this._IsInstall,
				this._InstallDate,
				this._InstallUserCode,
				this._PackCode,
				this._RevokeStart,
				this._SiteCode,
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
			public readonly static Field All = new Field("*", "TbWorkOrderDetail");
            /// <summary>
			/// ID
			/// </summary>
			public readonly static Field ID = new Field("ID", "TbWorkOrderDetail", "ID");
            /// <summary>
			/// 订单编号
			/// </summary>
			public readonly static Field OrderCode = new Field("OrderCode", "TbWorkOrderDetail", "订单编号");
            /// <summary>
			/// 车站编号
			/// </summary>
			public readonly static Field StationCode = new Field("StationCode", "TbWorkOrderDetail", "车站编号");
            /// <summary>
			/// 构件专业
			/// </summary>
			public readonly static Field ComponentMajor = new Field("ComponentMajor", "TbWorkOrderDetail", "构件专业");
            /// <summary>
			/// 大系统
			/// </summary>
			public readonly static Field BigSystem = new Field("BigSystem", "TbWorkOrderDetail", "大系统");
            /// <summary>
			/// 子系统
			/// </summary>
			public readonly static Field SonSystem = new Field("SonSystem", "TbWorkOrderDetail", "子系统");
            /// <summary>
			/// 系统类型
			/// </summary>
			public readonly static Field SystemType = new Field("SystemType", "TbWorkOrderDetail", "系统类型");
            /// <summary>
			/// 材料类型
			/// </summary>
			public readonly static Field MaterialType = new Field("MaterialType", "TbWorkOrderDetail", "材料类型");
            /// <summary>
			/// 构件编号
			/// </summary>
			public readonly static Field ComponentCode = new Field("ComponentCode", "TbWorkOrderDetail", "构件编号");
            /// <summary>
			/// 构建名称
			/// </summary>
			public readonly static Field ComponentName = new Field("ComponentName", "TbWorkOrderDetail", "构建名称");
            /// <summary>
			/// 材质
			/// </summary>
			public readonly static Field MaterialQuality = new Field("MaterialQuality", "TbWorkOrderDetail", "材质");
            /// <summary>
			/// 规格型号
			/// </summary>
			public readonly static Field SpecificationModel = new Field("SpecificationModel", "TbWorkOrderDetail", "规格型号");
            /// <summary>
			/// 长度
			/// </summary>
			public readonly static Field Length = new Field("Length", "TbWorkOrderDetail", "长度");
            /// <summary>
			/// 面积
			/// </summary>
			public readonly static Field Area = new Field("Area", "TbWorkOrderDetail", "面积");
            /// <summary>
			/// 材料名称
			/// </summary>
			public readonly static Field Material = new Field("Material", "TbWorkOrderDetail", "材料名称");
            /// <summary>
			/// 模型构件编码
			/// </summary>
			public readonly static Field MxGjBm = new Field("MxGjBm", "TbWorkOrderDetail", "模型构件编码");
            /// <summary>
			/// 模型构建id
			/// </summary>
			public readonly static Field MxGjId = new Field("MxGjId", "TbWorkOrderDetail", "模型构建id");
            /// <summary>
			/// 备注
			/// </summary>
			public readonly static Field Remark = new Field("Remark", "TbWorkOrderDetail", "备注");
            /// <summary>
			/// 大样图
			/// </summary>
			public readonly static Field LargePattern = new Field("LargePattern", "TbWorkOrderDetail", "大样图");
            /// <summary>
			/// 构件状态
			/// </summary>
			public readonly static Field ComponentStrat = new Field("ComponentStrat", "TbWorkOrderDetail", "构件状态");
            /// <summary>
			/// 是否打包
			/// </summary>
			public readonly static Field IsPack = new Field("IsPack", "TbWorkOrderDetail", "是否打包");
            /// <summary>
			/// 打包日期
			/// </summary>
			public readonly static Field PackDate = new Field("PackDate", "TbWorkOrderDetail", "打包日期");
            /// <summary>
			/// 打包用户
			/// </summary>
			public readonly static Field PackUserCode = new Field("PackUserCode", "TbWorkOrderDetail", "打包用户");
            /// <summary>
			/// 是否签收
			/// </summary>
			public readonly static Field IsSignFor = new Field("IsSignFor", "TbWorkOrderDetail", "是否签收");
            /// <summary>
			/// 签收时间
			/// </summary>
			public readonly static Field SignForDate = new Field("SignForDate", "TbWorkOrderDetail", "签收时间");
            /// <summary>
			/// 签收用户
			/// </summary>
			public readonly static Field SignForUserCode = new Field("SignForUserCode", "TbWorkOrderDetail", "签收用户");
            /// <summary>
			/// 是否安装
			/// </summary>
			public readonly static Field IsInstall = new Field("IsInstall", "TbWorkOrderDetail", "是否安装");
            /// <summary>
			/// 安装时间
			/// </summary>
			public readonly static Field InstallDate = new Field("InstallDate", "TbWorkOrderDetail", "安装时间");
            /// <summary>
			/// 安装用户
			/// </summary>
			public readonly static Field InstallUserCode = new Field("InstallUserCode", "TbWorkOrderDetail", "安装用户");
            /// <summary>
			/// 包件号
			/// </summary>
			public readonly static Field PackCode = new Field("PackCode", "TbWorkOrderDetail", "包件号");
            /// <summary>
			/// 撤销状态
			/// </summary>
			public readonly static Field RevokeStart = new Field("RevokeStart", "TbWorkOrderDetail", "撤销状态");
			/// <summary>
			/// 站点编号
			/// </summary>
			public readonly static Field SiteCode = new Field("SiteCode", "TbWorkOrderDetail", "站点编号");
		}
        #endregion
	}
}