using System.Collections.Generic;
using System.Linq;
using P.Model;
using P.Model.ViewModel;

namespace P.Service.Interface
{
    public interface IUserGroupService
    {
        IQueryable<UserGroup> UserGroups { get; }
        Result Insert(UserGroupVM model);
        Result Update(UserGroupVM model);
        Result Delete(IEnumerable<UserGroupVM> list);
        Result UpdateUserGroupRoles(int userId, string[] chkRoles);
    }
}
