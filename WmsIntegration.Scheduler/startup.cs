using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Hangfire;
using Hangfire.MemoryStorage;
using WmsIntegration.Scheduler;


var builder = WebApplication.CreateBuilder(args);

#region AppSettings setup
// Setup configuration to read from appsettings.json
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Initialize AppSettings with configuration values
AppSettings.Initialize(builder.Configuration);

// builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

#endregion

builder.Services.AddTransient<XmlProcessor>();
builder.Services.AddTransient<SchedulerService>();
builder.Services.AddHttpClient(); // For HttpClient injection

#region HangFire

builder.Services.AddHangfire(config =>
    config.UseMemoryStorage());

builder.Services.AddHangfireServer(); // Background job server
builder.Services.AddSingleton<XmlProcessor>();            // Register XmlProcessor

#endregion

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

System.Console.WriteLine("The scheduler service has started");
var app = builder.Build();

app.MapGet("/", () => "Hangfire with In-Memory Storage");
app.UseHangfireDashboard();
RecurringJob.AddOrUpdate<SchedulerService>(
    "xml-file-processor",
    service => service.RunAsync(),
    Cron.Minutely); // Every 1 minute, or change to your needs
//System.Console.WriteLine($"AppSettings.ERPURL: {AppSettings.GetSetting("ERPURL")}");
app.Run();
