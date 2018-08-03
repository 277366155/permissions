using P.Model;
using P.PContext.Interface;
using System;


namespace P.Repository.Interface
{
    /// <summary>
    ///   仓储操作层接口——角色信息
    /// </summary>
    public partial interface IRoleRepository : IRepository<Role, Int32>
    { }
}
