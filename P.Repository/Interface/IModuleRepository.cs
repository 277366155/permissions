using P.Model;
using P.Model.ViewModel;
using P.PContext.Interface;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace P.Repository.Interface
{
    /// <summary>
    ///   仓储操作层接口——模块
    /// </summary>
    public partial interface IModuleRepository : IRepository<Module, Int32>
    {
        IList<ModuleVM> GetListModuleVM(Expression<Func<Module, bool>> wh, int limit, int offset, out int total);
    }
}
