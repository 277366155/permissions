using P.PContext.Interface;

namespace P.PContext
{
    public class UnitOfWork : IUnitOfWork
    {
        protected BaseContext Context
        {
            get
            {
                return ContextFactory.GetCurrentDbContext(); 
            }
        }

        public int Commit()
        {
            return Context.SaveChanges();
        }
    }
}
