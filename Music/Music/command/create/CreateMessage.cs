using Music.dto;
using Music.controller;
using Music.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Music.db;

namespace Music.command
{
    class CreateMessage : ICommand
    {
        private IMessageService userMessageService = ServiceFactory.getInstance().GetMessageService();
        public object Execute(object request)
        {
            return userMessageService.createAndReturn((Message)request);
           
        }
    }
}
