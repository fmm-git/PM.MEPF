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
    public class TbBzhGlKuLogic
    {
        #region 分部分项树（方法）
        public List<TbBzhGlKu> GetDataList(string ProCode) 
        {
            string where = "";
            if (!string.IsNullOrWhiteSpace(ProCode)) 
            {
                where += " where 1=1 and ProCode='"+ProCode+"'";
            }
            string sql = "select * from TbBzhGlKu "+where+@" order by ID asc";
            List<TbBzhGlKu> list = Db.Context.FromSql(sql).ToList<TbBzhGlKu>();
            return list;
        }

        public Tuple<DataTable> FindFBFXEntity(int dataID)
        {
            var ret = Db.Context.From<TbBzhGlKu>()
              .Select(TbBzhGlKu._.All).Where(p => p.ID == dataID).ToDataTable();
            return new Tuple<DataTable>(ret);
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        public AjaxResult FBFXInsert(TbBzhGlKu model)
        {
            if (model == null)
                return AjaxResult.Warning("参数错误");
            try
            {
                model.InsertTime = DateTime.Now;
                model.InsertUserCode =OperatorProvider.Provider.CurrentUser.UserCode;
                var count = Repository<TbBzhGlKu>.Insert(model);
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
        public AjaxResult FBFXUpdate(TbBzhGlKu model)
        {
            if (model == null)
                return AjaxResult.Warning("参数错误");
            try
            {
                var count = Repository<TbBzhGlKu>.Update(model);
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
        public AjaxResult FBFXDelete(int dataID)
        {
            var model = Repository<TbBzhGlKu>.First(p => p.ID == dataID);
            try
            {
                if (model==null)
                    return AjaxResult.Warning("参数错误");
                //判断是否存在下级项目
                var soncount = Repository<TbBzhGlKu>.Any(p => p.ParentProCode == model.ProCode);
                if (soncount) 
                    return AjaxResult.Warning("存在下级项目，无法删除！！");
                var count = Repository<TbBzhGlKu>.Delete(p => p.ID == dataID);
                if (count > 0)
                    return AjaxResult.Success();
                return AjaxResult.Error("操作失败");
            }
            catch (Exception ex)
            {
                return AjaxResult.Error(ex.ToString());
            }
        }

        #endregion

        #region 标准件(方法)

        public PageModel GetBZJGridJson(TbBzhGjInfoRequest request)
        {
            //组装查询语句
            #region 模糊搜索条件

            string where = " where 1=1 ";
            if (!string.IsNullOrWhiteSpace(request.ProCode)) 
            {
                where += " and a.ProCode='"+request.ProCode+"'";
            }
            if (!string.IsNullOrWhiteSpace(request.ProName))
            {
                where += " and b.ProName='"+request.ProName+"'"; 
            }
            if (!string.IsNullOrWhiteSpace(request.ComponentName))
            {
                where += " and a.ComponentName='" + request.ComponentName + "'"; 
            }
            if (!string.IsNullOrWhiteSpace(request.ComponentType))
            {
                where += " and a.ComponentType='" + request.ComponentType + "'"; 
            }
            #endregion
            try
            {
                string sql = @"select a.*,b.ProName,c.DictionaryText as MeteringUnitText from TbBzhGjInfo a
                               left join TbBzhGlKu b on a.ProCode=b.ProCode
                               left join TbSysDictionaryData c on a.MeteringUnit=c.DictionaryCode and c.FDictionaryCode='Unit'";
                List<Dos.ORM.Parameter> para = new List<Dos.ORM.Parameter>();
                var data = Repository<TbBzhGjInfo>.FromSqlToPageTable(sql + where, para, request.rows, request.page, "ID", "desc");
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Tuple<DataTable> FindBZJEntity(int dataID)
        {
            var ret = Db.Context.From<TbBzhGjInfo>()
              .Select(TbBzhGjInfo._.All).Where(p => p.ID == dataID).ToDataTable();
            return new Tuple<DataTable>(ret);
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        public AjaxResult BZJInsert(TbBzhGjInfo model)
        {
            if (model == null)
                return AjaxResult.Warning("参数错误");
            try
            {
                model.InsertTime = DateTime.Now;
                model.InsertUserCode = OperatorProvider.Provider.CurrentUser.UserCode;
                var count = Repository<TbBzhGjInfo>.Insert(model);
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
        public AjaxResult BZJUpdate(TbBzhGjInfo model)
        {
            if (model == null)
                return AjaxResult.Warning("参数错误");
            try
            {
                var count = Repository<TbBzhGjInfo>.Update(model);
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
        public AjaxResult BZJDelete(int dataID)
        {
            try
            {
                var count = Repository<TbBzhGjInfo>.Delete(p => p.ID == dataID);
                if (count > 0)
                    return AjaxResult.Success();
                return AjaxResult.Error("操作失败");
            }
            catch (Exception ex)
            {
                return AjaxResult.Error(ex.ToString());
            }
        }

        #endregion

    }
}
