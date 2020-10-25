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
    class UserMessageService : IUserMessageService
    {
        private IUserMessageDao userMessageDao = DaoFactory.getInstance().GetUserMessageDao();
        public void create(UserMessage t)
        {
            userMessageDao.create(t);
        }

        public void delete(UserMessage t)
        {
            userMessageDao.delete(t);
        }

        public List<UserMessage> getAllMessageByUserId(int id)
        {
            return userMessageDao.getAllMessageByUserId(id);
        }

        public UserMessage readById(int id)
        {
            return userMessageDao.readById(id);
        }

        public UserMessage update(int id, UserMessage t)
        {
            return userMessageDao.update(id, t);
        }
    }
}
