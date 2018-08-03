using P.Model;
using P.PContext;
using P.PContext.Interface;
using P.Repository.Interface;
using System;


namespace P.Repository.Impl
{
    /// <summary>
    ///   仓储操作层实现——用户信息
    /// </summary>
    public partial class UserRepository : RepositoryBase<User, Int32>, IUserRepository
    { 
        public UserRepository(IUnitOfWork unitOfWork)
            : base()
        { }
     }
}
