using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailServiceBus
{
    internal interface IMessageServiceBus
    {
        Task PublishMessage(string QueueName , object Message);
    }
}
