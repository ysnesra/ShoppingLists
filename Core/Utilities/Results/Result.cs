using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
       
        //ekrana hem true/false hem message döndermek isteyebilirz
        public Result(bool success, string message) :this(success)
        {
            //this(success) ile Result classının tek parametreli constructorına successi yolla
            Message = message;
        }

        //ekrana sadece true/false döndermek isteyebilirz
        public Result(bool success)   
        {
            Success = success;
        }

        public bool Success { get; }
       
        public string Message { get; }
       
    }
}
