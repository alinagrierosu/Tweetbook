using System.Collections.Generic;
using System.Threading.Tasks;
using Tweetbook3.Domain;

namespace Tweetbook3.Services
{
    public interface IPostService
    {
        Task<List<Post>> GetPostsAsync();
        Task<Post> GetPostByIdAsync(string postId);
        Task<bool> UpdatePostAsync(Post postToUpdate);
        Task<bool> DeletePostAsync(string postId);
        Task<bool> CreatePostAsync(Post post);
        Task<bool> UserOwnsPostAsync(string postId, string userPost);
        Task<List<Tag>> GetAllTagsAsync();
    }
}
