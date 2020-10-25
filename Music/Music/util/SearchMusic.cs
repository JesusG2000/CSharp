using Music.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.util
{
    class SearchMusic
    {
        private Dictionary<string, bool> pairs;
        private const string NAME = "Name";
        private const string TYPE = "Type";
        private const string AUTHOR_NAME = "AuthorName";
        private const string ALBUM = "Album";
        private const string DURACTION = "Duraction";
        private const string DESCRIPTION = "Description";

        public SearchMusic()
        {
            pairs = new Dictionary<string, bool>();
            pairs.Add(NAME, false);
            pairs.Add(TYPE, false);
            pairs.Add(AUTHOR_NAME, false);
            pairs.Add(ALBUM, false);
            pairs.Add(DURACTION, false);
            pairs.Add(DESCRIPTION, false);
        }
        public void setCheckedOrNot(string name)
        {
            pairs[name] = !pairs[name];
        }
        public List<Song> getSongBySearchParams(List<Song> songs,string text)
        {
            List<Song> listSong = new List<Song>();
            foreach (KeyValuePair<string, bool> keyValue in pairs)
            {
                if (keyValue.Value)
                {
                    List<Song> founded = new List<Song>();
                 
                    switch (keyValue.Key.ToString())
                    {
                        case NAME:
                            {
                                founded=findByName(songs, text);
                                break;
                            }
                        case TYPE:
                            {
                                founded = findByType(songs, text);
                                break;
                            }
                        case AUTHOR_NAME:
                            {
                                founded = findByAuthorName(songs, text);
                                break;
                            }
                        case ALBUM:
                            {
                                founded = findByAlbum(songs, text);
                                break;
                            }
                        case DURACTION:
                            {
                                founded = findByDuraction(songs, text);
                                break;
                            }
                        case DESCRIPTION:
                            {
                                founded = findByDescription(songs, text);
                                break;
                            }
                    }
                    listSong.AddRange(founded);
                }
            }
            listSong = listSong.Distinct().ToList();
            return listSong;

        }

        private List<Song> findByDescription(List<Song> songs, string text)
        {
            List<Song> result = new List<Song>();
            foreach (Song s in songs)
            {
                if (s.Description.Contains(text))
                {
                    result.Add(s);
                }
            }
            return result;
        }

        private List<Song> findByDuraction(List<Song> songs, string text)
        {
            List<Song> result = new List<Song>();
            foreach (Song s in songs)
            {
                if (s.Duraction.ToString().Contains(text))
                {
                    result.Add(s);
                }
            }
            return result;
        }

        private List<Song> findByAlbum(List<Song> songs, string text)
        {
            List<Song> result = new List<Song>();
            foreach (Song s in songs)
            {
                if (s.Album.Contains(text))
                {
                    result.Add(s);
                }
            }
            return result;
        }

        private List<Song> findByAuthorName(List<Song> songs, string text)
        {
            List<Song> result = new List<Song>();
            foreach (Song s in songs)
            {
                if (s.AuthorName.Contains(text))
                {
                    result.Add(s);
                }
            }
            return result;
        }

        private List<Song> findByType(List<Song> songs, string text)
        {
            List<Song> result = new List<Song>();
            foreach (Song s in songs)
            {
                if (s.Type.Contains(text))
                {
                    result.Add(s);
                }
            }
            return result;
        }

        private List<Song> findByName(List<Song> songs, string text)
        {
            List<Song> result = new List<Song>();
            foreach(Song s in songs)
            {
                if (s.Name.Contains(text))
                {
                    result.Add(s);
                }
            }
            return result;
        }
    }
}
