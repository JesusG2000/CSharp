using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.dto
{
    enum UserOperation
    {

        CREATE = 1,
        READ,
        UPDATE,
        DELETE,
        GET_ALL_REGISTERED,
        IS_EXIST,
        IS_REGISTERED,
        READ_BY_LOGIN

    }
}
