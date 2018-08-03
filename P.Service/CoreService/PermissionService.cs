﻿

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using P.Model;
using P.Model.ViewModel;
using P.PContext.Interface;
using P.Repository.Interface;
using P.Service.Interface;

namespace P.Service.CoreService
{
    public class PermissionService : CoreServiceBase, IPermissionService
    {
        private readonly IPermissionRepository _PermissionRepository;

        public PermissionService(IPermissionRepository permissionRepository, IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            this._PermissionRepository = permissionRepository;
        }
        public IQueryable<Permission> Permissions
        {
            get
            {
                var Permission = _PermissionRepository.DbSet;

                return Permission;
            }
        }

        /// <summary>
        /// 获取模块分页列表
        /// </summary>
        /// <param name="wh">查询where表达式</param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public IList<PermissionVM> GetListPermissionVM(Expression<Func<Permission, bool>> wh, int limit, int offset, out int total)
        {
            return _PermissionRepository.GetListPermissionVM(wh, limit, offset, out total);
        }



        public Result Insert(PermissionVM model)
        {
            try
            {
                Permission oldPermission = Permissions.Where(c => c.Module.Id == model.ModuleId).FirstOrDefault(c => (c.Name == model.Name.Trim()) || (c.Code == model.Code.Trim()));
                if (oldPermission != null)
                {
                    return new Result(ResultType.Warning, "该模块中已经存在相同名称或编码的权限，请修改后重新提交！");
                }
                var entity = new Permission
                {
                    Name = model.Name,
                    ModuleId = model.ModuleId,
                    Code = model.Code,
                    Description = model.Description,
                    Enabled = model.Enabled,
                    UpdateTime = DateTime.Now
                };
                _PermissionRepository.Insert(entity);
                return new Result(ResultType.Success, "新增数据成功！");
            }
            catch
            {
                return new Result(ResultType.Error, "新增数据失败，数据库插入数据时发生了错误!");
            }
        }

        public Result Update(PermissionVM model)
        {
            try
            {
                var permission = Permissions.FirstOrDefault(c => c.Id == model.Id);
                if (permission == null)
                {
                    throw new Exception();
                }
                var other = Permissions.FirstOrDefault(c => c.Id != model.Id && c.ModuleId == model.ModuleId && (c.Name == model.Name.Trim() || c.Code == model.Code));
                if (other != null)
                {
                    return new Result(ResultType.Warning, "该模块中已经存在相同名称或编码的权限，请修改后重新提交！");
                }
                permission.Name = model.Name.Trim();
                permission.ModuleId = model.ModuleId;
                permission.Code = model.Code;
                permission.Description = model.Description;
                permission.Enabled = model.Enabled;
                permission.UpdateTime = DateTime.Now;
                _PermissionRepository.Update(permission);
                return new Result(ResultType.Success, "更新数据成功！");
            }
            catch
            {
                return new Result(ResultType.Error, "更新数据失败!");
            }
        }
    }
}
