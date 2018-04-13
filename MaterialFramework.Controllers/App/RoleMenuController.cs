﻿using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using NHibernate.Criterion;

using MaterialFramework.Model;
using MaterialFramework.BLL;

namespace MaterialFramework.Controller
{
    /// <summary>
    /// 角色菜单控制器
    /// </summary>
    public class RoleMenuController : ApiBase
    {
        /// <summary>
        /// bll
        /// </summary>
        private RoleMenuBLL bll;

        /// <summary>
        /// 获取带有权限信息的菜单树
        /// </summary>
        /// <param name="PID"></param>
        /// <param name="roleID"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetChildNodes(int PID, int roleID = 0)
        {
            return Json(bll.GetChildNodes(PID, roleID));
        }

        /// <summary>
        /// 保存权限信息
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Save(RoleMenuModel p)
        {
            var result = bll.Save(p);
            return Json(result);
        }
    }
}
