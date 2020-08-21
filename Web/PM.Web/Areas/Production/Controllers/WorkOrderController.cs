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
        private readonly string _fileConfig = System.Configuration.ConfigurationManager.AppSettings["uploadBIMFile"];

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
            ViewBag.LoginUserCode = OperatorProvider.Provider.CurrentUser.UserCode;
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
        /// 判断模型构建是否下单
        /// </summary>
        /// <param name="modelIdList">模型构建ID集合</param>
        /// <returns></returns>
        public ActionResult IsModelIdPlaceOrder(List<string> modelIdList) 
        {
            var data = _workOrderLogic.IsModelIdPlaceOrder(modelIdList);
            return Content(data.ToJson());
        }
        public ActionResult IsProListPlaceOrder(string SiteCode, List<string> rowSelectIds, List<int> rowSelectTotals, List<string> rowSonSelectIdsNew) 
        {
            var data = _workOrderLogic.IsProListPlaceOrder(SiteCode, rowSelectIds, rowSelectTotals, rowSonSelectIdsNew);
            return Content(data.ToJson());
        }
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
            var data = _workOrderLogic.GetProjectList(request);
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
        /// 通过项目编号查询订单主表信息（确认发起时调用）
        /// </summary>
        /// <param name="OrderCode"></param>
        /// <returns></returns>
        public ActionResult GetWorkOrderData(string OrderCode)
        {
            var data = _workOrderLogic.GetWorkOrderData(OrderCode);
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
        /// <summary>
        /// 订单打包列表信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOrderPackGridJson(WorkOrderRequest request)
        {
            var data = _workOrderLogic.GetOrderPackGridJson(request);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 获取未打包的订单明细信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult GetNoPackOrderDetail(WorkOrderDetailRequest request) 
        {
            var data = _workOrderLogic.GetNoPackOrderDetail(request);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 获取下一包件号
        /// </summary>
        /// <returns></returns>
        public ActionResult GetNextPackCode() 
        {
            var data = CreateCode.GetNoNew2("PackCode", "TbWorkOrderPack");
            return Content(data.ToString());
        }

        /// <summary>
        /// 保存订单打包信息
        /// </summary>
        /// <param name="OrderCode">订单编号</param>
        /// <param name="packModel">订单打包信息</param>
        /// <returns></returns>
        public ActionResult SubmitOrderPackForm(string OrderCode, string packModel)
        {
            try
            {
                var workOrderPackItem = JsonEx.JsonToObj<List<TbWorkOrderPack>>(packModel);
                var data = _workOrderLogic.OrderPackInsert(OrderCode, workOrderPackItem);
                return Content(data.ToJson());

            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// 获取包件二维码信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPackageQRCodeJson(PackageQRCodeRequest request)
        {
            request.rows = 15;
            var data = _workOrderLogic.GetPackageQRCodeJson(request);
            return Content(data.ToJson());
        }

        #endregion

        #region 订单签收

        public ActionResult WorkOrderSignForIndex()
        {
            return View();
        }
        /// <summary>
        /// 订单签收列表信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOrderSignForGridJson(WorkOrderRequest request)
        {
            var data = _workOrderLogic.GetOrderSignForGridJson(request);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 选择签收数据源类型
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangeSignForTypeList() 
        {
            return View();
        }
        /// <summary>
        /// 获取未签收的包件明细信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult GetNoSignForPackInfo(WorkOrderDetailRequest request)
        {
            var data = _workOrderLogic.GetNoSignForPackInfo(request);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 获取未签收的订单明细信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult GetNoSignForOrderDetail(WorkOrderDetailRequest request)
        {
            var data = _workOrderLogic.GetNoSignForOrderDetail(request);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 保存订单签收信息
        /// </summary>
        /// <param name="OrderCode">订单编号</param>
        /// <param name="signForModel">订单签收信息</param>
        /// <returns></returns>
        public ActionResult SubmitOrderSignForForm(string OrderCode, string signForModel)
        {
            try
            {
                var workOrderSignForItem = JsonEx.JsonToObj<List<TbWorkOrderSignFor>>(signForModel);
                var data = _workOrderLogic.OrderSignForInsert(OrderCode, workOrderSignForItem);
                return Content(data.ToJson());

            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 判断选中的模型构建ID集合是否存在已经签收了的模型构建id
        /// </summary>
        /// <param name="modelIdList">选中的模型构建ID集合</param>
        /// <returns></returns>
        public ActionResult IsPlaceOrderAndSignFor(List<string> modelIdList)
        {
            var data = _workOrderLogic.IsPlaceOrderAndSignFor(modelIdList);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 获取通过模型构件id要签收的信息
        /// </summary>
        /// <param name="mxgjid"></param>
        /// <returns></returns>
        public ActionResult GetModelGjInfo(string mxgjid) 
        {
            var data = _workOrderLogic.GetModelGjInfo(mxgjid);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 保存模型构件签收信息
        /// </summary>
        /// <param name="signForModel">订单签收信息</param>
        /// <returns></returns>
        public ActionResult SubmitModelSignForForm(string signForModel)
        {
            try
            {
                var workOrderSignForItem = JsonEx.JsonToObj<List<TbWorkOrderSignFor>>(signForModel);
                var data = _workOrderLogic.ModelSignForInsert(workOrderSignForItem);
                return Content(data.ToJson());

            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 订单安装

        /// <summary>
        /// 判断选中的模型构建ID集合是否存在已经安装了的模型构建id
        /// </summary>
        /// <param name="modelIdList">选中的模型构建ID集合</param>
        /// <returns></returns>
        public ActionResult IsPlaceOrderAndInstall(List<string> modelIdList)
        {
            var data = _workOrderLogic.IsPlaceOrderAndInstall(modelIdList);
            return Content(data.ToJson());
        }
        public ActionResult WorkOrderInstallIndex()
        {
            return View();
        }
        /// <summary>
        /// 订单安装列表信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOrderInstallGridJson(WorkOrderRequest request)
        {
            var data = _workOrderLogic.GetOrderInstallGridJson(request);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 选择安装数据源类型
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangeInstallTypeList()
        {
            return View();
        }
        /// <summary>
        /// 获取未安装的包件明细信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult GetNoInstallPackInfo(WorkOrderDetailRequest request)
        {
            var data = _workOrderLogic.GetNoInstallPackInfo(request);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 获取未安装的订单明细信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult GetNoInstallOrderDetail(WorkOrderDetailRequest request)
        {
            var data = _workOrderLogic.GetNoInstallOrderDetail(request);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 保存订单签收信息
        /// </summary>
        /// <param name="OrderCode">订单编号</param>
        /// <param name="installForModel">订单安装信息</param>
        /// <returns></returns>
        public ActionResult SubmitOrderInstallForm(string OrderCode, string installForModel)
        {
            try
            {
                var workOrderInstallItem = JsonEx.JsonToObj<List<TbWorkOrderInstall>>(installForModel);
                var data = _workOrderLogic.OrderInstallInsert(OrderCode, workOrderInstallItem);
                return Content(data.ToJson());

            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存模型构件安装信息
        /// </summary>
        /// <param name="installForModel">安装信息</param>
        /// <returns></returns>
        public ActionResult SubmitModelInstallForm(string installForModel)
        {
            try
            {
                var workOrderInstallItem = JsonEx.JsonToObj<List<TbWorkOrderInstall>>(installForModel);
                var data = _workOrderLogic.ModelInstallInsert(workOrderInstallItem);
                return Content(data.ToJson());

            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

    }
}
