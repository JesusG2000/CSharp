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
    class SqlPlayListSongDao : IPlayListSongDao
    {
        private static int NOT_PARAM = -1;
        public void create(PlayListSong t)
        {
            using (TestDbContext context = new TestDbContext())
            {
                context.PlayListSongOperation((int)PlayListSongOperation.CREATE, NOT_PARAM, t.SongId, t.PlayListId);
                context.SaveChanges();
            }
        }

        public void delete(PlayListSong t)
        {
            using (TestDbContext context = new TestDbContext())
            {
                context.PlayListSongOperation((int)PlayListSongOperation.DELETE, t.Id);
                context.SaveChanges();
            }
        }

       

        public PlayListSong readById(int id)
        {
            using (TestDbContext context = new TestDbContext())
            {
                var fromDb = context.PlayListSongOperation((int)PlayListSongOperation.READ, id).GetEnumerator();
                fromDb.MoveNext();
                return fromDb.Current;

            }
           
        }

        public PlayListSong readBySongAndPlayListIds(int songId, int playListId)
        {
            using (TestDbContext context = new TestDbContext())
            {
                var fromDb = context.PlayListSongOperation((int)PlayListSongOperation.READ_BY_SONG_AND_PLAYLIST_ID, NOT_PARAM , songId,playListId).GetEnumerator();
                fromDb.MoveNext();
                return fromDb.Current;
            }
           
        }

        public PlayListSong update(int id, PlayListSong t)
        {
            throw new NotImplementedException();
        }
    }
}
