using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P.PContext.Interface
{
   public  interface IUnitOfWork
    {
        /// <summary>
        /// 提交当前单元操作的结果
        /// </summary>
        /// <returns></returns>
        int Commit();
    }
}
