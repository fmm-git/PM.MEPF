using Dos.Common;
using Dos.ORM;
using PM.Common;
using PM.Common.Helper;
using PM.DataAccess;
using PM.DataAccess.DbContext;
using PM.DataEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM.Business
{
    public class TbUserRoleLogic
    {

        #region 新增、修改、删除

        /// <summary>
        /// 新增公司信息数据
        /// </summary>
        public AjaxResult Insert(TbUser user, List<TbUserRole> userRole, bool isApi = false)
        {
            if (user == null)
                return AjaxResult.Warning("参数错误");
            user.CreateTime = DateTime.Now;
            user.UserClosed = "在职";
            string pwd = GetLastStr(user.IDNumber, 8);
            var password = PM.Common.Encryption.EncryptionFactory.Md5Encrypt(pwd);
            user.UserPwd = password;
            user.RecruitmentSource = "1";
            try
            {
                using (DbTrans trans = Db.Context.BeginTransaction())
                {
                    Repository<TbUser>.Insert(trans, user, isApi);
                    if (userRole.Count > 0)
                    {
                        Repository<TbUserRole>.Insert(trans, userRole, isApi);
                    }
                    trans.Commit();

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
        /// 新增数据(单条)
        /// </summary>
        public AjaxResult InsertNew(List<TbUserRole> userroleList, bool isApi = false)
        {
            if (userroleList == null)
                return AjaxResult.Warning("参数错误");
            try
            {
                List<TbUserRole> data = Db.Context.From<TbUserRole>().Select(TbUserRole._.All).Where(a => a.DataSource == 0).ToList();
                using (DbTrans trans = Db.Context.BeginTransaction())
                {
                    //先删除原来的表
                    Repository<TbUserRole>.Delete(trans, data, isApi);
                    Repository<TbUserRole>.Insert(trans, userroleList, isApi);
                    trans.Commit();//提交事务
                    return AjaxResult.Success();
                }
            }

            catch (Exception)
            {
                return AjaxResult.Error("操作失败");
            }

        }

        /// <summary>
        /// 修改数据
        /// </summary>
        public AjaxResult Update(TbUser user, List<TbUserRole> userRole, bool isApi = false)
        {
            if (user == null)
                return AjaxResult.Warning("参数错误！");
            try
            {
                using (DbTrans trans = Db.Context.BeginTransaction())
                {
                    Repository<TbUser>.Update(user, isApi);
                    if (userRole.Count > 0)
                    {
                        Repository<TbUserRole>.Delete(trans, a => a.UserCode == user.UserId);
                        Repository<TbUserRole>.Insert(trans, userRole, isApi);
                    }
                    trans.Commit();
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
        /// 删除数据
        /// </summary>
        public AjaxResult Delete(string UserId)
        {
            try
            {
                //判断该部门下是否存在用户角色
                bool isUData = Repository<TbUserRole>.Any(a => a.UserCode == UserId);
                using (DbTrans trans = Db.Context.BeginTransaction())
                {
                    Repository<TbUser>.Delete(trans, t => t.UserId == UserId);
                    if (isUData) Repository<TbUserRole>.Delete(trans, t => t.UserCode == UserId);
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

        #region 获取后几位数
        /// <summary>
        /// 获取后几位数
        /// </summary>
        /// <param name="str">要截取的字符串</param>
        /// <param name="num">返回的具体位数</param>
        /// <returns>返回结果的字符串</returns>
        public string GetLastStr(string str, int num)
        {
            int count = 0;
            if (str.Length > num)
            {
                count = str.Length - num;
                str = str.Substring(count, num);
            }
            return str;
        }
        #endregion

        #endregion


        #region 获取数据

        public PageModel GetCompanyUser(TbUserRequest request, string UserName, string CompanyCode, string DepartmentProjectId)
        {
            //组装查询语句
            //参数化
            List<Parameter> parameter = new List<Parameter>();
            string where = " where 1=1 and ur.OrgId=@OrgId and ur.ProjectId=@ProjectId and ur.Flag=0 ";
            parameter.Add(new Parameter("@OrgId", CompanyCode, DbType.String, null));
            parameter.Add(new Parameter("@ProjectId", DepartmentProjectId, DbType.String, null));
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                where += " and u.UserName like @UserName";
                parameter.Add(new Parameter("@UserName", '%' + UserName + '%', DbType.String, null));
            }
            string sql = @"select distinct u.ID,u.UserId,u.UserCode,u.UserName,u.UserClosed,u.UserSex,u.IDNumber,u.MobilePhone,b.DeptOrOrRle,u.RecruitmentSource from TbUser u
left join TbUserRole ur on ur.UserCode=u.UserId
left join (SELECT  UserCode ,
        DeptOrOrRle = ( STUFF(( SELECT    ',' + DeptOrOrRle FROM (select a.UserCode,a.OrgId,b.DepartmentName+'('+c.RoleName+')' as DeptOrOrRle from TbUserRole a
left join TbDepartment b on a.DeptId=b.DepartmentId
left join TbRole c on a.RoleCode=c.RoleId) as A WHERE A.UserCode = Test.UserCode
                        FOR
                          XML PATH('')
                        ), 1, 1, '') )
FROM  (select a.UserCode,b.DepartmentName+'('+c.RoleName+')' as DeptOrOrRle from TbUserRole a
left join TbDepartment b on a.DeptId=b.DepartmentId
left join TbRole c on a.RoleCode=c.RoleId) AS Test
GROUP BY UserCode) b on u.UserId=b.UserCode ";
            var model = Repository<TbCompany>.FromSqlToPageTable(sql + where, parameter, request.rows, request.page, "ID", "asc");
            return model;
        }

        public TbUser GetFormJson(string keyValue)
        {
            string sql = "select ID,UserCode,UserId,UserName,UserPwd,UserClosed,UserSex,MobilePhone,IDNumber,Email,UserType,case  when UserType=0 then '内部人员' else '外部人员' end as UserTypeName  from  TbUser where  UserId='" + keyValue + "'";
            return Db.Context.FromSql(sql).ToFirstDefault<TbUser>();
        }

        public List<DetpOrRole> GetTreeGridDeptOrRoleJson(string ProjectId, string OrgType)
        {
            string sql = @"select * from (select '0' as pid,a.DepartmentId as id,a.DepartmentName as Name,DepartmentType as OrgType,DepartmentProjectId as ProjectId from TbDepartment a
                          union all
                          select a.DepartmentId as pid,a.RoleId as id,a.RoleName as Name,b.DepartmentType as OrgType,b.DepartmentProjectId as ProjectId from  TbRole a
                          left join TbDepartment b on a.DepartmentId=b.DepartmentId) Tb where Tb.ProjectId='" + ProjectId + "' and  Tb.OrgType='" + OrgType + "'";

            try
            {
                var data = Db.Context.FromSql(sql).ToList<DetpOrRole>();
                return data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public DataTable GetUserRoleNew(string ProjectId, string OrgId, string OrgType, string UserId)
        {
            string sql = "select distinct RoleCode,DeptId from TbUserRole where ProjectId='" + ProjectId + "' and OrgId='" + OrgId + "' and OrgType=" + OrgType + " and UserCode='" + UserId + "'";
            var ret = Db.Context.FromSql(sql).ToDataTable();
            return ret;
        }



        /// <summary>
        /// 获取数据列表(分页)
        /// </summary>
        public PageModel GetDataListForPage(TbUserRoleRequset request)
        {
            try
            {
                //参数化
                List<Parameter> parameter = new List<Parameter>();
                string where = " where 1=1 and u.UserName is not null ";
                if (!string.IsNullOrWhiteSpace(request.ProjectId))
                {
                    where += " and ur.ProjectId=@ProjectId";
                    parameter.Add(new Parameter("@ProjectId", request.ProjectId, DbType.String, null));
                }
                if (!string.IsNullOrWhiteSpace(request.DeptId))
                {
                    where += " and ur.DeptId=@DeptId";
                    parameter.Add(new Parameter("@DeptId", request.DeptId, DbType.String, null));
                }
                if (!string.IsNullOrWhiteSpace(request.RoleCode))
                {
                    where += " and ur.RoleCode=@RoleCode";
                    parameter.Add(new Parameter("@RoleCode", request.RoleCode, DbType.String, null));
                }
                if (!string.IsNullOrWhiteSpace(request.OrgType))
                {
                    where += " and ur.OrgType=@OrgType";
                    parameter.Add(new Parameter("@OrgType", request.OrgType, DbType.String, null));
                }
                string sql = @"select ur.*,u.UserName,r.RoleName,cp.CompanyFullName,dp.DepartmentName,case when ur.OrgType=1 then '' else pro.ProjectName end as ProjectName from TbUserRole ur
left join TbUser u on ur.UserCode=u.UserId
left join TbRole r on ur.RoleCode=r.RoleId
left join TbCompany cp on ur.OrgId=cp.CompanyCode
left join TbDepartment dp on ur.DeptId=dp.DepartmentId
left join TbProjectInfo pro on pro.ProjectId=ur.ProjectId ";
                var model = Repository<TbUserRole>.FromSqlToPageTable(sql + where, parameter, request.rows, request.page, "ID", "asc");
                return model;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        /// <summary>
        /// 获取登录用户信息
        /// </summary>
        /// <param name="menuCode"></param>
        /// <returns></returns>
        public CurrentUserInfo FindUserInfo(string userCode)
        {
            return TbUserRoleRepository.FindUserInfo(userCode);
        }
    }
}
