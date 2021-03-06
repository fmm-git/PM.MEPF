﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PM.Common.Extension;

namespace PM.DataEntity.BIM
{
    /// <summary>
    /// 模型项目结构树
    /// </summary>
    public class model_tree
    {
        public string id { get; set; }
        public string pid { get; set; }
        public string name { get; set; }
        public string FileName { get; set; }
        public int Total { get; set; }
        public int Type { get; set; }
    }
    public class modelData_tree
    {
        /// <summary>
        /// 专业
        /// </summary>
        public string Major { get; set; }
        /// <summary>
        /// 系统
        /// </summary>
        public string System { get; set; }
        /// <summary>
        /// 子系统
        /// </summary>
        public string Subsystem { get; set; }
        /// <summary>
        /// 材料类型
        /// </summary>
        public string MaterialType { get; set; }
        /// <summary>
        /// 材料名称
        /// </summary>
        public string Material { get; set; }
        public string ComponentCode { get; set; }
        public string FileName { get; set; }
        public int Total { get; set; }
    }
    /// <summary>
    /// 项目总清单列表
    /// </summary>
    public class ProjectListAllModel : ProjectListInfoModel
    {
        public string SiteCode { get; set; }
        /// <summary>
        /// 专业
        /// </summary>
        public string Major { get; set; }
        /// <summary>
        /// 系统
        /// </summary>
        public string System { get; set; }
        /// <summary>
        /// 子系统
        /// </summary>
        public string Subsystem { get; set; }
        /// <summary>
        /// 材料类型
        /// </summary>
        public string MaterialType { get; set; }
        /// <summary>
        /// 材料名称
        /// </summary>
        public string Material { get; set; }
        public string SystemType { get; set; }
        public string ComponentCodeShow
        {
            get
            {
                return StringEx.GetCodeSub(this.ComponentCode);
            }
        }
        public string ComponentCodeP { get; set; }
        /// <summary>
        /// 规格尺寸
        /// </summary>
        public string Size { get; set; }
        public string id { get; set; }
        public string Name { get; set; }
        public string Length { get; set; }
        public string Area { get; set; }
        public string PidTag
        {
            get
            {
                return this.ComponentCodeShow + "---" + this.Size;
            }

        }
    }
    public class ProjectListInfoModel
    {
        /// <summary>
        /// 计划总数
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// 构件编码
        /// </summary>
        public string ComponentCode { get; set; }
        /// <summary>
        /// 加工中
        /// </summary>
        public int Processing { get; set; }

        public string ProcessingShow
        {
            get
            {
                return GetPoint(this.Processing);
            }
        }
        /// <summary>
        /// 加工完成
        /// </summary>
        public int ProcessComplete { get; set; }
        public string ProcessCompleteShow
        {
            get
            {
                return GetPoint(this.ProcessComplete);
            }
        }
        /// <summary>
        /// 安装完成
        /// </summary>
        public int InstallComplete { get; set; }
        public string InstallCompleteShow
        {
            get
            {
                return GetPoint(this.InstallComplete);
            }
        }
        /// <summary>
        /// 计划完成时间
        /// </summary>
        public DateTime? PlanTime { get; set; }
        /// <summary>
        /// 实际完成时间
        /// </summary>
        public DateTime? ActualTime { get; set; }
        /// <summary>
        /// 进度状态 0待定  1正常 2滞后
        /// </summary>
        public int ProgressStatus { get; set; }
        public string ProgressStatusShow
        {
            get
            {
                this.ProgressStatus = 0;
                string str = "";
                int dayNum = 0;
                if (PlanTime.HasValue)
                {
                    if (ActualTime.HasValue)
                    {
                        dayNum = GetDay(PlanTime.Value, ActualTime.Value);
                    }
                    else
                    {
                        dayNum = GetDay(PlanTime.Value, DateTime.Now.Date);
                    }
                }
                if (this.ProgressStatus == 1)
                {
                    str = "正常";
                }
                else if (this.ProgressStatus == 2)
                {
                    str = "滞后(" + dayNum + "天)";
                }
                return str;
            }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        public bool IsAny { get; set; }
        /// <summary>
        /// 获取时间间隔天数
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private int GetDay(DateTime start, DateTime end)
        {
            int dayNum = 0;
            if (start < end)
            {
                TimeSpan sp = start.Subtract(end);
                dayNum = Math.Abs(sp.Days);
                this.ProgressStatus = 2;
            }
            else
            {
                this.ProgressStatus = 1;
            }
            return dayNum;
        }
        private string GetPoint(int data)
        {
            int total = this.Processing + this.ProcessComplete + this.InstallComplete;
            if (total == 0) return "";
            string str = data + "({0}%)";
            string point = "0";
            if (data > 0)
                point = ((data / (total)) * 100).ToString("f2");
            return string.Format(str, point);
        }
    }

