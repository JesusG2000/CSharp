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
    class PlayListService : IPlayListService
    {
        private IPlayListDao playListDao = DaoFactory.getInstance().GetPlayListDao();
        public void create(PlayList t)
        {
            playListDao.create(t);
        }

        public void delete(PlayList t)
        {
            playListDao.delete(t);
        }

        public List<PlayList> getAllByUserId(int userId)
        {
            return playListDao.getAllByUserId(userId);
        }

        public PlayList readById(int id)
        {
           return playListDao.readById(id);
        }

        public PlayList readNyName(string name)
        {
            return playListDao.readByName(name);
        }

        public PlayList update(int id, PlayList t)
        {
            return playListDao.update(id, t);
        }
    }
}
