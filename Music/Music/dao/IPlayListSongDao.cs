using Music.db;
using Music.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.dao
{
    interface IPlayListSongDao:Dao<PlayListSong>
    {
        

        PlayListSong readBySongAndPlayListIds(int songId, int playListId);
    }
}
