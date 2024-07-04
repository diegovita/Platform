using BloggingPlatform.Data;
using BloggingPlatform.Messages;
using BloggingPlatform.Models;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace BloggingPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginModel loginModel)
        {
            var token = await _mediator.SendRequest(new GetToken { Username = loginModel.Username, Password = loginModel.Password });

            return Ok(new { token.Token });
        }
    }
}

