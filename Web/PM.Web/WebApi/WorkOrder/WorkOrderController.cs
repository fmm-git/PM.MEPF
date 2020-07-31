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

namespace PM.Web.WebApi.WorkOrder
{
    public class WorkOrderController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetComponentDetails([FromUri]ProjectListRequest request)
        {
            try
            {
                string dbName = System.Web.Hosting.HostingEnvironment.MapPath("~/DB_Data/tmp60CB.tmp.db");
                BIMLogic _BIMLogic = new BIMLogic(dbName);
                var data = _BIMLogic.GetComponentDetails(request);
                return AjaxResult.Success(data).ToJsonApi();
            }
            catch (Exception)
            {
                return AjaxResult.Error("操作失败").ToJsonApi();
            }
        }
    }
}
