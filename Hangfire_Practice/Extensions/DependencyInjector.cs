using Hangfire;
using Hangfire.MemoryStorage;
using Hangfire.SqlServer;
using Hangfire_Practice.Hangfires;
using Hangfire_Practice.Hangfires.Config;

namespace Hangfire_Practice.Extensions;

public static class DependencyInjector
{
    public static void ConfigureAppService(this IServiceCollection services)
    {
        //Register Background Service/Dispatcher
        services.AddScoped<IBackgroundJobDispatcher, HangfireJobDispatcher>();

        //Register Job service
        services.AddTransient<ConsoleLogJob>();
        services.AddTransient<WorkArgsJob>();
        services.AddTransient<WorkRequestJob>();
    }


    public static void ConfigureHangfire(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("HangfireConnection");
        var storageType = configuration["Hangfire:StorageType"];
        var workerCount = configuration.GetValue<int>("Hangfire:WorkerCount");

        switch (storageType?.ToLower())
        {
            case "memory":
                services.AddHangfire(config => config
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseMemoryStorage());
                break;
            case "sqlserver":
            default:
                services.AddHangfire(config => config
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage(connectionString, new SqlServerStorageOptions
                    {
                        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                        QueuePollInterval = TimeSpan.Zero,
                        UseRecommendedIsolationLevel = true,
                        DisableGlobalLocks = true
                    }));
                break;
        }

        // 添加 Hangfire 服务器
        services.AddHangfireServer(options =>
        {
            options.WorkerCount = workerCount;
            options.Queues = configuration.GetSection("Hangfire:Queues").Get<string[]>() ?? new[] { "default" };
        });
    }
}
