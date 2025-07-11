namespace Hangfire_Practice.Hangfires.Config;

public interface IJob
{
    Task ExecuteAsync(CancellationToken cancellationToken = default);
}

public interface IJob<in T>
{
    Task ExecuteAsync(T args, CancellationToken cancellationToken = default);
}