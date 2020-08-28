using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PM.DataEntity.Production.ViewModel
{
    public class ProblemOrderRequest: PageSearchRequest
    {


        /// <summary>
        /// 撤销状态
        /// </summary>
        public string ChangeStatus { get; set; }

        /// <summary>
        /// 原订单编号
        /// </summary>
        public string OrderCode { get; set; }

        /// <summary>
        /// 变更编号
        /// </summary>
        public string ProblemOrderCode { get; set; }
        /// <summary>
        /// 变更类型
        /// </summary>
        public string ChangeType { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public string DateType { get; set; }
        /// <summary>
        /// 弹框收索条件
        /// </summary>
        public string keyword { get; set; }
        public bool IsOutPut { get; set; }
    }
}
