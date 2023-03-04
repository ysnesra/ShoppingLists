using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Core.Utilities.Interceptors
{
    // Bu attribute'u classlara veya metotlara ekleyebilirsin, birden fazla ekleyebilirsin, inherit edilen yerdede bu attribute çalışsın
    //AllowMultiple = true, olması hem veritabanına hem dosyaya loglayabilmesini sağlar

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor
    {
        public int Priority { get; set; }   //Priority__öncelik demek  //hangi attribute önce çalışsın

        public virtual void Intercept(IInvocation invocation)
        {

        }
    }
}
