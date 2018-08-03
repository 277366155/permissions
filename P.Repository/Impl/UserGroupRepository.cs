using P.Model;
using P.PContext;
using P.PContext.Interface;
using P.Repository.Interface;
using System;


namespace P.Repository.Impl
{
    /// <summary>
    ///   仓储操作层实现——用户组
    /// </summary>
    public partial class UserGroupRepository : RepositoryBase<UserGroup, Int32>, IUserGroupRepository
    { 
        public UserGroupRepository(IUnitOfWork unitOfWork)
            : base()
        { }
     }
}
