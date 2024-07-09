using BloggingPlatform.Messages;
using BloggingPlatform.Models;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace BloggingPlatform.Controllers;

[Authorize]
[Route("api/posts")]
[ApiController]
public class BlogPostsController : ControllerBase
{
    private readonly IMediator _mediator;

    public BlogPostsController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<BlogPost>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<List<BlogPost>>> GetBlogPosts()
    {
        try
        {
            var response = await _mediator.SendRequest(new GetBlogPosts());

            return Ok(response);
        }
        catch(Exception ex)
        {
            Log.Fatal(ex.Message, StatusCode(500));

            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
        }

        
    }

    [HttpPost]
    [ProducesResponseType(typeof(BlogPost), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BlogPost>> PostBlogPost(CreateBlogPost blogPost)
    {
        try
        {
            var response = await _mediator.SendRequest(blogPost);
            return CreatedAtAction(nameof(GetBlogPost), new { id = response.Id }, response);
        }
        catch(Exception ex)
        {
            Log.Fatal(ex.Message, StatusCode(500));
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BlogPost), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BlogPost?>> GetBlogPost(int id)
    {
        try
        {
            var response = await _mediator.SendRequest(new GetBlogPost { Id = id });

            if(response.Id != 0)
                return Ok(response);

            return NotFound($"Blogpost with ID {id} not found.");

        }
        catch(Exception ex)
        {
            Log.Fatal(ex.Message, StatusCode(500));

            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
        }
    }

    [HttpPost("{id}/comments")]
    [ProducesResponseType(typeof(Comment), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Comment>> AddComment(int id, [FromBody] AddComment request)
    {
        try
        {
            request.BlogPostId = id;

            var response = await _mediator.SendRequest(request);

            if (response.Id == 0)
            {
                return BadRequest($"Blogpost with ID {id} not found.");
            }

            return CreatedAtAction(nameof(GetBlogPost), new { Id = id }, response);
        }
        catch(Exception ex)
        {
            Log.Fatal(ex.Message, StatusCode(500));

            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
        }
    }
}
