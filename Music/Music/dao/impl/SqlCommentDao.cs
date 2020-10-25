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
    class SqlCommentDao : ICommentDao
    {
        private static int NOT_PARAM = -1;
        public void create(Comment t)
        {
            using (TestDbContext context = new TestDbContext())
            {
                context.CommentOperation((int)CommentOperation.CREATE, NOT_PARAM, t.Text, t.Date, t.UserId, t.SongId);
                context.SaveChanges();
            }
        }

        public void delete(Comment t)
        {
            using (TestDbContext context = new TestDbContext())
            {
                context.CommentOperation((int)CommentOperation.DELETE, t.Id);
                context.SaveChanges();
            }
        }

        public List<Comment> getAllBySongId(int songId)
        {
            List<Comment> comments = new List<Comment>();
            using (TestDbContext context = new TestDbContext())
            {
                var commentsFromDb = context.CommentOperation((int)CommentOperation.GET_ALL_BY_SONG_ID, songId);
                return commentsFromDb.Select(x => new Comment
                {
                    Id = x.Id,
                    Text = x.Text,
                    Date = x.Date,
                    UserId = x.UserId,
                    SongId = x.SongId,

                }).ToList();
               

            }
           
        }

        public List<Comment> getAllByUserId(int userId)
        {
           
            using (TestDbContext context = new TestDbContext())
            {
                var commentsFromDb = context.CommentOperation((int)CommentOperation.GET_ALL_BY_USER_ID, userId);
                return commentsFromDb.Select(x => new Comment
                {
                    Id = x.Id,
                    Text = x.Text,
                    Date = x.Date,
                    UserId = x.UserId,
                    SongId = x.SongId,

                }).ToList();
               
            }
        }

        public Comment readById(int id)
        {
            using (TestDbContext context = new TestDbContext())
            {
                var comment = context.CommentOperation((int)CommentOperation.READ, id).GetEnumerator();
                comment.MoveNext();
                return comment.Current;
            }
            
        }

        public Comment update(int id, Comment t)
        {
            throw new NotImplementedException();
        }
    }
}
