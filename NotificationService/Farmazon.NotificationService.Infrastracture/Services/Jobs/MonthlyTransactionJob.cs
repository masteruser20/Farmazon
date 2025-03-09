using Farmazon.Shared;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Farmazon.NotificationService.Infrastracture.Services.Jobs;

public class MonthlyTransactionJob(
    IOrderProvider orderService,
    IUserProvider userService,
    IServiceProvider emailService,
    IServiceProvider pdfService,
    IServiceProvider reportService,
    ILogger logger) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            logger.LogInformation("Starting monthly transaction email job");
            var startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-1);
            var endDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(-1);

            var customersWithTransactions = await orderService.GetCustomersWithTransactionsAsync(startDate, endDate);

            foreach (var customerTransaction in customersWithTransactions)
            {
                try
                {
                    var customer = await userService.GetCustomerAsync(customerTransaction);

                    if (await reportService.HasReportBeenSentAsync(customer, startDate, endDate))
                    {
                        logger.LogInformation("Report already sent to customer {CustomerId}",
                            customerTransaction);
                        continue;
                    }

                    // Generate PDF report
                    var pdf = await pdfService.GenerateTransactionReportAsync(
                        customer,
                        customer.transactions,
                        startDate,
                        endDate);

                    var emailModel = new object();

                    // Send email
                    // with retry policy
                    await emailService.SendTransactionReportEmailAsync(emailModel);

                    // some metrics

                    logger.LogInformation("Success");
                }
                catch (Exception ex)
                {
                    // mongodb to save failed jobs?
                    await reportRepository.AddSendReportFailed(customerTransaction);

                    logger.LogError(ex, "Error ");
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error");
            throw;
        }
    }
}