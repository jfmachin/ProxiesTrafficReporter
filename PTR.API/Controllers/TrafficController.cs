using MediatR;
using Microsoft.AspNetCore.Mvc;
using PTR.API.Features.Proxymesh.Queries.GetTraffic;

namespace PTR.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TrafficController : ControllerBase {
    private readonly IMediator mediator;

    public TrafficController(IMediator mediator) {
        this.mediator = mediator;
    }

    [HttpGet("proxyMesh")]
    public async Task<ActionResult<string>> TrafficPM() {
        var usage = await mediator.Send(new GetProxymeshTrafficQuery());
        return Ok(usage);
    }
}