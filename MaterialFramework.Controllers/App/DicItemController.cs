﻿using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

using MaterialFramework.Model;
using MaterialFramework.BLL;

namespace MaterialFramework.Controller
{
    /// <summary>
    /// 字典项目控制器
    /// </summary>
    public class DicItemController : ApiBase
    {
        /// <summary>
        /// bll
        /// </summary>
        private DicItemBLL bll;

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="PID"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult List(int PID)
        {
            var result = bll.List(PID);
            return Json(result);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Add(DicItem model)
        {
            var result = bll.Add(model);
            return Json(result);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(DicItem model)
        {
            var result = bll.Edit(model);
            return Json(result);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Delete(int ID)
        {
            var result = bll.Delete(ID);
            return Json(result);
        }
    }
}