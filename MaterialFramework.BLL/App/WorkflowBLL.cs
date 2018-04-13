﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MaterialFramework.Model;
using MaterialFramework.DAL;
using NHibernate.Criterion;

namespace MaterialFramework.BLL
{
    /// <summary>
    /// 工作流BLL
    /// </summary>
    public class WorkflowBLL : BaseBLL<Workflow>
    {
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="firstResult"></param>
        /// <param name="maxResults"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public ListResult<Workflow> List(int firstResult, int maxResults, string name = "")
        {
            ICriterion query = null;
            if (!string.IsNullOrEmpty(name))
            {
                query = Restrictions.Like("Name", name, MatchMode.Anywhere);
            }
            int total = 0;
            return base.List(firstResult, maxResults, out total, query);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override Result Add(Workflow model)
        {
            model.AddTime = DateTime.Now;
            model.AddUser = AdminHelper.Admin;
            model.ID = 0;
            model.Version = 1;
            return base.Add(model);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override Result Edit(Workflow model)
        {
            var model1 = dal.Get(model.ID);
            if (model1 == null)
            {
                return new Result(300, "数据获取失败！");
            }
            model1.Comment = model.Comment;
            model1.Name = model.Name;
            model1.Version++;
            return base.Edit(model1);
        }

        /// <summary>
        /// 保存工作流数据
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        public Result SaveXml(int ID, string xml)
        {
            var model = dal.Get(ID);
            if (model == null)
            {
                return new Result(300, "工作流不存在！");
            }
            model.Data = xml;

            return base.Edit(model);
        }

        /// <summary>
        /// 获取xml
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string GetXml(int ID)
        {
            var model = dal.Get(ID);
            if (model == null)
            {
                return "";
            }
            return model.Data;
        }
    }
}
