using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T data, string message) : base(data, true, message)
        {

        }
        //sadece datayı (sonucu) verebiliriz
        public SuccessDataResult(T data) : base(data, true)
        {

        }
        //sadece mesaj verebiliriz
        public SuccessDataResult(string message) : base(default, true, message)
        {

        }
        //hiç birşey vermeyebiliriz
        public SuccessDataResult() : base(default, true)
        {

        }
    }
}
