using BdAirport.Bd;
using BdAirport.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BdAirport.Services
{
    public class PostService
    {
        ApplicationContext bd;
        private readonly ILogger<PostController> logger;

        DbSet<Post> postBd { get => bd.Post; }


        public PostService(ILogger<PostController> l, ApplicationContext context)
        {
            logger = l;
            bd = context;
        }

        public async Task<ActionResult<IEnumerable<Post>>> GetPostList()
        {
            return await postBd.AsNoTracking().ToListAsync();
        }

        public async Task<Post> GetPost(int id)
        {
            Post post = await postBd.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (post == null)
            {
                throw new Exception("Post");
            }
            return post;
        }

        public async Task<int> AddPost(PostCreate post)
        {
            Post newPost = new Post
            {
                Position = post.Position,
            };

            if (newPost == null || post.Position == null)
            {
                throw new Exception("Post");
            }

            EntityEntry<Post> ent = await postBd.AddAsync(newPost);
            EntityState st = ent.State;

            if (st != EntityState.Added)
            {
                throw new Exception("Post");
            }
            await bd.SaveChangesAsync();
            return ent.Entity.Id;
        }

        public async Task UpdatePost(Post post)
        {
            if (post == null || !postBd.Any(x => x.Id == post.Id) || post.Position == null)
            {
                throw new Exception("Post");
            }

            Post upPost = postBd.Find(post.Id);
            upPost.Position = post.Position;

            await bd.SaveChangesAsync();
        }

        public async Task DeletePost(int id)
        {
            Post post = postBd.FirstOrDefault(o => o.Id == id);
            if (post == null)
            {
                throw new Exception("Post");
            }

            EntityEntry<Post> ent = postBd.Remove(post);
            EntityState st = ent.State;

            if (st != EntityState.Deleted)
            {
                throw new Exception("Post");
            }
            await bd.SaveChangesAsync();
        }
    }
}
