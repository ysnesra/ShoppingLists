using FluentValidation;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Validation
{
    public static class ValidationTool //Static olur.Tek bir instance oluşturur ve uygulama belleği sadece onu kullanır
    {
        public static void Validate(IValidator validator, object entity)   //entity,Dto hepsi girilebilir.Bu yüzden Object tipinde
        {
            var context = new ValidationContext<object>(entity);   //doğrulama yapacağımı söylediğm kod
            var result = validator.Validate(context);          //validate ile kontrolü yapıyor
            if (!result.IsValid)  //sonuç geçerli değilse 
            {
                throw new ValidationException(result.Errors);   //hata fırlatır
            }
        }
    }
}
