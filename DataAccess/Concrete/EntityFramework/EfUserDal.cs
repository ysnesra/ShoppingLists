using Core.DataAccess.Entityframework;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, DbShoppingListContext>, IUserDal
    {


        //public void AddUserDto(UserDetailRegisterDto userAdd)
        //{
        //    using (DbShoppingListContext context = new DbShoppingListContext())
        //    {
        //        if (context.Users.Any(u=>u.Email.ToLower()==userAdd.Email.ToLower()))
        //        {
        //            throw new Exception("Aynı mail adresinde kullanıcı bulunmaktadır");
        //        }
        //        else
        //        {
        //            var result = context.Users.Add(new User
        //            {
        //                //UserId = userAdd.UserId,
        //                FirstName = userAdd.FirstName,
        //                LastName = userAdd.LastName,
        //                Email = userAdd.Email,
        //                Password = userAdd.Password
        //            });

        //            context.SaveChanges();
        //        }

        //    }
        //}

        //public void EditUserDto(UserEditDto userEdit)
        //{
        //    using (DbShoppingListContext context = new DbShoppingListContext())
        //    {
        //        var editUser = context.Users.FirstOrDefault(u => u.UserId == userEdit.UserId);
        //        if(editUser == null)
        //        {
        //            //throw new Exception("Güncellenecek kişi kayıtlarda bulunmmaktadır.");
        //            throw new NotImplementedException();
        //        }
        //        else
        //        {
        //            //editUser.UserId=userEdit.UserId;
        //            editUser.FirstName = userEdit.FirstName;                    
        //            editUser.LastName = userEdit.LastName;
        //            editUser.Email = userEdit.Email;
        //            editUser.Password = userEdit.Password;
        //        }

        //        context.Users.Update(editUser);
        //        context.SaveChanges();
        //    }
        //}

        //public UserDetailRegisterDto GetByUserIdDto(UserDetailRegisterDto userDetail)
        //{
        //    using (DbShoppingListContext context = new DbShoppingListContext())
        //    {
        //        var userDto = context.Users.FirstOrDefault(u => u.UserId == userDetail.UserId);

        //        var useredit = new UserDetailRegisterDto()
        //        {
        //            UserId = userDto.UserId,
        //            FirstName = userDto.FirstName,
        //            LastName = userDto.LastName,
        //            Email = userDto.Email,
        //            Password = userDto.Password,
        //        };

        //        return useredit;

        //    }
        //}

        //public UserLoginDto GetLoginMatch(UserLoginDto userLogin)
        //{
        //    using (DbShoppingListContext context = new DbShoppingListContext())
        //    {
        //        var user = context.Users.SingleOrDefault(u => u.Email.ToLower() == userLogin.Email.ToLower() && u.Password == userLogin.Password);

        //        if (user != null)
        //        {
        //            //Claims listesi oluşturp cookide neleri tutcağımız tanımlandı
        //            List<Claim> claims = new List<Claim>();
        //            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()));
        //            claims.Add(new Claim(ClaimTypes.Email, user.Email));
        //            claims.Add(new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName));

        //            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme
        //                );
        //        }
        //        else
        //        {
        //            return null;
        //        }

        //    }
        //}

        //public List<UserDetailRegisterDto> GetUserLists()
        //{
        //    using (DbShoppingListContext context = new DbShoppingListContext())
        //    {
        //        var userDtoList = context.Users.Select(x => new UserDetailRegisterDto
        //        { 
        //            UserId = x.UserId,
        //            FirstName = x.FirstName,
        //            LastName = x.LastName,  
        //            Email = x.Email,
        //            Password = x.Password,  
        //            RePassword=x.Password

        //        }).ToList();

        //        return userDtoList;
        //    }
        //}
    }
}
