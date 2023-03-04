using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class UserLoginValidator : AbstractValidator<UserLoginDto>
    {
        //Solid- Single Responsibility prensibinden dolayı herbir kuralı ayrı ayrı satır oluşturarak veririz
        public UserLoginValidator()
        {   
            RuleFor(u => u.Email).NotEmpty().WithMessage("Mail alanı boş geçilemez");
            RuleFor(u => u.Email).EmailAddress().WithMessage("Geçerli bir e-Mail adresi giriniz! ");

            RuleFor(u => u.Password).NotEmpty().WithMessage("Parola alanı boş geçilemez!"); 
            RuleFor(u=>u.Password).Matches(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$").WithMessage("Parolanız en az sekiz karakter, en az bir harf ve bir sayı içermelidir!");

            // RuleFor(u => u.Password).Must(IsPasswordValid).WithMessage("Parolanız en az sekiz karakter, en az bir harf ve bir sayı içermelidir!");

           // RuleFor(u => u.Role).NotEmpty().WithMessage("Role alanı boş geçilemez!");
        }

        //Password en az 8 karakter olmalı ve büyük harf, küçük harf ve rakam içerirse True dönen metodumuz
        private bool IsPasswordValid(string arg)
        {
            Regex regex = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$");
            return regex.IsMatch(arg);
        }
    }
}
