using Music.controller;
using Music.db;
using Music.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.command.update
{
    class UpdateMessageConclusionTime : ICommand
    {
        private IMessageConclusionTimeService messageConclusionTimeService = ServiceFactory.getInstance().GetMessageConclusionTimeService();
        public object Execute(object request)
        {
            object[] arrData = (object[])request;
            int id = (int)arrData[0];
            MessageConclusionTime messageConclusionTime = (MessageConclusionTime)arrData[1];
            return messageConclusionTimeService.update(id, messageConclusionTime);
        }
    }
}
