using Manager.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Core.Communication.MessagesNotifications
{
    public class DomainNotification : Notification
    {
        public string Message { get; private set; }
        public DomainNotificationType Type { get; private set; }

        public DomainNotification(string message, DomainNotificationType type)
        {
            Message = message;
            Type = type;
        }
    }
}
