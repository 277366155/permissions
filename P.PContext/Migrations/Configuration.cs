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
            #region ģ�����
            List<Module> modules = new List<Module>
            {
                new Module { Id = 1, ParentId = null, Name = "��Ȩ����", Code = 200,LinkUrl="#",  Description = null, IsMenu = true, Enabled = true,  CreateTime = DateTime.Now},
                new Module { Id = 2, ParentId = 1, Name = "��ɫ����", LinkUrl = "~/Sys/Role/Index",  Code = 201,Description = null, IsMenu = true, Enabled = true, CreateTime = DateTime.Now},
                new Module { Id = 3, ParentId = 1, Name = "�û�����", LinkUrl = "~/Sys/User/Index", Code = 202, Description = null, IsMenu = true, Enabled = true, CreateTime = DateTime.Now },
                new Module { Id = 4, ParentId = 1, Name = "ģ�����", LinkUrl = "~/Sys/Module/Index",  Code = 204, Description = null, IsMenu = true, Enabled = true,  CreateTime = DateTime.Now },
                new Module { Id = 5, ParentId = 1, Name = "Ȩ�޹���", LinkUrl = "~/Sys/Permission/Index",  Code = 205, Description = null, IsMenu = true, Enabled = true,  CreateTime = DateTime.Now },
                 new Module { Id = 6, ParentId =null, Name = "ϵͳӦ��", LinkUrl = "#",  Code = 300,  Description = null, IsMenu = true, Enabled = true,  CreateTime = DateTime.Now },
                new Module { Id = 7, ParentId = 6, Name = "������־����", LinkUrl = "#",Code = 301,Description = null, IsMenu = true, Enabled = true,  CreateTime = DateTime.Now },
                new Module { Id = 8, ParentId = 1, Name = "�û������", LinkUrl = "~/Sys/UserGroup/Index",  Code = 203, Description = null, IsMenu = true, Enabled = true,  CreateTime = DateTime.Now }
                //~/SysConfig/OperateLog/Index
            };

            context.Modules.AddOrUpdate(t => new { t.Id }, modules.ToArray());
            context.SaveChanges();
            #endregion

            #region Ȩ�޹���
            List<Permission> permissions = new List<Permission>
            {
             #region ��ɫ
		       new Permission{Id=1, Name="��ѯ",Code=EnumPermissionCode.QueryRole.ToString(),
                    Description="����" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[1]},
               new Permission{Id=2, Name="����",Code=EnumPermissionCode.AddRole.ToString(), Description="����" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[1]},
               new Permission{Id=3, Name="�޸�",Code=EnumPermissionCode.UpdateRole.ToString(), Description="����" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[1]},
               new Permission{Id=4, Name="ɾ��",Code=EnumPermissionCode.DeleteRole.ToString(), Description="����" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[1]},
               new Permission{Id=5, Name="��Ȩ",Code=EnumPermissionCode.AuthorizeRole.ToString(), Description="����" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[1]}, 
             #endregion

             #region �û�
		       new Permission{Id=6, Name="��ѯ",Code=EnumPermissionCode.QueryUser.ToString(), Description="����" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[2]},
               new Permission{Id=7, Name="����",Code=EnumPermissionCode.AddUser.ToString(), Description="����" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[2]},
               new Permission{Id=8, Name="�޸�",Code=EnumPermissionCode.UpdateUser.ToString(), Description="����" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[2]},
               new Permission{Id=9, Name="ɾ��",Code=EnumPermissionCode.DeleteUser.ToString(), Description="����" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[2]},
               new Permission{Id=10, Name="��������",Code=EnumPermissionCode.ResetPwdUser.ToString(), Description="����" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[2]},
               new Permission{Id=11, Name="�����û���",Code=EnumPermissionCode.SetGroupUser.ToString(), Description="����" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[2]},
               new Permission{Id=12, Name="���ý�ɫ",Code=EnumPermissionCode.SetRolesUser.ToString(), Description="����" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[2]}, 
	         #endregion
             
             #region ģ��
		     new Permission{Id=13, Name="��ѯ",Code=EnumPermissionCode.QueryModule.ToString(), Description="����" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[3]},
             new Permission{Id=14, Name="����",Code=EnumPermissionCode.AddModule.ToString(), Description="����" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[3]},
             new Permission{Id=15, Name="�޸�",Code=EnumPermissionCode.UpdateModule.ToString(), Description="����" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[3]},
	         #endregion

             #region Ȩ��
		     new Permission{Id=16, Name="��ѯ",Code=EnumPermissionCode.QueryPermission.ToString(), Description="����" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[4]},
             new Permission{Id=17, Name="����",Code=EnumPermissionCode.AddPermission.ToString(), Description="����" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[4]},
             new Permission{Id=18, Name="�޸�",Code=EnumPermissionCode.UpdatePermission.ToString(), Description="����" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[4]},
	         #endregion

             #region ������־
		     new Permission{Id=19, Name="��ѯ",Code=EnumPermissionCode.QuerySystemLog.ToString(), Description="����" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[6]},
	         #endregion

             #region �û���
		     new Permission{Id=20, Name="��ѯ",Code=EnumPermissionCode.QueryUserGroup.ToString(), Description="����" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[7]},
             new Permission{Id=21, Name="����",Code=EnumPermissionCode.AddUserGroup.ToString(), Description="����" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[7]},
             new Permission{Id=22, Name="�޸�",Code=EnumPermissionCode.UpdateUserGroup.ToString(), Description="����" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[7]},
             new Permission{Id=23, Name="ɾ��",Code=EnumPermissionCode.DeleteUserGroup.ToString(), Description="����" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[7]},
             new Permission{Id=24, Name="���ý�ɫ",Code=EnumPermissionCode.SetRolesUserGroup.ToString(), Description="����" ,Enabled=true,CreateTime=DateTime.Now,Module=modules[7]}
	         #endregion
            };
            context.Permissions.AddOrUpdate(m => new { m.Id }, permissions.ToArray());
            context.SaveChanges();
            #endregion

            #region ��ɫ����
            List<Role> roles = new List<Role>
            {
                new Role { Id=1,  RoleName = "superadmin", Description="��������Ա",Enabled=true,OrderSort=1,CreateTime=DateTime.Now ,Permissions=permissions},
                new Role { Id=2,  RoleName = "����Ա", Description="ϵͳ����Ա",Enabled=true,OrderSort=1,CreateTime=DateTime.Now,Permissions=permissions},
                new Role { Id=3,  RoleName = "��ͨ��ɫ1", Description="��ͨ��ɫ1",Enabled=true,OrderSort=1,CreateTime=DateTime.Now ,Permissions=permissions},
                new Role { Id=4,  RoleName = "��ͨ��ɫ2", Description="��ͨ��ɫ2",Enabled=true,OrderSort=1,CreateTime=DateTime.Now },
                new Role { Id=5,  RoleName = "��ͨ��ɫ3", Description="��ͨ��ɫ3",Enabled=true,OrderSort=1,CreateTime=DateTime.Now },
                new Role { Id=6,  RoleName = "��ͨ��ɫ4", Description="��ͨ��ɫ4",Enabled=true,OrderSort=1,CreateTime=DateTime.Now },
                new Role { Id=7,  RoleName = "��ͨ��ɫ5", Description="��ͨ��ɫ5",Enabled=true,OrderSort=1,CreateTime=DateTime.Now },
                new Role { Id=8,  RoleName = "��ͨ��ɫ6", Description="��ͨ��ɫ6",Enabled=true,OrderSort=1,CreateTime=DateTime.Now },
                new Role { Id=9,  RoleName = "��ͨ��ɫ7", Description="��ͨ��ɫ7",Enabled=true,OrderSort=1,CreateTime=DateTime.Now },
                new Role { Id=10,  RoleName = "��ͨ��ɫ8", Description="��ͨ��ɫ8",Enabled=true,OrderSort=1,CreateTime=DateTime.Now },
                new Role { Id=11,  RoleName = "��ͨ��ɫ9", Description="��ͨ��ɫ9",Enabled=true,OrderSort=1,CreateTime=DateTime.Now },
                new Role { Id=12,  RoleName = "��ͨ��ɫ10", Description="��ͨ��ɫ10",Enabled=true,OrderSort=1,CreateTime=DateTime.Now }
            };
            
            context.Roles.AddOrUpdate(m => new { m.RoleName }, roles.ToArray());
            context.SaveChanges();
            #endregion

            #region �û�����
            List<User> members = new List<User>
            {
                new User { Id=1, UserName = "admin", Password = "e10adc3949ba59abbe56e057f20f883e", Email = "123456789@qq.com", TrueName = "����Ա",Phone="18181818181",Address="�㶫���������������·XX��XX��XXX��X��" ,Enabled=true,Roles=new List<Role>{roles[1]} },
                new User { Id=2, UserName = "xiaowu", Password = "e10adc3949ba59abbe56e057f20f883e", Email = "11111@1111.com", TrueName = "С��",Phone="18181818181",Address="�㶫���������������·XX��X�㶫���������������·XX��XX��XXX��X��",Enabled=true,Roles=new List<Role>{roles[1]} }
            };           
            context.Users.AddOrUpdate(m => new { m.Id }, members.ToArray());
            context.SaveChanges();
            #endregion

            #region �û������
            List<UserGroup> userGroups = new List<UserGroup>
            {
                new UserGroup { Id=1, GroupName = "������",Description = "������Ա��",Enabled=true,Roles=new List<Role>{roles[1]},OrderSort = 1,Users = new List<User>(){members[0]}},
                new UserGroup { Id=2, GroupName = "��Ŀ������", Description = "��Ŀ������",Enabled=true,Roles=new List<Role>{roles[1]},OrderSort = 2,Users = new List<User>(){members[1]}}
            };

            context.UserGroups.AddOrUpdate(m => new { m.Id }, userGroups.ToArray());
            context.SaveChanges();
            #endregion

        }
    }
}
