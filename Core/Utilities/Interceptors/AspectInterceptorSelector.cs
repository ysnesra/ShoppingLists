using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Interceptors
{
    //Classın attributelarını oku, metotun attributelarını oku
    //Onları bir listeye koy ve priority ye(öncelik değerine) göre sırala

    //public class AspectInterceptorSelector : IInterceptorSelector
    //{
    //    public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
    //    {
    //        var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>
    //            (true).ToList();
    //        var methodAttributes = type.GetMethod(method.Name)
    //            .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
    //        classAttributes.AddRange(methodAttributes);

    //        return classAttributes.OrderBy(x => x.Priority).ToArray();
    //    }
    //}
}

