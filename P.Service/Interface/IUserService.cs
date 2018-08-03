using System.Collections.Generic;
using System.Linq;
using P.Model;
using P.Model.ViewModel;

namespace P.Service.Interface
{
    public interface IUserService
    {
        IQueryable<User> Users { get; }
        Result Insert(UserVM model);
        Result Update(UserVM model);
        Result Delete(IEnumerable<UserVM> list);
        Result ResetPassword(IEnumerable<UserVM> list);
        Result UpdateUserRoles(int roleId, string[] chkRoles);
        Result UpdateUserGroups(int userId, string[] chkUserGroups);
    }
}
