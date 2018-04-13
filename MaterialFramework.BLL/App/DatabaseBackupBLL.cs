﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using NHibernate.Criterion;

using MaterialFramework.Model;

namespace MaterialFramework.BLL
{
    /// <summary>
    /// 数据库备份BLL
    /// </summary>
    public class DatabaseBackupBLL : BaseBLL<DatabaseBackup>
    {
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="firstResult"></param>
        /// <param name="maxResults"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public ListResult<DatabaseBackup> List(int firstResult, int maxResults, string name = null)
        {
            ICriterion query = null;
            if (!string.IsNullOrEmpty(name))
            {
                query = Restrictions.Like("Name", name, MatchMode.Anywhere);
            }
            var total = 0;
            return base.List(firstResult, maxResults, out total, query);
        }

        /// <summary>
        /// 删除数据库备份
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public override Result Delete(int ID)
        {
            var backup = dal.Get(ID);
            if (backup == null)
            {
                return new Result(300, "数据不存在！");
            }
            var path = HttpContext.Current.Server.MapPath("/App_Data/Backup");
            path = path + "\\" + backup.FileName;

            // 删除文件
            if (File.Exists(path))
            {
                try
                {
                    File.Delete(path);
                }
                catch (Exception e)
                {
                    var logger = FileLogHelper.GetLogger(this.GetType());
                    logger.Error(e.Message, e);
                    return new Result(300, "文件删除失败！");
                }
            }

            return base.Delete(ID);
        }

        /// <summary>
        /// 数据库备份
        /// </summary>
        /// <returns></returns>
        public Result Backup()
        {
            var now = DateTime.Now;
            var dbName = ConfigHelper.Get("DatabaseName");
            dbName = dbName == null ? "MaterialFramework" : dbName;
            var fileName = string.Format("MaterialFramework{0}.bak", now.ToString("yyyyMMddHHmmss"));
            var path = HttpContext.Current.Server.MapPath("/App_Data/Backup");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var sql = string.Format(@"backup database [{0}] to disk='{1}\{2}'", dbName, path, fileName);

            var model = new DatabaseBackup
            {
                ID = 0,
                Name = "数据库" + now.ToString("yyyy-MM-dd") + "备份",
                FileName = fileName,
                AddUser = AdminHelper.Admin,
                AddTime = now,
                IsCurrent = false
            };
            var result = dal.Save(model);
            if (result)
            {
                // 先添加数据再备份，否则还原后该记录消失
                var helper = new SqlHelper();
                helper.ExecuteSql(sql);
                return new Result(200, "备份成功！");
            }
            else
            {
                return new Result(300, "备份失败！");
            }
        }

        /// <summary>
        /// 数据库还原
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Result Restore(int ID)
        {
            var model = dal.Get(ID);
            if (model == null)
            {
                return new Result(300, "数据不存在！");
            }
            var path = HttpContext.Current.Server.MapPath("/App_Data/Backup");
            path = path + "\\" + model.FileName;
            if (!File.Exists(path))
            {
                return new Result(300, "备份文件不存在！");
            }
            var dbName = ConfigHelper.Get("DatabaseName");
            dbName = dbName == null ? "MaterialFramework" : dbName;
            var sql = string.Format("use master; alter database [{0}] set single_user with rollback immediate; restore database [{0}] from disk='{1}' with replace;alter database [{0}] set multi_user;", dbName, path);
            var helper = new SqlHelper();
            helper.ExecuteSql(sql);
            return new Result(200, "还原数据库成功！");
        }
    }
}
