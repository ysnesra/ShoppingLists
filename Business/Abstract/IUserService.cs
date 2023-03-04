using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        IResult Add(User user);
        IResult Update(User user);
        IResult Delete(User user);

        IResult AddUserDto(UserDetailRegisterDto userAdd);
        IResult EditUserDto(UserEditDto userEdit); 
        IResult DeleteUserDto(int userId);

        IDataResult<List<User>> GetAll();
        IDataResult<User> GetById(int userId);      //tek bir kullanıcının detayını getirir     
        IDataResult<User> GetUserDetailsByEmail(string email);  //Maile göre kullanıcıyı getirir

        IDataResult<List<UserDetailRegisterDto>> GetUserLists();
        IDataResult <UserEditDto> GetByUserIdDto(int userId);
        IDataResult <UserLoginDto> GetByLoginFilter(UserLoginDto userLogin);
       


    }
}
