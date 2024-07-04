using BloggingPlatform.Messages;
using BloggingPlatform.Models;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace BloggingPlatform.Controllers
{
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
        public async Task<ActionResult<List<BlogPost>>> GetBlogPosts()
        {
            var headers = Request.Headers;
            var response = await _mediator.SendRequest(new GetBlogPosts());

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<BlogPost>> PostBlogPost(CreateBlogPost blogPost)
        {
            var response = await _mediator.SendRequest(blogPost);

            return CreatedAtAction(nameof(GetBlogPost), new { id = response.Id }, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BlogPost?>> GetBlogPost(int id)
        {
            try
            {
                var response = await _mediator.SendRequest(new GetBlogPost { Id = id });

                return Ok(response);
            }
            catch(KeyNotFoundException ex)
            {
                Log.Fatal(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [HttpPost("{id}/comments")]
        public async Task<ActionResult<Comment>> AddComment(int id, [FromBody] AddComment request)
        {
            request.BlogPostId = id;

            var response = await _mediator.SendRequest(request);

            if (response is null)
            {
                return NotFound();
            }

            return CreatedAtAction(nameof(GetBlogPost), new { Id = id }, response);
        }
    }
}
