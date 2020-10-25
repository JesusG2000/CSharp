using Music.controller;
using Music.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.command.other
{
    class SendNotificationForUsers : ICommand
    {
        private IUserService userService = ServiceFactory.getInstance().GetUserService();
        private IMessageService messageService = ServiceFactory.getInstance().GetMessageService();
        private IUserMessageService useMessageService = ServiceFactory.getInstance().GetUserMessageService();
        private IMessageConclusionTimeService messageConclusionTimeService = ServiceFactory.getInstance().GetMessageConclusionTimeService();
        public object Execute(object request)
        {
            object[] arrData = (object[])request;
            int userId = (int)arrData[0];
            Song song = (Song)arrData[1];

            List<DUser> users = userService.getAllRegisterUser();
            Message message = new Message(string.Format("Song with name {0} , type {1} was added", song.Name, song.Type),userId ,DateTime.Now);
            foreach(DUser u in users)
            {
                UserMessage userMessage = new UserMessage();
                MessageConclusionTime messageConclusionTime = messageConclusionTimeService.findByUsersIds(userId, u.Id);
                if (messageConclusionTime == null)
                {
                    messageConclusionTime = new MessageConclusionTime(userId, u.Id, DateTime.Now, DateTime.Now);
                    messageConclusionTimeService.create(messageConclusionTime);
                }
                message = messageService.createAndReturn(message);
                userMessage.MessageId = message.Id;
                userMessage.UserGetterId = u.Id;
                useMessageService.create(userMessage);
               
            }
            return null;

        }
    }
}