    public class ProjectListItemModel : ProjectListInfoModel
    {
        public string id { get; set; }
        public string Name { get; set; }
        public string Length { get; set; }
        public string Area { get; set; }
    }

    public class BIMRequest : PageSearchRequest
    {
        /// <summary>
        /// 构件名称
        /// </summary>
        public string ComponentName { get; set; }
        /// <summary>
        /// 构件编号
        /// </summary>
        public string ComponentCode { get; set; }
        public string ComponentCodeList { get; set; }
        /// <summary>
        /// 材料名称
        /// </summary>
        public string Material { get; set; }
        /// <summary>
        /// 尺寸
        /// </summary>
        public string Size { get; set; }
        public int TotalCount { get; set; }
        public string DBName { get; set; }
        public DateTime? PlanTime { get; set; }
        public string Remark { get; set; }
        public int Type { get; set; }
        public bool IsWrite { get; set; }
        public int ID { get; set; }
    }

    public class ProjectListInsertModel
    {
        public string ID { get; set; }
        public string SiteCode { get; set; }
        public string ProjectId { get; set; }
        /// <summary>
        /// 专业
        /// </summary>
        public string Major { get; set; }
        /// <summary>
        /// 系统
        /// </summary>
        public string System { get; set; }
        /// <summary>
        /// 子系统
        /// </summary>
        public string Subsystem { get; set; }
        /// <summary>
        /// 材料类型
        /// </summary>
        public string MaterialType { get; set; }
        /// <summary>
        /// 材料名称
        /// </summary>
        public string Material { get; set; }
        /// <summary>
        /// 模型构件编码
        /// </summary>
        public string ComponentCode { get; set; }
        /// <summary>
        /// 族与类型
        /// </summary>
        public string ComponentName { get; set; }
        /// <summary>
        /// 尺寸
        /// </summary>
        public string Size { get; set; }
        /// <summary>
        /// 面积
        /// </summary>
        public string Area { get; set; }
        /// <summary>
        /// 材质
        /// </summary>
        public string Texture { get; set; }
        /// <summary>
        /// 车站编号
        /// </summary>
        public string StationName { get; set; }
        /// <summary>
        /// 长度
        /// </summary>
        public string Length { get; set; }
        /// <summary>
        /// 风管长度
        /// </summary>
        public string FGLength { get; set; }
        /// <summary>
        /// 系统类型
        /// </summary>
        public string SystemType { get; set; }
        /// <summary>
        /// 安装位置
        /// </summary>
        public string Position { get; set; }
        /// <summary>
        /// 支架图纸编号
        /// </summary>
        public string DrawingNo { get; set; }
        public int IsOrder { get; set; }
        public string FileName { get; set; }
        public string Other1 { get; set; }
        public string Other2 { get; set; }
        public string Other3 { get; set; }
        public string Other4 { get; set; }
        public string Other5 { get; set; }
    }

