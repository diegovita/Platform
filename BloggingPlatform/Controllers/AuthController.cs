using BloggingPlatform.Dto;
using BloggingPlatform.Messages;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using Serilog;

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Login([FromQuery] LoginDto loginDto)
        {
            try
            {
                var token = await _mediator.SendRequest(new GetToken { Username = loginDto.Username, Password = loginDto.Password });

                if (string.IsNullOrEmpty(token.Token))
                    return Unauthorized("Invalid username or password.");

                return Ok(new { token.Token });
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
            }
            
        }
    }
}

