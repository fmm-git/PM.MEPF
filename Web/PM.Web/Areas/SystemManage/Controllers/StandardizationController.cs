using PM.Business.System;
using PM.DataEntity;
using PM.DataEntity.System.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PM.Web.Areas.SystemManage.Controllers
{
    public class StandardizationController : Controller
    {
        private TbBzhGlKuLogic _bzhglkLogic = new TbBzhGlKuLogic();
        //
        //标准化管理
        #region 视图
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult FBFXIndex() 
        {
            return View();
        }
        public ActionResult FBFXForm()
        {
            return View();
        }
        public ActionResult BZJIndex() 
        {
            return View();
        }

        public ActionResult BZJForm()
        {
            return View();
        }
        #endregion

        #region 方法(分部分项树)

        /// <summary>
        /// 列表信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetGridJson(string ProCode)
        {
            var data = _bzhglkLogic.GetDataList(ProCode).ToList();
            var treeList = new List<TreeGridModel>();
            foreach (TbBzhGlKu item in data)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = data.Count(t => t.ParentProCode == item.ProCode) == 0 ? false : true;
                treeModel.id = item.ProCode;
                treeModel.text = item.ProName;
                if (data.Count(t => t.ProCode == item.ParentProCode) == 0)
                {
                    item.ParentProCode = "0";
                }
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.ParentProCode;
                treeModel.expanded = hasChildren;
                treeModel.entityJson = item.ToJson();
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeGridJson());
        }

        public ActionResult GetSelectJson(string keyValue)
        {
            var data = _bzhglkLogic.GetDataList(keyValue).ToList();
            var List = new List<TreeSelectModel>();
            foreach (TbBzhGlKu item in data)
            {
                TreeSelectModel Model = new TreeSelectModel();
                Model.id = item.ProCode;
                Model.text = item.ProName;
                Model.parentId = item.ParentProCode;
                List.Add(Model);
            }
            return Content(List.TreeSelectJson());
        }

        /// <summary>
        /// 获取编辑数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(int keyValue)
        {
            var data = _bzhglkLogic.FindFBFXEntity(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 新增、修改数据提交
        /// </summary>
        /// <param name="request"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SubmitForm(TbBzhGlKu model, string type)
        {
            try
            {
                if (type == "add")
                {
                    var data = _bzhglkLogic.FBFXInsert(model);
                    return Content(data.ToJson());
                }
                else
                {
                    var data = _bzhglkLogic.FBFXUpdate(model);
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
            var data = _bzhglkLogic.FBFXDelete(keyValue);
            return Content(data.ToJson());
        }

        #endregion

        #region 方法（标准件）

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetBZJGridJson(TbBzhGjInfoRequest request)
        {
            var data = _bzhglkLogic.GetBZJGridJson(request);
            return Content(data.ToJson());
        }

        public ActionResult GetBZJFormJson(int keyValue) 
        {
            var data = _bzhglkLogic.FindBZJEntity(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 新增、修改数据提交
        /// </summary>
        /// <param name="request"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SubmitBZJForm(TbBzhGjInfo model, string type)
        {
            try
            {
                if (type == "add")
                {
                    var data = _bzhglkLogic.BZJInsert(model);
                    return Content(data.ToJson());
                }
                else
                {
                    var data = _bzhglkLogic.BZJUpdate(model);
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
        public ActionResult BZJDeleteForm(int keyValue)
        {
            var data = _bzhglkLogic.BZJDelete(keyValue);
            return Content(data.ToJson());
        }

        #endregion

    }
}
