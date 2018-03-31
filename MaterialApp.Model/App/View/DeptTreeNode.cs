﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialApp.Model
{
    /// <summary>
    /// 机构树节点模型
    /// </summary>
    public class DeptTreeNode : Dept, ITreeNode
    {
        public bool? allowDrag { get; set; }

        public bool? allowDrop { get; set; }

        public bool? @checked { get; set; }

        public IList<ITreeNode> children { get; set; }

        public bool? expandable { get; set; }

        public bool? expanded { get; set; }

        public string icon { get; set; }

        public string iconCls { get; set; }

        public int id { get; set; }

        public bool? leaf { get; set; }

        public string qtip { get; set; }

        public string text { get; set; }
    }
}
