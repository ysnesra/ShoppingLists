using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Interceptors
{
    public abstract class MethodInterception : MethodInterceptionBaseAttribute
    {
        protected virtual void OnBefore(IInvocation invocation) { }
        protected virtual void OnAfter(IInvocation invocation) { }
        protected virtual void OnException(IInvocation invocation, System.Exception e) { }
        protected virtual void OnSuccess(IInvocation invocation) { }
        public override void Intercept(IInvocation invocation)

        //invocation aslında çalıştırmak istediğimiz metotu temsil ediyor. Mesela; Add metotu
        {
            var isSuccess = true;
            OnBefore(invocation);       //Metotun başında çalıştır    
            try
            {
                invocation.Proceed();
            }
            catch (Exception e)
            {
                isSuccess = false;
                OnException(invocation, e);     //Hata alırsa çalıştır
                throw;
            }
            finally
            {
                if (isSuccess)
                {
                    OnSuccess(invocation);      //Metot başarılı olduğunda çalışsın
                }
            }
            OnAfter(invocation);         //Metottan sonra çalışsın
        }
    }
}