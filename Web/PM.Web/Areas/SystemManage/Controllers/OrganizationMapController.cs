using PM.Business.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PM.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 地图组织机构数据
    /// </summary>
    public class OrganizationMapController : BaseController
    {
        private readonly OrganizationMapLogic _organizationMap = new OrganizationMapLogic();

        /// <summary>
        /// 获取线路数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult GetOrganizationMapList(int type=0, string projectId="")
        {
            var data = _organizationMap.GetOrganizationMapList(type, projectId);
            return Content(data.ToJson());
        }
    }
}
