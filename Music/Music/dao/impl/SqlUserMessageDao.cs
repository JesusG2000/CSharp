using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Music.dto;
using Music.db;

namespace Music.dao.impl
{
    class SqlUserMessageDao : IUserMessageDao
    {
        private static int NOT_PARAM = -1;
        public void create(UserMessage t)
        {
            using (TestDbContext context = new TestDbContext())
            {
                context.UserMessageOperation((int)UserMessageOperation.CREATE, NOT_PARAM, t.MessageId, t.UserGetterId);
                context.SaveChanges();
            }
        }

        public void delete(UserMessage t)
        {
            using (TestDbContext context = new TestDbContext())
            {
                context.UserMessageOperation((int)UserMessageOperation.DELETE,t.Id);
                context.SaveChanges();
            }
        }

        public List<UserMessage> getAllMessageByUserId(int id)
        {
            using (TestDbContext context = new TestDbContext())
            {
                var fromDb = context.UserMessageOperation((int)UserMessageOperation.GET_ALL_MESSAGE_BY_USER_ID, id);
                return fromDb.Select(x => new UserMessage
                {
                    Id = x.Id,
                    MessageId = x.MessageId,
                    UserGetterId = x.UserGetterId
                }
                ).ToList();
            }
        }

        public UserMessage readById(int id)
        {
            using (TestDbContext context = new TestDbContext())
            {
               var fromDb =  context.UserMessageOperation((int)UserMessageOperation.DELETE, id).GetEnumerator();
                fromDb.MoveNext();
                return fromDb.Current;

            }
        }

        public UserMessage update(int id, UserMessage t)
        {
            throw new NotImplementedException();
        }
    }
}
