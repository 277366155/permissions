﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace P.Model
{
    /// <summary>
    /// 用户组--实体
    /// </summary>
    [Description("用户组")]
    public class UserGroup : EntityBase<int>
    {
        public UserGroup()
        {
            this.Roles = new List<Role>();
            this.Users = new List<User>();
        }

        [Required]
        [Description("用户组名称")]
        [StringLength(20)]
        public string GroupName { get; set; }

        [Description("描述")]
        [StringLength(100)]
        public string Description { set; get; }

        [Display(Name = "排序")]
        [RegularExpression(@"\d+", ErrorMessage = "排序必须是数字")]
        [Range(1, 99999)]
        public int OrderSort { get; set; }

        [Description("是否激活")]
        public bool Enabled { get; set; }
        /// <summary>
        /// 角色集合
        /// </summary>
        public virtual ICollection<Role> Roles { get; set; }

        /// <summary>
        /// 用户集合
        /// </summary>
        public virtual ICollection<User> Users { get; set; }
    }
}
