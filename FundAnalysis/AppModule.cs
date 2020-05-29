using FundAnalysis.DTO;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace FundAnalysis
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpAutoMapperModule)
    )]
    public class AppModule: AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var service = context.Services;
            service.AddAutoMapperObjectMapper<AppModule>();
           
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<MyProfile>(true);
            });
        }
    }
}