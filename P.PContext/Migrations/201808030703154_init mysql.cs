namespace P.PContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initmysql : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Modules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(),
                        Name = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        LinkUrl = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        IsMenu = c.Boolean(nullable: false),
                        Code = c.Int(nullable: false),
                        Description = c.String(maxLength: 100, storeType: "nvarchar"),
                        Enabled = c.Boolean(nullable: false),
                        CreateTime = c.DateTime(nullable: false, precision: 0),
                        UpdateTime = c.DateTime(nullable: false, precision: 0),
                        ParentModule_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Modules", t => t.ParentModule_Id)
                .Index(t => t.ParentModule_Id);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        Code = c.String(unicode: false),
                        Description = c.String(maxLength: 100, storeType: "nvarchar"),
                        Enabled = c.Boolean(nullable: false),
                        ModuleId = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false, precision: 0),
                        UpdateTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Modules", t => t.ModuleId, cascadeDelete: true)
                .Index(t => t.ModuleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        Description = c.String(maxLength: 100, storeType: "nvarchar"),
                        Enabled = c.Boolean(nullable: false),
                        OrderSort = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false, precision: 0),
                        UpdateTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupName = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        Description = c.String(maxLength: 100, storeType: "nvarchar"),
                        OrderSort = c.Int(nullable: false),
                        Enabled = c.Boolean(nullable: false),
                        CreateTime = c.DateTime(nullable: false, precision: 0),
                        UpdateTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        Password = c.String(nullable: false, maxLength: 32, storeType: "nvarchar"),
                        TrueName = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        Email = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        Phone = c.String(maxLength: 50, storeType: "nvarchar"),
                        Address = c.String(maxLength: 300, storeType: "nvarchar"),
                        Enabled = c.Boolean(nullable: false),
                        CreateTime = c.DateTime(nullable: false, precision: 0),
                        UpdateTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RolePermissions",
                c => new
                    {
                        Role_Id = c.Int(nullable: false),
                        Permission_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_Id, t.Permission_Id })
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .ForeignKey("dbo.Permissions", t => t.Permission_Id, cascadeDelete: true)
                .Index(t => t.Role_Id)
                .Index(t => t.Permission_Id);
            
            CreateTable(
                "dbo.UserGroupRoles",
                c => new
                    {
                        UserGroup_Id = c.Int(nullable: false),
                        Role_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserGroup_Id, t.Role_Id })
                .ForeignKey("dbo.UserGroups", t => t.UserGroup_Id, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .Index(t => t.UserGroup_Id)
                .Index(t => t.Role_Id);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Role_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Role_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Role_Id);
            
            CreateTable(
                "dbo.UserUserGroups",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        UserGroup_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.UserGroup_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.UserGroups", t => t.UserGroup_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.UserGroup_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserUserGroups", "UserGroup_Id", "dbo.UserGroups");
            DropForeignKey("dbo.UserUserGroups", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserGroupRoles", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.UserGroupRoles", "UserGroup_Id", "dbo.UserGroups");
            DropForeignKey("dbo.RolePermissions", "Permission_Id", "dbo.Permissions");
            DropForeignKey("dbo.RolePermissions", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.Permissions", "ModuleId", "dbo.Modules");
            DropForeignKey("dbo.Modules", "ParentModule_Id", "dbo.Modules");
            DropIndex("dbo.UserUserGroups", new[] { "UserGroup_Id" });
            DropIndex("dbo.UserUserGroups", new[] { "User_Id" });
            DropIndex("dbo.UserRoles", new[] { "Role_Id" });
            DropIndex("dbo.UserRoles", new[] { "User_Id" });
            DropIndex("dbo.UserGroupRoles", new[] { "Role_Id" });
            DropIndex("dbo.UserGroupRoles", new[] { "UserGroup_Id" });
            DropIndex("dbo.RolePermissions", new[] { "Permission_Id" });
            DropIndex("dbo.RolePermissions", new[] { "Role_Id" });
            DropIndex("dbo.Permissions", new[] { "ModuleId" });
            DropIndex("dbo.Modules", new[] { "ParentModule_Id" });
            DropTable("dbo.UserUserGroups");
            DropTable("dbo.UserRoles");
            DropTable("dbo.UserGroupRoles");
            DropTable("dbo.RolePermissions");
            DropTable("dbo.Users");
            DropTable("dbo.UserGroups");
            DropTable("dbo.Roles");
            DropTable("dbo.Permissions");
            DropTable("dbo.Modules");
        }
    }
}
