using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    //Encapsulation 
    //işlem sonucunu(data) ve işlem mesajını(ok) aynı anda sonuç olarak verebilmek için oluşturuldu

    public interface IResult
    {
        bool Success { get; }  //constructorda set edeceğiz

        string Message { get; }
    }
}
