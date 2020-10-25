using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Music.dto;
using Music.dto.dto;
using Music.dao;
using Music.db;

namespace Music.service.impl
{
    class DialogService : IDialogService
    {
        private IUserDao userDao = DaoFactory.getInstance().GetUserDao();
        private IMessageDao messageDao = DaoFactory.getInstance().GetMessageDao();
        public List<Dialog> createDialogs(List<UserMessage> userMessages, int ownerId)
        {
           
            List<Dialog> dialogs = new List<Dialog>();
            foreach (UserMessage um in userMessages)
            {
                int getId = um.UserGetterId;
                int sendId = messageDao.readById(um.MessageId).UserSenderId;
                List<UserMessage> listMessages = new List<UserMessage>();
                foreach (UserMessage um1 in userMessages)
                {
                    int getterUserId = um1.UserGetterId;
                    int ownUserId = messageDao.readById(um1.MessageId).UserSenderId;
                    if ((getId == getterUserId && sendId == ownUserId) || (getId == ownUserId && sendId == getterUserId))
                    {
                        listMessages.Add(um1);
                    }
                }
                Dialog dialog = new Dialog();
                dialog.UserMessage = listMessages;
                DUser ownUser = userDao.readById(ownerId);
                DUser otherUser;
                if (getId != ownerId)
                {
                    otherUser = userDao.readById(getId);
                }
                else
                {
                    otherUser = userDao.readById(sendId);
                }
                dialog.OwnUser = ownUser;
                dialog.OtherUser = otherUser;
                dialogs.Add(dialog);
            }
            dialogs = deleteCopyFromDialog(dialogs);
            return dialogs;

        }

        private List<Dialog> deleteCopyFromDialog(List<Dialog> dialogs)
        {
            List<Dialog> dialogWithOutCopy = new List<Dialog>();
            foreach (Dialog d in dialogs)
            {
                foreach (Dialog d1 in dialogs)
                {
                    if (!d.Equals(d1) && !checkIfHas(dialogWithOutCopy,d))
                    {
                        dialogWithOutCopy.Add(d);
                    }
                }
            }
            if (dialogWithOutCopy.Capacity == 0&&dialogs.Capacity!=0)
            {
                dialogWithOutCopy.Add(dialogs[0]);
            }
            return dialogWithOutCopy;
        }
        private bool checkIfHas(List<Dialog> items , Dialog dialog)
        {
            foreach(Dialog d in items)
            {
                if (d.Equals(dialog))
                {
                    return true;
                }
            }
            return false;
        }

        public Dialog getDialogByUsers(List<Dialog> dialogs, DUser ownUser, DUser otherUser)
        {
            Dialog dialog = new Dialog();
            foreach (Dialog d in dialogs)
            {
                if ((d.OwnUser.Id == ownUser.Id && d.OtherUser.Id == otherUser.Id) ||
                   (d.OwnUser.Id == otherUser.Id && d.OtherUser.Id == ownUser.Id))
                {
                    dialog = d;
                }
            }
            return dialog;
        }
    }
}
