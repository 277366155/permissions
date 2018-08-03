
using P.Model;
using P.Model.ViewModel;
using P.PContext;
using P.PContext.Interface;
using P.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace P.Repository.Impl
{
	/// <summary>
    ///   仓储操作层实现——模块
    /// </summary>
    public partial class ModuleRepository : RepositoryBase<Module, Int32>, IModuleRepository
    { 
        public ModuleRepository(IUnitOfWork unitOfWork)
            : base()
        { }

        public IList<ModuleVM> GetListModuleVM(Expression<Func<Module, bool>> wh, int limit, int offset, out int total)
        {
            var q = from m1 in Context.Modules.Where(wh)
                    join m2 in Context.Modules on m1.ParentId equals m2.Id into joinModule
                    from item in joinModule.DefaultIfEmpty()
                    select new ModuleVM
                    {
                        Id = m1.Id,
                        Name = m1.Name,
                        LinkUrl = m1.LinkUrl,
                        IsMenu = m1.IsMenu,
                        Code = m1.Code,
                        Description = m1.Description,
                        Enabled = m1.Enabled,
                        ParentName = item.Name,
                        UpdateDate = m1.CreateTime
                    };
            total = q.Count();
            if (offset >= 0)
            {
                return q.OrderBy(c => c.Code).Skip(offset).Take(limit).ToList();
            }
            return q.ToList();
        }
    }
}
