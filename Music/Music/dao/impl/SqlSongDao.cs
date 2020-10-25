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
    class SqlSongDao : ISongDao
    {
        private static int NOT_PARAM = -1;
        public void create(Song t)
        {
            using (TestDbContext context = new TestDbContext())
            {
                context.SongOperation((int)SongOperation.CREATE, NOT_PARAM, t.MultimediaData, t.Name, t.Description, t.Type, t.AuthorName, t.ReleaseDate, t.Album, t.Duraction);
                context.SaveChanges();
            }
        }

        public void delete(Song t)
        {
            using (TestDbContext context = new TestDbContext())
            {
                context.SongOperation((int)SongOperation.DELETE, t.Id);
                context.SaveChanges();

            }
        }

        public List<Song> getAllSong()
        {
            using (TestDbContext context = new TestDbContext())
            {
                var fromDb = context.SongOperation((int)SongOperation.GET_ALL);
                return fromDb.Select(x => new Song
                {
                    Id = x.Id,
                    MultimediaData = x.MultimediaData,
                    Name = x.Name,
                    Description = x.Description,
                    Type = x.Type,
                    AuthorName = x.AuthorName,
                    ReleaseDate = x.ReleaseDate,
                    Album = x.Album,
                    Duraction = x.Duraction,
                }).ToList();
            }
        }

        public Song readById(int id)
        {
            using (TestDbContext context = new TestDbContext())
            {
                var fromDb = context.SongOperation((int)SongOperation.READ, id).GetEnumerator();
                fromDb.MoveNext();
                return fromDb.Current;
            }
        }

        public List<Song> getAllSongsByPlayListId(int id)
        {

           
            using (TestDbContext context = new TestDbContext())
            {
                var fromDb = context.SongOperation((int)SongOperation.GET_ALL_SONGS_BY_PLAYLIST_ID,id);
                return fromDb.Select(x => new Song
                {
                    Id = x.Id,
                    MultimediaData = x.MultimediaData,
                    Name = x.Name,
                    Description = x.Description,
                    Type = x.Type,
                    AuthorName = x.AuthorName,
                    ReleaseDate = x.ReleaseDate,
                    Album = x.Album,
                    Duraction = x.Duraction,
                }).ToList();
            }
           
        }

        public Song update(int id, Song t)
        {
            using (TestDbContext context = new TestDbContext())
            {
                var fromDb = context.SongOperation((int)SongOperation.UPDATE, id, t.MultimediaData, t.Name, t.Description, t.Type, t.AuthorName, t.ReleaseDate, t.Album, t.Duraction).GetEnumerator();
                context.SaveChanges();
                fromDb.MoveNext();
                return fromDb.Current;
            }
        }
    }
}
