﻿using Music.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.service
{
    interface IMessageConclusionTimeService:Service<MessageConclusionTime>
    {
        List<MessageConclusionTime> getAllByUserId(int userId);
        MessageConclusionTime findByUsersIds(int firstUserId, int secondUserId);
    }
}
