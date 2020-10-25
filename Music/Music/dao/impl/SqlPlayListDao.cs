using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Music.dto;
using Music.db;
using Newtonsoft.Json.Linq;

namespace Music.dao.impl
{
    class SqlPlayListDao : IPlayListDao
    {
        private static int NOT_PARAM = -1;
        public void create(PlayList t)
        {
            using (TestDbContext context = new TestDbContext())
            {
                context.PlayListOperation((int)PlayListOperation.CREATE, NOT_PARAM, t.Name, t.Description, t.UserId);
                context.SaveChanges();
            }
        }

        public void delete(PlayList t)
        {
            using (TestDbContext context = new TestDbContext())
            {
                context.PlayListOperation((int)PlayListOperation.DELETE,t.Id);
                context.SaveChanges();
            }
        }

        public List<PlayList> getAllByUserId(int userId)
        {
            
            using (TestDbContext context = new TestDbContext())
            {
                var fromDb = context.PlayListOperation((int)PlayListOperation.GET_ALL_BY_USER_ID, userId);
                return fromDb.Select(x => new PlayList
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    UserId = x.UserId,
                }).ToList();

            }
           
        }

        public PlayList readById(int id)
        {
            using (TestDbContext context = new TestDbContext())
            {
                var fromDb = context.PlayListOperation((int)PlayListOperation.READ, id).GetEnumerator();
                fromDb.MoveNext();
                return fromDb.Current;
            }
          
        }

        public PlayList readByName(string name)
        {
            using (TestDbContext context = new TestDbContext())
            {
                var fromDb = context.PlayListOperation((int)PlayListOperation.READ_BY_NAME, NOT_PARAM,name).GetEnumerator();
                fromDb.MoveNext();
                return fromDb.Current;
            }
            
        }

        public PlayList update(int id, PlayList t)
        {
           
            using (TestDbContext context = new TestDbContext())
            {
                var fromDb = context.PlayListOperation((int)PlayListOperation.UPDATE, id, t.Name, t.Description, t.UserId).GetEnumerator();
                fromDb.MoveNext();
                context.SaveChanges();
                return fromDb.Current;
            }
            
        }
    }
}
