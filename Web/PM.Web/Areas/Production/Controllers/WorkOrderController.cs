using Dos.Common;
using PM.Business.Production;
using PM.Business.RawMaterial;
using PM.Common;
using PM.Common.Extension;
using PM.Common.Helper;
using PM.DataAccess.DbContext;
using PM.DataEntity;
using PM.DataEntity.Production.ViewModel;
using PM.DataEntity.System.ViewModel;
using PM.Domain.WebBase;
using PM.Web.Models.ExcelModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using PM.Business.BIM;

namespace PM.Web.Areas.Production.Controllers
{
    [HandlerLogin]
    public class WorkOrderController : BaseController
    {
        //
        //加工订单
        private readonly TbWorkOrderLogic _workOrderLogic = new TbWorkOrderLogic();

        #region 加工订单

        #region 列表
        public ActionResult Index()
        {
            return RedirectToAction("Default", "Home", new { area = "", isOrder = true });
        }

        /// <summary>
        /// 加工订单主页
        /// </summary>
        /// <returns></returns>
        public ActionResult WorkOrderIndex()
        {
            return View();
        }
        /// <summary>
        /// 加工订单报表
        /// </summary>
        /// <returns></returns>
        public ActionResult RightReportChar()
        {
            return View();
        }
        /// <summary>
        /// 包件二维码
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderPackIndex()
        {
            return View();
        }
        /// <summary>
        /// 获取分页列表数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult GetGridJson(WorkOrderRequest request)
        {
            if (request.SiteCode == null)
            {
                if (OperatorProvider.Provider.CurrentUser.OrgType != "1")
                    request.SiteCode = OperatorProvider.Provider.CurrentUser.CompanyId;
            }
            var data = _workOrderLogic.GetDataListForPage(request);
            return Content(data.ToJson());
        }

        #endregion

        #region 新增、修改、查看

        /// <summary>
        /// 订单新增、修改页面
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult Form(string type)
        {
            if (type == "add")
            {
                ViewBag.OrderCode = CreateCode.GetTableMaxCode("JGDD", "OrderCode", "TbWorkOrder");
                ViewBag.UserName = base.UserName;
                string orgType = PM.Common.OperatorProvider.Provider.CurrentUser.OrgType;
                ViewBag.OrgType = orgType;
                //判断当前登录人是否是站点人员
                if (orgType == "5")
                {
                    string CompanyId = PM.Common.OperatorProvider.Provider.CurrentUser.CompanyId;
                    DataTable dt = _workOrderLogic.GetCompany(CompanyId);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ViewBag.SiteCode = CompanyId;
                        ViewBag.SiteName = dt.Rows[0]["CompanyFullName"].ToString();
                        ViewBag.DistributionAdd = dt.Rows[0]["Address"].ToString();
                    }

                }
                ViewBag.ProcessFactoryCode = PM.Common.OperatorProvider.Provider.CurrentUser.ProcessFactoryCode;
                ViewBag.ProcessFactoryName = PM.Common.OperatorProvider.Provider.CurrentUser.ProcessFactoryName;
                ViewBag.ProjectId = PM.Common.OperatorProvider.Provider.CurrentUser.ProjectId;
            }
            var processingTechnology = new TbProcessingTechnologyLogic().GetChildList()
             .Select(p => string.Join(",", string.Format("{0}:{1}", p.ID, p.ProcessingTechnologyName)))
             .ToJson().ToString().Replace("[", "").Replace("]", "").Replace("\"", "").Replace(",", ";");

            ViewBag.ProcessingTechnology = processingTechnology;
            return View();
        }
        /// <summary>
        /// 新增、修改订单选择项目清单页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ProjectList() 
        {
            return View();
        }
        [HttpGet]
        /// <summary>
        /// 获取项目清单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult GetProjectList(ProjectListRequest request)
        {
            string dbName = Server.MapPath("~/DB_Data/tmp60CB.tmp.db");
            BIMLogic _BIMLogic = new BIMLogic(dbName);
            var data = _BIMLogic.GetProjectList(request);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 订单详情页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Details()
        {
            return View();
        }
        /// <summary>
        /// 编辑/查看页获取数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>

        [HandlerLogin(Ignore = false)]
        public ActionResult GetFormJson(int keyValue)
        {
            var data = _workOrderLogic.FindEntity(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 新增、修改数据提交
        /// </summary>
        /// <param name="model">主表信息</param>
        /// <param name="itemModel">明细信息</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public ActionResult SubmitForm(string model, string itemModel, string type)
        {
            try
            {
                var workOrderModel = JsonEx.JsonToObj<TbWorkOrder>(model);
                var workOrderItem = JsonEx.JsonToObj<List<TbWorkOrderDetail>>(itemModel);

                if (type == "add")
                {
                    var data = _workOrderLogic.Insert(workOrderModel, workOrderItem);
                    return Content(data.ToJson());
                }
                else
                {
                    var data = _workOrderLogic.Update(workOrderModel, workOrderItem);
                    return Content(data.ToJson());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region 删除

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteForm(int keyValue)
        {
            var data = _workOrderLogic.Delete(keyValue);
            return Content(data.ToJson());
        }

        #endregion

        #region 信息验证

        /// <summary>
        /// 判断信息是否可操作
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult AnyInfo(int keyValue)
        {
            var data = _workOrderLogic.AnyInfo(keyValue);
            return Content(data.ToJson());
        }


        #endregion

        #endregion

        #region 订单打包

        public ActionResult WorkOrderPackIndex()
        {
            return View();
        }

        #endregion

        #region 订单签收

        public ActionResult WorkOrderSignForIndex()
        {
            return View();
        }

        #endregion

        #region 订单安装

        public ActionResult WorkOrderInstallIndex()
        {
            return View();
        }

        #endregion

    }
}
