using P.Model;
using P.Model.ViewModel;
using P.PContext;
using P.PContext.Interface;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace P.Repository.Interface.Impl
{
    /// <summary>
    ///   仓储操作层实现——权限
    /// </summary>
    public partial class PermissionRepository : RepositoryBase<Permission, Int32>, IPermissionRepository
    {
        public PermissionRepository(IUnitOfWork unitOfWork)
            : base()
        { }

        public IList<PermissionVM> GetListPermissionVM(Expression<Func<Permission, bool>> wh, int limit, int offset, out int total)
        {
            var q = from p in Context.Permissions.Where(wh)
                    join m in Context.Modules on p.Module.Id equals m.Id into joinModule
                    from item in joinModule
                    select new PermissionVM
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Code = p.Code,
                        Description = p.Description,
                        Enabled = p.Enabled,
                        UpdateDate = p.CreateTime,
                        ModuleId = item.Id,
                        ModuleName = item.Name
                    };
            total = q.Count();
            if (offset >= 0)
            {
                return q.OrderBy(c => c.ModuleId).ThenBy(c => c.Code).Skip(offset).Take(limit).ToList();
            }
            return q.ToList();
        }
    }
}
