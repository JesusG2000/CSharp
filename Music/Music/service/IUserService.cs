using Music.db;
using Music.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.service
{
    interface IUserService:Service<DUser>
    {
        List<DUser> getAllUser();
        bool isRegistered(DUser user);
        bool isExist(DUser user);

        DUser readByName(string name);
        List<DUser> getAllRegisterUser();
        //string encryptPassword(string pass);
        // string decryptPassword(string encryptPass);
    }
}
