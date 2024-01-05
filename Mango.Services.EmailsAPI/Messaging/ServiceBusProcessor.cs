using Azure.Messaging.ServiceBus;

namespace Mango.Services.EmailsAPI.Messaging
{
    public class ServiceBusProcessor
    {
        public string ConnectionString { get; set; } =
            "Endpoint=sb://mangowebbt.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=YcC2C+YeRZzGLS2SDnqBYyhD/HeykIHLg+ASbEaL6iI="; 
        
        public string QueueName { get; set; } = "mangoemailsurvicebus";
        
        public Azure.Messaging.ServiceBus.ServiceBusProcessor? _Processor { get; set; }  

        public ServiceBusProcessor()
        {
            var client = new ServiceBusClient(ConnectionString);
             _Processor = client.CreateProcessor(QueueName);
             _Processor.
        }
    }
}
