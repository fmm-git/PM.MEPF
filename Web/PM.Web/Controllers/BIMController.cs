using Dos.Common;
using PM.Business.BIM;
using PM.Business.Production;
using PM.Common.Extension;
using PM.Common.Helper;
using PM.DataEntity.BIM;
using PM.DataEntity.Production.ViewModel;
using PM.Web.Models.ExcelModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PM.Web.Controllers
{
    /// <summary>
    /// GIS,3D模型
    /// </summary>
    [HandlerLogin]
    public class BIMController : BaseController
    {
        private readonly TbWorkOrderLogic _workOrderLogic = new TbWorkOrderLogic();

        #region GIS
        public ActionResult BIMGISView()
        {
            return View();
        }
        public ActionResult GISGridList()
        {
            return View();
        }
        /// <summary>
        /// 获取分页列表数据(构件进度)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult GetGISGridJson(WorkOrderRequest request)
        {
            var data = _workOrderLogic.GetDataListForPage(request);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        public ActionResult OutputGISExcel(string jsonData)
        {
            var request = JsonEx.JsonToObj<WorkOrderRequest>(jsonData);
            request.IsOutPut = true;
            var ret = _workOrderLogic.GetDataListForPage(request);
            decimal hj = 0;
            var data = (List<WorkOrderListModel>)ret.rows;
            if (data.Count > 0)
            {
                hj = data.Sum(p => p.WeightTotal);
            }
            string hzzfc = "合计(KG):" + hj;
            var dataList = MapperHelper.Map<WorkOrderListModel, WorkOrderExcel>(data);
            var fileStream = ExcelHelper.EntityListToExcelStream<WorkOrderExcel>(dataList, "构件进度", hzzfc);
            return File(fileStream, "application/vnd.ms-excel", "构件进度.xls");
        }

        #endregion

        #region 3D
        public ActionResult BIM3DView()
        {
            return View();
        }
        public ActionResult BIM3DGridList()
        {
            return View();
        }
        /// <summary>
        ///工点进度概况
        /// </summary>
        /// <returns></returns>
        public ActionResult ProgressOverView()
        {
            return View();
        }
        /// <summary>
        /// 获取分页列表数据(项目总清单)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult Get3DGridJson(BIMRequest request)
        {
            string dbName = Server.MapPath("~/DB_Data/tmp60CB.tmp.db");
            BIMLogic _BIMLogic = new BIMLogic(dbName);
            var data = _BIMLogic.GetDataListForPage(request);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 获取分页列表数据(项目清单明细_分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult Get3DItemGridJson(BIMRequest request)
        {
            string dbName = Server.MapPath("~/DB_Data/tmp60CB.tmp.db");
            BIMLogic _BIMLogic = new BIMLogic(dbName);
            var data = _BIMLogic.GetDataItemListForPage(request);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 获取项目结构树
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult Getmodel_tree()
        {
            string dbName = Server.MapPath("~/DB_Data/tmp60CB.tmp.db");
            BIMLogic _BIMLogic = new BIMLogic(dbName);
            var data = _BIMLogic.Getmodel_tree();
            var treeList = new List<TreeGridModel>();
            foreach (model_tree item in data)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = data.Count(t => t.pid == item.id) == 0 ? false : true;
                treeModel.id = item.id;
                treeModel.text = item.name;
                if (data.Count(t => t.id == item.pid) == 0)
                {
                    item.pid = "0";
                }
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.pid;
                treeModel.expanded = hasChildren;
                treeModel.entityJson = item.ToJson();
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeGridJson());
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        public ActionResult Output3DExcel(string jsonData)
        {
            var request = JsonEx.JsonToObj<WorkOrderRequest>(jsonData);
            request.IsOutPut = true;
            var ret = _workOrderLogic.GetDataListForPage(request);
            decimal hj = 0;
            var data = (List<WorkOrderListModel>)ret.rows;
            if (data.Count > 0)
            {
                hj = data.Sum(p => p.WeightTotal);
            }
            string hzzfc = "合计(KG):" + hj;
            var dataList = MapperHelper.Map<WorkOrderListModel, WorkOrderExcel>(data);
            var fileStream = ExcelHelper.EntityListToExcelStream<WorkOrderExcel>(dataList, "项目总清单", hzzfc);
            return File(fileStream, "application/vnd.ms-excel", "项目总清单.xls");
        }

        #endregion

        #region 构件资料
        public ActionResult ComponentData()
        {
            return View();
        }

        /// <summary>
        /// 获取分页列表数据(加工订单)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult GetWorkOrderGridJson(WorkOrderRequest request)
        {
            var data = _workOrderLogic.GetDataListForPage(request);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 获取分页列表数据(现场签收)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult GetSignGridJson(WorkOrderRequest request)
        {
            var data = _workOrderLogic.GetDataListForPage(request);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 获取分页列表数据(现场安装)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult GetInstallGridJson(WorkOrderRequest request)
        {
            var data = _workOrderLogic.GetDataListForPage(request);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 获取分页列表数据(完工验收)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult GetCheckGridJson(WorkOrderRequest request)
        {
            var data = _workOrderLogic.GetDataListForPage(request);
            return Content(data.ToJson());
        }
        #endregion

        #region 统计报表

        /// <summary>
        /// 右侧统计图
        /// </summary>
        /// <returns></returns>
        public ActionResult RightReportChar()
        {
            return View();
        }
        /// <summary>
        /// 订单报警详情
        /// </summary>
        /// <returns></returns>
        public ActionResult AlarmDetails()
        {
            return View();
        }
        /// <summary>
        /// 获取分页列表数据(订单审批超时)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult GetApprovalGridJson(WorkOrderRequest request)
        {
            var data = _workOrderLogic.GetDataListForPage(request);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 获取分页列表数据(订单完成超期)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult GetCompleteGridJson(WorkOrderRequest request)
        {
            var data = _workOrderLogic.GetDataListForPage(request);
            return Content(data.ToJson());
        }
        #endregion
    }
}
