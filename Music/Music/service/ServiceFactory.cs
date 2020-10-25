using Music.service.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.service
{
   sealed class ServiceFactory
    {
        private static ServiceFactory instance = new ServiceFactory();
        private IUserService userService = new UserService();
        private ICommentService commentService = new CommentService();
        private IMessageConclusionTimeService messageConclusionTimeService = new MessageConclusionTimeService();
        private IMessageService messageService = new MessageService();
        private IPlayListService playListService = new PlayListService();
        private IPlayListSongService playListSongService = new PlayListSongService();
        private ISongService songService = new SongService();
     
        private IUserMessageService userMessageService = new UserMessageService();
        private IDialogService dialogService = new DialogService();
        private ServiceFactory() { }
        public static ServiceFactory getInstance()
        {
            return instance;
        }
        public IUserService GetUserService()
        {
            return userService;
        }
        public ICommentService GetCommentService()
        {
            return commentService;
        }
        public IMessageConclusionTimeService GetMessageConclusionTimeService()
        {
            return messageConclusionTimeService;
        }
        public IMessageService GetMessageService()
        {
            return messageService;
        }
        public IPlayListService GetPlayListService()
        {
            return playListService;
        }
        public IPlayListSongService GetPlayListSongService()
        {
            return playListSongService;
        }
        public ISongService GetSongService()
        {
            return songService;
        }
      
        public IUserMessageService GetUserMessageService()
        {
            return userMessageService;
        }
        public IDialogService GetDialogService()
        {
            return dialogService;
        }
    }
}
