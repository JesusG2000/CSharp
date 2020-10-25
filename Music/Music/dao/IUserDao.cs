using Music.db;
using Music.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.dao
{
    interface IUserDao : Dao<DUser>
    {
        List<DUser> getAllUser();
        bool isRegistered(DUser user);

        bool isExist(DUser user);

        DUser readByLogin(string name);
        List<DUser> getAllRegisterUser();
    }
}
