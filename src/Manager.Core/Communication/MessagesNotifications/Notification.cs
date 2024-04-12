using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Core.Communication.MessagesNotifications
{
    public abstract class Notification : INotification
    {
        public string Hash { get; private set; }

        public Notification()
        {
            Hash = Guid.NewGuid().ToString();
        }
    }
}
