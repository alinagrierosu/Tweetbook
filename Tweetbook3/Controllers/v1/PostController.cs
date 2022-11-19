using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetbook.Contracts.Helpers;
using Tweetbook.Contracts.Requests;
using Tweetbook.Contracts.Responses;
using Tweetbook.Contracts.v1;
using Tweetbook.Contracts.v1.Requests.Queries;
using Tweetbook.Contracts.v1.Responses;
using Tweetbook3.Cache;
using Tweetbook3.Domain;
using Tweetbook3.Extensions;
using Tweetbook3.Services;

namespace Tweetbook3.Controllers.v1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    public class PostsController : Controller
    {
        private readonly IPostService _postService;
        private readonly IUriService _uriService;

        public PostsController(IPostService postService, IUriService uriService)
        {
            _postService = postService;
            _uriService = uriService;
        }

        [HttpGet(ApiRoutes.Posts.GetAll)]
        [Cached(600)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllPostsQuery getAllQuery, [FromQuery]PaginationQuery paginationQuery)
        {
            var pagination = new PaginationFilter
            {
                PageNumber = paginationQuery.PageNumber,
                PageSize = paginationQuery.PageSize
            };

            var posts = await _postService.GetPostsAsync(getAllQuery, pagination);
            var postResponses = posts.Select(post => new PostResponse
            {
                Id = post.Id,
                Name = post.Name,
                Tags = post.Tags.Select(x => new TagResponse { Name = x.TagName }).ToList()
            });

            if (pagination.PageNumber < 1 || pagination.PageSize < 1)
            {
                return Ok(new PagedResponse<PostResponse>(postResponses));
            }

            var paginationResponse = PaginationHelpers.CreatePaginatedResponse<PostResponse>(_uriService, pagination, postResponses);
            return Ok(paginationResponse);
        }

        [HttpGet(ApiRoutes.Posts.Get)]
        [Cached(600)]
        public async Task<IActionResult> Get([FromRoute] string postId)
        {
            var result = await _postService.GetPostByIdAsync(postId);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(new Response<PostResponse>(new PostResponse
            {
                Id = result.Id,
                Name = result.Name,
                Tags = result.Tags.Select(x => new TagResponse { Name = x.TagName }).ToList()
            }));
        }

        [HttpPut(ApiRoutes.Posts.Update)]
        public async Task<IActionResult> Update([FromRoute] string postId, [FromBody] UpdatePostRequest postRequest)
        {
            var userOwnsPost = await _postService.UserOwnsPostAsync(postId, HttpContext.GetUserId());
            if (!userOwnsPost)
            {
                return BadRequest(new { error = "You do not own this post" });
            }

            var post = await _postService.GetPostByIdAsync(postId);
            post.Name = postRequest.Name;

            var updated = await _postService.UpdatePostAsync(post);
            if (updated)
                return Ok(new Response<Post>(post));

            return NotFound();
        }


        /// <summary>
        /// Creates a post in the system
        /// </summary>
        /// <response code="201">Creates a post in the system</response>
        /// <response code="400">Unable to create the tag due to validation error</response>
        [HttpPost(ApiRoutes.Posts.Create)]
        [ProducesResponseType(typeof(PostResponse), 201)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest postRequest)
        {
            var newPostId = Guid.NewGuid().ToString();
            var post = new Post
            {
                Id = newPostId,
                Name = postRequest.Name,
                UserId = HttpContext.GetUserId(),
                Tags = postRequest.Tags.Select(x => new PostTag { PostId = newPostId, TagName = x.TagName }).ToList()
            };
            await _postService.CreatePostAsync(post);
            var locationUri = _uriService.GetPostUri(post.Id.ToString());
            var response = new PostResponse
            {
                Id = post.Id,
                Name = post.Name,
                UserId = post.UserId,
                Tags = post.Tags.Select(x => new TagResponse { Name = x.TagName }).ToList()
            };
            return Created(locationUri, new Response<PostResponse>(response));
        }

        [HttpDelete(ApiRoutes.Posts.Delete)]
        [Authorize(Policy = "MustWorkForChapsas")]
        public async Task<IActionResult> Delete([FromRoute] string postId)
        {
            var userOwnsPost = await _postService.UserOwnsPostAsync(postId, HttpContext.GetUserId());
            if (!userOwnsPost)
            {
                return BadRequest(new { error = "You do not own this post" });
            }

            var isDeleted = await _postService.DeletePostAsync(postId);

            if (!isDeleted)
                return NotFound();

            return NoContent();
        }

    }
}
