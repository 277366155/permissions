using P.Model;
using System.Data.Entity;

namespace P.PContext
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class BaseContext:DbContext
    {
        public BaseContext() : base("DbConn")
        {
        }

        public BaseContext(bool isReadOnly):base(isReadOnly?"DbReadOnlyConn":"DbConn")
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<UserGroup> UserGroups { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<Module> Modules { get; set; }
    }
}
