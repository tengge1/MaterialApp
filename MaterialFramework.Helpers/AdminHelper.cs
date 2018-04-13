﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

using MaterialFramework.Model;
using NHibernate.Criterion;

namespace MaterialFramework
{
    /// <summary>
    /// 获取当前登录用户
    /// </summary>
    public class AdminHelper
    {
        /// <summary>
        /// 当前登录用户
        /// </summary>
        public static User Admin
        {
            get
            {
                var userID = HttpContext.Current.Session["__userID"];
                if (userID == null)
                {
                    return null;
                }

                // 获取当前用户
                var session = NHibernateHelper.GetCurrentSession();
                var criteria = session.CreateCriteria<User>();
                criteria.Add(Restrictions.Eq("ID", int.Parse(userID.ToString())));
                return criteria.UniqueResult<User>();
            }
        }
    }
}
