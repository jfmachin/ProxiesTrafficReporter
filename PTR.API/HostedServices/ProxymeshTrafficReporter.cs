using MediatR;
using PTR.API.Features.Proxymesh.Queries.GetTraffic;

namespace PTR.API.HostedServices;
public class ProxymeshTrafficReporter : BackgroundService {
    private readonly ILogger<ProxymeshTrafficReporter> logger;
    private readonly IMediator mediator;
    private readonly TimeSpan _period = TimeSpan.FromSeconds(1);

    public ProxymeshTrafficReporter(ILogger<ProxymeshTrafficReporter> logger, IMediator mediator) {
        this.logger = logger;
        this.mediator = mediator;
    }
    protected async override Task ExecuteAsync(CancellationToken stoppingToken) {
        logger.LogInformation($"BackgroundService: {nameof(ProxymeshTrafficReporter)} has been started");
        while (!stoppingToken.IsCancellationRequested) {
            await mediator.Send(new GetProxymeshTrafficQuery(), stoppingToken);
            await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
        }
    }
}