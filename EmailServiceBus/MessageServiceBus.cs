using System.Text;
using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;

namespace EmailServiceBus
{
    public class MessageServiceBus : IMessageServiceBus

    {
        private string ServiceBusConnectionString { get; set; } =
            "Endpoint=sb://mangowebbt.servicebus.windows.net/;SharedAccessKeyName=EmailServiceBus;SharedAccessKey=3uCh5iEkHGfr9PWtstbzuh4TJ48ZhNU6k+ASbDudKZ0=;EntityPath=mangoemailsurvicebus";

        public async Task PublishMessage(string QueueName, object Message)
        {
            //serviceBus client to to create the sender witch would send the message
            var client = new ServiceBusClient(ServiceBusConnectionString);
            //creating the service-bus sender to send the message
            var sender = client.CreateSender(QueueName);
            //serializing the message to convert it to jason to send it 
            var jsonmessage = JsonConvert.SerializeObject(Message);
            //to send the message the message has to be encoded so we use the encoder and encoding the message to the utf8 encoding and addding a guid to be its id 
            var serviceBusMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(jsonmessage))
            {
                CorrelationId = Guid.NewGuid().ToString(),
            };

            await sender.SendMessageAsync(serviceBusMessage);

            await client.DisposeAsync();
        }
    }
}
