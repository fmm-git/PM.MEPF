using PM.Business.Production;
using PM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using PM.DataEntity.Production.ViewModel;
using PM.Business.RawMaterial;
using PM.Business;
using PM.Business.BIM;
using PM.Business.System;
using Newtonsoft.Json.Linq;
using PM.Common.Extension;
using PM.DataEntity;

namespace PM.Web.WebApi.WorkOrder
{
    public class WorkOrderController : ApiController
    {
        private readonly TbWorkOrderLogic _workOrderLogic = new TbWorkOrderLogic();
        private readonly TbAttachmentLogic _attachmentImp = new TbAttachmentLogic();
        private readonly ProblemOrderLogic _problemOrder = new ProblemOrderLogic();
        private readonly string _fileConfig = System.Configuration.ConfigurationManager.AppSettings["uploadBIMFile"];
        #region java接口
        [HttpGet]
        public HttpResponseMessage GetComponentDetails([FromUri]ProjectListRequest request)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(request.SiteCode))
                {
                    string Path = request.SiteCode + "/";
                    string ProjectId = request.ProjectId;
                    if (string.IsNullOrWhiteSpace(ProjectId))
                    {
                        ProjectId = "6372912251695766465";
                    }
                    if (!string.IsNullOrWhiteSpace(request.modelPath))
                    {
                        string a = request.modelPath.Replace(@"//", @"/");
                        string[] b = a.Split('/');
                        Path += b[4] + "/" + b[4] + ".tmp.db";
                    }
                    //string path = _workOrderLogic.GetModelDBPath(request.SiteCode, ProjectId);
                    string dbName = System.Web.Hosting.HostingEnvironment.MapPath("/" + _fileConfig + "/" + Path);
                    BIMLogic _BIMLogic = new BIMLogic(dbName);
                    var data = _BIMLogic.GetComponentDetails(request);
                    return AjaxResult.Success(data).ToJsonApi();
                }
                else
                {
                    return AjaxResult.Error("操作失败").ToJsonApi();
                }
            }
            catch (Exception)
            {
                return AjaxResult.Error("操作失败").ToJsonApi();
            }
        }

        /// <summary>
        /// 获取站点所有订单中模型构件的状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetSiteCodeDetailsState([FromUri]ProjectListRequest request)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(request.SiteCode))
                {
                    string ProjectId = request.ProjectId;
                    if (string.IsNullOrWhiteSpace(ProjectId))
                    {
                        ProjectId = "6372912251695766465";
                    }
                    var data = _workOrderLogic.GetSiteCodeDetailsState(request.SiteCode, ProjectId);
                    return AjaxResult.Success(data).ToJsonApi();
                }
                else
                {
                    return AjaxResult.Error("操作失败").ToJsonApi();
                }

            }
            catch (Exception)
            {
                return AjaxResult.Error("操作失败").ToJsonApi();
            }
        }

        #endregion

        #region App加工订单

        /// <summary>
        /// 获取订单列表（所有条目）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetAppDataListForPage([FromUri]WorkOrderRequest request)
        {
            try
            {
                var data = _workOrderLogic.GetDataListForPage(request);
                return AjaxResult.Success(data).ToJsonApi();

            }
            catch (Exception)
            {
                return AjaxResult.Error("操作失败").ToJsonApi();
            }
        }
        /// <summary>
        /// 订单详情（基本信息、明细信息）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetAppGetFormJson(int ID)
        {
            try
            {
                var data = _workOrderLogic.FindEntity(ID);
                return AjaxResult.Success(data).ToJsonApi();

            }
            catch (Exception)
            {
                return AjaxResult.Error("操作失败").ToJsonApi();
            }
        }

        /// <summary>
        /// 订单详情（基本信息、明细信息）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetAppFileUrl(string FileId)
        {
            try
            {
                var data = _attachmentImp.GetEnclosureUrl(FileId);
                return AjaxResult.Success(data).ToJsonApi();

            }
            catch (Exception)
            {
                return AjaxResult.Error("操作失败").ToJsonApi();
            }
        }

        #endregion

        #region App订单打包

        /// <summary>
        /// 包件列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetAppOrderPackList([FromUri]PackageQRCodeRequest request)
        {
            try
            {
                var data = _workOrderLogic.GetAppOrderPackList(request);
                return AjaxResult.Success(data).ToJsonApi();

            }
            catch (Exception)
            {
                return AjaxResult.Error("操作失败").ToJsonApi();
            }
        }
        /// <summary>
        /// 扫描二维码-包件信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetAppScanQRCodePackInfo(int ID)
        {
            try
            {
                var data = _workOrderLogic.GetScanQRCodePackInfo(ID);
                return AjaxResult.Success(data).ToJsonApi();

            }
            catch (Exception)
            {
                return AjaxResult.Error("操作失败").ToJsonApi();
            }
        }

        /// <summary>
        /// 包件详细信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetAppCodePackItemList(string PackCode)
        {
            try
            {
                var data = _workOrderLogic.GetAppCodePackItemList(PackCode);
                return AjaxResult.Success(data).ToJsonApi();

            }
            catch (Exception)
            {
                return AjaxResult.Error("操作失败").ToJsonApi();
            }
        }
        /// <summary>
        /// 包件明细签收
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage SubmitAppPackItemSignFor([FromBody]JObject jdata)
        {
            try
            {
                string modelstr = jdata["model"] == null ? "" : jdata["model"].ToString();
                if (string.IsNullOrWhiteSpace(modelstr))
                    return AjaxResult.Error("参数错误").ToJsonApi();
                var modelList = JsonEx.JsonToObj<List<PM.Business.Production.TbWorkOrderLogic.AppSignForOrInstallItem>>(modelstr);
                var data = _workOrderLogic.GetAppPackItemSignFor(modelList);
                return AjaxResult.Success(data).ToJsonApi();
            }
            catch (Exception)
            {

                return AjaxResult.Error("操作失败").ToJsonApi();
            }
        }

        /// <summary>
        /// 包件明细安装
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage SubmitAppPackItemInstall([FromBody]JObject jdata)
        {
            try
            {
                string modelstr = jdata["model"] == null ? "" : jdata["model"].ToString();
                if (string.IsNullOrWhiteSpace(modelstr))
                    return AjaxResult.Error("参数错误").ToJsonApi();
                var modelList = JsonEx.JsonToObj<List<PM.Business.Production.TbWorkOrderLogic.AppSignForOrInstallItem>>(modelstr);
                var data = _workOrderLogic.GetAppPackItemInstall(modelList);
                return AjaxResult.Success(data).ToJsonApi();
            }
            catch (Exception)
            {

                return AjaxResult.Error("操作失败").ToJsonApi();
            }
        }

        #endregion

        #region App签收
        /// <summary>
        /// 获取未签收的包件明细信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetNoSignForPackInfo([FromUri]WorkOrderDetailRequest request)
        {
            var data = _workOrderLogic.GetNoSignForPackInfo(request);
            return AjaxResult.Success(data).ToJsonApi();
        }
        /// <summary>
        /// 获取未签收的订单明细信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetNoSignForOrderDetail([FromUri]WorkOrderDetailRequest request)
        {
            var data = _workOrderLogic.GetNoSignForOrderDetail(request);
            return AjaxResult.Success(data).ToJsonApi();
        }
        /// <summary>
        /// 保存订单签收信息
        /// </summary>
        /// <param name="OrderCode">订单编号</param>
        /// <param name="signForModel">订单签收信息</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage SubmitOrderSignForForm([FromBody]JObject jdata)
        {
            try
            {
                string model = jdata["model"] == null ? "" : jdata["model"].ToString();
                string OrderCode = jdata["OrderCode"] == null ? "" : jdata["OrderCode"].ToString();
                if (string.IsNullOrWhiteSpace(model) || string.IsNullOrWhiteSpace(OrderCode))
                    return AjaxResult.Error("参数错误").ToJsonApi();
                var workOrderSignForItem = JsonEx.JsonToObj<List<TbWorkOrderSignFor>>(model);
                var data = _workOrderLogic.OrderSignForInsert(OrderCode, workOrderSignForItem);
                return AjaxResult.Success(data).ToJsonApi();

            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        ///判断选择的模型构件是否可以进行签收
        /// </summary>
        /// <param name="modelIdList"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage IsPlaceOrderAndSignFor([FromUri]WorkOrderDetailRequest request)
        {
            var data = _workOrderLogic.IsPlaceOrderAndSignFor(request.modelIdList);
            return AjaxResult.Success(data).ToJsonApi();
        }

        /// <summary>
        /// 获取通过模型构件id要签收的信息
        /// </summary>
        /// <param name="mxgjid"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetModelGjInfo(string mxgjid)
        {
            var data = _workOrderLogic.GetModelGjInfo(mxgjid);
            return AjaxResult.Success(data).ToJsonApi();
        }

        /// <summary>
        /// 保存模型构件签收信息
        /// </summary>
        /// <param name="signForModel">订单签收信息</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage SubmitModelSignForForm([FromBody]JObject jdata)
        {
            try
            {
                string model = jdata["model"] == null ? "" : jdata["model"].ToString();
                if (string.IsNullOrWhiteSpace(model))
                    return AjaxResult.Error("参数错误").ToJsonApi();
                var workOrderSignForItem = JsonEx.JsonToObj<List<TbWorkOrderSignFor>>(model);
                var data = _workOrderLogic.ModelSignForInsert(workOrderSignForItem);
                return AjaxResult.Success(data).ToJsonApi();

            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region App安装
        /// <summary>
        /// 获取未安装的包件明细信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetNoInstallPackInfo([FromUri]WorkOrderDetailRequest request)
        {
            var data = _workOrderLogic.GetNoInstallPackInfo(request);
            return AjaxResult.Success(data).ToJsonApi();
        }
        /// <summary>
        /// 获取未安装的订单明细信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetNoInstallOrderDetail([FromUri]WorkOrderDetailRequest request)
        {
            var data = _workOrderLogic.GetNoInstallOrderDetail(request);
            return AjaxResult.Success(data).ToJsonApi();
        }
        /// <summary>
        /// 保存订单签收信息
        /// </summary>
        /// <param name="OrderCode">订单编号</param>
        /// <param name="installForModel">订单安装信息</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage SubmitOrderInstallForm([FromBody]JObject jdata)
        {
            try
            {
                string model = jdata["model"] == null ? "" : jdata["model"].ToString();
                string OrderCode = jdata["OrderCode"] == null ? "" : jdata["OrderCode"].ToString();
                if (string.IsNullOrWhiteSpace(model) || string.IsNullOrWhiteSpace(OrderCode))
                    return AjaxResult.Error("参数错误").ToJsonApi();
                var workOrderInstallItem = JsonEx.JsonToObj<List<TbWorkOrderInstall>>(model);
                var data = _workOrderLogic.OrderInstallInsert(OrderCode, workOrderInstallItem);
                return AjaxResult.Success(data).ToJsonApi();

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 判断选中的模型构建ID集合是否存在已经安装了的模型构建id
        /// </summary>
        /// <param name="modelIdList">选中的模型构建ID集合</param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage IsPlaceOrderAndInstall([FromUri]WorkOrderDetailRequest request)
        {
            var data = _workOrderLogic.IsPlaceOrderAndInstall(request.modelIdList);
            return AjaxResult.Success(data).ToJsonApi();
        }
        /// <summary>
        /// 保存模型构件安装信息
        /// </summary>
        /// <param name="installForModel">安装信息</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage SubmitModelInstallForm([FromBody]JObject jdata)
        {
            try
            {
                string model = jdata["model"] == null ? "" : jdata["model"].ToString();
                if (string.IsNullOrWhiteSpace(model))
                    return AjaxResult.Error("参数错误").ToJsonApi();
                var workOrderInstallItem = JsonEx.JsonToObj<List<TbWorkOrderInstall>>(model);
                var data = _workOrderLogic.ModelInstallInsert(workOrderInstallItem);
                return AjaxResult.Success(data).ToJsonApi();

            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region App变更订单
        /// <summary>
        /// 获取订单列表（所有条目）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetProblemOrderList([FromUri]ProblemOrderRequest request)
        {
            try
            {
                var data = _problemOrder.GetDataListForPage(request);
                return AjaxResult.Success(data).ToJsonApi();

            }
            catch (Exception)
            {
                return AjaxResult.Error("操作失败").ToJsonApi();
            }
        }
        /// <summary>
        /// 获取订单列表（所有条目）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetProblemOrderFormJson(int ID)
        {
            try
            {
                var data = _problemOrder.FindEntity(ID);
                return AjaxResult.Success(data).ToJsonApi();

            }
            catch (Exception)
            {
                return AjaxResult.Error("操作失败").ToJsonApi();
            }
        }

        #endregion
    }
}
