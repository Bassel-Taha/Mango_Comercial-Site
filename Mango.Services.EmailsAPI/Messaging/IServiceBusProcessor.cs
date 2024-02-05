namespace Mango.Services.EmailsAPI.Messaging
{
    public interface IServiceBusProcessor
    {
        Task Start();

        Task Stop();
    }
}
