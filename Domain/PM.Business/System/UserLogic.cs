using Dos.Common;
using Dos.ORM;
using PM.Business.Production;
using PM.Common;
using PM.DataAccess.DbContext;
using PM.DataEntity;
using PM.DataEntity.System.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace PM.Business
{
    /// <summary>
    /// 用户员工数据处理
    /// </summary>
    public class UserLogic
    {
        private readonly TbWorkOrderLogic _workOrderLogic = new TbWorkOrderLogic();

        #region 员工查询处理

        /// <summary>
        /// 查询全部公司左侧分类
        /// </summary>
        /// <returns></returns>
        public List<TbCompany> GetAllCompany()
        {
            return Db.Context.From<TbCompany>().ToList();
        }

        /// <summary>
        /// 根据左侧公司Code查询员工信息
        /// </summary>
        /// <param name="ComCode"></param>
        /// <returns></returns>
        public List<TbUser> GetUserByCompanyCode(string ComCode)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select ");
            sb.Append("a.ID,a.UserCode,a.UserName,a.UserPwd,a.UserClosed,a.UserSex,");
            sb.Append("a.Birthday,a.IDNumber,a.PoliticalLandscape,a.MobilePhone,a.Tell,a.Email,");
            sb.Append("a.QQ,a.WeChat,a.RecruitmentSource,a.PurchaseSocialSecurity,a.TheLaborContract,");
            sb.Append("a.ConfidentialityContract,a.CardNumber,a.CardBankName,a.CardBankAdd,a.CreateTime,a.CreateUser,f.UserName as CreateUserName,");
            sb.Append("b.CompanyFullName as CompanyCode,d.DepartmentName as DepartmentCode ");
            sb.Append("from TbUser a left join TbCompany b on a.CompanyCode=b.CompanyCode ");
            sb.Append("left join TbUserAndDepRelevance c on a.UserCode=c.UserCode ");
            sb.Append("left join TbDepartment d on c.DepartmentCode=d.DepartmentCode left join TbUser f on a.CreateUser=f.UserCode where 1=1");
            sb.Append(" and a.CompanyCode = @name");
            sb.Append(" order by a.CompanyCode,d.DepartmentCode;");
            return Db.Context.FromSql(sb.ToString()).AddInParameter(
                "@name", DbType.String, ComCode).ToList<TbUser>();
        }

        /// <summary>
        /// 查询全部或者根据条件查询员工信息
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public List<TbUser> GetAllOrBySearchUser(string keyValue)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select ");
            sb.Append("a.ID,a.UserCode,a.UserName,a.UserPwd,a.UserClosed,a.UserSex,");
            sb.Append("a.Birthday,a.IDNumber,a.PoliticalLandscape,a.MobilePhone,a.Tell,a.Email,");
            sb.Append("a.QQ,a.WeChat,a.RecruitmentSource,a.PurchaseSocialSecurity,a.TheLaborContract,");
            sb.Append("a.ConfidentialityContract,a.CardNumber,a.CardBankName,a.CardBankAdd,a.CreateTime,a.CreateUser,f.UserName as CreateUserName,");
            sb.Append("b.CompanyFullName as CompanyCode,d.DepartmentName as DepartmentCode ");
            sb.Append("from TbUser a left join TbCompany b on a.CompanyCode=b.CompanyCode ");
            sb.Append("left join TbUserAndDepRelevance c on a.UserCode=c.UserCode ");
            sb.Append("left join TbDepartment d on c.DepartmentCode=d.DepartmentCode left join TbUser f on a.CreateUser=f.UserCode where 1=1");
            //判断条件查询是否为空，不为空，条件添加进行查询
            if (!string.IsNullOrEmpty(keyValue))
            {
                sb.Append(" and a.UserName like @name");
            }
            sb.Append(" order by a.CompanyCode,d.DepartmentCode;");
            return Db.Context.FromSql(sb.ToString()).AddInParameter(
                "@name", DbType.String, "%" + keyValue + "%").ToList<TbUser>();
        }

        /// <summary>
        /// 获取数据列表(分页)
        /// </summary>
        public PageModel GetDataListForPage(TbUserRequest request)
        {
            //组装查询语句
            #region 模糊搜索条件

            var where = " where 1=1";
            if (!string.IsNullOrWhiteSpace(request.UserName))
            {
                where += " and a.UserName like '%" + request.UserName + "%' or a.MobilePhone like '%" + request.UserName + "%' or a.IDNumber like '%" + request.UserName + "%' or a.UserCode like '%" + request.UserName + "%'";
            }

            #endregion

            try
            {
                string sql = @"select a.ID,a.UserCode,a.UserName,a.UserPwd,a.UserId,a.UserClosed,a.UserSex,a.IDNumber,a.MobilePhone,a.Email,a.UserType,b.DeptOrOrRle,a.RecruitmentSource from TbUser a
left join (SELECT  UserCode ,
        DeptOrOrRle = ( STUFF(( SELECT    ',' + DeptOrOrRle FROM (select a.UserCode,b.DepartmentName+'('+c.RoleName+')' as DeptOrOrRle from TbUserRole a
left join TbDepartment b on a.DeptId=b.DepartmentId
left join TbRole c on a.RoleCode=c.RoleId) as A WHERE A.UserCode = Test.UserCode
                        FOR
                          XML PATH('')
                        ), 1, 1, '') )
FROM  (select a.UserCode,b.DepartmentName+'('+c.RoleName+')' as DeptOrOrRle from TbUserRole a
left join TbDepartment b on a.DeptId=b.DepartmentId
left join TbRole c on a.RoleCode=c.RoleId) AS Test
GROUP BY UserCode) b on a.UserId=b.UserCode";
                List<Parameter> parameter = new List<Parameter>();
                var ret = Repository<TbUser>.FromSqlToPageTable(sql + where, parameter, request.rows, request.page, "ID", "asc");
                return ret;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public TbUser GetFormJson(string keyValue)
        {
            string sql = "select ID,UserCode,UserId,UserName,UserPwd,UserClosed,UserSex,MobilePhone,IDNumber,Email,UserType,case  when UserType=0 then '内部人员' else '外部人员' end as UserTypeName  from  TbUser where  UserId='" + keyValue + "'";
            return Db.Context.FromSql(sql).ToFirstDefault<TbUser>();
        }

        /// <summary>
        /// select 绑定
        /// </summary>
        /// <param name="dataCode"></param>
        /// <returns></returns>
        public List<TbSysDictionaryData> GetList(string dataCode)
        {
            var where = new Where<TbSysDictionaryData>();
            where.And(d => d.FDictionaryCode == dataCode);
            return Repository<TbSysDictionaryData>.Query(where, d => d.DictionaryOrder, "asc").ToList();
        }

        /// <summary>
        /// 查询用户所有权限
        /// </summary>
        /// <param name="usercode"></param>
        /// <returns></returns>
        public List<TbUserRoleUserRequset> GetUserRole(string usercode)
        {
            return Db.Context.FromSql("select A.*,B.UserName from (select a.RoleCode,a.RoleName,a.State,b.UserCode from TbRole a left join TbUserRole b on a.RoleCode=b.RoleCode) A left join TbUser B on A.UserCode=B.UserCode where B.UserCode=@ucode").AddInParameter("@ucode", DbType.String, usercode).ToList<TbUserRoleUserRequset>();
        }

        #endregion

        #region 员工插入、修改处理

        /// <summary>
        /// 新增公司信息数据
        /// </summary>
        public AjaxResult Insert(TbUser user, bool isApi = false)
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
                Repository<TbUser>.Insert(user, isApi);
                return AjaxResult.Success();
            }
            catch (Exception e)
            {
                var err = e.ToString();
                return AjaxResult.Error(err);
            }
        }

        public AjaxResult InsertNew(List<TbUser> user, bool isApi = false)
        {
            if (user == null)
                return AjaxResult.Warning("参数错误");
            try
            {
                List<TbUser> data = Db.Context.From<TbUser>().Select(TbUser._.All).Where(a => a.RecruitmentSource == "0").ToList();
                using (DbTrans trans = Db.Context.BeginTransaction())
                {
                    //先删除原来的表
                    Repository<TbUser>.Delete(trans, data, isApi);
                    //插入从BM那边取过来的数据
                    Repository<TbUser>.Insert(trans, user, isApi);
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
        /// 修改数据
        /// </summary>
        public AjaxResult Update(TbUser user, bool isApi = false)
        {
            if (user == null)
                return AjaxResult.Warning("参数错误！");
            string pwd = GetLastStr(user.IDNumber, 8);
            var password = PM.Common.Encryption.EncryptionFactory.Md5Encrypt(pwd);
            user.UserPwd = password;
            try
            {
                Repository<TbUser>.Update(user, p => p.ID == user.ID);
                return AjaxResult.Success();
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

        /// <summary>
        /// 删除数据
        /// </summary>
        public AjaxResult UpdateUserClosed(int ID, int Type)
        {
            try
            {
                //判断该部门下是否存在用户角色
                var model = Repository<TbUser>.First(p => p.ID == ID);
                if (Type == 1)
                {
                    model.UserClosed = "在职";
                }
                else
                {
                    model.UserClosed = "离职";
                }
                Repository<TbUser>.Update(model, p => p.ID == model.ID);
                return AjaxResult.Success();
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

        #region 根据用户编码查询出对应的个人信息
        public TbUser UserFormSelectList(string UserCode)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select ");
                sb.Append("a.UserCode,a.UserName,a.UserClosed,");
                sb.Append("a.Birthday,a.IDNumber,a.PoliticalLandscape,a.MobilePhone,a.Tell,a.Email,");
                sb.Append("a.QQ,a.WeChat,a.RecruitmentSource,a.PurchaseSocialSecurity,a.TheLaborContract,");
                sb.Append("a.ConfidentialityContract,a.CardNumber,a.CardBankName,a.CardBankAdd,a.CreateTime,a.CreateUser,");
                sb.Append("b.CompanyFullName as CompanyCodeName,a.CompanyCode,d.DepartmentName as DepartmentName,");
                sb.Append("d.DepartmentCode as DepartmentCode ");
                sb.Append("from TbUser a left join TbCompany b on a.CompanyCode=b.CompanyCode ");
                sb.Append("left join TbUserAndDepRelevance c on a.UserCode=c.UserCode ");
                sb.Append("left join TbDepartment d on c.DepartmentCode=d.DepartmentCode where");
                sb.Append(" a.UserCode = @name");
                return Db.Context.FromSql(sb.ToString()).AddInParameter(
                    "@name", DbType.String, UserCode).ToFirstDefault<TbUser>();
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region 根据用户编码修改个人信息
        public AjaxResult UserUpdateSubmitForm(TbUser user)
        {
            try
            {
                var data = Repository<TbUser>.First(m => m.UserCode == user.UserCode);
                data.UserName = user.UserName;
                data.Birthday = user.Birthday;
                data.UserClosed = user.UserClosed;
                data.PoliticalLandscape = user.PoliticalLandscape;
                data.MobilePhone = user.MobilePhone;
                data.Tell = user.Tell;
                data.Email = user.Email;
                data.QQ = user.QQ;
                data.WeChat = user.WeChat;
                data.RecruitmentSource = user.RecruitmentSource;
                data.PurchaseSocialSecurity = user.PurchaseSocialSecurity;
                data.TheLaborContract = user.TheLaborContract;
                data.ConfidentialityContract = user.ConfidentialityContract;
                data.CreateTime = user.CreateTime;
                data.CompanyCode = user.CompanyCode;
                data.DepartmentCode = user.DepartmentCode;
                var count = Repository<TbUser>.Update(data);
                if (count <= 0)
                {
                    return AjaxResult.Success("个人信息修改失败，请重试！");
                }
                return AjaxResult.Success("个人信息修改成功！");

            }
            catch (Exception)
            {
                return AjaxResult.Error("个人信息修改失败，请重试！");
            }
        }
        #endregion

        #region 确认原密码，如果原密码输入错误，禁止用户操作
        public bool SelectPwd(string UserCode, string Pwd)
        {
            var model = Repository<TbUser>.First(m => m.UserCode == UserCode);
            var password = PM.Common.Encryption.EncryptionFactory.Md5Encrypt(Pwd);
            if (model.UserPwd == password)
                return true;
            else
                return false;
        }
        #endregion

        #region 修改密码按钮事件
        public AjaxResult UpDatePwd(UserRequest result, string UserCode)
        {
            var pwd = PM.Common.Encryption.EncryptionFactory.Md5Encrypt(result.UserPwd);
            try
            {
                var model = Repository<TbUser>.First(m => m.UserCode == UserCode);
                model.UserPwd = pwd;
                var data = Repository<TbUser>.Update(model);
                if (data > 0)
                {
                    return AjaxResult.Success("密码修改成功，请重新进行登陆！");
                }
                return AjaxResult.Error("密码修改失败，请重试！");
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 查询此用户是否是离职人员
        public string UserClosedSelect(string userName)
        {
            try
            {
                //判断用户名是否存在
                if (!string.IsNullOrWhiteSpace(userName))
                {
                    string sql = @"SELECT UserClosed FROM TbUser WHERE UserCode=@UserName";
                    var data = Db.Context.FromSql(sql).AddInParameter("@UserName", DbType.String, userName).ToList<TbUser>();
                    var Closed = data[0].UserClosed;
                    if (Closed == "在职")
                    {
                        return "1";
                    }
                }
                return "-1";
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region 获取该用户下所有的项目组织机构

        public List<ProjectOrg> GetAllProjectOrg(string UserCode)
        {
            if (!string.IsNullOrWhiteSpace(UserCode))
            {
                List<ProjectOrg> list = new List<ProjectOrg>();
                string psql = @"select ur.ProjectId,p.ProjectName from TbUserRole ur left join TbProjectInfo p on ur.ProjectId=p.ProjectId where UserCode=@UserCode and Flag=0 group by ur.ProjectId,p.ProjectName";
                var pret = Db.Context.FromSql(psql).AddInParameter("@UserCode", DbType.String, UserCode).ToDataTable();
                if (pret != null && pret.Rows.Count > 0)
                {
                    for (int p = 0; p < pret.Rows.Count; p++)
                    {
                        ProjectOrg model = new ProjectOrg();
                        model.OrgId = pret.Rows[p]["ProjectId"].ToString();//id
                        model.OrgName = pret.Rows[p]["ProjectName"].ToString();
                        model.ProjectId = "0";//pid
                        model.ProjectName = "";
                        model.ProjectOrgAllId = pret.Rows[p]["ProjectId"].ToString();
                        model.ProjectOrgAllName = pret.Rows[p]["ProjectName"].ToString();
                        model.OrgType = "-1";
                        list.Add(model);
                    }
                }
                string sql = @"select ur.ProjectId,pro.ProjectName,ur.OrgId,cp.CompanyFullName as OrgName,cp.ParentCompanyCode,ur.OrgType from TbUserRole ur
left join TbCompany cp on ur.OrgId=cp.CompanyCode and ur.OrgType=cp.OrgType
left join TbProjectInfo pro on ur.ProjectId=pro.ProjectId
where UserCode=@UserCode and Flag=0 group by ur.ProjectId,pro.ProjectName,ur.OrgId,cp.CompanyFullName,cp.ParentCompanyCode,ur.OrgType;";
                var ret = Db.Context.FromSql(sql).AddInParameter("@UserCode", DbType.String, UserCode).ToDataTable();
                if (ret != null && ret.Rows.Count > 0)
                {
                    for (int i = 0; i < ret.Rows.Count; i++)
                    {
                        ProjectOrg model = new ProjectOrg();
                        string ProjectOrgAllId = "";
                        string ProjectOrgAllName = "";
                        string sql1 = @"with tab as
                                    (
                                     select CompanyCode,ParentCompanyCode,CompanyFullName,OrgType from TbCompany where CompanyCode=@CompanyCode
                                     union all
                                     select b.CompanyCode,b.ParentCompanyCode,b.CompanyFullName,b.OrgType
                                     from
                                      tab a,
                                      TbCompany b 
                                      where a.ParentCompanyCode=b.CompanyCode
                                    )
                                    select * from tab order by OrgType asc;";
                        var ret1 = Db.Context.FromSql(sql1).AddInParameter("@CompanyCode", DbType.String, ret.Rows[i]["OrgId"]).ToDataTable();
                        if (ret1 != null && ret1.Rows.Count > 0)
                        {
                            for (int j = 0; j < ret1.Rows.Count; j++)
                            {
                                if (ret1.Rows.Count == 1)
                                {
                                    ProjectOrgAllId += ret1.Rows[j]["CompanyCode"];
                                    ProjectOrgAllName += ret1.Rows[j]["CompanyFullName"];
                                }
                                else
                                {
                                    if (ret1.Rows.Count != j + 1)
                                    {
                                        ProjectOrgAllId += ret1.Rows[j]["CompanyCode"] + "/";
                                        ProjectOrgAllName += ret1.Rows[j]["CompanyFullName"] + "/";
                                    }
                                    else
                                    {
                                        ProjectOrgAllId += ret1.Rows[j]["CompanyCode"];
                                        ProjectOrgAllName += ret1.Rows[j]["CompanyFullName"];
                                    }
                                }
                            }
                        }
                        model.OrgId = ret.Rows[i]["OrgId"].ToString();//id
                        model.OrgName = ret.Rows[i]["OrgName"].ToString();
                        model.ProjectId = ret.Rows[i]["ProjectId"].ToString();//pid
                        model.ProjectName = ret.Rows[i]["ProjectName"].ToString();
                        model.ProjectOrgAllId = ProjectOrgAllId;
                        model.ProjectOrgAllName = ProjectOrgAllName;
                        model.OrgType = ret.Rows[i]["OrgType"].ToString();
                        list.Add(model);
                    }
                }
                return list;
            }
            else
            {
                return null;
            }

        }
        /// <summary>
        /// 切换项目组织机构重新保存切换后的组织机构信息到登陆信息中
        /// </summary>
        /// <param name="UserCode"></param>
        /// <param name="OrgId"></param>
        /// <param name="OrgName"></param>
        /// <param name="ProjectId"></param>
        /// <param name="OrgType"></param>
        /// <param name="ProjectOrgAllId"></param>
        /// <param name="ProjectOrgAllName"></param>
        /// <returns></returns>
        public CurrentUserInfo SaveProjectOrg(string UserCode, string OrgId, string OrgName, string ProjectId, string OrgType, string ProjectOrgAllId, string ProjectOrgAllName)
        {
            try
            {
                string sqlRole = @"select distinct ur.RoleCode as RoleId,r.RoleCode,r.RoleName from TbUserRole ur
left join TbUser u on ur.UserCode=u.UserId
left join TbRole r on ur.RoleCode=r.RoleId
where u.UserCode=@UserCode and ProjectId=@ProjectId and OrgId=@OrgId and ur.Flag=0";
                string strRole = "";
                DataTable dtRole = Db.Context.FromSql(sqlRole).AddInParameter("@UserCode", DbType.String, UserCode).AddInParameter("@ProjectId", DbType.String, ProjectId).AddInParameter("@OrgId", DbType.String, OrgId).ToDataTable();
                if (dtRole != null && dtRole.Rows.Count > 0)
                {
                    for (int i = 0; i < dtRole.Rows.Count; i++)
                    {
                        if (i == dtRole.Rows.Count - 1)
                        {
                            strRole += "'" + dtRole.Rows[i]["RoleId"] + "'";
                        }
                        else
                        {
                            strRole += "'" + dtRole.Rows[i]["RoleId"] + "',";
                        }
                    }
                }
                CurrentUserInfo cui = new CurrentUserInfo();
                cui.OrgType = OrgType;
                cui.CompanyId = OrgId;
                cui.ComPanyName = OrgName;
                cui.ProjectOrgAllId = ProjectOrgAllId;
                cui.ProjectOrgAllName = ProjectOrgAllName;
                if (OrgType == "1")
                {
                    cui.ProjectId = "";
                    cui.ProcessFactoryCode = OrgId;
                    cui.ProcessFactoryName = OrgName;
                }
                else
                {
                    cui.ProjectId = ProjectId;
                    //cui.ProcessFactoryCode = "";
                    //cui.ProcessFactoryName = "所有加工厂";
                    cui.ProcessFactoryCode = OperatorProvider.Provider.CurrentUser.ProcessFactoryCode;
                    cui.ProcessFactoryName = OperatorProvider.Provider.CurrentUser.ProcessFactoryName;
                }
                cui.RoleCode = strRole;
                cui.UserCode = OperatorProvider.Provider.CurrentUser.UserCode;
                cui.UserId = OperatorProvider.Provider.CurrentUser.UserId;
                cui.UserName = OperatorProvider.Provider.CurrentUser.UserName;
                cui.LoginTime = OperatorProvider.Provider.CurrentUser.LoginTime;
                cui.LoginToken = OperatorProvider.Provider.CurrentUser.LoginToken;
                cui.IsSystem = OperatorProvider.Provider.CurrentUser.IsSystem;
                cui.IsDriver = OperatorProvider.Provider.CurrentUser.IsDriver;
                return cui;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        /// <summary>
        /// 切换项目组织机构重新保存切换后的组织机构信息到登陆信息中
        /// </summary>
        /// <param name="UserCode"></param>
        /// <param name="OrgId"></param>
        /// <param name="OrgName"></param>
        /// <param name="ProjectId"></param>
        /// <param name="OrgType"></param>
        /// <param name="ProjectOrgAllId"></param>
        /// <param name="ProjectOrgAllName"></param>
        /// <returns></returns>
        public CurrentUserInfo AppSaveProjectOrg(CurrentUserInfo model)
        {
            try
            {
                string sqlRole = @"select distinct ur.RoleCode as RoleId,r.RoleCode,r.RoleName from TbUserRole ur
left join TbUser u on ur.UserCode=u.UserId
left join TbRole r on ur.RoleCode=r.RoleId
where u.UserCode=@UserCode and ProjectId=@ProjectId and OrgId=@OrgId and ur.Flag=0";
                string strRole = "";
                DataTable dtRole = Db.Context.FromSql(sqlRole).AddInParameter("@UserCode", DbType.String, model.UserCode)
                    .AddInParameter("@ProjectId", DbType.String, model.ProjectId)
                    .AddInParameter("@OrgId", DbType.String, model.CompanyId).ToDataTable();
                if (dtRole != null && dtRole.Rows.Count > 0)
                {
                    for (int i = 0; i < dtRole.Rows.Count; i++)
                    {
                        if (i == dtRole.Rows.Count - 1)
                        {
                            strRole += "'" + dtRole.Rows[i]["RoleId"] + "'";
                        }
                        else
                        {
                            strRole += "'" + dtRole.Rows[i]["RoleId"] + "',";
                        }
                    }
                }
                CurrentUserInfo cui = new CurrentUserInfo();
                cui.OrgType = model.OrgType;
                cui.CompanyId = model.CompanyId;
                cui.ComPanyName = model.CompanyId;
                cui.ProjectOrgAllId = model.ProjectOrgAllId;
                cui.ProjectOrgAllName = model.ProjectOrgAllName;
                if (model.OrgType == "1")
                {
                    cui.ProjectId = "";
                    cui.ProcessFactoryCode = model.CompanyId;
                    cui.ProcessFactoryName = model.CompanyId;
                }
                else
                {
                    cui.ProjectId = model.ProjectId;
                    //cui.ProcessFactoryCode = "";
                    //cui.ProcessFactoryName = "所有加工厂";
                    cui.ProcessFactoryCode = model.CompanyId;
                    cui.ProcessFactoryName = model.CompanyId;
                }
                cui.RoleCode = strRole;
                cui.UserCode = model.UserCode;
                cui.UserId = model.UserId;
                cui.UserName = model.UserName;
                cui.LoginTime = model.LoginTime;
                cui.LoginToken = model.LoginToken;
                cui.IsSystem = model.IsSystem;
                cui.IsDriver = model.IsDriver;
                return cui;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public PageModel GetAllProjectJgc(PageSearchRequest request)
        {
            //参数化
            List<Parameter> parameter = new List<Parameter>();
            //            string sql = @" select 1 as ID,'' as ProcessFactoryCode,'所有加工厂' as ProcessFactoryName 
            //                            union all
            //                            select id,CompanyCode,CompanyFullName from TbCompany where OrgType=1 ";
            string sql = @"select id as ID,CompanyCode as ProcessFactoryCode,CompanyFullName as ProcessFactoryName from TbCompany where OrgType=1 ";
            var model = Repository<TbCompany>.FromSqlToPageTable(sql, parameter, request.rows, request.page, "ID", "asc");
            return model;
        }

        /// <summary>
        /// 切换加工厂重新保存切换后的加工厂信息到登陆信息中
        /// </summary>
        /// <param name="ProcessFactoryCode"></param>
        /// <returns></returns>
        public CurrentUserInfo SaveProcessFactoryCode(string ProcessFactoryCode, string ProcessFactoryName)
        {
            try
            {
                CurrentUserInfo cui = new CurrentUserInfo();
                cui.OrgType = OperatorProvider.Provider.CurrentUser.OrgType;
                cui.CompanyId = OperatorProvider.Provider.CurrentUser.CompanyId;
                cui.ComPanyName = OperatorProvider.Provider.CurrentUser.ComPanyName;
                cui.ProjectOrgAllId = OperatorProvider.Provider.CurrentUser.ProjectOrgAllId;
                cui.ProjectOrgAllName = OperatorProvider.Provider.CurrentUser.ProjectOrgAllName;
                cui.ProjectId = OperatorProvider.Provider.CurrentUser.ProjectId;
                cui.RoleCode = OperatorProvider.Provider.CurrentUser.RoleCode;
                cui.UserCode = OperatorProvider.Provider.CurrentUser.UserCode;
                cui.UserId = OperatorProvider.Provider.CurrentUser.UserId;
                cui.UserName = OperatorProvider.Provider.CurrentUser.UserName;
                cui.LoginTime = OperatorProvider.Provider.CurrentUser.LoginTime;
                cui.LoginToken = OperatorProvider.Provider.CurrentUser.LoginToken;
                cui.IsSystem = OperatorProvider.Provider.CurrentUser.IsSystem;
                cui.IsDriver = OperatorProvider.Provider.CurrentUser.IsDriver;
                cui.ProcessFactoryCode = ProcessFactoryCode;
                cui.ProcessFactoryName = ProcessFactoryName;
                return cui;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        /// <summary>
        /// 切换加工厂重新保存切换后的加工厂信息到登陆信息中
        /// </summary>
        /// <param name="ProcessFactoryCode"></param>
        /// <returns></returns>
        public CurrentUserInfo AppSaveProcessFactoryCode(CurrentUserInfo model)
        {
            try
            {
                CurrentUserInfo cui = new CurrentUserInfo();
                cui = model;
                return cui;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        #endregion

        #region 得到本周第一天、最后一天
        /// <summary>  
        /// 得到本周第一天(以星期一为第一天)  
        /// </summary>  
        /// <param name="datetime"></param>  
        /// <returns></returns>  
        public DateTime GetWeekFirstDayMon(DateTime datetime)
        {
            //星期一为第一天  
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);

            //因为是以星期一为第一天，所以要判断weeknow等于0时，要向前推6天。  
            weeknow = (weeknow == 0 ? (7 - 1) : (weeknow - 1));
            int daydiff = (-1) * weeknow;

            //本周第一天  
            string FirstDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(FirstDay);
        }
        /// <summary>  
        /// 得到本周最后一天(以星期天为最后一天)  
        /// </summary>  
        /// <param name="datetime"></param>  
        /// <returns></returns>  
        public DateTime GetWeekLastDaySun(DateTime datetime)
        {
            //星期天为最后一天  
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);
            weeknow = (weeknow == 0 ? 7 : weeknow);
            int daydiff = (7 - weeknow);

            //本周最后一天  
            string LastDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(LastDay);
        }

        #endregion
        public List<TbArticle> GetJiaoLiu(PageSearchRequest psr)
        {
            try
            {
                var data = Db.Context.From<TbArticle>().Where(p => p.Type == 1)
                  .OrderByDescending(TbArticle._.ID)
                  .ToList();
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region 获取部门、角色

        public DataTable GetDeptInfo(string ProjectId, string OrgType)
        {
            string sql = "select * from TbDepartment where DepartmentProjectId='" + ProjectId + "' and DepartmentType='" + OrgType + "'";
            DataTable dt = Db.Context.FromSql(sql).ToDataTable();
            return dt;
        }

        public DataTable GetRoleInfo(string DepartmentId)
        {
            string sql = "select * from TbRole where DepartmentId='" + DepartmentId + "'";
            DataTable dt = Db.Context.FromSql(sql).ToDataTable();
            return dt;
        }
        public AjaxResult SavaUserRole(TbUserRole userRole)
        {
            if (userRole == null)
                return AjaxResult.Warning("参数错误");
            try
            {
                Repository<TbUserRole>.Insert(userRole);
                return AjaxResult.Success();
            }
            catch (Exception e)
            {
                var err = e.ToString();
                return AjaxResult.Error(err);
            }
        }
        #endregion


    }

    public class ProjectOrg
    {
        public string ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectOrgAllId { get; set; }
        public string ProjectOrgAllName { get; set; }
        public string OrgId { get; set; }
        public string OrgName { get; set; }
        public string OrgType { get; set; }
    }
    public class DetpOrRole
    {
        public string pid { get; set; }
        public string id { get; set; }
        public string Name { get; set; }
        public string OrgType { get; set; }
        public string ProjectId { get; set; }
    }
}
