using Music.controller;
using Music.dto.dto;
using Music.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.command.create
{
    class CreateUserDialog : ICommand
    {
        private IDialogService dialogService = ServiceFactory.getInstance().GetDialogService();
        private IUserMessageService userMessageService = ServiceFactory.getInstance().GetUserMessageService();

        public object Execute(object request)
        {
            DUser[] arrData = (DUser[])request;
            DUser user = arrData[0];
            DUser otherUser = arrData[1];
            List<UserMessage> userMessages = userMessageService.getAllMessageByUserId(user.Id);
            List<Dialog> dialogs= dialogService.createDialogs(userMessages, user.Id);
            return dialogService.getDialogByUsers(dialogs,user,otherUser);
        }
    }
}
