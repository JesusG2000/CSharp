using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Music.dto;
using Music.dao;
using Music.db;

namespace Music.service.impl
{
    class PlayListSongService : IPlayListSongService
    {
        private IPlayListSongDao playListSongDao = DaoFactory.getInstance().GetPlayListSongDao();
        public void create(PlayListSong t)
        {
            playListSongDao.create(t);
        }

        public void delete(PlayListSong t)
        {
            playListSongDao.delete(t);
        }

      

        public PlayListSong readById(int id)
        {
            return playListSongDao.readById(id);
        }

        public PlayListSong readBySongAndPlayListIds(int songId, int playListId)
        {
            return playListSongDao.readBySongAndPlayListIds(songId, playListId);
        }

        public PlayListSong update(int id, PlayListSong t)
        {
            return playListSongDao.update(id, t);
        }
    }
}
