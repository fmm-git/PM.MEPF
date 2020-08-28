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
using System.Dynamic;
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
            if (!string.IsNullOrEmpty(request.ComponentCode))
            {
                whereSql += " and ComponentCode like @ComponentCode";
                parameters.Add(new SQLiteParameter("@ComponentCode", request.ComponentCode + "_%"));
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

            string sql = @"SELECT id,ComponentCode,Name,Length,Size,Area,Material,1 as Total from (
                                  SELECT id,
                                                          MAX(CASE name WHEN '族与类型' THEN value ELSE '' END) Name,
                                                          MAX(CASE name WHEN '尺寸' THEN value ELSE '' END) Size,
                                                          MAX(CASE name WHEN '长度' THEN value ELSE '' END) Length,
                                                          MAX(CASE name WHEN '面积' THEN value ELSE '' END) Area,
                                                          MAX(CASE name WHEN '材质' THEN value ELSE '' END) Material,
                                                          MAX(CASE name WHEN '模型构件编码' THEN value ELSE '' END) ComponentCode
                                   from model_property 
                                   GROUP BY id
                                  ) a
                                {0}";
            sql = string.Format(sql, whereSql);
            try
            {
                var dataList = _sqlite.ExecuteList<ProjectListAllModel>(sql, CommandType.Text, parameters.ToArray());
                if (dataList.Count < request.TotalCount)
                {
                    int lastIndex = request.ComponentCode.LastIndexOf('_');
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
        /// 获取项目结构树
        /// </summary>
        public List<model_tree> Getmodel_tree()
        {
            try
            {
                //获取所有模型信息数据
                var list = new List<model_tree>();
                string modelpropertyListStr = @"SELECT Major,System,Subsystem,MaterialType,Material,MAX(ComponentCode) ComponentCode from ( 
                                                    SELECT id,
                                                          MAX(CASE name WHEN '_专业' THEN value ELSE '' END) Major,
                                                          MAX(CASE name WHEN '_系统' THEN value ELSE '' END) System,
                                                          MAX(CASE name WHEN '_子系统' THEN value ELSE '' END) Subsystem,
                                                          MAX(CASE name WHEN '_设备材料类型' THEN value ELSE '' END) MaterialType,
                                                          MAX(CASE name WHEN '_设备材料名称' THEN value ELSE '' END) Material,
                                                          MAX(CASE name WHEN '模型构件编码' THEN value ELSE '' END) ComponentCode
                                   from model_property 
                                   GROUP BY id) GROUP BY Major,System,Subsystem,MaterialType,Material";
                var modelpropertyList = _sqlite.ExecuteList<modelData_tree>(modelpropertyListStr, CommandType.Text);
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
                    dataList = _groupBy.Select(g => (new model_tree { name = g.Key, id = g.Max(item => item.ComponentCode) })).ToList();
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
            SQLiteParameter[] parameters = { new SQLiteParameter("@Code", request.ComponentCode + "%") };
            try
            {
                //获取所有模型信息数据
                string modelpropertyListStr = @"SELECT DISTINCT id from model_property where name='模型构件编码' and value like @Code";
                var modelpropertyList = _sqlite.ExecuteList<model_tree>(modelpropertyListStr, CommandType.Text, parameters);
                if (modelpropertyList.Any())
                    ret = modelpropertyList.Select(p => p.id).ToList();
                return ret;
            }
            catch (Exception ex)
            {
                return new List<string>();
            }
        }

        
        public List<TbModel_Property> GetModelInfoList(string siteCode,string projectId,string fileName)
        {
            string sql = @"SELECT  
                        id,
                       '{0}' as SiteCode,
                       '{1}' as ProjectId,
                        MAX(CASE name WHEN '_专业' THEN value ELSE '' END) Major,
                        MAX(CASE name WHEN '尺寸' THEN value ELSE '' END) Size,
                        MAX(CASE name WHEN '_系统' THEN value ELSE '' END) System,
                        MAX(CASE name WHEN '_子系统' THEN value ELSE '' END) Subsystem,
                        MAX(CASE name WHEN '_设备材料类型' THEN value ELSE '' END) MaterialType,
                        MAX(CASE name WHEN '_设备材料名称' THEN value ELSE '' END) Material,
                        MAX(CASE name WHEN '模型构件编码' THEN value ELSE '' END) ComponentCode,
                        MAX(CASE name WHEN '族与类型' THEN value ELSE '' END) ComponentName,
                        MAX(CASE name WHEN '尺寸' THEN value ELSE '' END) Size,
                        MAX(CASE name WHEN '面积' THEN value ELSE '' END) Area,
                        MAX(CASE name WHEN '材质' THEN value ELSE '' END) Texture,
                        MAX(CASE name WHEN '车站编号' THEN value ELSE '' END) StationName,
                        MAX(case when name='长度' and propertygroup='尺寸标注' THEN value else '' END) Length,
                        MAX(case when name='风管长度' and propertygroup='尺寸标注' THEN value else '' END) FGLength,
                        MAX(CASE name WHEN '系统类型' THEN value ELSE '' END) SystemType,
                        MAX(CASE name WHEN '安装位置' THEN value ELSE '' END) Position,
                        MAX(CASE name WHEN '支架图纸编号' THEN value ELSE '' END) DrawingNo,
                       -- 0 as IsOrder,
                       '{2}' as FileName,
                        '' as Other1,
                        '' as Other2,
                        '' as Other3,
                        '' as Other4,
                        '' as Other5
                    FROM model_property
                    where name in('_专业','尺寸','_系统','_子系统','_设备材料类型','_设备材料名称','模型构件编码','族与类型','尺寸','面积','材质','车站编号','长度','风管长度','系统类型','安装位置','支架图纸编号')
                    GROUP BY id";
            sql = string.Format(sql, siteCode, projectId, fileName);
            try
            {
                var dataList = _sqlite.ExecuteList<TbModel_Property>(sql, CommandType.Text);
                return dataList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region Private

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

        #region 加工订单获取模型清单数据(没有用到)
        public PageModel GetProjectList(ProjectListRequest request)
        {
            string where = " where 1=1 ";
            if (!string.IsNullOrWhiteSpace(request.id))
            {
                StringBuilder sbmxid = new StringBuilder();
                string[] mxjgid = request.id.Split(',');
                for (int i = 0; i < mxjgid.Length; i++)
                {
                    if (i == mxjgid.Length - 1)
                    {
                        sbmxid.Append("'" + mxjgid[i] + "'");
                    }
                    else
                    {
                        sbmxid.Append("'" + mxjgid[i] + "',");
                    }
                }
                where += " and Tb.id in(" + sbmxid.ToString() + ")";
            }
            if (!string.IsNullOrWhiteSpace(request.mxgjbm))
            {
                where += " and Tb.mxgjbm like '%" + request.mxgjbm + "%'";
            }
            if (!string.IsNullOrWhiteSpace(request.gjmc))
            {
                where += " and Tb.sbclmc like '%" + request.gjmc + "%'";
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
                        MAX(CASE name WHEN '族与类型' THEN value ELSE '' END) sbclmc,
                        MAX(CASE name WHEN '模型构件编码' THEN value ELSE '' END) mxgjbm,
						'' as gjbm,
                        MAX(CASE name WHEN '尺寸' THEN value ELSE '' END) ggcc,
                        FLOOR(MAX(case when name='长度' and propertygroup='尺寸标注' THEN value when name='风管长度' and propertygroup='尺寸标注' THEN value else '' END )) cd,
                        MAX(CASE name WHEN '面积' THEN value ELSE '' END) mj
                    FROM model_property
                    where name in('车站编号','_专业','_系统','_子系统','系统类型','材质','_设备材料类型','族与类型','模型构件编码','尺寸','长度','面积') 
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

        public DataTable GetComponentDetails(ProjectListRequest request)
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
            var data = _sqlite.ExecuteDataTable(sql + where, CommandType.Text,cmdParms);
            return data;
        }

        #endregion
    }
}
