using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.Caching;
using EntityFramework.Extensions;
using P.Common.Tools;
using P.Model;
using P.Model.ViewModel;
using P.PContext.Interface;
using P.Repository.Interface;
using P.Service.Interface;

namespace P.Service.CoreService
{
    public class UserService : CoreServiceBase, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleService _roleService;
        private readonly IUserGroupService _userGroupService;

        public UserService(IUserRepository userRepository, IRoleService roleService,IUserGroupService userGroupService, IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            this._userRepository = userRepository;
            this._roleService = roleService;
            this._userGroupService = userGroupService;
        }
        public IQueryable<User> Users
        {
            get { return _userRepository.DbSet; }
        }

        public Result Insert(UserVM model)
        {
            try
            {
                User oldUser = _userRepository.DbSet.FirstOrDefault(c => c.UserName == model.UserName.Trim());
                if (oldUser != null)
                {
                    return new Result(ResultType.Warning, "数据库中已经存在相同的用户名称，请修改后重新提交！");
                }
                var entity = new User
                {
                    UserName = model.UserName.Trim(),
                    TrueName = model.TrueName.Trim(),
                    Password = model.Password,
                    Phone = model.Phone,
                    Email = model.Email,
                    Address = model.Address,
                    Enabled = model.Enabled,
                    CreateTime = DateTime.Now
                };
                _userRepository.Insert(entity);
                return new Result(ResultType.Success, "新增数据成功！");
            }
            catch
            {
                return new Result(ResultType.Error, "新增数据失败，数据库插入数据时发生了错误!");
            }
        }

        public Result Update(UserVM model)
        {
            try
            {
                var user = Users.FirstOrDefault(c => c.Id == model.Id);
                if (user == null)
                {
                    throw new Exception();
                }
                var other = Users.FirstOrDefault(c => c.Id != model.Id && c.UserName == model.UserName.Trim());
                if (other != null)
                {
                    return new Result(ResultType.Warning, "数据库中已经存在相同的用户名称，请修改后重新提交！");
                }
                user.TrueName = model.TrueName.Trim();
                user.UserName = model.UserName.Trim();
                user.Address = model.Address;
                user.Phone = model.Phone;
                user.Email = model.Email;
                user.UpdateTime = DateTime.Now;
                _userRepository.Update(user);
                return new Result(ResultType.Success, "更新数据成功！");
            }
            catch
            {
                return new Result(ResultType.Error, "更新数据失败!");
            }
        }

        public Result Delete(IEnumerable<UserVM> list)
        {
            try
            {
                if (list != null)
                {
                    var userIds = list.Select(c => c.Id).ToList();
                    int count = _userRepository.DbSet.Where(c => userIds.Contains(c.Id)).Delete();
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

        public Result ResetPassword(IEnumerable<UserVM> list)
        {

            var listIds = list.Select(c => c.Id).ToList();
            try
            {
                string md5Pwd = EncryptionHelper.GetMd5Hash("123456");
                _userRepository.DbSet.Where(u => listIds.Contains(u.Id))
                    .Update(u => new User() { Password = md5Pwd });
                UnitOfWork.Commit();
                return new Result(ResultType.Success, "密码重置成功！");
            }
            catch
            {
                return new Result(ResultType.Error, "密码重置失败!");
            }

        }


        public Result UpdateUserRoles(int userId, string[] chkRoles)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var oldUser = Users.FirstOrDefault(c => c.Id == userId);
                    if (oldUser == null)
                    {
                        throw new Exception();
                    }
                    oldUser.Roles.Clear();
                    List<Role> newRoles = new List<Role>();
                    if (chkRoles != null && chkRoles.Length > 0)
                    {
                        int[] idInts = Array.ConvertAll<string, int>(chkRoles, Convert.ToInt32);
                        newRoles = _roleService.Roles.Where(c => idInts.Contains(c.Id)).ToList();
                        oldUser.Roles = newRoles;
                    }
                    UnitOfWork.Commit();
                    #region 重置权限缓存
                    var roleIdsByUser = newRoles.Select(r => r.Id).ToList();
                    var roleIdsByUserGroup = oldUser.UserGroups.SelectMany(g => g.Roles).Select(r => r.Id).ToList();
                    roleIdsByUser.AddRange(roleIdsByUserGroup);
                    var roleIds = roleIdsByUser.Distinct().ToList();
                    List<Permission> permissions = _roleService.Roles.Where(t => roleIds.Contains(t.Id) && t.Enabled == true).SelectMany(c => c.Permissions).Distinct().ToList();
                    var strKey = CacheKey.StrPermissionsByUid + "_" + oldUser.Id;
                    //设置Cache滑动过期时间为1天
                    CacheHelper.SetCache(strKey, permissions, Cache.NoAbsoluteExpiration, new TimeSpan(1, 0, 0, 0));
                    #endregion
                    scope.Complete();
                    return new Result(ResultType.Success, "设置用户角色成功！");
                }
            }
            catch
            {
                return new Result(ResultType.Error, "设置用户角色失败!");
            }
        }

        public Result UpdateUserGroups(int userId, string[] chkUserGroups)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var oldUser = Users.FirstOrDefault(c => c.Id == userId);
                    if (oldUser == null)
                    {
                        throw new Exception();
                    }
                    oldUser.UserGroups.Clear();
                    List<UserGroup> newUserGroups = new List<UserGroup>();
                    if (chkUserGroups != null && chkUserGroups.Length > 0)
                    {
                        int[] idInts = Array.ConvertAll<string, int>(chkUserGroups, Convert.ToInt32);
                        newUserGroups = _userGroupService.UserGroups.Where(c => idInts.Contains(c.Id)).ToList();
                        oldUser.UserGroups = newUserGroups;
                    }
                    UnitOfWork.Commit();
                    #region 重置权限缓存
                    var roleIdsByUser = newUserGroups.Select(r => r.Id).ToList();
                    var roleIdsByUserGroup = oldUser.UserGroups.SelectMany(g => g.Roles).Select(r => r.Id).ToList();
                    roleIdsByUser.AddRange(roleIdsByUserGroup);
                    var roleIds = roleIdsByUser.Distinct().ToList();
                    List<Permission> permissions = _roleService.Roles.Where(t => roleIds.Contains(t.Id) && t.Enabled == true).SelectMany(c => c.Permissions).Distinct().ToList();
                    var strKey = CacheKey.StrPermissionsByUid + "_" + oldUser.Id;
                    //设置Cache滑动过期时间为1天
                    CacheHelper.SetCache(strKey, permissions, Cache.NoAbsoluteExpiration, new TimeSpan(1, 0, 0, 0));
                    #endregion
                    scope.Complete();
                    return new Result(ResultType.Success, "设置用户组成功！");
                }
            }
            catch
            {
                return new Result(ResultType.Error, "设置用户组失败!");
            }
        }
    }
}
