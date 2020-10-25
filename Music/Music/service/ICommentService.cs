using Music.db;
using Music.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.service
{
    interface ICommentService:Service<Comment>
    {
        List<Comment> getAllByUserId(int userId);
        List<Comment> getAllBySongId(int songId);
    }
}
