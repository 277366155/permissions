using System.Collections.Generic;
using System.Linq;
using P.Model;
using P.Model.ViewModel;

namespace P.Service.Interface
{
    public interface IRoleService
    {
        IQueryable<Role> Roles { get; }
        Result Insert(RoleVM model);
        Result Update(RoleVM model);
        Result Delete(IEnumerable<RoleVM> list);
        IList<ZTreeVM> GetListZTreeVM(int id);
        Result UpdateAuthorize(int roleId, int[] ids);
    }
}
