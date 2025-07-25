⭐ If you find this sample code helpful in any way, feel free to give it a star. Your support means a lot!

﻿# Hangfire_Practice
A simple repository for learning and practicing Hangfire background job processing in .NET applications.

## Overview

This project demonstrates how to integrate and use Hangfire for background job processing, including different types of job scheduling and management.

## Project Structure

```

📁 Extensions/
   └── Dependency injection configuration
📁 Filters/
   └── JWT authentication filters for Hangfire Dashboard protection
📁 Hangfire/
   📁 Config/
      └── Main scheduling and Hangfire setup configurations
   ├── ConsoleLogJob.cs - Simple job without parameters
   ├── WorkArgsJob.cs - Job with parameters
   └── WorkRequestJob.cs - Job using DTO models
```

## Features

### Job Types Examples

- **ConsoleLogJob**: Simple background job without parameters
- **WorkArgsJob**: Background job with parameters
- **WorkRequestJob**: Complex job using DTO models

### Security Features

- JWT authentication filter protects Hangfire Dashboard
- Prevents unauthorized access to job management interface

## Required Dependencies

```bash
dotnet add package Hangfire.Core
dotnet add package Hangfire.SqlServer
dotnet add package Hangfire.AspNetCore
dotnet add package Hangfire.MemoryStorage
dotnet add package TimeZoneConverter
```

## Getting Started

1. Clone the repository
2. Install the required NuGet packages
3. Configure your database connection (if using SQL Server storage)
4. Run the application
5. Access Hangfire Dashboard at `/hangfire` (with proper authentication) - Not applied at this project
6. Configure your Hangfire timezone in appsettings.json. Only timezones listed in the table below are supported.

## Important Notes

- Every new job class must be registered in `Extensions/DependencyInjection`
- Add `services.AddTransient<YourJobClass>();` for each new job
- Config folder contains the main scheduling and setup logic
- All job examples demonstrate different parameter patterns

## Available Timezones

### Asia Pacific Region
| Region                  | IANA TimeZone ID    | UTC Offset   | Description              | TZConvert Result ID (Windows)-Will show at hangfire dashboard |
| ----------------------- | ------------------- | ------------ | ------------------------ | ----------------------------- |
| 🇲🇾 Malaysia           | `Asia/Kuala_Lumpur` | UTC+8        | Malaysia Standard Time   | **Singapore Standard Time**   |
| 🇸🇬 Singapore          | `Asia/Singapore`    | UTC+8        | Singapore Standard Time  | **Singapore Standard Time**   |
| 🇨🇳 China              | `Asia/Shanghai`     | UTC+8        | China Standard Time      | **China Standard Time**       |
| 🇭🇰 Hong Kong          | `Asia/Hong_Kong`    | UTC+8        | Hong Kong Time           | **China Standard Time**       |
| 🇹🇼 Taiwan             | `Asia/Taipei`       | UTC+8        | Taiwan Standard Time     | **Taipei Standard Time**      |
| 🇯🇵 Japan              | `Asia/Tokyo`        | UTC+9        | Japan Standard Time      | **Tokyo Standard Time**       |
| 🇰🇷 South Korea        | `Asia/Seoul`        | UTC+9        | Korea Standard Time      | **Korea Standard Time**       |
| 🇹🇭 Thailand           | `Asia/Bangkok`      | UTC+7        | Indochina Time           | **SE Asia Standard Time**     |
| 🇻🇳 Vietnam            | `Asia/Ho_Chi_Minh`  | UTC+7        | Indochina Time           | **SE Asia Standard Time**     |
| 🇮🇩 Indonesia          | `Asia/Jakarta`      | UTC+7        | Western Indonesian Time  | **SE Asia Standard Time**     |
| 🇵🇭 Philippines        | `Asia/Manila`       | UTC+8        | Philippine Standard Time | **Taipei Standard Time**      |
| 🇮🇳 India              | `Asia/Kolkata`      | UTC+5:30     | India Standard Time      | **India Standard Time**       |
| 🇦🇺 Australia (Sydney) | `Australia/Sydney`  | UTC+10/+11\* | Australian Eastern Time  | **AUS Eastern Standard Time** |


### Europe & Americas
| Region              | IANA TimeZone ID      | UTC Offset | Description                | TZConvert Result ID (Windows) |
| ------------------- | --------------------- | ---------- | -------------------------- | ----------------------------- |
| 🌍 UTC              | `UTC`                 | UTC+0      | Coordinated Universal Time | **UTC**                       |
| 🇬🇧 United Kingdom | `Europe/London`       | UTC+0/+1\* | Greenwich Mean Time        | **GMT Standard Time**         |
| 🇩🇪 Germany        | `Europe/Berlin`       | UTC+1/+2\* | Central European Time      | **W. Europe Standard Time**   |
| 🇫🇷 France         | `Europe/Paris`        | UTC+1/+2\* | Central European Time      | **Romance Standard Time**     |
| 🇺🇸 US Eastern     | `America/New_York`    | UTC-5/-4\* | Eastern Standard Time      | **Eastern Standard Time**     |
| 🇺🇸 US Pacific     | `America/Los_Angeles` | UTC-8/-7\* | Pacific Standard Time      | **Pacific Standard Time**     |


> **Note:** * indicates regions that observe Daylight Saving Time (DST)
## Complete Timezone Reference

For a comprehensive list of all available IANA timezone identifiers, refer to:
- [IANA Time Zone Database](https://www.iana.org/time-zones)
- [Wikipedia: List of tz database time zones](https://en.wikipedia.org/wiki/List_of_tz_database_time_zones)
- [TimeZoneConverter Documentation](https://github.com/mattjohnsonpint/TimeZoneConverter)

## Validation

To validate if a timezone is supported, you can test it programmatically:

```csharp
using TimeZoneConverter;

try 
{
    var timeZone = TZConvert.GetTimeZoneInfo("Asia/Kuala_Lumpur");
    Console.WriteLine($"✅ Timezone: {timeZone.DisplayName}");
}
catch (TimeZoneNotFoundException)
{
    Console.WriteLine("❌ Timezone not supported");
}
```

## Important Notes for time zone

1. **Cross-Platform Compatible**: Uses IANA timezone identifiers that work on both Windows and Linux
2. **DST Handling**: Automatically handles Daylight Saving Time transitions
3. **Recurring Jobs**: All cron expressions are evaluated in the configured timezone
4. **Logging**: Job scheduling logs show both local time and UTC time for clarity

## Purpose

This repository serves as a practice ground and code reference for Hangfire implementation patterns in .NET applications.
