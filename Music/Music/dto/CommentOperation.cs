using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.dto
{
    enum CommentOperation
    {
        CREATE = 1,
        READ,
        DELETE,
        GET_ALL_BY_SONG_ID,
        GET_ALL_BY_USER_ID

    }
}
