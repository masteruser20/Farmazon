using Farmazon.NotificationService.App.Interfaces;
using Farmazon.NotificationService.Infrastracture.Services.Jobs;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Farmazon.NotificationService.Infrastracture.Services;

public class QuartzHostedService(ISchedulerFactory schedulerFactory,
    MonthlyTransactionJob monthlyJob,
    ILogger<QuartzHostedService> logger) : IQuartzHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var scheduler = await schedulerFactory.GetScheduler(cancellationToken);
            
        var jobDetail = JobBuilder.Create<MonthlyTransactionJob>()
            .WithIdentity("MonthlyTransactionJob")
            .Build();

        var trigger = TriggerBuilder.Create()
            .WithIdentity("MonthlyTransactionrigger")
            .WithCronSchedule("0 0 0 L * ?")
            .Build();

        await scheduler.ScheduleJob(jobDetail, trigger, cancellationToken);
        await scheduler.Start(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}