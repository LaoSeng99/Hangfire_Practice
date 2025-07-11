using Hangfire_Practice.Hangfires.Config;

namespace Hangfire_Practice.Hangfires;

public record WorkArgsJobArgs(string UserId, int WorkItemCount);

public class WorkArgsJob(
    ILogger<WorkArgsJob> _logger
    ) :IJob<WorkArgsJobArgs>
{
    public Task ExecuteAsync(WorkArgsJobArgs args, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("WorkJob execute at {Time}", DateTime.Now);
        _logger.LogInformation("WorkJob started for user: {UserId}, count: {Count}", args.UserId, args.WorkItemCount);
        return Task.CompletedTask;
    }
}