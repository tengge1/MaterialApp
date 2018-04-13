﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.Attributes;

namespace MaterialFramework.Model
{
    /// <summary>
    /// 配置节点表
    /// </summary>
    [Class(Table = "AppConfigSection")]
    public class ConfigSection
    {
        /// <summary>
        /// ID
        /// </summary>
        [Id(Name = "ID")]
        [Generator(Class = "identity")]
        public virtual int ID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Property]
        public virtual string Name { get; set; }

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
