using Music.dao.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.dao
{
   sealed class DaoFactory
    {
        private static DaoFactory instance = new DaoFactory();

        private IUserDao userDao = new SqlUserDao();
        private IMessageDao messageDao = new SqlMessageDao();
        private ICommentDao commentDao = new SqlCommentDao();
        private IMessageConclusionTimeDao messageConclusion = new SqlMessageConclusionTimeDao();
        private IPlayListDao playListDao = new SqlPlayListDao();
        private IPlayListSongDao playListSongDao = new SqlPlayListSongDao();
        private ISongDao songDao = new SqlSongDao();
      
        private IUserMessageDao userMessageDao = new SqlUserMessageDao();
        private DaoFactory()
        {

        }
        public static DaoFactory getInstance()
        {
            return instance;
        }
        public IUserDao GetUserDao()
        {
            return userDao;
        }
        public IMessageDao GetMessageDao()
        {
            return messageDao;
        }
        public ICommentDao GetCommentDao()
        {
            return commentDao;
        }
        public IMessageConclusionTimeDao GetMessageConclusionTimeDao()
        {
            return messageConclusion;
        }
        public IPlayListDao GetPlayListDao()
        {
            return playListDao;
        }
        public IPlayListSongDao GetPlayListSongDao()
        {
            return playListSongDao;
        }
        public ISongDao GetSongDao()
        {
            return songDao;
        } 
        public IUserMessageDao GetUserMessageDao()
        {
            return userMessageDao;
        }
    }
}
