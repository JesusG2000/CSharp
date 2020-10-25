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
    class MessageConclusionTimeService : IMessageConclusionTimeService
    {
        private IMessageConclusionTimeDao conclusionTimeDao = DaoFactory.getInstance().GetMessageConclusionTimeDao();
        public void create(MessageConclusionTime t)
        {
            conclusionTimeDao.create(t);
        }

        public void delete(MessageConclusionTime t)
        {
            conclusionTimeDao.delete(t);
        }

        public MessageConclusionTime findByUsersIds(int firstUserId, int secondUserId)
        {
            return conclusionTimeDao.findByUsersIds(firstUserId, secondUserId);
        }

        public List<MessageConclusionTime> getAllByUserId(int userId)
        {
            return conclusionTimeDao.getAllByUserId(userId);
        }

        public MessageConclusionTime readById(int id)
        {
            return conclusionTimeDao.readById(id);
        }

        public MessageConclusionTime update(int id, MessageConclusionTime t)
        {
            return conclusionTimeDao.update(id, t);
        }
    }
}
