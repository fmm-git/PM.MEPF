using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PM.Business;
using PM.DataEntity;
using PM.Common.Extension;
using PM.Domain.WebBase;

namespace PM.Web.Areas.SystemManage.Controllers
{
    public class RoleUserController : BaseController
    {
        private readonly TbUserRoleLogic _userRole = new TbUserRoleLogic();

        #region 页面视图
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Form(string type)
        {
            ViewBag.type = type;
            ViewBag.UserId = CreateCode.GetOrgId();
            ViewBag.UserCode = CreateCode.GetNextUserCode();
            return View();
        }
        public ActionResult Details()
        {
            return View();
        }
        public ActionResult UserList()
        {
            return View();
        }

        #endregion


        #region 查询语句
        /// <summary>
        /// 获取该组织机构下的所有用户(列表数据)
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCompanyUser(TbUserRequest request, string UserName, string CompanyCode, string DepartmentProjectId)
        {
            var data = _userRole.GetCompanyUser(request, UserName, CompanyCode, DepartmentProjectId);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 详情信息
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyValue)
        {
            var data = _userRole.GetFormJson(keyValue);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 获取部门角色
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="OrgType"></param>
        /// <returns></returns>
        public ActionResult GetTreeGridDeptOrRoleJson(string ProjectId, string OrgType)
        {
            var data = _userRole.GetTreeGridDeptOrRoleJson(ProjectId, OrgType).ToList();
            var treeList = new List<TreeGridModel>();
            foreach (DetpOrRole item in data)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = data.Count(t => t.pid == item.id) == 0 ? false : true;
                treeModel.id = item.id;
                treeModel.text = item.Name;
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
        /// 获取该用户下有哪些角色
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="OrgId"></param>
        /// <param name="OrgType"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public ActionResult GetUserRoleNew(string ProjectId, string OrgId, string OrgType, string UserId)
        {
            var data = _userRole.GetUserRoleNew(ProjectId, OrgId, OrgType, UserId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 获取分页列表数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetGridJson(TbUserRoleRequset request)
        {
            var data = _userRole.GetDataListForPage(request);
            return Content(data.ToJson());
        }
        #endregion


        #region 页面数据操作方法

        public ActionResult SubmitForm(string userModel, string userRoleModel, string type)
        {
            var user = JsonEx.JsonToObj<TbUser>(userModel);
            var userRole = JsonEx.JsonToObj<List<TbUserRole>>(userRoleModel);
            if (type == "add")
            {
                var data = _userRole.Insert(user, userRole);
                return Content(data.ToJson());
            }
            else
            {
                var data = _userRole.Update(user, userRole);
                return Content(data.ToJson());
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            var data = _userRole.Delete(keyValue);
            return Content(data.ToJson());
        }

        #endregion

    }
}
