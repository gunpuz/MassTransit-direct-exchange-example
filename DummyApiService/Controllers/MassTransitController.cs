using DummyApiService.Extensions;
using GreenPipes;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DummyApiService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MassTransitController : ControllerBase
    {
        private readonly IRequestClient<Models.Request> client;

        public MassTransitController(
            IBus bus
        )
        {
            var addr = new Uri($"exchange:my-exchange?type=direct&durable=false");
            client = bus.CreateRequestClient<Models.Request>(addr);
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            using var request = client.Create(new Models.Request() { foo = "baz" });
            request.UseExecute(x => x.AddRequestHeaders<Models.Response>());
            var translationResponse = await request.GetResponse<Models.Response>();

            return Ok(translationResponse.Message);
        }
    }
}
