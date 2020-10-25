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
    class CommentService : ICommentService
    {
        private ICommentDao commentDao = DaoFactory.getInstance().GetCommentDao();
        public void create(Comment t)
        {
            commentDao.create(t);
        }

        public void delete(Comment t)
        {
            commentDao.delete(t);
        }

        public List<Comment> getAllBySongId(int songId)
        {
            return commentDao.getAllBySongId(songId);
        }

        public List<Comment> getAllByUserId(int userId)
        {
            return commentDao.getAllByUserId(userId);
        }

        public Comment readById(int id)
        {
            return commentDao.readById(id);
        }

        public Comment update(int id, Comment t)
        {
            return commentDao.update(id, t);
        }
    }
}
