using P.Model;
using P.PContext.Interface;
using System;


namespace P.Repository.Interface
{
    /// <summary>
    ///   仓储操作层接口——用户组
    /// </summary>
    public partial interface IUserGroupRepository : IRepository<UserGroup, Int32>
    { }
}
