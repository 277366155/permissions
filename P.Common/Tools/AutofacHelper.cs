using Autofac.Integration.Mvc;
using Autofac;

namespace P.Common.Tools
{
    public class AutofacHelper : IAutofacHelper
    {
        public T GetByName<T>(string name)
        {
            return AutofacDependencyResolver.Current.RequestLifetimeScope.ResolveNamed<T>(name);
        }
    }


}
