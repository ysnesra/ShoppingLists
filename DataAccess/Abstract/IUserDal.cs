using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        //List<UserDetailRegisterDto> GetUserLists();

        //UserDetailRegisterDto GetByUserIdDto(int userId); 
        //UserLoginDto GetLoginMatch(UserLoginDto userLogin);


        //void AddUserDto(UserDetailRegisterDto userAdd); 
        //void EditUserDto(UserEditDto userAdd); 
    }
}
