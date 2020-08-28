using Dos.ORM;
using PM.Business.Production;
using PM.Common;
using PM.Common.EnumModel;
using PM.Common.Extension;
using PM.DataAccess.DbContext;
using PM.DataEntity;
using PM.DataEntity.BIM;
using PM.DataEntity.System.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace PM.Business.BIM
{
    /// <summary>
    /// 本地模型基础信息
    /// </summary>
    public class ModelPropertyLogIc
    {
        private readonly TbWorkOrderLogic _workOrderLogic = new TbWorkOrderLogic();

        #region 项目清单

        /// <summary>
        /// 获取数据列表(项目清单_分页)
        /// </summary>
        public PageModel GetDataListForPage(BIMRequest request)
        {
            #region 查询语句
            StringBuilder whereSql = new StringBuilder(" where 1=1 and SiteCode=@SiteCode");
            List<Parameter> parameter = new List<Parameter>();
            parameter.Add(new Parameter("@SiteCode", request.SiteCode, DbType.String, null));
            if (!string.IsNullOrEmpty(request.Material))
            {
                whereSql.Append(" and Material=@Material");
                parameter.Add(new Parameter("@Material", request.Material, DbType.String, null));
            }
            List<string> codeList = null;
            if (!request.ComponentCodeList.IsEmpty())
            {
                codeList = request.ComponentCodeList.Split(',').ToList();
                whereSql.Append(" and ( ComponentCode like @ComponentCode");
                parameter.Add(new Parameter("@ComponentCode", codeList[0] + "_%", DbType.String, null));
                for (int i = 1; i < codeList.Count; i++)
                {
                    whereSql.Append(" or ComponentCode like @ComponentCode" + i);
                    parameter.Add(new Parameter("@ComponentCode" + i, codeList[i] + "_%", DbType.String, null));
                }
                whereSql.Append(" )");
            }
            #endregion

            string sql = @"SELECT SiteCode,Major,System,Subsystem,MaterialType,Material,Size,count(*) as Total, MAX(ComponentCode) as ComponentCode,MAX(SystemType) as SystemType
                        from TbModel_Property
                        {0}
                        GROUP BY Major,System,Subsystem,MaterialType,Material,Size,SiteCode ";
            sql = string.Format(sql, whereSql);
            try
            {
                var ret = Repositorys<ProjectListAllModel>.FromSqlToPage(sql, parameter, request.rows, request.page, "Major");
                List<ProjectListAllModel> dataList = ret.rows as List<ProjectListAllModel>;
                request.Type = 1;
                if (codeList != null)
                {
                    codeList = GetSubCode(request);
                    codeList.ForEach(x =>
                    {
                        var items = dataList.Where(p => p.ComponentCode.Contains(x)).ToList();
                        if (items.Any())
                            items.ForEach(i => { i.ComponentCodeP = x; });
                    });
                }
                CreatProjectOtherInfo(dataList, request);
                return ret;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取数据列表(项目清单明细_分页)
        /// </summary>
        public List<ProjectListAllModel> GetDataItemListForPage(BIMRequest request)
        {
            #region 查询语句

            string whereSql = "where 1=1";
            List<Parameter> parameter = new List<Parameter>();
            parameter.Add(new Parameter("@SiteCode", request.SiteCode, DbType.String, null));
            whereSql += " and SiteCode=@SiteCode";
            if (!string.IsNullOrEmpty(request.Material))
            {
                whereSql += " and Material=@Material";
                parameter.Add(new Parameter("@Material", request.Material, DbType.String, null));
            }
            if (!request.Size.IsEmpty())
            {
                whereSql += " and Size=@Size";
                parameter.Add(new Parameter("@Size", request.Size, DbType.String, null));
            }
            else
            {
                whereSql += " and Size=''";
            }
            if (!string.IsNullOrEmpty(request.ComponentCode))
            {
                whereSql += " and ComponentCode like @ComponentCode";
                parameter.Add(new Parameter("@ComponentCode", request.ComponentCode + "_%", DbType.String, null));
            }
            #endregion

            string sql = @"SELECT id,ComponentCode, ComponentName as Name,SystemType,Length,Size,Area,Material,1 as Total 
                           from TbModel_Property
                           {0}";
            sql = string.Format(sql, whereSql);
            try
            {
                var dataList = Repositorys<ProjectListAllModel>.FromSql(sql, parameter);
                if (dataList.Count == 0)
                {
                    request.ComponentCode = request.ComponentCode.Substring(0, request.ComponentCode.Length - 1);
                    return GetDataItemListForPage(request);
                }
                else
                {
                    if (dataList.Count < request.TotalCount)
                    {
                        request.ComponentCode = StringEx.GetCode(request.ComponentCode);
                        return GetDataItemListForPage(request);
                    }
                }
                if (!request.IsWrite)
                {
                    request.Type = 2;
                    CreatProjectOtherInfo(dataList, request);
                }
                return dataList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 单条修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public AjaxResult EditModelOtherInfo(TbModelOtherInfo model)
        {
            if (model == null)
                return AjaxResult.Warning("参数错误");
            try
            {
                var data = Repository<TbModelOtherInfo>.First(p => p.ComponentCode == model.ComponentCode && p.Type == model.Type);
                if (data != null)
                {
                    data.PlanTime = model.PlanTime;
                    Repository<TbModelOtherInfo>.Update(data);
                }
                else
                {
                    if (model.Type == 2) model.ComponentParentCode = model.ComponentCodeShow;
                    Repository<TbModelOtherInfo>.Insert(model);
                }
                if (model.Type == 1)
                {
                    //修改统计最小计划日期
                    UpdataMaxTime(model.ComponentCodeP);
                }
                else
                {
                    //修改父级最小计划时间
                    var pData = Repository<TbModelOtherInfo>.First(p => p.ComponentCode == model.ComponentCodeShow && p.Type == 1);
                    if (pData.PlanTime < model.PlanTimeP)
                    {
                        pData.PlanTime = model.PlanTimeP;
                        Repository<TbModelOtherInfo>.Update(pData);
                        UpdataMaxTime(model.ComponentCodeP);
                    }
                }
                return AjaxResult.Success();
            }
            catch (Exception ex)
            {
                return AjaxResult.Error();
            }
        }
        /// <summary>
        /// 批量修改(明细)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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
                if (lastList.Any())
                {
                    Repository<TbModelOtherInfo>.Insert(lastList);
                }
                return AjaxResult.Success();
            }
            catch (Exception)
            {
                return AjaxResult.Error();
            }
        }
        #endregion

        #region 模型结构树
        /// <summary>
        /// 获取项目结构树
        /// </summary>
        public List<model_tree> Getmodel_tree(BIMRequest request)
        {
            List<Parameter> parameter = new List<Parameter>();
            parameter.Add(new Parameter("@SiteCode", request.SiteCode, DbType.String, null));
            try
            {
                string sql = @"select ComponentCode as id,ComponentCodeP as pid,ComponentName as name,FileName
                               from 
                               TbModelReporte
                               where SiteCode=@SiteCode";
                var list = Repositorys<model_tree>.FromSql(sql, parameter);

                //var report = CreatReportModel(new TbModelOrg { SiteCode = "6373083923489249281", ProjectId = "6372912251695766465"});
                //using (DbTrans trans = Db.Context.BeginTransaction())
                //{
                //    UpdateModelReportData(trans, report.Item1, report.Item2, report.Item3);
                //    trans.Commit();
                //}
                return list;
            }
            catch (Exception ex)
            {
                return new List<model_tree>();
            }
        }

        /// <summary>
        /// 获取显示的模型id
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public List<string> GetModelIdList(BIMRequest request)
        {
            List<string> ret = new List<string>();
            var where = new Where<TbModel_Property>();
            where.And(p => p.ComponentCode.StartsWith(request.ComponentCode));
            try
            {
                ret = Repository<TbModel_Property>.Query(p => p.SiteCode == request.SiteCode && p.ComponentCode.StartsWith(request.ComponentCode)).Select(p => p.ID).ToList();
                return ret;
            }
            catch (Exception ex)
            {
                return new List<string>();
            }
        }
        #endregion

        #region 模型统计信息

        /// <summary>
        /// 获取数据列表(模型统计信息)
        /// </summary>
        public List<ModelReportList> GetModelReportForPage(BIMRequest request, out int count)
        {
            #region 查询语句
            string whereSql = "where 1=1";
            List<Parameter> parameter = new List<Parameter>();
            if (!string.IsNullOrWhiteSpace(request.SiteCode))
            {
                List<string> SiteList = _workOrderLogic.GetCompanyWorkAreaOrSiteList(request.SiteCode, 5);//站点
                string siteStr = string.Join("','", SiteList);
                whereSql += (" and SiteCode in('" + siteStr + "')");
            }
            #endregion

            string sql = @"select tc.CompanyFullName as SiteName,ret.* from (
                            select 
                            SiteCode,
                            isnull(convert(varchar(50),min(PlanTime)),'') as PlanTime,
                            isnull(convert(varchar(50),max(ActualTime)),'') as ActualTime,
                            isnull((select min(InsertTime) from TbWorkOrder where SiteCode=a.SiteCode),'') as BginTime,
                            (select sum(PlanTotal) from TbModelReporte where SiteCode=a.SiteCode and type=3) as PlanTotal1, 
                            (select sum(PlanTotal) from TbModelReporte where SiteCode=a.SiteCode and type=4) as PlanTotal2, 
                            (select sum(PlanTotal) from TbModelReporte where SiteCode=a.SiteCode and type=5) as PlanTotal3, 
                            (select count(*) from TbModelReporte where SiteCode=a.SiteCode and State=1 and type=4) as ProcessingTotal1, 
                            (select count(*) from TbModelReporte where SiteCode=a.SiteCode and State=1 and type=5) as ProcessingTotal2, 
                            (select count(*) from TbWorkOrderDetail where SiteCode=a.SiteCode and ComponentStrat='加工中' and RevokeStart='未撤销') as ProcessingTotal3, 
                            (select count(*) from TbModelReporte where SiteCode=a.SiteCode and State=2 and type=4) as MachinTotal1, 
                            (select count(*) from TbModelReporte where SiteCode=a.SiteCode and State=2 and type=5) as MachinTotal2, 
                            (select count(*) from TbWorkOrderDetail where SiteCode=a.SiteCode and ComponentStrat='加工完成') as MachinTotal3,  
                            (select count(*) from TbModelReporte where SiteCode=a.SiteCode and State=3 and type=4) as InstallTotal1, 
                            (select count(*) from TbModelReporte where SiteCode=a.SiteCode and State=3 and type=5) as InstallTotal2, 
                            (select count(*) from TbWorkOrderDetail where SiteCode=a.SiteCode and IsInstall='是') as InstallTotal3, 
                            (select count(*) from TbModelReporte where SiteCode=a.SiteCode and type=4 and Difference<0) as lag1, 
                            (select count(*) from TbModelReporte where SiteCode=a.SiteCode and type=5 and Difference<0) as lag2, 
                            (select count(*) from TbModelOtherInfo where SiteCode=a.SiteCode and type=2 and Difference<0) as lag3, 
                            (select count(*) from TbModelReporte where SiteCode=a.SiteCode and type=4 and Difference>0) as lea1, 
                            (select count(*) from TbModelReporte where SiteCode=a.SiteCode and type=5 and Difference>0) as lea2, 
                            (select count(*) from TbModelOtherInfo where SiteCode=a.SiteCode and type=2 and Difference>0) as lea3 
                            from TbModelReporte a
                            {0}
                            group by SiteCode
                            ) ret
                            left join TbCompany tc on ret.SiteCode=tc.CompanyCode ";
            sql = string.Format(sql, whereSql);
            request.orderBy = "SiteCode";
            try
            {
                var ret = Repositorys<ModelReportList>.FromSql(sql, parameter, request, out count);
                return ret;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 更新模型统计信息
        /// </summary>
        public void UpdateModelReportData(DbTrans trans, List<TbModelReporte> insertList, List<TbModelReporte> updateList, List<TbModelReporte> delList)
        {
            if (insertList.Any())
                Repository<TbModelReporte>.Insert(trans, insertList);
            if (updateList.Any())
                Repository<TbModelReporte>.Update(trans, updateList);
            if (delList.Any())
                Repository<TbModelReporte>.Delete(trans, delList);
        }

        /// <summary>
        /// 模型统计信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public Tuple<List<TbModelReporte>, List<TbModelReporte>, List<TbModelReporte>> CreatReportModel(TbModelOrg model)
        {
            List<TbModelReporte> dataList = new List<TbModelReporte>();
            var list = CreatModel_tree(model.SiteCode);
            //项目结构树信息
            list.ForEach(x =>
            {
                var item = new TbModelReporte()
                {
                    SiteCode = model.SiteCode,
                    ProjectId = model.ProjectId,
                    FileName = x.FileName,
                    PlanTotal = x.Total,
                    ComponentName = x.name,
                    ComponentCode = x.id,
                    ComponentCodeP = x.pid,
                    Type = x.Type,
                    OpType = 1
                };
                dataList.Add(item);
            });
            var reportList = Repository<TbModelReporte>.Query(p => p.SiteCode == model.SiteCode).ToList();
            if (reportList.Any())
            {
                dataList.ForEach(x =>
                {
                    var item = reportList.First(p => p.Type == x.Type && p.ComponentCode == x.ComponentCode);
                    if (item != null)
                    {
                        item.PlanTotal = x.PlanTotal;
                        item.OpType = 2;
                        x.OpType = 2;
                    }
                    else { x.OpType = 1; }
                });
            }
            var insertList = dataList.Where(p => p.OpType == 1).ToList();
            var updateList = reportList.Where(p => p.OpType == 2).ToList();
            var delList = reportList.Where(p => p.OpType == 0).ToList();
            return new Tuple<List<TbModelReporte>, List<TbModelReporte>, List<TbModelReporte>>(insertList, updateList, delList);
        }

        /// <summary>
        /// 构件标签信息(组织机构)
        /// </summary>
        /// <returns></returns>
        public List<OrgLabData> GetOrgLabInfoList(BIMRequest request)
        {
            #region 查询语句
            string whereSql = "where 1=1";
            List<Parameter> parameter = new List<Parameter>();
            if (!string.IsNullOrWhiteSpace(request.SiteCode))
            {
                List<string> SiteList = _workOrderLogic.GetCompanyWorkAreaOrSiteList(request.SiteCode, 5);//站点
                string siteStr = string.Join("','", SiteList);
                whereSql += (" and SiteCode in('" + siteStr + "')");
            }
            #endregion

            string sql = @" select 
                            SiteCode as Code,
                            (select sum(PlanTotal) from TbModelReporte where SiteCode=a.SiteCode and type=4) as PlanTotal, 
                            (select count(*) from TbModelReporte where SiteCode=a.SiteCode and type=5 and Difference<0) as lag
                            from TbModelReporte a
                            {0}
                            group by SiteCode";
            sql = string.Format(sql, whereSql);
            try
            {
                var retData = new List<OrgLabData>();
                var data = Repositorys<ModelLabelData>.FromSql(sql, parameter);
                if (data.Any())
                {
                    var orgList = data.Select(p => p.Code).Distinct().ToList();
                    orgList.ForEach(x =>
                    {
                        var oList = data.First(p => p.Code == x);
                        var labList = new List<OrgLab>();
                        labList.Add(new OrgLab("项目", oList.PlanTotal, ColorEnum.蓝色.GetValue()));
                        labList.Add(new OrgLab("滞后", oList.lag, ColorEnum.红色.GetValue()));
                        var item = new OrgLabData()
                        {
                            OrgCode = x,
                            LabList = labList
                        };
                        retData.Add(item);
                    });
                }
                return retData;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 构件标签信息(项目结构)
        /// </summary>
        /// <returns></returns>
        public List<OrgLabData> GetComponentLabInfoList(BIMRequest request)
        {
            #region 查询语句
            string whereSql = "where type=5";
            List<Parameter> parameter = new List<Parameter>();
            if (!string.IsNullOrWhiteSpace(request.SiteCode))
            {
                whereSql += " and SiteCode=@SiteCode";
                parameter.Add(new Parameter("@SiteCode", request.SiteCode, DbType.String, null));
            }
            #endregion

            string sql = @" select 
                             ComponentCode as Code,
                            (select PlanTotal from TbModelReporte where ComponentCode=a.ComponentCode) as PlanTotal,
                            (select count(*) from TbWorkOrderDetail where MxGjBm like a.ComponentCode+'_%' and IsInstall='是') as MachinTotal,  
                            (select count(*) from TbModelOtherInfo where ComponentCode like a.ComponentCode+'_%' and type=2 and Difference<0 and IsOver=0) as lag
                            from TbModelReporte a 
                            {0}";
            sql = string.Format(sql, whereSql);
            try
            {
                var retData = new List<OrgLabData>();
                var data = Repositorys<ModelLabelData>.FromSql(sql, parameter);
                if (data.Any())
                {
                    var orgList = data.Select(p => p.Code).Distinct().ToList();
                    orgList.ForEach(x =>
                    {
                        var oList = data.First(p => p.Code == x);
                        var labList = new List<OrgLab>();
                        labList.Add(new OrgLab("总数", oList.PlanTotal, ColorEnum.蓝色.GetValue()));
                        labList.Add(new OrgLab("完工", oList.MachinTotal, ColorEnum.绿色.GetValue()));
                        labList.Add(new OrgLab("滞后未完工", oList.lag, ColorEnum.红色.GetValue()));
                        var item = new OrgLabData()
                        {
                            OrgCode = x,
                            LabList = labList
                        };
                        retData.Add(item);
                    });
                }
                return retData;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #region 统计图信息

        /// <summary>
        /// 项目进度
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ReportRetData GetProjectProgress(BIMRequest request)
        {
            #region 查询语句
            string whereSql = "where 1=1";
            List<Parameter> parameter = new List<Parameter>();
            if (!string.IsNullOrWhiteSpace(request.SiteCode))
            {
                List<string> SiteList = _workOrderLogic.GetCompanyWorkAreaOrSiteList(request.SiteCode, 5);//站点
                string siteStr = string.Join("','", SiteList);
                whereSql += (" and SiteCode in('" + siteStr + "')");
            }
            #endregion

            string sql = @"select count(*) y, '未完成' as name,'{1}' as color from TbModelReporte {0} and type=5 and state!=3 
                           union all
                           select count(*) y, '超期未完成' as name,'{2}' as color from TbModelReporte {0} and type=5 and state!=3 and Difference<0 
                           union all
                           select count(*) y, '超期完成' as name,'{3}' as color from TbModelReporte {0} and type=5 and state=3 and Difference<0 
                           union all
                           select count(*) y, '按期完成' as name,'{4}' as color from TbModelReporte {0} and type=5 and state=3 and Difference>=0 ";

            sql = string.Format(sql, whereSql, ColorEnum.灰色.GetValue(), ColorEnum.红色.GetValue(), ColorEnum.橘黄色.GetValue(), ColorEnum.绿色.GetValue());
            try
            {
                var retData = new ReportRetData();
                var data = Repositorys<ReportData>.FromSql(sql, parameter);
                if (data.Any())
                {
                    retData.Item = data;
                    retData.TotalCount = data.Where(p => p.name != "超期未完成").Sum(p => p.y);
                }
                return retData;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 构件量统计
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ReportRetData GetComponentReport(BIMRequest request)
        {
            #region 查询语句
            string whereSql = "where 1=1";
            List<Parameter> parameter = new List<Parameter>();
            if (!string.IsNullOrWhiteSpace(request.SiteCode))
            {
                List<string> SiteList = _workOrderLogic.GetCompanyWorkAreaOrSiteList(request.SiteCode, 5);//站点
                string siteStr = string.Join("','", SiteList);
                whereSql += (" and SiteCode in('" + siteStr + "')");
            }
            #endregion

            string sql = @" select Sum(y) as y, '未开始' as name,'{1}' as color from (
                            select (Sum(PlanTotal)-(
                            select Count(*) from TbWorkOrderDetail 
                            where ComponentCode=a.ComponentCode and ComponentStrat='加工中' and RevokeStart='未撤销')
                            ) y
                            from TbModelReporte a 
                             {0} and a.type=5 and State=0 group by ComponentCode) ret
                           union all
                           select count(*) y, '加工中' as name,'{2}' as color from TbWorkOrderDetail {0} and ComponentStrat='加工中' and RevokeStart='未撤销'
                           union all
                           select count(*) y, '加工完成' as name,'{3}' as color from TbWorkOrderDetail {0} and ComponentStrat='加工完成' 
                           union all
                           select count(*) y, '安装完成' as name,'{4}' as color from TbWorkOrderDetail {0} and IsInstall='是' ";

            sql = string.Format(sql, whereSql, ColorEnum.灰色.GetValue(), ColorEnum.橘黄色.GetValue(), ColorEnum.蓝色.GetValue(), ColorEnum.绿色.GetValue());
            try
            {
                var retData = new ReportRetData();
                var data = Repositorys<ReportData>.FromSql(sql, parameter);
                if (data.Any())
                {
                    retData.Item = data;
                    retData.TotalCount = data.Where(p => p.name != "安装完成").Sum(p => p.y);
                }
                return retData;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #endregion

        #region Private

        /// <summary>
        /// 获取项目结构树
        /// </summary>
        private List<model_tree> CreatModel_tree(string siteCode)
        {
            List<Parameter> parameter = new List<Parameter>();
            parameter.Add(new Parameter("@SiteCode", siteCode, DbType.String, null));
            try
            {
                string sql = @"select Major,System,Subsystem,MaterialType,Material,MAX(ComponentCode) as ComponentCode,MAX(FileName) as FileName,count(*) as Total
                               from 
                               TbModel_Property
                               where SiteCode=@SiteCode
                               group by Major,System,Subsystem,MaterialType,Material";
                var modelpropertyList = Repositorys<modelData_tree>.FromSql(sql, parameter);
                var list = new List<model_tree>();
                for (int i = 2; i < 7; i++)
                {
                    var cList = modelpropertyList;
                    var dataList = new List<model_tree>();
                    IEnumerable<IGrouping<dynamic, modelData_tree>> _groupBy = null;
                    switch (i)
                    {
                        case 2://专业
                            _groupBy = cList.GroupBy(a => a.Major);
                            dataList = _groupBy.Select(g => ((model_tree)CreatTreeModel(g, g.Key, i))).ToList();
                            break;
                        case 3://系统
                            _groupBy = cList.GroupBy(a => new { a.Major, a.System });
                            dataList = _groupBy.Select(g => ((model_tree)CreatTreeModel(g, g.Key.System, i))).ToList();
                            break;
                        case 4://子系统
                            _groupBy = cList.GroupBy(a => new { a.Major, a.System, a.Subsystem });
                            dataList = _groupBy.Select(g => ((model_tree)CreatTreeModel(g, g.Key.Subsystem, i))).ToList();
                            break;
                        case 5://材料类型
                            _groupBy = cList.GroupBy(a => new { a.Major, a.System, a.Subsystem, a.MaterialType });
                            dataList = _groupBy.Select(g => ((model_tree)CreatTreeModel(g, g.Key.MaterialType, i))).ToList();
                            break;
                        case 6://材料名称
                            _groupBy = cList.GroupBy(a => new { a.Major, a.System, a.Subsystem, a.MaterialType, a.Material });
                            dataList = _groupBy.Select(g => ((model_tree)CreatTreeModel(g, g.Key.Material, i))).ToList();
                            break;
                        default:
                            break;
                    }
                    if (dataList.Any())
                    {
                        dataList.ForEach(x =>
                        {
                            if (!string.IsNullOrEmpty(x.id))
                            {
                                var codeArry = x.id.Split('_');
                                var levelData = CreatLevelId(codeArry, i);
                                var model = list.FirstOrDefault(p => p.id == levelData.Item2);
                                if (model == null)
                                {
                                    x.id = levelData.Item2;
                                    x.pid = levelData.Item1;
                                    x.Type = i - 1;
                                    list.Add(x);
                                }
                            }
                        });
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 构建结构树Id
        /// </summary>
        /// <param name="codeArry"></param>
        /// <param name="type"></param>
        /// <param name="idStr"></param>
        /// <returns></returns>
        private Tuple<string, string> CreatLevelId(string[] codeArry, int type, string idStr = "")
        {
            string pId = "";
            string id = "";
            bool flag = false;
            if (type == 7)
            {
                type = 6;
                flag = true;
            }
            for (int i = 0; i <= type; i++)
            {
                if (type > 2 && i > 0)
                    pId += StringEx.GetCode(codeArry[i - 1]) + "_";
                id += StringEx.GetCode(codeArry[i]) + "_";
            }
            pId = pId.TrimEnd('_');
            id = id.TrimEnd('_');
            if (flag)
            {
                pId = id;
                id = id + "_" + idStr;
            }
            return new Tuple<string, string>(pId, id);
        }

        /// <summary>
        /// 获取清单附加信息
        /// </summary>
        /// <param name="dataList"></param>
        /// <param name="type"></param>
        private void CreatProjectOtherInfo(List<ProjectListAllModel> dataList, BIMRequest request)
        {
            List<string> idList = new List<string>();
            if (request.Type == 1)
                idList = dataList.Select(p => p.ComponentCodeShow).ToList();
            else
                idList = dataList.Select(p => p.id).ToList();
            var codeList = dataList.Select(p => p.ComponentCode).ToList();
            //加工订单信息
            List<TbWorkOrderDetail> orderDetailList = new List<TbWorkOrderDetail>();
            if (request.Type == 1)
                orderDetailList = Repository<TbWorkOrderDetail>.Query(p => p.SiteCode == request.SiteCode && p.RevokeStart == "未撤销" && p.ComponentCode.In(idList)).ToList();
            else
                orderDetailList = Repository<TbWorkOrderDetail>.Query(p => p.SiteCode == request.SiteCode && p.RevokeStart == "未撤销" && p.MxGjId.In(idList)).ToList();
            //模型附件信息
            var modelOtherInfoList = Repository<TbModelOtherInfo>.Query(p => p.ComponentCode.In(codeList) && p.Type == request.Type).ToList();
            dataList.ForEach(x =>
            {
                List<TbWorkOrderDetail> orderData = new List<TbWorkOrderDetail>();
                if (request.Type == 1)
                    orderData = orderDetailList.Where(p => p.MxGjBm == x.ComponentCodeShow).ToList();
                else
                    orderData = orderDetailList.Where(p => p.MxGjId == x.id).ToList();
                x.Processing = orderData.Where(p => p.ComponentStrat == "加工中").Count();
                x.ProcessComplete = orderData.Where(p => p.ComponentStrat == "加工完成").Count();
                x.InstallComplete = orderData.Where(p => p.IsInstall == "是").Count();
                var modelOther = modelOtherInfoList.Where(p => p.ComponentCode == x.ComponentCode).FirstOrDefault();
                if (modelOther != null)
                {
                    x.PlanTime = modelOther.PlanTime;
                    x.ActualTime = modelOther.ActualTime;
                    x.Remark = modelOther.Remark;
                    x.IsAny = true;
                }
                else
                {
                    if (request.PlanTime.HasValue)
                        x.PlanTime = request.PlanTime.Value;
                    if (!string.IsNullOrEmpty(request.Remark))
                        x.Remark = request.Remark;
                }
            });
        }

        private model_tree CreatTreeModel(IGrouping<dynamic, modelData_tree> g, dynamic key, int i)
        {
            var model = new model_tree()
            {
                name = key,
                Total = g.Count(),
                id = g.Max(item => item.ComponentCode),
                FileName = g.Max(item => item.FileName)
            };
            if (i == 6)
                model.Total = g.Sum(item => item.Total);
            return model;
        }

        private void UpdataMaxTime(string componentCode)
        {
            //修改统计最小计划日期
            var report = Repository<TbModelReporte>.First(p => p.ComponentCode == componentCode && p.Type == 5);
            if (report != null)
            {
                var maxDate = Repository<TbModelOtherInfo>.Query(p => p.ComponentCode.StartsWith(componentCode) && p.Type == 1).Max(p => p.PlanTime);
                if (report.PlanTime == null || report.PlanTime > maxDate)
                {
                    report.PlanTime = maxDate;
                    Repository<TbModelReporte>.Update(report);
                }
            }
        }

        public List<string> GetSubCode(BIMRequest request)
        {
            #region 查询语句
            StringBuilder whereSql = new StringBuilder(" where SiteCode=@SiteCode and Type=5 ");
            List<Parameter> parameter = new List<Parameter>();
            parameter.Add(new Parameter("@SiteCode", request.SiteCode, DbType.String, null));
            List<string> codeList = null;
            if (!request.ComponentCodeList.IsEmpty())
            {
                codeList = request.ComponentCodeList.Split(',').ToList();
                whereSql.Append(" and ( ComponentCode like @ComponentCode");
                parameter.Add(new Parameter("@ComponentCode", codeList[0] + "_%", DbType.String, null));
                for (int i = 1; i < codeList.Count; i++)
                {
                    whereSql.Append(" or ComponentCode like @ComponentCode" + i);
                    parameter.Add(new Parameter("@ComponentCode" + i, codeList[i] + "_%", DbType.String, null));
                }
                whereSql.Append(" )");
            }
            #endregion

            string sql = @"SELECT ComponentCode from TbModelReporte {0}";
            sql = string.Format(sql, whereSql);
            try
            {
                return Repositorys<TbModelReporte>.FromSql(sql, parameter).Select(p => p.ComponentCode).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion
    }
}
