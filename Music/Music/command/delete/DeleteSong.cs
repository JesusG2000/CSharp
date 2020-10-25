using Music.controller;
using Music.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.command.delete
{
    class DeleteSong : ICommand
    {
        private ISongService songService = ServiceFactory.getInstance().GetSongService();
       

        public object Execute(object request)
        {
            Song song = (Song)request;
          
            songService.delete(song);
            return null;
        }
    }
}
