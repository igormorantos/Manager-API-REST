using Manager.Core.Communication.MessagesNotifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Core.Communication.Mediator.Interfaces
{
    public interface IMediatorHandler
    {
        Task PublishDomainNotificationAsync<T>(T appNotification)
            where T : DomainNotification;
    }
}
