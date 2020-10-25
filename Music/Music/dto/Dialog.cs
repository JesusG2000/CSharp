using Music.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.dto.dto
{
    public class Dialog
    {
        public List<UserMessage> UserMessage { get; set; }
        public DUser OwnUser { get; set; }
        public DUser OtherUser { get; set; }

        public Dialog(List<UserMessage> userMessage, DUser ownUser, DUser otherUser)
        {
            UserMessage = userMessage;
            OwnUser = ownUser;
            OtherUser = otherUser;
        }

        public Dialog()
        {
        }

        public override bool Equals(object obj)
        {
            var dialog = obj as Dialog;
            return dialog != null &&
                   UserMessage.SequenceEqual(dialog.UserMessage) &&
                   EqualityComparer<DUser>.Default.Equals(OwnUser, dialog.OwnUser) &&
                   EqualityComparer<DUser>.Default.Equals(OtherUser, dialog.OtherUser);
        }

        public override int GetHashCode()
        {
            var hashCode = -1316946345;
            hashCode = hashCode * -1521134295 + EqualityComparer<List<UserMessage>>.Default.GetHashCode(UserMessage);
            hashCode = hashCode * -1521134295 + EqualityComparer<DUser>.Default.GetHashCode(OwnUser);
            hashCode = hashCode * -1521134295 + EqualityComparer<DUser>.Default.GetHashCode(OtherUser);
            return hashCode;
        }

        public override string ToString()
        {
            return "Dialog{" +
                  "UserMessage=" + UserMessage +
                  ", OwnUser=" + OwnUser +
                  ", OtherUser=" + OtherUser +
                  '}';
        }
    }
}
