namespace Hangfire_Practice.Models
{
    public enum CronScheduleType
    {
        Minutely,   // 每 N 分钟一次
        Hourly,     // 每 N 小时一次
        Daily,      // 每天固定时间
        Weekly,     // 每周固定时间
        Monthly,    // 每月固定日
        Custom      // 用户自定义表达式
    }

    public class CronExpressionRequest
    {
        public bool IsOneTimeJob { get; set; }

        // 类型：Secondly, Minutely, Hourly, Daily, Weekly, Monthly, Custom
        public CronScheduleType ScheduleType { get; set; }

        public int? Interval { get; set; } // 对于 Minutely/Hourly 用于 */N 表达式

        public DailySchedule? Daily { get; set; }
        public WeeklySchedule? Weekly { get; set; }
        public MonthlySchedule? Monthly { get; set; }

        // 对 Custom 类型，可以直接提供完整 Cron 字符串（不建议，但可兼容）
        public string? CustomCron { get; set; }
    }

    public class DailySchedule
    {
        public int Hour { get; set; }  // 0-23
        public int Minute { get; set; } // 0-59
    }

    public class WeeklySchedule
    {
        public List<DayOfWeek> Days { get; set; } = [];
        public int Hour { get; set; }
        public int Minute { get; set; }
    }

    public class MonthlySchedule
    {
        public List<int> Days { get; set; } = []; // 1-31
        public int Hour { get; set; }
        public int Minute { get; set; }
    }
}
