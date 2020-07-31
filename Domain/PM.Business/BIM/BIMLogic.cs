using Dos.ORM;
using PM.Common;
using PM.DataAccess.DbContext;
using PM.DataEntity;
using PM.DataEntity.BIM;
using PM.DataEntity.Production.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM.Business.BIM
{
    public class BIMLogic
    {
        private readonly SQLiteHelper _sqlite = null;
        public BIMLogic(string dbName)
        {
            _sqlite = new SQLiteHelper("Data Source=" + dbName);
        }

        #region 新增数据

        /// <summary>
        /// 新增数据
        /// </summary>
        public AjaxResult Insert()
        {
            try
            {
                SQLiteParameter[] parameters = {
                new SQLiteParameter("@OSVersion", "")};
                int count = _sqlite.ExecuteNonQuery("Select * From model_property", CommandType.Text, parameters);
                return AjaxResult.Success();
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
        public AjaxResult Update()
        {
            try
            {
                SQLiteParameter[] parameters = {
                new SQLiteParameter("@OSVersion", "")};
                int count = _sqlite.ExecuteNonQuery("Select * From model_property", CommandType.Text, parameters);
                return AjaxResult.Success();
            }
            catch (Exception ex)
            {
                return AjaxResult.Error();
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
                SQLiteParameter[] parameters = {
                new SQLiteParameter("@OSVersion", "")};
                int count = _sqlite.ExecuteNonQuery("Select * From model_property", CommandType.Text, parameters);
                return AjaxResult.Success();
            }
            catch (Exception ex)
            {
                return AjaxResult.Error();
            }
        }

        #endregion

        #region 获取数据

        /// <summary>
        /// 获取数据列表(项目清单_分页)
        /// </summary>
        public PageModel GetDataListForPage(BIMRequest request)
        {
            #region 查询语句

            string whereSql = "where 1=1";
            List<SQLiteParameter> parameters = new List<SQLiteParameter>();
            if (!string.IsNullOrEmpty(request.Material))
            {
                whereSql += " and Material=@Material";
                parameters.Add(new SQLiteParameter("@Material", request.Material));
            }
            #endregion

            string sql = @"SELECT Major,System,Subsystem,MaterialType,Material,Size,count(*) as Total, MAX(ComponentCode) as ComponentCode
                        from (
                          SELECT 
                        MAX(CASE name WHEN '尺寸' THEN value ELSE '' END) Size,
                        MAX(CASE name WHEN '_系统' THEN value ELSE '' END) System,
                        MAX(CASE name WHEN '_子系统' THEN value ELSE '' END) Subsystem,
                        MAX(CASE name WHEN '_设备材料类型' THEN value ELSE '' END) MaterialType,
                        MAX(CASE name WHEN '_设备材料名称' THEN value ELSE '' END) Material,
                        MAX(CASE name WHEN '模型构件编码' THEN value ELSE '' END) ComponentCode,
                        MAX(CASE name WHEN '_专业' THEN value ELSE '' END) Major
                    FROM model_property
                    where name in('尺寸','_系统','_子系统','_设备材料类型','_设备材料名称','模型构件编码','_专业')
                    GROUP BY id
                    ) as a
                    {0}
                    GROUP BY Major,System,Subsystem,MaterialType,Material,Size ";
            sql = string.Format(sql, whereSql);
            try
            {
                var ret = _sqlite.SelectPaging<ProjectListAllModel>(sql, request.rows, request.page, parameters.ToArray());
                List<ProjectListAllModel> dataList = ret.rows as List<ProjectListAllModel>;
                var codeList = dataList.Select(p => p.ComponentCodeShow).ToList();
                //加工订单信息
                //var orderCodeList = Repository<TbWorkOrder>.Query(p => p.SiteCode == request.SiteCode).Select(p => p.OrderCode).ToList();
                var orderDetailList = Repository<TbWorkOrderDetail>.Query(p => p.MxGjBm.In(codeList)).ToList();
                //模型附件信息
                var modelOtherInfoList = Repository<TbModelOtherInfo>.Query(p => p.ComponentCode.In(codeList)).ToList();
                dataList.ForEach(x =>
                {
                    var orderData = orderDetailList.Where(p => p.MxGjBm == x.ComponentCodeShow).ToList();
                    x.Processing = orderData.Where(p => p.ComponentStrat == "加工中").Count();
                    x.ProcessComplete = orderData.Where(p => p.ComponentStrat == "加工完成").Count();
                    x.InstallComplete = orderData.Where(p => p.ComponentStrat == "安装完成").Count();
                    var modelOther = modelOtherInfoList.Where(p => p.ComponentCode == x.ComponentCodeShow).FirstOrDefault();
                    if (modelOther != null)
                    {
                        x.PlanTime = modelOther.PlanTime;
                        x.ActualTime = modelOther.ActualTime;
                        x.Remark = modelOther.Remark;
                    }
                });

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
        public PageModel GetDataItemListForPage(BIMRequest request)
        {
            #region 查询语句

            string whereSql = "where 1=1";
            List<SQLiteParameter> parameters = new List<SQLiteParameter>();
            if (!string.IsNullOrEmpty(request.Material))
            {
                whereSql += " and Material=@Material";
                parameters.Add(new SQLiteParameter("@Material", request.Material));
            }
            if (!string.IsNullOrEmpty(request.ComponentCode))
            {
                whereSql += " and ComponentCode like @ComponentCode";
                parameters.Add(new SQLiteParameter("@ComponentCode", request.ComponentCode + "_%"));
            }
            if (!string.IsNullOrEmpty(request.Size))
            {
                whereSql += " and Size=@Size";
                parameters.Add(new SQLiteParameter("@Size", request.Size));
            }
            #endregion

            string sql = @"SELECT id,ComponentCode,Name,Length,1 as Total from (
                                  SELECT id,
                                                          MAX(CASE name WHEN '族与类型' THEN value ELSE '' END) Name,
                                                          MAX(CASE name WHEN '尺寸' THEN value ELSE '' END) Size,
                                                          MAX(CASE name WHEN '长度' THEN value ELSE '' END) Length,
                                                          MAX(CASE name WHEN '模型构件编码' THEN value ELSE '' END) ComponentCode
                                   from model_property 
                                   GROUP BY id
                                  ) a
                                {0}";
            sql = string.Format(sql, whereSql);
            try
            {
                var ret = _sqlite.SelectPaging<ProjectListItemModel>(sql, request.rows, request.page, parameters.ToArray());
                List<ProjectListItemModel> dataList = ret.rows as List<ProjectListItemModel>;
                var idList = dataList.Select(p => p.id).ToList();
                var codeList = dataList.Select(p => p.ComponentCode).ToList();
                //加工订单信息
                //var orderCodeList = Repository<TbWorkOrder>.Query(p => p.SiteCode == request.SiteCode).Select(p => p.OrderCode).ToList();
                var orderDetailList = Repository<TbWorkOrderDetail>.Query(p => p.MxGjId.In(idList)).ToList();
                //模型附件信息
                var modelOtherInfoList = Repository<TbModelOtherInfo>.Query(p => p.ComponentCode.In(codeList)).ToList();
                dataList.ForEach(x =>
                {
                    var orderData = orderDetailList.Where(p => p.MxGjId == x.id).ToList();
                    x.Processing = orderData.Where(p => p.ComponentStrat == "加工中").Count();
                    x.ProcessComplete = orderData.Where(p => p.ComponentStrat == "加工完成").Count();
                    x.InstallComplete = orderData.Where(p => p.ComponentStrat == "安装完成").Count();
                    var modelOther = modelOtherInfoList.Where(p => p.ComponentCode == x.ComponentCode).FirstOrDefault();
                    if (modelOther != null)
                    {
                        x.PlanTime = modelOther.PlanTime;
                        x.ActualTime = modelOther.ActualTime;
                        x.Remark = modelOther.Remark;
                    }
                });

                return ret;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取项目结构树
        /// </summary>
        public List<model_tree> Getmodel_tree()
        {
            try
            {
                //获取所有模型信息数据
                var list = new List<model_tree>();
                string modelpropertyListStr = @"SELECT id,value as name,name as pid from model_property where name in('模型构件编码','_专业','_系统','_子系统','_设备材料类型','_设备材料名称') ";
                var modelpropertyList = _sqlite.ExecuteList<model_tree>(modelpropertyListStr, CommandType.Text);
                var cList = modelpropertyList.Where(p => p.pid == "模型构件编码").Distinct().ToList();
                if (cList.Any())
                {
                    cList.ForEach(x =>
                    {
                        var codeArry = x.name.Split('_');
                        var dataList = modelpropertyList.Where(p => p.id == x.id).ToList();
                        //专业
                        GetTreeData(codeArry, 2, dataList, list);
                        //系统      
                        GetTreeData(codeArry, 3, dataList, list);
                        //子系统    
                        GetTreeData(codeArry, 4, dataList, list);
                        //材料类型  
                        GetTreeData(codeArry, 5, dataList, list);
                        //材料名称  
                        GetTreeData(codeArry, 6, dataList, list);
                    });
                }
                return list;
            }
            catch (Exception ex)
            {
                return new List<model_tree>();
            }
        }

        #endregion

        #region Private

        /// <summary>
        /// 模型项目结构树
        /// </summary>
        /// <param name="codeArry"></param>
        /// <param name="type"></param>
        /// <param name="listData"></param>
        /// <param name="list"></param>
        private void GetTreeData(string[] codeArry, int type, List<model_tree> listData, List<model_tree> list)
        {
            string str = "";
            switch (type)
            {
                case 2:
                    str = "_专业";
                    break;
                case 3:
                    str = "_系统";
                    break;
                case 4:
                    str = "_子系统";
                    break;
                case 5:
                    str = "_设备材料类型";
                    break;
                case 6:
                    str = "_设备材料名称";
                    break;
                default:
                    break;
            }
            var data = listData.FirstOrDefault(p => p.pid == str);
            if (data != null)
            {
                var levelData = CreatLevelId(codeArry, type);
                var model = list.FirstOrDefault(p => p.id == levelData.Item2);
                if (model == null)
                {
                    data.id = levelData.Item2;
                    data.pid = levelData.Item1;
                    list.Add(data);
                }
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
            for (int i = 2; i < type + 1; i++)
            {
                if (i > 2)
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

        #endregion

        #region 加工订单获取模型清单数据
        public PageModel GetProjectList(ProjectListRequest request)
        {
            string where = " where 1=1 ";
            if (!string.IsNullOrWhiteSpace(request.id)) 
            {
                where += " and Tb.id in('"+request.id+"')";
            }
            if (!string.IsNullOrWhiteSpace(request.mxgjbm))
            {
                where += " and Tb.mxgjbm like '%" + request.mxgjbm + "%'";
            }
            if (!string.IsNullOrWhiteSpace(request.sbclmc))
            {
                where += " and Tb.sbclmc like '%" + request.sbclmc + "%'";
            }
            StringBuilder sb = new StringBuilder();
            //获取加工订单明细中已经存在了模型构建id
            string sqlgjid = "select MxGjId from TbWorkOrderDetail";
            var dt = Db.Context.FromSql(sqlgjid).ToList<string>();
            if (dt.Count > 0)
            {
                for (int i = 0; i < dt.Count; i++)
                {
                    if (i == dt.Count - 1)
                    {
                        sb.Append("'" + dt[i] + "'");
                    }
                    else
                    {
                        sb.Append("'" + dt[i] + "'" + ",");
                    }
                }
                if (sb.Length > 0)
                {
                    where += " and Tb.id not in(" + sb + ")";
                }
            }
            string sql = @"select * from (SELECT id,
                        MAX(CASE name WHEN '车站编号' THEN value ELSE '' END) zzbh,
                        MAX(CASE name WHEN '_专业' THEN value ELSE '' END) zy,
                        MAX(CASE name WHEN '_系统' THEN value ELSE '' END) dxt,
                        MAX(CASE name WHEN '_子系统' THEN value ELSE '' END) zxt,
                        MAX(CASE name WHEN '系统类型' THEN value ELSE '' END) xtlx,
                        MAX(CASE name WHEN '材质' THEN value ELSE '' END) cz,
                        MAX(CASE name WHEN '_设备材料类型' THEN value ELSE '' END) sbcllx,
                        MAX(CASE name WHEN '_设备材料名称' THEN value ELSE '' END) sbclmc,
                        MAX(CASE name WHEN '模型构件编码' THEN value ELSE '' END) mxgjbm,
                        MAX(CASE name WHEN '模型构件编码' THEN reverse(substr(reverse(value),1,charindex('_',reverse(value)) - 1)) ELSE '' END) gjbm,
                        MAX(CASE name WHEN '尺寸' THEN value ELSE '' END) ggcc,
                        FLOOR(MAX(CASE name WHEN '长度' THEN value ELSE '' END)) cd
                    FROM model_property
                    where name in('车站编号','_专业','_系统','_子系统','系统类型','材质','_设备材料类型','_设备材料名称','模型构件编码','模型构件编码','尺寸','长度') 
                    GROUP BY id) Tb";
            //参数化
            SQLiteParameter[] cmdParms ={
                    //new SQLiteParameter("@mxgjbm", DbType.String,request.mxgjbm)
                                        };
            var data = _sqlite.SelectPaging(sql + where, request.rows, request.page, cmdParms);
            return data;
        }

        #endregion

        #region 接口

        public PageModel GetComponentDetails(ProjectListRequest request)
        {
            string where = " where 1=1 ";
            if (!string.IsNullOrWhiteSpace(request.id))
            {
                where += " and id='" + request.id + "'";
            }
            string sql = @"select * from model_property ";
            //参数化
            SQLiteParameter[] cmdParms ={
                    //new SQLiteParameter("@mxgjbm", DbType.String,request.mxgjbm)
                                        };
            var data = _sqlite.SelectPaging(sql + where, request.rows, request.page, cmdParms);
            return data;
        }

        #endregion
    }
}
