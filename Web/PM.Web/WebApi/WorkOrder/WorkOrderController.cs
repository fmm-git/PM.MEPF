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

namespace PM.Web.WebApi.WorkOrder
{
    public class WorkOrderController : ApiController
    {
        private readonly TbWorkOrderLogic _workOrderLogic = new TbWorkOrderLogic();
        private readonly string _fileConfig = System.Configuration.ConfigurationManager.AppSettings["uploadBIMFile"];
        [HttpGet]
        public HttpResponseMessage GetComponentDetails([FromUri]ProjectListRequest request)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(request.SiteCode))
                {
                    string Path = request.SiteCode+"/";
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

        /// <summary>
        /// 通过订单编号获取打包列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetOrderPackList([FromUri]PackageQRCodeRequest request)
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
    }
}
