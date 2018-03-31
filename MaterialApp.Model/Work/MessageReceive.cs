﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.Attributes;

namespace MaterialApp.Model
{
    /// <summary>
    /// 消息接收
    /// </summary>
    [Class(Table = "MessageReceive")]
    public class MessageReceive
    {
        /// <summary>
        /// ID
        /// </summary>
        [Id(Name = "ID")]
        [Generator(Class = "identity")]
        public virtual int ID { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        [ManyToOne(ClassType = typeof(Message), Column = "MessageID", Lazy = Laziness.False)]
        public virtual Message Message { get; set; }

        /// <summary>
        /// 接收人
        /// </summary>
        [ManyToOne(ClassType = typeof(User), Column = "UserID", Lazy = Laziness.False)]
        public virtual User User { get; set; }

        /// <summary>
        /// 阅读时间
        /// </summary>
        [Property]
        public virtual DateTime? ReadTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [ManyToOne(ClassType = typeof(DicItem), Column = "Status", Lazy = Laziness.False)]
        public virtual DicItem Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Property]
        public virtual string Comment { get; set; }
    }
}
