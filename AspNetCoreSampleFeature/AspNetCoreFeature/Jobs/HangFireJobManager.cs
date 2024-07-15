using Hangfire;

namespace AspNetCoreFeature.Jobs;

public class HangFireJobManager
{
    private readonly IBackgroundJobClient _backgroundJobClient;
    private readonly IRecurringJobManager _recurringJobManager;

    public HangFireJobManager(IBackgroundJobClient backgroundJobClient, IRecurringJobManager recurringJobManager)
    {
        _backgroundJobClient = backgroundJobClient;
        _recurringJobManager = recurringJobManager;
    }

    public void RegisterJobs()
    {
        _recurringJobManager.AddOrUpdate("test", () => Console.WriteLine("recurring test"), Cron.Minutely());
        _backgroundJobClient.Enqueue(() => Console.WriteLine("enqueue-test"));
        _recurringJobManager.Trigger("test");
    }
}