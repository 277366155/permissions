﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace P.Model
{
    [Description("角色信息")]
    public class Role : EntityBase<int>
    {
        public Role()
        {
            this.Users = new List<User>();
            this.UserGroups = new List<UserGroup>();
            this.Permissions = new List<Permission>();
        }

        [Required]
        [Display(Name = "角色名称")]
        [StringLength(20)]
        public string RoleName { get; set; }

        [Display(Name = "描述")]
        [StringLength(100)]
        public string Description { get; set; }
        [Display(Name = "是否激活")]
        public bool Enabled { get; set; }
        [Display(Name = "排序")]
        [RegularExpression(@"\d+", ErrorMessage = "排序必须是数字")]
        [Range(1, 99999)]
        public int OrderSort { get; set; }

        /// <summary>
        /// 用户集合
        /// </summary>
        public virtual ICollection<User> Users { get; set; }

        /// <summary>
        /// 用户组集合
        /// </summary>
        public virtual ICollection<UserGroup> UserGroups { get; set; }

        /// <summary>
        /// 权限集合
        /// </summary>
        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
