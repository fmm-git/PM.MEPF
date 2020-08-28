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
    /// 模型构件统计信息
    /// </summary>
    [Table("TbModelReporte")]
    [Serializable]
    public partial class TbModelReporte : Entity
    {
        #region Model
		private int _ID;
		private string _SiteCode;
		private string _ProjectId;
		private string _ComponentCode;
		private string _ComponentName;
		private string _ComponentCodeP;
		private string _FileName;
		private int _Type;
		private int _PlanTotal;
		private int _State;
		private DateTime? _PlanTime;
		private DateTime? _ActualTime;
		private int _Difference;

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
		/// 项目id
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
		/// 模型编号
		/// </summary>
		[Field("ComponentCode")]
		public string ComponentCode
		{
			get{ return _ComponentCode; }
			set
			{
				this.OnPropertyValueChange("ComponentCode");
				this._ComponentCode = value;
			}
		}
		/// <summary>
		/// 模型名称
		/// </summary>
		[Field("ComponentName")]
		public string ComponentName
		{
			get { return _ComponentName; }
			set
			{
				this.OnPropertyValueChange("ComponentName");
				this._ComponentName = value;
			}
		}
		/// <summary>
		/// 父及编号
		/// </summary>
		[Field("ComponentCodeP")]
		public string ComponentCodeP
		{
			get{ return _ComponentCodeP; }
			set
			{
				this.OnPropertyValueChange("ComponentCodeP");
				this._ComponentCodeP = value;
			}
		}
		/// <summary>
		/// 文件地址
		/// </summary>
		[Field("FileName")]
		public string FileName
		{
			get{ return _FileName; }
			set
			{
				this.OnPropertyValueChange("FileName");
				this._FileName = value;
			}
		}
		/// <summary>
		/// 类型: 1专业 2大系统 3小系统 4材料类型 5材料名称
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
		/// 计划总数
		/// </summary>
		[Field("PlanTotal")]
		public int PlanTotal
		{
			get{ return _PlanTotal; }
			set
			{
				this.OnPropertyValueChange("PlanTotal");
				this._PlanTotal = value;
			}
		}
		/// <summary>
		/// _1加工中 2加工完成  3安装完成 4签收完成
		/// </summary>
		[Field("State")]
		public int State
		{
			get{ return _State; }
			set
			{
				this.OnPropertyValueChange("State");
				this._State = value;
			}
		}
		/// <summary>
		/// 计划时间
		/// </summary>
		[Field("PlanTime")]
		public DateTime? PlanTime
		{
			get{ return _PlanTime; }
			set
			{
				this.OnPropertyValueChange("PlanTime");
				this._PlanTime = value;
			}
		}
		/// <summary>
		/// 完成时间
		/// </summary>
		[Field("ActualTime")]
		public DateTime? ActualTime
		{
			get{ return _ActualTime; }
			set
			{
				this.OnPropertyValueChange("ActualTime");
				this._ActualTime = value;
			}
		}
		/// <summary>
		/// 差额
		/// </summary>
		[Field("Difference")]
		public int Difference
		{
			get{ return _Difference; }
			set
			{
				this.OnPropertyValueChange("Difference");
				this._Difference = value;
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
				_.ProjectId,
				_.ComponentCode,
				_.ComponentName,
				_.ComponentCodeP,
				_.FileName,
				_.Type,
				_.PlanTotal,
				_.State,
				_.PlanTime,
				_.ActualTime,
				_.Difference,
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
				this._ProjectId,
				this._ComponentCode,
				this._ComponentName,
				this._ComponentCodeP,
				this._FileName,
				this._Type,
				this._PlanTotal,
				this._State,
				this._PlanTime,
				this._ActualTime,
				this._Difference,
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
			public readonly static Field All = new Field("*", "TbModelReporte");
            /// <summary>
			/// ID
			/// </summary>
			public readonly static Field ID = new Field("ID", "TbModelReporte", "ID");
            /// <summary>
			/// 站点编号
			/// </summary>
			public readonly static Field SiteCode = new Field("SiteCode", "TbModelReporte", "站点编号");
            /// <summary>
			/// 项目id
			/// </summary>
			public readonly static Field ProjectId = new Field("ProjectId", "TbModelReporte", "项目id");
            /// <summary>
			/// 模型编号
			/// </summary>
			public readonly static Field ComponentCode = new Field("ComponentCode", "TbModelReporte", "模型编号");
			/// <summary>
			/// 模型名称
			/// </summary>
			public readonly static Field ComponentName = new Field("ComponentName", "TbModelReporte", "模型名称");
			/// <summary>
			/// 父及编号
			/// </summary>
			public readonly static Field ComponentCodeP = new Field("ComponentCodeP", "TbModelReporte", "父及编号");
            /// <summary>
			/// 文件地址
			/// </summary>
			public readonly static Field FileName = new Field("FileName", "TbModelReporte", "文件地址");
            /// <summary>
			/// 类型: 1专业 2大系统 3小系统 4材料类型 5材料名称
			/// </summary>
			public readonly static Field Type = new Field("Type", "TbModelReporte", "类型: 1专业 2大系统 3小系统 4材料类型 5材料名称");
            /// <summary>
			/// 计划总数
			/// </summary>
			public readonly static Field PlanTotal = new Field("PlanTotal", "TbModelReporte", "计划总数");
            /// <summary>
			/// _1加工中 2加工完成  3安装完成 4签收完成
			/// </summary>
			public readonly static Field State = new Field("State", "TbModelReporte", "_1加工中 2加工完成  3安装完成 4签收完成");
            /// <summary>
			/// 计划时间
			/// </summary>
			public readonly static Field PlanTime = new Field("PlanTime", "TbModelReporte", "计划时间");
            /// <summary>
			/// 完成时间
			/// </summary>
			public readonly static Field ActualTime = new Field("ActualTime", "TbModelReporte", "完成时间");
            /// <summary>
			/// 差额
			/// </summary>
			public readonly static Field Difference = new Field("Difference", "TbModelReporte", "差额");
        }
		#endregion

		#region 扩展字段
		/// <summary>
		/// 操作类型 1添加 2修改 0删除
		/// </summary>
		public int OpType { get; set; }
		#endregion
	}
}