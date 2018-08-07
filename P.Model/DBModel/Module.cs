﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace P.Model
{
    /// <summary>
    /// 模块---实体
    /// </summary>
    [Description("模块")]
    public class Module : EntityBase<int>
    {
        public Module()
        {
            this.Permissions = new List<Permission>();
            this.ChildModules = new List<Module>();
        }
        
        [Description("父模块Id")]
        public int? ParentId { get; set; }
        [Required]
        [Description("名称")]
        [StringLength(20)]
        public string Name { get; set; }
        [Required]
        [Description("链接地址")]
        [StringLength(50)]
        public string LinkUrl { get; set; }

        [Description("是否是菜单")]
        public bool IsMenu { get; set; }
        [Description("模块编号")]
        public int Code { get; set; }
        [Description("描述")]
        [StringLength(100)]
        public string Description { get; set; }
        [Description("是否激活")]
        public bool Enabled { get; set; }

        public virtual Module Parent { get; set; }

        public virtual ICollection<Module> ChildModules { get; set; }

        /// <summary>
        /// 权限集合
        /// </summary>
        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
