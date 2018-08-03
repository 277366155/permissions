using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using P.Model;
using P.PContext.Interface;
using P.Service.Interface;
using P.Repository.Interface;
using P.Model.ViewModel;
using EntityFramework.Extensions;

namespace P.Service.CoreService
{
    public class UserGroupService : CoreServiceBase, IUserGroupService
    {

        private readonly IUserGroupRepository _userGroupRepository;
        private readonly IRoleService _roleService;

        public UserGroupService(IUserGroupRepository userGroupRepository, IRoleService roleService, IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            this._userGroupRepository = userGroupRepository;
            this._roleService = roleService;
        }

        public IQueryable<UserGroup> UserGroups
        {
            get { return _userGroupRepository.DbSet; }
        }

        public Result Insert(UserGroupVM model)
        {
            try
            {
                UserGroup oldGroup = _userGroupRepository.DbSet.FirstOrDefault(c => c.GroupName == model.GroupName.Trim());
                if (oldGroup != null)
                {
                    return new Result(ResultType.Warning, "数据库中已经存在相同名称的用户组，请修改后重新提交！");
                }
                var entity = new UserGroup()
                {
                    GroupName = model.GroupName.Trim(),
                    Description = model.Description,
                    OrderSort = model.OrderSort,
                    Enabled = model.Enabled,
                    UpdateTime = DateTime.Now
                };
                _userGroupRepository.Insert(entity);
                return new Result(ResultType.Success, "新增数据成功！");
            }
            catch
            {
                return new Result(ResultType.Error, "新增数据失败，数据库插入数据时发生了错误!");
            }
        }

        public Result Update(UserGroupVM model)
        {
            try
            {
                var oldRole = UserGroups.FirstOrDefault(c => c.Id == model.Id);
                if (oldRole == null)
                {
                    throw new Exception();
                }
                var other = UserGroups.FirstOrDefault(c => c.Id != model.Id && c.GroupName == model.GroupName.Trim());
                if (other != null)
                {
                    return new Result(ResultType.Warning, "数据库中已经存在相同名称的用户组，请修改后重新提交！");
                }
                oldRole.GroupName = model.GroupName.Trim();
                oldRole.Description = model.Description;
                oldRole.OrderSort = model.OrderSort;
                oldRole.Enabled = model.Enabled;
                oldRole.UpdateTime = DateTime.Now;
                _userGroupRepository.Update(oldRole);
                return new Result(ResultType.Success, "更新数据成功！");
            }
            catch
            {
                return new Result(ResultType.Error, "更新数据失败!");
            }
        }

        public Result Delete(IEnumerable<UserGroupVM> list)
        {
            try
            {
                if (list != null)
                {
                    var groupIds = list.Select(c => c.Id).ToList();
                    int count = _userGroupRepository.DbSet.Where(c => groupIds.Contains(c.Id)).Delete();
                    if (count > 0)
                    {
                        return new Result(ResultType.Success, "删除数据成功！");
                    }
                    else
                    {
                        return new Result(ResultType.Error, "删除数据失败!");
                    }
                }
                else
                {
                    return new Result(ResultType.ParamError, "参数错误，请选择需要删除的数据!");
                }
            }
            catch
            {
                return new Result(ResultType.Error, "删除数据失败!");
            }
        }

        public Result UpdateUserGroupRoles(int userGroupId, string[] chkRoles)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var oldUserGroup = UserGroups.FirstOrDefault(c => c.Id == userGroupId);
                    if (oldUserGroup == null)
                    {
                        throw new Exception();
                    }
                    oldUserGroup.Roles.Clear();
                    List<Role> newRoles = new List<Role>();
                    if (chkRoles != null && chkRoles.Length > 0)
                    {
                        int[] idInts = Array.ConvertAll<string, int>(chkRoles, Convert.ToInt32);
                        newRoles = _roleService.Roles.Where(c => idInts.Contains(c.Id)).ToList();
                        oldUserGroup.Roles = newRoles;
                    }
                    UnitOfWork.Commit();
                    scope.Complete();
                    return new Result(ResultType.Success, "设置用户组角色成功！");
                }
            }
            catch
            {
                return new Result(ResultType.Error, "设置用户组角色失败!");
            }
        }
    }
}
