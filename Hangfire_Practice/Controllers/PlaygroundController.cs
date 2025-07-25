using Hangfire_Practice.Hangfires;
using Hangfire_Practice.Hangfires.Config;
using Hangfire_Practice.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hangfire_Practice.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlaygroundController(
     IBackgroundJobDispatcher _dispatcher
    ) : ControllerBase
{
    [HttpPost("hangfire/cron-generator")]
    public IActionResult GenerateCron([FromBody] CronExpressionRequest request)
    {
        if (request.IsOneTimeJob)
            return Ok("");

        string cron = request.ScheduleType switch
        {
            CronScheduleType.Minutely => $"*/{request.Interval ?? 1} * * * *",
            CronScheduleType.Hourly => $"0 */{request.Interval ?? 1} * * *",
            CronScheduleType.Daily when request.Daily is { } d =>
                $"{d.Minute} {d.Hour} * * *",
            CronScheduleType.Weekly when request.Weekly is { } w =>
                $"{w.Minute} {w.Hour} * * {string.Join(",", w.Days.Select(d => d.ToString().Substring(0, 3).ToUpper()))}",
            CronScheduleType.Monthly when request.Monthly is { } m =>
                $"{m.Minute} {m.Hour} {string.Join(",", m.Days)} * *",
            CronScheduleType.Custom when !string.IsNullOrWhiteSpace(request.CustomCron) =>
                request.CustomCron,
            _ => string.Empty
        };

        if (string.IsNullOrWhiteSpace(cron))
            return BadRequest("Invalid schedule parameters");

        return Ok(new { cron });
    }

    [HttpPost("console-log/enqueue")]
    public IActionResult EnqueueConsoleLog()
    {
        _dispatcher.Enqueue<ConsoleLogJob>();
        return Ok("ConsoleLogJob enqueued");
    }

    /// <summary>
    /// Schedules a background job to be executed at the specified UTC time.
    /// </summary>
    /// <param name="request">The request containing job details and the UTC schedule time.</param>
    [HttpPost("console-log/schedule")]
    public IActionResult ScheduleConsoleLog([FromQuery] int delaySeconds)
    {
        _dispatcher.Schedule<ConsoleLogJob>(TimeSpan.FromSeconds(delaySeconds));
        return Ok("ConsoleLogJob scheduled");
    }

    /// <param name="jobId">
    /// Unique Job Id for the recurring job.
    /// - If a different jobId is provided, a new recurring job will be created.
    /// - If the same jobId already exists, the existing job will be replaced (updated with new settings).
    /// </param>
    [HttpPost("console-log/recurring")]
    public IActionResult RegisterRecurringConsoleLog([FromQuery] string cron, [FromQuery] string jobId ="ConsoleJob")
    {
        _dispatcher.AddOrUpdateRecurring<ConsoleLogJob>(jobId, cron);
        return Ok("ConsoleLogJob recurring registered");
    }

    [HttpPost("work-args/enqueue")]
    public IActionResult EnqueueWorkArgs([FromBody] WorkArgsJobArgs args)
    {
        _dispatcher.Enqueue<WorkArgsJob, WorkArgsJobArgs>(args);
        return Ok("WorkArgsJob enqueued");
    }

    /// <summary>
    /// Schedules a background job to be executed at the specified UTC time.
    /// </summary>
    /// <param name="request">The request containing job details and the UTC schedule time.</param>
    [HttpPost("work-args/schedule")]
    public IActionResult ScheduleWorkArgs([FromBody] WorkArgsJobArgs args, [FromQuery] int delaySeconds)
    {
        _dispatcher.Schedule<WorkArgsJob, WorkArgsJobArgs>(args, TimeSpan.FromSeconds(delaySeconds));
        return Ok("WorkArgsJob scheduled");
    }

    /// <param name="jobId">
    /// Unique Job Id for the recurring job.
    /// - If a different jobId is provided, a new recurring job will be created.
    /// - If the same jobId already exists, the existing job will be replaced (updated with new settings).
    /// </param>
    [HttpPost("work-args/recurring")]
    public IActionResult RegisterRecurringWorkArgs([FromBody] WorkArgsJobArgs args, [FromQuery] string cron, [FromQuery] string jobId="WorkArgsJob")
    {
        _dispatcher.AddOrUpdateRecurring<WorkArgsJob, WorkArgsJobArgs>("WorkArgsJob", args, cron);
        return Ok("WorkArgsJob recurring registered");
    }

    [HttpPost("work-request/enqueue")]
    public IActionResult EnqueueWorkRequest([FromBody] WorkRequestJobRequest request)
    {
        _dispatcher.Enqueue<WorkRequestJob, WorkRequestJobRequest>(request);
        return Ok("WorkRequestJob enqueued");
    }

    /// <summary>
    /// Schedules a background job to be executed at the specified UTC time.
    /// </summary>
    /// <param name="request">The request containing job details and the UTC schedule time.</param>
    [HttpPost("work-request/schedule")]
    public IActionResult ScheduleWorkRequest([FromBody] WorkRequestJobRequest request)
    {
        _dispatcher.Schedule<WorkRequestJob, WorkRequestJobRequest>(request, request.ScheduledAt);
        return Ok("WorkRequestJob scheduled");
    }

    /// <param name="jobId">
    /// Unique Job Id for the recurring job.
    /// - If a different jobId is provided, a new recurring job will be created.
    /// - If the same jobId already exists, the existing job will be replaced (updated with new settings).
    /// </param>
    [HttpPost("work-request/recurring")]
    public IActionResult RegisterRecurringWorkRequest([FromBody] WorkRequestJobRequest request, [FromQuery] string cron, [FromQuery] string jobId="WorkRequestJob")
    {
        _dispatcher.AddOrUpdateRecurring<WorkRequestJob, WorkRequestJobRequest>(jobId, request, cron);
        return Ok("WorkRequestJob recurring registered");
    }

    [HttpDelete("recurring/{jobId}")]
    public IActionResult RemoveRecurringJob(string jobId)
    {
        _dispatcher.RemoveRecurring(jobId);  
        return Ok($"Recurring job {jobId} removed");
    }


}
