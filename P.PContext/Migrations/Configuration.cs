namespace P.PContext.Migrations
{
    using P.Model;
    using P.Model.Enum;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<P.PContext.BaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            this.SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
        }

        protected override void Seed(P.PContext.BaseContext context)
        {
            #region 模块管理
            List<Module> modules = new List<Module>
            {
                new Module { Id = 1, ParentId = null, Name = "授权管理", Code = 200,LinkUrl="#",  Description = null, IsMenu = true, Enabled = true,  CreateTime = DateTime.Now},
                new Module { Id = 2, ParentId = 1, Name = "角色管理", LinkUrl = "~/Sys/Role/Index",  Code = 201,Description = null, IsMenu = true, Enabled = true, CreateTime = DateTime.Now},
                new Module { Id = 3, ParentId = 1, Name = "用户管理", LinkUrl = "~/Sys/User/Index", Code = 202, Description = null, IsMenu = true, Enabled = true, CreateTime = DateTime.Now },
                new Module { Id = 4, ParentId = 1, Name = "模块管理", LinkUrl = "~/Sys/Module/Index",  Code = 204, Description = null, IsMenu = true, Enabled = true,  CreateTime = DateTime.Now },
                new Module { Id = 5, ParentId = 1, Name = "权限管理", LinkUrl = "~/Sys/Permission/Index",  Code = 205, Description = null, IsMenu = true, Enabled = true,  CreateTime = DateTime.Now },
                 new Module { Id = 6, ParentId =null, Name = "系统应用", LinkUrl = "#",  Code = 300,  Description = null, IsMenu = true, Enabled = true,  CreateTime = DateTime.Now },
                new Module { Id = 7, ParentId = 6, Name = "操作日志管理", LinkUrl = "#",Code = 301,Description = null, IsMenu = true, Enabled = true,  CreateTime = DateTime.Now },
                new Module { Id = 8, ParentId = 1, Name = "用户组管理", LinkUrl = "~/Sys/UserGroup/Index",  Code = 203, Description = null, IsMenu = true, Enabled = true,  CreateTime = DateTime.Now }
                //~/SysConfig/OperateLog/Index
            };

            context.Modules.AddOrUpdate(t => new { t.Id }, modules.ToArray());
            context.SaveChanges();
            #endregion

            #region 权限管理
            List<Permission> permissions = new List<Permission>
            {
             #region 角色
		       new Permission{Id=1, Name="查询",Code=EnumPermissionCode.QueryRole.ToString(),
                    Description="描述" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[1]},
               new Permission{Id=2, Name="新增",Code=EnumPermissionCode.AddRole.ToString(), Description="描述" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[1]},
               new Permission{Id=3, Name="修改",Code=EnumPermissionCode.UpdateRole.ToString(), Description="描述" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[1]},
               new Permission{Id=4, Name="删除",Code=EnumPermissionCode.DeleteRole.ToString(), Description="描述" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[1]},
               new Permission{Id=5, Name="授权",Code=EnumPermissionCode.AuthorizeRole.ToString(), Description="描述" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[1]}, 
             #endregion

             #region 用户
		       new Permission{Id=6, Name="查询",Code=EnumPermissionCode.QueryUser.ToString(), Description="描述" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[2]},
               new Permission{Id=7, Name="新增",Code=EnumPermissionCode.AddUser.ToString(), Description="描述" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[2]},
               new Permission{Id=8, Name="修改",Code=EnumPermissionCode.UpdateUser.ToString(), Description="描述" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[2]},
               new Permission{Id=9, Name="删除",Code=EnumPermissionCode.DeleteUser.ToString(), Description="描述" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[2]},
               new Permission{Id=10, Name="重置密码",Code=EnumPermissionCode.ResetPwdUser.ToString(), Description="描述" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[2]},
               new Permission{Id=11, Name="设置用户组",Code=EnumPermissionCode.SetGroupUser.ToString(), Description="描述" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[2]},
               new Permission{Id=12, Name="设置角色",Code=EnumPermissionCode.SetRolesUser.ToString(), Description="描述" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[2]}, 
	         #endregion
             
             #region 模块
		     new Permission{Id=13, Name="查询",Code=EnumPermissionCode.QueryModule.ToString(), Description="描述" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[3]},
             new Permission{Id=14, Name="新增",Code=EnumPermissionCode.AddModule.ToString(), Description="描述" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[3]},
             new Permission{Id=15, Name="修改",Code=EnumPermissionCode.UpdateModule.ToString(), Description="描述" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[3]},
	         #endregion

             #region 权限
		     new Permission{Id=16, Name="查询",Code=EnumPermissionCode.QueryPermission.ToString(), Description="描述" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[4]},
             new Permission{Id=17, Name="新增",Code=EnumPermissionCode.AddPermission.ToString(), Description="描述" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[4]},
             new Permission{Id=18, Name="修改",Code=EnumPermissionCode.UpdatePermission.ToString(), Description="描述" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[4]},
	         #endregion

             #region 操作日志
		     new Permission{Id=19, Name="查询",Code=EnumPermissionCode.QuerySystemLog.ToString(), Description="描述" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[6]},
	         #endregion

             #region 用户组
		     new Permission{Id=20, Name="查询",Code=EnumPermissionCode.QueryUserGroup.ToString(), Description="描述" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[7]},
             new Permission{Id=21, Name="新增",Code=EnumPermissionCode.AddUserGroup.ToString(), Description="描述" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[7]},
             new Permission{Id=22, Name="修改",Code=EnumPermissionCode.UpdateUserGroup.ToString(), Description="描述" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[7]},
             new Permission{Id=23, Name="删除",Code=EnumPermissionCode.DeleteUserGroup.ToString(), Description="描述" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[7]},
             new Permission{Id=24, Name="设置角色",Code=EnumPermissionCode.SetRolesUserGroup.ToString(), Description="描述" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[7]}
	         #endregion
            };
            context.Permissions.AddOrUpdate(m => new { m.Id }, permissions.ToArray());
            context.SaveChanges();
            #endregion

            #region 角色管理
            List<Role> roles = new List<Role>
            {
                new Role { Id=1,  RoleName = "superadmin", Description="超级管理员",Enabled=true,OrderSort=1,CreateTime=DateTime.Now ,Permissions=permissions},
                new Role { Id=2,  RoleName = "管理员", Description="系统管理员",Enabled=true,OrderSort=1,CreateTime=DateTime.Now,Permissions=permissions},
                new Role { Id=3,  RoleName = "普通角色1", Description="普通角色1",Enabled=true,OrderSort=1,CreateTime=DateTime.Now ,Permissions=permissions},
                new Role { Id=4,  RoleName = "普通角色2", Description="普通角色2",Enabled=true,OrderSort=1,CreateTime=DateTime.Now },
                new Role { Id=5,  RoleName = "普通角色3", Description="普通角色3",Enabled=true,OrderSort=1,CreateTime=DateTime.Now },
                new Role { Id=6,  RoleName = "普通角色4", Description="普通角色4",Enabled=true,OrderSort=1,CreateTime=DateTime.Now },
                new Role { Id=7,  RoleName = "普通角色5", Description="普通角色5",Enabled=true,OrderSort=1,CreateTime=DateTime.Now },
                new Role { Id=8,  RoleName = "普通角色6", Description="普通角色6",Enabled=true,OrderSort=1,CreateTime=DateTime.Now },
                new Role { Id=9,  RoleName = "普通角色7", Description="普通角色7",Enabled=true,OrderSort=1,CreateTime=DateTime.Now },
                new Role { Id=10,  RoleName = "普通角色8", Description="普通角色8",Enabled=true,OrderSort=1,CreateTime=DateTime.Now },
                new Role { Id=11,  RoleName = "普通角色9", Description="普通角色9",Enabled=true,OrderSort=1,CreateTime=DateTime.Now },
                new Role { Id=12,  RoleName = "普通角色10", Description="普通角色10",Enabled=true,OrderSort=1,CreateTime=DateTime.Now }
            };
            
            context.Roles.AddOrUpdate(m => new { m.RoleName }, roles.ToArray());
            context.SaveChanges();
            #endregion

            #region 用户管理
            List<User> members = new List<User>
            {
                new User { Id=1, UserName = "admin", Password = "e10adc3949ba59abbe56e057f20f883e", Email = "123456789@qq.com", TrueName = "管理员",Phone="18181818181",Address="广东广州市天河区科韵路XX街XX号XXX房X号" ,Enabled=true,Roles=new List<Role>{roles[1]} },
                new User { Id=2, UserName = "xiaowu", Password = "e10adc3949ba59abbe56e057f20f883e", Email = "11111@1111.com", TrueName = "小吴",Phone="18181818181",Address="广东广州市天河区科韵路XX街X广东广州市天河区科韵路XX街XX号XXX房X号",Enabled=true,Roles=new List<Role>{roles[1]} }
            };           
            context.Users.AddOrUpdate(m => new { m.Id }, members.ToArray());
            context.SaveChanges();
            #endregion

            #region 用户组管理
            List<UserGroup> userGroups = new List<UserGroup>
            {
                new UserGroup { Id=1, GroupName = "开发组",Description = "开发人员组",Enabled=true,Roles=new List<Role>{roles[1]},OrderSort = 1,Users = new List<User>(){members[0]}},
                new UserGroup { Id=2, GroupName = "项目经理组", Description = "项目经理组",Enabled=true,Roles=new List<Role>{roles[1]},OrderSort = 2,Users = new List<User>(){members[1]}}
            };

            context.UserGroups.AddOrUpdate(m => new { m.Id }, userGroups.ToArray());
            context.SaveChanges();
            #endregion

        }
    }
}
