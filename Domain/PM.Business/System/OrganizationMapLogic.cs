using Dos.ORM;
using PM.Common;
using PM.DataAccess.DbContext;
using PM.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM.Business.System
{
    public class OrganizationMapLogic
    {
        public AjaxResult GetOrganizationMapList(int type, string projectId)
        {
            var retData = new List<TbOrganizationMap>();
            var where = new Where<TbOrganizationMap>();
            if (type > 0)
                where.And(p => p.Type == type);
            if (!string.IsNullOrEmpty(projectId))
                where.And(p => p.ProjectId == projectId);

            retData = Db.Context.From<TbOrganizationMap>()
                    .Select(
                      TbOrganizationMap._.All)
                  .Where(where).ToList();
            return AjaxResult.Success(retData);
        }
    }
}
