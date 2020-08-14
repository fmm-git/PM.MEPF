﻿using Dos.Common;
using Dos.ORM;
using PM.Business.BIM;
using PM.Business.Production;
using PM.Common;
using PM.Common.Helper;
using PM.DataAccess.DbContext;
using PM.DataEntity;
using PM.DataEntity.BIM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM.Business.System
{
    public class OrganizationMapLogic
    {
        private readonly TbWorkOrderLogic _workOrderLogic = new TbWorkOrderLogic();
        public AjaxResult GetOrganizationMapList(BIMRequest request)
        {
            var retData = new List<TbOrganizationMap>();
            var where = new Where<TbOrganizationMap>();
            if (request.Type > 0)
                where.And(p => p.Type == request.Type);
            if (!string.IsNullOrEmpty(request.ProjectId))
                where.And(p => p.ProjectId == request.ProjectId);
            if (!string.IsNullOrEmpty(request.SiteCode))
            {
                List<string> SiteList = _workOrderLogic.GetCompanyWorkAreaOrSiteList(request.SiteCode, 5);//站点
                where.And(p => p.CompanyCode.In(SiteList) || p.ParentCompanyCode == request.SiteCode);
            }
            retData = Db.Context.From<TbOrganizationMap>()
                    .Select(
                      TbOrganizationMap._.All)
                  .Where(where).ToList();
            return AjaxResult.Success(retData);
        }

        #region 上传模型

        /// <summary>
        /// 列表数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<TbModelOrg> GetAllInfo(BIMRequest request)
        {
            string where = " where 1=1 ";
            if (!string.IsNullOrWhiteSpace(request.ProjectId))
            {
                where += " and a.ProjectId='" + request.ProjectId + "'";
            }
            if (!string.IsNullOrWhiteSpace(request.SiteCode))
            {
                List<string> SiteList = _workOrderLogic.GetCompanyWorkAreaOrSiteList(request.SiteCode, 5);//站点
                string siteStr = string.Join("','", SiteList);
                where += (" and a.SiteCode in('" + siteStr + "')");
            }
            try
            {
                string sql = @"select a.*,b.CompanyFullName as SiteName,c.CompanyFullName as WorkArea ,d.CompanyFullName as Branch 
                            from TbModelOrg a
                            left join TbCompany b on a.SiteCode=b.CompanyCode
                            left join TbCompany c on b.ParentCompanyCode=c.CompanyCode
                            left join TbCompany d on c.ParentCompanyCode=d.CompanyCode";

                var data = Db.Context.FromSql(sql + where).ToList<TbModelOrg>();
                return data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public TbModelOrg GetInfoById(int keyValue)
        {
            return Repository<TbModelOrg>.First(p => p.ID == keyValue);
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        public AjaxResult Insert(TbModelOrg model, string dbPath)
        {
            if (model == null)
                return AjaxResult.Warning("参数错误");
            BIMLogic _BIMLogic = new BIMLogic(dbPath);
            try
            {
                var list = _BIMLogic.GetModelInfoList(model.SiteCode, model.ProjectId, model.Path);
                var insertSql = SqlBuilderHelper.BulkInsertSql<ProjectListInsertModel>(list, "TbModel_Property");
                using (DbTrans trans = Db.Context.BeginTransaction())
                {
                    //添加模型基础信息
                    var dataList = Db.Context.FromSql(insertSql).SetDbTransaction(trans).ExecuteNonQuery();
                    //添加模型上传信息
                    Repository<TbModelOrg>.Insert(model);
                    trans.Commit();
                    return AjaxResult.Success();
                }
            }
            catch (Exception ex)
            {
                return AjaxResult.Error();
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        public AjaxResult Delete(int keyValue)
        {
            try
            {
                var data = Repository<TbModelOrg>.First(p => p.ID == keyValue);
                using (DbTrans trans = Db.Context.BeginTransaction())
                {
                    Repository<TbModel_Property>.Delete(trans, p => p.FileName == data.Path && p.SiteCode == data.SiteCode && p.ProjectId == data.ProjectId);
                    Repository<TbModelOrg>.Delete(trans, p => p.ID == keyValue);
                    trans.Commit();
                    return AjaxResult.Success();
                }
            }
            catch (Exception ex)
            {
                return AjaxResult.Error(ex.ToString());
            }
        }

        #endregion

        #region 模型项目清单附加信息

        public AjaxResult EditModelOtherInfo(TbModelOtherInfo model)
        {
            if (model == null)
                return AjaxResult.Warning("参数错误");
            try
            {
                var data = Repository<TbModelOtherInfo>.First(p => p.ComponentCode == model.ComponentCode && p.Type == model.Type);
                if (data != null)
                {
                    Repository<TbModelOtherInfo>.Update(model);
                }
                else
                {
                    Repository<TbModelOtherInfo>.Insert(model);
                }
                return AjaxResult.Success();
            }
            catch (Exception)
            {
                return AjaxResult.Error();
            }
        }

        public AjaxResult EditModelOtherInfo(List<TbModelOtherInfo> model)
        {
            try
            {
                var codeList = model.Select(p => p.ComponentCode).ToList();
                var removeList = new List<string>();
                var data = Repository<TbModelOtherInfo>.Query(p => p.ComponentCode.In(codeList) && p.Type == 2);
                if (data.Any())
                {
                    data.ForEach(x =>
                    {
                        var md = model.First(p => p.ComponentCode == x.ComponentCode);
                        x.PlanTime = md.PlanTime;
                        removeList.Add(x.ComponentCode);
                    });
                    Repository<TbModelOtherInfo>.Update(data);
                }
                var lastList = model.Where(p => !removeList.Contains(p.ComponentCode)).ToList();
                Repository<TbModelOtherInfo>.Insert(lastList);
                return AjaxResult.Success();
            }
            catch (Exception)
            {
                return AjaxResult.Error();
            }
        }
        #endregion
    }
}