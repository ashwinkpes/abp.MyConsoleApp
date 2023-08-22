using System.Threading.Tasks;
using Acme.MyConsoleApp.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;

namespace Acme.MyConsoleApp;

[DependsOn(
    typeof(AbpAutofacModule),
     typeof(AbpEntityFrameworkCoreSqlServerModule)
)]
public class MyConsoleAppModule : AbpModule
{
    public override Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        var logger = context.ServiceProvider.GetRequiredService<ILogger<MyConsoleAppModule>>();
        var configuration = context.ServiceProvider.GetRequiredService<IConfiguration>();
        logger.LogInformation($"MySettingName => {configuration["MySettingName"]}");

        var hostEnvironment = context.ServiceProvider.GetRequiredService<IHostEnvironment>();
        logger.LogInformation($"EnvironmentName => {hostEnvironment.EnvironmentName}");

        return Task.CompletedTask;
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);
        ConfigureEfCore(context);
    }

    private void ConfigureEfCore(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<TodoAppDbContext>(options =>
        {
            /* You can remove "includeAllEntities: true" to create
             * default repositories only for aggregate roots
             * Documentation: https://docs.abp.io/en/abp/latest/Entity-Framework-Core#add-default-repositories
             */
            options.AddDefaultRepositories(includeAllEntities: true);
        });

        Configure<AbpDbContextOptions>(options =>
        {
            options.Configure(configurationContext =>
            {
                configurationContext.UseSqlServer();
            });
        });

    }
}
