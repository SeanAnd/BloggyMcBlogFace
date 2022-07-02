using System;
using System.IO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreWithReactAndBasicAuth.Core.Models;
using NetCoreWithReactAndBasicAuth.Core.Services;
using NetCoreWithReactAndBasicAuth.Web.Models;

namespace NetCoreWithReactAndBasicAuth.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly ILogger<PostController> logger;
        private readonly IPostService postService;
        protected readonly IMapper mapper;


        public PostController(ILogger<PostController> logger, IPostService postService, IMapper mapper)
        {
            this.logger = logger;
            this.postService = postService;
            this.mapper = mapper;
        }

        public string userId => User?.Identity?.Name ?? "0";

        [HttpGet]
        public async Task<List<PostViewModel>> Get()
        {
            return MapToViewModel(await postService.Get());
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create([FromForm] PostViewModel postVM)
        {
            var mappedPost = await MapToCoreModelAsync(postVM);
            mappedPost.UserId = int.Parse(userId);
            Post? post = await postService.Create(mappedPost);
            if (post == null)
            {
                return BadRequest();
            }
            return Ok(post);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id, [FromForm]PostViewModel postVM)
        {
            Post? post = await postService.GetById(id);
            if (post == null || post?.UserId != int.Parse(userId) || postVM?.UserId != int.Parse(userId))
            {
                return BadRequest();
            }
            //if the files have changed, delete the old ones.
            if(postVM.Photos?.Count > 0 && postVM.Photos[0].File != null)
            {
                CleanupPhotos(post.Photos);
            }
            var mappedPost = await MapToCoreModelAsync(postVM);
            await postService.Update(mappedPost);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Post? post = await postService.GetById(id);
            if (post == null || post?.UserId != int.Parse(userId))
            {
                return BadRequest();
            }
            CleanupPhotos(post.Photos);
            await postService.Delete(post);
            return NoContent();
        }

        private async Task<Post> MapToCoreModelAsync(PostViewModel viewModel)
        {
            var mappedModel = mapper.Map<Post>(viewModel);
            List<Photo> photos = new List<Photo>();
            string currentPath = Path.Combine(Directory.GetCurrentDirectory());

            if (!Directory.Exists(currentPath))
                Directory.CreateDirectory(currentPath);


            foreach (var photo in viewModel.Photos)
            {
                if (photo.File?.Length > 0)
                {
                    string fileName = Path.GetFileNameWithoutExtension(photo.File.FileName).Substring(0, 10)
                    + DateTime.UtcNow.ToString("mmyyssffff") + Path.GetExtension(photo.File.FileName);
                    string filePath = Path.Combine(currentPath, "Files", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await photo.File.CopyToAsync(stream);
                    }
                    photos.Add(new Photo
                    {
                        Name = fileName,
                        Path = string.Format("{0}://{1}{2}/Files/{3}",Request.Scheme,Request.Host,Request.PathBase,fileName)
                    });
                }
                if(!string.IsNullOrEmpty(photo.Path) && photo.File == null)
                {
                    photos.Add(new Photo
                    {
                        Id = photo.Id,
                        Name = photo.Name,
                        Path = photo.Path,
                        PostId = photo.PostId,
                    });
                }
            }
            mappedModel.Photos = photos;

            return mappedModel;
        }

        private List<PostViewModel> MapToViewModel(IEnumerable<Post> coreModels)
        {
            return mapper.Map<List<PostViewModel>>(coreModels);
        }

        private void CleanupPhotos(List<Photo> photos)
        {
            photos.ForEach(photo =>
            {
                DeleteImage(photo.Name);
            });
        }

        private void DeleteImage(string name)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Files", name);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}