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
    /// 实体类TbFlowEarlyWarningOtherInfo。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("TbFlowEarlyWarningOtherInfo")]
    [Serializable]
    public partial class TbFlowEarlyWarningOtherInfo : Entity
    {
        #region Model
		private int _ID;
		private string _EarlyWarningCode;
		private string _FlowPerformID;
		private string _FlowCode;
		private string _FlowNodeCode;
		private string _EWFormCode;
		private int? _EWFormDataCode;
		private string _EarlyWarningContent;
		private DateTime? _EarlyWarningTime;
		private string _SiteCode;
		private string _WorkAreaCode;
		private string _BranchCode;
		private string _ProcessFactoryCode;
		private string _ProjectId;
		private string _FlowSpUserCode;
		private string _FlowYjUserCode;
		private string _TableName;
		private string _DataCode;
		private int? _FlowPerformOID;
		private int? _EarlyWarningStart;

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
		/// 预警条件编号
		/// </summary>
		[Field("EarlyWarningCode")]
		public string EarlyWarningCode
		{
			get{ return _EarlyWarningCode; }
			set
			{
				this.OnPropertyValueChange("EarlyWarningCode");
				this._EarlyWarningCode = value;
			}
		}
		/// <summary>
		/// 流程审批编号
		/// </summary>
		[Field("FlowPerformID")]
		public string FlowPerformID
		{
			get{ return _FlowPerformID; }
			set
			{
				this.OnPropertyValueChange("FlowPerformID");
				this._FlowPerformID = value;
			}
		}
		/// <summary>
		/// 流程编号
		/// </summary>
		[Field("FlowCode")]
		public string FlowCode
		{
			get{ return _FlowCode; }
			set
			{
				this.OnPropertyValueChange("FlowCode");
				this._FlowCode = value;
			}
		}
		/// <summary>
		/// 流程节点编号
		/// </summary>
		[Field("FlowNodeCode")]
		public string FlowNodeCode
		{
			get{ return _FlowNodeCode; }
			set
			{
				this.OnPropertyValueChange("FlowNodeCode");
				this._FlowNodeCode = value;
			}
		}
		/// <summary>
		/// 预警表单
		/// </summary>
		[Field("EWFormCode")]
		public string EWFormCode
		{
			get{ return _EWFormCode; }
			set
			{
				this.OnPropertyValueChange("EWFormCode");
				this._EWFormCode = value;
			}
		}
		/// <summary>
		/// 预警数据ID
		/// </summary>
		[Field("EWFormDataCode")]
		public int? EWFormDataCode
		{
			get{ return _EWFormDataCode; }
			set
			{
				this.OnPropertyValueChange("EWFormDataCode");
				this._EWFormDataCode = value;
			}
		}
		/// <summary>
		/// 预警内容
		/// </summary>
		[Field("EarlyWarningContent")]
		public string EarlyWarningContent
		{
			get{ return _EarlyWarningContent; }
			set
			{
				this.OnPropertyValueChange("EarlyWarningContent");
				this._EarlyWarningContent = value;
			}
		}
		/// <summary>
		/// 预警时间
		/// </summary>
		[Field("EarlyWarningTime")]
		public DateTime? EarlyWarningTime
		{
			get{ return _EarlyWarningTime; }
			set
			{
				this.OnPropertyValueChange("EarlyWarningTime");
				this._EarlyWarningTime = value;
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
		/// 工区编号
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
		/// 分部编号
		/// </summary>
		[Field("BranchCode")]
		public string BranchCode
		{
			get{ return _BranchCode; }
			set
			{
				this.OnPropertyValueChange("BranchCode");
				this._BranchCode = value;
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
		/// 流程审批人编号
		/// </summary>
		[Field("FlowSpUserCode")]
		public string FlowSpUserCode
		{
			get{ return _FlowSpUserCode; }
			set
			{
				this.OnPropertyValueChange("FlowSpUserCode");
				this._FlowSpUserCode = value;
			}
		}
		/// <summary>
		/// 流程预警人编号
		/// </summary>
		[Field("FlowYjUserCode")]
		public string FlowYjUserCode
		{
			get{ return _FlowYjUserCode; }
			set
			{
				this.OnPropertyValueChange("FlowYjUserCode");
				this._FlowYjUserCode = value;
			}
		}
		/// <summary>
		/// 预警表名称
		/// </summary>
		[Field("TableName")]
		public string TableName
		{
			get{ return _TableName; }
			set
			{
				this.OnPropertyValueChange("TableName");
				this._TableName = value;
			}
		}
		/// <summary>
		/// 预警数据编号
		/// </summary>
		[Field("DataCode")]
		public string DataCode
		{
			get{ return _DataCode; }
			set
			{
				this.OnPropertyValueChange("DataCode");
				this._DataCode = value;
			}
		}
		/// <summary>
		/// 流程审批消息id
		/// </summary>
		[Field("FlowPerformOID")]
		public int? FlowPerformOID
		{
			get{ return _FlowPerformOID; }
			set
			{
				this.OnPropertyValueChange("FlowPerformOID");
				this._FlowPerformOID = value;
			}
		}
		/// <summary>
		/// 预警状态
		/// </summary>
		[Field("EarlyWarningStart")]
		public int? EarlyWarningStart
		{
			get{ return _EarlyWarningStart; }
			set
			{
				this.OnPropertyValueChange("EarlyWarningStart");
				this._EarlyWarningStart = value;
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
				_.EarlyWarningCode,
				_.FlowPerformID,
				_.FlowCode,
				_.FlowNodeCode,
				_.EWFormCode,
				_.EWFormDataCode,
				_.EarlyWarningContent,
				_.EarlyWarningTime,
				_.SiteCode,
				_.WorkAreaCode,
				_.BranchCode,
				_.ProcessFactoryCode,
				_.ProjectId,
				_.FlowSpUserCode,
				_.FlowYjUserCode,
				_.TableName,
				_.DataCode,
				_.FlowPerformOID,
				_.EarlyWarningStart,
			};
        }
        /// <summary>
        /// 获取值信息
        /// </summary>
        public override object[] GetValues()
        {
            return new object[] {
				this._ID,
				this._EarlyWarningCode,
				this._FlowPerformID,
				this._FlowCode,
				this._FlowNodeCode,
				this._EWFormCode,
				this._EWFormDataCode,
				this._EarlyWarningContent,
				this._EarlyWarningTime,
				this._SiteCode,
				this._WorkAreaCode,
				this._BranchCode,
				this._ProcessFactoryCode,
				this._ProjectId,
				this._FlowSpUserCode,
				this._FlowYjUserCode,
				this._TableName,
				this._DataCode,
				this._FlowPerformOID,
				this._EarlyWarningStart,
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
			public readonly static Field All = new Field("*", "TbFlowEarlyWarningOtherInfo");
            /// <summary>
			/// ID
			/// </summary>
			public readonly static Field ID = new Field("ID", "TbFlowEarlyWarningOtherInfo", "ID");
            /// <summary>
			/// 预警条件编号
			/// </summary>
			public readonly static Field EarlyWarningCode = new Field("EarlyWarningCode", "TbFlowEarlyWarningOtherInfo", "预警条件编号");
            /// <summary>
			/// 流程审批编号
			/// </summary>
			public readonly static Field FlowPerformID = new Field("FlowPerformID", "TbFlowEarlyWarningOtherInfo", "流程审批编号");
            /// <summary>
			/// 流程编号
			/// </summary>
			public readonly static Field FlowCode = new Field("FlowCode", "TbFlowEarlyWarningOtherInfo", "流程编号");
            /// <summary>
			/// 流程节点编号
			/// </summary>
			public readonly static Field FlowNodeCode = new Field("FlowNodeCode", "TbFlowEarlyWarningOtherInfo", "流程节点编号");
            /// <summary>
			/// 预警表单
			/// </summary>
			public readonly static Field EWFormCode = new Field("EWFormCode", "TbFlowEarlyWarningOtherInfo", "预警表单");
            /// <summary>
			/// 预警数据ID
			/// </summary>
			public readonly static Field EWFormDataCode = new Field("EWFormDataCode", "TbFlowEarlyWarningOtherInfo", "预警数据ID");
            /// <summary>
			/// 预警内容
			/// </summary>
			public readonly static Field EarlyWarningContent = new Field("EarlyWarningContent", "TbFlowEarlyWarningOtherInfo", "预警内容");
            /// <summary>
			/// 预警时间
			/// </summary>
			public readonly static Field EarlyWarningTime = new Field("EarlyWarningTime", "TbFlowEarlyWarningOtherInfo", "预警时间");
            /// <summary>
			/// 站点编号
			/// </summary>
			public readonly static Field SiteCode = new Field("SiteCode", "TbFlowEarlyWarningOtherInfo", "站点编号");
            /// <summary>
			/// 工区编号
			/// </summary>
			public readonly static Field WorkAreaCode = new Field("WorkAreaCode", "TbFlowEarlyWarningOtherInfo", "工区编号");
            /// <summary>
			/// 分部编号
			/// </summary>
			public readonly static Field BranchCode = new Field("BranchCode", "TbFlowEarlyWarningOtherInfo", "分部编号");
            /// <summary>
			/// 加工厂编号
			/// </summary>
			public readonly static Field ProcessFactoryCode = new Field("ProcessFactoryCode", "TbFlowEarlyWarningOtherInfo", "加工厂编号");
            /// <summary>
			/// 项目id
			/// </summary>
			public readonly static Field ProjectId = new Field("ProjectId", "TbFlowEarlyWarningOtherInfo", "项目id");
            /// <summary>
			/// 流程审批人编号
			/// </summary>
			public readonly static Field FlowSpUserCode = new Field("FlowSpUserCode", "TbFlowEarlyWarningOtherInfo", "流程审批人编号");
            /// <summary>
			/// 流程预警人编号
			/// </summary>
			public readonly static Field FlowYjUserCode = new Field("FlowYjUserCode", "TbFlowEarlyWarningOtherInfo", "流程预警人编号");
            /// <summary>
			/// 预警表名称
			/// </summary>
			public readonly static Field TableName = new Field("TableName", "TbFlowEarlyWarningOtherInfo", "预警表名称");
            /// <summary>
			/// 预警数据编号
			/// </summary>
			public readonly static Field DataCode = new Field("DataCode", "TbFlowEarlyWarningOtherInfo", "预警数据编号");
            /// <summary>
			/// 流程审批消息id
			/// </summary>
			public readonly static Field FlowPerformOID = new Field("FlowPerformOID", "TbFlowEarlyWarningOtherInfo", "流程审批消息id");
            /// <summary>
			/// 预警状态
			/// </summary>
			public readonly static Field EarlyWarningStart = new Field("EarlyWarningStart", "TbFlowEarlyWarningOtherInfo", "预警状态");
        }
        #endregion
	}
}