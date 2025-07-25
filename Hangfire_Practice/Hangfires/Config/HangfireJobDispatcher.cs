using Hangfire;
using Microsoft.Extensions.Options;
using TimeZoneConverter;

namespace Hangfire_Practice.Hangfires.Config;

public class HangfireJobDispatcher : IBackgroundJobDispatcher
{
    private readonly ILogger<HangfireJobDispatcher> _logger;
    private RecurringJobOptions _recurringJobOption;
    public HangfireJobDispatcher(ILogger<HangfireJobDispatcher> logger, IOptions<HangfireOptions> options)
    {
        _logger = logger;
        try
        {
            var timeZone = TZConvert.GetTimeZoneInfo(options.Value.TimeZoneId);
            _logger.LogInformation("Using timezone: {TimeZoneId} ({DisplayName})",
           options.Value.TimeZoneId, timeZone.DisplayName);
            _recurringJobOption = new RecurringJobOptions
            {
                TimeZone = timeZone
            };
        }
        catch (TimeZoneNotFoundException)
        {

            _logger.LogWarning("Timezone {TimeZoneId} not found, falling back to UTC", options.Value.TimeZoneId);
            _recurringJobOption = new RecurringJobOptions
            {
                TimeZone = TimeZoneInfo.Utc
            };
        }
    }

    public void Enqueue<TJob>() where TJob : IJob
    {
        _logger.LogInformation("Enqueue: {JobType}", typeof(TJob).FullName);
        BackgroundJob.Enqueue<TJob>(job => job.ExecuteAsync(default));
    }

    public void Enqueue<TJob, TArgs>(TArgs args) where TJob : IJob<TArgs>
    {
        _logger.LogInformation("Enqueue: {JobType} with args: {@Args}", typeof(TJob).FullName, args);
        BackgroundJob.Enqueue<TJob>(job => job.ExecuteAsync(args, default));
    }

    public void Schedule<TJob>(TimeSpan delay) where TJob : IJob
    {
        _logger.LogInformation("Schedule: {JobType} after delay: {Delay}", typeof(TJob).FullName, delay);
        BackgroundJob.Schedule<TJob>(job => job.ExecuteAsync(default), delay);
    }

    public void Schedule<TJob, TArgs>(TArgs args, TimeSpan delay) where TJob : IJob<TArgs>
    {
        _logger.LogInformation("Schedule: {JobType} with args: {@Args} after delay: {Delay}", typeof(TJob).FullName, args, delay);
        BackgroundJob.Schedule<TJob>(job => job.ExecuteAsync(args, default), delay);
    }

    public void Schedule<TJob>(DateTimeOffset enqueueAt) where TJob : IJob
    {
        _logger.LogInformation("Schedule: {JobType} at: {Time}", typeof(TJob).FullName, enqueueAt);
        BackgroundJob.Schedule<TJob>(job => job.ExecuteAsync(default), enqueueAt);
    }

    public void Schedule<TJob, TArgs>(TArgs args, DateTimeOffset enqueueAt) where TJob : IJob<TArgs>
    {
        _logger.LogInformation("Schedule: {JobType} with args: {@Args} at: {Time}", typeof(TJob).FullName, args, enqueueAt);
        BackgroundJob.Schedule<TJob>(job => job.ExecuteAsync(args, default), enqueueAt);
    }

    public void AddOrUpdateRecurring<TJob>(string jobId, string cron) where TJob : IJob
    {
        _logger.LogInformation("AddOrUpdateRecurring: {JobId} for {JobType} on schedule: {Cron}", jobId, typeof(TJob).FullName, cron);
        RecurringJob.AddOrUpdate<TJob>(jobId, job => job.ExecuteAsync(default), cron, _recurringJobOption);
    }

    public void AddOrUpdateRecurring<TJob, TArgs>(string jobId, TArgs args, string cron) where TJob : IJob<TArgs>
    {
        _logger.LogInformation("AddOrUpdateRecurring: {JobId} for {JobType} with args: {@Args} on schedule: {Cron}", jobId, typeof(TJob).FullName, args, cron);

        RecurringJob.AddOrUpdate<TJob>(jobId, job => job.ExecuteAsync(args, default), cron, _recurringJobOption);
    }

    public void RemoveRecurring(string jobId)
    {
        _logger.LogInformation("RemoveRecurring: {JobId}", jobId);
        RecurringJob.RemoveIfExists(jobId);
    }
}
