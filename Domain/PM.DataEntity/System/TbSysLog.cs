﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dos.ORM;

//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.36392
//     Website: http://ITdos.com/Dos/ORM/Index.html
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace PM.DataEntity
{
    /// <summary>
    /// 实体类TbSysLog。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("TbSysLog")]
    [Serializable]
    public partial class TbSysLog : Entity
    {
        #region Model
        private int _id;
        private DateTime _LogDate;
        private string _ActionMenu;
        private string _UserIP;
        private string _HostName;
        private string _UserCode;
        private string _ActionType;

        public string UserName { get; set; }

        /// <summary>
        /// 日志表标识ID
        /// </summary>
        [Field("id")]
        public int id
        {
            get { return _id; }
            set
            {
                this.OnPropertyValueChange("id");
                this._id = value;
            }
        }
        /// <summary>
        /// 日志时间
        /// </summary>
        [Field("LogDate")]
        public DateTime LogDate
        {
            get { return _LogDate; }
            set
            {
                this.OnPropertyValueChange("LogDate");
                this._LogDate = value;
            }
        }
        /// <summary>
        /// 操作菜单
        /// </summary>
        [Field("ActionMenu")]
        public string ActionMenu
        {
            get { return _ActionMenu; }
            set
            {
                this.OnPropertyValueChange("ActionMenu");
                this._ActionMenu = value;
            }
        }
        public string ActionMenuName { get; set; }
        /// <summary>
        /// 操作用户IP
        /// </summary>
        [Field("UserIP")]
        public string UserIP
        {
            get { return _UserIP; }
            set
            {
                this.OnPropertyValueChange("UserIP");
                this._UserIP = value;
            }
        }
        /// <summary>
        /// 操作主机名
        /// </summary>
        [Field("HostName")]
        public string HostName
        {
            get { return _HostName; }
            set
            {
                this.OnPropertyValueChange("HostName");
                this._HostName = value;
            }
        }
        /// <summary>
        /// 操作用户Code
        /// </summary>
        [Field("UserCode")]
        public string UserCode
        {
            get { return _UserCode; }
            set
            {
                this.OnPropertyValueChange("UserCode");
                this._UserCode = value;
            }
        }
        /// <summary>
        /// 操作类型
        /// </summary>
        [Field("ActionType")]
        public string ActionType
        {
            get { return _ActionType; }
            set
            {
                this.OnPropertyValueChange("ActionType");
                this._ActionType = value;
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
				_.id,
			};
        }
        /// <summary>
        /// 获取实体中的标识列
        /// </summary>
        public override Field GetIdentityField()
        {
            return _.id;
        }
        /// <summary>
        /// 获取列信息
        /// </summary>
        public override Field[] GetFields()
        {
            return new Field[] {
				_.id,
				_.LogDate,
				_.ActionMenu,
				_.UserIP,
				_.HostName,
				_.UserCode,
				_.ActionType,
			};
        }
        /// <summary>
        /// 获取值信息
        /// </summary>
        public override object[] GetValues()
        {
            return new object[] {
				this._id,
				this._LogDate,
				this._ActionMenu,
				this._UserIP,
				this._HostName,
				this._UserCode,
				this._ActionType,
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
            public readonly static Field All = new Field("*", "TbSysLog");
            /// <summary>
            /// 日志表标识ID
            /// </summary>
            public readonly static Field id = new Field("id", "TbSysLog", "日志表标识ID");
            /// <summary>
            /// 日志时间
            /// </summary>
            public readonly static Field LogDate = new Field("LogDate", "TbSysLog", "日志时间");
            /// <summary>
            /// 操作菜单
            /// </summary>
            public readonly static Field ActionMenu = new Field("ActionMenu", "TbSysLog", "操作菜单");
            /// <summary>
            /// 操作用户IP
            /// </summary>
            public readonly static Field UserIP = new Field("UserIP", "TbSysLog", "操作用户IP");
            /// <summary>
            /// 操作主机名
            /// </summary>
            public readonly static Field HostName = new Field("HostName", "TbSysLog", "操作主机名");
            /// <summary>
            /// 操作用户Code
            /// </summary>
            public readonly static Field UserCode = new Field("UserCode", "TbSysLog", "操作用户Code");
            /// <summary>
            /// 操作类型
            /// </summary>
            public readonly static Field ActionType = new Field("ActionType", "TbSysLog", "操作类型");
        }
        #endregion
    }
}
