using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PM.Web.Models.ExcelModel
{
    public class ModelReportExcel
    {
        /// <summary>
        /// 站点名称
        /// </summary>
        [DataMember(Name = "站点")]
        public string SiteName { get; set; }
        /// <summary>
        ///  工程计划时间
        /// </summary>
        [DataMember(Name = "工程计划时间", Order = 30)]
        public string PlanTime_ActualTime { get; set; }

        public string PlanTime { get { return "开工:" + PlanTime; } set { } }
        public string ActualTime { get { return " 完工:" + ActualTime; } set { } }
        /// <summary>
        /// 计划总数
        /// </summary>
        [DataMember(Name = "计划总数")]
        public string PlanTotalShow { get; set; }
        /// <summary>
        /// 加工中
        /// </summary>
        [DataMember(Name = "加工中")]
        public string ProcessingTotalShow { get; set; }
        /// <summary>
        /// 加工完成
        /// </summary>
        [DataMember(Name = "加工完成")]
        public string MachinTotalShow { get; set; }
        /// <summary>
        /// 安装完成
        /// </summary>
        [DataMember(Name = "安装完成")]
        public string InstallTotalShow { get; set; }
        /// <summary>
        /// 进度超前
        /// </summary>
        [DataMember(Name = "进度超前")]
        public string leaShow { get; set; }
        /// <summary>
        /// 进度滞后
        /// </summary>
        [DataMember(Name = "进度滞后")]
        public string lagShow { get; set; }
        /// <summary>
        /// 滞后未完成
        /// </summary>
        [DataMember(Name = "滞后未完成")]
        public string NolagShow { get; set; }
    }
}