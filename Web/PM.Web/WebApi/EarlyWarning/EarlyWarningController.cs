using Dos.ORM;
using Newtonsoft.Json.Linq;
using PM.Business.EarlyWarning;
using PM.Common;
using PM.Common.Extension;
using PM.DataEntity;
using PM.DataEntity.EarlyWarning.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace PM.Web.WebApi.EarlyWarning
{
    public class EarlyWarningController : ApiController
    {
        private readonly TbFormEarlyWarningNodeInfoLogic _earlywarning = new TbFormEarlyWarningNodeInfoLogic();

        #region 预警信息列表
        /// <summary>
        /// 预警信息列表
        /// </summary>
        /// <param name="UserCode">登录人编号</param>
        /// <param name="Start">预警状态</param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetEarlyWarningInfoList(string UserCode, int Start)
        {
            try
            {
                var data = _earlywarning.GetEarlyWarningInfoList(UserCode, Start);
                return AjaxResult.Success(data).ToJsonApi();
            }
            catch (Exception)
            {
                return AjaxResult.Error("操作失败").ToJsonApi();
            }
        }

        /// <summary>
        /// 流程预警信息列表
        /// </summary>
        /// <param name="UserCode">登录人编号</param>
        /// <param name="Start">预警状态</param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetFlowEarlyWarningInfoList(string UserCode, int Start)
        {
            try
            {
                var data = _earlywarning.GetFlowEarlyWarningInfoList(UserCode, Start);
                return AjaxResult.Success(data).ToJsonApi();
            }
            catch (Exception)
            {
                return AjaxResult.Error("操作失败").ToJsonApi();
            }
        }
        /// <summary>
        /// 表单预警信息列表
        /// </summary>
        /// <param name="UserCode">登录人编号</param>
        /// <param name="Start">预警状态</param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetFormEarlyWarningInfoList(string UserCode, int Start)
        {
            try
            {
                var data = _earlywarning.GetFormEarlyWarningInfoList(UserCode, Start);
                return AjaxResult.Success(data).ToJsonApi();
            }
            catch (Exception)
            {
                return AjaxResult.Error("操作失败").ToJsonApi();
            }
        }
        #endregion

        /// <summary>
        /// 处理流程预警信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage HandleEarlyWarning(int ID, string EwType)
        {
            try
            {
                var data = _earlywarning.HandleEarlyWarning(ID, EwType);
                return AjaxResult.Success(data).ToJsonApi();
            }
            catch (Exception)
            {
                return AjaxResult.Error("操作失败").ToJsonApi();
            }
        }

        /// <summary>
        /// 配送预警信息列表
        /// </summary>
        /// <param name="UserCode">登录人编号</param>
        /// <param name="ProjectId">项目编号</param>
        /// <param name="Start">预警状态</param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetDeliveryEarlyWarningList(int Start, string UserCode, string ProjectId)
        {
            try
            {
                var data = _earlywarning.GetDeliveryEarlyWarningList(Start, UserCode, ProjectId);
                return AjaxResult.Success(data).ToJsonApi();
            }
            catch (Exception)
            {
                return AjaxResult.Error("操作失败").ToJsonApi();
            }
        }

    }
}
