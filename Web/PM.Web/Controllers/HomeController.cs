using PM.Common;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using PM.Business;
using PM.DataEntity;
using PM.Business.Production;
using PM.Business.System;
using PM.DataEntity.System.ViewModel;
using PM.Business.RawMaterial;

namespace PM.Web.Controllers
{
    [HandlerLogin]
    public class HomeController : BaseController
    {

        private readonly UserLogic _User = new UserLogic();
        private readonly TbWorkOrderLogic _workorder = new TbWorkOrderLogic();
        private HomeLogic _Home = new HomeLogic();
        public HomeController()
        {
        }

        #region 切换组织机构

        /// <summary>
        /// 切换项目组织机构
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateProjectOrg()
        {
            ViewBag.ProjectOrgAllId = OperatorProvider.Provider.CurrentUser.ProjectOrgAllId;
            return View();
        }

        public ActionResult GetAllProjectOrg()
        {
            string UserCode = OperatorProvider.Provider.CurrentUser.UserId;
            var list = _User.GetAllProjectOrg(UserCode);
            var treeList = new List<TreeGridModel>();
            foreach (var item in list)
            {
                bool hasChildren = list.Count(p => p.ProjectId == item.OrgId) == 0 ? false : true;
                TreeGridModel treeModel = new TreeGridModel();
                treeModel.id = item.OrgId;
                treeModel.text = item.OrgName;
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.ProjectId;
                treeModel.expanded = true;
                treeModel.entityJson = item.ToJson();
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeGridJson());
        }

        //切换组织机构修改登陆信息中的组织机构相关信息
        public ActionResult SaveProjectOrg(string OrgId, string OrgName, string ProjectId, string OrgType, string ProjectOrgAllId, string ProjectOrgAllName)
        {
            string UserCode = OperatorProvider.Provider.CurrentUser.UserCode;
            var operatorModel = _User.SaveProjectOrg(UserCode, OrgId, OrgName, ProjectId, OrgType, ProjectOrgAllId, ProjectOrgAllName);
            OperatorProvider.Provider.RemoveCurrent();//先移除Cookie或者Session
            OperatorProvider.Provider.AddCurrent(operatorModel);//在添加Cookie或者Session
            Session["username"] = OperatorProvider.Provider.CurrentUser.UserName;
            Session["usercode"] = OperatorProvider.Provider.CurrentUser.UserCode;
            Session["userid"] = OperatorProvider.Provider.CurrentUser.UserId;
            return Content(operatorModel.ToJson());
        }


        #endregion

        #region 切换加工厂
        /// <summary>
        /// 切换项目组织机构
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateProjectJgc()
        {
            ViewBag.ProcessFactoryCode = OperatorProvider.Provider.CurrentUser.ProcessFactoryCode;
            ViewBag.ProcessFactoryName = OperatorProvider.Provider.CurrentUser.ProcessFactoryName;
            return View();
        }
        public ActionResult GetAllProjectJgc(PageSearchRequest request)
        {
            request.rows = 50;
            var data = _User.GetAllProjectJgc(request);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 切换加工厂信息，将当前切换的加工厂信息存到登陆信息中
        /// </summary>
        /// <returns></returns>
        public ActionResult SaveProcessFactoryCode(string ProcessFactoryCode, string ProcessFactoryName)
        {
            var operatorModel = _User.SaveProcessFactoryCode(ProcessFactoryCode, ProcessFactoryName);
            OperatorProvider.Provider.RemoveCurrent();//先移除Cookie或者Session
            OperatorProvider.Provider.AddCurrent(operatorModel);//在添加Cookie或者Session
            Session["username"] = OperatorProvider.Provider.CurrentUser.UserName;
            Session["usercode"] = OperatorProvider.Provider.CurrentUser.UserCode;
            Session["userid"] = OperatorProvider.Provider.CurrentUser.UserId;
            return Content(operatorModel.ToJson());
        }
        #endregion

        #region App下载

        public ActionResult AppDown()
        {
            return View();
        }

        #endregion

        /// <summary>
        /// 修改密码窗体
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdatePwd()
        {
            ViewBag.UserCode = base.UserCode;
            return View();
        }

        /// <summary>
        /// 确认原密码，如果原密码输入错误，禁止用户操作
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPwd(string UserCode,string Pwd)
        {
            var data = _User.SelectPwd(UserCode, Pwd);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 修改密码按钮事件
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdatePwdA(UserRequest result, string UserCode)
        {
            var data = _User.UpDatePwd(result, UserCode);
            return Content(data.ToJson());
        }

        public ActionResult Index()
        {
            //return View(OperatorProvider.Provider.CurrentUser);
            ViewBag.UserName = base.UserName;
            ViewBag.OrgName = OperatorProvider.Provider.CurrentUser.ComPanyName;
            ViewBag.OrgId = OperatorProvider.Provider.CurrentUser.CompanyId;
            ViewBag.ProjectOrgAllName = OperatorProvider.Provider.CurrentUser.ProjectOrgAllName;
            ViewBag.OrgType = OperatorProvider.Provider.CurrentUser.OrgType;
            ViewBag.IsSystem = OperatorProvider.Provider.CurrentUser.IsSystem;
            ViewBag.ProcessFactoryCode = OperatorProvider.Provider.CurrentUser.ProcessFactoryCode;
            ViewBag.ProcessFactoryName = OperatorProvider.Provider.CurrentUser.ProcessFactoryName;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Main()
        {
            return View();
        }
        public ActionResult NoRight()
        {
            return View();
        }
        //欢迎首页
        [HttpGet]
        public ActionResult Default(bool isOrder=false)
        {
            ViewBag.UserName = base.UserName;
            ViewBag.OrgType = OperatorProvider.Provider.CurrentUser.OrgType;
            ViewBag.ProcessFactoryCode = OperatorProvider.Provider.CurrentUser.ProcessFactoryCode;
            ViewBag.IsOrder = isOrder;
            return View();
        }

        #region 接口测试页面
        public ActionResult InterfaceTestPage()
        {
            return View();
        }
        #endregion

        
    }
}