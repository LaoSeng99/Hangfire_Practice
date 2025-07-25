namespace Hangfire_Practice.Hangfires.Config;

public class HangfireOptions
{
    public string TimeZoneId { get; set; } = "UTC";
    public string DashboardTitle { get; set; } = "Hangfire Dashboard";
    public int WorkerCount { get; set; } = 20;
    public string[] Queues { get; set; } = ["default", "critical", "background"];
    public string StorageType { get; set; } = "Memory";
}
