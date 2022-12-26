using MediatR;
using Microsoft.AspNetCore.Mvc;
using PMR.API.Features.Proxymesh.Queries.GetTraffic;

namespace PMR.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TrafficController : ControllerBase {
    private readonly IMediator mediator;

    public TrafficController(IMediator mediator) {
        this.mediator = mediator;
    }

    [HttpGet("proxyMesh")]
    public async Task<ActionResult<string>> traffic() {
        var traffic = await mediator.Send(new GetProxymeshTrafficQuery());
        return Ok(traffic);
    }
}