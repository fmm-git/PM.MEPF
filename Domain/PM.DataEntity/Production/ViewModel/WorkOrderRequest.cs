using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PM.DataEntity.Production.ViewModel
{
    public class WorkOrderRequest : PageSearchRequest
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderCode { get; set; }
        /// <summary>
        /// 专业
        /// </summary>
        public string Major { get; set; }
        /// <summary>
        /// 系统类型
        /// </summary>
        public string SystemType { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public string OrderState { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        public string OrderType { get; set; }
        /// <summary>
        /// 紧急程度
        /// </summary>
        public string UrgentDegree { get; set; }
        /// <summary>
        /// 历史月份
        /// </summary>
        public DateTime? HistoryMonth { get; set; }
        /// <summary>
        /// 签收类型
        /// </summary>
        public string SignForType { get; set; }
        /// <summary>
        /// 安装类型
        /// </summary>
        public string InstallType { get; set; }
        public bool IsOutPut { get; set; }

    }
    public class WorkOrderDetailRequest : PageSearchRequest
    {
        public int ID { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderCode { get; set; }
        /// <summary>
        /// 系统类型
        /// </summary>
        public string SystemType { get; set; }
        /// <summary>
        /// 材料类型
        /// </summary>
        public string MaterialType { get; set; }
        /// <summary>
        /// 构件编号
        /// </summary>
        public string ComponentCode { get; set; }
        /// <summary>
        /// 构件名称
        /// </summary>
        public string ComponentName { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string SpecificationModel { get; set; }
        /// <summary>
        /// 长度
        /// </summary>
        public decimal Length { get; set; }
        
    }

    /// <summary>
    /// 加工订单列表
    /// </summary>
    public class WorkOrderListModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderCode { get; set; }
        /// <summary>
        /// 类型编号
        /// </summary>
        public string TypeCode { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 加工厂编号
        /// </summary>
        public string ProcessFactoryCode { get; set; }
        /// <summary>
        /// 站点编号
        /// </summary>
        public string SiteCode { get; set; }
        /// <summary>
        /// 使用部位
        /// </summary>
        public string UsePart { get; set; }
        /// <summary>
        /// 加工状态
        /// </summary>
        public string ProcessingState { get; set; }
        /// <summary>
        /// 配送时间
        /// </summary>
        public string DistributionTime { get; set; }
        /// <summary>
        /// 配送地址
        /// </summary>
        public string DistributionAdd { get; set; }
        /// <summary>
        /// 总量合计
        /// </summary>
        public decimal WeightTotal { get; set; }
        /// <summary>
        /// 紧急程度
        /// </summary>
        public string UrgentDegree { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 附件
        /// </summary>
        public string Enclosure { get; set; }
        /// <summary>
        /// 录入人
        /// </summary>
        public string InsertUserCode { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public string InsertTime { get; set; }
        /// <summary>
        /// 审批状态
        /// </summary>
        public string Examinestatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ProjectId { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public string OrderStart { get; set; }
        /// <summary>
        /// 领料状态
        /// </summary>
        public string PickingState { get; set; }
        /// <summary>
        ///  是否线下订单 0否 1是
        /// </summary>
        public int IsOffline { get; set; }
        public string SiteName { get; set; }
        public string ProcessingStateNew { get; set; }
        public string ExaminestatusNew { get; set; }
        public string UserName { get; set; }
        public string DistributionStart { get; set; }
        public string ProcessFactoryName { get; set; }
        public string UrgentDegreeNew { get; set; }
        public string LoadCompleteTime { get; set; }
        public string SignState { get; set; }
        public string EnclosureSF { get; set; }
        public string SemiFinishedSignId { get; set; }
        /// <summary>
        /// 加工完成时间
        /// </summary>
        public string FinishProcessingDateTime { get; set; }
    }

    /// <summary>
    /// 项目清单信息
    /// </summary>
    public class ProjectListRequest : PageSearchRequest 
    {
        public string id { get; set; }
        public string zzbh { get; set; }
        public string zy { get; set; }
        public string dxt { get; set; }
        public string zxt { get; set; }
        public string xtlx { get; set; }
        public string cz { get; set; }
        public string sbcllx { get; set; }
        public string sbclmc { get; set; }
        public string mxgjbm { get; set; }
        public string gjbm { get; set; }
        public string ggcc { get; set; }
        public string cd { get; set; }
        public string mj { get; set; }
        /// <summary>
        /// 模型数据库文件
        /// </summary>
        public string dbName { get; set; }
    }

    public class PackageQRCodeRequest:PageSearchRequest
    {
        public int ID { get; set; }
        public string OrderCode { get; set; }
        public string SiteName { get; set; }
        public int SumNumber { get; set; }
        public string Major { get; set; }
        public string SignForState { get; set; }
        public string PackCode { get; set; }
        public string SystemType { get; set; }
        public string MaterialType { get; set; }
        public string ComponentName { get; set; }
        public string OrderDetialId { get; set; }
        public int ThisPackNumber { get; set; }
        public DateTime PackDate { get; set; }
    }
}