    /// <summary>
    /// 1类型数 2名称数 3件数
    /// </summary>
    public class ModelReportList
    {
        public string SiteCode { get; set; }
        public string SiteName { get; set; }
        public string PlanTime { get; set; }
        public string ActualTime { get; set; }
        public int Month
        {
            get
            {
                if (PlanTime.IsEmpty() || ActualTime.IsEmpty()) return 0;
                DateTime dt1 = Convert.ToDateTime(PlanTime);
                DateTime dt2 = Convert.ToDateTime(ActualTime);
                return (dt2.Year - dt1.Year) * 12 + (dt2.Month - dt1.Month);
            }
        }
        public DateTime BginTime { get; set; }
        public string BginTimeShow { get { return BginTime.ToString("yyyy-MM-dd"); } }
        //计划总数
        public int PlanTotal1 { get; set; }
        public int PlanTotal2 { get; set; }
        public int PlanTotal3 { get; set; }
        public string PlanTotalShow { get { return PlanTotal1 + "/" + PlanTotal2 + "/" + PlanTotal3; } }
        //加工中总数
        public int ProcessingTotal1 { get; set; }
        public int ProcessingTotal2 { get; set; }
        public int ProcessingTotal3 { get; set; }
        public string ProcessingTotalShow { get { return ProcessingTotal1 + "/" + ProcessingTotal2 + "/" + ProcessingTotal3; } }
        //加工完成总数
        public int MachinTotal1 { get; set; }
        public int MachinTotal2 { get; set; }
        public int MachinTotal3 { get; set; }
        public string MachinTotalShow { get { return MachinTotal1 + "/" + MachinTotal2 + "/" + MachinTotal3; } }
        //安装完成总数
        public int InstallTotal1 { get; set; }
        public int InstallTotal2 { get; set; }
        public int InstallTotal3 { get; set; }
        public string InstallTotalShow { get { return InstallTotal1 + "/" + InstallTotal2 + "/" + InstallTotal3; } }
        //未完成总数
        public int NoTotal1 { get { return this.PlanTotal1 - InstallTotal1; } }
        public int NoTotal2 { get { return this.PlanTotal2 - InstallTotal2; } }
        public int NoTotal3 { get { return this.PlanTotal2 - InstallTotal2; } }
        public string NoTotalShow { get { return NoTotal1 + "/" + NoTotal2 + "/" + NoTotal3; } }
        //未开始总数
        public int NoStart1 { get { return this.PlanTotal1 - (ProcessingTotal1+ MachinTotal1 + InstallTotal1); } }
        public int NoStart2 { get { return this.PlanTotal2 - (ProcessingTotal2 + MachinTotal2 + InstallTotal2); } }
        public int NoStart3 { get { return this.PlanTotal2 - (ProcessingTotal3 + MachinTotal3); } }
        public string NoStartShow { get { return NoTotal1 + "/" + NoTotal2 + "/" + NoTotal3; } }
        //滞后总数
        public int lag1 { get; set; }
        public int lag2 { get; set; }
        public int lag3 { get; set; }
        public string lagShow { get { return lag1 + "/" + lag2 + "/" + lag3; } }
        //超前完成总数
        public int lea1 { get; set; }
        public int lea2 { get; set; }
        public int lea3 { get; set; }
        public string leaShow { get { return lea1 + "/" + lea2 + "/" + lea3; } }
        //滞后未完成总数
        public int Nolag1 { get; set; }
        public int Nolag2 { get; set; }
        public int Nolag3 { get; set; }
        public string NolagShow { get { return Nolag1 + "/" + Nolag2 + "/" + Nolag3; } }
        //完工百分比
        public string OverPoint
        {
            get
            {
                if (PlanTotal1 == 0) return "0";
                return (InstallTotal1 / PlanTotal1).ToString("f2");
            }
        }
        //滞后百分比
        public string lagPoint
        {
            get
            {
                if (PlanTotal1 == 0) return "0";
                return (lag1 / PlanTotal1).ToString("f2");
            }
        }
    }

    public class ModelLabelData
    {
        public string Code { get; set; }
        public int PlanTotal { get; set; }
        public int lag { get; set; }
        public int MachinTotal { get; set; }
    }

    public class ReportRetData
    {
        public int TotalCount { get; set; }
        public List<ReportData> Item { get; set; }
    }
    public class ReportData
    {
        public string name { get; set; }
        public int y { get; set; }
        public string color { get; set; }
    }
}
