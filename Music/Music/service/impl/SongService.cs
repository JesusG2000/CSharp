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
    class SongService : ISongService
    {
        private ISongDao songDao = DaoFactory.getInstance().GetSongDao();
        public void create(Song t)
        {
            songDao.create(t);
        }

        public void delete(Song t)
        {
            songDao.delete(t);
        }

        public List<Song> getAllSong()
        {
            return songDao.getAllSong();
        }

        public Song readById(int id)
        {
            return songDao.readById(id);
        }

        public List<Song> getAllSongsByPlayListId(int id)
        {
            return songDao.getAllSongsByPlayListId(id);
        }

        public Song update(int id, Song t)
        {
            return songDao.update(id, t);
        }
    }
}
