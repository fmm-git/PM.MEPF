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
                    Repository<TbWorkOrderPack>.Delete(trans, p => p.OrderCode == ((TbWorkOrder)anyRet.data).OrderCode);
                    Repository<TbPackDetail>.Delete(trans, p => p.OrderCode == ((TbWorkOrder)anyRet.data).OrderCode);
                    Repository<TbWorkOrderSignFor>.Delete(trans, p => p.OrderCode == ((TbWorkOrder)anyRet.data).OrderCode);
                    Repository<TbSignForDetail>.Delete(trans, p => p.OrderCode == ((TbWorkOrder)anyRet.data).OrderCode);
                    Repository<TbWorkOrderInstall>.Delete(trans, p => p.OrderCode == ((TbWorkOrder)anyRet.data).OrderCode);

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
               TbWorkOrderDetail._.All
               , TbUser._.UserName.As("PackUserName"))
               .LeftJoin<TbUser>((a, c) => a.PackUserCode == c.UserCode)
               .AddSelect(Db.Context.From<TbUser>().Select(p => p.UserName)
               .Where(TbUser._.UserCode == TbWorkOrderDetail._.SignForUserCode), "SignForUserName")
               .AddSelect(Db.Context.From<TbUser>().Select(p => p.UserName)
               .Where(TbUser._.UserCode == TbWorkOrderDetail._.InstallUserCode), "InstallUserName")
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
                    .AddSelect(Db.Context.From<TbWorkOrderPack>().Select(p => p.ID.Count())
                    .Where(TbWorkOrderPack._.OrderCode == TbWorkOrder._.OrderCode), "PackNum")
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
        /// <summary>
        /// 判断模型构建是否下单
        /// </summary>
        /// <param name="modelIdList">模型构建ID集合</param>
        /// <returns></returns>
        public AjaxResult IsModelIdPlaceOrder(List<string> modelIdList)
        {
            string str = pjMxIdList(modelIdList);
            string sql = "select MxGjId from TbWorkOrderDetail where MxGjId in(" + str + ")";
            DataTable dt = Db.Context.FromSql(sql).ToDataTable();
            if (dt.Rows.Count > 0)
                return AjaxResult.Error("选中的模型构建中存在已经下单了构建");
            else
                return AjaxResult.Success();
        }
        /// <summary>
        /// 判断选中的模型构建ID集合是否存在已经签收了的模型构建id
        /// </summary>
        /// <param name="modelIdList">选中的模型构建ID集合</param>
        /// <returns></returns>
        public AjaxResult IsPlaceOrderAndSignFor(List<string> modelIdList)
        {
            string str = pjMxIdList(modelIdList);
            string sql = @"select a.MxGjId from TbWorkOrderDetail a
                left join TbWorkOrder b on a.OrderCode = b.OrderCode
                where b.Examinestatus = '审核完成' and MxGjId in(" + str + ") and IsSignFor = '否'";
            DataTable dt = Db.Context.FromSql(sql).ToDataTable();
            if (dt.Rows.Count <= 0)
            {
                return AjaxResult.Error("选中的模型构件还不能进行该操作！！");
            }
            else if (dt.Rows.Count > 0 && dt.Rows.Count < modelIdList.Count)
                return AjaxResult.Error("选中的模型构件中有已经签收的构件！！");
            else
                return AjaxResult.Success();
        }
        /// <summary>
        /// 判断选中的模型构建ID集合是否存在已经安装了的模型构件id
        /// </summary>
        /// <param name="modelIdList">选中的模型构件ID集合</param>
        /// <returns></returns>
        public AjaxResult IsPlaceOrderAndInstall(List<string> modelIdList)
        {
            string str = pjMxIdList(modelIdList);
            string sql = @"select a.MxGjId from TbWorkOrderDetail a
                left join TbWorkOrder b on a.OrderCode = b.OrderCode
                where b.Examinestatus = '审核完成' and MxGjId in(" + str + ") and IsInstall = '否'";
            DataTable dt = Db.Context.FromSql(sql).ToDataTable();
            if (dt.Rows.Count <= 0)
            {
                return AjaxResult.Error("选中的模型构件还不能进行该操作！！");
            }
            else if (dt.Rows.Count > 0 && dt.Rows.Count < modelIdList.Count)
                return AjaxResult.Error("选中的模型构件中有已经安装的构件！！");
            else
                return AjaxResult.Success();
        }
        public string pjMxIdList(List<string> modelIdList)
        {
            StringBuilder sb = new StringBuilder();
            if (modelIdList.Count > 0)
            {
                for (int i = 0; i < modelIdList.Count; i++)
                {
                    if (i == modelIdList.Count - 1)
                    {
                        sb.Append("'" + modelIdList[i] + "'");
                    }
                    else
                    {
                        sb.Append("'" + modelIdList[i] + "',");
                    }
                }
            }
            return sb.ToString();
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

        #region 订单打包信息

        /// <summary>
        /// 获取订单打包信息列表（分页）
        /// </summary>
        public PageModel GetOrderPackGridJson(WorkOrderRequest request)
        {
            //组装查询语句
            #region 模糊搜索条件

            string where = " where 1=1 ";
            if (!string.IsNullOrWhiteSpace(request.OrderCode))
            {
                where += " and a.OrderCode='" + request.OrderCode + "'";
            }

            #endregion

            try
            {
                string sql = @"select a.*,c.CompanyFullName as SiteName from TbWorkOrderPack a
left join TbWorkOrder b on a.OrderCode=b.OrderCode
left join TbCompany c on b.SiteCode=c.CompanyCode ";
                //参数化
                List<Parameter> parameter = new List<Parameter>();
                var model = Repository<TbWorkOrderPack>.FromSqlToPageTable(sql + where, parameter, request.rows, request.page, "ID", "asc");
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取订单未打包的订单明细信息列表（分页）
        /// </summary>
        public PageModel GetNoPackOrderDetail(WorkOrderDetailRequest request)
        {
            //组装查询语句
            #region 模糊搜索条件

            string where = " where 1=1 and a.IsPack='否' and a.IsSignFor='否' ";
            if (!string.IsNullOrWhiteSpace(request.OrderCode))
            {
                where += " and a.OrderCode='" + request.OrderCode + "'";
            }

            #endregion

            try
            {

                string sql = @"select a.ID,c.CompanyFullName as SiteName,a.OrderCode,a.SystemType,a.MaterialType,a.ComponentCode,a.ComponentName,a.SpecificationModel,a.Length from TbWorkOrderDetail a
left join TbWorkOrder b on a.OrderCode=b.OrderCode
left join TbCompany c on b.SiteCode=c.CompanyCode ";
                //参数化
                List<Parameter> parameter = new List<Parameter>();
                var model = Repository<TbWorkOrderDetail>.FromSqlToPageTable(sql + where, parameter, request.rows, request.page, "ComponentCode", "asc");
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 新增订单打包信息数据
        /// </summary>
        public AjaxResult OrderPackInsert(string OrderCode, List<TbWorkOrderPack> items)
        {
            if (string.IsNullOrWhiteSpace(OrderCode) || items.Count <= 0)
                return AjaxResult.Warning("参数错误");
            var orderModel = Repository<TbWorkOrder>.First(p => p.OrderCode == OrderCode);
            var orderItemList = Repository<TbWorkOrderDetail>.Query(p => p.OrderCode == OrderCode);
            List<TbWorkOrderDetail> orderDetailList = new List<TbWorkOrderDetail>();
            List<TbPackDetail> packDetial = new List<TbPackDetail>();
            //定义一个List集合存放打包订单明细id
            List<string> packOrderDetailIdList = new List<string>();
            for (int i = 0; i < items.Count; i++)
            {
                items[i].PackUserCode = OperatorProvider.Provider.CurrentUser.UserCode;
                var itemId = items[i].OrderDetialId;
                var itemIdStr = itemId.Split(',');
                if (itemIdStr.Length > 0)
                {
                    for (int j = 0; j < itemIdStr.Length; j++)
                    {
                        packOrderDetailIdList.Add(itemIdStr[j]);

                        //修改订单明细表信息中的打包信息===================
                        int orderId = Convert.ToInt32(itemIdStr[j]);
                        var orderDetailNew = orderItemList.Find(p => p.ID == orderId);
                        if (orderDetailNew != null)
                        {
                            orderDetailNew.IsPack = "是";
                            orderDetailNew.PackDate = items[i].PackDate;
                            orderDetailNew.PackUserCode = items[i].PackUserCode;
                            orderDetailNew.PackCode = items[i].PackCode;
                            //判断订单是否加工完成
                            if (orderDetailNew.ComponentStrat == "加工中")
                            {
                                orderDetailNew.ComponentStrat = "加工完成";
                            }
                            orderDetailList.Add(orderDetailNew);
                        }
                        //修改订单明细表信息中的打包状态===================
                        TbPackDetail packModel = new TbPackDetail();
                        packModel.OrderCode = items[i].OrderCode;
                        packModel.PackCode = items[i].PackCode;
                        packModel.OrderDetialId = itemIdStr[j];
                        packModel.IsSignFor = "否";
                        packModel.IsInstall = "否";
                        packDetial.Add(packModel);
                    }
                }
            }

            //判断是否有已经打包了构件明细信息，后面又被删除了的情况。
            if (packOrderDetailIdList.Count < orderItemList.Count)
            {
                orderModel.PackState = "部分打包";
                var orderDetailNew = orderItemList.Where(p => p.IsPack == "是").ToList();
                if (orderDetailNew.Count() > 0)
                {
                    for (int i = 0; i < orderDetailNew.Count(); i++)
                    {
                        //不存在,就把订单明细中的打包信息还原。
                        if (packOrderDetailIdList.Contains(orderDetailNew[i].ID.ToString()) == false)
                        {
                            var orderDetailNew1 = orderItemList.Find(p => p.ID == orderDetailNew[i].ID);
                            if (orderDetailNew1 != null)
                            {
                                orderDetailNew1.IsPack = "否";
                                orderDetailNew1.PackDate = null;
                                orderDetailNew1.PackUserCode = null;
                                //判断订单如果是加工完成，就应该把订单的加工状态还原为加工中
                                if (orderDetailNew1.ComponentStrat == "加工完成" && orderDetailNew1.IsSignFor == "否")
                                {
                                    orderDetailNew1.ComponentStrat = "加工中";
                                }
                                orderDetailList.Add(orderDetailNew1);
                            }
                        }
                    }
                }
                //判断是否有已经打包了构件明细信息，后面又被删除了的情况。==============
            }
            else
            {
                orderModel.PackState = "打包完成";
                //判断订单是否加工完成
                if (string.IsNullOrWhiteSpace(orderModel.JgProgress))
                {
                    orderModel.OrderState = "加工完成";
                    orderModel.JgCompleteTiem = DateTime.Now;
                    string dt1 = Convert.ToDateTime(orderModel.DistributionTime).ToShortDateString();
                    string dt2 = DateTime.Now.ToShortDateString();
                    if (DateTime.Parse(dt1) == DateTime.Parse(dt2))
                    {
                        orderModel.JgProgress = "正常";
                    }
                    else if (DateTime.Parse(dt1) > DateTime.Parse(dt2))
                    {
                        orderModel.JgProgress = "提前";
                    }
                    else
                    {
                        orderModel.JgProgress = "延迟";
                    }
                }
            }
            //修改订单主表信息中的打包状态===================

            using (DbTrans trans = Db.Context.BeginTransaction())
            {
                try
                {
                    if (orderModel != null)
                    {
                        //修改订单主表中的订单打包状态
                        Repository<TbWorkOrder>.Update(trans, orderModel);
                    }
                    if (orderDetailList.Count > 0)
                    {
                        //修改订单明细表中是否打包信息
                        Repository<TbWorkOrderDetail>.Update(trans, orderDetailList);
                    }
                    //先删除原来的打包明细信息  
                    Repository<TbPackDetail>.Delete(trans, p => p.OrderCode == OrderCode);
                    //添加打包明细信息
                    Repository<TbPackDetail>.Insert(trans, packDetial);
                    //先删除原来的订单打包信息  
                    Repository<TbWorkOrderPack>.Delete(trans, p => p.OrderCode == OrderCode);
                    //添加打包信息
                    Repository<TbWorkOrderPack>.Insert(trans, items);
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


        public PageModel GetPackageQRCodeJson(PackageQRCodeRequest request)
        {
            string where = " where 1=1 and b.ID is not null ";
            if (!string.IsNullOrWhiteSpace(request.ProjectId))
            {
                where += " and a.ProjectId='" + request.ProjectId + "'";
            }
            if (!string.IsNullOrWhiteSpace(request.OrderCode))
            {
                where += " and a.OrderCode='" + request.OrderCode + "'";
            }
            if (!string.IsNullOrWhiteSpace(request.SiteCode))
            {
                List<string> SiteList = GetCompanyWorkAreaOrSiteList(request.SiteCode, 5);//站点
                string siteStr = string.Join("','", SiteList);
                where += (" and a.SiteCode in(" + SiteList + ")");
            }
            try
            {
                string sql = @"select b.*,CONVERT(varchar(100), b.PackDate, 23) as PackDateNew,CONVERT(varchar(100), a.DistributionTime, 23) as DistributionTime,a.SumNumber,a.Major,c.CompanyFullName as SiteName,a.SignForState from TbWorkOrder a
left join TbWorkOrderPack b on a.OrderCode=b.OrderCode
left join TbCompany c on a.SiteCode=c.CompanyCode ";
                //参数化
                List<Parameter> parameter = new List<Parameter>();
                var model = Repository<TbWorkOrderPack>.FromSqlToPageTable(sql + where, parameter, request.rows, request.page, "PackCode", "desc");
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region 订单签收信息

        /// <summary>
        /// 获取订单签收信息列表（分页）
        /// </summary>
        public PageModel GetOrderSignForGridJson(WorkOrderRequest request)
        {
            //组装查询语句
            #region 模糊搜索条件

            string where = " where 1=1 ";

            if (!string.IsNullOrWhiteSpace(request.SignForType))
            {
                //通过模型签收
                if (request.SignForType == "ModelSignFor")
                {
                    where += " and a.OrderCode=''";
                }
                else//通过订单签收
                {
                    if (!string.IsNullOrWhiteSpace(request.OrderCode))
                    {
                        where += " and a.OrderCode='" + request.OrderCode + "'";
                    }
                }
            }
            #endregion

            try
            {
                string sql = @"select a.*,c.CompanyFullName as SiteName from TbWorkOrderSignFor a
left join TbWorkOrder b on a.OrderCode=b.OrderCode
left join TbCompany c on b.SiteCode=c.CompanyCode ";
                //参数化
                List<Parameter> parameter = new List<Parameter>();
                var model = Repository<TbWorkOrderSignFor>.FromSqlToPageTable(sql + where, parameter, request.rows, request.page, "ID", "asc");
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取未签收的打包信息列表（分页）
        /// </summary>
        public PageModel GetNoSignForPackInfo(WorkOrderDetailRequest request)
        {
            //组装查询语句
            #region 模糊搜索条件

            string where = " where 1=1 and ISNULL(d.ThisPackNoSignForNumber,0)>0";
            if (!string.IsNullOrWhiteSpace(request.OrderCode))
            {
                where += " and a.OrderCode='" + request.OrderCode + "'";
            }

            #endregion

            try
            {

                string sql = @"select a.ID,c.CompanyFullName as SiteName,a.PackCode,a.OrderCode,a.SystemType,a.MaterialType,a.ComponentName,ISNULL(d.ThisPackNoSignForNumber,0) as ThisPackNumber,d.OrderDetialId,a.PackDate,a.Remark from TbWorkOrderPack a
left join (SELECT COUNT(1) as ThisPackNoSignForNumber,PackCode,OrderDetialId=(STUFF(( SELECT ',' + OrderDetialId FROM TbPackDetail WHERE PackCode = Test.PackCode and IsSignFor='否' FOR XML PATH('')), 1, 1, '') )
FROM TbPackDetail AS Test where IsSignFor='否' GROUP BY PackCode) d on a.PackCode=d.PackCode
left join TbWorkOrder b on a.OrderCode=b.OrderCode
left join TbCompany c on b.SiteCode=c.CompanyCode ";
                //参数化
                List<Parameter> parameter = new List<Parameter>();
                var model = Repository<TbWorkOrderPack>.FromSqlToPageTable(sql + where, parameter, request.rows, request.page, "PackCode", "asc");
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取未签收的订单明细信息列表（分页）
        /// </summary>
        public PageModel GetNoSignForOrderDetail(WorkOrderDetailRequest request)
        {
            //组装查询语句
            #region 模糊搜索条件

            string where = " where 1=1 and a.IsPack='否' and a.IsSignFor='否'";
            if (!string.IsNullOrWhiteSpace(request.OrderCode))
            {
                where += " and a.OrderCode='" + request.OrderCode + "'";
            }

            #endregion

            try
            {

                string sql = @"select a.ID,c.CompanyFullName as SiteName,a.OrderCode,a.SystemType,a.MaterialType,a.ComponentCode,a.ComponentName,a.SpecificationModel,a.Length from TbWorkOrderDetail a
left join TbWorkOrder b on a.OrderCode=b.OrderCode
left join TbCompany c on b.SiteCode=c.CompanyCode ";
                //参数化
                List<Parameter> parameter = new List<Parameter>();
                var model = Repository<TbWorkOrderDetail>.FromSqlToPageTable(sql + where, parameter, request.rows, request.page, "ComponentCode", "asc");
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 新增订单签收信息数据
        /// </summary>
        public AjaxResult OrderSignForInsert(string OrderCode, List<TbWorkOrderSignFor> items)
        {
            if (string.IsNullOrWhiteSpace(OrderCode) || items.Count <= 0)
                return AjaxResult.Warning("参数错误");
            var orderModel = Repository<TbWorkOrder>.First(p => p.OrderCode == OrderCode);
            var orderItemList = Repository<TbWorkOrderDetail>.Query(p => p.OrderCode == OrderCode);
            var packOrderList = Repository<TbWorkOrderPack>.Query(p => p.OrderCode == OrderCode);
            var packDetialList = Repository<TbPackDetail>.Query(p => p.OrderCode == OrderCode);
            List<TbWorkOrderDetail> orderDetailList = new List<TbWorkOrderDetail>();
            List<TbPackDetail> packDetialModelList = new List<TbPackDetail>();
            List<TbSignForDetail> signForDetailList = new List<TbSignForDetail>();
            //定义一个List集合存放签收订单明细id
            List<int> orderDetialIdList = new List<int>();
            for (int i = 0; i < items.Count; i++)
            {
                string SignCode = CreateCode.GetNoNew2("SignForCode", "TbWorkOrderSignFor");
                long NewCode = Convert.ToInt64(SignCode) + i;
                items[i].SignForCode = NewCode.ToString();
                items[i].SignForUserCode = OperatorProvider.Provider.CurrentUser.UserCode;
                var itemId = items[i].OrderDetialId;
                var itemIdStr = itemId.Split(',');
                if (itemIdStr.Length > 0)
                {
                    for (int j = 0; j < itemIdStr.Length; j++)
                    {
                        int orderId = Convert.ToInt32(itemIdStr[j]);
                        if (!orderDetialIdList.Contains(orderId))
                        {
                            orderDetialIdList.Add(orderId);
                        }
                        //新增签收明细信息
                        TbSignForDetail signDetailModel = new TbSignForDetail();
                        signDetailModel.OrderCode = items[i].OrderCode;
                        signDetailModel.PackCode = items[i].PackCode;
                        signDetailModel.SignFoeCode = items[i].SignForCode;
                        signDetailModel.OrderDetialId = orderId;
                        signDetailModel.IsInstall = "否";
                        signForDetailList.Add(signDetailModel);
                        //修改订单明细表信息中的签收信息
                        var orderDetailNew = orderItemList.Find(p => p.ID == orderId && p.IsSignFor == "否");
                        if (orderDetailNew != null)
                        {
                            orderDetailNew.IsSignFor = "是";
                            orderDetailNew.SignForDate = items[i].SignForDate;
                            orderDetailNew.SignForUserCode = items[i].SignForUserCode;
                            //判断订单是否加工完成
                            if (orderDetailNew.ComponentStrat == "加工中")
                            {
                                orderDetailNew.ComponentStrat = "加工完成";
                            }
                            orderDetailList.Add(orderDetailNew);
                        }
                        //修改打包明细中的签收信息
                        var packDetialModel = packDetialList.Find(p => p.OrderDetialId == itemIdStr[j] && p.IsSignFor == "否");
                        if (packDetialModel != null)
                        {
                            packDetialModel.IsSignFor = "是";
                            packDetialModelList.Add(packDetialModel);
                        }
                    }
                }
            }

            if (orderDetialIdList.Count == orderItemList.Count)
            {
                #region 订单主表中的状态

                orderModel.SignForState = "已签收";
                //判断订单是否加工完成
                if (string.IsNullOrWhiteSpace(orderModel.JgProgress))
                {
                    orderModel.OrderState = "加工完成";
                    orderModel.JgCompleteTiem = DateTime.Now;
                    string dt1 = Convert.ToDateTime(orderModel.DistributionTime).ToShortDateString();
                    string dt2 = DateTime.Now.ToShortDateString();
                    if (DateTime.Parse(dt1) == DateTime.Parse(dt2))
                    {
                        orderModel.JgProgress = "正常";
                    }
                    else if (DateTime.Parse(dt1) > DateTime.Parse(dt2))
                    {
                        orderModel.JgProgress = "提前";
                    }
                    else
                    {
                        orderModel.JgProgress = "延迟";
                    }
                }

                #endregion
            }
            else
            {
                orderModel.SignForState = "部分签收";
                var orderDetailNewList = orderItemList.Where(p => p.IsSignFor == "是").ToList();
                for (int i = 0; i < orderDetailNewList.Count; i++)
                {
                    if (!orderDetialIdList.Contains(orderDetailNewList[i].ID))
                    {
                        #region 修改订单明细

                        var orderDetailNew = orderItemList.Find(p => p.ID == orderDetailNewList[i].ID);
                        if (orderDetailNew != null)
                        {
                            orderDetailNew.IsSignFor = "否";
                            orderDetailNew.SignForDate = null;
                            orderDetailNew.SignForUserCode = null;
                            //判断订单如果是加工完成，就应该把订单的加工状态还原为加工中
                            if (orderDetailNew.ComponentStrat == "加工完成" && orderDetailNew.IsPack == "否")
                            {
                                orderDetailNew.ComponentStrat = "加工中";
                            }
                            orderDetailList.Add(orderDetailNew);
                        }

                        #endregion

                        #region 修改包件明细

                        var packDetialModelNew = packDetialList.Find(p => p.OrderDetialId == orderDetailNewList[i].ID.ToString() && p.IsSignFor == "是");
                        if (packDetialModelNew != null)
                        {
                            packDetialModelNew.IsSignFor = "否";
                            packDetialModelList.Add(packDetialModelNew);
                        }

                        #endregion
                    }
                }
            }

            using (DbTrans trans = Db.Context.BeginTransaction())
            {
                try
                {
                    if (orderModel != null)
                        //修改订单主表中的订单签收状态
                        Repository<TbWorkOrder>.Update(trans, orderModel);
                    if (orderDetailList.Count > 0)
                        //修改订单明细表中是否签收信息
                        Repository<TbWorkOrderDetail>.Update(trans, orderDetailList);
                    if (packDetialModelList.Count > 0)
                        //修改订单打包信息中的签收信息
                        Repository<TbPackDetail>.Update(trans, packDetialModelList);
                    //先删除原来的签收主表、明细表信息  
                    Repository<TbWorkOrderSignFor>.Delete(trans, p => p.OrderCode == OrderCode);
                    Repository<TbSignForDetail>.Delete(trans, p => p.OrderCode == OrderCode);
                    //添加签收主表、明细表信息  
                    Repository<TbWorkOrderSignFor>.Insert(trans, items);
                    if (signForDetailList.Count > 0)
                        Repository<TbSignForDetail>.Insert(trans, signForDetailList);
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

        public Tuple<DataTable> GetModelGjInfo(string mxgjid)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(mxgjid))
            {
                string[] str = mxgjid.Split(',');
                if (str.Length > 0)
                {
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (i == str.Length - 1)
                        {
                            sb.Append("'" + str[i] + "'");
                        }
                        else
                        {
                            sb.Append("'" + str[i] + "',");
                        }
                    }
                }
            }
            string sql = @"select a.ID,c.CompanyFullName as SiteName,case when a.PackCode is not null then a.PackCode else '/' end PackCode,a.OrderCode,a.SystemType,a.MaterialType,a.ComponentName,a.ID as OrderDetialId from TbWorkOrderDetail a
left join TbWorkOrder b on a.OrderCode=b.OrderCode
left join TbCompany c on b.SiteCode=c.CompanyCode
 where MxGjId in(" + sb.ToString() + ") ";
            DataTable dt = Db.Context.FromSql(sql).ToDataTable();
            return new Tuple<DataTable>(dt);
        }

        /// <summary>
        /// 新增模型构件签收信息数据
        /// </summary>
        public AjaxResult ModelSignForInsert(List<TbWorkOrderSignFor> items)
        {
            if (items.Count <= 0)
                return AjaxResult.Warning("参数错误");
            //定义一个集合存放订单编号
            List<string> orderCodeList = new List<string>();
            List<TbWorkOrder> orderModelList = new List<TbWorkOrder>();
            List<TbWorkOrderDetail> orderDetialModelList = new List<TbWorkOrderDetail>();
            List<TbPackDetail> orderPackDetialModelList = new List<TbPackDetail>();
            List<TbSignForDetail> signForDetialModelList = new List<TbSignForDetail>();
            for (int i = 0; i < items.Count; i++)
            {
                string SignCode = CreateCode.GetNoNew2("SignForCode", "TbWorkOrderSignFor");
                long NewCode = Convert.ToInt64(SignCode) + i;
                items[i].SignForUserCode = OperatorProvider.Provider.CurrentUser.UserCode;
                items[i].SignForCode = NewCode.ToString();
                if (orderCodeList.Contains(items[i].OrderCode) == false)
                {
                    orderCodeList.Add(items[i].OrderCode);
                }
            }
            //查询签收的所有订单
            var orderList = Repository<TbWorkOrder>.Query(p => p.OrderCode.In(orderCodeList));
            //查询签收的所有订单明细
            var orderItemList = Repository<TbWorkOrderDetail>.Query(p => p.OrderCode.In(orderCodeList));
            //查询签收的所有订单明细
            var packItemList = Repository<TbPackDetail>.Query(p => p.OrderCode.In(orderCodeList));
            //查询签收的所有订单明细
            var signForItemList = Repository<TbSignForDetail>.Query(p => p.OrderCode.In(orderCodeList));
            for (int i = 0; i < orderList.Count; i++)
            {
                var orderModel = orderList.Find(p => p.OrderCode == orderList[i].OrderCode);
                var orderItemList1 = orderItemList.Where(p => p.OrderCode == orderList[i].OrderCode).ToList();
                var orderPackDetialList = packItemList.Where(p => p.OrderCode == orderList[i].OrderCode).ToList();
                var signForItemList1 = signForItemList.Where(p => p.OrderCode == orderList[i].OrderCode).ToList();
                var items1 = items.Where(p => p.OrderCode == orderList[i].OrderCode).ToList();
                for (int j = 0; j < items1.Count; j++)
                {
                    string mxid = items1[j].OrderDetialId;
                    string[] str = mxid.Split(',');
                    var orderPackDetialList1 = orderPackDetialList.Where(p => p.PackCode == items1[j].PackCode).ToList();
                    for (int s = 0; s < str.Length; s++)
                    {
                        int orderId = Convert.ToInt32(str[s]);

                        #region 修改订单明细表中的签收信息
                        //获取此条订单中本次签收的订单明细信息
                        var thisSignForOrderItem = orderItemList1.Find(p => p.ID == orderId && p.IsSignFor == "否");
                        if (thisSignForOrderItem != null)
                        {
                            thisSignForOrderItem.IsSignFor = "是";
                            thisSignForOrderItem.SignForDate = items1[j].SignForDate;
                            thisSignForOrderItem.SignForUserCode = items1[j].SignForUserCode;
                            //判断订单是否加工完成
                            if (thisSignForOrderItem.ComponentStrat == "加工中")
                            {
                                thisSignForOrderItem.ComponentStrat = "加工完成";
                            }
                            orderDetialModelList.Add(thisSignForOrderItem);
                        }
                        #endregion

                        #region 修改订单打包明细中的签收状态

                        var orderPackDetialList2 = orderPackDetialList1.Find(p => p.OrderDetialId == str[s] && p.IsSignFor == "否");
                        if (orderPackDetialList2 != null)
                        {
                            orderPackDetialList2.IsSignFor = "是";
                            orderPackDetialModelList.Add(orderPackDetialList2);
                        }

                        #endregion

                        #region 新增签收明细

                        //新增签收明细信息
                        TbSignForDetail signDetailModel = new TbSignForDetail();
                        signDetailModel.OrderCode = items1[j].OrderCode;
                        signDetailModel.PackCode = items1[j].PackCode;
                        signDetailModel.OrderDetialId = orderId;
                        signDetailModel.SignFoeCode = items1[j].SignForCode;
                        signDetailModel.IsInstall = "否";
                        signForDetialModelList.Add(signDetailModel);

                        #endregion
                    }
                }

                #region 修改订单主表中的签收状态信息表

                //判断此订单是否全部签收(此订单已经签收了的数量=此订单所有明细数量)
                int signForNum = orderItemList1.Where(p => p.IsSignFor == "是").Count();
                if (orderItemList1.Count == signForNum)
                {
                    orderModel.SignForState = "已签收";
                    //判断订单是否加工完成
                    if (string.IsNullOrWhiteSpace(orderModel.JgProgress))
                    {
                        orderModel.OrderState = "加工完成";
                        orderModel.JgCompleteTiem = DateTime.Now;
                        string dt1 = Convert.ToDateTime(orderModel.DistributionTime).ToShortDateString();
                        string dt2 = DateTime.Now.ToShortDateString();
                        if (DateTime.Parse(dt1) == DateTime.Parse(dt2))
                        {
                            orderModel.JgProgress = "正常";
                        }
                        else if (DateTime.Parse(dt1) > DateTime.Parse(dt2))
                        {
                            orderModel.JgProgress = "提前";
                        }
                        else
                        {
                            orderModel.JgProgress = "延迟";
                        }
                    }
                    orderModelList.Add(orderModel);
                }
                else
                {
                    orderModel.SignForState = "部分签收";
                    orderModelList.Add(orderModel);
                }

                #endregion

            }
            using (DbTrans trans = Db.Context.BeginTransaction())
            {
                try
                {
                    //新增签收信息
                    Repository<TbWorkOrderSignFor>.Insert(trans, items);
                    //新增签收明细信息
                    if (signForDetialModelList.Count > 0)
                        Repository<TbSignForDetail>.Insert(trans, signForDetialModelList);
                    //修改订单主表中的订单签收状态
                    if (orderModelList.Count > 0)
                        Repository<TbWorkOrder>.Update(trans, orderModelList);
                    //修改订单明细中的构件签收状态
                    if (orderDetialModelList.Count > 0)
                        Repository<TbWorkOrderDetail>.Update(trans, orderDetialModelList);
                    //修改包件明细中构件的签收状态
                    if (orderPackDetialModelList.Count > 0)
                        Repository<TbPackDetail>.Update(trans, orderPackDetialModelList);
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

        #endregion

        #region 订单安装信息

        /// <summary>
        /// 获取订单安装信息列表（分页）
        /// </summary>
        public PageModel GetOrderInstallGridJson(WorkOrderRequest request)
        {
            //组装查询语句
            #region 模糊搜索条件

            string where = " where 1=1 ";
            if (!string.IsNullOrWhiteSpace(request.InstallType))
            {
                //通过模型签收
                if (request.InstallType == "ModelSignFor")
                {
                    where += " and a.OrderCode=''";
                }
                else//通过订单签收
                {
                    if (!string.IsNullOrWhiteSpace(request.OrderCode))
                    {
                        where += " and a.OrderCode='" + request.OrderCode + "'";
                    }
                }
            }

            #endregion

            try
            {
                string sql = @"select a.*,c.CompanyFullName as SiteName from TbWorkOrderInstall a
left join TbWorkOrder b on a.OrderCode=b.OrderCode
left join TbCompany c on b.SiteCode=c.CompanyCode ";
                //参数化
                List<Parameter> parameter = new List<Parameter>();
                var model = Repository<TbWorkOrderSignFor>.FromSqlToPageTable(sql + where, parameter, request.rows, request.page, "ID", "asc");
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取未安装的打包信息列表（分页）
        /// </summary>
        public PageModel GetNoInstallPackInfo(WorkOrderDetailRequest request)
        {
            //组装查询语句
            #region 模糊搜索条件

            string where = " where 1=1 and ISNULL(d.ThisPackNoSignForNumber,0)>0";
            if (!string.IsNullOrWhiteSpace(request.OrderCode))
            {
                where += " and a.OrderCode='" + request.OrderCode + "'";
            }

            #endregion

            try
            {

                string sql = @"select a.ID,c.CompanyFullName as SiteName,a.PackCode,a.OrderCode,case when ISNULL(e.NoSignForCount,0)>0 then '否' else '是' end IsSignFor,a.SystemType,a.MaterialType,a.ComponentName,ISNULL(d.ThisPackNoSignForNumber,0) as ThisPackNumber,d.OrderDetialId,a.PackDate,a.Remark from TbWorkOrderPack a
left join (SELECT COUNT(1) as ThisPackNoSignForNumber,PackCode,OrderDetialId=(STUFF(( SELECT ',' + OrderDetialId FROM TbPackDetail WHERE PackCode = Test.PackCode and IsInstall='否' FOR XML PATH('')), 1, 1, '') )
FROM TbPackDetail AS Test where IsInstall='否' GROUP BY PackCode) d on a.PackCode=d.PackCode
left join (select COUNT(1) as NoSignForCount,PackCode from TbPackDetail where IsSignFor='否' group by PackCode) e on a.PackCode=e.PackCode
left join TbWorkOrder b on a.OrderCode=b.OrderCode
left join TbCompany c on b.SiteCode=c.CompanyCode ";
                //参数化
                List<Parameter> parameter = new List<Parameter>();
                var model = Repository<TbWorkOrderPack>.FromSqlToPageTable(sql + where, parameter, request.rows, request.page, "PackCode", "asc");
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取未安装的订单明细信息列表（分页）
        /// </summary>
        public PageModel GetNoInstallOrderDetail(WorkOrderDetailRequest request)
        {
            //组装查询语句
            #region 模糊搜索条件

            string where = " where 1=1 and a.IsPack='否' and a.IsInstall='否'";
            if (!string.IsNullOrWhiteSpace(request.OrderCode))
            {
                where += " and a.OrderCode='" + request.OrderCode + "'";
            }

            #endregion

            try
            {

                string sql = @"select a.ID,c.CompanyFullName as SiteName,a.OrderCode,a.SystemType,a.MaterialType,a.ComponentCode,a.ComponentName,a.SpecificationModel,a.Length,a.IsSignFor from TbWorkOrderDetail a
left join TbWorkOrder b on a.OrderCode=b.OrderCode
left join TbCompany c on b.SiteCode=c.CompanyCode ";
                //参数化
                List<Parameter> parameter = new List<Parameter>();
                var model = Repository<TbWorkOrderDetail>.FromSqlToPageTable(sql + where, parameter, request.rows, request.page, "ComponentCode", "asc");
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 新增订单安装信息数据
        /// </summary>
        public AjaxResult OrderInstallInsert(string OrderCode, List<TbWorkOrderInstall> items)
        {
            if (string.IsNullOrWhiteSpace(OrderCode) || items.Count <= 0)
                return AjaxResult.Warning("参数错误");
            //获取订单主表信息
            var orderModel = Repository<TbWorkOrder>.First(p => p.OrderCode == OrderCode);
            //获取订单详细信息
            var orderItemList = Repository<TbWorkOrderDetail>.Query(p => p.OrderCode == OrderCode);
            //获取订单详细信息
            var packDetailList = Repository<TbPackDetail>.Query(p => p.OrderCode == OrderCode);
            //获取订单签收信息
            var signForOrderList = Repository<TbWorkOrderSignFor>.Query(p => p.OrderCode == OrderCode);
            //获取订单签收信息
            var signForDetailList = Repository<TbSignForDetail>.Query(p => p.OrderCode == OrderCode);
            List<TbWorkOrderDetail> orderDetailModelList = new List<TbWorkOrderDetail>();
            List<TbPackDetail> packDetailModelList = new List<TbPackDetail>();
            List<TbWorkOrderSignFor> orderSignForAddModelList = new List<TbWorkOrderSignFor>();
            List<TbSignForDetail> orderSignForDetailAddModelList = new List<TbSignForDetail>();
            List<TbSignForDetail> orderSignForDetailUpdateModelList = new List<TbSignForDetail>();
            //定义一个List集合存放安装的订单明细id
            List<int> orderDetailIdList = new List<int>();
            for (int i = 0; i < items.Count; i++)
            {
                items[i].InstallUserCode = OperatorProvider.Provider.CurrentUser.UserCode;
                var itemId = items[i].OrderDetialId;
                var itemIdStr = itemId.Split(',');
                if (itemIdStr.Length > 0)
                {
                    string SignCode = CreateCode.GetNoNew2("SignForCode", "TbWorkOrderSignFor");
                    long NewCode = Convert.ToInt64(SignCode) + i;
                    int qsCount = 0;
                    for (int j = 0; j < itemIdStr.Length; j++)
                    {
                        int orderId = Convert.ToInt32(itemIdStr[j]);
                        if (!orderDetailIdList.Contains(orderId))
                        {
                            orderDetailIdList.Add(orderId);
                        }
                        //修改加工订单明细中的签收、安装信息
                        var orderDetailNew = orderItemList.Find(p => p.ID == orderId && p.IsInstall == "否");
                        if (orderDetailNew != null)
                        {
                            orderDetailNew.IsInstall = "是";
                            orderDetailNew.InstallDate = items[i].InstallDate;
                            orderDetailNew.InstallUserCode = items[i].InstallUserCode;
                            //判断订单明细是否签收
                            if (orderDetailNew.IsSignFor == "否")
                            {
                                orderDetailNew.IsSignFor = "是";
                                orderDetailNew.SignForDate = items[i].InstallDate;
                                orderDetailNew.SignForUserCode = items[i].InstallUserCode;
                            }
                            //判断订单是否加工完成
                            if (orderDetailNew.ComponentStrat == "加工中")
                            {
                                orderDetailNew.ComponentStrat = "加工完成";
                            }
                            orderDetailModelList.Add(orderDetailNew);
                        }
                        //修改打包明细中的签收、安装信息
                        var packDetialModel = packDetailList.Find(p => p.OrderDetialId == itemIdStr[j] && p.IsInstall == "否");
                        if (packDetialModel != null)
                        {
                            packDetialModel.IsInstall = "是";
                            if (packDetialModel.IsSignFor == "否")
                            {
                                packDetialModel.IsSignFor = "是";
                            }
                            packDetailModelList.Add(packDetialModel);
                        }
                        //修改签收明细中的安装状态
                        var signForDetailModel = signForDetailList.Find(p => p.OrderDetialId == orderId);
                        if (signForDetailModel != null)
                        {
                            if (signForDetailModel.IsInstall == "否")
                            {
                                signForDetailModel.IsInstall = "是";
                                orderSignForDetailUpdateModelList.Add(signForDetailModel);
                            }
                        }
                        else
                        {
                            //新增签收明细信息
                            TbSignForDetail signDetailModel = new TbSignForDetail();
                            signDetailModel.OrderCode = items[i].OrderCode;
                            signDetailModel.PackCode = items[i].PackCode;
                            signDetailModel.OrderDetialId = orderId;
                            signDetailModel.SignFoeCode = NewCode.ToString();
                            signDetailModel.IsInstall = "是";
                            orderSignForDetailAddModelList.Add(signDetailModel);
                            qsCount++;
                        }
                    }
                    //如果安装的构件还没有签收，就直接生成签收信息
                    if (qsCount > 0)
                    {
                        TbWorkOrderSignFor signForModel = new TbWorkOrderSignFor();
                        signForModel.SignForCode = NewCode.ToString();
                        signForModel.OrderCode = items[i].OrderCode;
                        signForModel.PackCode = items[i].PackCode;
                        signForModel.SystemType = items[i].SystemType;
                        signForModel.MaterialType = items[i].MaterialType;
                        signForModel.ComponentName = items[i].ComponentName;
                        signForModel.OrderDetialId = items[i].OrderDetialId;
                        signForModel.SignForNumber = qsCount;
                        signForModel.SignForDate = items[i].InstallDate;
                        signForModel.Remark = "通过安装自动生成的签收记录";
                        signForModel.SignForUserCode = items[i].InstallUserCode;
                        orderSignForAddModelList.Add(signForModel);
                    }
                }
            }
            if (orderDetailIdList.Count == orderItemList.Count)
            {
                #region 订单主表中的状态

                orderModel.SignForState = "已签收";
                orderModel.InstallState = "已安装";
                //判断订单是否加工完成
                if (string.IsNullOrWhiteSpace(orderModel.JgProgress))
                {
                    orderModel.OrderState = "加工完成";
                    orderModel.JgCompleteTiem = DateTime.Now;
                    string dt1 = Convert.ToDateTime(orderModel.DistributionTime).ToShortDateString();
                    string dt2 = DateTime.Now.ToShortDateString();
                    if (DateTime.Parse(dt1) == DateTime.Parse(dt2))
                    {
                        orderModel.JgProgress = "正常";
                    }
                    else if (DateTime.Parse(dt1) > DateTime.Parse(dt2))
                    {
                        orderModel.JgProgress = "提前";
                    }
                    else
                    {
                        orderModel.JgProgress = "延迟";
                    }
                }

                #endregion
            }
            else
            {
                orderModel.InstallState = "部分安装";
                var yqsCount = orderItemList.Where(p => p.IsSignFor == "是").Count();
                if (yqsCount == orderItemList.Count)
                {
                    orderModel.SignForState = "全部签收";
                }
                else
                {
                    orderModel.SignForState = "部分签收";
                }
                var orderDetailNewList = orderItemList.Where(p => p.IsInstall == "是").ToList();
                for (int i = 0; i < orderDetailNewList.Count; i++)
                {
                    if (!orderDetailIdList.Contains(orderDetailNewList[i].ID))
                    {
                        #region 修改订单明细

                        var orderDetailNew = orderItemList.Find(p => p.ID == orderDetailNewList[i].ID);
                        if (orderDetailNew != null)
                        {
                            orderDetailNew.IsInstall = "否";
                            orderDetailNew.InstallDate = null;
                            orderDetailNew.InstallUserCode = null;
                            //判断订单如果是加工完成，就应该把订单的加工状态还原为加工中
                            if (orderDetailNew.ComponentStrat == "加工完成" && orderDetailNew.IsPack == "否" && orderDetailNew.IsSignFor == "否")
                            {
                                orderDetailNew.ComponentStrat = "加工中";
                            }
                            orderDetailModelList.Add(orderDetailNew);
                        }

                        #endregion

                        #region 修改包件明细

                        var packDetialModelNew = packDetailList.Find(p => p.OrderDetialId == orderDetailNewList[i].ID.ToString() && p.IsInstall == "是");
                        if (packDetialModelNew != null)
                        {
                            packDetialModelNew.IsInstall = "否";
                            packDetailModelList.Add(packDetialModelNew);
                        }

                        #endregion

                        #region 修改签收明细

                        var signForDetailModelNew = signForDetailList.Find(p => p.OrderDetialId == orderDetailNewList[i].ID && p.IsInstall == "是");
                        if (signForDetailModelNew != null)
                        {
                            signForDetailModelNew.IsInstall = "否";
                            orderSignForDetailUpdateModelList.Add(signForDetailModelNew);
                        }

                        #endregion
                    }
                }
            }
            using (DbTrans trans = Db.Context.BeginTransaction())
            {
                try
                {
                    if (orderModel != null)
                        //修改订单主表中的订单签收状态
                        Repository<TbWorkOrder>.Update(trans, orderModel);
                    if (orderDetailModelList.Count > 0)
                        //修改订单明细表中是否签收、安装信息
                        Repository<TbWorkOrderDetail>.Update(trans, orderDetailModelList);
                    if (packDetailModelList.Count > 0)
                        //修改包件明细表中是否签收、安装信息
                        Repository<TbPackDetail>.Update(trans, packDetailModelList);
                    if (orderSignForAddModelList.Count > 0)
                        //新增签收信息主表
                        Repository<TbWorkOrderSignFor>.Insert(trans, orderSignForAddModelList);
                    if (orderSignForDetailAddModelList.Count > 0)
                        //新增签收信息明细表
                        Repository<TbSignForDetail>.Insert(trans, orderSignForDetailAddModelList);
                    if (orderSignForDetailUpdateModelList.Count > 0)
                        //修改签收信息明细表
                        Repository<TbSignForDetail>.Update(trans, orderSignForDetailUpdateModelList);
                    //先删除原来的订单打包信息  
                    Repository<TbWorkOrderInstall>.Delete(trans, p => p.OrderCode == OrderCode);
                    //添加信息及明细信息
                    Repository<TbWorkOrderInstall>.Insert(trans, items);
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
        /// 
        /// </summary>
        /// <param name="items">新增构件安装信息数据</param>
        /// <returns></returns>
        public AjaxResult ModelInstallInsert(List<TbWorkOrderInstall> items)
        {
            if (items.Count <= 0)
                return AjaxResult.Warning("参数错误");
            //定义一个集合存放订单编号
            List<string> orderCodeList = new List<string>();
            List<TbWorkOrder> orderModelList = new List<TbWorkOrder>();
            List<TbWorkOrderDetail> orderDetialModelList = new List<TbWorkOrderDetail>();
            List<TbPackDetail> orderPackDetialModelList = new List<TbPackDetail>();
            List<TbWorkOrderSignFor> signForModelList = new List<TbWorkOrderSignFor>();
            List<TbSignForDetail> signForDetialModelUpdateList = new List<TbSignForDetail>();
            List<TbSignForDetail> orderSignForDetailAddModelList = new List<TbSignForDetail>();
            List<int> orderDetailIdList = new List<int>();
            for (int i = 0; i < items.Count; i++)
            {
                items[i].InstallUserCode = OperatorProvider.Provider.CurrentUser.UserCode;
                if (orderCodeList.Contains(items[i].OrderCode) == false)
                {
                    orderCodeList.Add(items[i].OrderCode);
                }
            }
            //查询签收的所有订单
            var orderList = Repository<TbWorkOrder>.Query(p => p.OrderCode.In(orderCodeList));
            //查询签收的所有订单明细
            var orderItemList = Repository<TbWorkOrderDetail>.Query(p => p.OrderCode.In(orderCodeList));
            //查询签收的所有订单明细
            var packItemList = Repository<TbPackDetail>.Query(p => p.OrderCode.In(orderCodeList));
            //查询签收的所有订单明细
            var signForItemList = Repository<TbSignForDetail>.Query(p => p.OrderCode.In(orderCodeList));
            for (int i = 0; i < orderList.Count; i++)
            {
                var orderModel = orderList.Find(p => p.OrderCode == orderList[i].OrderCode);
                var orderItemList1 = orderItemList.Where(p => p.OrderCode == orderList[i].OrderCode).ToList();
                var orderPackDetialList = packItemList.Where(p => p.OrderCode == orderList[i].OrderCode).ToList();
                var signForItemList1 = signForItemList.Where(p => p.OrderCode == orderList[i].OrderCode).ToList();
                var items1 = items.Where(p => p.OrderCode == orderList[i].OrderCode).ToList();
                for (int j = 0; j < items1.Count; j++)
                {
                    int xzqscount = 0;//存放签收明细的条数
                    string SignCode = CreateCode.GetNoNew2("SignForCode", "TbWorkOrderSignFor");
                    long NewCode = Convert.ToInt64(SignCode) + i;
                    string mxid = items1[j].OrderDetialId;
                    string[] str = mxid.Split(',');
                    var orderPackDetialList1 = orderPackDetialList.Where(p => p.PackCode == items1[j].PackCode).ToList();
                    for (int s = 0; s < str.Length; s++)
                    {
                        int orderId = Convert.ToInt32(str[s]);
                        if (!orderDetailIdList.Contains(orderId))
                        {
                            orderDetailIdList.Add(orderId);
                        }
                        #region 修改订单明细表中的签收、安装信息
                        //获取此条订单中本次签收的订单明细信息
                        var thisSignForOrderItem = orderItemList1.Find(p => p.ID == orderId && p.IsInstall == "否");
                        if (thisSignForOrderItem != null)
                        {
                            thisSignForOrderItem.IsInstall = "是";
                            thisSignForOrderItem.InstallDate = items1[j].InstallDate;
                            thisSignForOrderItem.InstallUserCode = items1[j].InstallUserCode;
                            //判断订单是否加工完成
                            if (thisSignForOrderItem.ComponentStrat == "加工中")
                            {
                                thisSignForOrderItem.ComponentStrat = "加工完成";
                            }
                            if (thisSignForOrderItem.IsSignFor == "否")
                            {
                                thisSignForOrderItem.IsSignFor = "是";
                                thisSignForOrderItem.SignForDate = items1[j].InstallDate;
                                thisSignForOrderItem.SignForUserCode = items1[j].InstallUserCode;
                            }
                            orderDetialModelList.Add(thisSignForOrderItem);
                        }
                        #endregion

                        //修改打包明细中的签收、安装信息
                        var packDetialModel = orderPackDetialList1.Find(p => p.OrderDetialId == str[s] && p.IsInstall == "否");
                        if (packDetialModel != null)
                        {
                            packDetialModel.IsInstall = "是";
                            if (packDetialModel.IsSignFor == "否")
                            {
                                packDetialModel.IsSignFor = "是";
                            }
                            orderPackDetialModelList.Add(packDetialModel);
                        }
                        //修改签收明细中的安装状态
                        var signForDetailModel = signForItemList1.Find(p => p.OrderDetialId == orderId);
                        if (signForDetailModel != null)
                        {
                            if (signForDetailModel.IsInstall == "否")
                            {
                                signForDetailModel.IsInstall = "是";
                                signForDetialModelUpdateList.Add(signForDetailModel);
                            }
                        }
                        else
                        {
                            //新增签收明细信息
                            TbSignForDetail signDetailModel = new TbSignForDetail();
                            signDetailModel.OrderCode = items1[j].OrderCode;
                            signDetailModel.PackCode = items1[j].PackCode;
                            signDetailModel.OrderDetialId = orderId;
                            signDetailModel.SignFoeCode = NewCode.ToString();
                            signDetailModel.IsInstall = "是";
                            orderSignForDetailAddModelList.Add(signDetailModel);
                            xzqscount++;
                        }
                    }
                    //如果安装的构件还没有签收，就直接生成签收信息
                    if (xzqscount > 0)
                    {
                        TbWorkOrderSignFor signForModel = new TbWorkOrderSignFor();
                        signForModel.SignForCode = NewCode.ToString();
                        signForModel.OrderCode = items1[j].OrderCode;
                        signForModel.PackCode = items1[j].PackCode;
                        signForModel.SystemType = items1[j].SystemType;
                        signForModel.MaterialType = items1[j].MaterialType;
                        signForModel.ComponentName = items1[j].ComponentName;
                        signForModel.OrderDetialId = items1[j].OrderDetialId;
                        signForModel.SignForNumber = Convert.ToInt32(xzqscount);
                        signForModel.SignForDate = items1[j].InstallDate;
                        signForModel.Remark = "通过安装自动生成的签收记录";
                        signForModel.SignForUserCode = items1[j].InstallUserCode;
                        signForModelList.Add(signForModel);
                    }
                }
                var orderItemList2 = orderItemList1.Where(p =>p.IsInstall=="是").ToList();
                if (orderItemList2.Count == orderItemList1.Count)
                {
                    #region 订单主表中的状态

                    orderModel.SignForState = "已签收";
                    orderModel.InstallState = "已安装";
                    //判断订单是否加工完成
                    if (string.IsNullOrWhiteSpace(orderModel.JgProgress))
                    {
                        orderModel.OrderState = "加工完成";
                        orderModel.JgCompleteTiem = DateTime.Now;
                        string dt1 = Convert.ToDateTime(orderModel.DistributionTime).ToShortDateString();
                        string dt2 = DateTime.Now.ToShortDateString();
                        if (DateTime.Parse(dt1) == DateTime.Parse(dt2))
                        {
                            orderModel.JgProgress = "正常";
                        }
                        else if (DateTime.Parse(dt1) > DateTime.Parse(dt2))
                        {
                            orderModel.JgProgress = "提前";
                        }
                        else
                        {
                            orderModel.JgProgress = "延迟";
                        }
                    }
                    orderModelList.Add(orderModel);
                    #endregion
                }
                else
                {
                    orderModel.InstallState = "部分安装";
                    var yqsCount = orderItemList1.Where(p => p.IsSignFor == "是").Count();
                    if (yqsCount == orderItemList1.Count)
                    {
                        orderModel.SignForState = "全部签收";
                    }
                    else
                    {
                        orderModel.SignForState = "部分签收";
                    }
                    orderModelList.Add(orderModel);

                    var orderDetailNewList = orderItemList1.Where(p => p.IsInstall == "是").ToList();
                    for (int o = 0; o < orderDetailNewList.Count; o++)
                    {
                        if (!orderDetailIdList.Contains(orderDetailNewList[o].ID))
                        {
                            #region 修改订单明细

                            var orderDetailNew = orderItemList.Find(p => p.ID == orderDetailNewList[o].ID);
                            if (orderDetailNew != null)
                            {
                                orderDetailNew.IsInstall = "否";
                                orderDetailNew.InstallDate = null;
                                orderDetailNew.InstallUserCode = null;
                                //判断订单如果是加工完成，就应该把订单的加工状态还原为加工中
                                if (orderDetailNew.ComponentStrat == "加工完成" && orderDetailNew.IsPack == "否" && orderDetailNew.IsSignFor == "否")
                                {
                                    orderDetailNew.ComponentStrat = "加工中";
                                }
                                orderDetialModelList.Add(orderDetailNew);
                            }

                            #endregion

                            #region 修改包件明细

                            var packDetialModelNew = orderPackDetialList.Find(p => p.OrderDetialId == orderDetailNewList[o].ID.ToString() && p.IsInstall == "是");
                            if (packDetialModelNew != null)
                            {
                                packDetialModelNew.IsInstall = "否";
                                orderPackDetialModelList.Add(packDetialModelNew);
                            }

                            #endregion

                            #region 修改签收明细

                            var signForDetailModelNew = signForItemList1.Find(p => p.OrderDetialId == orderDetailNewList[o].ID && p.IsInstall == "是");
                            if (signForDetailModelNew != null)
                            {
                                signForDetailModelNew.IsInstall = "否";
                                signForDetialModelUpdateList.Add(signForDetailModelNew);
                            }

                            #endregion
                        }
                    }

                }
            }
            using (DbTrans trans = Db.Context.BeginTransaction())
            {
                try
                {
                    if (orderModelList.Count > 0)
                        //修改订单主表中的订单签收状态
                        Repository<TbWorkOrder>.Update(trans, orderModelList);
                    if (orderDetialModelList.Count > 0)
                        //修改订单明细表中是否签收、安装信息
                        Repository<TbWorkOrderDetail>.Update(trans, orderDetialModelList);
                    if (orderPackDetialModelList.Count > 0)
                        //修改包件明细表中是否签收、安装信息
                        Repository<TbPackDetail>.Update(trans, orderPackDetialModelList);
                    if (signForModelList.Count > 0)
                        //新增签收信息主表
                        Repository<TbWorkOrderSignFor>.Insert(trans, signForModelList);
                    if (orderSignForDetailAddModelList.Count > 0)
                        //新增签收信息明细表
                        Repository<TbSignForDetail>.Insert(trans, orderSignForDetailAddModelList);
                    if (signForDetialModelUpdateList.Count > 0)
                        //修改签收信息明细表
                        Repository<TbSignForDetail>.Update(trans, signForDetialModelUpdateList);
                    //添加信息及明细信息
                    Repository<TbWorkOrderInstall>.Insert(trans, items);
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

        #endregion

        #region 获取站点对应模型的数据库地址信息

        public string GetModelDBPath(string SiteCode, string ProjectId)
        {
            string path = "";
            string sql = "select Path from TbModelOrg where ProjectId='" + ProjectId + "' and SiteCode='" + SiteCode + "' and Type=2";
            DataTable dt = Db.Context.FromSql(sql).ToDataTable();
            if (dt.Rows.Count > 0)
            {
                path = dt.Rows[0]["Path"].ToString();
            }
            return path;
        }

        #endregion

        #region 模型改变构件颜色状态接口

        public DataTable GetSiteCodeDetailsState(string SiteCode, string ProjectId)
        {
            string sql = @"select b.id,b.MxGjId,case when b.IsInstall='否' then b.ComponentStrat else '安装完成' end Orderstate from TbWorkOrder a
left join TbWorkOrderDetail b on a.OrderCode=b.OrderCode
where a.Examinestatus='审核完成' and a.ProjectId=@ProjectId and a.SiteCode=@SiteCode";
            DataTable dt = Db.Context.FromSql(sql)
                .AddInParameter("@ProjectId", DbType.String, ProjectId)
                .AddInParameter("@SiteCode", DbType.String, SiteCode)
                .ToDataTable();

            return dt;
        }

        #endregion
    }
}
