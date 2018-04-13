﻿using System;
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
    /// 菜单控制器
    /// </summary>
    public class MenuController : ApiBase
    {
        /// <summary>
        /// bll
        /// </summary>
        private MenuBLL bll;

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult List()
        {
            return Json(bll.List());
        }

        /// <summary>
        /// 获取子节点
        /// </summary>
        /// <param name="PID"></param>
        /// <param name="authorize"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetChildNodes(int PID, bool? authorize = false)
        {
            var result = bll.GetChildNodes(PID, authorize);
            return Json(result);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Add(Menu model)
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
        public JsonResult Edit(Menu model)
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
