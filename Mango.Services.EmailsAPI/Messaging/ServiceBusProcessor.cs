using System.Text;
using Azure.Messaging.ServiceBus;
using Mango.Services.EmailsAPI.Data;
using Mango.Services.EmailsAPI.Model;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Mango.Services.EmailsAPI.Messaging
{
    public class ServiceBusProcessor : IServiceBusProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public string ConnectionString { get; set; } =
            "Endpoint=sb://mangowebbt.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=YcC2C+YeRZzGLS2SDnqBYyhD/HeykIHLg+ASbEaL6iI="; 
        
        public string QueueName { get; set; } = "mangoemailsurvicebus";
        
        public Azure.Messaging.ServiceBus.ServiceBusProcessor? _Processor { get; set; }  

        public ServiceBusProcessor( IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
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
            await _Processor.StartProcessingAsync();
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
                var scope = _scopeFactory.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<EmailDBContext>();
                var mail = new EmialDto()
                {
                    Email = DeserializedMessage.CartHeader.Email,
                    ContentMessage = DecodedMessage,
                    SentTiming = DateTime.Now
                };

                await context.Emails.AddAsync(mail);
                await context.SaveChangesAsync();
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
