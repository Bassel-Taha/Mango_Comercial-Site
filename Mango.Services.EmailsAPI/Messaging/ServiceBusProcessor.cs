using System.Text;
using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Newtonsoft.Json;

namespace Mango.Services.EmailsAPI.Messaging
{
    public class ServiceBusProcessor : IServiceBusProcessor
    {
        public string ConnectionString { get; set; } =
            "Endpoint=sb://mangowebbt.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=YcC2C+YeRZzGLS2SDnqBYyhD/HeykIHLg+ASbEaL6iI="; 
        
        public string QueueName { get; set; } = "mangoemailsurvicebus";
        
        public Azure.Messaging.ServiceBus.ServiceBusProcessor? _Processor { get; set; }  

        public ServiceBusProcessor()
        {
            var client = new ServiceBusClient(ConnectionString);
             _Processor = client.CreateProcessor(QueueName);
        }

        // the stop method for the processor to stop receiving messagers from the servicebus
        public async Task Stop()
        {
            await _Processor.StartProcessingAsync(); 
            await _Processor.DisposeAsync();
        }

        //the method to start receiving messages from the service bus 
        public async Task Start()
        {
            _Processor.ProcessMessageAsync += OnCartReqeuestRecived;
            _Processor.ProcessErrorAsync += ErrorHandler;
            //must start the processing or the processor wont work 
            _Processor.StartProcessingAsync();
        }

        
        #region the messages delegate events to receive the messages from the bus

        private async Task OnCartReqeuestRecived(ProcessMessageEventArgs arg)
        {
            var Message =  arg.Message;
            var DecodedMessage = Encoding.UTF8.GetString(Message.Body);
            var DeserializedMessage = JsonConvert.DeserializeObject<CartDto>(DecodedMessage);
            try
            {
                //ToDo try to log the email
                await arg.CompleteMessageAsync(arg.Message);
            }
            catch (Exception e)
            {
                throw;

            }
        }

        private Task ErrorHandler(ProcessErrorEventArgs arg)
        {
            Console.WriteLine(arg.Exception);
            return Task.CompletedTask;
        }

        #endregion
    }
}
