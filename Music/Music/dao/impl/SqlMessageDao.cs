using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Music.dto;
using Music.db;
using System.Windows.Media.Media3D;
using Newtonsoft.Json.Linq;

namespace Music.dao.impl
{
    class SqlMessageDao : IMessageDao
    {
        private static int NOT_PARAM = -1;
        public void create(Message t)
        {
            using (TestDbContext context = new TestDbContext())
            {
                context.MessageOperation((int)MessageOperation.CREATE, NOT_PARAM, t.Text, t.UserSenderId, t.Date);
                context.SaveChanges();
            }
        }

        public Message createAndReturn(Message message)
        {
            using (TestDbContext context = new TestDbContext())
            {
                var fromDb =  context.MessageOperation((int)MessageOperation.CREATE_AND_RETURN, NOT_PARAM, message.Text, message.UserSenderId, message.Date).GetEnumerator();
                fromDb.MoveNext();
                return fromDb.Current;
            }
        }

        public void delete(Message t)
        {
            using (TestDbContext context = new TestDbContext())
            {
                context.MessageOperation((int)MessageOperation.DELETE, t.Id);
                context.SaveChanges();
            }
        }

        public Message readById(int id)
        {
            using (TestDbContext context = new TestDbContext())
            {
                var fromDb = context.MessageOperation((int)MessageOperation.READ, id).GetEnumerator();
                fromDb.MoveNext();
                return fromDb.Current;
            }
           
        }

        public Message update(int id, Message t)
        {
            throw new NotImplementedException();
        }
    }
}
