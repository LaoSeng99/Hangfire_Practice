using Hangfire_Practice.Hangfires.Config;

namespace Hangfire_Practice.Hangfires;

public record WorkRequestJobRequest
{
    public string DataId { get; init; }
    public DateTime ScheduledAt { get; init; }
    public List<WorkRequestJobDto> Works { get; init; } = [];
}

public record WorkRequestJobDto
{
    public string WorkerName { get; init; }
    public string WorkDescription { get; init; }
}

public class WorkRequestJob(
    ILogger<WorkRequestJob> _logger
    ) : IJob<WorkRequestJobRequest>
{
    public Task ExecuteAsync(WorkRequestJobRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("WorkRequestJob execute at {Time}", DateTime.Now);
        _logger.LogInformation("WorkRequestJob started: DataId={DataId}, ScheduledAt={Time}, WorkCount={Count}",
            request.DataId, request.ScheduledAt, request.Works.Count);

        foreach (var work in request.Works)
        {
            _logger.LogInformation("Worker: {Name}, Description: {Desc}",
                work.WorkerName, work.WorkDescription);
        }

        return Task.CompletedTask;
    }
}
