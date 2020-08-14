using PM.Business.System;
using PM.Common.Helper;
using PM.DataEntity;
using PM.DataEntity.BIM;
using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly string _fileConfig = System.Configuration.ConfigurationManager.AppSettings["uploadBIMFile"];
        /// <summary>
        /// 获取线路数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult GetOrganizationMapList(BIMRequest request)
        {
            var data = _organizationMap.GetOrganizationMapList(request);
            return Content(data.ToJson());
        }

        #region 上传模型

        /// <summary>
        /// 模型上传列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 新增页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Form()
        {
            return View();
        }

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult GetGridJson(BIMRequest request)
        {
            var data = _organizationMap.GetAllInfo(request);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteForm(int keyValue)
        {
            //判断该站点是否已有模型
            var modelInfo = _organizationMap.GetInfoById(keyValue);
            if (modelInfo != null)
            {
                //删除信息
                var data = _organizationMap.Delete(keyValue);
                var srcPath = Server.MapPath("/" + _fileConfig + "/" + modelInfo.SiteCode + "/" + modelInfo.Path);
                ZipHelper.DelectDir(srcPath);
                return Content(data.ToJson());
            }
            return Error("暂无模型数据信息");
        }

        /// <summary>
        /// 新增数据提交
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SubmitForm(string siteCode, string projectId)
        {
            try
            {
                var fileContext = Request.Files;
                if (fileContext.Count == 0) return Error("请上传文件");
                var fileItem = fileContext[0];
                var path = Server.MapPath("/" + _fileConfig + "/" + siteCode);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                }
                string srcZipFile = String.Format("{0}{1}", path, fileItem.FileName);
                fileItem.SaveAs(srcZipFile);
                //获取文件名称
                var dbName = ZipHelper.UnZipFile(srcZipFile);
                var folderName = dbName.Split('.')[0];
                path = path + "\\" + folderName;
                var files = ZipHelper.UnZipFile(srcZipFile, path);
                //删除文件
                System.IO.File.Delete(srcZipFile);
                string dbPath = path+ "\\" + dbName;
                var modelOrg = new TbModelOrg()
                {
                    Path = folderName,
                    ProjectId = projectId,
                    SiteCode = siteCode,
                    Type = 1,
                    Remark= fileItem.FileName.Split('.')[0]
                };
                var data = _organizationMap.Insert(modelOrg, dbPath);
                return Content(data.ToJson());
            }
            catch (Exception ex)
            {
                return Error("操作失败");
            }
        }
        #endregion
    }
}
