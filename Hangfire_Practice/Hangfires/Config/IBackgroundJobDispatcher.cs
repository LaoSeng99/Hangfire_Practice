namespace Hangfire_Practice.Hangfires.Config;

public interface IBackgroundJobDispatcher
{
    void Enqueue<TJob>() where TJob : IJob;
    void Enqueue<TJob, TArgs>(TArgs args) where TJob : IJob<TArgs>;

    void Schedule<TJob>(TimeSpan delay) where TJob : IJob;
    void Schedule<TJob, TArgs>(TArgs args, TimeSpan delay) where TJob : IJob<TArgs>;

    void Schedule<TJob>(DateTimeOffset enqueueAt) where TJob : IJob;
    void Schedule<TJob, TArgs>(TArgs args, DateTimeOffset enqueueAt) where TJob : IJob<TArgs>;

    void AddOrUpdateRecurring<TJob>(string jobId, string cron) where TJob : IJob;
    void AddOrUpdateRecurring<TJob, TArgs>(string jobId, TArgs args, string cron) where TJob : IJob<TArgs>;

    void RemoveRecurring(string jobId);
}