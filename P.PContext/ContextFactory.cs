using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace P.PContext
{
   public class ContextFactory
    {
        //帮我们返回当前线程内的数据库上下文，如果当前线程内没有上下文，那么创建一个上下文，并保证

        //上线问实例在线程内部是唯一的

        public static BaseContext GetCurrentDbContext(bool isReadOnly=false)
        {
            //CallContext：是线程内部唯一的独用的数据槽（一块内存空间）
            //传递DbContext进去获取实例的信息，在这里进行强制转换。
            var  dbContext = CallContext.GetData("DbContext") as BaseContext;

            if (dbContext == null)  //线程在数据槽里面没有此上下文
            {
                dbContext = new BaseContext(isReadOnly); //如果不存在上下文的话，创建一个EF上下文

                //我们在创建一个，放到数据槽中去
                CallContext.SetData("DbContext", dbContext);
            }
            return dbContext;

        }
    }
}
