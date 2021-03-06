﻿using Dos.ORM;
using PM.Common;
using PM.DataAccess.DbContext;
using PM.DataEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PM.Business
{
    public class TbRoleLogic
    {
        #region 新增数据

        /// <summary>
        /// 新增数据(单条)
        /// </summary>
        public AjaxResult Insert(TbRole model, bool isApi = false)
        {
            if (model == null)
                return AjaxResult.Warning("参数错误");
            try
            {
                model.State = "启用";
                model.RoleDetail = "1";
                var count = Repository<TbRole>.Insert(model);
                if (count > 0)
                {
                    return AjaxResult.Success();
                }
                return AjaxResult.Error();
            }
            catch (Exception)
            {
                return AjaxResult.Error();
            }

        }

        /// <summary>
        /// 新增数据(单条)从BIM获取数据
        /// </summary>
        public AjaxResult InsertNew1(List<TbRole> model, bool isApi = false)
        {
            if (model == null)
                return AjaxResult.Warning("参数错误");

            try
            {
                List<TbRole> data = Db.Context.From<TbRole>().Select(TbRole._.All).Where(a => a.RoleDetail == "0").ToList();
                using (DbTrans trans = Db.Context.BeginTransaction())
                {
                    //先删除原来的表
                    Repository<TbRole>.Delete(trans, data, isApi);
                    //插入从BM那边取过来的数据
                    Repository<TbRole>.Insert(trans, model, isApi);
                    trans.Commit();//提交事务
                    return AjaxResult.Success();
                }
            }
            catch (Exception)
            {
                return AjaxResult.Error();
            }

        }

        #endregion

        #region 修改数据

        /// <summary>
        /// 修改数据(单条)
        /// </summary>
        public AjaxResult Update(TbRole model)
        {
            if (model == null)
                return AjaxResult.Warning("参数错误");
            try
            {
                Repository<TbRole>.Update(model);
                return AjaxResult.Success();
            }
            catch (Exception)
            {
                return AjaxResult.Error();
            }

        }

        #endregion

        #region 删除数据

        /// <summary>
        /// 删除数据
        /// </summary>
        public AjaxResult Delete(string RoleId, bool isApi = false)
        {
            try
            {
                //判断该部门下是否存在角色与菜单
                bool isUData = Repository<TbUserRole>.Any(a => a.RoleCode == RoleId);
                bool isMData = Repository<TbRoleMenu>.Any(a => a.RoleCode == RoleId);
                using (DbTrans trans = Db.Context.BeginTransaction())
                {
                    Repository<TbRole>.Delete(trans, t => t.RoleId == RoleId);
                    if (isUData) Repository<TbUserRole>.Delete(trans, t => t.RoleCode == RoleId);
                    if (isMData) Repository<TbRoleMenu>.Delete(trans, t => t.RoleCode == RoleId);
                    trans.Commit();//提交事务
                    return AjaxResult.Success();
                }
            }
            catch (Exception)
            {
                return AjaxResult.Error();
            }

        }

        #endregion

        #region 获取数据

        public TbRole FindEntity(string RoleCode)
        {
            var model = Repository<TbRole>.First(d => d.RoleCode == RoleCode);
            return model;
        }
        /// <summary>
        /// 获取数据列表(分页)
        /// </summary>
        public PageModel GetDataListForPage(TbRoleRequset request)
        {
            //组装查询语句
            #region 模糊搜索条件

            var where = new Where<TbRole>();
            if (!string.IsNullOrWhiteSpace(request.RoleName))
            {
                where.And(p => p.RoleName.Contains(request.RoleName));
            }
            //if (!string.IsNullOrWhiteSpace(request.DepartmentId))
            //{
            where.And(p => p.DepartmentId == request.DepartmentId);
            //}
            #endregion

            try
            {
                var ret = Db.Context.From<TbRole>()
              .Select(TbRole._.All).Where(where).OrderByDescending(d => d.RoleId).ToPageList(request.rows, request.page);
                return ret;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取不分页数据列表
        /// </summary>
        public List<TbRole> GetNoPageGridList()
        {
            var where = new Where<TbRole>();
            //where.And(d => d.State == "启用");
            return Repository<TbRole>.Query(where, d => d.RoleCode, "asc").ToList();
        }
        #endregion

        public string NextRoleCode(string DepartmentId, string orgType)
        {
            string RoleCode = "";
            string sql = @"select MAX(RoleCode) from TbRole where DepartmentId=@DepartmentId";
            var dt = Db.Context.FromSql(sql)
                .AddInParameter("@DepartmentId", DbType.String, DepartmentId).ToDataTable();
            if (dt != null && dt.Rows.Count > 0)
                if (!string.IsNullOrWhiteSpace(dt.Rows[0][0].ToString()))
                {
                    var length = dt.Rows[0][0].ToString().Length;
                    string Code1 = dt.Rows[0][0].ToString().Substring(0, 1);
                    string Code2 = dt.Rows[0][0].ToString().Substring(1, length - 1);
                    int intNum = Int32.Parse(Code2) + 10;
                    RoleCode = Code1 + intNum;
                }
                else
                {
                    if (orgType == "2")
                    {
                        RoleCode = "B10010010";
                    }
                    else if (orgType == "3")
                    {
                        RoleCode = "B20010010";
                    }
                    else if (orgType == "4")
                    {
                        RoleCode = "B30010010";
                    }
                    else if (orgType == "5")
                    {
                        RoleCode = "B40010010";
                    }
                    else
                    {
                        RoleCode = "B50010010";
                    }
                }
            return RoleCode;
        }
    }
}
