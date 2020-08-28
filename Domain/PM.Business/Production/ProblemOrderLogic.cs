using Dos.ORM;
using PM.Common;
using PM.DataAccess.DbContext;
using PM.DataEntity;
using PM.DataEntity.Production.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM.Business.Production
{
    /// <summary>
    /// 订单变更
    /// </summary>
    public class ProblemOrderLogic
    {
        private readonly TbWorkOrderLogic _workOrderLogic = new TbWorkOrderLogic();

        #region 新增数据

        /// <summary>
        /// 新增数据
        /// </summary>
        public AjaxResult Insert(TbProblemOrder model, List<TbProblemOrderItem> items)
        {
            if (model == null)
                return AjaxResult.Warning("参数错误");
            model.InsertUserCode = OperatorProvider.Provider.CurrentUser.UserCode;
            model.Examinestatus = "未发起";
            model.ProcessFactoryCode = OperatorProvider.Provider.CurrentUser.ProcessFactoryCode;
            try
            {
                using (DbTrans trans = Db.Context.BeginTransaction())
                {
                    //添加信息
                    Repository<TbProblemOrder>.Insert(trans, model);
                    //添加明细信息
                    Repository<TbProblemOrderItem>.Insert(trans, items);
                    trans.Commit();
                    return AjaxResult.Success();
                }
            }
            catch (Exception ex)
            {
                return AjaxResult.Error();
            }
        }

        #endregion

        #region 修改数据

        /// <summary>
        /// 修改数据
        /// </summary>
        public AjaxResult Update(TbProblemOrder model, List<TbProblemOrderItem> items)
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
                    Repository<TbProblemOrder>.Update(trans, model, p => p.ID == model.ID);
                    if (items.Count > 0)
                    {
                        //删除历史明细信息
                        Repository<TbProblemOrderItem>.Delete(trans, p => p.ProblemOrderCode == model.ProblemOrderCode);
                        //添加明细信息
                        Repository<TbProblemOrderItem>.Insert(trans, items);
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

        #endregion

        #region 删除数据

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
                    //删除信息
                    var count = Repository<TbProblemOrder>.Delete(trans, p => p.ID == keyValue);
                    //删除明细信息
                    Repository<TbProblemOrderItem>.Delete(trans, p => p.ProblemOrderCode == ((TbProblemOrder)anyRet.data).ProblemOrderCode);
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

        #region 获取数据

        /// <summary>
        /// 获取编辑数据
        /// </summary>
        /// <param name="keyValue">数据Id</param>
        /// <returns></returns>
        public Tuple<object, object> FindEntity(int keyValue)
        {
            var ret = Db.Context.From<TbProblemOrder>()
                .Select(
                       TbProblemOrder._.All
                    , TbUser._.UserName
                    , TbCompany._.CompanyFullName.As("SiteName"))
                    .AddSelect(Db.Context.From<TbCompany>()
                      .Select(p => p.CompanyFullName)
                      .Where(TbCompany._.CompanyCode == TbProblemOrder._.ProcessFactoryCode), "ProcessFactoryName")
                  .LeftJoin<TbUser>((a, c) => a.InsertUserCode == c.UserCode)
                  .LeftJoin<TbCompany>((a, c) => a.SiteCode == c.CompanyCode)
                  .Where(p => p.ID == keyValue).ToDataTable();
            var items = Db.Context.From<TbProblemOrderItem>()
               .Select(TbProblemOrderItem._.All)
                 .Where(p => p.ProblemOrderCode == ret.Rows[0]["ProblemOrderCode"].ToString()).ToDataTable();
            return new Tuple<object, object>(ret, items);
        }

        /// <summary>
        /// 获取数据列表(分页)
        /// </summary>
        public PageModel GetDataListForPage(ProblemOrderRequest request)
        {

            #region 模糊搜索条件
            var where = new Where<TbProblemOrder>();
            if (!string.IsNullOrWhiteSpace(request.OrderCode))
            {
                where.And(d => d.OrderCode.Like(request.OrderCode));
            }
            if (!string.IsNullOrWhiteSpace(request.SiteCode))
            {
                List<string> SiteList = _workOrderLogic.GetCompanyWorkAreaOrSiteList(request.SiteCode, 5);
                where.And(p => p.SiteCode.In(SiteList));
            }
            if (!string.IsNullOrWhiteSpace(request.ProcessFactoryCode))
            {
                where.And(p => p.ProcessFactoryCode == request.ProcessFactoryCode);
            }
            if (!string.IsNullOrWhiteSpace(request.ChangeStatus))
            {
                where.And(d => d.ChangeStatus == request.ChangeStatus);
            }
            #endregion

            #region 数据权限
            if (!string.IsNullOrEmpty(request.ProjectId))
                where.And(p => p.ProjectId == request.ProjectId);
            #endregion

            try
            {
                var sql = Db.Context.From<TbProblemOrder>()
                    .Select(
                      TbProblemOrder._.All
                    , TbProblemOrder._.Examinestatus.As("ExaminestatusNew")
                    , TbUser._.UserName
                    , TbCompany._.CompanyFullName.As("SiteName"))
                  .LeftJoin<TbUser>((a, c) => a.InsertUserCode == c.UserCode)
                  .LeftJoin<TbCompany>((a, c) => a.SiteCode == c.CompanyCode)
                  .Where(where)
                  .OrderByDescending(p => p.ID);
                var data = new PageModel();
                if (request.IsOutPut)
                {
                    data.rows = sql.ToDataTable();
                }
                else
                {
                    data = sql.ToPageList(request.rows, request.page);
                }
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 判断信息是否可操作
        /// </summary>
        /// <returns></returns>
        public AjaxResult AnyInfo(int keyValue)
        {
            var problemOrder = Repository<TbProblemOrder>.First(p => p.ID == keyValue);
            if (problemOrder == null)
                return AjaxResult.Warning("信息不存在");
            if (problemOrder.Examinestatus != "未发起" && problemOrder.Examinestatus != "已退回")
                return AjaxResult.Warning("信息正在审核中或已审核完成,不能进行此操作");

            return AjaxResult.Success(problemOrder);
        }
        #endregion

        /// <summary>
        /// 获取数据列表(原订单)
        /// </summary>
        public PageModel GetOrderDataList(ProblemOrderRequest request)
        {
            #region 模糊搜索条件
            var where = new Where<TbWorkOrder>();
            where.And(p => p.Examinestatus == "审核完成" && p.OrderState != "加工完成" && p.ChangeStatus != "全部变更" && p.OrderType == "jzjg");
            where.And(TbWorkOrder._.OrderCode
                     .SubQueryNotIn(Db.Context.From<TbProblemOrder>().Where(p => p.Examinestatus == "未发起" || p.Examinestatus == "审批中").Select(p => p.OrderCode)));
            if (!string.IsNullOrWhiteSpace(request.keyword))
                where.And(p => p.OrderCode.Like(request.keyword));
            if (!string.IsNullOrEmpty(request.ProcessFactoryCode))
                where.And(p => p.ProcessFactoryCode == request.ProcessFactoryCode);
            if (!string.IsNullOrEmpty(request.ProjectId))
                where.And(p => p.ProjectId == request.ProjectId);
            if (!string.IsNullOrWhiteSpace(request.SiteCode))
            {
                List<string> SiteList = _workOrderLogic.GetCompanyWorkAreaOrSiteList(request.SiteCode, 5);//站点
                where.And(p => p.SiteCode.In(SiteList));
            }
            #endregion
            try
            {
                var data = Db.Context.From<TbWorkOrder>().Select(
                    TbWorkOrder._.All,
                    TbCompany._.CompanyFullName.As("SiteName"))
                    .AddSelect(Db.Context.From<TbCompany>()
                      .Select(p => p.CompanyFullName)
                      .Where(TbCompany._.CompanyCode == TbWorkOrder._.ProcessFactoryCode), "ProcessFactoryName")
                    .LeftJoin<TbCompany>((a, c) => a.SiteCode == c.CompanyCode)
                    .Where(where)
                    .OrderByDescending(p => p.ID)
                    .ToPageList(request.rows, request.page);
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取数据列表(原订单明细)
        /// </summary>
        public PageModel GetOrderItemDataList(ProblemOrderRequest request)
        {
            #region 模糊搜索条件

            var where = new Where<TbWorkOrderDetail>();
            where.And(p => p.ComponentStrat == "加工中");
            if (!string.IsNullOrWhiteSpace(request.keyword))
            {
                where.And(p => p.ComponentName.Like(request.keyword));
            }
            if (!string.IsNullOrWhiteSpace(request.OrderCode))
            {
                where.And(p => p.OrderCode == request.OrderCode);
            }
            #endregion
            try
            {
                var data = Db.Context.From<TbWorkOrderDetail>().Select(
                    TbWorkOrderDetail._.All,
                    TbWorkOrderDetail._.ID.As("WorkOrderDetailId"))
                    .Where(where)
                    .ToPageList(request.rows, request.page);
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetProblemReport(ProblemOrderRequest request)
        {
            try
            {
                string where = " where 1=1 and Examinestatus!='未发起' ";
                if (!string.IsNullOrWhiteSpace(request.ProjectId))
                {
                    where += " and ProjectId=@ProjectId ";
                }
                if (!string.IsNullOrWhiteSpace(request.SiteCode))
                {
                    List<string> SiteList = _workOrderLogic.GetCompanyWorkAreaOrSiteList(request.SiteCode, 5);//站点
                    string siteStr = string.Join("','", SiteList);
                    where += " and SiteCode in(" + siteStr + ")";
                }
                if (!string.IsNullOrWhiteSpace(request.DateType))
                {
                    if (request.DateType == "Month")
                    {
                        where += " and CONVERT(varchar(100), InsertTime,23)>=CONVERT(varchar(100), dateadd(month, datediff(month, 0, getdate()), 0), 23) and CONVERT(varchar(100), InsertTime,23)<=CONVERT(varchar(100), dateadd(month, datediff(month, 0, dateadd(month, 1, getdate())), -1), 23)";
                    }
                    else
                    {
                        where += " and CONVERT(varchar(100), InsertTime,23)>=CONVERT(varchar(100), dateadd(year, datediff(year, 0, getdate()), 0),23) and CONVERT(varchar(100), InsertTime,23)<=CONVERT(varchar(100), dateadd(year, datediff(year, 0, dateadd(year, 1, getdate())), -1),23)";
                    }
                }
                string sql = @"select COUNT(1) as Count,SiteCode,CompanyFullName as SiteName from TbProblemOrder
                             left join TbCompany on TbProblemOrder.SiteCode=TbCompany.CompanyCode ";

                where += "group by SiteCode,CompanyFullName";
                DataTable dt = Db.Context.FromSql(sql+where).AddInParameter("@ProjectId", DbType.String, request.ProjectId).ToDataTable();
                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
