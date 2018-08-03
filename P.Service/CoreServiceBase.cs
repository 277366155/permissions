using P.PContext.Interface;

namespace P.Service
{
    /// <summary>
    /// 核心业务实现基类
    /// </summary>
    public abstract class CoreServiceBase
    {
        protected IUnitOfWork UnitOfWork;

        protected CoreServiceBase(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}
