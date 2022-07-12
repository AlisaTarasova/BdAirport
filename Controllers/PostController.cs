using BdAirport.Bd;
using BdAirport.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BdAirport.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly ILogger<PostController> ilogger;
        private PostService postService;

        public PostController(ILogger<PostController> logger, PostService ps)
        {
            ilogger = logger;
            postService = ps;
        }

        /// <summary>
        /// Получить список должностей
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> ReadList()
        {
            return await postService.GetPostList();
        }

        /// <summary>
        /// Получить должность по Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> Read(int id)
        {
            Post post = await postService.GetPost(id);
            return Ok(post);
        }

        /// <summary>
        /// Добавить должность
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Post>> Create(PostCreate post)
        {
            int postId = await postService.AddPost(post);
            return Ok($"Должность добавлена под Id = {postId}!");
        }

        /// <summary>
        /// Изменить должность
        /// </summary>
        [HttpPut]
        public async Task<ActionResult<Post>> Update(Post post)
        {
            await postService.UpdatePost(post);
            return Ok("Данные о должности обновлены!");
        }

        /// <summary>
        /// Удалить должность по Id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Post>> Delete(int id)
        {
            await postService.DeletePost(id);
            return Ok("Должность удалена!");
        }
    }
}
