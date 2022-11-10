using FluentAssertions;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Tweetbook3.Contracts.Requests;
using Tweetbook3.Contracts.v1;
using Tweetbook3.Domain;
using Xunit;

namespace Tweetbook3.IntegrationTests
{
    public class PostControllerTest : IntegrationTest
    {
        [Fact]
        public async Task GetAll_WithoutAnyPosts_ReturnsEmptyResponse()
        {
            //Arrange
            await AuthenticateAsync();

            //Act
            var response = await TestClient.GetAsync(ApiRoutes.Posts.GetAll);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<List<Post>>()).Should().BeEmpty();
        }

        [Fact]
        public async Task Get_WhenPostExistsInTheDatabase_ReturnsPost()
        {
            //Arrange
            await AuthenticateAsync();
            var createdPost = await CreatePostAsync(new CreatePostRequest
            {
                Name = "Test post"
            });

            //Act
            var response = await TestClient.GetAsync(ApiRoutes.Posts.Get.Replace("{postId}", createdPost.Id));

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var returnedPost = await response.Content.ReadAsAsync<Post>();
            returnedPost.Id.Should().Be(createdPost.Id);
            returnedPost.Name.Should().Be("Test post");
        }
    }
}
