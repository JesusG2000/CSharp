using Music.db;
using Music.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.dao
{
    interface IPlayListDao:Dao<PlayList>
    {
        PlayList readByName(string name);
        List<PlayList> getAllByUserId(int userId);
    }
}
