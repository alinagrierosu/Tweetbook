using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetbook.Contracts.v1.Requests.Queries;
using Tweetbook3.Data;
using Tweetbook3.Domain;
using static Tweetbook.Contracts.v1.ApiRoutes;

namespace Tweetbook3.Services
{
    public class PostService : IPostService
    {
        private readonly DataContext _dataContext;

        public PostService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Post> GetPostByIdAsync(string postId)
        {
            return await _dataContext.Posts.SingleOrDefaultAsync(x => x.Id == postId);
        }

        public async Task<List<Post>> GetPostsAsync(GetAllPostsQuery getAllQuery = null, PaginationFilter paginationFilter = null)
        {
            var queryable = _dataContext.Posts.AsQueryable();
            if (paginationFilter== null)
            {
                return await queryable.Include(x => x.Tags).ToListAsync();
            }

            if (getAllQuery != null && !string.IsNullOrEmpty(getAllQuery.UserId))
            {
                queryable = queryable.Where(x => x.UserId == getAllQuery.UserId);
            }

            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
            return await queryable.Include(x => x.Tags)
                .Skip(skip).Take(paginationFilter.PageSize).ToListAsync();

        }

        public async Task<bool> CreatePostAsync(Post post)
        {
            await _dataContext.AddAsync(post);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> UpdatePostAsync(Post postToUpdate)
        {
            _dataContext.Posts.Update(postToUpdate);
            var updated = await _dataContext.SaveChangesAsync();

            return updated > 0;
        }

        public async Task<bool> DeletePostAsync(string postId)
        {
            var post = await GetPostByIdAsync(postId);
            _dataContext.Posts.Remove(post);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<bool> UserOwnsPostAsync(string postId, string userId)
        {
            var post = await _dataContext.Posts.AsNoTracking().SingleOrDefaultAsync(x => x.Id == postId);
            if(post == null)
            {
                return false;       
            }

            if(post.UserId != userId)
            {
                return false;
            }

            return true;
        }

        public async Task<List<Tag>> GetAllTagsAsync()
        {
            return await _dataContext.Tags.ToListAsync();
        }
    }
}
