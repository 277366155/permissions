using P.Model;
using P.Model.ViewModel;
using P.PContext.Interface;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace P.Repository.Interface
{
    /// <summary>
    ///   仓储操作层接口——权限
    /// </summary>
    public partial interface IPermissionRepository : IRepository<Permission, Int32>
    {
        IList<PermissionVM> GetListPermissionVM(Expression<Func<Permission, bool>> wh, int limit, int offset, out int total);
    }
}
