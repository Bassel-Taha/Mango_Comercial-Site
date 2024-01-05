using Mango.Services.EmailsAPI.Messaging;

namespace Mango.Services.EmailsAPI.extention_classes
{
    public static class ServiceBusExtentionMethod 
    {
        private static IServiceBusProcessor _serviceBusProcessor { get; set; }

        public static IApplicationBuilder  ServiceBusExtentionApplicationBuilder (this IApplicationBuilder app)
        {
            _serviceBusProcessor =  app.ApplicationServices.GetService<IServiceBusProcessor>();
            var host = app.ApplicationServices.GetService<IHostApplicationLifetime>();
            host.ApplicationStarted.Register(OnStart);
            host.ApplicationStopped.Register(OnStop);
            return app;
        }

        private static void OnStop()
        {
            _serviceBusProcessor.Stop();

        }

        private static void OnStart()
        {
            _serviceBusProcessor.Start();
        }
    }
}
