using Hangfire;

namespace Hangfire_Practice.Hangfires.Config;

public class HangfireJobDispatcher(ILogger<HangfireJobDispatcher> logger) : IBackgroundJobDispatcher
{
    public void Enqueue<TJob>() where TJob : IJob
    {
        logger.LogInformation("Enqueue: {JobType}", typeof(TJob).FullName);
        BackgroundJob.Enqueue<TJob>(job => job.ExecuteAsync(default));
    }

    public void Enqueue<TJob, TArgs>(TArgs args) where TJob : IJob<TArgs>
    {
        logger.LogInformation("Enqueue: {JobType} with args: {@Args}", typeof(TJob).FullName, args);
        BackgroundJob.Enqueue<TJob>(job => job.ExecuteAsync(args, default));
    }

    public void Schedule<TJob>(TimeSpan delay) where TJob : IJob
    {
        logger.LogInformation("Schedule: {JobType} after delay: {Delay}", typeof(TJob).FullName, delay);
        BackgroundJob.Schedule<TJob>(job => job.ExecuteAsync(default), delay);
    }

    public void Schedule<TJob, TArgs>(TArgs args, TimeSpan delay) where TJob : IJob<TArgs>
    {
        logger.LogInformation("Schedule: {JobType} with args: {@Args} after delay: {Delay}", typeof(TJob).FullName, args, delay);
        BackgroundJob.Schedule<TJob>(job => job.ExecuteAsync(args, default), delay);
    }

    public void Schedule<TJob>(DateTimeOffset enqueueAt) where TJob : IJob
    {
        logger.LogInformation("Schedule: {JobType} at: {Time}", typeof(TJob).FullName, enqueueAt);
        BackgroundJob.Schedule<TJob>(job => job.ExecuteAsync(default), enqueueAt);
    }

    public void Schedule<TJob, TArgs>(TArgs args, DateTimeOffset enqueueAt) where TJob : IJob<TArgs>
    {
        logger.LogInformation("Schedule: {JobType} with args: {@Args} at: {Time}", typeof(TJob).FullName, args, enqueueAt);
        BackgroundJob.Schedule<TJob>(job => job.ExecuteAsync(args, default), enqueueAt);
    }

    public void AddOrUpdateRecurring<TJob>(string jobId, string cron) where TJob : IJob
    {
        logger.LogInformation("AddOrUpdateRecurring: {JobId} for {JobType} on schedule: {Cron}", jobId, typeof(TJob).FullName, cron);
        RecurringJob.AddOrUpdate<TJob>(jobId, job => job.ExecuteAsync(default), cron);
    }

    public void AddOrUpdateRecurring<TJob, TArgs>(string jobId, TArgs args, string cron) where TJob : IJob<TArgs>
    {
        logger.LogInformation("AddOrUpdateRecurring: {JobId} for {JobType} with args: {@Args} on schedule: {Cron}", jobId, typeof(TJob).FullName, args, cron);
        RecurringJob.AddOrUpdate<TJob>(jobId, job => job.ExecuteAsync(args, default), cron);
    }

    public void RemoveRecurring(string jobId)
    {
        logger.LogInformation("RemoveRecurring: {JobId}", jobId);
        RecurringJob.RemoveIfExists(jobId);
    }
}
