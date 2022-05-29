using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public interface IUserDAO
    {
        int User_Login(string UserAccount, string UserPassword);

        int User_Register(string UserAccount, string UserPassword, string UserFullName, string UserAddress, string UserPhoneNumber,int RoleID);

        List<UserDTO> Users_GetList();

    }
}
