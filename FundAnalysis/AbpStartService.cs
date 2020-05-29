using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace FundAnalysis
{
    public static class AbpExtensions
    {
     
        public static IServiceCollection AddAbpApplication<TStartupModule>(
            this IServiceCollection services,
            Action<AbpApplicationCreationOptions> optionsAction = null
        )
        where TStartupModule: IAbpModule
        {
            var app = services.AddApplication<TStartupModule>(optionsAction);
            services.AddSingleton(app);
            services.AddHostedService<AbpStartService>();
            return services;
        }
    }

    internal class AbpStartService: IHostedService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IAbpApplicationWithExternalServiceProvider _application;

        public AbpStartService(IServiceProvider serviceProvider, IAbpApplicationWithExternalServiceProvider application)
        {
            _serviceProvider = serviceProvider;
            _application = application;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _application.Initialize(_serviceProvider);
            // Console.WriteLine("_application run!");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _application.Shutdown();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _application?.Dispose();
        }
    }
    
}