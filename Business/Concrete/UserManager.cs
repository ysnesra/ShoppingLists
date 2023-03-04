using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IResult Add(User user)
        {
            //*validation olarak UserValidator clasına Password kurallarını yazdım          
            _userDal.Add(user);
            return new SuccessResult(Messages.UserAdded);      
        }

        //[ValidationAspect(typeof(UserRegisterValidator))]
        public IResult AddUserDto(UserDetailRegisterDto userAdd)
        {
            //Bu mail adresi kullanılıyor mu kontrol edelim 
            var dbUser = _userDal.Get(x => x.Email == userAdd.Email);
            if (dbUser is null)   //db de bu mail yok demekki ekleyebilriz
            {
                User newUser = new User()
                {
                    //UserId = userAdd.UserId, gerek yok 0 zaten
                    FirstName = userAdd.FirstName,
                    LastName = userAdd.LastName,
                    Email = userAdd.Email,
                    Password = userAdd.Password
                };
                _userDal.Add(newUser);

                return new SuccessResult(Messages.UserAdded);
            }
            return new ErrorResult(Messages.NoAdded);    
        }

        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult(Messages.UserDeleted);
        }

        public IResult DeleteUserDto(int userId)
        {
            User dbUser= _userDal.Get(x => x.UserId == userId);
            _userDal.Delete(dbUser);
            return new SuccessResult(Messages.UserDeleted);
        }

        public IResult EditUserDto(UserEditDto userEdit)
        {
            var existUser = _userDal.Get(x => x.UserId == userEdit.UserId);
            if (existUser is null)              
                throw new Exception("Aynı mail adresinde kullanıcı bulunmaktadır");
                //ModelState.AddModelError("", "Aynı mail adresinde kullanıcı bulunmaktadır");

            existUser.FirstName = userEdit.FirstName;
            existUser.LastName = userEdit.LastName;
            existUser.Email = userEdit.Email;
            existUser.Password = userEdit.Password;

            _userDal.Update(existUser);
        
            return new SuccessResult(Messages.UserUpdated);
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll(), Messages.UsersListed);
        }

        public IDataResult<User> GetById(int userId)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.UserId == userId), Messages.UserDetail);
        }
        
        public IDataResult<UserLoginDto> GetByLoginFilter(UserLoginDto userLogin)
        {

            var existUser = _userDal.Get(x => x.Email == userLogin.Email && x.Password == userLogin.Password);
            if (existUser is null)
            {
                throw new Exception("Bu Mail ve Parola ya ait kullanıcı bulunamadı");
                return null;
            }

            UserLoginDto response = new()
            {
                UserId=existUser.UserId,
                Email = existUser.Email,
                Password = existUser.Password,
                Role=existUser.Role,
              
            };

            return new SuccessDataResult<UserLoginDto>(response);
        }

        public IDataResult<UserEditDto> GetByUserIdDto(int userId)
        {
            var user = _userDal.Get(x => x.UserId == userId);

            UserEditDto response = new()
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
            };
         
            return new SuccessDataResult<UserEditDto>(response, Messages.UsersListed);
        }

        public IDataResult<User> GetUserDetailsByEmail(string email)
        {
            return new SuccessDataResult<User>(_userDal.Get(u=>u.Email==email));
        }

        public IDataResult<List<UserDetailRegisterDto>> GetUserLists()
        {
            var userlists = _userDal.GetAll();
            List<UserDetailRegisterDto> response = userlists.Select(x => new UserDetailRegisterDto
            {
                UserId = x.UserId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Password = x.Password,
                RePassword = x.Password
            }).ToList();
             
            return new SuccessDataResult<List<UserDetailRegisterDto>>(response, Messages.UsersListed);
        }
      
        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult(Messages.UserUpdated);
        }
    }
}
