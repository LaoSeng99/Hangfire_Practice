# Hangfire_Practice
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
```

## Getting Started

1. Clone the repository
2. Install the required NuGet packages
3. Configure your database connection (if using SQL Server storage)
4. Run the application
5. Access Hangfire Dashboard at `/hangfire` (with proper authentication) - Not applied at this project

## Important Notes

- Every new job class must be registered in `Extensions/DependencyInjection`
- Add `services.AddTransient<YourJobClass>();` for each new job
- Config folder contains the main scheduling and setup logic
- All job examples demonstrate different parameter patterns

## Purpose

This repository serves as a practice ground and code reference for Hangfire implementation patterns in .NET applications.
