﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;

using MaterialApp.Model;

namespace MaterialApp
{
    /// <summary>
    /// 日志帮助类（记录在数据库）
    /// </summary>
    public sealed class LogHelper
    {
        /// <summary>
        /// 将日志写入数据库
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="type">类型</param>
        /// <param name="source">来源</param>
        /// <param name="level">级别</param>
        /// <param name="comment">备注</param>
        public static void SaveLog(string title, string content = "", LogType type = LogType.System, LogSource source = LogSource.WebApp, LogLevel level = LogLevel.Info, string comment = "")
        {
            // 获取所有日志类型、来源、级别
            var query = Restrictions.In("Code", new string[] { "LogType", "LogSource", "LogLevel" });
            var session = NHibernateHelper.GetCurrentSession();
            var criteria = session.CreateCriteria<DicItem>().CreateCriteria("Dic").Add(query);
            var list = criteria.List<DicItem>();
            var typeID = list.Where(o => o.Dic != null && o.Dic.Code == "LogType" && o.Code == type.ToString()).Select(o => o.ID).FirstOrDefault();
            var sourceID = list.Where(o => o.Dic != null && o.Dic.Code == "LogSource" && o.Code == source.ToString()).Select(o => o.ID).FirstOrDefault();
            var levelID = list.Where(o => o.Dic != null && o.Dic.Code == "LogLevel" && o.Code == level.ToString()).Select(o => o.ID).FirstOrDefault();

            var log = new Log
            {
                AddTime = DateTime.Now,
                AddUser = null,
                Content = content,
                IP = "127.0.0.1",
                Level = new DicItem { ID = levelID },
                Comment = comment,
                Source = new DicItem { ID = sourceID },
                Status = 0,
                Title = title,
                Type = new DicItem { ID = typeID }
            };
            if (type == LogType.User) // 用户事件写入IP和用户信息
            {
                log.AddUser = AdminHelper.Admin;
                log.IP = HttpContext.Current.Request.UserHostAddress;
            }
            session.SaveOrUpdate(log);
            session.Flush();
        }

        /// <summary>
        /// 将致命消息写入数据库
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="type">类型</param>
        /// <param name="source">来源</param>
        /// <param name="comment">备注</param>
        public static void Fatal(string title, string content = "", LogType type = LogType.System, LogSource source = LogSource.WebApp, string comment = "")
        {
            SaveLog(title, content, type, source, LogLevel.Fatal, comment);
        }

        /// <summary>
        /// 将错误消息写入数据库
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="type">类型</param>
        /// <param name="source">来源</param>
        /// <param name="comment">备注</param>
        public static void Error(string title, string content = "", LogType type = LogType.System, LogSource source = LogSource.WebApp, string comment = "")
        {
            SaveLog(title, content, type, source, LogLevel.Error, comment);
        }

        /// <summary>
        /// 将警告消息写入数据库
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="type">类型</param>
        /// <param name="source">来源</param>
        /// <param name="comment">备注</param>
        public static void Warn(string title, string content = "", LogType type = LogType.System, LogSource source = LogSource.WebApp, string comment = "")
        {
            SaveLog(title, content, type, source, LogLevel.Warn, comment);
        }

        /// <summary>
        /// 将一般消息写入数据库
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="type">类型</param>
        /// <param name="source">来源</param>
        /// <param name="comment">备注</param>
        public static void Info(string title, string content = "", LogType type = LogType.System, LogSource source = LogSource.WebApp, string comment = "")
        {
            SaveLog(title, content, type, source, LogLevel.Info, comment);
        }

        /// <summary>
        /// 将一般消息写入数据库
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="type">类型</param>
        /// <param name="source">来源</param>
        /// <param name="comment">备注</param>
        public static void Log(string title, string content = "", LogType type = LogType.System, LogSource source = LogSource.WebApp, string comment = "")
        {
            SaveLog(title, content, type, source, LogLevel.Info, comment);
        }

        /// <summary>
        /// 将调试消息写入数据库
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="type">类型</param>
        /// <param name="source">来源</param>
        /// <param name="comment">备注</param>
        public static void Debug(string title, string content = "", LogType type = LogType.System, LogSource source = LogSource.WebApp, string comment = "")
        {
            SaveLog(title, content, type, source, LogLevel.Debug, comment);
        }
    }
}
