﻿using RedSocial.Application.ViewModel.Posts;
using RedSocial.Domain.Entities;

namespace RedSocial.Application.Interfaces.Services
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<Post> CreatePostAsync(Post post);
        Task<bool> UpdatePostAsync(Post post);
        Task<bool> DeletePostAsync(int id);
        Task<List<PostViewModel>> GetPostsByUserIdAsync(int userId);
    }
}
