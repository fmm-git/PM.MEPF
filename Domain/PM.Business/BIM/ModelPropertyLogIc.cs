using Dos.ORM;
using PM.Common;
using PM.Common.Extension;
using PM.DataAccess.DbContext;
using PM.DataEntity;
using PM.DataEntity.BIM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM.Business.BIM
{
    /// <summary>
    /// 本地模型基础信息
    /// </summary>
    public class ModelPropertyLogIc
    {
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
            if (!request.ComponentCodeList.IsEmpty())
            {
                var codeList = request.ComponentCodeList.Split(',').ToList();
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

            string sql = @"SELECT SiteCode,Major,System,Subsystem,MaterialType,Material,Size,count(*) as Total, MAX(ComponentCode) as ComponentCode
                        from TbModel_Property
                        {0}
                        GROUP BY Major,System,Subsystem,MaterialType,Material,Size,SiteCode ";
            sql = string.Format(sql, whereSql);
            try
            {
                var ret = Repositorys<ProjectListAllModel>.FromSqlToPage(sql, parameter, request.rows, request.page, "Major");
                List<ProjectListAllModel> dataList = ret.rows as List<ProjectListAllModel>;
                request.Type = 1;
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
            if (!string.IsNullOrEmpty(request.ComponentCode))
            {
                whereSql += " and ComponentCode like @ComponentCode";
                parameter.Add(new Parameter("@ComponentCode", request.ComponentCode + "_%", DbType.String, null));
            }
            #endregion
            string sql = @"SELECT id,ComponentCode,ComponentName as Name,Length,Size,Area,Material,1 as Total 
                           from TbModel_Property
                           {0}";
            sql = string.Format(sql, whereSql);
            try
            {
                var dataList = Repositorys<ProjectListAllModel>.FromSql(sql, parameter);
                if (dataList.Count < request.TotalCount)
                {
                    int lastIndex = request.ComponentCode.LastIndexOf('-');
                    request.ComponentCode = request.ComponentCode.Substring(0, lastIndex);
                    return GetDataItemListForPage(request);
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
                    Repository<TbModelOtherInfo>.Update(model);
                }
                else
                {
                    Repository<TbModelOtherInfo>.Insert(model);
                }
                if (model.Type == 1)
                {
                    //修改统计最小计划日期
                    int lastIndex = model.ComponentCode.LastIndexOf('_');
                    string code = model.ComponentCode.Substring(0, lastIndex);
                    var report = Repository<TbModelReporte>.First(p => p.ComponentCode == code && p.Type == 5);
                    var minDate = Repository<TbModelOtherInfo>.Query(p => p.ComponentCode.StartsWith(code) && p.Type == 1).Min(p => p.PlanTime);
                    if (report.PlanTime > minDate)
                    {
                        report.PlanTime = minDate;
                        Repository<TbModelReporte>.Update(report);
                    }
                }
                return AjaxResult.Success();
            }
            catch (Exception)
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
                Repository<TbModelOtherInfo>.Insert(lastList);
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
                    IEnumerable<IGrouping<string, modelData_tree>> _groupBy = null;
                    switch (i)
                    {
                        case 2://专业
                            _groupBy = cList.GroupBy(a => a.Major);
                            break;
                        case 3://系统
                            _groupBy = cList.GroupBy(a => a.System);
                            break;
                        case 4://子系统
                            _groupBy = cList.GroupBy(a => a.Subsystem);
                            break;
                        case 5://材料类型
                            _groupBy = cList.GroupBy(a => a.MaterialType);
                            break;
                        case 6://材料名称
                            _groupBy = cList.GroupBy(a => a.Material);
                            break;
                        default:
                            break;
                    }
                    dataList = _groupBy.Select(g => (new model_tree { name = g.Key, Total = g.Count(), id = g.Max(item => item.ComponentCode), FileName = g.Max(item => item.FileName) })).ToList();
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
        public List<ModelReportList> GetModelReportForPage(BIMRequest request,out int count)
        {
            #region 查询语句
            string whereSql = "where 1=1";
            List<Parameter> parameter = new List<Parameter>();
            if (request.SiteCode.IsEmpty())
            {
                whereSql += "and SiteCode=@SiteCode";
                parameter.Add(new Parameter("@SiteCode", request.SiteCode, DbType.String, null));
            }
            #endregion

            string sql = @"select 
                            SiteCode,
                            min(PlanTime)as PlanTime,
                            max(ActualTime)as ActualTime,
                            (select sum(PlanTotal) from TbModelReporte where SiteCode=a.SiteCode and type=4) as PlanTotal1, 
                            (select sum(PlanTotal) from TbModelReporte where SiteCode=a.SiteCode and type=5) as PlanTotal2, 
                            (select sum(PlanTotal) from TbModelReporte where SiteCode=a.SiteCode and type=6) as PlanTotal3, 
                            (select sum(ProcessingTotal) from TbModelReporte where SiteCode=a.SiteCode and type=4) as ProcessingTotal1, 
                            (select sum(ProcessingTotal) from TbModelReporte where SiteCode=a.SiteCode and type=5) as ProcessingTotal2, 
                            (select sum(ProcessingTotal) from TbModelReporte where SiteCode=a.SiteCode and type=6) as ProcessingTotal3, 
                            (select sum(MachinTotal) from TbModelReporte where SiteCode=a.SiteCode and type=4) as MachinTotal1, 
                            (select sum(MachinTotal) from TbModelReporte where SiteCode=a.SiteCode and type=5) as MachinTotal2, 
                            (select sum(MachinTotal) from TbModelReporte where SiteCode=a.SiteCode and type=6) as MachinTotal3,  
                            (select sum(InstallTotal) from TbModelReporte where SiteCode=a.SiteCode and type=4) as InstallTotal1, 
                            (select sum(InstallTotal) from TbModelReporte where SiteCode=a.SiteCode and type=5) as InstallTotal2, 
                            (select sum(InstallTotal) from TbModelReporte where SiteCode=a.SiteCode and type=6) as InstallTotal3,  
                            (select sum(CheckTotal) from TbModelReporte where SiteCode=a.SiteCode and type=4) as CheckTotal1, 
                            (select sum(CheckTotal) from TbModelReporte where SiteCode=a.SiteCode and type=5) as CheckTotal2, 
                            (select sum(CheckTotal) from TbModelReporte where SiteCode=a.SiteCode and type=6) as CheckTotal3, 
                            (select count(*) from TbModelReporte where SiteCode=a.SiteCode and type=4 and Difference<0) as lag1, 
                            (select count(*) from TbModelReporte where SiteCode=a.SiteCode and type=5 and Difference<0) as lag2, 
                            (select count(*) from TbModelOtherInfo where SiteCode=a.SiteCode and type=2 and Difference<0) as lag3, 
                            (select count(*) from TbModelReporte where SiteCode=a.SiteCode and type=4 and Difference>0) as lea1, 
                            (select count(*) from TbModelReporte where SiteCode=a.SiteCode and type=5 and Difference>0) as lea2, 
                            (select count(*) from TbModelOtherInfo where SiteCode=a.SiteCode and type=2 and Difference>0) as lea3 
                            from TbModelReporte a
                            {0}
                            group by SiteCode ";
            sql = string.Format(sql, whereSql);
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
        public void UpdateModelReportData(DbTrans trans, List<TbModelReporte> insertList, List<TbModelReporte> updateList)
        {
            if (insertList.Any())
                Repository<TbModelReporte>.Insert(trans, insertList);
            if (updateList.Any())
                Repository<TbModelReporte>.Update(trans, updateList);
        }

        /// <summary>
        /// 模型统计信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public Tuple<List<TbModelReporte>, List<TbModelReporte>> CreatReportModel(TbModelOrg model)
        {
            List<TbModelReporte> dataList = new List<TbModelReporte>();
            int count = 0;
            var list = Getmodel_tree2(model.SiteCode, out count);
            var com = new TbModelReporte()
            {
                SiteCode = model.SiteCode,
                ProjectId = model.ProjectId,
                FileName = "",
                PlanTotal = count,
                ComponentCode = "",
                ComponentCodeP = "",
                Type = 6
            };
            dataList.Add(com);
            //项目结构树信息
            list.ForEach(x =>
            {
                var item = new TbModelReporte()
                {
                    SiteCode = model.SiteCode,
                    ProjectId = model.ProjectId,
                    FileName = model.Path,
                    PlanTotal = x.Total,
                    ComponentCode = x.id,
                    ComponentCodeP = x.pid,
                    Type = x.Type
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
                        item.IsUpdate = true;
                        x.IsUpdate = true;
                    }
                });
            }
            var insertList = dataList.Where(p => !p.IsUpdate).ToList();
            var updateList = reportList.Where(p => p.IsUpdate).ToList();
            return new Tuple<List<TbModelReporte>, List<TbModelReporte>>(insertList, updateList);
        }
        #endregion

        #region Private

        /// <summary>
        /// 获取项目结构树
        /// </summary>
        private List<model_tree> Getmodel_tree2(string siteCode, out int count)
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
                count = modelpropertyList.Where(p => !p.Major.IsEmpty()).Sum(p => p.Total);
                var list = new List<model_tree>();
                for (int i = 5; i < 7; i++)
                {
                    var cList = modelpropertyList;
                    var dataList = new List<model_tree>();
                    IEnumerable<IGrouping<string, modelData_tree>> _groupBy = null;
                    switch (i)
                    {
                        case 2://专业
                            _groupBy = cList.GroupBy(a => a.Major);
                            break;
                        case 3://系统
                            _groupBy = cList.GroupBy(a => a.System);
                            break;
                        case 4://子系统
                            _groupBy = cList.GroupBy(a => a.Subsystem);
                            break;
                        case 5://材料类型
                            _groupBy = cList.GroupBy(a => a.MaterialType);
                            break;
                        case 6://材料名称
                            _groupBy = cList.GroupBy(a => a.Material);
                            break;
                        default:
                            break;
                    }
                    dataList = _groupBy.Select(g => (new model_tree { name = g.Key, Total = g.Count(), id = g.Max(item => item.ComponentCode), FileName = g.Max(item => item.FileName) })).ToList();
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
                    pId += codeArry[i - 1] + "_";
                id += codeArry[i] + "_";
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
            //var orderCodeList = Repository<TbWorkOrder>.Query(p => p.SiteCode == request.SiteCode).Select(p => p.OrderCode).ToList();
            if (request.Type == 1)
                orderDetailList = Repository<TbWorkOrderDetail>.Query(p => p.MxGjBm.In(idList)).ToList();
            else
                orderDetailList = Repository<TbWorkOrderDetail>.Query(p => p.MxGjId.In(idList)).ToList();
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
                x.InstallComplete = orderData.Where(p => p.ComponentStrat == "安装完成").Count();
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

        #endregion
    }
}
