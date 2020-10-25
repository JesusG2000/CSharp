using Music.controller;
using Music.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.command.other
{
    class GetAllSongsByPlayListId : ICommand
    {
        private ISongService songService = ServiceFactory.getInstance().GetSongService();
        public object Execute(object request)
        {
            return songService.getAllSongsByPlayListId((int)request);
        }
    }
}
