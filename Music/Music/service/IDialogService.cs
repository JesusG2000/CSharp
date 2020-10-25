using Music.db;
using Music.dto;
using Music.dto.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.service
{
    interface IDialogService
    {
        List<Dialog> createDialogs(List<UserMessage> userMessages, int ownerId);
        Dialog getDialogByUsers(List<Dialog> dialogs, DUser ownUser, DUser otherUser);
    }
}
