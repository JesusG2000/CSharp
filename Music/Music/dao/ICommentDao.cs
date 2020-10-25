using Music.db;
using Music.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.dao
{
    interface ICommentDao:Dao<Comment>
    {
        List<Comment> getAllByUserId(int userId);
        List<Comment> getAllBySongId(int userId);
    }
}
