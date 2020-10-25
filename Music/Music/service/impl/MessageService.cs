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
    class MessageService : IMessageService
    {
        private IMessageDao messageDao = DaoFactory.getInstance().GetMessageDao();
        public void create(Message t)
        {
            messageDao.create(t);
        }

        public Message createAndReturn(Message message)
        {
            return messageDao.createAndReturn(message);
        }

        public void delete(Message t)
        {
            messageDao.delete(t);
        }

        public Message readById(int id)
        {
           return messageDao.readById(id);
        }

        public Message update(int id, Message t)
        {
            return messageDao.update(id, t);
        }
    }
}
