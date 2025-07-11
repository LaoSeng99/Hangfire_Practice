using Hangfire_Practice.Hangfires.Config;

namespace Hangfire_Practice.Hangfires;

public class ConsoleLogJob(ILogger<ConsoleLogJob> _logger) : IJob
{
    public Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("ConsoleLogJob executed at "+ DateTime.Now);
        return Task.CompletedTask;
    }
}
