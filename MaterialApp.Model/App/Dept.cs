﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.Attributes;

namespace MaterialApp.Model
{
    /// <summary>
    /// 组织机构表
    /// </summary>
    [Class(Table = "AppDept")]
    public class Dept
    {
        /// <summary>
        /// ID
        /// </summary>
        [Id(Name = "ID")]
        [Generator(Class = "identity")]
        public virtual int ID { get; set; }

        /// <summary>
        /// 父机构
        /// </summary>
        [ManyToOne(ClassType = typeof(Dept), Column = "PID", Lazy = Laziness.False)]
        public virtual Dept PDept { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [Property]
        public virtual string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Property]
        public virtual string Name { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [ManyToOne(ClassType = typeof(DicItem), Column = "TypeID", Lazy = Laziness.False)]
        public virtual DicItem Type { get; set; }

        /// <summary>
        /// 添加人
        /// </summary>
        [ManyToOne(ClassType = typeof(User), Column = "AddUserID", Lazy = Laziness.False)]
        public virtual User AddUser { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        [Property]
        public virtual DateTime? AddTime { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Property]
        public virtual int? Sort { get; set; }

        /// <summary>
        /// 状态（1-启用，0-禁用）
        /// </summary>
        [Property]
        public virtual int? Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Property]
        public virtual string Comment { get; set; }
    }
}
