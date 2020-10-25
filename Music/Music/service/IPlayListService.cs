using Music.db;
using Music.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.service
{
    interface IPlayListService:Service<PlayList>
    {
        PlayList readNyName(string name);
        List<PlayList> getAllByUserId(int userId);
    }
}
