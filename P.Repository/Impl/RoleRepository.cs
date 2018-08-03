using P.Model;
using P.PContext;
using P.PContext.Interface;
using P.Repository.Interface;
using System;


namespace P.Repository.Impl
{
    /// <summary>
    ///   仓储操作层实现——角色信息
    /// </summary>
    public partial class RoleRepository : RepositoryBase<Role, Int32>, IRoleRepository
    { 
        public RoleRepository(IUnitOfWork unitOfWork)
            : base()
        { }
     }
}
