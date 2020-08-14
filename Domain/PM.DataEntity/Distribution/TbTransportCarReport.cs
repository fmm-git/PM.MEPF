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
    /// 实体类TbTransportCarReport。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("TbTransportCarReport")]
    [Serializable]
    public partial class TbTransportCarReport : Entity
    {
        #region Model
		private int _ID;
		private string _OrderCode;
		private string _DistributionCode;
		private int? _FlowState;
		private string _SiteCode;
		private string _TypeCode;
		private string _TypeName;
		private string _PlanDistributionTime;
		private DateTime? _LoadCompleteTime;
		private string _VehicleCode;
		private DateTime? _LeaveFactoryTime;
		private DateTime? _EnterSpaceTime;
		private int? _WaitTime;
		private DateTime? _StartDischargeTime;
		private DateTime? _EndDischargeTime;
		private DateTime? _OutSpaceTime;
		private string _IsProblem;
		private int? _State;
		private string _ProjectId;
		private int? _DisEntOrderId;

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
		/// 配送装车单号
		/// </summary>
		[Field("DistributionCode")]
		public string DistributionCode
		{
			get{ return _DistributionCode; }
			set
			{
				this.OnPropertyValueChange("DistributionCode");
				this._DistributionCode = value;
			}
		}
		/// <summary>
		/// 卸货过程状态 1正常 2等待超过30分钟 3卸货存在问题
		/// </summary>
		[Field("FlowState")]
		public int? FlowState
		{
			get{ return _FlowState; }
			set
			{
				this.OnPropertyValueChange("FlowState");
				this._FlowState = value;
			}
		}
		/// <summary>
		/// 卸货站点
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
		/// 类型编码
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
		/// 计划配送时间
		/// </summary>
		[Field("PlanDistributionTime")]
		public string PlanDistributionTime
		{
			get{ return _PlanDistributionTime; }
			set
			{
				this.OnPropertyValueChange("PlanDistributionTime");
				this._PlanDistributionTime = value;
			}
		}
		/// <summary>
		/// 实际配送时间
		/// </summary>
		[Field("LoadCompleteTime")]
		public DateTime? LoadCompleteTime
		{
			get{ return _LoadCompleteTime; }
			set
			{
				this.OnPropertyValueChange("LoadCompleteTime");
				this._LoadCompleteTime = value;
			}
		}
		/// <summary>
		/// 车牌号
		/// </summary>
		[Field("VehicleCode")]
		public string VehicleCode
		{
			get{ return _VehicleCode; }
			set
			{
				this.OnPropertyValueChange("VehicleCode");
				this._VehicleCode = value;
			}
		}
		/// <summary>
		/// 出厂时间
		/// </summary>
		[Field("LeaveFactoryTime")]
		public DateTime? LeaveFactoryTime
		{
			get{ return _LeaveFactoryTime; }
			set
			{
				this.OnPropertyValueChange("LeaveFactoryTime");
				this._LeaveFactoryTime = value;
			}
		}
		/// <summary>
		/// 运输到场时间
		/// </summary>
		[Field("EnterSpaceTime")]
		public DateTime? EnterSpaceTime
		{
			get{ return _EnterSpaceTime; }
			set
			{
				this.OnPropertyValueChange("EnterSpaceTime");
				this._EnterSpaceTime = value;
			}
		}
		/// <summary>
		/// 等待卸货时间
		/// </summary>
		[Field("WaitTime")]
		public int? WaitTime
		{
			get{ return _WaitTime; }
			set
			{
				this.OnPropertyValueChange("WaitTime");
				this._WaitTime = value;
			}
		}
		/// <summary>
		/// 开始卸货时间
		/// </summary>
		[Field("StartDischargeTime")]
		public DateTime? StartDischargeTime
		{
			get{ return _StartDischargeTime; }
			set
			{
				this.OnPropertyValueChange("StartDischargeTime");
				this._StartDischargeTime = value;
			}
		}
		/// <summary>
		/// 卸货完成时间
		/// </summary>
		[Field("EndDischargeTime")]
		public DateTime? EndDischargeTime
		{
			get{ return _EndDischargeTime; }
			set
			{
				this.OnPropertyValueChange("EndDischargeTime");
				this._EndDischargeTime = value;
			}
		}
		/// <summary>
		/// 车辆出场时间
		/// </summary>
		[Field("OutSpaceTime")]
		public DateTime? OutSpaceTime
		{
			get{ return _OutSpaceTime; }
			set
			{
				this.OnPropertyValueChange("OutSpaceTime");
				this._OutSpaceTime = value;
			}
		}
		/// <summary>
		/// 卸货过程是否存在问题
		/// </summary>
		[Field("IsProblem")]
		public string IsProblem
		{
			get{ return _IsProblem; }
			set
			{
				this.OnPropertyValueChange("IsProblem");
				this._IsProblem = value;
			}
		}
		/// <summary>
		/// 状态0运输中 1运输完成
		/// </summary>
		[Field("State")]
		public int? State
		{
			get{ return _State; }
			set
			{
				this.OnPropertyValueChange("State");
				this._State = value;
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
		/// 配送装车订单信息Id
		/// </summary>
		[Field("DisEntOrderId")]
		public int? DisEntOrderId
		{
			get{ return _DisEntOrderId; }
			set
			{
				this.OnPropertyValueChange("DisEntOrderId");
				this._DisEntOrderId = value;
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
				_.DistributionCode,
				_.FlowState,
				_.SiteCode,
				_.TypeCode,
				_.TypeName,
				_.PlanDistributionTime,
				_.LoadCompleteTime,
				_.VehicleCode,
				_.LeaveFactoryTime,
				_.EnterSpaceTime,
				_.WaitTime,
				_.StartDischargeTime,
				_.EndDischargeTime,
				_.OutSpaceTime,
				_.IsProblem,
				_.State,
				_.ProjectId,
				_.DisEntOrderId,
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
				this._DistributionCode,
				this._FlowState,
				this._SiteCode,
				this._TypeCode,
				this._TypeName,
				this._PlanDistributionTime,
				this._LoadCompleteTime,
				this._VehicleCode,
				this._LeaveFactoryTime,
				this._EnterSpaceTime,
				this._WaitTime,
				this._StartDischargeTime,
				this._EndDischargeTime,
				this._OutSpaceTime,
				this._IsProblem,
				this._State,
				this._ProjectId,
				this._DisEntOrderId,
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
			public readonly static Field All = new Field("*", "TbTransportCarReport");
            /// <summary>
			/// ID
			/// </summary>
			public readonly static Field ID = new Field("ID", "TbTransportCarReport", "ID");
            /// <summary>
			/// 订单编号
			/// </summary>
			public readonly static Field OrderCode = new Field("OrderCode", "TbTransportCarReport", "订单编号");
            /// <summary>
			/// 配送装车单号
			/// </summary>
			public readonly static Field DistributionCode = new Field("DistributionCode", "TbTransportCarReport", "配送装车单号");
            /// <summary>
			/// 卸货过程状态 1正常 2等待超过30分钟 3卸货存在问题
			/// </summary>
			public readonly static Field FlowState = new Field("FlowState", "TbTransportCarReport", "卸货过程状态 1正常 2等待超过30分钟 3卸货存在问题");
            /// <summary>
			/// 卸货站点
			/// </summary>
			public readonly static Field SiteCode = new Field("SiteCode", "TbTransportCarReport", "卸货站点");
            /// <summary>
			/// 类型编码
			/// </summary>
			public readonly static Field TypeCode = new Field("TypeCode", "TbTransportCarReport", "类型编码");
            /// <summary>
			/// 类型名称
			/// </summary>
			public readonly static Field TypeName = new Field("TypeName", "TbTransportCarReport", "类型名称");
            /// <summary>
			/// 计划配送时间
			/// </summary>
			public readonly static Field PlanDistributionTime = new Field("PlanDistributionTime", "TbTransportCarReport", "计划配送时间");
            /// <summary>
			/// 实际配送时间
			/// </summary>
			public readonly static Field LoadCompleteTime = new Field("LoadCompleteTime", "TbTransportCarReport", "实际配送时间");
            /// <summary>
			/// 车牌号
			/// </summary>
			public readonly static Field VehicleCode = new Field("VehicleCode", "TbTransportCarReport", "车牌号");
            /// <summary>
			/// 出厂时间
			/// </summary>
			public readonly static Field LeaveFactoryTime = new Field("LeaveFactoryTime", "TbTransportCarReport", "出厂时间");
            /// <summary>
			/// 运输到场时间
			/// </summary>
			public readonly static Field EnterSpaceTime = new Field("EnterSpaceTime", "TbTransportCarReport", "运输到场时间");
            /// <summary>
			/// 等待卸货时间
			/// </summary>
			public readonly static Field WaitTime = new Field("WaitTime", "TbTransportCarReport", "等待卸货时间");
            /// <summary>
			/// 开始卸货时间
			/// </summary>
			public readonly static Field StartDischargeTime = new Field("StartDischargeTime", "TbTransportCarReport", "开始卸货时间");
            /// <summary>
			/// 卸货完成时间
			/// </summary>
			public readonly static Field EndDischargeTime = new Field("EndDischargeTime", "TbTransportCarReport", "卸货完成时间");
            /// <summary>
			/// 车辆出场时间
			/// </summary>
			public readonly static Field OutSpaceTime = new Field("OutSpaceTime", "TbTransportCarReport", "车辆出场时间");
            /// <summary>
			/// 卸货过程是否存在问题
			/// </summary>
			public readonly static Field IsProblem = new Field("IsProblem", "TbTransportCarReport", "卸货过程是否存在问题");
            /// <summary>
			/// 状态0运输中 1运输完成
			/// </summary>
			public readonly static Field State = new Field("State", "TbTransportCarReport", "状态0运输中 1运输完成");
            /// <summary>
			/// 项目id
			/// </summary>
			public readonly static Field ProjectId = new Field("ProjectId", "TbTransportCarReport", "项目id");
            /// <summary>
			/// 配送装车订单信息Id
			/// </summary>
			public readonly static Field DisEntOrderId = new Field("DisEntOrderId", "TbTransportCarReport", "配送装车订单信息Id");
        }
        #endregion
	}
}