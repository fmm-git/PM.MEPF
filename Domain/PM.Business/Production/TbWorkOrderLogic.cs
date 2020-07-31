using Dos.Common;
using Dos.ORM;
using PM.Common;
using PM.Common.Helper;
using PM.DataAccess.DbContext;
using PM.DataEntity;
using PM.DataEntity.Production.ViewModel;
using PM.DataEntity.System.ViewModel;
using PM.Domain.WebBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PM.Business.Production
{
    public class TbWorkOrderLogic
    {

        #region 新增、修改、删除数据
        /// <summary>
        /// 新增数据
        /// </summary>
        public AjaxResult Insert(TbWorkOrder model, List<TbWorkOrderDetail> items)
        {
            if (model == null)
                return AjaxResult.Warning("参数错误");
            model.InsertUserCode = OperatorProvider.Provider.CurrentUser.UserCode;
            model.InsertTime = DateTime.Now;
            using (DbTrans trans = Db.Context.BeginTransaction())
            {
                try
                {
                    //添加信息及明细信息
                    Repository<TbWorkOrder>.Insert(trans, model);
                    Repository<TbWorkOrderDetail>.Insert(trans, items);
                    trans.Commit();
                    return AjaxResult.Success();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return AjaxResult.Error(ex.ToString());
                }
                finally
                {
                    trans.Close();
                }
            }
        }


        /// <summary>
        /// 修改数据
        /// </summary>
        public AjaxResult Update(TbWorkOrder model, List<TbWorkOrderDetail> items)
        {
            if (model == null)
                return AjaxResult.Warning("参数错误");
            var anyRet = AnyInfo(model.ID);
            if (anyRet.state.ToString() != ResultType.success.ToString())
                return anyRet;
            try
            {
                using (DbTrans trans = Db.Context.BeginTransaction())//使用事务
                {
                    //修改信息
                    Repository<TbWorkOrder>.Update(trans, model, p => p.ID == model.ID);

                    if (items.Count > 0)
                    {
                        //删除历史明细信息,添加明细信息
                        Repository<TbWorkOrderDetail>.Delete(trans, p => p.OrderCode == model.OrderCode);
                        Repository<TbWorkOrderDetail>.Insert(trans, items);
                    }
                    trans.Commit();//提交事务

                    return AjaxResult.Success();
                }
            }
            catch (Exception ex)
            {
                return AjaxResult.Error(ex.ToString());
            }
        }


        /// <summary>
        /// 删除数据
        /// </summary>
        public AjaxResult Delete(int keyValue)
        {
            try
            {
                //判断信息是否存在
                var anyRet = AnyInfo(keyValue);
                if (anyRet.state.ToString() != ResultType.success.ToString())
                    return anyRet;
                using (DbTrans trans = Db.Context.BeginTransaction())
                {
                    //删除信息，删除明细信息
                    var count = Repository<TbWorkOrder>.Delete(trans, p => p.ID == keyValue);
                    Repository<TbWorkOrderDetail>.Delete(trans, p => p.OrderCode == ((TbWorkOrder)anyRet.data).OrderCode);

                    trans.Commit();//提交事务
                    return AjaxResult.Success();
                }
            }
            catch (Exception ex)
            {
                return AjaxResult.Error(ex.ToString());
            }
        }

        #endregion

        #region 获取数据

        /// <summary>
        /// 获取编辑数据
        /// </summary>
        /// <param name="dataID">数据Id</param>
        /// <returns></returns>
        public Tuple<DataTable, DataTable> FindEntity(int dataID)
        {
            var ret = Db.Context.From<TbWorkOrder>()
            .Select(
                    TbWorkOrder._.All
                    , TbCompany._.CompanyFullName.As("SiteName")
                    , TbSysDictionaryData._.DictionaryText.As("ProcessingStateNew")
                    , TbUser._.UserName)
                  .LeftJoin<TbCompany>((a, c) => a.SiteCode == c.CompanyCode)
                  .AddSelect(Db.Context.From<TbCompany>().Select(p => p.CompanyFullName)
                  .Where(TbCompany._.CompanyCode == TbWorkOrder._.ProcessFactoryCode), "ProcessFactoryName")
                  .AddSelect(Db.Context.From<TbSysDictionaryData>().Select(p => p.DictionaryText)
                  .Where(TbSysDictionaryData._.DictionaryCode == TbWorkOrder._.UrgentDegree && TbSysDictionaryData._.FDictionaryCode == "UrgentDegree"), "UrgentDegreeNew")
                  .LeftJoin<TbSysDictionaryData>((a, c) => a.OrderState == c.DictionaryCode && c.FDictionaryCode == "ProcessingState")
                  .LeftJoin<TbUser>((a, c) => a.InsertUserCode == c.UserCode)
                  .LeftJoin<TbDistributionPlanInfo>((a, c) => a.OrderCode == c.OrderCode)
                    .Where(p => p.ID == dataID).ToDataTable();
            //查找明细信息
            var items = Db.Context.From<TbWorkOrderDetail>().Select(
               TbWorkOrderDetail._.All)
           .Where(p => p.OrderCode == ret.Rows[0]["OrderCode"].ToString()).ToDataTable();
            return new Tuple<DataTable, DataTable>(ret, items);
        }

        /// <summary>
        /// 获取数据列表(分页)
        /// </summary>
        public PageModel GetDataListForPage(WorkOrderRequest request)
        {
            //组装查询语句
            #region 模糊搜索条件

            var where = new Where<TbWorkOrder>();


            if (!string.IsNullOrWhiteSpace(request.OrderCode))
            {
                where.And(p => p.OrderCode.Like(request.OrderCode));
            }

            #endregion

            #region 数据权限新

            //if (!string.IsNullOrWhiteSpace(request.ProcessFactoryCode))
            //{
            //    where.And(p => p.ProcessFactoryCode == request.ProcessFactoryCode);
            //}
            if (!string.IsNullOrWhiteSpace(request.ProjectId))
            {
                where.And(p => p.ProjectId == request.ProjectId);
            }
            if (!string.IsNullOrWhiteSpace(request.SiteCode))
            {
                List<string> SiteList = GetCompanyWorkAreaOrSiteList(request.SiteCode, 5);
                if (SiteList.Count > 0)
                {
                    where.And(p => p.SiteCode.In(SiteList));
                }
            }

            #endregion

            try
            {
                var ret = Db.Context.From<TbWorkOrder>()
              .Select(
                      TbWorkOrder._.All
                      , TbCompany._.CompanyFullName.As("SiteName")
                      , TbSysDictionaryData._.DictionaryText.As("OrderStateNew")
                      , TbUser._.UserName)
                    .LeftJoin<TbCompany>((a, c) => a.SiteCode == c.CompanyCode)
                    .LeftJoin<TbSysDictionaryData>((a, c) => a.OrderState == c.DictionaryCode && c.FDictionaryCode == "OrderState")
                    .AddSelect(Db.Context.From<TbSysDictionaryData>().Select(p => p.DictionaryText)
                    .Where(TbSysDictionaryData._.DictionaryCode == TbWorkOrder._.UrgentDegree && TbSysDictionaryData._.FDictionaryCode == "UrgentDegree"), "UrgentDegreeNew")
                    .AddSelect(Db.Context.From<TbSysDictionaryData>().Select(p => p.DictionaryText)
                    .Where(TbSysDictionaryData._.DictionaryCode == TbWorkOrder._.OrderType && TbSysDictionaryData._.FDictionaryCode == "OrderType"), "OrderTypeNew")
                    .LeftJoin<TbUser>((a, c) => a.InsertUserCode == c.UserCode)
                      .Where(where).OrderByDescending(d => d.ID).ToPageList(request.rows, request.page);
                return ret;
            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion

        #region 判断

        /// <summary>
        /// 判断信息是否可操作
        /// </summary>
        /// <returns></returns>
        public AjaxResult AnyInfo(int keyValue)
        {
            var monthDemandPlan = Repository<TbWorkOrder>.First(p => p.ID == keyValue);
            if (monthDemandPlan == null)
                return AjaxResult.Warning("信息不存在");
            if (monthDemandPlan.Examinestatus != "未发起" && monthDemandPlan.Examinestatus != "已退回")
                return AjaxResult.Warning("信息正在审核中或已审核完成,不能进行此操作");

            return AjaxResult.Success(monthDemandPlan);
        }

        #endregion

        #region 获取该组织机构下的所有站点
        /// <summary>
        /// 获取该组织机构下的所有站点
        /// </summary>
        /// <param name="parentCode"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<string> GetCompanyWorkAreaOrSiteList(string parentCode, int type)
        {
            string sql = @"WITH TREE AS(SELECT * FROM TbCompany WHERE CompanyCode =@parentCode UNION ALL SELECT TbCompany.* FROM TbCompany, TREE WHERE TbCompany.ParentCompanyCode = TREE.CompanyCode) SELECT CompanyCode FROM TREE where TREE.OrgType=@type";
            var dt = Db.Context.FromSql(sql)
                .AddInParameter("@parentCode", DbType.String, parentCode)
                .AddInParameter("@type", DbType.Int32, type).ToList<string>();
            if (!dt.Any())
                dt.Add(parentCode);
            return dt;
        }
        public DataTable GetCompany(string CompanyId)
        {
            var ret = Db.Context.From<TbCompany>()
             .Select(
                     TbCompany._.CompanyCode
                     , TbCompany._.CompanyFullName
                     , TbCompany._.Address)
                     .Where(p => p.CompanyCode == CompanyId).ToDataTable();
            return ret;

        }
        #endregion

    }
}
