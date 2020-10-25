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
    class SqlMessageConclusionTimeDao : IMessageConclusionTimeDao
    {
        private static int NOT_PARAM = -1;
        public void create(MessageConclusionTime t)
        {
            using (TestDbContext context = new TestDbContext())
            {
                context.MessageConclusionTimeOperation((int)MessageConclusionTimeOperation.CREATE, NOT_PARAM, t.FirstUserId, t.SecondUserId, t.FirstUserDate, t.SecondUserDate);
                context.SaveChanges();
            }
        }

        public void delete(MessageConclusionTime t)
        {
            using (TestDbContext context = new TestDbContext())
            {
                context.MessageConclusionTimeOperation((int)MessageConclusionTimeOperation.DELETE, t.Id);

                context.SaveChanges();
            }
        }

        public MessageConclusionTime findByUsersIds(int firstUserId, int secondUserId)
        {
            using (TestDbContext context = new TestDbContext())
            {
                var fromDb = context.MessageConclusionTimeOperation((int)MessageConclusionTimeOperation.FIND_BY_USERS_IDS, NOT_PARAM, firstUserId, secondUserId).GetEnumerator();
                fromDb.MoveNext();
                return fromDb.Current;

            }
        }

        public List<MessageConclusionTime> getAllByUserId(int userId)
        {
            using (TestDbContext context = new TestDbContext())
            {
                var fromDb = context.MessageConclusionTimeOperation((int)MessageConclusionTimeOperation.FIND_BY_USERS_IDS, userId);
                return fromDb.Select(x => new MessageConclusionTime
                {
                    Id = x.Id,
                    FirstUserId = x.FirstUserId,
                    SecondUserId = x.SecondUserId,
                    FirstUserDate = x.FirstUserDate,
                    SecondUserDate = x.SecondUserDate,
                }
                ).ToList();


            }
        }

        public MessageConclusionTime readById(int id)
        {
            using (TestDbContext context = new TestDbContext())
            {
                var fromDb = context.MessageConclusionTimeOperation((int)MessageConclusionTimeOperation.READ, id).GetEnumerator();
                fromDb.MoveNext();
                return fromDb.Current;
            }

        }

        public MessageConclusionTime update(int id, MessageConclusionTime t)
        {

            using (TestDbContext context = new TestDbContext())
            {
                var fromDb = context.MessageConclusionTimeOperation((int)MessageConclusionTimeOperation.UPDATE, id, t.FirstUserId, t.SecondUserId, t.FirstUserDate, t.SecondUserDate).GetEnumerator();
                fromDb.MoveNext();
                context.SaveChanges();
                return fromDb.Current;
            }

        }
    }
}
