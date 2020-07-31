using Dos.ORM;
using PM.Common;
using PM.DataAccess.DbContext;
using PM.DataEntity;
using PM.DataEntity.System.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM.Business.System
{
    public class TbProjectInfoLogic
    {

        /// <summary>
        /// 新增数据
        /// </summary>
        public AjaxResult Insert(TbProjectInfo model)
        {
            if (model == null)
                return AjaxResult.Warning("参数错误");
            model.InsertTime = DateTime.Now;
            try
            {
                var count = Repository<TbProjectInfo>.Insert(model);
                if (count > 0)
                {
                    return AjaxResult.Success();
                }
                return AjaxResult.Error("操作失败");
            }
            catch (Exception ex)
            {
                return AjaxResult.Error(ex.ToString());
            }
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        public AjaxResult Update(TbProjectInfo model)
        {
            if (model == null)
                return AjaxResult.Warning("参数错误");
            try
            {
                var count = Repository<TbProjectInfo>.Update(model);
                if (count > 0)
                    return AjaxResult.Success();
                return AjaxResult.Error("操作失败");
            }
            catch (Exception ex)
            {
                return AjaxResult.Error(ex.ToString());
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        public AjaxResult Delete(int dataID)
        {
            try
            {
                var count = Repository<TbProjectInfo>.Delete(p => p.ID == dataID);
                if (count > 0)
                    return AjaxResult.Success();
                return AjaxResult.Error("操作失败");
            }
            catch (Exception ex)
            {
                return AjaxResult.Error(ex.ToString());
            }
        }


        /// <summary>
        /// 获取编辑数据
        /// </summary>
        /// <param name="dataID">数据Id</param>
        /// <returns></returns>
        public Tuple<DataTable> FindEntity(int dataID)
        {
            var ret = Db.Context.From<TbProjectInfo>()
              .Select(TbProjectInfo._.All).Where(p => p.ID == dataID).ToDataTable();
            return new Tuple<DataTable>(ret);
        }

        /// <summary>
        /// 获取数据列表(分页)
        /// </summary>
        public PageModel GetDataListForPage(TbProjectInfoRequset request)
        {
            try
            {
                string orgType = OperatorProvider.Provider.CurrentUser.OrgType;
                string userCode=OperatorProvider.Provider.CurrentUser.UserCode;
                var where = new Where<TbProjectInfo>();
                if (orgType != "1" && userCode != "500000") {
                    where.And(d => d.ProjectId == request.ProjectId);
                }
                var ret = Db.Context.From<TbProjectInfo>()
              .Select(TbProjectInfo._.All).OrderBy(d => d.ID).ToPageList(request.rows, request.page);
                return ret;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 新增数据(单条)
        /// </summary>
        public AjaxResult InsertNew(List<TbProjectInfo> model, bool isApi = false)
        {
            if (model == null)
                return AjaxResult.Warning("参数错误");

            try
            {
                List<TbProjectInfo> data = Db.Context.From<TbProjectInfo>().Select(TbProjectInfo._.All).Where(a => a.ProSource == 0).ToList();
                using (DbTrans trans = Db.Context.BeginTransaction())
                {
                    //先删除原来的表
                    Repository<TbProjectInfo>.Delete(trans, data, isApi);
                    //插入从BM那边取过来的数据
                    Repository<TbProjectInfo>.Insert(trans, model, isApi);
                    trans.Commit();//提交事务
                    return AjaxResult.Success();
                }
            }
            catch (Exception)
            {
                return AjaxResult.Error();
            }

        }
    }
}
