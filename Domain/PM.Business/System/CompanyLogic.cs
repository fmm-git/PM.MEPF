using Dos.Common;
using Dos.ORM;
using PM.Common;
using PM.DataAccess.DbContext;
using PM.DataEntity;
using PM.DataEntity.System.ViewModel;
using PM.Domain.WebBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM.Business
{
    public class CompanyLogic
    {
        #region 公司管理查询处理

        /// <summary>
        /// 查询公司（全部查询 or 条件查询）
        /// </summary>
        /// <param name="pr"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<TbCompanyOrParent> GetAllCompanyOrBySearch(string keyword)
        {
            try
            {
                var listAll = new List<TbCompanyOrParent>();
                string orgType = OperatorProvider.Provider.CurrentUser.OrgType;
                string userCode = OperatorProvider.Provider.CurrentUser.UserCode;
                string where = "";
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    where += " and a.CompanyFullName like '%" + keyword + "%'";
                }
                if (orgType != "1" && userCode != "500000")
                {
                    where += " and a.FullCode='" + OperatorProvider.Provider.CurrentUser.ProjectId + "'";
                }
                string sql = @"select a.*,a.FullCode as ProjectId,b.CompanyFullName as ParentCompanyName from TbCompany a
left join TbCompany b on a.ParentCompanyCode=b.CompanyCode 
where 1=1 " + where + @" order by a.LocalCurrency asc";
                var list1 = Db.Context.FromSql(sql).ToList<TbCompanyOrParent>();
                var list2 = list1.Where(a => a.OrgType != 1).ToList();
                listAll.AddRange(list2);
                //查询所有的经理部
                string sql1 = "select FullCode,CompanyCode,CompanyFullName,Address,OrgType,PostalCode from TbCompany where OrgType=2;";
                //查询所有的加工厂
                string sql2 = "select FullCode,CompanyCode,CompanyFullName,Address,OrgType,PostalCode from TbCompany where OrgType=1 order by CompanyCode asc;";
                DataTable dt1 = Db.Context.FromSql(sql1).ToDataTable();
                DataTable dt2 = Db.Context.FromSql(sql2).ToDataTable();
                if (dt1.Rows.Count > 0)
                {
                    List<TbCompanyOrParent> list3 = new List<TbCompanyOrParent>();
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        for (int j = 0; j < dt2.Rows.Count; j++)
                        {
                            if (dt1.Rows[i]["PostalCode"].ToString() == dt2.Rows[j]["PostalCode"].ToString())
                            {
                                TbCompanyOrParent cm = new TbCompanyOrParent();
                                cm.CompanyCode = dt2.Rows[j]["CompanyCode"].ToString() + "-" + dt1.Rows[i]["CompanyCode"].ToString();
                                cm.ParentCompanyCode = dt1.Rows[i]["CompanyCode"].ToString();
                                cm.ParentCompanyName = dt1.Rows[i]["CompanyFullName"].ToString();
                                cm.CompanyFullName = dt2.Rows[j]["CompanyFullName"].ToString();
                                cm.Address = dt2.Rows[j]["Address"].ToString();
                                cm.OrgType = Convert.ToInt32(dt2.Rows[j]["OrgType"]);
                                cm.ProjectId = dt1.Rows[i]["FullCode"].ToString();
                                list3.Add(cm);
                            }
                        }
                    }
                    listAll.AddRange(list3);
                }
                return listAll;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<TbCompany> GetAllCompanyOrBySearchNew()
        {
            string ProjectId = OperatorProvider.Provider.CurrentUser.ProjectId;
            var listAll = new List<TbCompany>();
            List<TbCompany> list2 = new List<TbCompany>();
            string sql1 = @"select pc.ProjectId,pj.ProjectName from TbProjectCompany pc
left join TbProjectInfo pj on pc.ProjectId=pj.ProjectId where pc.OrgType=2 and pc.ProjectId='" + ProjectId + "'";
            DataTable dt1 = Db.Context.FromSql(sql1).ToDataTable();
            if (dt1 != null)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    TbCompany cm = new TbCompany();
                    cm.CompanyCode = dt1.Rows[i]["ProjectId"].ToString();
                    cm.ParentCompanyCode = "0";
                    cm.CompanyFullName = dt1.Rows[i]["ProjectName"].ToString();
                    cm.OrgType = -1;
                    list2.Add(cm);
                }
                listAll.AddRange(list2);
            }
            string sql = @"select cp.id,cp.CompanyCode,cp.CompanyFullName,cp.ParentCompanyCode,cp.OrgType from TbCompany cp
left join TbProjectCompany pcp on cp.CompanyCode=pcp.CompanyCode where pcp.ProjectId='" + ProjectId + "' order by id asc";
            List<TbCompany> list1 = Db.Context.FromSql(sql).ToList<TbCompany>();
            List<TbCompany> list3 = list1.Where(s => s.OrgType != 2).ToList();
            listAll.AddRange(list3);
            List<TbCompany> list4 = list1.Where(s => s.OrgType == 2).ToList();
            if (list4.Count > 0)
            {
                for (int i = 0; i < list4.Count; i++)
                {
                    switch (list4[i].CompanyCode)//用户输入的值,和下面的case匹配
                    {
                        case "6247574415609954304":
                            list4[i].ParentCompanyCode = "6245721945602523136";
                            break;
                        case "6247574415609954305":
                            list4[i].ParentCompanyCode = "6245721945602523137";
                            break;
                        case "6247574415609954309":
                            list4[i].ParentCompanyCode = "6245721945602523139";
                            break;
                        case "6247574415609954306":
                            list4[i].ParentCompanyCode = "6422195692059623424";
                            break;
                        default: //如果匹配全不成功则执行下面的代码
                            break;
                    }
                }
                listAll.AddRange(list4);
            }
            return listAll;
        }

        public AjaxResult SynchronizationPro()
        {
            try
            {
                string projectCode = "";
                string sql1 = @"select CompanyCode from TbCompany where OrgType=2 and PostalCode='0'";
                DataTable dt1 = Db.Context.FromSql(sql1).ToDataTable();
                if (dt1 != null)
                {
                    List<TbCompany> pcList = new List<TbCompany>();
                    List<TbCompany> cpList = Db.Context.From<TbCompany>().Select(TbCompany._.All).ToList();
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        var parentCode = dt1.Rows[i]["CompanyCode"].ToString();
                        string sql = @"WITH TREE AS(SELECT * FROM TbCompany WHERE CompanyCode =@parentCode UNION ALL SELECT TbCompany.* FROM TbCompany, TREE WHERE TbCompany.ParentCompanyCode = TREE.CompanyCode) SELECT CompanyCode,ParentCompanyCode,CompanyFullName,OrgType,Address FROM TREE ";
                        DataTable dt = Db.Context.FromSql(sql)
                            .AddInParameter("@parentCode", DbType.String, parentCode).ToDataTable();
                        switch (parentCode)
                        {
                            case "6247574415609954304":
                                projectCode = "6245721945602523136";
                                break;
                            case "6247574415609954305":
                                projectCode = "6245721945602523137";
                                break;
                            case "6247574415609954309":
                                projectCode = "6245721945602523139";
                                break;
                            case "6247574415609954306":
                                projectCode = "6422195692059623424";
                                break;
                            case "6247574415609954308":
                                projectCode = "6245721945602523138";
                                break;
                            default:
                                break;
                        }
                        if (dt != null)
                        {
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                string CompanyCode = dt.Rows[j]["CompanyCode"].ToString();
                                TbCompany pcModel = cpList.Where(a => a.CompanyCode == CompanyCode).First();
                                pcModel.FullCode = projectCode;
                                pcList.Add(pcModel);
                            }
                        }
                    }
                    using (DbTrans trans = Db.Context.BeginTransaction())
                    {
                        Repository<TbCompany>.Update(trans, pcList);
                        trans.Commit();//提交事务
                    }
                }

                return AjaxResult.Success();
            }
            catch (Exception e)
            {
                return AjaxResult.Error(e.ToString());
            }
        }

        /// <summary>
        /// 根据编码查询公司
        /// </summary>
        /// <param name="menuCode"></param>
        /// <returns></returns>
        public TbCompany FindEntity(string companyCode)
        {
            var company = Repository<TbCompany>.First(d => d.CompanyCode == companyCode);
            return company;
        }

        /// <summary>
        /// 全部查询
        /// </summary>
        /// <param name="pr"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<TbCompany> GetList()
        {
            //OperatorProvider.Provider.CurrentUser.ProjectId
            //根据分页条件查询部分数据
            var list = Db.Context.From<TbCompany>()
              .Select(TbCompany._.All).ToList();
            return list;
        }

        #endregion

        #region 公司管理插入、修改处理

        /// <summary>
        /// 修改公司状态
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public AjaxResult EditIsEnable(string code, int val)
        {
            var count = Db.Context.Update<TbCompany>(TbCompany._.IsEnable, val, TbCompany._.CompanyCode == code);
            if (count > 0)
            {
                return AjaxResult.Success();
            }
            return AjaxResult.Error();
        }

        public int JudgeExist()
        {
            var list = Db.Context.From<TbCompany>().Where(d => d.ParentCompanyCode == "1").ToList();
            return list.Count;
        }

        public int VerificationMethod(TbCompany company, string type)
        {
            if (type == "add")
            {
                var list = Db.Context.From<TbCompany>().Where(d => d.CompanyFullName == company.CompanyFullName && d.CompanyCode == company.CompanyCode).ToList().Count;
                return list;
            }
            else
            {
                //新建查询where
                var where = new Where<TbCompany>();
                where.And(t => t.CompanyFullName == company.CompanyFullName);
                where.And(t => t.CompanyCode != company.CompanyCode);
                where.And(t => t.id != company.id);
                //根据条件查询部分数据
                var list = Repository<TbCompany>.Query(where, d => d.id).ToList().Count;
                return list;
            }

        }

        /// <summary>
        /// 新增公司信息数据
        /// </summary>
        public AjaxResult Insert(TbCompany cpy, bool isApi = false)
        {
            if (cpy == null)
                return AjaxResult.Warning("参数错误");
            if (cpy.ParentCompanyCode == "1")
            {
                var num = JudgeExist();
                if (num > 0)
                {
                    return AjaxResult.Warning("父级(顶级)公司只能存在一个！");
                }
            }
            cpy.PostalCode = "1";
            try
            {
                Repository<TbCompany>.Insert(cpy, isApi);
                return AjaxResult.Success();
            }
            catch (Exception e)
            {
                var err = e.ToString();
                return AjaxResult.Error(err);
            }
        }
        /// <summary>
        /// 新增公司信息数据
        /// </summary>
        public AjaxResult InsertNew(List<TbCompany> cpy, bool isApi = false)
        {
            if (cpy == null)
                return AjaxResult.Warning("参数错误");
            try
            {
                List<TbCompany> data = Db.Context.From<TbCompany>().Select(TbCompany._.All).Where(a => a.PostalCode == "0").ToList();
                using (DbTrans trans = Db.Context.BeginTransaction())
                {
                    //先删除原来的表
                    Repository<TbCompany>.Delete(trans, data, isApi);
                    //插入从BM那边取过来的数据
                    Repository<TbCompany>.Insert(trans, cpy, isApi);
                    trans.Commit();//提交事务
                    return AjaxResult.Success();
                }
            }
            catch (Exception e)
            {
                var err = e.ToString();
                return AjaxResult.Error(err);
            }
        }
        /// <summary>
        /// 修改公司信息数据
        /// </summary>
        public AjaxResult Update(TbCompany cyp, bool isApi = false)
        {
            if (cyp == null)
                return AjaxResult.Warning("参数错误");

            try
            {
                Repository<TbCompany>.Update(cyp, d => d.CompanyCode == cyp.CompanyCode, isApi);
                return AjaxResult.Success();
            }
            catch (Exception e)
            {
                var err = e.ToString();
                return AjaxResult.Error(err);
            }
        }

        #endregion

        #region 公司管理删除处理

        /// <summary>
        /// 删除数据
        /// </summary>
        public AjaxResult Delete(string companyCode, bool isApi = false)
        {
            var any = false;
            try
            {
                any = Repository<TbCompany>.Any(p => p.ParentCompanyCode == companyCode);
                if (any)
                {
                    return AjaxResult.Warning("该公司存在子公司，不允许删除！");
                }
                Repository<TbCompany>.Delete(t => t.CompanyCode == companyCode, isApi);
                return AjaxResult.Success();
            }
            catch (Exception e)
            {
                var err = e.ToString();
                return AjaxResult.Error();
            }
        }

        #endregion

        #region 获取工区信息 by ProjectId

        public PageModel GetWorkAreaByProjectId(TbCompanyRequest request)
        {
            var sql = @"select 
                        a.id,
                        a.CompanyCode,
                        a.Address,
                        c.CompanyFullName +'/'+a.CompanyFullName as CompanyFullName,
                        b.ProjectId
                        from TbCompany a
                        left join TbProjectCompany b on a.CompanyCode=b.CompanyCode
                        left join TbCompany c on a.ParentCompanyCode=c.CompanyCode";

            string where = " where b.OrgType=4";
            List<Parameter> parameter = new List<Parameter>();
            if (!string.IsNullOrWhiteSpace(request.ProjectId))
            {
                where += " and  b.ProjectId=@ProjectId";
                parameter.Add(new Parameter("@ProjectId", request.ProjectId, DbType.String, null));
            }

            if (!string.IsNullOrWhiteSpace(request.keyword))
            {
                where += " and a.CompanyFullName like @CompanyFullName";
                parameter.Add(new Parameter("@CompanyFullName", '%' + request.keyword + '%', DbType.String, null));
            }
            if (!string.IsNullOrEmpty(request.ParentCompanyCode))
            {
                where += " and a.ParentCompanyCode like @ParentCompanyCode";
                parameter.Add(new Parameter("@ParentCompanyCode", request.ParentCompanyCode, DbType.String, null));
            }
            try
            {
                var data = Repository<TbCompany>.FromSqlToPageTable(sql + where, parameter, request.rows, request.page, "id");
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region 获取分部、工区、站点（的上级组织机构）

        public PageModel GetCompanyByProjectId(TbCompanyRequest request)
        {
            List<Parameter> parameter = new List<Parameter>();
            string where = " where 1=1 ";
            string sql = "";
            if (!string.IsNullOrWhiteSpace(request.OrgType)) 
            {
                if (request.OrgType == "5")
                {
                    sql = @"select a.id,d.CompanyFullName+'/'+c.CompanyFullName+'/'+b.CompanyFullName as ParentCompanyName,a.CompanyCode,a.ParentCompanyCode_F,a.ParentCompanyCode,a.CompanyFullName,a.FullCode as ProjectId,a.Address,a.OrgType from TbCompany a
left join TbCompany b on a.ParentCompanyCode=b.CompanyCode
left join TbCompany c on b.ParentCompanyCode=c.CompanyCode
left join TbCompany d on c.ParentCompanyCode=d.CompanyCode ";
                }
                else if (request.OrgType == "4")
                {
                    sql = @"select a.id,c.CompanyFullName+'/'+b.CompanyFullName as ParentCompanyName,a.CompanyCode,a.ParentCompanyCode_F,a.ParentCompanyCode,a.CompanyFullName,a.FullCode as ProjectId,a.Address,a.OrgType from TbCompany a
left join TbCompany b on a.ParentCompanyCode=b.CompanyCode
left join TbCompany c on b.ParentCompanyCode=c.CompanyCode";
                }
                else if (request.OrgType == "3")
                {
                    sql = @"select a.id,b.CompanyFullName as ParentCompanyName,a.CompanyCode,a.ParentCompanyCode_F,a.ParentCompanyCode,a.CompanyFullName,a.FullCode as ProjectId,a.Address,a.OrgType from TbCompany a
left join TbCompany b on a.ParentCompanyCode=b.CompanyCode";
                }

                where += " and a.OrgType=@OrgType";
                parameter.Add(new Parameter("@OrgType", request.OrgType, DbType.String, null));
            }
            if (!string.IsNullOrWhiteSpace(request.ProjectId))
            {
                where += " and a.FullCode=@ProjectId";
                parameter.Add(new Parameter("@ProjectId", request.ProjectId, DbType.String, null));
            }

            if (!string.IsNullOrWhiteSpace(request.keyword))
            {
                where += " and a.CompanyFullName like @CompanyFullName";
                parameter.Add(new Parameter("@CompanyFullName", '%' + request.keyword + '%', DbType.String, null));
            }
            try
            {
                var data = Repository<TbCompany>.FromSqlToPageTable(sql + where, parameter, request.rows, request.page, "id");
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
