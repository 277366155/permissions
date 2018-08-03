

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EntityFramework.Extensions;
using P.Model;
using P.Model.ViewModel;
using P.PContext.Interface;
using P.Repository.Interface;
using P.Service.Interface;

namespace P.Service.CoreService
{
    public class ModuleService : CoreServiceBase, IModuleService
    {
        private readonly IModuleRepository _moduleRepository;

        public ModuleService(IModuleRepository moduleRepository, IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _moduleRepository = moduleRepository;
        }

        #region 查询
        public IQueryable<Module> Modules
        {
            get { return _moduleRepository.DbSet; }
        }
        /// <summary>
        /// 贪婪加载实体
        /// </summary>
        /// <param name="inclueList"></param>
        /// <returns></returns>
        public IQueryable<Module> GetEntitiesByEager(IEnumerable<string> inclueList)
        {
            return _moduleRepository.GetEntitiesByEager(inclueList);
        }
        /// <summary>
        /// 获取模块分页列表
        /// </summary>
        /// <param name="wh">查询where表达式</param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public IList<ModuleVM> GetListModuleVM(Expression<Func<Module, bool>> wh, int limit, int offset, out int total)
        {
            return _moduleRepository.GetListModuleVM(wh, limit, offset, out total);
        }
        #endregion


        public Result Insert(ModuleVM model)
        {
            try
            {
                Module oldModule = Modules.FirstOrDefault(c => c.Name == model.Name.Trim());
                if (oldModule != null)
                {
                    return new Result(ResultType.Warning, "数据库中已经存在相同名称的模块，请修改后重新提交！");
                }
                var entity = new Module
                {
                    Name = model.Name.Trim(),
                    ParentId = model.ParentId,
                    LinkUrl = model.LinkUrl,
                    IsMenu = model.IsMenu,
                    Code = model.Code,
                    Description = model.Description,
                    Enabled = model.Enabled,
                    UpdateTime = DateTime.Now
                };
                _moduleRepository.Insert(entity);
                return new Result(ResultType.Success, "新增数据成功！");
            }
            catch
            {
                return new Result(ResultType.Error, "新增数据失败，数据库插入数据时发生了错误!");
            }
        }

        public Result Update(ModuleVM model)
        {
            try
            {
                var module = Modules.FirstOrDefault(c => c.Id == model.Id);
                if (module == null)
                {
                    throw new Exception();
                }
                var other = Modules.FirstOrDefault(c => c.Id != model.Id && c.Name == model.Name.Trim());
                if (other != null)
                {
                    return new Result(ResultType.Warning, "数据库中已经存在相同名称的模块，请修改后重新提交！");
                }
                module.Name = model.Name.Trim();
                module.ParentId = model.ParentId;
                module.LinkUrl = model.LinkUrl;
                module.IsMenu = model.IsMenu;
                module.Code = model.Code;
                module.Description = model.Description;
                module.Enabled = model.Enabled;
                module.UpdateTime = DateTime.Now;
                _moduleRepository.Update(module);
                return new Result(ResultType.Success, "更新数据成功！");
            }
            catch
            {
                return new Result(ResultType.Error, "更新数据失败!");
            }
        }


        public Result Delete(IEnumerable<ModuleVM> list)
        {
            try
            {
                if (list != null)
                {
                    var moduleIds = list.Select(c => c.Id).ToList();
                    int count = _moduleRepository.DbSet.Where(c => moduleIds.Contains(c.Id)).Delete();
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

    }
}