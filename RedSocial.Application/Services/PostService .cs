using RedSocial.Application.Interfaces.Repositories;
using RedSocial.Application.Interfaces.Services;
using RedSocial.Domain.Entities;


namespace RedSocial.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _postRepository.GetAllPostsAsync();
        }

        public async Task<Post> CreatePostAsync(Post post)
        {
            return await _postRepository.CreatePostAsync(post);
        }

        public async Task<bool> UpdatePostAsync(Post post)
        {
            return await _postRepository.UpdatePostAsync(post);
        }

        public async Task<bool> DeletePostAsync(int id)
        {
            return await _postRepository.DeletePostAsync(id);
        }
    }
}
