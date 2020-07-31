using PM.Business.System;
using PM.DataEntity;
using PM.DataEntity.System.ViewModel;
using PM.Domain.WebBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PM.Web.Areas.SystemManage.Controllers
{
    public class ProjectController : Controller
    {
        //
        // 项目信息
        private readonly TbProjectInfoLogic _projectInfo = new TbProjectInfoLogic();
        #region 视图
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form()
        {
            ViewBag.ProjectId = CreateCode.GetOrgId();
            return View();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取分页列表数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult GetGridJson(TbProjectInfoRequset request)
        {
            var data = _projectInfo.GetDataListForPage(request);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 编辑/查看页获取数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>

        [HandlerLogin(Ignore = false)]
        public ActionResult GetFormJson(int keyValue)
        {
            var data = _projectInfo.FindEntity(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 新增、修改数据提交
        /// </summary>
        /// <param name="request"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SubmitForm(TbProjectInfo model, string type)
        {
            try
            {
                if (type == "add")
                {
                    var data = _projectInfo.Insert(model);
                    return Content(data.ToJson());
                }
                else
                {
                    var data = _projectInfo.Update(model);
                    return Content(data.ToJson());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteForm(int keyValue)
        {
            var data = _projectInfo.Delete(keyValue);
            return Content(data.ToJson());
        }

        #endregion
    }
}
