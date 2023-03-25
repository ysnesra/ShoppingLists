using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
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
            //Aynı mail adresinden kayıt varsa eklemesin iş kuralı Parçacığı
            IResult result = BusinessRules.Run(CheckIfEmailExists(userAdd.Email));

            //result null geliyorsa-->Yani bütün kurallara uyuyordur
            //null gelmezse hata mesajı gelmiş demektir
            if (result != null)
            {
                return result;
            }
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

        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult(Messages.UserDeleted);
        }

        public IResult DeleteUserDto(int userId)
        {
            //Böyle bir kullanıcı sistemde var mı iş kuralı Parçacığı
            IResult result = BusinessRules.Run(CheckIfUserIdExists(userId));

            //result null geliyorsa-->Yani bütün kurallara uyuyordur
            //null gelmezse hata mesajı gelmiş demektir
            if (result != null)
            {
                return result;
            }
            User dbuser = new User(); 
            dbuser.UserId=userId ;

            _userDal.Delete(dbuser);
            return new SuccessResult(Messages.UserDeleted);
        }

        public IResult EditUserDto(UserEditDto userEdit)
        {
            //Böyle bir kullanıcı sistemde var mı iş kuralı Parçacığı
            IResult result = BusinessRules.Run(CheckIfUserIdExists(userEdit.UserId));
            
            if (result != null)
            {
                return result;
            }

            User existUser = new User();
            existUser.UserId=userEdit.UserId;   
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
                UserId = existUser.UserId,
                Email = existUser.Email,
                Password = existUser.Password,
                Role = existUser.Role,

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
            return new SuccessDataResult<User>(_userDal.Get(u => u.Email == email));
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

        //İş kuralı parçacığı //Aynı mail adresinden kayıt varsa eklemesin
        //Mailadresi var mı metotu
        private IResult CheckIfEmailExists(string email)
        {
            //Any --> var mı demek, varsa true dönderir. //bool sonuç
            var result = _userDal.GetAll(x => x.Email == email).Any();

            if (result == true)
            {
                return new ErrorResult(Messages.EmailAlreadyExists);
            }
            return new SuccessResult();
        }

        //İş kuralı parçacığı //Böyle bir kullanıcı sistemde kayıtlı mı
        private IResult CheckIfUserIdExists(int userId)
        {
            var result = _userDal.Get(x => x.UserId == userId);

            if (result == null)
            {
                return new ErrorResult(Messages.UserIdNoExists);
            }
            return new SuccessResult();
        }

    }
}
